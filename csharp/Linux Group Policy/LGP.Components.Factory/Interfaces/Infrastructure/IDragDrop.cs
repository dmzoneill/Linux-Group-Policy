#region

using System.Windows;
using System.Windows.Input;

#endregion

namespace LGP.Components.Factory.Interfaces.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDragDrop
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dragElement"></param>
        /// <param name="e"></param>
        void StartDrag( FrameworkElement dragElement , MouseEventArgs e );
    }
}