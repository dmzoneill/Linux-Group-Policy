namespace LGP.Components.Factory.Interfaces.Infrastructure
{
    /// <summary>
    /// </summary>
    public interface INotification
    {
        /// <summary>
        ///   Displays the popup for x microsecond
        /// </summary>
        /// <param name = "text">the test message</param>
        /// <param name = "timeout">microseonds</param>
        void Display( string text , int timeout );
    }
}