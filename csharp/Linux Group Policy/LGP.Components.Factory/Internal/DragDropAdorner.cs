#region

using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using LGP.Components.Factory.Interfaces.Infrastructure;

#endregion

namespace LGP.Components.Factory.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public class DragDropAdorner : IDragDrop
    {
        private static DragDropAdorner _instance;

        private DragDropAdorner()
        {
        }

        #region IDragDrop Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dragElement"></param>
        /// <param name="e"></param>
        public void StartDrag( FrameworkElement dragElement , MouseEventArgs e )
        {
            try
            {
                if( e.LeftButton == MouseButtonState.Pressed && !DragAdorner.IsDragging )
                {
                    var position = e.GetPosition( null );

                    if( Math.Abs( position.X - DragAdorner.StartPoint.X ) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs( position.Y - DragAdorner.StartPoint.Y ) > SystemParameters.MinimumVerticalDragDistance )
                    {
                        DragAdorner.DragScope = Application.Current.MainWindow.Content as FrameworkElement;

                        if( DragAdorner.DragScope != null )
                        {
                            DragAdorner.DragScope.AllowDrop = true;

                            DragAdorner.DragScope.PreviewDragOver += this.OnDragOver;
                            DragAdorner.DragScope.DragLeave += this.OnDragScopeLeave;
                            DragAdorner.DragScope.QueryContinueDrag += this.OnDragScopeQuery;

                            DragAdorner.IsDragging = true;

                            DragAdorner.Adorner = new DragAdorner( DragAdorner.DragScope , dragElement , true , 0.5 );
                            DragAdorner.Layer = AdornerLayer.GetAdornerLayer( DragAdorner.DragScope );
                            DragAdorner.Layer.Add( DragAdorner.Adorner );

                            DragAdorner.DragHasLeftScope = false;
                            var data = new DataObject();
                            data.SetData( dragElement );
                            DragDrop.DoDragDrop( DragAdorner.DragScope , data , DragDropEffects.Move );
                            AdornerLayer.GetAdornerLayer( DragAdorner.DragScope ).Remove( DragAdorner.Adorner );
                            DragAdorner.Adorner = null;

                            DragAdorner.IsDragging = false;
                            DragAdorner.DragScope.DragLeave -= this.OnDragScopeLeave;
                            DragAdorner.DragScope.QueryContinueDrag -= this.OnDragScopeQuery;
                            DragAdorner.DragScope.PreviewDragOver -= this.OnDragOver;
                        }
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DragDropAdorner GetInstance()
        {
            return _instance ?? ( _instance = new DragDropAdorner() );
        }

        private void OnDragScopeQuery( object sender , QueryContinueDragEventArgs e )
        {
            if( !DragAdorner.DragHasLeftScope )
            {
                return;
            }

            e.Action = DragAction.Cancel;
            e.Handled = true;
        }


        private void OnDragScopeLeave( object sender , DragEventArgs e )
        {
            try
            {
                if( e.OriginalSource != DragAdorner.DragScope )
                {
                    return;
                }

                var p = e.GetPosition( DragAdorner.DragScope );
                var r = VisualTreeHelper.GetContentBounds( DragAdorner.DragScope );

                if( r.Contains( p ) )
                {
                    return;
                }

                DragAdorner.DragHasLeftScope = true;
                e.Handled = true;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void OnDragOver( object sender , DragEventArgs args )
        {
            try
            {
                if( DragAdorner.Adorner == null )
                {
                    return;
                }

                DragAdorner.Adorner.LeftOffset = args.GetPosition( DragAdorner.DragScope ).X;
                DragAdorner.Adorner.TopOffset = args.GetPosition( DragAdorner.DragScope ).Y;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}