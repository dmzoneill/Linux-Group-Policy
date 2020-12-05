#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Components.Factory.Interfaces.Infrastructure;
using LGP.Components.Factory.Interfaces.Module;
using IModule = LGP.Components.Factory.Interfaces.Module.IModule;

#endregion

namespace LGP.Components.Factory.Internal
{
    /// <summary>
    /// Class library handler
    /// </summary>
    public class LibraryHandler : IClassLibraryHandler
    {
        private static LibraryHandler _instance;
        private static bool _hasError;
        private static string _lastError;
        private readonly string _assemblyLocation;
        private readonly List< IComponent > _internalComponents;
        private readonly List< IModule > _moduleComponents;
        private readonly List< Type > _modulePreferencesPanes;

        private Type _applicationMenuType;
        private Type _applicationPanelType;
        private IContextMenus _contextMenusHandler;
        private IDatabase _databaseGateway;
        private Type _databaseGatewayType;
        private Type _dialogType;

        private string[ ] _dllPaths;
        private Type _notificationsType;


        private LibraryHandler()
        {
            try
            {
                _hasError = false;
                this._moduleComponents = new List< IModule >();
                this._internalComponents = new List< IComponent >();
                this._modulePreferencesPanes = new List< Type >();
                this._assemblyLocation = Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location );

                this.LoadDllListing();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region IClassLibraryHandler Members

        /// <summary>
        ///   Gets a listing of dlls from the application directory
        /// </summary>
        public void LoadDllListing()
        {
            _hasError = false;

            try
            {
                this._dllPaths = Directory.GetFiles( this._assemblyLocation , "*.dll" );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                _hasError = true;
                _lastError = error.Message;
            }
        }


        /// <summary>
        ///   Loads a dll given a path
        /// </summary>
        /// <param name = "dllPath">The path to the dll</param>
        public void LoadIComponent( string dllPath )
        {
            _hasError = false;

            try
            {
                var assembly = Assembly.LoadFile( dllPath );

                foreach( var type in assembly.GetTypes() )
                {
                    if( !type.IsClass || type.IsNotPublic )
                    {
                        continue;
                    }

                    var interfaces = type.GetInterfaces();

                    if( interfaces.Contains( typeof( IDialog ) ) )
                    {
                        this._dialogType = type;
                    }

                    if( interfaces.Contains( typeof( INotification ) ) )
                    {
                        this._notificationsType = type;
                    }

                    if( interfaces.Contains( typeof( IMenu ) ) )
                    {
                        this._applicationMenuType = type;
                    }

                    if( interfaces.Contains( typeof( IPanel ) ) )
                    {
                        this._applicationPanelType = type;
                    }

                    if( interfaces.Contains( typeof( IContextMenus ) ) )
                    {
                        var obj = Activator.CreateInstance( type );
                        this._contextMenusHandler = ( IContextMenus ) obj;
                    }

                    if( interfaces.Contains( typeof( IModule ) ) )
                    {
                        var obj = Activator.CreateInstance( type );
                        this._moduleComponents.Add( ( IModule ) obj );
                    }

                    if( interfaces.Contains( typeof( IDatabase ) ) )
                    {
                        this._databaseGatewayType = type;
                        var obj = Activator.CreateInstance( type );
                        this._databaseGateway = ( IDatabase ) obj;
                    }

                    if( interfaces.Contains( typeof( IDatabaseModule ) ) )
                    {
                        this._databaseGateway.AddDatabaseType( type );
                    }

                    if( interfaces.Contains( typeof( IPreferences ) ) )
                    {
                        this._modulePreferencesPanes.Add( type );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                _hasError = true;
                _lastError = error.Message;
            }
        }


        /// <summary>
        ///   Get the types that realised the IModule interface
        /// </summary>
        /// <returns>List of modules</returns>
        public List< IModule > GetModules()
        {
            return this._moduleComponents;
        }


        /// <summary>
        ///   Get the types that realised the iComponent interface
        /// </summary>
        /// <returns>List of IComponents</returns>
        public List< IComponent > GetComponents()
        {
            return this._internalComponents;
        }


        /// <summary>
        ///   Check to see if there was an error
        /// </summary>
        /// <returns>bool error</returns>
        public bool HasError()
        {
            return _hasError;
        }


        /// <summary>
        ///   Get the last error occurred
        /// </summary>
        /// <returns>String error</returns>
        public string GetError()
        {
            return _lastError;
        }


        /// <summary>
        ///   Get the type that realised the Imenu interface
        /// </summary>
        /// <returns></returns>
        public Type GetMenuType()
        {
            return this._applicationMenuType;
        }


        /// <summary>
        ///   Get the type that realized the IDialog interface
        /// </summary>
        /// <returns></returns>
        public Type GetDialogType()
        {
            return this._dialogType;
        }


        /// <summary>
        ///   Get the type that realized the IPanle interface
        /// </summary>
        /// <returns></returns>
        public Type GetPanelType()
        {
            return this._applicationPanelType;
        }


        /// <summary>
        ///   Get the type that realized the IPanle interface
        /// </summary>
        /// <returns></returns>
        public Type GetNotifcationType()
        {
            return this._notificationsType;
        }


        /// <summary>
        ///   Return dlls in directory path
        /// </summary>
        /// <returns>String[] dlls</returns>
        public string[ ] GetDllPaths()
        {
            return this._dllPaths;
        }


        /// <summary>
        ///   Gets the name of an dll asembly given its path
        /// </summary>
        /// <param name = "assemblyPath">Dll path</param>
        /// <returns>Asembly name</returns>
        public string GetAssemblyName( string assemblyPath )
        {
            try
            {
                var assembly = AssemblyName.GetAssemblyName( assemblyPath );
                return assembly.Name;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }


        /// <summary>
        ///   Gets the assembly version
        /// </summary>
        /// <param name = "assemblyPath">Dll path</param>
        /// <returns>Dll version</returns>
        public string GetAssemblyVersion( string assemblyPath )
        {
            try
            {
                var assembly = AssemblyName.GetAssemblyName( assemblyPath );
                return assembly.Version.ToString();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }


        /// <summary>
        ///   Create an object of type
        /// </summary>
        /// <param name = "type">type of object you would like to create</param>
        /// <returns></returns>
        public Object CreateObject( Type type )
        {
            try
            {
                var obj = Activator.CreateInstance( type );
                return obj;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }


        /// <summary>
        ///   Create an object of type
        /// </summary>
        /// <param name = "type">type of object you would like to create</param>
        /// <param name = "o">The contructor parameter</param>
        /// <returns></returns>
        public Object CreateObject( Type type , Menu o )
        {
            try
            {
                var obj = Activator.CreateInstance( type , o );
                return obj;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }


        /// <summary>
        ///   Gets a previously created database strategy context object
        /// </summary>
        /// <returns>IDatabaseModule type</returns>
        /// <exception cref = "Exception">Exception context strategy null</exception>
        public IDatabase GetDatabaseContextHandler()
        {
            return this._databaseGateway;
        }


        /// <summary>
        ///   Recreated the database gateway given a new strategy type
        /// </summary>
        /// <returns>IDatabaseModule type</returns>
        /// <exception cref = "Exception">Exception context strategy null</exception>
        public IDatabase GetDatabaseContextHandler( Type strategyType )
        {
            try
            {
                var obj = Activator.CreateInstance( this._databaseGatewayType , strategyType );
                this._databaseGateway = ( IDatabase ) obj;
                return this._databaseGateway;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }


        /// <summary>
        ///   Gets the preferences panes associated with the modules
        /// </summary>
        /// <returns></returns>
        public List< Type > GetPreferencesPanes()
        {
            return this._modulePreferencesPanes;
        }

        /// <summary>
        ///   Gets the conext menus handler
        /// </summary>
        /// <returns></returns>
        public IContextMenus GetConextMenusHandler()
        {
            return this._contextMenusHandler;
        }

        #endregion

        /// <summary>
        /// Gets an instance of the library handler
        /// </summary>
        /// <returns></returns>
        public static LibraryHandler GetInstance()
        {
            return _instance ?? ( _instance = new LibraryHandler() );
        }
    }
}