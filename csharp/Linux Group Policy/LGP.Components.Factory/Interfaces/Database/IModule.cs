#region

using System.Collections.Generic;

#endregion

namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    /// </summary>
    public interface IModule : IModuleObserver
    {
        /// <summary>
        ///   The file contents when used by the module editor
        /// </summary>
        string FileContents
        {
            get;
            set;
        }

        /// <summary>
        ///   Returns the module id
        /// </summary>
        /// <returns>int</returns>
        int GetModuleId();

        /// <summary>
        ///   Gets the name of the module
        /// </summary>
        /// <returns>string</returns>
        string GetModuleName();

        /// <summary>
        ///   Sets the module name
        /// </summary>
        /// <param name = "name"></param>
        void SetModuleName( string name );

        /// <summary>
        ///   Adds a grammer to this module
        /// </summary>
        /// <param name = "key">string</param>
        /// <param name = "value">string</param>
        /// <returns>IGrammer</returns>
        IGrammer AddGrammer( string key , string value );


        /// <summary>
        ///   Deletes a grammer given a key
        /// </summary>
        /// <param name = "key"></param>
        void DeleteGrammer( string key );


        /// <summary>
        ///   Dletes all the grammers associated with this module
        /// </summary>
        void DeleteAllGrammer();


        /// <summary>
        ///   Gets all the grammers for this module
        /// </summary>
        /// <returns>List IGrammer</returns>
        List< IGrammer > GetAllGrammers();


        /// <summary>
        ///   Gets a grammer given a key
        /// </summary>
        /// <param name = "key">grammer lvalue</param>
        /// <returns>IGrammer</returns>
        IGrammer GetGrammer( string key );


        /// <summary>
        ///   Gets a grammer given an id
        /// </summary>
        /// <param name = "id">grammer id</param>
        /// <returns>IGrammer</returns>
        IGrammer GetGrammer( int id );


        /// <summary>
        ///   Get the has of the module
        /// </summary>
        /// <returns>int</returns>
        int GetHash();


        /// <summary>
        ///   Gets the module location
        /// </summary>
        /// <returns>string</returns>
        string GetModuleLocation();
    }
}