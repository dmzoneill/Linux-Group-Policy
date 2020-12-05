#region

using System;

#endregion

namespace LGP.Components.Factory.Publishers.Events
{
    /// <summary>
    ///   Derived Event to which subscribers can listen for
    /// </summary>
    public class PanelEvent : Event
    {
        /// <summary>
        ///   Constructor
        /// </summary>
        /// <param name = "sender"></param>
        public PanelEvent( Object sender ) : base( sender )
        {
        }
    }
}