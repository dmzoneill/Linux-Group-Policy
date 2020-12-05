#region

using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

#endregion

namespace LGP.Components.Factory.Internal
{
    /// <summary>
    /// 
    /// </summary>
    internal class DragAdorner : Adorner
    {
        private static DragAdorner _adorner;
        private static bool _dragHasLeftScope;
        private static AdornerLayer _layer;
        private static Point _startPoint;
        private static bool _isDragging;

        /// <summary>
        /// 
        /// </summary>
        protected UIElement Child;

        /// <summary>
        /// 
        /// </summary>
        protected UIElement Owner;

        /// <summary>
        /// 
        /// </summary>
        protected double XCenter;

        /// <summary>
        /// 
        /// </summary>
        protected double YCenter;

        private double _leftOffset;
        private double _scale = 1.0;
        private double _topOffset;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        public DragAdorner( UIElement owner ) : base( owner )
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="adornElement"></param>
        /// <param name="useVisualBrush"></param>
        /// <param name="opacity"></param>
        public DragAdorner( UIElement owner , UIElement adornElement , bool useVisualBrush , double opacity ) : base( owner )
        {
            Debug.Assert( owner != null );
            Debug.Assert( adornElement != null );
            this.Owner = owner;

            if( useVisualBrush )
            {
                var brush = new VisualBrush( adornElement )
                {
                    Opacity = opacity
                };

                var r = new Rectangle
                {
                    RadiusX = 3 , RadiusY = 3 , Width = adornElement.DesiredSize.Width , Height = adornElement.DesiredSize.Height
                };

                this.XCenter = adornElement.DesiredSize.Width / 2;
                this.YCenter = adornElement.DesiredSize.Height / 2;

                r.Fill = brush;
                this.Child = r;
            }
            else
            {
                this.Child = adornElement;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double LeftOffset
        {
            get
            {
                return this._leftOffset;
            }
            set
            {
                this._leftOffset = value - this.XCenter;
                this.UpdatePosition();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double TopOffset
        {
            get
            {
                return this._topOffset;
            }
            set
            {
                this._topOffset = value - this.YCenter;

                this.UpdatePosition();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static FrameworkElement DragScope
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public static DragAdorner Adorner
        {
            get
            {
                return _adorner;
            }
            set
            {
                _adorner = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool DragHasLeftScope
        {
            get
            {
                return _dragHasLeftScope;
            }
            set
            {
                _dragHasLeftScope = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Point StartPoint
        {
            get
            {
                return _startPoint;
            }
            set
            {
                _startPoint = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static AdornerLayer Layer
        {
            get
            {
                return _layer;
            }
            set
            {
                _layer = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool IsDragging
        {
            get
            {
                return _isDragging;
            }
            set
            {
                _isDragging = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Scale
        {
            get
            {
                return this._scale;
            }
            set
            {
                this._scale = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double XAxisCenter
        {
            get
            {
                return this.XCenter;
            }
            set
            {
                this.XCenter = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double YAxisCenter
        {
            get
            {
                return this.YCenter;
            }
            set
            {
                this.YCenter = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public override GeneralTransform GetDesiredTransform( GeneralTransform transform )
        {
            var result = new GeneralTransformGroup();

            // ReSharper disable AssignNullToNotNullAttribute
            result.Children.Add( base.GetDesiredTransform( transform ) );
            // ReSharper restore AssignNullToNotNullAttribute
            result.Children.Add( new TranslateTransform( this._leftOffset , this._topOffset ) );
            return result;
        }

        private void UpdatePosition()
        {
            var adorner = ( AdornerLayer ) this.Parent;
            if( adorner != null )
            {
                adorner.Update( this.AdornedElement );
            }
        }


        /// <summary>
        /// Overrides <see cref="M:System.Windows.Media.Visual.GetVisualChild(System.Int32)"/>, and returns a child at the specified index from a collection of child elements. 
        /// </summary>
        /// <returns>
        /// The requested child element. This should not return null; if the provided index is out of range, an exception is thrown.
        /// </returns>
        /// <param name="index">The zero-based index of the requested child element in the collection.</param>
        protected override Visual GetVisualChild( int index )
        {
            return this.Child;
        }


        /// <summary>
        /// Implements any custom measuring behavior for the adorner.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Size"/> object representing the amount of layout space needed by the adorner.
        /// </returns>
        /// <param name="finalSize">A size to constrain the adorner to.</param>
        protected override Size MeasureOverride( Size finalSize )
        {
            this.Child.Measure( finalSize );
            return this.Child.DesiredSize;
        }


        /// <summary>
        /// When overridden in a derived class, positions child elements and determines a size for a <see cref="T:System.Windows.FrameworkElement"/> derived class. 
        /// </summary>
        /// <returns>
        /// The actual size used.
        /// </returns>
        /// <param name="finalSize">The final area within the parent that this element should use to arrange itself and its children.</param>
        protected override Size ArrangeOverride( Size finalSize )
        {
            this.Child.Arrange( new Rect( this.Child.DesiredSize ) );
            return finalSize;
        }
    }
}