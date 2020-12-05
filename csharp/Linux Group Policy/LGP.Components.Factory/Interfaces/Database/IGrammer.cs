namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    /// </summary>
    public interface IGrammer : IGrammerObserver
    {
        /// <summary>
        ///   Gets the grammer rowid
        /// </summary>
        /// <returns>int</returns>
        int GetId();

        /// <summary>
        ///   Gets the grammer moduleid
        /// </summary>
        /// <returns>int</returns>
        int GetModuleId();

        /// <summary>
        ///   Gets the grammer key
        /// </summary>
        /// <returns>string</returns>
        string GetKey();

        /// <summary>
        ///   Gets the grammer value
        /// </summary>
        /// <returns>string</returns>
        string GetValue();

        /// <summary>
        ///   Sets the grammer moduleid
        /// </summary>
        /// <param name = "val">int</param>
        void SetModuleId( int val );

        /// <summary>
        ///   Sets the grammer key
        /// </summary>
        /// <param name = "val">string</param>
        void SetKey( string val );

        /// <summary>
        ///   Sets the grammer value
        /// </summary>
        /// <param name = "val">string</param>
        void SetValue( string val );
    }
}