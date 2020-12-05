#region

using System;
using System.Windows;
using LGP.Components.Factory;

#endregion

namespace LGP.Controls
{
    /// <summary>
    ///   Interaction logic for Background.xaml
    /// </summary>
    public partial class Background
    {
        /// <summary>
        ///   Constructor
        /// </summary>
        public Background()
        {
            try
            {
                this.InitializeComponent();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Window loaded Event
        /// </summary>
        /// <param name = "sender">object</param>
        /// <param name = "e">args</param>
        private void UserControlLoaded( object sender , RoutedEventArgs e )
        {
            try
            {
                this.presentationImage.Source = Framework.Images.GetImage( "lgp" , "128x128" ).Source;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}