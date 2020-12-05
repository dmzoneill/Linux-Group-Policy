#region

using System.Drawing;
using LGP.Components.Factory.Interfaces.Infrastructure;
using LGP.ImageLibrary;
using Image = System.Windows.Controls.Image;

#endregion

namespace LGP.Components.Factory.Internal
{
    /// <summary>
    ///   Image factory indirection, reducing coupling with components
    /// </summary>
    internal class ImageHandler : IImageHandler
    {
        private static ImageHandler _instance;


        /// <summary>
        ///   Constructor
        /// </summary>
        private ImageHandler()
        {
        }

        #region IImageHandler Members

        /// <summary>
        ///   Gets an image from the dll resources
        /// </summary>
        /// <param name = "name">The name of the image</param>
        /// <param name = "folder">The folder in which the resource can be found</param>
        /// <returns>An image</returns>
        public Image GetImage( string name , string folder )
        {
            return IconSet.GetImage( name , folder );
        }


        /// <summary>
        ///   Gets an image from the dll resources
        /// </summary>
        /// <param name = "name">The name of the image</param>
        /// <param name = "folder">The folder in which the resource can be found</param>
        /// <param name = "maxwidth">The maxwidth of the returned image</param>
        /// <returns>An image</returns>
        public Image GetImage( string name , string folder , int maxwidth )
        {
            return IconSet.GetImage( name , folder , maxwidth );
        }


        /// <summary>
        ///   Gets an icon stored in the dll resources
        /// </summary>
        /// <param name = "name">The name of the icon</param>
        /// <returns>An icon (ico)</returns>
        public Icon GetIcon( string name )
        {
            return IconSet.GetIcon( name );
        }


        /// <summary>
        ///   Gets the icon associated with the shell extension
        /// </summary>
        /// <param name = "filename">Filename</param>
        /// <returns></returns>
        public Image GetExtensionIcon( string filename )
        {
            return IconSet.GetExtensionIcon( filename );
        }


        /// <summary>
        ///   Convert bitmap to image
        /// </summary>
        /// <param name = "bitmap">Bitmap to convert</param>
        /// <returns>Image</returns>
        public Image ConvertBitmapToImage( Bitmap bitmap )
        {
            return IconSet.ConvertBitmapToImage( bitmap );
        }


        /// <summary>
        ///   Convert bitmap to image
        /// </summary>
        /// <param name = "bitmap">Bitmap to convert</param>
        /// <param name="size">image size</param>
        /// <returns>Image</returns>
        public Image ConvertBitmapToImage( Bitmap bitmap , int size )
        {
            return IconSet.ConvertBitmapToImage( bitmap );
        }

        #endregion

        public static IImageHandler GetInstance()
        {
            return _instance ?? ( _instance = new ImageHandler() );
        }
    }
}