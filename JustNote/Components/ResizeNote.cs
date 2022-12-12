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
        Thumb thumb1, thumb2;

        public ResizeNote(UIElement adornedElement) : base(adornedElement)
        {
            AdornerVisual = new VisualCollection(this);

            thumb1 = new Thumb() { Background = Brushes.Brown, Height = 10, Width = 10};
            thumb2 = new Thumb() { Background = Brushes.Brown, Height = 10, Width = 10};

            thumb1.DragDelta += thumb1_DragDelta;
            thumb2.DragDelta += thumb2_DragDelta;

            AdornerVisual.Add(thumb1);
            AdornerVisual.Add(thumb2);
        }

        private void thumb1_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var ele = (FrameworkElement)AdornedElement;

           // ele.Height = ele.Height - e.VerticalChange < 0 ? 0: ele.Height - e.VerticalChange;
           // ele.Width = ele.Width - e.HorizontalChange < 0 ? 0: ele.Width - e.HorizontalChange;
        }

        private void thumb2_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var ele = (FrameworkElement)AdornedElement;

            ele.Height = ele.Height + e.VerticalChange < 0 ? 0 : ele.Height + e.VerticalChange;
            ele.Width = ele.Width + e.HorizontalChange < 0 ? 0 : ele.Width + e.HorizontalChange;

            Debug.WriteLine(ele.Height);
        }



        protected override Visual GetVisualChild(int index)
        {
            return AdornerVisual[index];
        }

        protected override int VisualChildrenCount => AdornerVisual.Count;

        protected override Size ArrangeOverride(Size finalSize)
        {
            thumb1.Arrange(new Rect(0,0,10,10));
            thumb2.Arrange(new Rect(AdornedElement.DesiredSize.Width,AdornedElement.DesiredSize.Height,10,10));



            return base.ArrangeOverride(finalSize);
        }
    }
}
