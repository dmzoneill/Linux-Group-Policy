#region

using System;
using System.Collections.Generic;
using System.Windows.Controls;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Database;
using IModule = LGP.Components.Factory.Interfaces.Module.IModule;

#endregion

namespace LGP.Components.Factory.Interfaces.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public interface IClassLibraryHandler
    {
        /// <summary>
        ///   Gets a listing of dlls from the application directory
        /// </summary>
        void LoadDllListing();

        /// <summary>
        ///   Loads a dll given a path
        /// </summary>
        /// <param name = "dllPath">The path to the dll</param>
        void LoadIComponent( string dllPath );

        /// <summary>
        ///   Get the types that realised the IModule interface
        /// </summary>
        /// <returns>List of modules</returns>
        List< IModule > GetModules();

        /// <summary>
        ///   Get the types that realised the iComponent interface
        /// </summary>
        /// <returns>List of IComponents</returns>
        List< IComponent > GetComponents();

        /// <summary>
        ///   Check to see if there was an error
        /// </summary>
        /// <returns>bool error</returns>
        bool HasError();

        /// <summary>
        ///   Get the last error occurred
        /// </summary>
        /// <returns>String error</returns>
        string GetError();

        /// <summary>
        ///   Get the type that realised the Imenu interface
        /// </summary>
        /// <returns></returns>
        Type GetMenuType();

        /// <summary>
        ///   Get the type that realized the IDialog interface
        /// </summary>
        /// <returns></returns>
        Type GetDialogType();

        /// <summary>
        ///   Get the type that realized the IPanle interface
        /// </summary>
        /// <returns></returns>
        Type GetPanelType();

        /// <summary>
        ///   Get the type that realized the IPanle interface
        /// </summary>
        /// <returns></returns>
        Type GetNotifcationType();

        /// <summary>
        ///   Return dlls in directory path
        /// </summary>
        /// <returns>String[] dlls</returns>
        string[ ] GetDllPaths();

        /// <summary>
        ///   Gets the name of an dll asembly given its path
        /// </summary>
        /// <param name = "assemblyPath">Dll path</param>
        /// <returns>Asembly name</returns>
        string GetAssemblyName( string assemblyPath );

        /// <summary>
        ///   Gets the assembly version
        /// </summary>
        /// <param name = "assemblyPath">Dll path</param>
        /// <returns>Dll version</returns>
        string GetAssemblyVersion( string assemblyPath );

        /// <summary>
        ///   Create an object of type
        /// </summary>
        /// <param name = "type">type of object you would like to create</param>
        /// <returns></returns>
        Object CreateObject( Type type );

        /// <summary>
        ///   Create an object of type
        /// </summary>
        /// <param name = "type">type of object you would like to create</param>
        /// <param name = "o">The contructor parameter</param>
        /// <returns></returns>
        Object CreateObject( Type type , Menu o );

        /// <summary>
        ///   Gets a previously created database strategy context object
        /// </summary>
        /// <returns>IDatabaseModule type</returns>
        /// <exception cref = "Exception">Exception context strategy null</exception>
        IDatabase GetDatabaseContextHandler();

        /// <summary>
        ///   Recreated the database gateway given a new strategy type
        /// </summary>
        /// <returns>IDatabaseModule type</returns>
        /// <exception cref = "Exception">Exception context strategy null</exception>
        IDatabase GetDatabaseContextHandler( Type strategyType );

        /// <summary>
        ///   Gets the preferences panes associated with the modules
        /// </summary>
        /// <returns></returns>
        List< Type > GetPreferencesPanes();

        /// <summary>
        ///   Gets the conext menus handler
        /// </summary>
        /// <returns></returns>
        IContextMenus GetConextMenusHandler();
    }
}