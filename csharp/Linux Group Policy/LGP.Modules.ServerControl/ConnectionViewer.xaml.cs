#region

using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using LGP.Components.Factory;

#endregion

namespace LGP.Modules.ServerControl
{
    /// <summary>
    /// Interaction logic for ConnectionViewer.xaml
    /// </summary>
    public partial class ConnectionViewer
    {
        private int _delay;
        private int _numupdates;
        private Thread _serverConnectionThread;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionViewer()
        {
            this.InitializeComponent();

            this.image1.Source = Framework.Images.GetImage( "Networkgroup" , "VS2010ImageLibrary" ).Source;
            this.ipaddr.Content = Framework.Network.GetServerAddress();
        }

        private void Slider1ValueChanged( object sender , RoutedPropertyChangedEventArgs< double > e )
        {
            try
            {
                var numupdates = ( ( int ) this.slider1.Value );

                if( numupdates == 500 )
                {
                    numupdates = 999999999;
                }

                this.numUpdates.Content = numupdates + " " + Properties.Resources.ClientUpdates;
                this._numupdates = numupdates;
            }
            catch( Exception )
            {
            }
        }

        private void PushPolicyUpdateClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var client = Framework.Network.CreateClient();
                client.Connect();
                client.SendPolicyUpdated();
                client.Disconnect();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void PushClientUpdateClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var client = Framework.Network.CreateClient();
                client.Connect();
                client.SendPushPolicies();
                client.Disconnect();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void StressTestClick( object sender , RoutedEventArgs e )
        {
            try
            {
                this.StressTest.Content = Properties.Resources.Working;
                this.StressTest.IsEnabled = false;
                this._serverConnectionThread = new Thread( this.Stressthread );
                this._serverConnectionThread.Start();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void Stressthread()
        {
            try
            {
                var client = Framework.Network.CreateClient();

                for( var h = 0; h < this._numupdates; h++ )
                {
                    client.Connect();
                    client.SendPushPolicies();
                    client.Disconnect();

                    this.threadStatusLabel.Dispatcher.BeginInvoke( DispatcherPriority.Normal , ( Action ) ( delegate
                    {
                        this.threadStatusLabel.Content = ( h + 1 ) + " " + Properties.Resources.Requests;
                    } ) );

                    Thread.Sleep( this._delay );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }

            this.StressTest.Dispatcher.BeginInvoke( DispatcherPriority.Normal , ( Action ) ( delegate
            {
                this.StressTest.Content = Properties.Resources.Go;
                this.StressTest.IsEnabled = true;
            } ) );
        }

        private void Slider2ValueChanged( object sender , RoutedPropertyChangedEventArgs< double > e )
        {
            try
            {
                this.delay.Content = ( ( int ) this.slider2.Value ) + Properties.Resources.Delay;
                this._delay = ( ( int ) this.slider2.Value );
            }
            catch( Exception )
            {
            }
        }


        /// <summary>
        /// Stops the stress tester if it is running
        /// </summary>
        public void StopStressTester()
        {
            if( this._serverConnectionThread != null )
            {
                try
                {
                    this._serverConnectionThread.Abort();
                }
                catch( Exception )
                {
                }
            }
        }

        private void PushModulesUpdateClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var client = Framework.Network.CreateClient();
                client.Connect();
                client.SendPushModules();
                client.Disconnect();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}