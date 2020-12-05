#region

using System;
using System.Drawing;
using System.Runtime.InteropServices;

#endregion

namespace LGP.ImageWindowsShellIcons
{
    /// <summary>
    ///   Retrievs shell info associated with a file or filetype
    /// </summary>
    /// <summary>
    ///   Get a 32x32 or 16x16 System.Drawing.Icon depending on which function you call
    ///   either GetSmallIcon(string fileName) or GetLargeIcon(string fileName)
    /// </summary>
    public class ShellIcon
    {
        /// <summary>
        ///   Gets the small icon from the win32 api invocation
        /// </summary>
        /// <param name = "fileName">The file name to lookup ( extension )</param>
        /// <returns></returns>
        public static Icon GetSmallIcon( string fileName )
        {
            var shinfo = new Shfileinfo();
            var hImgSmall = Win32.SHGetFileInfo( fileName , 0 , ref shinfo , ( uint ) Marshal.SizeOf( shinfo ) , Win32.ShgfiIcon | Win32.ShgfiSmallicon | Win32.UseFileAtrributes );
            //The icon is returned in the hIcon member of the shinfo struct
            var icon = ( Icon ) Icon.FromHandle( shinfo.hIcon ).Clone();
            Win32.DestroyIcon( shinfo.hIcon );
            return icon;
        }


        /// <summary>
        ///   Gets the large icon from the win32 api invocation
        /// </summary>
        /// <param name = "fileName">The file name to lookup ( extension )</param>
        /// <returns></returns>
        public static Icon GetLargeIcon( string fileName )
        {
            var shinfo = new Shfileinfo();
            var hImgLarge = Win32.SHGetFileInfo( fileName , 0 , ref shinfo , ( uint ) Marshal.SizeOf( shinfo ) , Win32.ShgfiIcon | Win32.ShgfiLargeicon | Win32.UseFileAtrributes );
            var icon = ( Icon ) Icon.FromHandle( shinfo.hIcon ).Clone();
            Win32.DestroyIcon( shinfo.hIcon );
            return icon;
        }

        #region Nested type: Shfileinfo

        /// <summary>
        ///   Struct represting the information returned by the pinvoke
        /// </summary>
        [StructLayout( LayoutKind.Sequential )]
        public struct Shfileinfo
        {
            /// <summary>
            ///   Pointer to the icon
            /// </summary>
            public IntPtr hIcon;

            /// <summary>
            ///   Pointer to the icon
            /// </summary>
            public IntPtr iIcon;

            /// <summary>
            ///   Uint represting the drawing attributes
            /// </summary>
            public uint dwAttributes;

            /// <summary>
            ///   Display name
            /// </summary>
            [MarshalAs( UnmanagedType.ByValTStr , SizeConst = 260 )]
            public string szDisplayName;

            /// <summary>
            ///   Size type name
            /// </summary>
            [MarshalAs( UnmanagedType.ByValTStr , SizeConst = 80 )]
            public string szTypeName;
        };

        #endregion

        #region Nested type: Win32

        private class Win32
        {
            public const uint ShgfiIcon = 0x100;
            public const uint ShgfiLargeicon = 0x0; // Large icon
            public const uint ShgfiSmallicon = 0x1; // Small icon
            public const uint UseFileAtrributes = 0x000000010; // when the full path isn't available


            [DllImport( "shell32.dll" )]
            public static extern IntPtr SHGetFileInfo( string pszPath , uint dwFileAttributes , ref Shfileinfo psfi , uint cbSizeFileInfo , uint uFlags );


            [DllImport( "User32.dll" )]
            public static extern int DestroyIcon( IntPtr hIcon );
        }

        #endregion
    }
}