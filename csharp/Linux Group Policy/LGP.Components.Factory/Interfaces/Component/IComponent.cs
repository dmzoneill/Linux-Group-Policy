#region

using System.Windows.Controls;

#endregion

namespace LGP.Components.Factory.Interfaces.Component
{
    /// <summary>
    ///   Component Interface
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        ///   Gets the name of the main component
        /// </summary>
        /// <returns>String name</returns>
        string GetName();


        /// <summary>
        ///   Gets the image that represents the module
        /// </summary>
        /// <returns>An image</returns>
        Image GetIcon();


        /// <summary>
        ///   Asks the component to register is menu components
        /// </summary>
        /// <param name = "menu">The program main menu</param>
        void RegisterMenuEntries( IMenu menu );
    }
}