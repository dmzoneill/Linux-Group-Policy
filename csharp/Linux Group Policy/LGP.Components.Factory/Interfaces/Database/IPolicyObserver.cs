namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    ///   Observer interface
    /// </summary>
    public interface IPolicyObserver
    {
        /// <summary>
        ///   Update this observer with a refernece to the ou
        /// </summary>
        /// <param name = "policy">IPolicy</param>
        /// <param name = "source">source observer</param>
        void Update( IPolicy policy , IPolicyObserver source );


        /// <summary>
        ///   Attach objects that observer this IPolicy
        /// </summary>
        /// <param name = "obj">IPolicyObserver</param>
        void Attach( IPolicyObserver obj );


        /// <summary>
        ///   Detach objects that observer this IPolicy
        /// </summary>
        /// <param name = "obj">IPolicyObserver</param>
        void Detach( IPolicyObserver obj );


        /// <summary>
        ///   Notify observers of this IPolicy
        /// </summary>
        void Notify();


        /// <summary>
        ///   Dispose this object and all the observers
        /// </summary>
        /// <param name = "obj">IPolicyObserver</param>
        void Dispose( IPolicyObserver obj );

    }
}