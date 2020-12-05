#region

using System.Windows.Controls;

#endregion

namespace LGP.Components.Factory.Interfaces.Component
{
    /// <summary>
    ///   The interface to the application menu
    /// </summary>
    public interface IMenu : IComponent
    {
        /// <summary>
        ///  Builds the menu
        /// </summary>
        /// <param name = "menu">The application menu</param>
        void InitializeMenu( Menu menu );

        /// <summary>
        ///   Adds a item to the main menu
        /// </summary>
        /// <param name = "item">The item to be added</param>
        void AddMainItem( MenuItem item );


        /// <summary>
        ///   Remove an item from the menu at given index
        /// </summary>
        /// <param name = "indexItem">The index item to remove</param>
        void RemoveMainMenuItem( int indexItem );


        /// <summary>
        ///   Add an item under a given item specified by index
        /// </summary>
        /// <param name = "indexItem"></param>
        /// <param name = "subitem"></param>
        void AddSubMenuItem( int indexItem , MenuItem subitem );


        /// <summary>
        ///   Remove an index given two indexes Item / subitem
        /// </summary>
        /// <param name = "indexItem">The main item index</param>
        /// <param name = "subitem">The sub item index</param>
        void RemoveSubMenuItem( int indexItem , int subitem );


        /// <summary>
        ///   Add a subitem to a subitem item, item / subitem specified by indexes
        /// </summary>
        /// <param name = "indexItem">The item index</param>
        /// <param name = "indexSubItem">The sub item index</param>
        /// <param name = "subsubitem">The new item to add to the sub item</param>
        void AddSubSubMenuItem( int indexItem , int indexSubItem , MenuItem subsubitem );


        /// <summary>
        ///   Remove a indexed subitem , given an item index and its sub item index
        /// </summary>
        /// <param name = "indexItem">The item index</param>
        /// <param name = "indexSubItem">The sub item index</param>
        /// <param name = "indexSubSubItem">The item index to remove</param>
        void RemoveSubMenuItem( int indexItem , int indexSubItem , int indexSubSubItem );


        /// <summary>
        ///   Used as an accessor to check if there was an operation error
        /// </summary>
        /// <returns>True or false if an error occured</returns>
        bool GetError();


        /// <summary>
        ///   Gets the error from the last operation that caused it
        /// </summary>
        /// <returns>The error describing the error</returns>
        string GetStrError();
    }
}