#region

using LGP.Components.Factory.Interfaces.Infrastructure;
using Microsoft.Win32;

#endregion

namespace LGP.Components.Factory.Internal
{
    /// <summary>
    ///   Simple Registry class
    /// </summary>
    internal class RegistryHandler : IRegistryHandler
    {
        private static RegistryHandler _instance;
        private readonly RegistryKey _applicationSubKey;


        private RegistryHandler()
        {
            this._applicationSubKey = Registry.LocalMachine.CreateSubKey( @"SOFTWARE\Linux Group Policy" );
        }

        #region IRegistryHandler Members

        /// <summary>
        ///   Read a sub key for the registry application root
        /// </summary>
        /// <param name = "keyname">The keyname to read</param>
        /// <returns>string</returns>
        public string ReadKey( string keyname )
        {
            return ( string ) this._applicationSubKey.GetValue( keyname );
        }


        /// <summary>
        ///   Write a sub key for the registry application root
        /// </summary>
        /// <param name = "keyname">The keyname to write</param>
        /// <param name = "keyvalue">The value for the key</param>
        /// <returns>string</returns>
        public void WriteKey( string keyname , object keyvalue )
        {
            this._applicationSubKey.SetValue( keyname , keyvalue , RegistryValueKind.String );
        }


        /// <summary>
        ///   Delete a subkey from the application root key
        /// </summary>
        /// <param name = "keyname">The keyname to write</param>
        /// <returns>string</returns>
        public void DeleteKey( string keyname )
        {
            this._applicationSubKey.DeleteValue( keyname , false );
        }


        /// <summary>
        ///   Delete a subkeytree from the application root key
        /// </summary>
        /// <param name = "keyname">The keyname to write</param>
        /// <returns>string</returns>
        public void DeleteSubKeyTree( string keyname )
        {
            this._applicationSubKey.DeleteSubKeyTree( keyname , false );
        }

        #endregion

        /// <summary>
        ///   Singlton factory method
        /// </summary>
        /// <returns>LGPRegistry instance</returns>
        public static RegistryHandler GetInstance()
        {
            return _instance ?? ( _instance = new RegistryHandler() );
        }
    }
}