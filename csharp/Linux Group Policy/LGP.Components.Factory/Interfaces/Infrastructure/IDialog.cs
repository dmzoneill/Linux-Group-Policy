#region

using System.Windows.Controls;

#endregion

namespace LGP.Components.Factory.Interfaces.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDialog
    {
        /// <summary>
        ///   Sets the popup window child control
        /// </summary>
        /// <param name = "control">usercontrol</param>
        void SetChild( UserControl control );

        /// <summary>
        /// </summary>
        /// <param name = "width"></param>
        /// <param name = "height"></param>
        void ResizePopup( int width , int height );

        /// <summary>
        /// Closes the dialog
        /// </summary>
        void CloseDialog();

        /// <summary>
        /// Show the dialog, non blocking
        /// </summary>
        void ShowNonBlocking();

        /// <summary>
        /// Show the dialog, blocking
        /// </summary>
        void ShowBlocking();
    }
}