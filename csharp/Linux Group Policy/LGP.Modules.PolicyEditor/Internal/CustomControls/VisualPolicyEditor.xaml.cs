#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Modules.PolicyEditor.Internal.CustomControls
{
    /// <summary>
    /// Interaction logic for VisualPolicyEditor.xaml
    /// </summary>
    public partial class VisualPolicyEditor : IPolicyObserver
    {
        #region Delegates

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public delegate Point Position( IInputElement element );

        #endregion

        private readonly ObservableCollection< VisualPolicyEntry > _list;
        private object _addedEntry;
        private bool _addingNewEntry;
        private int _colCount;
        private IGrammerGateway _ggw;
        private PolicyHandler _handler;
        private PolicyEditor _parent;
        private IPolicy _policy;
        private int _rowIndex = -1;
        private object _selection;
        private bool _updatingFromEditor;

        /// <summary>
        /// Constructor
        /// </summary>
        public VisualPolicyEditor()
        {
            try
            {
                this.InitializeComponent();
                this._list = new ObservableCollection< VisualPolicyEntry >();
                this.dataGrid1.PreviewMouseLeftButtonDown += this.DataGridPreviewMouseLeftButtonDown;
                this.dataGrid1.Drop += this.DataGridDrop;
                this.Tag = "Statistics Viewer";
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void DataGridDrop( object sender , DragEventArgs e )
        {
            try
            {
                if( this._rowIndex < 0 )
                {
                    return;
                }

                var index = this.GetCurrentRowIndex( e.GetPosition );

                if( index < 0 || index == this._rowIndex || index >= this._list.Count )
                {
                    return;
                }

                var changed = this._list[ this._rowIndex ];
                this._list.RemoveAt( this._rowIndex );
                this._list.Insert( index , changed );

                this.dataGrid1.Items.Refresh();

                this._updatingFromEditor = false;

                this.UpdateDsl();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void DataGridPreviewMouseLeftButtonDown( object sender , MouseButtonEventArgs e )
        {
            try
            {
                if( Keyboard.Modifiers != ModifierKeys.Shift )
                {
                    return;
                }

                this.dataGrid1.CommitEdit( DataGridEditingUnit.Row , true );

                this._rowIndex = this.GetCurrentRowIndex( e.GetPosition );

                if( this._rowIndex >= this._list.Count || this._rowIndex == -1 )
                {
                    return;
                }

                var row = ( DataGridRow ) this.dataGrid1.ItemContainerGenerator.ContainerFromIndex( this._rowIndex );
                if( row == null )
                {
                    // May be virtualized, bring into view and try again.
                    this.dataGrid1.UpdateLayout();
                    this.dataGrid1.ScrollIntoView( this.dataGrid1.Items[ this._rowIndex ] );
                    row = ( DataGridRow ) this.dataGrid1.ItemContainerGenerator.ContainerFromIndex( this._rowIndex );
                }

                Framework.DragDrop.StartDrag( row , e );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private bool GetMouseTargetRow( Visual theTarget , Position position )
        {
            var rect = VisualTreeHelper.GetDescendantBounds( theTarget );
            var point = position( ( IInputElement ) theTarget );
            return rect.Contains( point );
        }


        private DataGridRow GetRowItem( int index )
        {
            if( this.dataGrid1.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated )
            {
                return null;
            }

            return this.dataGrid1.ItemContainerGenerator.ContainerFromIndex( index ) as DataGridRow;
        }


        private int GetCurrentRowIndex( Position pos )
        {
            var curIndex = -1;

            try
            {
                for( var i = 0; i < this.dataGrid1.Items.Count; i++ )
                {
                    var itm = this.GetRowItem( i );
                    if( this.GetMouseTargetRow( itm , pos ) )
                    {
                        curIndex = i;
                        break;
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }

            return curIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="editor"></param>
        /// <param name="handler"></param>
        public void SetPolicy( IPolicy policy , PolicyEditor editor , PolicyHandler handler )
        {
            try
            {
                this._policy = policy;
                this._policy.Attach( this );
                this._parent = editor;
                this._handler = handler;
                this.PrepareRows();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void PrepareRows()
        {
            try
            {
                this._list.Clear();
                this._ggw = Framework.Database.CreateGrammerGateway();

                var grammers = this._ggw.GetGrammer();
                var dslActionsStructureRegex = new Regex( "#(\\s*)ACTION(\\s*):(.*?):(\\s*)END(\\s*)#" , RegexOptions.Singleline );

                var structureMatch = dslActionsStructureRegex.Matches( this._handler.Document.Text );

                if( structureMatch.Count <= 0 )
                {
                    return;
                }

                var structure = structureMatch[ 0 ].ToString();

                foreach( var grammer in grammers )
                {
                    var decodedrvalue = Framework.Utils.Base64Decode( grammer.GetValue() ).Replace( "\\\\" , "\\" );
                    var regstr = "\\s*" + grammer.GetKey() + "\\s*:" + decodedrvalue + "\\s*";
                    var dslAction = new Regex( regstr , RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline );
                    var rvaluematches = dslAction.Matches( structure );
                    var lastpos = 0;

                    VisualPolicyEntry.AddKey( grammer.GetKey() );

                    foreach( Match matched in rvaluematches )
                    {
                        var sparams = new List< string >();
                        var colcount = 0;

                        foreach( Group group in matched.Groups )
                        {
                            var entry = group.ToString().Trim();

                            if( entry.CompareTo( "" ) == 0 )
                            {
                                continue;
                            }

                            if( group.Index == lastpos )
                            {
                                continue;
                            }

                            if( matched.ToString().Trim().CompareTo( entry ) == 0 )
                            {
                                continue;
                            }

                            sparams.Add( "" );

                            if( group.Captures.Count > 1 )
                            {
                                foreach( var capture in group.Captures )
                                {
                                    sparams[ colcount ] += capture;
                                }
                            }
                            else
                            {
                                sparams[ colcount ] = entry;
                            }

                            lastpos = group.Index;
                            colcount++;

                            if( colcount <= this._colCount )
                            {
                                continue;
                            }

                            this._colCount = colcount;
                        }

                        var vpentry = new VisualPolicyEntry( false , grammer.GetKey() , sparams.ToArray() );
                        vpentry.SetValidationRule( regstr );
                        this._list.Add( vpentry );
                    }
                }

                this.dataGrid1.ItemsSource = this._list;

                foreach( var column in this.dataGrid1.Columns )
                {
                    try
                    {
                        var dataGridComboBox = column as DataGridComboBoxColumn;
                        if( dataGridComboBox != null )
                        {
                            dataGridComboBox.ItemsSource = VisualPolicyEntry.Keys.ToArray();
                        }
                    }
                    catch( Exception )
                    {
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void DataGridLoadingRow( object sender , DataGridRowEventArgs e )
        {
            e.Row.Header = ( e.Row.GetIndex() + 1 ).ToString();
        }


        private bool IsValid()
        {
            var valid = true;

            try
            {
                var i = 0;
                var end = this._list.Count;

                while( i < end )
                {
                    var item = this._list[ i ];
                    if( item.Validate() == false )
                    {
                        valid = false;
                        break;
                    }
                    i++;
                }

                this._parent.DeleteRowButton.IsEnabled = valid;
                this._parent.AddRowButton.IsEnabled = valid;

                if( !valid )
                {
                    if( !this._addingNewEntry )
                    {
                        Framework.Notification.Display( Properties.Resources.FixError , 3000 );
                    }
                }
                else
                {
                    this.UpdateDsl();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }

            return valid;
        }


        /// <summary>
        /// 
        /// </summary>
        public void AddRow()
        {
            try
            {
                if( !this.IsValid() )
                {
                    return;
                }

                RowValidationRule.Override = true;

                this.dataGrid1.CommitEdit( DataGridEditingUnit.Row , true );
                this.dataGrid1.SelectedItem = null;

                var vpentry = new VisualPolicyEntry();
                this._list.Add( vpentry );

                this._addedEntry = vpentry;
                this._addingNewEntry = true;
                this._selection = null;

                this.dataGrid1.Items.Refresh();

                this.dataGrid1.SelectedItem = vpentry;

                this.UpdateDsl();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteSelectedRows()
        {
            try
            {
                if( !this.IsValid() )
                {
                    return;
                }

                this.dataGrid1.CommitEdit( DataGridEditingUnit.Row , true );

                var i = 0;
                var end = this._list.Count;

                while( i < end )
                {
                    var item = this._list[ i ];
                    if( item.IsSelected )
                    {
                        this._list.RemoveAt( i );
                        end = this._list.Count;
                        i--;
                    }
                    i++;
                }

                this.UpdateDsl();

                this.dataGrid1.Items.Refresh();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void DataGrid1SelectionChanged( object sender , SelectionChangedEventArgs e )
        {
            try
            {
                if( this._selection == null )
                {
                    this._selection = this.dataGrid1.SelectedItem;
                }

                if( this._selection != this.dataGrid1.SelectedItem )
                {
                    if( this._addingNewEntry )
                    {
                        var entry = ( VisualPolicyEntry ) this._addedEntry;

                        if( string.IsNullOrEmpty( entry.EntryKey ) )
                        {
                            Framework.Notification.Display( Properties.Resources.RemovingBlank , 5000 );
                            this._list.Remove( entry );
                            this.dataGrid1.Items.Refresh();
                        }
                        else if( entry.Validate() == false )
                        {
                            Framework.Notification.Display( Properties.Resources.InvalidEntry , 5000 );
                            this._list.Remove( entry );
                            this.dataGrid1.Items.Refresh();
                        }

                        this._addingNewEntry = false;

                        RowValidationRule.Override = false;
                    }
                    else
                    {
                        var changed = false;
                        var i = 0;
                        var end = this._list.Count;

                        while( i < end )
                        {
                            var item = this._list[ i ];
                            if( string.IsNullOrEmpty( item.EntryKey ) )
                            {
                                changed = true;
                                this._list.RemoveAt( i );
                                end = this._list.Count;
                                i--;
                            }
                            i++;
                        }

                        if( changed )
                        {
                            this.dataGrid1.Items.Refresh();

                            if( this._list.Contains( ( VisualPolicyEntry ) this._selection ) )
                            {
                                this.dataGrid1.SelectedItem = this._selection;
                            }
                        }

                        if( !this.IsValid() )
                        {
                            return;
                        }
                    }

                    this._selection = this.dataGrid1.SelectedItem;
                }

                this.IsValid();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void UpdateDsl()
        {
            try
            {
                if( this._updatingFromEditor )
                {
                    return;
                }

                var dsl = this._handler.Document.Text;
                var rules = new StringBuilder();

                rules.Append( "\n" );

                foreach( var visualPolicyEntry in this._list )
                {
                    rules.Append( "    " + visualPolicyEntry.EntryKey + " : " );

                    if( !string.IsNullOrEmpty( visualPolicyEntry.Rvalue1 ) )
                    {
                        rules.Append( visualPolicyEntry.Rvalue1 + " " );
                    }

                    if( !string.IsNullOrEmpty( visualPolicyEntry.Rvalue2 ) )
                    {
                        rules.Append( visualPolicyEntry.Rvalue2 + " " );
                    }

                    if( !string.IsNullOrEmpty( visualPolicyEntry.Rvalue3 ) )
                    {
                        rules.Append( visualPolicyEntry.Rvalue3 + " " );
                    }

                    if( !string.IsNullOrEmpty( visualPolicyEntry.Rvalue4 ) )
                    {
                        rules.Append( visualPolicyEntry.Rvalue4 + " " );
                    }

                    if( !string.IsNullOrEmpty( visualPolicyEntry.Rvalue5 ) )
                    {
                        rules.Append( visualPolicyEntry.Rvalue5 + " " );
                    }

                    if( !string.IsNullOrEmpty( visualPolicyEntry.Rvalue6 ) )
                    {
                        rules.Append( visualPolicyEntry.Rvalue6 + " " );
                    }

                    if( !string.IsNullOrEmpty( visualPolicyEntry.Rvalue7 ) )
                    {
                        rules.Append( visualPolicyEntry.Rvalue7 + " " );
                    }

                    if( !string.IsNullOrEmpty( visualPolicyEntry.Rvalue8 ) )
                    {
                        rules.Append( visualPolicyEntry.Rvalue8 + " " );
                    }

                    if( !string.IsNullOrEmpty( visualPolicyEntry.Rvalue9 ) )
                    {
                        rules.Append( visualPolicyEntry.Rvalue9 + " " );
                    }

                    if( !string.IsNullOrEmpty( visualPolicyEntry.Rvalue10 ) )
                    {
                        rules.Append( visualPolicyEntry.Rvalue10 + " " );
                    }

                    if( !string.IsNullOrEmpty( visualPolicyEntry.Rvalue11 ) )
                    {
                        rules.Append( visualPolicyEntry.Rvalue11 + " " );
                    }

                    rules.Append( " \n" );
                }

                rules.Append( "\n" );

                var dslActionsStructureRegex = new Regex( "#(\\s*)ACTION(\\s*):(.*?):(\\s*)END(\\s*)#" , RegexOptions.Singleline );
                var matches = dslActionsStructureRegex.Matches( dsl );
                dsl = dsl.Replace( matches[ 0 ].ToString() , "#ACTION:\n" + rules + ":END#" );

                this._handler.Document.Text = dsl;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void DataGrid1CurrentCellChanged( object sender , EventArgs e )
        {
            try
            {
                if( Keyboard.Modifiers == ModifierKeys.Shift )
                {
                    return;
                }

                if( this.dataGrid1.CurrentColumn is DataGridCheckBoxColumn )
                {
                    this.dataGrid1.BeginEdit();
                }

                if( this.dataGrid1.CurrentColumn is DataGridComboBoxColumn )
                {
                    this.dataGrid1.BeginEdit();
                }

                if( this.dataGrid1.CurrentColumn is DataGridTextColumn )
                {
                    this.dataGrid1.BeginEdit();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void DataGrid1PreparingCellForEdit( object sender , DataGridPreparingCellForEditEventArgs e )
        {
            try
            {
                var checkBox = e.EditingElement as CheckBox;
                if( checkBox != null )
                {
                    checkBox.IsChecked = !checkBox.IsChecked;
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void DataGrid1CellEditEnding( object sender , DataGridCellEditEndingEventArgs e )
        {
            try
            {
                this._updatingFromEditor = false;
                this.UpdateDsl();

                var comboBox = e.EditingElement as ComboBox;
                if( comboBox != null )
                {
                    var row = ( VisualPolicyEntry ) this.dataGrid1.SelectedItem;
                    if( comboBox.SelectedValue != null )
                    {
                        row.EntryKey = comboBox.SelectedValue.ToString();
                    }
                }

                var textBox = e.EditingElement as TextBox;
                var textBoxColumn = e.Column as DataGridTextColumn;
                if( textBox != null )
                {
                    var row = ( VisualPolicyEntry ) this.dataGrid1.SelectedItem;
                    if( textBoxColumn != null )
                    {
                        switch( textBoxColumn.DisplayIndex )
                        {
                            case 2 :
                                row.Rvalue1 = textBox.Text;
                                break;
                            case 3 :
                                row.Rvalue2 = textBox.Text;
                                break;
                            case 4 :
                                row.Rvalue3 = textBox.Text;
                                break;
                            case 5 :
                                row.Rvalue4 = textBox.Text;
                                break;
                            case 6 :
                                row.Rvalue5 = textBox.Text;
                                break;
                            case 7 :
                                row.Rvalue6 = textBox.Text;
                                break;
                            case 8 :
                                row.Rvalue7 = textBox.Text;
                                break;
                            case 9 :
                                row.Rvalue8 = textBox.Text;
                                break;
                            case 10 :
                                row.Rvalue9 = textBox.Text;
                                break;
                            case 11 :
                                row.Rvalue10 = textBox.Text;
                                break;
                            case 12 :
                                row.Rvalue11 = textBox.Text;
                                break;
                        }
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void DataGrid1MouseLeave( object sender , MouseEventArgs e )
        {
            this._updatingFromEditor = true;
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
                if( source == this )
                {
                    return;
                }

                this._policy = policy;
                this.PrepareRows();
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
            if( obj == this )
            {
                return;
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
        }

        /// <summary>
        ///   Dispose this object and all the observers
        /// </summary>
        /// <param name = "obj">IPolicyObserver</param>
        public void Dispose( IPolicyObserver obj )
        {
            if( obj == this )
            {
                return;
            }
        }

        #endregion
    }
}