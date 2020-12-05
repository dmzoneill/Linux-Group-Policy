#region

using System.Windows.Controls;

#endregion

namespace LGP.Components.Factory.Interfaces.Component
{
    /// <summary>
    ///   Interface to the panel component
    /// </summary>
    public interface IPanel : IComponent
    {
        /// <summary>
        ///   Requests the Panel realizer to add a User control to its display
        /// </summary>
        /// <param name = "control">The user control</param>
        /// <param name = "toolbarName">The title on the toolbar header</param>
        /// <param name = "image">The image icon assoacited with the control</param>
        void AddSideComponent( UserControl control , string toolbarName , Image image );


        /// <summary>
        ///   Requests the Panel realizer to remove a User control from its display
        /// </summary>
        /// <param name = "control">The control to remove</param>
        void RemoveSideComponent( UserControl control );


        /// <summary>
        ///   Adds a UserControl to the main component container
        /// </summary>
        /// <param name = "control">UserControl element</param>
        /// <param name = "controlTabTitle">The title to give the tab element</param>
        void AddMainComponent( UserControl control , string controlTabTitle );


        /// <summary>
        ///   Requests the Panel realizer to remove a User control from its display
        /// </summary>
        /// <param name = "control">UserControl element</param>
        void RemoveMainComponent( UserControl control );


        /// <summary>
        ///   Requests the Panel to update the panel tab names
        /// </summary>
        void UpdateTabNames();


        /// <summary>
        ///   Load all side bar components provided by the modules
        /// </summary>
        void LoadSideBarComponents();


        /// <summary>
        ///   Subscribes subscriber events
        /// </summary>
        void SubscribeEvents();
    }
}