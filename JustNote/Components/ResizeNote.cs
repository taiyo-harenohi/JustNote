// Author: Nikola Machálková

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace JustNote.App.Components
{
    public class ResizeNote : Adorner
    {
        VisualCollection AdornerVisual;
        Thumb thumb;

        public ResizeNote(UIElement adornedElement) : base(adornedElement)
        {
            AdornerVisual = new VisualCollection(this);

            thumb = new Thumb() { Background = Brushes.Brown, Height = 10, Width = 10};

            thumb.DragDelta += thumb_DragDelta;

            AdornerVisual.Add(thumb);
        }


        private void thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var ele = (FrameworkElement)AdornedElement;

            ele.Height = ele.ActualHeight + e.VerticalChange < 0 ? 0 : ele.ActualHeight + e.VerticalChange;
            ele.Width = ele.ActualWidth + e.HorizontalChange < 0 ? 0 : ele.ActualWidth + e.HorizontalChange;

            Debug.WriteLine(ele.Height);
        }



        protected override Visual GetVisualChild(int index)
        {
            return AdornerVisual[index];
        }

        protected override int VisualChildrenCount => AdornerVisual.Count;

        protected override Size ArrangeOverride(Size finalSize)
        {
            thumb.Arrange(new Rect(AdornedElement.DesiredSize.Width,AdornedElement.DesiredSize.Height,10,10));



            return base.ArrangeOverride(finalSize);
        }
    }
}
