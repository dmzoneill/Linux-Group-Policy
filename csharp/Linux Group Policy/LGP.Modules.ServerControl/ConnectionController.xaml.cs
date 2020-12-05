#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Module;
using LGP.Components.Factory.Interfaces.Network;
using LGP.Components.Factory.Internal.ServerControl;

#endregion

namespace LGP.Modules.ServerControl
{
    /// <summary>
    /// Interaction logic for ConnectionController.xaml
    /// </summary>
    public partial class ConnectionController : IUserControl
    {
        private static ConnectionController _instance;
        private readonly Timer _refreshTicker;
        private readonly List< IServerInfo > _serverList;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionController()
        {
            try
            {
                this.InitializeComponent();

                this.Tag = Properties.Resources.ServerControl;

                this.ServerImage.Source = Framework.Images.GetImage( "Networkgroup" , "VS2010ImageLibrary" ).Source;

                this._serverList = new List< IServerInfo >();
                this._refreshTicker = new Timer
                {
                    Enabled = true
                };

                this._refreshTicker.Elapsed += this.RefreshTickerElapsed;
                this._refreshTicker.Interval = 2000;
                this._refreshTicker.Start();

                Framework.ShutDown += this.Shutdown;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void Shutdown( object sender , CancelEventArgs e )
        {
            try
            {
                if( this.ConnectionViewerContainer.Child != null )
                {
                    var connectionViewer = this.ConnectionViewerContainer.Child as ConnectionViewer;
                    if( connectionViewer != null )
                    {
                        connectionViewer.StopStressTester();
                    }
                }

                this._refreshTicker.Stop();
            }
            catch( Exception )
            {
            }
        }


        private bool InspectItems( string server )
        {
            try
            {
                foreach( var t in this._serverList )
                {
                    if( t.ServerAddress.CompareTo( server ) == 0 )
                    {
                        return true;
                    }
                }
                return false;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return false;
            }
        }


        private void RefreshTickerElapsed( object sender , ElapsedEventArgs e )
        {
            try
            {
                var update = false;

                var servers = Framework.Network.GetServers();

                foreach( var t in servers )
                {
                    if( this.InspectItems( t.ServerAddress ) == false )
                    {
                        update = true;
                    }
                }


                this.ServerListBox.Dispatcher.BeginInvoke( DispatcherPriority.Normal , ( Action ) ( delegate
                {
                    if( update )
                    {
                        this._serverList.Clear();

                        foreach( var t in servers )
                        {
                            var n = new ServerInfo
                            {
                                LastSeen = t.LastSeen ,
                                ServerAddress = t.ServerAddress
                            };
                            this._serverList.Add( n );
                        }
                        this.ServerListBox.DataContext = this._serverList;
                    }
                } ) );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        /// Gets the instance of this usercontrol
        /// </summary>
        /// <returns>ConnectionController</returns>
        public static ConnectionController GetInstance()
        {
            return _instance ?? ( _instance = new ConnectionController() );
        }

        private void ConnectServerButtonClick( object sender , RoutedEventArgs e )
        {
            try
            {
                if( this.textBox1.Text.CompareTo( "" ) != 0 )
                {
                    Framework.Network.SetServerAddress( this.textBox1.Text );
                    this.ConnectionViewerContainer.Child = new ConnectionViewer();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void SelectServerButtonClick( object sender , RoutedEventArgs e )
        {
            try
            {
                if( this.ServerListBox.SelectedItem != null )
                {
                    Framework.Network.SetServerAddress( this.ServerListBox.SelectedItem.ToString() );
                    this.ConnectionViewerContainer.Child = new ConnectionViewer();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void ServerListNodeDoubleClick( object sender , MouseButtonEventArgs e )
        {
            try
            {
                if( this.ServerListBox.SelectedIndex != -1 )
                {
                    Framework.Network.SetServerAddress( this.ServerListBox.SelectedItem.ToString() );
                    this.ConnectionViewerContainer.Child = new ConnectionViewer();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region Implementation of IUserControl

        /// <summary>
        ///   Aks the UserControl to do some clean up!
        /// </summary>
        public void Dispose()
        {
            try
            {
                this._refreshTicker.Stop();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        /// Asks the Usercontrol to refresh itself
        /// </summary>
        public void Refresh()
        {
            this.Tag = Properties.Resources.ServerControl;
        }


        /// <summary>
        /// When the control is no longer in focus, asks it to pause ( heavy duty non critical, ie threads updating graphs )
        /// </summary>
        public void Pause()
        {
            try
            {
            }
            catch( Exception )
            {
            }
        }

        /// <summary>
        /// When the tab comes back into focus, resume ( heavy duty non critical, ie threads updating graphs )
        /// </summary>
        public void Resume()
        {
            try
            {
            }
            catch( Exception )
            {
            }
        }

        #endregion
    }
}