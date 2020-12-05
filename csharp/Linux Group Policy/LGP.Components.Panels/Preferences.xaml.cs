#region

using System;
using System.Windows.Controls;
using AvalonDock;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Module;

#endregion

namespace LGP.Components.Panels
{
    /// <summary>
    ///   Interaction logic for Preferences.xaml
    /// </summary>
    public partial class Preferences : IPreferences
    {
        private ISettings _parent;

        /// <summary>
        ///   Constructor
        /// </summary>
        public Preferences()
        {
            this.InitializeComponent();
        }

        #region IPreferences Members

        /// <summary>
        ///   Gets the name of the preferences Pane
        /// </summary>
        /// <returns>string</returns>
        public string GetName()
        {
            return Properties.Resources.Panels;
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
            string[ ] themes = {
                                   "LGP Default,/LGP.Components.Panels;component/Themes/lgp.xaml" ,
                                   "AvalonDock Default,generic" ,
                                   "Expression Blend,/AvalonDock.Themes;component/themes/ExpressionDark.xaml" ,
                                   "Visual Studio 2010,/AvalonDock.Themes;component/themes/ExpressionDark.xaml" ,
                                   "Aero Normal,aero.normalcolor" ,
                                   "Classic,classic" ,
                                   "Luna Noraml,luna.normalcolor"
                               };

            foreach( var theme in themes )
            {
                var bits = theme.Split( ',' );
                var item = new ComboBoxItem
                {
                    Tag = bits[ 1 ] ,
                    Content = bits[ 0 ]
                };
                this.themeComboBox.Items.Add( item );
            }

            this.themeComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Gets the type it Controls
        /// </summary>
        /// <returns>Type</returns>
        public Type GetControlType()
        {
            return typeof( IPanel );
        }

        /// <summary>
        /// Gets the preferences icon
        /// </summary>
        /// <returns></returns>
        public Image GetIcon()
        {
            return Framework.Images.GetImage( "panel" , "VS2010ImageLibrary" , 14 );
        }

        #endregion

        private void ThemeComboBoxSelectionChanged( object sender , SelectionChangedEventArgs e )
        {
            try
            {
                var comboitem = this.themeComboBox.SelectedItem as ComboBoxItem;

                switch( this.themeComboBox.SelectedIndex )
                {
                    case 0 :
                    case 2 :
                    case 3 :
                        ThemeFactory.ChangeTheme( new Uri( comboitem.Tag.ToString() , UriKind.RelativeOrAbsolute ) );
                        break;

                    case 1 :
                    case 4 :
                    case 5 :
                    case 6 :
                        ThemeFactory.ChangeTheme( comboitem.Tag.ToString() );
                        break;
                }
            }

            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}