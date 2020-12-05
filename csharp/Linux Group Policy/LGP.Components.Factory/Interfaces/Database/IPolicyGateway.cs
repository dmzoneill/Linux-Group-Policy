#region

using System.Collections.Generic;

#endregion

namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    ///   Policy gateway
    /// </summary>
    public interface IPolicyGateway
    {
        /// <summary>
        ///   Create a policy for a given ou
        /// </summary>
        /// <param name = "ou">the associated ou</param>
        /// <param name="defaultpolicy">default policy</param>
        /// <returns>IPolicy</returns>
        IPolicy CreatePolicy( IOu ou , string defaultpolicy );

        /// <summary>
        ///   Deletes a policy
        /// </summary>
        /// <param name = "ou">IOu</param>
        void DeletePolicy( IOu ou );

        /// <summary>
        ///   Deletes a policy
        /// </summary>
        /// <param name = "policy">IPolicy</param>
        void DeletePolicy( IPolicy policy );

        /// <summary>
        ///   Deletes a policy
        /// </summary>
        /// <param name = "policyid">policy id</param>
        void DeletePolicy( int policyid );

        /// <summary>
        ///   Gets a policy
        /// </summary>
        /// <param name = "id">policy id</param>
        /// <returns>IPolicy</returns>
        IPolicy GetPolicy( int id );

        /// <summary>
        ///   Gets a policy
        /// </summary>
        /// <param name = "ou">IOu</param>
        /// <returns>IPolicy</returns>
        IPolicy GetPolicy( IOu ou );

        /// <summary>
        ///   Gets an predecessor policy
        /// </summary>
        /// <param name = "policy">IPolicy</param>
        /// <returns>IPolicy</returns>
        IPolicy GetPredecessorPolicy( IPolicy policy );

        /// <summary>
        ///   Gets an predecessor policy
        /// </summary>
        /// <param name = "ou">IOu</param>
        /// <returns>IPolicy</returns>
        IPolicy GetPredecessorPolicy( IOu ou );

        /// <summary>
        ///   Gets an ancestor policy
        /// </summary>
        /// <param name = "policy">IPolicy</param>
        /// <returns>IPolicy</returns>
        IPolicy GetAncestorPolicy( IPolicy policy );

        /// <summary>
        ///   Gets an ancestor policy
        /// </summary>
        /// <param name = "ou">IOu</param>
        /// <returns>IPolicy</returns>
        IPolicy GetAncestorPolicy( IOu ou );

        /// <summary>
        ///   Gets a list of all policies
        /// </summary>
        /// <returns>List IPolicy</returns>
        List< IPolicy > GetPolicies();

        /// <summary>
        ///   Gets the policy in a given ou
        /// </summary>
        /// <param name = "ou">IOu</param>
        /// <returns>IPolicy</returns>
        IPolicy GetPolicyByOu( IOu ou );

        /// <summary>
        /// Refreshes the list
        /// </summary>
        void Refresh();
    }
}