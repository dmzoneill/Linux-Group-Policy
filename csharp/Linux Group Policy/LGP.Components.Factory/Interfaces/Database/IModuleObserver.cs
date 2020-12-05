namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    /// </summary>
    public interface IModuleObserver
    {
        /// <summary>
        ///   Update this observer with a refernece to the module
        /// </summary>
        /// <param name = "module">IModule</param>
        /// <param name = "source">IModuleObserver</param>
        void Update( IModule module , IModuleObserver source );


        /// <summary>
        ///   Attach objewcts that observer this module
        /// </summary>
        /// <param name = "obj">IModuleObserver</param>
        void Attach( IModuleObserver obj );


        /// <summary>
        ///   Detach objewcts that observer this module
        /// </summary>
        /// <param name = "obj">IModuleObserver</param>
        void Detach( IModuleObserver obj );


        /// <summary>
        ///   Notify observers of this module
        /// </summary>
        void Notify();


        /// <summary>
        ///   Dispose this object and all the observers
        /// </summary>
        /// <param name = "obj">IModuleObserver</param>
        void Dispose( IModuleObserver obj );
    }
}