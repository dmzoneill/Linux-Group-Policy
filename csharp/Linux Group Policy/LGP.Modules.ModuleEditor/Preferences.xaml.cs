#region

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Module;
using UserControl = System.Windows.Controls.UserControl;

#endregion

namespace LGP.Modules.ModuleEditor
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
            try
            {
                this.InitializeComponent();
                var regedit = Framework.Registry;
                this.ModulesFolderLocationTextBox.Text = regedit.ReadKey( "moduleslocation" );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region IPreferences Members

        /// <summary>
        ///   Gets the name of the preferences Pane
        /// </summary>
        /// <returns>string</returns>
        public string GetName()
        {
            return Properties.Resources.ModuleEditor;
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
            return typeof( IModule );
        }

        /// <summary>
        /// Gets the preferences icon
        /// </summary>
        /// <returns></returns>
        public Image GetIcon()
        {
            return Framework.Images.GetImage( "pencil" , "16X16" , 14 );
        }

        #endregion

        private void SelectFolderButtonClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var folderDialog = new FolderBrowserDialog
                {
                    SelectedPath = @"C:\\"
                };

                var result = folderDialog.ShowDialog();
                if( result.ToString() == "OK" )
                {
                    Framework.Registry.WriteKey( "moduleslocation" , folderDialog.SelectedPath );
                    this.ModulesFolderLocationTextBox.Text = folderDialog.SelectedPath;
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}