namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    ///   Policy interface
    /// </summary>
    public interface IPolicy : IPolicyObserver
    {
        /// <summary>
        ///   Gets the id of this policy
        /// </summary>
        /// <returns>int</returns>
        int GetId();

        /// <summary>
        ///   Gets the ou id
        /// </summary>
        /// <returns>int</returns>
        int GetOuId();

        /// <summary>
        ///   Sets the ou id
        /// </summary>
        /// <param name = "id">int</param>
        void SetOuId( int id );

        /// <summary>
        ///   Gets the created date
        /// </summary>
        /// <returns>string</returns>
        string GetCreated();

        /// <summary>
        ///   Sets the created date
        /// </summary>
        /// <param name = "created">string</param>
        void SetCreated( string created );

        /// <summary>
        ///   Gets the edited date
        /// </summary>
        /// <returns>string</returns>
        string GetEdited();

        /// <summary>
        ///   Sets the edited date
        /// </summary>
        /// <param name = "edited">string</param>
        void SetEdited( string edited );

        /// <summary>
        ///   Gets the enabled status
        /// </summary>
        /// <returns>bool</returns>
        bool IsEnabled();

        /// <summary>
        ///   Sets the enabled status
        /// </summary>
        /// <param name = "enabled"></param>
        void SetEnabled( bool enabled );

        /// <summary>
        ///   Get the DSL or rules
        /// </summary>
        /// <returns></returns>
        string GetDsl();

        /// <summary>
        ///   Set the DSL rules
        /// </summary>
        /// <param name = "dsl"></param>
        void SetDsl( string dsl );


        /// <summary>
        ///   Gets the version
        /// </summary>
        /// <returns>string</returns>
        string GetVersion();


        /// <summary>
        ///   Sets the version
        /// </summary>
        /// <param name = "version">string</param>
        void SetVersion( string version );
    }
}