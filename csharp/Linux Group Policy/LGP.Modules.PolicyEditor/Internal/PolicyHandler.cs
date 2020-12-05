#region

using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Rendering;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Modules.PolicyEditor.Internal
{
    /// <summary>
    /// </summary>
    public class PolicyHandler : IPolicyObserver , INotifyPropertyChanged
    {
        private static IHighlightingDefinition _highLightDefintion;
        private readonly Label _col;
        private readonly TextEditor _editor1;
        private readonly Label _length;
        private readonly Label _lines;
        private readonly PolicyEditor _parent;
        private readonly Label _row;
        private readonly Label _selection;
        private int _caretoffset;
        private CompletionWindow _completionWindow;
        private TextDocument _document;
        private int _hashContents;
        private IPolicy _policy;

        /// <summary>
        /// </summary>
        /// <param name = "parent"></param>
        /// <param name = "aeditor1"></param>
        /// <param name = "policy"></param>
        public PolicyHandler( PolicyEditor parent , TextEditor aeditor1 , IPolicy policy )
        {
            try
            {
                this._parent = parent;
                this._editor1 = aeditor1;
                this._policy = policy;
                this._policy.Attach( this );

                this._col = new Label
                {
                    Margin = new Thickness( 0 ) , Padding = new Thickness( 0 ) , FontSize = 12 , Height = 28 , Content = "0"
                };
                this._length = new Label
                {
                    Margin = new Thickness( 0 ) , Padding = new Thickness( 0 ) , FontSize = 12 , Height = 28 , Content = "0"
                };
                this._lines = new Label
                {
                    Margin = new Thickness( 0 ) , Padding = new Thickness( 0 ) , FontSize = 12 , Height = 28 , Content = "0"
                };
                this._row = new Label
                {
                    Margin = new Thickness( 0 ) , Padding = new Thickness( 0 ) , FontSize = 12 , Height = 28 , Content = "0"
                };
                this._selection = new Label
                {
                    Margin = new Thickness( 0 ) , Padding = new Thickness( 0 ) , FontSize = 12 , Height = 28 , Content = "0"
                };

                this.SetUpLookAndFeel();
                this.PrepareEditorContents();
                this.LoadGrammerKeyHighLightDefintion();
                this.SetHandlers();
                this.Bind();

                this._document.UndoStack = new UndoStack();
                this._hashContents = this._document.Text.GetHashCode();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Document associated with this object
        /// </summary>
        public TextDocument Document
        {
            get
            {
                return this._document;
            }
            set
            {
                this._document = value;
                this.OnPropertyChanged( "DocumentChanged" );
            }
        }


        /// <summary>
        ///   Get the lines count
        /// </summary>
        public Label LineCount
        {
            get
            {
                return this._lines;
            }
        }


        /// <summary>
        ///   Get the module lenght
        /// </summary>
        public Label FileLength
        {
            get
            {
                return this._length;
            }
        }


        /// <summary>
        ///   Get the caret row
        /// </summary>
        public Label CaretRow
        {
            get
            {
                return this._row;
            }
        }


        /// <summary>
        ///   Get the caret row
        /// </summary>
        public Label CaretCol
        {
            get
            {
                return this._col;
            }
        }

        /// <summary>
        ///   Get the caret row
        /// </summary>
        public int CaretOffset
        {
            get
            {
                return this._caretoffset;
            }
            set
            {
                this._editor1.CaretOffset = value;
                this._caretoffset = value;
            }
        }

        /// <summary>
        ///   Get the caret row
        /// </summary>
        public Label SelectionLength
        {
            get
            {
                return this._selection;
            }
        }

        #region Implementation of IPolicyObserver

        /// <summary>
        ///   Update this observer with a refernece to the ou
        /// </summary>
        /// <param name = "policy">IPolicy</param>
        /// <param name = "source">source observer</param>
        public void Update( IPolicy policy , IPolicyObserver source )
        {
            try
            {
                this._policy = policy;
                this._document.Text = this._policy.GetDsl();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Attach objects that observer this IPolicy
        /// </summary>
        /// <param name = "obj">IPolicyObserver</param>
        public void Attach( IPolicyObserver obj )
        {
            try
            {
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Detach objects that observer this IPolicy
        /// </summary>
        /// <param name = "obj">IPolicyObserver</param>
        public void Detach( IPolicyObserver obj )
        {
            try
            {
                this._policy.Detach( this );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Notify observers of this IPolicy
        /// </summary>
        public void Notify()
        {
            try
            {
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Dispose this object and all the observers
        /// </summary>
        /// <param name = "obj">IPolicyObserver</param>
        public void Dispose( IPolicyObserver obj )
        {
            try
            {
                this._policy.Detach( this );
                this._policy = null;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion

        private void SetUpLookAndFeel()
        {
            try
            {
                this._editor1.ShowLineNumbers = true;
                this._editor1.Options.AllowScrollBelowDocument = true;
                this._editor1.Options.ConvertTabsToSpaces = true;
                this._editor1.Options.EnableEmailHyperlinks = true;
                this._editor1.Options.EnableHyperlinks = true;
                this._editor1.Options.EnableRectangularSelection = true;
                this._editor1.Options.EnableTextDragDrop = true;
                this._editor1.Options.IndentationSize = 4;
                this._editor1.Options.InheritWordWrapIndentation = true;
                this._editor1.Options.ShowBoxForControlCharacters = true;
                this._editor1.Options.ShowTabs = true;

                this._editor1.TextArea.TextView.BackgroundRenderers.Add( new HighlightCurrentLineBackgroundRenderer( this._editor1 ) );

                this._editor1.TextArea.Caret.PositionChanged += this.CaretPositionChanged1;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void CaretPositionChanged1( object sender , EventArgs e )
        {
            this.CaretChangedUpdater( this._editor1 );
        }

        private void Bind()
        {
            try
            {
                var documentBinding = new Binding( "Document" )
                {
                    Source = this
                };
                BindingOperations.SetBinding( this._editor1 , TextEditor.DocumentProperty , documentBinding );

                BindingOperations.SetBinding( this._parent.FileLine , ContentControl.ContentProperty , new Binding( "CaretRow" )
                {
                    Source = this
                } );
                BindingOperations.SetBinding( this._parent.FileColumn , ContentControl.ContentProperty , new Binding( "CaretCol" )
                {
                    Source = this
                } );
                BindingOperations.SetBinding( this._parent.FileSelection , ContentControl.ContentProperty , new Binding( "SelectionLength" )
                {
                    Source = this
                } );
                BindingOperations.SetBinding( this._parent.FileLength , ContentControl.ContentProperty , new Binding( "FileLength" )
                {
                    Source = this
                } );
                BindingOperations.SetBinding( this._parent.FileLines , ContentControl.ContentProperty , new Binding( "LineCount" )
                {
                    Source = this
                } );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void SetHandlers()
        {
            try
            {
                this.Document.TextChanged += this.DocumentTextChanged;
                this._editor1.TextArea.Caret.PositionChanged += this.CaretPositionChanged1;
                this._editor1.TextArea.TextEntering += this.TextAreaTextEntering1;
                this._editor1.TextArea.TextEntered += this.TextAreaTextEntered1;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void CaretChangedUpdater( TextEditor editor )
        {
            try
            {
                editor.TextArea.TextView.InvalidateLayer( KnownLayer.Background );
                this._col.Content = editor.TextArea.Caret.Column;
                this._row.Content = editor.TextArea.Caret.Line;
                this._selection.Content = editor.SelectionLength;
                this._length.Content = editor.Text.Length;
                this._caretoffset = editor.CaretOffset;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void OnPropertyChanged( string info )
        {
            try
            {
                var handler = this.PropertyChanged;
                if( handler != null )
                {
                    handler( this , new PropertyChangedEventArgs( info ) );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void LoadGrammerKeyHighLightDefintion()
        {
            try
            {
                var template = File.ReadAllText( "dsltemplate.xshd" );

                template = template.Replace( "{words}" , this.BuildGrammerKeys() );

                var stream = new MemoryStream( Encoding.ASCII.GetBytes( template ) );

                var reader = new XmlTextReader( stream );
                _highLightDefintion = HighlightingLoader.Load( reader , HighlightingManager.Instance );
                reader.Close();

                this._editor1.SyntaxHighlighting = _highLightDefintion;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private string BuildGrammerKeys()
        {
            var gw = Framework.Database.CreateGrammerGateway();
            var names = "";
            var grammers = gw.GetGrammer();

            for( var k = 0; k < grammers.Count - 1; k++ )
            {
                try
                {
                    var next = grammers[ k ].GetKey() + "|";
                    names += next;
                }
                catch( Exception error )
                {
                    Framework.EventBus.Publish( error.Message );
                }
            }
            try
            {
                names += grammers[ grammers.Count - 1 ].GetKey();
            }
            catch( Exception error )
            {
                names = names.Substring( 0 , names.Length - 1 );
                Framework.EventBus.Publish( error.Message );
            }

            return names;
        }

        private void PrepareEditorContents()
        {
            try
            {
                this.Document = new TextDocument
                {
                    Text = this._policy.GetDsl()
                };

                this._lines.Content = this.Document.LineCount;
                this._length.Content = this.Document.TextLength;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void DocumentTextChanged( object sender , EventArgs e )
        {
            try
            {
                this._lines.Content = this.Document.LineCount;
                this._length.Content = this.Document.TextLength;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void TextAreaTextEntered1( object sender , TextCompositionEventArgs e )
        {
            this.HandleTextEntered( this._editor1 );
        }

        private void HandleTextEntered( TextEditor editor )
        {
            try
            {
                var currentline = editor.Document.GetLineByOffset( editor.CaretOffset );
                var startswith = "";
                const char space = ' ';

                var currentPos = editor.CaretOffset;
                var i = currentPos - 1;

                while( i > -1 && i > currentline.Offset - 1 )
                {
                    var previousChar = this._document.GetCharAt( i );
                    if( previousChar.CompareTo( space ) == 0 || previousChar.CompareTo( '\t' ) == 0 )
                    {
                        i = -2;
                    }
                    else
                    {
                        startswith = this._document.GetCharAt( i ) + startswith;
                        i--;
                    }
                }

                if( startswith.Length > 0 )
                {
                    this._completionWindow = new CompletionWindow( editor.TextArea )
                    {
                        WindowStyle = WindowStyle.None , Background = null , AllowsTransparency = true
                    };
                    this._completionWindow.Closed += delegate
                    {
                        this._completionWindow = null;
                    };

                    var data = this._completionWindow.CompletionList.CompletionData;

                    var gw = Framework.Database.CreateGrammerGateway();
                    var grammers = gw.GetGrammer();

                    foreach( var gr in grammers )
                    {
                        if( gr.GetKey().StartsWith( startswith ) )
                        {
                            data.Add( new CompletionData( gr.GetKey() , startswith , 0 ) );
                        }
                    }

                    if( data.Count > 0 )
                    {
                        this._completionWindow.Show();
                    }
                }

                this._hashContents = this._document.Text.GetHashCode();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void TextAreaTextEntering1( object sender , TextCompositionEventArgs e )
        {
            this.TextEntering( e );
        }

        private void TextEntering( TextCompositionEventArgs e )
        {
            try
            {
                if( e.Text.Length > 0 && this._completionWindow != null )
                {
                    if( !char.IsLetterOrDigit( e.Text[ 0 ] ) )
                    {
                        this._completionWindow.CompletionList.RequestInsertion( e );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Gets the has of the document
        /// </summary>
        /// <returns></returns>
        public int GetHash()
        {
            return this._hashContents;
        }

        #region Implementation of INotifyPropertyChanged

        /// <summary>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}