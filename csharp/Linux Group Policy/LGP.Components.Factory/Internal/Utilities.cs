#region

using System;
using System.Reflection;
using System.Text;
using System.Windows.Markup;
using LGP.Components.Factory.Interfaces.Infrastructure;

#endregion

namespace LGP.Components.Factory.Internal
{
    /// <summary>
    ///   Utilies class used by the framework
    /// </summary>
    public class Utilities : IUtilities
    {
        private static Utilities _instance;


        private Utilities()
        {
        }

        #region IUtilities Members

        /// <summary>
        ///   Cleans a given string, allowing only characters and numbers
        /// </summary>
        /// <param name = "s">string</param>
        /// <returns>string</returns>
        public string CleanString( string s )
        {
            var cleaned = "";
            try
            {
                foreach( var c in s )
                {
                    if( char.IsLetterOrDigit( c ) )
                    {
                        cleaned += c.ToString();
                    }
                    else
                    {
                        Framework.Notification.Display( "Illegal character removed" , 4000 );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
            return cleaned;
        }


        /// <summary>
        ///   Convert epoch time to windows time
        /// </summary>
        /// <param name = "unixTime">seconds from 1970</param>
        /// <returns>DateTime</returns>
        public DateTime FromUnixTime( long unixTime )
        {
            var epoch = new DateTime( 1970 , 1 , 1 , 0 , 0 , 0 , DateTimeKind.Utc );
            return epoch.AddSeconds( unixTime );
        }


        /// <summary>
        /// Encodes string to base64
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Base64Encode( string data )
        {
            try
            {
                var encDataByte = Encoding.UTF8.GetBytes( data );
                var encodedData = Convert.ToBase64String( encDataByte );
                return encodedData;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return "";
            }
        }


        /// <summary>
        /// Decodes base64 string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Base64Decode( string data )
        {
            try
            {
                var encoder = new UTF8Encoding();
                var utf8Decode = encoder.GetDecoder();

                var todecodeByte = Convert.FromBase64String( data );
                var charCount = utf8Decode.GetCharCount( todecodeByte , 0 , todecodeByte.Length );
                var decodedChar = new char[charCount];
                utf8Decode.GetChars( todecodeByte , 0 , todecodeByte.Length , decodedChar , 0 );
                var result = new String( decodedChar );
                return result;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return "";
            }
        }

        /// <summary>
        /// Loads a resource file
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="tobject"></param>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        public object LoadResource( Assembly assembly , object tobject , string relativeUrl )
        {
            var location = tobject.GetType().Namespace + relativeUrl;

            var resource = assembly.GetManifestResourceStream( location );

            if( resource != null )
            {
                var obj = XamlReader.Load( resource , new ParserContext() );
                if( obj != null )
                {
                    return obj;
                }
            }

            return null;
        }

        #endregion

        /// <summary>
        /// Loads a resource file
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="absoluteUrl"></param>
        /// <returns></returns>
        public object LoadResource( Assembly assembly , string absoluteUrl )
        {
            var location = absoluteUrl;

            var resource = assembly.GetManifestResourceStream( location );

            if( resource != null )
            {
                var obj = XamlReader.Load( resource , new ParserContext() );
                if( obj != null )
                {
                    return obj;
                }
            }

            return null;
        }

        /// <summary>
        ///   Gets an instance of the utility class
        /// </summary>
        /// <returns></returns>
        public static Utilities GetInstance()
        {
            return _instance ?? ( _instance = new Utilities() );
        }
    }
}