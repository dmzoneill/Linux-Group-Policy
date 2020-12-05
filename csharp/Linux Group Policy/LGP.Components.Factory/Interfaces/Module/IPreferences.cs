#region

using System;
using System.Windows.Controls;
using LGP.Components.Factory.Interfaces.Component;

#endregion

namespace LGP.Components.Factory.Interfaces.Module
{
    /// <summary>
    ///   Preferences User Control Interface
    /// </summary>
    public interface IPreferences
    {
        /// <summary>
        ///   Gets the name of the preferences Pane
        /// </summary>
        /// <returns>string</returns>
        string GetName();


        /// <summary>
        ///   Gets the preferences window
        /// </summary>
        /// <returns>UserControl</returns>
        UserControl GetControl();


        /// <summary>
        ///   Aks the preferences panel to save its entries
        /// </summary>
        void Save();


        /// <summary>
        ///   Asks the preferences to load its settings
        /// </summary>
        /// <param name="settingsParent"></param>
        /// <returns>string</returns>
        void Load( ISettings settingsParent );


        /// <summary>
        /// Gets the type it Controls
        /// </summary>
        /// <returns>Type</returns>
        Type GetControlType();


        /// <summary>
        /// Gets the preferences icon
        /// </summary>
        /// <returns></returns>
        Image GetIcon();
    }
}