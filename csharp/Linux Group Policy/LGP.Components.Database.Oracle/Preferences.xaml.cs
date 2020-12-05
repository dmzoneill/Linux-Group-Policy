#region

using System;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Components.Factory.Interfaces.Module;

#endregion

namespace LGP.Components.Database.Oracle
{
    /// <summary>
    ///   Interaction logic for Preferences.xaml
    /// </summary>
    public partial class Preferences : IPreferences
    {
        private ISettings _parent;

        /// <summary>
        /// </summary>
        public Preferences()
        {
            this.InitializeComponent();
        }

        #region Implementation of IPreferences

        /// <summary>
        ///   Gets the name of the preferences Pane
        /// </summary>
        /// <returns>string</returns>
        public string GetName()
        {
            return Properties.Resources.Oracle;
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
            var regedit = Framework.Registry;
            var port = this.textBox1.Text;
            int intport;

            regedit.WriteKey( "OraclePort" , int.TryParse( port , out intport ) ? intport : 3306 );
        }

        /// <summary>
        ///   Asks the preferences to load its settings
        /// </summary>
        /// <param name="settingsParent"></param>
        /// <returns>string</returns>
        public void Load( ISettings settingsParent )
        {
            this._parent = settingsParent;
            var regedit = Framework.Registry;
            var port = regedit.ReadKey( "OraclePort" );
            this.textBox1.Text = port ?? "3306";
        }

        /// <summary>
        /// Gets the type it Controls
        /// </summary>
        /// <returns>Type</returns>
        public Type GetControlType()
        {
            return typeof( IDatabaseModule );
        }

        /// <summary>
        ///   Gets the image that represents the module
        /// </summary>
        /// <returns>An image</returns>
        public Image GetIcon()
        {
            return Framework.Images.GetImage( "oracle" , "VS2010ImageLibrary" , 14 );
        }

        #endregion

        private void SetDefaultPortButtonClickClick( object sender , System.Windows.RoutedEventArgs e )
        {
        }
    }
}