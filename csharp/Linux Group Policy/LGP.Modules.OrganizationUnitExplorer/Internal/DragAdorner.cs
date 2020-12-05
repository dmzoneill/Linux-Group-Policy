#region

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using LGP.Modules.OrganizationUnitExplorer.Internal.CustomControls;

#endregion

namespace LGP.Modules.OrganizationUnitExplorer.Internal
{
    internal class DragAdorner : Adorner
    {
        protected UIElement Child;
        protected UIElement Owner;
        public double Scale = 1.0;
        protected double XCenter;
        protected double YCenter;
        private double _leftOffset;
        private double _topOffset;

        public DragAdorner( UIElement owner ) : base( owner )
        {
        }

        public DragAdorner( UIElement owner , UIElement adornElement , bool useVisualBrush , double opacity ) : base( owner )
        {
            Debug.Assert( owner != null );
            Debug.Assert( adornElement != null );
            this.Owner = owner;

            if( useVisualBrush )
            {
                VisualBrush brush;
                try
                {
                    var ele = ( TreeViewOuElement ) adornElement;
                    ele.IsSelected = false;
                    brush = new VisualBrush( ele )
                    {
                        Opacity = opacity
                    };
                    ele.IsSelected = true;
                }
                catch( Exception )
                {
                    brush = new VisualBrush( adornElement )
                    {
                        Opacity = opacity
                    };
                }

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

        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        private void UpdatePosition()
        {
            var adorner = ( AdornerLayer ) this.Parent;
            if( adorner != null )
            {
                adorner.Update( this.AdornedElement );
            }
        }

        protected override Visual GetVisualChild( int index )
        {
            return this.Child;
        }


        protected override Size MeasureOverride( Size finalSize )
        {
            this.Child.Measure( finalSize );
            return this.Child.DesiredSize;
        }

        protected override Size ArrangeOverride( Size finalSize )
        {
            this.Child.Arrange( new Rect( this.Child.DesiredSize ) );
            return finalSize;
        }

        public override GeneralTransform GetDesiredTransform( GeneralTransform transform )
        {
            var result = new GeneralTransformGroup();

            result.Children.Add( base.GetDesiredTransform( transform ) );
            result.Children.Add( new TranslateTransform( this._leftOffset , this._topOffset ) );
            return result;
        }
    }
}