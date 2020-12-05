#region

using System;
using System.Windows;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Rendering;
using LGP.Components.Factory;

#endregion

namespace LGP.Modules.PolicyEditor.Internal
{
    internal class HighlightCurrentLineBackgroundRenderer : IBackgroundRenderer
    {
        private readonly TextEditor _editor;

        public HighlightCurrentLineBackgroundRenderer( TextEditor editor )
        {
            this._editor = editor;
        }

        #region IBackgroundRenderer Members

        public KnownLayer Layer
        {
            get
            {
                return KnownLayer.Caret;
            }
        }

        public void Draw( TextView textView , DrawingContext drawingContext )
        {
            if( this._editor.Document == null )
            {
                return;
            }

            try
            {
                textView.EnsureVisualLines();
                var currentLine = this._editor.Document.GetLineByOffset( this._editor.CaretOffset );
                foreach( var rect in BackgroundGeometryBuilder.GetRectsForSegment( textView , currentLine ) )
                {
                    if( textView.ActualWidth - 32 > 0 && rect.Height > 0 )
                    {
                        var Size = new Size( textView.ActualWidth - 32 , rect.Height );
                        var Rect = new Rect( rect.Location , Size );
                        drawingContext.DrawRectangle( new SolidColorBrush( Color.FromArgb( 50 , 250 , 250 , 100 ) ) , null , Rect );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion
    }
}