
// Authors:
//      Nikola Machálková - LoadGrid function and command
//            Lukáš Leták – everything else


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using JustNote.App.Services;
using JustNote.Backend.Data;
using Microsoft.Toolkit.Mvvm.Input;
using System.Diagnostics;
using JustNote.App.Components;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Media;

namespace JustNote.App.Viewmodels
{
    public class NoteViewModel : ViewModelBase
    {

        private int _key;
        private int[] _position;
        private int[] _positionDX;
        private string _text;

        private bool _selected = false;
        private Double prevX, prevY;

        private System.Windows.IInputElement grid;

        public NoteViewModel()
        {
            
        }

        public NoteViewModel(Note note)
        {
            Key = note.Key;
            Text = note.Text;
            Position = note.Position;
            MouseLBDownCommand = new RelayCommand<System.Windows.IInputElement>(Canvas => MouseLBDown(Canvas));
            MouseLBUpCommand = new RelayCommand(MouseLBUp);
            MouseMoveCommand = new RelayCommand<System.Windows.IInputElement>(Canvas => MouseMove(Canvas));
            LoadGridCommand = new RelayCommand<System.Windows.Media.Visual>(Grid => LoadGrid(Grid));
        }

        public ICommand MouseLBDownCommand { get; }
        public ICommand MouseLBUpCommand { get; }
        public ICommand MouseMoveCommand { get; }
        public ICommand LoadGridCommand { get; }

        private void LoadGrid(System.Windows.Media.Visual Grid)
        {
            if (Grid == null)
                return;

            grid = (System.Windows.IInputElement)Grid;
            AdornerLayer.GetAdornerLayer(Grid).Add(new ResizeNote((System.Windows.UIElement)Grid));
        }

        private void MouseMove(System.Windows.IInputElement DateCanvas)
        {
            if (!_selected)
                return;
            if (DateCanvas == null)
                return;

            var drag = (grid as UserControl);
            if (drag == null)
            {
                return;
            }


            if (drag != null && _selected)
            {
                var mouseX = Mouse.GetPosition(DateCanvas).X + _positionDX[0];
                var mouseY = Mouse.GetPosition(DateCanvas).Y + _positionDX[1];

                if (mouseX < 0)
                {
                    mouseX = 0;
                }
                if (mouseY < 0)
                {
                    mouseY = 0;
                }

                int[] mouseCoord = { (int)mouseX, (int)mouseY };

                Position = mouseCoord;
            }
        }

        private void MouseLBDown(System.Windows.IInputElement DateCanvas)
        {
            _selected = true;

            var drag = (grid as UserControl);
            if (drag == null)
            {
                return;
            }

            var mouseX = Mouse.GetPosition(DateCanvas).X;
            var mouseY = Mouse.GetPosition(DateCanvas).Y;

            var transform = (drag.RenderTransform as TranslateTransform);


            transform.X = (_position[0] - (int)mouseX);
            transform.Y = (_position[1] - (int)mouseY);
            
            if (prevX > 0)
            {
                transform.X += prevX;
                transform.Y += prevY;
            }


            int[] mouseCoord = { (int)transform.X, (int)transform.Y};
            _positionDX =  mouseCoord;
        }

        private void MouseLBUp()
        {
            _selected = false;

            var drag = (grid as UserControl);
            if (drag == null)
            {
                return;
            }

            var transform = (drag.RenderTransform as TranslateTransform);
            if (transform != null)
            {
                prevX = transform.X;
                prevY = transform.Y;
            }

            drag.ReleaseMouseCapture();
        }

        public int Key
        {
            get => _key;
            set
            {
                _key = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public int[] Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }
    }
}
