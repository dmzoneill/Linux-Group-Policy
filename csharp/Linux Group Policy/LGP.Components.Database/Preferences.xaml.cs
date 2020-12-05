#region

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Components.Factory.Interfaces.Module;

#endregion

namespace LGP.Components.Database
{
    /// <summary>
    ///   Interaction logic for Preferences.xaml
    /// </summary>
    public partial class Preferences : IPreferences
    {
        private IAsyncResult _aSyncResult;
        private ConnectionAttemptDelegate _attemptDelegate;
        private ISettings _parent;


        /// <summary>
        ///   Constructor
        /// </summary>
        public Preferences()
        {
            try
            {
                this.InitializeComponent();
                this.passwordBox.PasswordChar = '*';
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

       
        private void DatabaseTypeComboBoxSelectionChanged( object sender , SelectionChangedEventArgs e )
        {
            try
            {
                this.suggestionDatabaseType.Foreground = this.databaseTypeComboBox.SelectedIndex > -1 ? Brushes.DarkSlateGray : Brushes.DarkMagenta;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void ConnectButtonClick( object sender , RoutedEventArgs e )
        {
            try
            {
                if( this.databaseTypeComboBox.SelectedIndex > -1 )
                {
                    var dbType = ( Type ) this.databaseTypeComboBox.Items[ this.databaseTypeComboBox.SelectedIndex ];
                    this._attemptDelegate = this.AtttemptDatabaseConnection;

                    this.databaseTypeComboBox.IsEnabled = false;
                    this.hostnameTextBox.IsEnabled = false;
                    this.usernameTextBox.IsEnabled = false;
                    this.passwordBox.IsEnabled = false;
                    this.databaseTextBox.IsEnabled = false;
                    this.connectButton.Content = Properties.Resources.Standby;

                    this._aSyncResult = this._attemptDelegate.BeginInvoke( dbType , this.usernameTextBox.Text , this.passwordBox.Password , this.hostnameTextBox.Text , this.databaseTextBox.Text , this.ConnectionAttemptCallback , this._attemptDelegate );

                    this.suggestionDatabaseType.Foreground = Brushes.DarkSlateGray;
                }
                else
                {
                    this.suggestionDatabaseType.Foreground = Brushes.DarkMagenta;
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private bool AtttemptDatabaseConnection( Type type , string user , string pass , string host , string dbname )
        {
            try
            {
                var db = Framework.Database.ChangeStrategy( type );

                if( db.Connect( user , pass , host , dbname ) )
                {
                    return true;
                }

                return false;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return false;
            }
        }


        private void ConnectionAttemptCallback( IAsyncResult result )
        {
            try
            {
                var returnValue = ( ( ConnectionAttemptDelegate ) result.AsyncState ).EndInvoke( result );

                this.Dispatcher.Invoke( DispatcherPriority.Normal , ( Action ) ( () => this.UpdateUi( returnValue ) ) );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void UpdateUi( bool result )
        {
            try
            {
                if( result )
                {
                    this.Save();
                    this.connectButton.Content = Properties.Resources.Connected;
                }
                else
                {
                    this.connectButton.Content = Properties.Resources.ConnectionFailed;
                }

                this.databaseTypeComboBox.IsEnabled = true;
                this.hostnameTextBox.IsEnabled = true;
                this.usernameTextBox.IsEnabled = true;
                this.passwordBox.IsEnabled = true;
                this.databaseTextBox.IsEnabled = true;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region Nested type: ConnectionAttemptDelegate

        internal delegate bool ConnectionAttemptDelegate( Type type , string user , string pass , string host , string dbname );

        #endregion

        #region Nested type: ConnectionTimedOutDelegate

        internal delegate void ConnectionTimedOutDelegate();

        #endregion

        #region Implementation of IPreferences

        /// <summary>
        ///   Gets the name of the preferences Pane
        /// </summary>
        /// <returns>string</returns>
        public string GetName()
        {
            return Properties.Resources.Database;
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
            try
            {
                this._parent = null;
                var regedit = Framework.Registry;

                if( this.databaseTypeComboBox.SelectedIndex > -1 )
                {
                    var dbType = ( Type ) this.databaseTypeComboBox.Items[ this.databaseTypeComboBox.SelectedIndex ];
                    regedit.WriteKey( "dbtype" , dbType.ToString() );
                    regedit.WriteKey( "dbusername" , this.usernameTextBox.Text );
                    regedit.WriteKey( "dbpassword" , this.passwordBox.Password );
                    regedit.WriteKey( "dbname" , this.databaseTextBox.Text );
                    regedit.WriteKey( "dbhost" , this.hostnameTextBox.Text );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Asks the preferences to load its settings
        /// </summary>
        /// <param name="settingsParent"></param>
        /// <returns>string</returns>
        public void Load( ISettings settingsParent )
        {
            try
            {
                this._parent = settingsParent;
                this.databaseTypeComboBox.DataContext = Framework.DatabaseTypes.ToList();

                var regedit = Framework.Registry;
                var dbType = regedit.ReadKey( "dbtype" );

                foreach( var item in Framework.DatabaseTypes.ToList() )
                {
                    var t = item;
                    if( t.ToString().CompareTo( dbType ) == 0 )
                    {
                        this.databaseTypeComboBox.SelectedItem = item;
                    }
                }

                this.usernameTextBox.Text = regedit.ReadKey( "dbusername" );
                this.passwordBox.Password = regedit.ReadKey( "dbpassword" );
                this.databaseTextBox.Text = regedit.ReadKey( "dbname" );
                this.hostnameTextBox.Text = regedit.ReadKey( "dbhost" );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        /// Gets the type it Controls
        /// </summary>
        /// <returns>Type</returns>
        public Type GetControlType()
        {
            return typeof( IDatabase );
        }

        /// <summary>
        ///   Gets the image that represents the module
        /// </summary>
        /// <returns>An image</returns>
        public Image GetIcon()
        {
            return Framework.Images.GetImage( "database2" , "VS2010ImageLibrary" , 14 );
        }

        #endregion
    }
}