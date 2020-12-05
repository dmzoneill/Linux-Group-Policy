#region

using System;
using System.Windows.Controls;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using LGP.Components.Factory;

#endregion

namespace LGP.Modules.ModuleEditor.Internal.AvalonEditHelpers
{
    internal class CompletionData : ICompletionData
    {
        private static readonly Image MethodImage;
        private static readonly Image PropertyImage;
        private readonly int _type;

        static CompletionData()
        {
            try
            {
                MethodImage = Framework.Images.GetImage( "method" , "16x16" , 14 );
                PropertyImage = Framework.Images.GetImage( "property" , "16x16" , 14 );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        public CompletionData( string text , string enteredTest , int type )
        {
            this._type = type;
            this.Text = text;
            this.EnteredText = enteredTest;
        }

        public string EnteredText
        {
            get;
            private set;
        }

        #region ICompletionData Members

        public ImageSource Image
        {
            get
            {
                if( this._type == 0 )
                {
                    return MethodImage.Source;
                }
                if( this._type == 1 )
                {
                    return PropertyImage.Source;
                }
                return null;
            }
        }

        public string Text
        {
            get;
            private set;
        }

        // Use this property if you want to show a fancy UIElement in the list.
        public object Content
        {
            get
            {
                return this.Text;
            }
        }

        public object Description
        {
            get
            {
                return "";
            }
        }

        public double Priority
        {
            get
            {
                return 1.0;
            }
        }


        public void Complete( TextArea textArea , ISegment completionSegment , EventArgs insertionRequestEventArgs )
        {
            try
            {
                textArea.Document.Replace( completionSegment , this.Text.Replace( this.EnteredText , "" ) );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion
    }
}