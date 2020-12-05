#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Threading;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Components.Factory.Interfaces.Network;
using LGP.Modules.ModuleEditor.Internal.CustomControls;
using Newtonsoft.Json;

#endregion

namespace LGP.Modules.ModuleEditor.Internal
{
    internal class ModuleHandler
    {
        private static IServerController _client;
        private static readonly Regex UseModuleRegex;
        private static List< ModuleFileState > _modulesFiles;

        static ModuleHandler()
        {
            UseModuleRegex = new Regex( "use(\\s+)base(\\s+)(\"|')Module(\"|')(\\s*);" );
        }

        public static StackPanel Panel
        {
            get;
            set;
        }


        public static IServerController GetClient()
        {
            if( _client == null )
            {
                _client = Framework.Network.CreateClient();

                if( _client != null )
                {
                    _client.OnDataReceived += ModulesReceived;
                    _client.Connect();
                    _client.FetchModules();
                    _client.Disconnect();
                }
            }

            return _client;
        }

        private static void ModulesReceived( string data )
        {
            try
            {
                if( _modulesFiles != null )
                {
                    Panel.Dispatcher.BeginInvoke( DispatcherPriority.Normal , ( Action ) ( delegate
                    {
                        var g = Panel.Children.Count - 1;

                        while( g > -1 )
                        {
                            var el = ( StackPanelModuleElement ) Panel.Children[ g ];
                            el.Dispose( el );
                            g = Panel.Children.Count - 1;
                        }

                        Panel.Children.Clear();
                    } ) );
                }

                var values = JsonConvert.DeserializeObject< Dictionary< string , string > >( data );

                _modulesFiles = new List< ModuleFileState >();

                foreach( var value in values )
                {
                    _modulesFiles.Add( new ModuleFileState( value.Key , Framework.Utils.Base64Decode( value.Value ) ) );
                }

                Panel.Dispatcher.BeginInvoke( DispatcherPriority.Normal , ( Action ) ( delegate
                {
                    Panel.Children.Clear();

                    for( var h = 0; h < _modulesFiles.Count; h++ )
                    {
                        Panel.Children.Add( new StackPanelModuleElement( _modulesFiles[ h ] , h ) );
                    }
                } ) );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        public static void CreateModule( string moduleName , StackPanel panel )
        {
            try
            {
                const string templateLocation = @"Module.template";

                var moduleFullName = moduleName + ".pm";

                foreach( var moduleFileState in _modulesFiles )
                {
                    if( moduleFileState.GetModuleName().CompareTo( moduleName ) == 0 )
                    {
                        Framework.Notification.Display( Properties.Resources.DuplicateModule , 5000 );
                        return;
                    }
                }

                var contents = File.ReadAllText( templateLocation );

                if( UseModuleRegex.IsMatch( contents ) )
                {
                    contents = contents.Replace( "{PACKAGE}" , moduleName );

                    var client = GetClient();
                    client.Connect();
                    client.SaveModule( moduleFullName , contents );
                    client.Disconnect();

                    var module = new ModuleFileState( moduleFullName , contents );
                    _modulesFiles.Add( module );
                    panel.Children.Add( new StackPanelModuleElement( module ) );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        public static void DeleteModule( IModule module )
        {
            try
            {
                for( var g = 0; g < _modulesFiles.Count; g++ )
                {
                    if( _modulesFiles[ g ].GetModuleLocation().CompareTo( module.GetModuleLocation() ) == 0 )
                    {
                        var modulename = _modulesFiles[ g ].GetModuleName();
                        var location = _modulesFiles[ g ].GetModuleLocation();
                        _modulesFiles[ g ].Dispose( module );
                        var moduleGateway = Framework.Database.CreateModuleGateway();
                        moduleGateway.DeleteModule( modulename );

                        var client = GetClient();
                        client.Connect();
                        client.DeleteModule( location );
                        client.Disconnect();

                        _modulesFiles.RemoveAt( g );

                        break;
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        public static void RefreshModuleFiles()
        {
            try
            {
                var client = GetClient();
                if( client != null )
                {
                    _client.Connect();
                    _client.FetchModules();
                    _client.Disconnect();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        public static ModuleFileState GetModuleFileByPackageName( string name )
        {
            try
            {
                for( var g = 0; g < _modulesFiles.Count; g++ )
                {
                    if( _modulesFiles[ g ].GetModuleName().CompareTo( name ) == 0 )
                    {
                        return _modulesFiles[ g ];
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
    }
}