#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Infralution.Localization.Wpf;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Module;
using LGP.Components.Factory.Interfaces.Network;
using LGP.Components.Factory.Internal.ServerControl;

#endregion

namespace LGP
{
    /// <summary>
    /// Interaction logic for Preferences.xaml
    /// </summary>
    public partial class Preferences : IPreferences
    {
        private readonly Timer _refreshTicker;
        private readonly List< IServerInfo > _serverList;
        private ISettings _parent;

        /// <summary>
        /// 
        /// </summary>
        public Preferences()
        {
            this.InitializeComponent();
            var cultures = new List< CultureInfo >( CultureInfo.GetCultures( CultureTypes.SpecificCultures ) );
            cultures.Sort( new CultureInfoComparer() );
            this._cultureCombo.ItemsSource = cultures;
            this._cultureCombo.SelectedItem = CultureManager.UICulture;

            this._serverList = new List< IServerInfo >();
            this._refreshTicker = new Timer
            {
                Enabled = true
            };

            this._refreshTicker.Elapsed += this.RefreshTickerElapsed;
            this._refreshTicker.Interval = 2000;
            this._refreshTicker.Start();
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
        /// Set the CultureManager.UICulture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CultureComboSelectionChanged( object sender , SelectionChangedEventArgs e )
        {
            try
            {
                var culture = this._cultureCombo.SelectedItem as CultureInfo;

                if( culture != null )
                {
                    CultureManager.UICulture = culture;
                    Framework.Registry.WriteKey( "Language" , culture.Name );
                    if( this._parent != null )
                    {
                        this._parent.Refresh();
                    }
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
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region Implementation of IPreferences

        /// <summary>
        ///   Gets the name of the preferences Pane
        /// </summary>
        /// <returns>string</returns>
        public string GetName()
        {
            return "LGP";
        }

        /// <summary>
        ///   Gets the preferences window
        /// </summary>
        /// <returns>UserControl</returns>
        public UserControl GetControl()
        {
            return this;
        }

        /// <summary>
        ///   Aks the preferences panel to save its entries
        /// </summary>
        public void Save()
        {
            this._parent = null;
            this._refreshTicker.Stop();
        }


        /// <summary>
        ///   Asks the preferences to load its settings
        /// </summary>
        /// <param name="settingsParent"></param>
        /// <returns>string</returns>
        public void Load( ISettings settingsParent )
        {
            this._parent = settingsParent;
        }

        /// <summary>
        /// Gets the type it Controls
        /// </summary>
        /// <returns>Type</returns>
        public Type GetControlType()
        {
            return typeof( INetwork );
        }


        /// <summary>
        /// Gets the preferences icon
        /// </summary>
        /// <returns></returns>
        public Image GetIcon()
        {
            return Framework.Images.ConvertBitmapToImage( Properties.Images.lgpico1 , 14 );
        }

        #endregion

        #region Nested type: CultureInfoComparer

        /// <summary>
        /// Handle sorting Culture Info
        /// </summary>
        private class CultureInfoComparer : Comparer< CultureInfo >
        {
            public override int Compare( CultureInfo x , CultureInfo y )
            {
                if( x != null )
                {
                    return x.DisplayName.CompareTo( y.DisplayName );
                }

                return -1;
            }
        }

        #endregion
    }
}