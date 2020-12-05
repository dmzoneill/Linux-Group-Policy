#region

using System.Collections.Generic;

#endregion

namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    ///   IOuGateway interface
    /// </summary>
    public interface IGrammerGateway
    {
        /// <summary>
        ///   Adds a grammer given a moduleid or returns existing Grammer
        /// </summary>
        /// <param name = "moduleId">int</param>
        /// <param name = "key">string</param>
        /// <param name = "val">string</param>
        /// <returns>IGrammer</returns>
        IGrammer CreateGrammer( int moduleId , string key , string val );


        /// <summary>
        ///   Deletes a grammer entry given key and moduleid
        /// </summary>
        /// <param name = "mid">int</param>
        /// <param name = "key">string</param>
        void DeleteGrammer( int mid , string key );


        /// <summary>
        ///   Deletes all grammer given key the moduleid
        /// </summary>
        /// <param name = "mid">int</param>
        void DeleteGrammer( int mid );


        /// <summary>
        ///   Gets a Grammer given a the moduleid and key
        /// </summary>
        /// <param name = "mid">int</param>
        /// <param name = "key">string</param>
        /// <returns>IGrammer</returns>
        IGrammer GetGrammer( int mid , string key );


        /// <summary>
        ///   Gets a Grammer given a the moduleid and id
        /// </summary>
        /// <param name = "mid">int</param>
        /// <param name = "id">int</param>
        /// <returns>IGrammer</returns>
        IGrammer GetGrammer( int mid , int id );


        /// <summary>
        ///   Gets all Grammer given a the moduleid
        /// </summary>
        /// <param name = "mid">int</param>
        /// <returns>IGrammer list</returns>
        List< IGrammer > GetGrammer( int mid );


        /// <summary>
        ///   Gets all grammers
        /// </summary>
        /// <returns>IGrammer List</returns>
        List< IGrammer > GetGrammer();

        /// <summary>
        /// Refreshes the list
        /// </summary>
        void Refresh();
    }
}