namespace LGP.Components.Factory.Interfaces.Module
{
    /// <summary>
    ///   Interface for UserControls to supply Dispose method
    /// </summary>
    public interface IUserControl
    {
        /// <summary>
        ///   Aks the UserControl to do some clean up!
        /// </summary>
        void Dispose();

        /// <summary>
        /// Asks the Usercontrol to refresh itself
        /// </summary>
        void Refresh();

        /// <summary>
        /// When the control is no longer in focus, asks it to pause ( heavy duty non critical, ie threads updating graphs )
        /// </summary>
        void Pause();

        /// <summary>
        /// When the tab comes back into focus, resume ( heavy duty non critical, ie threads updating graphs )
        /// </summary>
        void Resume();
    }
}