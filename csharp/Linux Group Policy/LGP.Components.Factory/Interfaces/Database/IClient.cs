#region

using System.Windows.Controls;

#endregion

namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    ///   Client entity class
    /// </summary>
    public interface IClient : IClientObserver
    {
        /// <summary>
        ///   Sets the client id
        /// </summary>
        /// <param name = "val">int</param>
        void SetId( int val );


        /// <summary>
        ///   Gets the client id
        /// </summary>
        /// <returns>int</returns>
        int GetId();


        /// <summary>
        ///   Sets the client ou id
        /// </summary>
        /// <param name = "val">int</param>
        void Setouid( int val );


        /// <summary>
        ///   Gets the client ou id
        /// </summary>
        /// <returns>int</returns>
        int GetouId();


        /// <summary>
        ///   Sets the client guid
        /// </summary>
        /// <param name = "val">string</param>
        void SetGuid( string val );


        /// <summary>
        ///   Gets the client guid
        /// </summary>
        /// <returns>string</returns>
        string GetGuid();


        /// <summary>
        ///   Sets the client name
        /// </summary>
        /// <param name = "val">string</param>
        void SetName( string val );


        /// <summary>
        ///   Gets the client name
        /// </summary>
        /// <returns>string</returns>
        string GetName();


        /// <summary>
        ///   Sets the client OS
        /// </summary>
        /// <param name = "val">string</param>
        void SetOs( string val );


        /// <summary>
        ///   Gets the client OS
        /// </summary>
        /// <returns>string</returns>
        string GetOs();


        /// <summary>
        ///   Sets the client architecture
        /// </summary>
        /// <param name = "val">string</param>
        void SetArch( string val );


        /// <summary>
        ///   Gets the client architecture
        /// </summary>
        /// <returns>string</returns>
        string GetArch();


        /// <summary>
        ///   Sets the client kernel
        /// </summary>
        /// <param name = "val">string</param>
        void SetKernel( string val );


        /// <summary>
        ///   Gets the client kernel
        /// </summary>
        /// <returns>string</returns>
        string GetKernel();


        /// <summary>
        ///   Sets the client psuedoname
        /// </summary>
        /// <param name = "val">string</param>
        void SetPseudoName( string val );


        /// <summary>
        ///   Gets the client psuedoname
        /// </summary>
        /// <returns>string</returns>
        string GetPseudoName();


        /// <summary>
        ///   Sets the client Distribution
        /// </summary>
        /// <param name = "val">string</param>
        void SetDistribution( string val );


        /// <summary>
        ///   Gets the client Distribution
        /// </summary>
        /// <returns>string</returns>
        string GetDistribution();


        /// <summary>
        ///   Sets the client Distribution version
        /// </summary>
        /// <param name = "val">string</param>
        void SetDistributionVersion( string val );


        /// <summary>
        ///   Gets the client Distribution version
        /// </summary>
        /// <returns>string</returns>
        string GetDistributionVersion();

        /// <summary>
        ///   Sets the client version
        /// </summary>
        /// <param name = "val">string</param>
        void SetVersion( string val );


        /// <summary>
        ///   Gets the client version
        /// </summary>
        /// <returns>string</returns>
        string GetVersion();


        /// <summary>
        ///   Sets the client last seen time
        /// </summary>
        /// <param name = "val">string</param>
        void SetLastseen( string val );


        /// <summary>
        ///   Gets the client last seen time
        /// </summary>
        /// <returns>string</returns>
        string GetLastseen();


        /// <summary>
        ///   Sets the client binded state
        /// </summary>
        /// <param name = "val">string</param>
        void SetBinded( string val );


        /// <summary>
        ///   Gets the client binded state
        /// </summary>
        /// <returns>string</returns>
        string GetBinded();


        /// <summary>
        ///   Sets the clients version
        /// </summary>
        /// <param name = "val">string</param>
        void SetClientVersion( string val );


        /// <summary>
        ///   Gets the clients version
        /// </summary>
        /// <returns>string</returns>
        string GetClientVersion();


        /// <summary>
        ///   Sets the clients ip address
        /// </summary>
        /// <param name = "val">string</param>
        void SetIpaddress( string val );


        /// <summary>
        ///   Gets the clients ip address
        /// </summary>
        /// <returns>string</returns>
        string GetIpaddress();


        /// <summary>
        ///   Sets the clients port
        /// </summary>
        /// <param name = "val">string</param>
        void SetPort( string val );


        /// <summary>
        ///   Gets the clients port
        /// </summary>
        /// <returns>string</returns>
        string GetPort();


        /// <summary>
        ///   Gets the client distro image
        /// </summary>
        /// <param name = "size">int</param>
        /// <returns>image</returns>
        Image GetDistroImage( int size );
    }
}