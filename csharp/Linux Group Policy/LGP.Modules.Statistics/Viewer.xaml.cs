#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Module;

#endregion

namespace LGP.Modules.Statistics
{
    /// <summary>
    ///   Interaction logic for Viewer.xaml
    /// </summary>
    public partial class Viewer : IUserControl
    {
        private static UserControl _instance;
        private readonly Timer _timer;
        private List< KeyValuePair< string , int > > _cpu;
        private bool _enabled;
        private List< KeyValuePair< string , int > > _incoming;
        private List< KeyValuePair< string , int > > _outgoing;

        private List< KeyValuePair< string , int > > _rx;
        private List< KeyValuePair< string , int > > _tx;

        /// <summary>
        ///   Constructor
        /// </summary>
        public Viewer()
        {
            try
            {
                this.InitializeComponent();
                this._enabled = true;

                this._timer = new Timer
                {
                    Interval = 1000
                };
                this._timer.Elapsed += this.TimerElapsed;
                this._timer.Start();

                Framework.ShutDown += this.Terminate;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void Terminate( object sender , CancelEventArgs args )
        {
            try
            {
                this._timer.Stop();
            }
            catch( Exception )
            {
            }
        }

        private void TimerElapsed( object sender , ElapsedEventArgs e )
        {
            try
            {
                var servers = Framework.Network.GetServers();

                if( servers.Count == 0 )
                {
                    return;
                }

                var server = servers[ 0 ];
                var incoming = server.GetIncoming();
                var outgoing = server.GetOutgoing();
                var cpu = server.GetCpu();
                var tx = server.GetTx();
                var rx = server.GetRx();

                this._incoming = new List< KeyValuePair< string , int > >();
                this._outgoing = new List< KeyValuePair< string , int > >();
                this._tx = new List< KeyValuePair< string , int > >();
                this._rx = new List< KeyValuePair< string , int > >();
                this._cpu = new List< KeyValuePair< string , int > >();

                for( var h = 0; h < incoming.Count; h++ )
                {
                    var str = ( h + 1 ).ToString();
                    this._incoming.Add( new KeyValuePair< string , int >( str , incoming[ h ] ) );
                }

                for( var h = 0; h < outgoing.Count; h++ )
                {
                    var str = ( h + 1 ).ToString();
                    this._outgoing.Add( new KeyValuePair< string , int >( str , outgoing[ h ] ) );
                }

                for( var h = 0; h < cpu.Count; h++ )
                {
                    var str = ( h + 1 ).ToString();
                    this._cpu.Add( new KeyValuePair< string , int >( str , cpu[ h ] ) );
                }

                for( var h = 0; h < tx.Count; h++ )
                {
                    var str = ( h + 1 ).ToString();
                    this._tx.Add( new KeyValuePair< string , int >( str , tx[ h ] ) );
                }


                for( var h = 0; h < rx.Count; h++ )
                {
                    var str = ( h + 1 ).ToString();
                    this._rx.Add( new KeyValuePair< string , int >( str , rx[ h ] ) );
                }

                this.rxSeries.Dispatcher.BeginInvoke( DispatcherPriority.Normal , ( Action ) ( delegate
                {
                    this.rxSeries.ItemsSource = null;
                    this.rxSeries.ItemsSource = this._rx;
                } ) );

                this.txSeries.Dispatcher.BeginInvoke( DispatcherPriority.Normal , ( Action ) ( delegate
                {
                    this.txSeries.ItemsSource = null;
                    this.txSeries.ItemsSource = this._tx;
                } ) );

                this.LineSeries1.Dispatcher.BeginInvoke( DispatcherPriority.Normal , ( Action ) ( delegate
                {
                    this.LineSeries1.ItemsSource = null;
                    this.LineSeries1.ItemsSource = this._incoming;
                } ) );

                this.LineSeries2.Dispatcher.BeginInvoke( DispatcherPriority.Normal , ( Action ) ( delegate
                {
                    this.LineSeries2.ItemsSource = null;
                    this.LineSeries2.ItemsSource = this._outgoing;
                } ) );

                this.LineSeries3.Dispatcher.BeginInvoke( DispatcherPriority.Normal , ( Action ) ( delegate
                {
                    this.LineSeries3.ItemsSource = null;
                    this.LineSeries3.ItemsSource = this._cpu;
                } ) );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Returns this instance of the user control ( Singleton )
        /// </summary>
        /// <returns>Usercontrol Instance</returns>
        public static UserControl GetInstance()
        {
            return _instance ?? ( _instance = new Viewer() );
        }

        private void UserControlKeyDown( object sender , System.Windows.Input.KeyEventArgs e )
        {
            if( e.Key == Key.R )
            {
                Console.WriteLine( @"restarting" );
                try
                {
                    this._timer.Stop();

                    try
                    {
                        this._timer.Start();
                    }
                    catch( Exception )
                    {
                    }
                }
                catch( Exception )
                {
                    this._timer.Start();
                }
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
                this._timer.Stop();
                this._rx = null;
                this._tx = null;
                this._incoming = null;
                this._outgoing = null;
            }
            catch( Exception )
            {
            }
        }

        /// <summary>
        /// Asks the Usercontrol to refresh itself
        /// </summary>
        public void Refresh()
        {
            this.Tag = Properties.Resources.Statistics;
        }

        /// <summary>
        /// When the control is no longer in focus, asks it to pause ( heavy duty non critical, ie threads updating graphs )
        /// </summary>
        public void Pause()
        {
            try
            {
                if( this._enabled )
                {
                    this._timer.Stop();
                    this._enabled = false;
                }
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
                if( !this._enabled )
                {
                    this._timer.Start();
                    this._enabled = true;
                }
            }
            catch( Exception )
            {
            }
        }

        #endregion
    }
}