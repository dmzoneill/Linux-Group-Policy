#region

using System.Collections.Generic;

#endregion

namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    /// </summary>
    public interface IModuleGateway
    {
        /// <summary>
        ///   Creates a Module
        /// </summary>
        /// <param name = "modulename">The new Module name</param>
        /// <returns>IModule</returns>
        IModule CreateModule( string modulename );


        /// <summary>
        ///   Deletes an Module
        /// </summary>
        /// <param name = "modulename">the module to delete</param>
        void DeleteModule( string modulename );


        /// <summary>
        ///   Gets an IModule given a name
        /// </summary>
        /// <param name = "modulename">the module name</param>
        /// <returns>IModule</returns>
        IModule GetModuleByName( string modulename );


        /// <summary>
        ///   Get the list of modules in the database
        /// </summary>
        /// <returns>List IModule</returns>
        List< IModule > GetModules();


        /// <summary>
        /// refreshes the list
        /// </summary>
        void Refresh();
    }
}