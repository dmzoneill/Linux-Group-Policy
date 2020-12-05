#region

using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Resources;

#endregion

namespace ResxTranslator
{
    internal class Translator
    {
        public static readonly string[ ] LanguageNamesList =
            {
                "af"
                , "ar"
                , "be"
                , "bg"
                , "ca"
                , "cs"
                , "cy"
                , "da"
                , "de"
                , "el"
                , "es"
                , "et"
                , "fa"
                , "fi"
                , "fr"
                , "ga"
                , "gl"
                , "hi"
                , "hr"
                , "ht"
                , "hu"
                , "id"
                , "is"
                , "it"
                , "iw"
                , "ja"
                , "ko"
                , "lt"
                , "lv"
                , "mk"
                , "ms"
                , "mt"
                , "nl"
                , "no"
                , "pl"
                , "pt"
                , "ro"
                , "ru"
                , "sk"
                , "sl"
                , "sq"
                , "sr"
                , "sv"
                , "sw"
                , "th"
                , "tl"
                , "tr"
                , "uk"
                , "vi"
                , "yi"
                , "zh"
                , "zh-TW"
            };

        public void Translate( string path )
        {
            this.TranslateIntoLocales( path );
        }

        private void TranslateIntoLocales( string file )
        {
            Console.WriteLine( file + Environment.NewLine );

            foreach( var locale in LanguageNamesList )
            {
                var fileinfo = new FileInfo( file );
                var dirinfo = fileinfo.Directory;

                if( dirinfo == null )
                {
                    continue;
                }

                var localefile = dirinfo.FullName + "\\Resources." + locale + ".resx";
                this.TranslateLocale( file , locale , localefile );

                Console.WriteLine( locale + Environment.NewLine + " ========================================================================= " );
            }

            Console.WriteLine( Environment.NewLine );
        }

        private void TranslateLocale( string filename , string locale , string newfile )
        {
            var fileExists = File.Exists( newfile );

            if( fileExists )
            {
                File.Delete( newfile );
            }

            var reader = new ResXResourceReader( filename );
            var writer = new ResXResourceWriter( newfile );

            foreach( DictionaryEntry d in reader )
            {
                var originalString = d.Value.ToString();

                if( string.IsNullOrEmpty( originalString.Trim() ) )
                {
                    continue;
                }

                var langPair = "en|" + locale;
                var translatedString = GoogleTranslate.TranslateText( originalString , langPair );
                writer.AddResource( d.Key.ToString() , WebUtility.HtmlDecode( translatedString ) );
                Console.WriteLine(originalString + " == " + translatedString);
                System.Threading.Thread.Sleep( 500 );
            }

            writer.Close();
            reader.Close();
        }
    }
}