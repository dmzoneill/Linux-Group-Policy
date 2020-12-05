#region

using System;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Infrastructure;
using LGP.Components.Factory.Interfaces.Module;

#endregion

namespace LGP.Components.Notifications
{
    /// <summary>
    ///   Interaction logic for Preferences.xaml
    /// </summary>
    public partial class Preferences : IPreferences
    {
        private ISettings _parent;

        /// <summary>
        /// 
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
            return Properties.Resources.Notifications;
        }

        /// <summary>
        ///   Gets the preferences window
        /// </summary>
        /// <returns>UserControl</returns>
        public UserControl GetControl()
        {
            return new Preferences();
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
            return typeof( INotification );
        }

        /// <summary>
        /// Gets the preferences icon
        /// </summary>
        /// <returns></returns>
        public Image GetIcon()
        {
            return Framework.Images.GetImage( "database2" , "VS2010ImageLibrary" , 14 );
        }

        #endregion
    }
}