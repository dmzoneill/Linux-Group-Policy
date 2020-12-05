#region

using System.Windows.Controls;
using LGP.Components.Factory.Interfaces.Component;

#endregion

namespace LGP.Components.Factory.Interfaces.Module
{
    /// <summary>
    ///   Module Interface
    /// </summary>
    public interface IModule : IComponent
    {
        /// <summary>
        ///   Gets the main component the module provides
        /// </summary>
        /// <returns>An instance of UserControl</returns>
        UserControl GetMainControl();


        /// <summary>
        ///   Gets the sidebar component the module provides
        /// </summary>
        /// <returns>An instance of UserControl</returns>
        UserControl GetSidebarControl();


        /// <summary>
        ///   Gets the bottombar component the module provides
        /// </summary>
        /// <returns>An instance of UserControl</returns>
        UserControl GetBottombarControl();


        /// <summary>
        ///   Gets the toolbar component the module provides
        /// </summary>
        /// <returns>An instance of UserControl</returns>
        UserControl GetToolbarControl();


        /// <summary>
        ///   Gets the preferences component the module provides
        /// </summary>
        /// <returns>An instance of UserControl</returns>
        UserControl GetPreferencesControl();
    }
}