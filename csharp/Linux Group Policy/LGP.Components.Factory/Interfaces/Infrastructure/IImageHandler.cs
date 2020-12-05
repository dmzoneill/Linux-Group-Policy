#region

using System.Drawing;
using Image = System.Windows.Controls.Image;

#endregion

namespace LGP.Components.Factory.Interfaces.Infrastructure
{
    /// <summary>
    ///   Interface for image handler
    /// </summary>
    public interface IImageHandler
    {
        /// <summary>
        ///   Gets an image from the dll resources
        /// </summary>
        /// <param name = "name">The name of the image</param>
        /// <param name = "folder">The folder in which the resource can be found</param>
        /// <returns>An image</returns>
        Image GetImage( string name , string folder );


        /// <summary>
        ///   Gets an image from the dll resources
        /// </summary>
        /// <param name = "name">The name of the image</param>
        /// <param name = "folder">The folder in which the resource can be found</param>
        /// <param name = "maxwidth">The maxwidth of the returned image</param>
        /// <returns>An image</returns>
        Image GetImage( string name , string folder , int maxwidth );


        /// <summary>
        ///   Gets an icon stored in the dll resources
        /// </summary>
        /// <param name = "name">The name of the icon</param>
        /// <returns>An icon (ico)</returns>
        Icon GetIcon( string name );


        /// <summary>
        ///   Gets the icon associated with the shell extension
        /// </summary>
        /// <param name = "filename">Filename</param>
        /// <returns></returns>
        Image GetExtensionIcon( string filename );


        /// <summary>
        ///   Convert bitmap to image
        /// </summary>
        /// <param name = "bitmap">Bitmap to convert</param>
        /// <returns>Image</returns>
        Image ConvertBitmapToImage( Bitmap bitmap );


        /// <summary>
        ///   Convert bitmap to image
        /// </summary>
        /// <param name = "bitmap">Bitmap to convert</param>
        /// <param name="size">image size</param>
        /// <returns>Image</returns>
        Image ConvertBitmapToImage( Bitmap bitmap , int size );
    }
}