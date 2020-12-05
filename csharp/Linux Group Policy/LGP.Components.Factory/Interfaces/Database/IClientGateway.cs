#region

using System.Collections.Generic;

#endregion

namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    ///   IOuGateway interface
    /// </summary>
    public interface IClientGateway
    {
        /// <summary>
        ///   Gets a client by id
        /// </summary>
        /// <param name = "id">client id</param>
        /// <returns>IClient</returns>
        IClient GetClientById( int id );


        /// <summary>
        ///   Get clients from a given ou
        /// </summary>
        /// <param name = "ouid">id of the ou</param>
        /// <returns>List of clients</returns>
        List< IClient > GetOuClients( int ouid );


        /// <summary>
        /// Refresh the client listing
        /// </summary>
        void Refresh();
    }
}