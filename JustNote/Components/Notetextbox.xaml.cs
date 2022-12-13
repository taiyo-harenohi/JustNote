// Author: Nikola Machálková
//   Note: not used in final version, but previously had the same purpose as events in MainViewModel.cs

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JustNote.App.Components
{
    /// <summary>
    /// Interaction logic for NoteTextbox.xaml
    /// </summary>
    public partial class NoteTextbox : UserControl
    {
        protected Boolean isDragging;
        private Point mousePosition;
        private Double prevX, prevY;

        public NoteTextbox()
        {
            InitializeComponent();

            Loaded += NoteTextbox_Loaded;

            // subscribing to events
            this.MouseLeftButtonDown += new MouseButtonEventHandler(Control_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(Control_MouseLeftButtonUp);
            this.MouseMove += new MouseEventHandler(Control_MouseMove);
        }

        // for sizeable note
        private void NoteTextbox_Loaded(object sender, RoutedEventArgs e)
        {
            AdornerLayer.GetAdornerLayer(NoteGrid).Add(new ResizeNote(NoteGrid));
        }

        // mouse controls for draggable events
        private void Control_MouseLeftButtonDown(Object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            var draggableControl = (sender as UserControl);
            if (draggableControl == null)
            {
                return;
            }
            mousePosition = e.GetPosition(Parent as UIElement);
            draggableControl.CaptureMouse();
        }

        private void Control_MouseLeftButtonUp(Object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            var draggable = (sender as UserControl);
            if (draggable == null)
            {
                return;
            }
            var transform = (draggable.RenderTransform as TranslateTransform);
            if (transform != null)
            {
                prevX = transform.X;
                prevY = transform.Y;
            }
            draggable.ReleaseMouseCapture();
            // so the thumb moves too
            AdornerLayer.GetAdornerLayer(NoteGrid).Add(new ResizeNote(NoteGrid));
        }

        private void Control_MouseMove(Object sender, MouseEventArgs e)
        {
            var draggableControl = (sender as UserControl);
            if (draggableControl == null)
            {
                return;
            }
            if (isDragging && draggableControl != null)
            {
                var currentPosition = e.GetPosition(Parent as UIElement);
                var transform = (draggableControl.RenderTransform as TranslateTransform);
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    draggableControl.RenderTransform = transform;
                }
                transform.X = (currentPosition.X - mousePosition.X);
                transform.Y = (currentPosition.Y - mousePosition.Y);
                if (prevX > 0)
                {
                    transform.X += prevX;
                    transform.Y += prevY;
                }
            }
        }
    }
}
