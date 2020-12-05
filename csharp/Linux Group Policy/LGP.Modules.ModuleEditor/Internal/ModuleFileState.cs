#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Modules.ModuleEditor.Internal
{
    /// <summary>
    /// </summary>
    public class ModuleFileState : IModule
    {
        private static readonly IModuleGateway ModuleGateway;
        private static readonly Regex ModuleNameRegex;
        private readonly IModule _databaseModuleEntry;
        private readonly ModuleFileGrammerStateHandler _grammerhandler;
        private int _contentsHash;
        private string _fileContents;
        private string _fileLocation;
        private string _moduleName;
        private List< IModuleObserver > _observers;
        private bool _saving;

        static ModuleFileState()
        {
            ModuleNameRegex = new Regex( "package\\s+(.*?)\\s*;" );
            ModuleGateway = Framework.Database.CreateModuleGateway();
        }


        /// <summary>
        /// </summary>
        /// <param name = "fileLocation"></param>
        /// <param name = "fileText"></param>
        public ModuleFileState( string fileLocation , string fileText )
        {
            try
            {
                this._observers = new List< IModuleObserver >();
                this._fileLocation = fileLocation;
                this._fileContents = fileText;
                this._contentsHash = this._fileContents.GetHashCode();
                this.ScanForPackageName();

                this._databaseModuleEntry = ModuleGateway.GetModuleByName( this._moduleName );

                if( this._databaseModuleEntry == null )
                {
                    this._databaseModuleEntry = ModuleGateway.CreateModule( this._moduleName );
                }

                this._grammerhandler = new ModuleFileGrammerStateHandler( this , this._databaseModuleEntry );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Moves the file to the new location
        /// </summary>
        /// <param name = "location">string</param>
        public void SetModuleLocation( string location )
        {
            try
            {
                this._fileLocation = location;
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Saves all changes to the file and database
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                this._saving = true;
                this._contentsHash = this._fileContents.GetHashCode();
                this.ScanForPackageName();
                this._grammerhandler.Save();
                this.Save();
                this._saving = false;
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void ScanForPackageName()
        {
            try
            {
                var match = ModuleNameRegex.Match( this._fileContents );
                var package = match.Groups[ 1 ].ToString();

                if( this._saving )
                {
                    this._databaseModuleEntry.SetModuleName( package );
                }

                this._moduleName = package;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void Save()
        {
            try
            {
                var client = ModuleHandler.GetClient();

                if( client != null )
                {
                    client.Connect();
                    client.SaveModule( this._fileLocation , this._fileContents );
                    client.Disconnect();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void OnPropertyChanged( string info )
        {
            try
            {
                this._contentsHash = this._fileContents.GetHashCode();
                var handler = this.PropertyChanged;
                if( handler != null )
                {
                    handler( this , new PropertyChangedEventArgs( info ) );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region Implementation of IModuleObserver

        /// <summary>
        ///   Update this observer with a refernece to the module
        /// </summary>
        /// <param name = "module">IModule</param>
        /// <param name = "source">IModuleObserver</param>
        public void Update( IModule module , IModuleObserver source )
        {
        }

        /// <summary>
        ///   Attach objewcts that observer this module
        /// </summary>
        /// <param name = "obj">IModuleObserver</param>
        public void Attach( IModuleObserver obj )
        {
            try
            {
                if( !this._observers.Contains( obj ) )
                {
                    this._observers.Add( obj );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Detach objewcts that observer this module
        /// </summary>
        /// <param name = "obj">IModuleObserver</param>
        public void Detach( IModuleObserver obj )
        {
            try
            {
                this._observers.Remove( obj );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Notify observers of this module
        /// </summary>
        public void Notify()
        {
            try
            {
                for( var x = 0; x < this._observers.Count; x++ )
                {
                    this._observers[ x ].Update( this , this );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Dispose this object and all the observers
        /// </summary>
        /// <param name = "obj">IModuleObserver</param>
        public void Dispose( IModuleObserver obj )
        {
            try
            {
                var i = this._observers.Count - 1;
                while( i > -1 )
                {
                    this._observers[ i ].Dispose( this );
                    i = this._observers.Count - 1;
                }
                this._observers = null;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion

        #region Implementation of IModule

        /// <summary>
        ///   Returns the module id
        /// </summary>
        /// <returns>int</returns>
        public int GetModuleId()
        {
            return this._databaseModuleEntry.GetModuleId();
        }

        /// <summary>
        ///   Adds a grammer to this module
        /// </summary>
        /// <param name = "key">string</param>
        /// <param name = "value">string</param>
        public IGrammer AddGrammer( string key , string value )
        {
            try
            {
                var grammer = this._grammerhandler.AddGrammer( key , this._moduleName );
                this.Notify();
                return grammer;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }

        /// <summary>
        ///   Deletes a grammer given a key
        /// </summary>
        /// <param name = "key"></param>
        public void DeleteGrammer( string key )
        {
            try
            {
                this._grammerhandler.DeleteGrammer( key );
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Deletes all the grammers associated with this module
        /// </summary>
        public void DeleteAllGrammer()
        {
            try
            {
                this._grammerhandler.DeleteAllGrammer();
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Gets all the grammers for this module
        /// </summary>
        /// <returns>List ModuleFileGrammerState</returns>
        public List< IGrammer > GetAllGrammers()
        {
            try
            {
                var grammer = new List< IGrammer >();
                foreach( var grammerstate in this._grammerhandler.GetAllGrammer() )
                {
                    if( grammerstate.IsDeleted() == false )
                    {
                        grammer.Add( grammerstate );
                    }
                }
                return grammer;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }

        /// <summary>
        ///   Gets a grammer given a key
        /// </summary>
        /// <param name = "key">grammer lvalue</param>
        /// <returns>IGrammer</returns>
        public IGrammer GetGrammer( string key )
        {
            return this._grammerhandler.GetGrammer( key );
        }

        /// <summary>
        ///   Gets a grammer given an id
        /// </summary>
        /// <param name = "id">grammer id</param>
        /// <returns>IGrammer</returns>
        public IGrammer GetGrammer( int id )
        {
            return this._grammerhandler.GetGrammer( id );
        }

        /// <summary>
        ///   Document associated with this object
        /// </summary>
        public string FileContents
        {
            get
            {
                return this._fileContents;
            }
            set
            {
                try
                {
                    this._fileContents = value;
                    this._grammerhandler.SynchronizeDbWithStates();
                    this.SaveChanges();
                    this.Notify();
                    this.OnPropertyChanged( "DocumentChanged" );
                }
                catch( Exception error )
                {
                    Framework.EventBus.Publish( error );
                }
            }
        }


        /// <summary>
        ///   Gets the module package name
        /// </summary>
        /// <returns>string</returns>
        public string GetModuleName()
        {
            return this._moduleName;
        }


        /// <summary>
        ///   Sets the module package name
        /// </summary>
        /// <returns>string</returns>
        public void SetModuleName( string packagename )
        {
            try
            {
                var currentpackagename = string.Copy( this._moduleName );
                this._moduleName = packagename;
                this.FileContents = this.FileContents.Replace( currentpackagename , packagename );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Gets the location of the file on the disk
        /// </summary>
        /// <returns>string</returns>
        public string GetModuleLocation()
        {
            return this._fileLocation;
        }

        /// <summary>
        ///   Gets the file hash
        /// </summary>
        public int GetHash()
        {
            return this._contentsHash;
        }

        /// <summary>
        ///   Gets the name of the module
        /// </summary>
        /// <returns>string</returns>
        public string GetName()
        {
            return this._databaseModuleEntry.GetModuleName();
        }

        /// <summary>
        ///   Sets the module name
        /// </summary>
        /// <param name = "name"></param>
        public void SetName( string name )
        {
            try
            {
                this._databaseModuleEntry.SetModuleName( this._moduleName );
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion

        #region Implementation of INotifyPropertyChanged

        /// <summary>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}