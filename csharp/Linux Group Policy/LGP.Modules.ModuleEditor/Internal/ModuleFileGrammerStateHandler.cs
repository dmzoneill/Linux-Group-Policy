#region

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Modules.ModuleEditor.Internal
{
    internal class ModuleFileGrammerStateHandler
    {
        private static readonly Regex GrammerMethodRegex;
        private static readonly Regex GrammerMethodParamsRegex;
        private readonly IModule _filestate;
        private readonly List< ModuleFileGrammerState > _grammerStates;
        private readonly IModule _module;

        static ModuleFileGrammerStateHandler()
        {
            GrammerMethodRegex = new Regex( "addGrammer\\s*\\((.*?)\\)\\s*;" , RegexOptions.Singleline );
            GrammerMethodParamsRegex = new Regex( "(\"(.*?)\")" , RegexOptions.Singleline); //  (\"|')(.*?)(\"|')
        }

        public ModuleFileGrammerStateHandler( IModule state , IModule module )
        {
            try
            {
                this._grammerStates = new List< ModuleFileGrammerState >();
                this._module = module;
                this._filestate = state;
                this.Prepare();
                this.SynchronizeDbWithStates();
                this.Save();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        public void Prepare()
        {
            try
            {
                foreach( var grammer in this._module.GetAllGrammers() )
                {
                    this._grammerStates.Add( new ModuleFileGrammerState( grammer , this._module ) );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        public void SynchronizeDbWithStates()
        {
            try
            {
                var grammerMethodsCollection = GrammerMethodRegex.Matches( this._filestate.FileContents );

                var foundGrammers = new List< KeyValuePair< string , string > >();

                if( grammerMethodsCollection.Count > 0 )
                {
                    foreach( Match grammerMethodMatch in grammerMethodsCollection )
                    {
                        var grammerParamatersCollection = GrammerMethodParamsRegex.Matches( grammerMethodMatch.Groups[ 1 ].ToString() );

                        if( grammerParamatersCollection.Count > 0 )
                        {
                            for( var h = 0; h < grammerParamatersCollection.Count; h += 2 )
                            {
                                var key = grammerParamatersCollection[ h ].Groups[ 2 ].ToString();
                                var value = grammerParamatersCollection[ h + 1 ].Groups[ 2 ].ToString();
                                var found = false;

                                foundGrammers.Add( new KeyValuePair< string , string >( key , value ) );

                                foreach( var grammerState in this._grammerStates )
                                {
                                    if( grammerState.GetKey().CompareTo( key ) == 0 && grammerState.GetValue().CompareTo( Framework.Utils.Base64Encode( value ) ) == 0 )
                                    {
                                        grammerState.SetDeleted( false );
                                        found = true;
                                    }
                                }

                                if( !found )
                                {
                                    this._grammerStates.Add( new ModuleFileGrammerState( this._module , key , value ) );
                                }
                            }
                        }
                    }
                }

                for( var g = 0; g < this._grammerStates.Count; g++ )
                {
                    var found = false;

                    foreach( var foundGrammer in foundGrammers )
                    {
                        if( foundGrammer.Key.CompareTo( this._grammerStates[ g ].GetKey() ) == 0 )
                        {
                            found = true;
                        }
                    }

                    if( !found )
                    {
                        this._grammerStates[ g ].SetDeleted( true );
                    }
                }

                for( var g = 0; g < this._grammerStates.Count; g++ )
                {
                    var found = false;

                    foreach( var foundGrammer in foundGrammers )
                    {
                        var temp = Framework.Utils.Base64Encode(foundGrammer.Value);
                        if (temp.CompareTo(this._grammerStates[g].GetValue()) == 0)
                        {
                            found = true;
                        }
                    }

                    if( !found )
                    {
                        this._grammerStates[ g ].SetDeleted( true );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        public List< ModuleFileGrammerState > GetAllGrammer()
        {
            return this._grammerStates;
        }


        public IGrammer GetGrammer( int id )
        {
            try
            {
                for( var g = 0; g < this._grammerStates.Count; g++ )
                {
                    if( this._grammerStates[ g ].GetId() == id )
                    {
                        return this._grammerStates[ g ];
                    }
                }
                return null;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }


        public IGrammer GetGrammer( string key )
        {
            try
            {
                for( var g = 0; g < this._grammerStates.Count; g++ )
                {
                    if( this._grammerStates[ g ].GetKey().CompareTo( key ) == 0 )
                    {
                        return this._grammerStates[ g ];
                    }
                }
                return null;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }

        public void Save()
        {
            try
            {
                for( var g = 0; g < this._grammerStates.Count; g++ )
                {
                    this._grammerStates[ g ].Save();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        public void DeleteAllGrammer()
        {
            try
            {
                for( var g = 0; g < this._grammerStates.Count; g++ )
                {
                    this._grammerStates[ g ].SetDeleted( true );
                }
                this.Save();
                this._grammerStates.Clear();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        public IGrammer AddGrammer( string key , string value )
        {
            try
            {
                var grammer = new ModuleFileGrammerState( this._module , key , value );
                this._grammerStates.Add( grammer );
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
                for( var g = 0; g < this._grammerStates.Count; g++ )
                {
                    if( this._grammerStates[ g ].GetKey().CompareTo( key ) == 0 )
                    {
                        this._grammerStates[ g ].SetDeleted( true );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}