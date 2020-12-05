#region

using System.Collections.Generic;

#endregion

namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    ///   IOuGateway interface
    /// </summary>
    public interface IOuGateway
    {
        /// <summary>
        ///   Adds a Ou to a given parent ou
        /// </summary>
        /// <param name = "ouName">The new ou name</param>
        /// <param name = "parentId">The new parent id</param>
        /// <returns>IOu</returns>
        IOu CreateOu( string ouName , int parentId );


        /// <summary>
        ///   Deletes an Ou to a given parent ou
        /// </summary>
        /// <param name = "ouid">the ou id to delete</param>
        void DeleteOu( int ouid );


        /// <summary>
        ///   Gets an Iou given an id
        /// </summary>
        /// <param name = "ouid">int id</param>
        /// <returns>IOu</returns>
        IOu GetOuById( int ouid );


        /// <summary>
        ///   Gets a parent Iou given an id
        /// </summary>
        /// <param name = "ouid">int id</param>
        /// <returns>IOu</returns>
        IOu GetOuParentById( int ouid );


        /// <summary>
        ///   Gets a listing of Ous
        /// </summary>
        /// <returns>IOu List</returns>
        List< IOu > GetOuListing();


        /// <summary>
        ///   Get the children of a ou
        /// </summary>
        /// <param name = "ouid">int outid</param>
        /// <returns>IOu List</returns>
        List< IOu > GetChildren( int ouid );


        /// <summary>
        ///   Get the parents with no parents
        /// </summary>
        /// <returns>IOu List</returns>
        List< IOu > GetRoots();


        /// <summary>
        /// Refreshes the ou list
        /// </summary>
        void Refresh();
    }
}