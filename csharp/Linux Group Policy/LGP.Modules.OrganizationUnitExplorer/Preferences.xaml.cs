#region

using System;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Module;

#endregion

namespace LGP.Modules.OrganizationUnitExplorer
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
            try
            {
                this.InitializeComponent();
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
            return Properties.Resources.OuExplorer;
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
            return Framework.Images.GetImage( "category-2" , "16X16" , 14 );
        }

        #endregion
    }
}