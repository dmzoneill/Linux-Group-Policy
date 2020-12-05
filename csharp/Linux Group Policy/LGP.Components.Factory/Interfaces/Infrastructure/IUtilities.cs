#region

using System;
using System.Reflection;

#endregion

namespace LGP.Components.Factory.Interfaces.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUtilities
    {
        /// <summary>
        ///   Cleans a given string, allowing only characters and numbers
        /// </summary>
        /// <param name = "s">string</param>
        /// <returns>string</returns>
        string CleanString( string s );


        /// <summary>
        ///   Convert epoch time to windows time
        /// </summary>
        /// <param name = "unixTime">seconds from 1970</param>
        /// <returns>DateTime</returns>
        DateTime FromUnixTime( long unixTime );


        /// <summary>
        /// Decodes base64 string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string Base64Decode( string data );


        /// <summary>
        /// Encodes string to base64
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string Base64Encode( string data );


        /// <summary>
        /// Loads a resource file
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="tobject"></param>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        object LoadResource( Assembly assembly , object tobject , string relativeUrl );


        /// <summary>
        /// Loads a resource file
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="absoluteUrl"></param>
        /// <returns></returns>
        object LoadResource( Assembly assembly , string absoluteUrl );
    }
}