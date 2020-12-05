#region

using System.Windows.Controls;

#endregion

namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    ///   Ou Entity interface
    /// </summary>
    public interface IOu : IOuObserver
    {
        /// <summary>
        ///   Sets the ou Id
        /// </summary>
        /// <param name = "val">int</param>
        void SetOuId( int val );


        /// <summary>
        ///   Gets the ou Id
        /// </summary>
        /// <returns>int</returns>
        int GetOuId();


        /// <summary>
        ///   Sets the ou name
        /// </summary>
        /// <param name = "val">string</param>
        void SetName( string val );


        /// <summary>
        ///   Gets the ou name
        /// </summary>
        /// <returns>string</returns>
        string GetName();


        /// <summary>
        ///   Sets the parent ou Id
        /// </summary>
        /// <param name = "val">int</param>
        void SetParentOuId( int val );


        /// <summary>
        ///   Gets the parent ou Id
        /// </summary>
        /// <returns>int</returns>
        int GetParentOuId();


        /// <summary>
        ///   Sets the ou policy group
        /// </summary>
        /// <param name = "val"></param>
        void SetPolicyGroup( string val );


        /// <summary>
        ///   Gets the ou policy group
        /// </summary>
        /// <returns></returns>
        string GetPolicyGroup();


        /// <summary>
        ///   Gets the ou image
        /// </summary>
        /// <param name = "size">int</param>
        /// <returns>image</returns>
        Image GetOuImage( int size );
    }
}