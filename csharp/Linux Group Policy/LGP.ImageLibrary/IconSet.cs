#region

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;
using LGP.ImageWindowsShellIcons;
using Image = System.Windows.Controls.Image;

#endregion

namespace LGP.ImageLibrary
{
    /// <summary>
    ///   The Control class for accessing the image resources
    /// </summary>
    public class IconSet
    {
        private static readonly string Namepsace;


        /// <summary>
        ///   Constructor
        /// </summary>
        static IconSet()
        {
            var myType = typeof( IconSet );
            Namepsace = myType.Namespace;
        }


        [DllImport( "shell32.dll" , CharSet = CharSet.Auto )]
        internal static extern int ExtractIconEx( string stExeFileName , int nIconIndex , IntPtr phiconLarge , IntPtr phiconSmall , int nIcons );


        /// <summary>
        ///   Gets an image from the dll resources
        /// </summary>
        /// <param name = "name">The name of the image</param>
        /// <param name = "folder">The folder in which the resource can be found</param>
        /// <returns>An image</returns>
        public static Image GetImage( string name , string folder )
        {
            var uri = "/" + Namepsace + ";component/Images/" + folder + "/" + name + ".png";

            var icon = new Image();

            try
            {
                var bmImage = new BitmapImage();
                bmImage.BeginInit();
                bmImage.UriSource = new Uri( uri , UriKind.Relative );
                bmImage.EndInit();
                icon.Source = bmImage;
            }
            catch( Exception )
            {
                icon = null;
            }

            return icon;
        }


        /// <summary>
        ///   Gets an image from the dll resources
        /// </summary>
        /// <param name = "name">The name of the image</param>
        /// <param name = "folder">The folder in which the resource can be found</param>
        /// <param name = "maxwidth">The maxwidth of the returned image</param>
        /// <returns>An image</returns>
        public static Image GetImage( string name , string folder , int maxwidth )
        {
            Image icon;

            try
            {
                icon = GetImage( name , folder );
                icon.MaxWidth = maxwidth;
            }
            catch( Exception )
            {
                icon = null;
            }

            return icon;
        }


        /// <summary>
        ///   Gets an icon stored in the dll resources
        /// </summary>
        /// <param name = "name">The name of the icon</param>
        /// <returns>An icon (ico)</returns>
        public static Icon GetIcon( string name )
        {
            var uri = "pack://application:,,,/" + Namepsace + ";component/Icons/" + name + ".ico";

            Icon icon = null;

            try
            {
                var streamResourceInfo = Application.GetResourceStream( new Uri( uri ) );
                if( streamResourceInfo != null )
                {
                    var iconStream = streamResourceInfo.Stream;
                    icon = new Icon( iconStream );
                }
            }
            catch( Exception )
            {
                icon = null;
            }

            return icon;
        }


        /// <summary>
        ///   Gets the icon associated with the shell extension
        /// </summary>
        /// <param name = "filename"></param>
        /// <returns></returns>
        public static Image GetExtensionIcon( string filename )
        {
            var bmp = ShellIcon.GetSmallIcon( filename ).ToBitmap();
            var stream = new MemoryStream();
            bmp.Save( stream , ImageFormat.Png );
            var bmpImage = new BitmapImage();
            bmpImage.BeginInit();
            stream.Seek( 0 , SeekOrigin.Begin );
            bmpImage.StreamSource = stream;
            bmpImage.EndInit();

            var j = new Image
            {
                Source = bmpImage
            };
            return j;
        }


        /// <summary>
        ///   Convert bitmap to image
        /// </summary>
        /// <param name = "bitmap">Bitmap to convert</param>
        /// <returns>Image</returns>
        public static Image ConvertBitmapToImage( Bitmap bitmap )
        {
            var bmp = bitmap;
            var stream = new MemoryStream();
            bmp.Save( stream , ImageFormat.Png );
            var bmpImage = new BitmapImage();
            bmpImage.BeginInit();
            stream.Seek( 0 , SeekOrigin.Begin );
            bmpImage.StreamSource = stream;
            bmpImage.EndInit();

            var j = new Image
            {
                Source = bmpImage
            };
            return j;
        }


        /// <summary>
        ///   Convert bitmap to image
        /// </summary>
        /// <param name = "bitmap">Bitmap to convert</param>
        /// <param name="size">image size</param>
        /// <returns>Image</returns>
        public static Image ConvertBitmapToImage(Bitmap bitmap , int size )
        {
            var bmp = bitmap;
            var stream = new MemoryStream();
            bmp.Save(stream, ImageFormat.Png);
            var bmpImage = new BitmapImage();
            bmpImage.BeginInit();
            stream.Seek(0, SeekOrigin.Begin);
            bmpImage.StreamSource = stream;
            bmpImage.EndInit();

            var j = new Image
            {
                Source = bmpImage , MaxWidth = size
            };


            return j;
        }
    }
}