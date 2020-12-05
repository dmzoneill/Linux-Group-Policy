namespace LGP.Components.Factory.Interfaces.Infrastructure
{
    /// <summary>
    ///   Registry interface
    /// </summary>
    public interface IRegistryHandler
    {
        /// <summary>
        ///   Read a sub key for the registry application root
        /// </summary>
        /// <param name = "keyname">The keyname to read</param>
        /// <returns>string</returns>
        string ReadKey( string keyname );


        /// <summary>
        ///   Write a sub key for the registry application root
        /// </summary>
        /// <param name = "keyname">The keyname to write</param>
        /// <param name = "keyvalue">The value for the key</param>
        /// <returns>string</returns>
        void WriteKey( string keyname , object keyvalue );


        /// <summary>
        ///   Delete a subkey from the application root key
        /// </summary>
        /// <param name = "keyname">The keyname to write</param>
        /// <returns>string</returns>
        void DeleteKey( string keyname );


        /// <summary>
        ///   Delete a subkeytree from the application root key
        /// </summary>
        /// <param name = "keyname">The keyname to write</param>
        /// <returns>string</returns>
        void DeleteSubKeyTree( string keyname );
    }
}