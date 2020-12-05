#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Rendering;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Modules.ModuleEditor.Internal.AvalonEditHelpers;

#endregion

namespace LGP.Modules.ModuleEditor.Internal
{
    /// <summary>
    /// </summary>
    public class ModuleDocument : INotifyPropertyChanged , IModuleObserver
    {
        private static readonly BraceFoldingStrategy BraceFoldingStrategy;
        private static List< string > _perlMethodNames;
        private static IHighlightingDefinition _perlHighlighterDefintion;
        private readonly Label _col;
        private readonly TextEditor _editor1;
        private readonly TextEditor _editor2;
        private readonly Label _length;
        private readonly Label _lines;
        private readonly ModuleEditor _parent;
        private readonly Label _row;
        private readonly Label _selection;
        private CompletionWindow _completionWindow;
        private TextDocument _document;
        private FoldingManager _foldingManager1;
        private FoldingManager _foldingManager2;
        private int _hashContents;
        private IModule _module;
        private List< string > _perlVariables;


        static ModuleDocument()
        {
            try
            {
                BraceFoldingStrategy = new BraceFoldingStrategy();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Constructor
        /// </summary>
        /// <param name = "parent">ModuleEditor</param>
        /// <param name = "module">Document to bind</param>
        /// <param name = "avalon1">TextEditor</param>
        /// <param name = "avalon2">TextEditor</param>
        public ModuleDocument( IModule module , ModuleEditor parent , TextEditor avalon1 , TextEditor avalon2 )
        {
            try
            {
                this._parent = parent;
                this._editor1 = avalon1;
                this._editor2 = avalon2;
                this._module = module;
                this._module.Attach( this );

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
                this.LoadHighlighter();
                this.SetHandlers();
                this.Bind();
                this.PrepareFoldingStrategy();

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
        public Label SelectionLength
        {
            get
            {
                return this._selection;
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        ///   Event for the document change
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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

                this._editor2.ShowLineNumbers = true;
                this._editor2.Options.AllowScrollBelowDocument = true;
                this._editor2.Options.ConvertTabsToSpaces = true;
                this._editor2.Options.EnableEmailHyperlinks = true;
                this._editor2.Options.EnableHyperlinks = true;
                this._editor2.Options.EnableRectangularSelection = true;
                this._editor2.Options.EnableTextDragDrop = true;
                this._editor2.Options.IndentationSize = 4;
                this._editor2.Options.InheritWordWrapIndentation = true;
                this._editor2.Options.ShowBoxForControlCharacters = true;
                this._editor2.Options.ShowTabs = true;

                var myBinding = new Binding( "WordWrap" )
                {
                    Source = this._editor1
                };
                this._editor2.SetBinding( TextEditor.WordWrapProperty , myBinding );

                var myBinding2 = new Binding( "ShowLineNumbers" )
                {
                    Source = this._editor1
                };
                this._editor2.SetBinding( TextEditor.ShowLineNumbersProperty , myBinding2 );

                this._editor1.TextArea.TextView.BackgroundRenderers.Add( new HighlightCurrentLineBackgroundRenderer( this._editor1 ) );

                this._editor1.TextArea.Caret.PositionChanged += this.CaretPositionChanged1;

                this._editor2.TextArea.TextView.BackgroundRenderers.Add( new HighlightCurrentLineBackgroundRenderer( this._editor2 ) );

                this._editor2.TextArea.Caret.PositionChanged += this.CaretPositionChanged2;
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

        private void CaretPositionChanged2( object sender , EventArgs e )
        {
            this.CaretChangedUpdater( this._editor2 );
        }


        private void PrepareFoldingStrategy()
        {
            try
            {
                this._foldingManager1 = FoldingManager.Install( this._editor1.TextArea );
                BraceFoldingStrategy.UpdateFoldings( this._foldingManager1 , this._document );

                this._foldingManager2 = FoldingManager.Install( this._editor2.TextArea );
                BraceFoldingStrategy.UpdateFoldings( this._foldingManager2 , this._document );

                var foldingUpdateTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds( 3 )
                };
                foldingUpdateTimer.Tick += this.BraceFoldingUpdateTimerTick;
                foldingUpdateTimer.Start();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void BraceFoldingUpdateTimerTick( object sender , EventArgs e )
        {
            try
            {
                BraceFoldingStrategy.UpdateFoldings( this._foldingManager1 , this._document );
                BraceFoldingStrategy.UpdateFoldings( this._foldingManager2 , this._document );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
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
                BindingOperations.SetBinding( this._editor2 , TextEditor.DocumentProperty , documentBinding );

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
                this._editor2.TextArea.Caret.PositionChanged += this.CaretPositionChanged2;
                this._editor1.TextArea.TextEntering += this.TextAreaTextEntering1;
                this._editor2.TextArea.TextEntering += this.TextAreaTextEntering2;
                this._editor1.TextArea.TextEntered += this.TextAreaTextEntered1;
                this._editor2.TextArea.TextEntered += this.TextAreaTextEntered2;
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


        private void PrepareEditorContents()
        {
            try
            {
                this.Document = new TextDocument
                {
                    Text = this._module.FileContents
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


        private void LoadHighlighter()
        {
            try
            {
                var extension = Path.GetExtension( this._module.GetModuleLocation() );
                if( extension != null )
                {
                    var ext = extension.ToLower();

                    if( ext.CompareTo( ".pl" ) == 0 || ext.CompareTo( ".pm" ) == 0 )
                    {
                        if( _perlHighlighterDefintion == null )
                        {
                            this.LoadPerlSyntaxHighlighter();
                            LoadPerlMethodNames();
                        }

                        this.ScanVariableNames();

                        this._editor1.SyntaxHighlighting = _perlHighlighterDefintion;
                        this._editor2.SyntaxHighlighting = _perlHighlighterDefintion;
                    }
                    else
                    {
                        this._editor1.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension( extension );
                        this._editor2.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension( extension );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void ScanVariableNames()
        {
            try
            {
                this._perlVariables = new List< string >();

                var variableRegex = new Regex( "(\\${1,2}|\\$?@|\\$?%)([A-Za-z0-9_]+)" );

                var collection = variableRegex.Matches( this._document.Text );

                foreach( Match match in collection )
                {
                    if( !this._perlVariables.Contains( match.ToString() ) )
                    {
                        this._perlVariables.Add( match.ToString() );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private static void LoadPerlMethodNames()
        {
            try
            {
                if( _perlMethodNames == null )
                {
                    var fileStream = new FileStream( "perl.xshd" , FileMode.Open , FileAccess.Read );
                    var reader = new XmlTextReader( fileStream );

                    _perlMethodNames = new List< string >();

                    while( reader.Read() )
                    {
                        if( reader.NodeType == XmlNodeType.Element )
                        {
                            reader.Read();
                            _perlMethodNames.Add( reader.Value );
                        }
                    }

                    reader.Close();
                    fileStream.Close();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void LoadPerlSyntaxHighlighter()
        {
            try
            {
                if( _perlHighlighterDefintion == null )
                {
                    var fileStream = new FileStream( "perl.xshd" , FileMode.Open , FileAccess.Read );
                    var reader = new XmlTextReader( fileStream );
                    _perlHighlighterDefintion = HighlightingLoader.Load( reader , HighlightingManager.Instance );
                    reader.Close();
                    fileStream.Close();
                }
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


        private void TextAreaTextEntered2( object sender , TextCompositionEventArgs e )
        {
            this.HandleTextEntered( this._editor2 );
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

                    for( var h = 0; h < _perlMethodNames.Count; h++ )
                    {
                        if( _perlMethodNames[ h ].StartsWith( startswith ) )
                        {
                            data.Add( new CompletionData( _perlMethodNames[ h ] , startswith , 0 ) );
                        }
                    }

                    for( var h = 0; h < this._perlVariables.Count; h++ )
                    {
                        if( this._perlVariables[ h ].StartsWith( startswith ) )
                        {
                            data.Add( new CompletionData( this._perlVariables[ h ] , startswith , 1 ) );
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

        private void TextAreaTextEntering2( object sender , TextCompositionEventArgs e )
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
        /// </summary>
        /// <param name = "text"></param>
        public void GoTo( string text )
        {
            try
            {
                var location = this._document.GetLocation( this._document.Text.IndexOf( text ) );
                this._editor1.ScrollToLine( location.Line );
                this._editor2.ScrollToLine( location.Line );
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

        #region Implementation of IModuleObserver

        /// <summary>
        ///   Update this observer with a refernece to the module
        /// </summary>
        /// <param name = "module">IModule</param>
        /// <param name = "source">IModuleObserver</param>
        public void Update( IModule module , IModuleObserver source )
        {
            try
            {
                this._module = module;

                var replacePackage = new Regex( "package\\s+([A-Za-z0-9_]+)(\\s+)?;" );
                var match = replacePackage.Match( this._document.Text );

                if( this._module.GetModuleName().CompareTo( match.Groups[ 1 ].ToString() ) != 0 )
                {
                    this._document.Text = this._document.Text.Replace( match.Groups[ 0 ].ToString() , "package " + this._module.GetModuleName() + ";" );
                    this._hashContents = this._document.Text.GetHashCode();
                    this._module.FileContents = this._document.Text;
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Attach objewcts that observer this module
        /// </summary>
        /// <param name = "obj">IModuleObserver</param>
        public void Attach( IModuleObserver obj )
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
        ///   Detach objewcts that observer this module
        /// </summary>
        /// <param name = "obj">IModuleObserver</param>
        public void Detach( IModuleObserver obj )
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
        ///   Notify observers of this module
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
        /// <param name = "obj">IModuleObserver</param>
        public void Dispose( IModuleObserver obj )
        {
            try
            {
                if (this._module != null)
                {
                    this._module.Detach( this );
                    this._module = null;
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