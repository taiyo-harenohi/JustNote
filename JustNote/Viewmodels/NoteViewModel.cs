
// Author: Lukáš Leták

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

namespace JustNote.App.Viewmodels
{
    public class NoteViewModel : ViewModelBase
    {

        private int _key;
        private int[] _position;
        private int[] _positionDX;
        private string _text;

        private bool _selected = false;
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

            var k = (System.Windows.UIElement)Grid;
            AdornerLayer.GetAdornerLayer(Grid).Add(new ResizeNote((System.Windows.UIElement)Grid));
        }

        private void MouseMove(System.Windows.IInputElement DateCanvas)
        {
            if (!_selected)
                return;
            if (DateCanvas == null)
                return;
            var mouseX = Mouse.GetPosition(DateCanvas).X + _positionDX[0];
            var mouseY = Mouse.GetPosition(DateCanvas).Y + _positionDX[1];
            int[] mouseCoord = { (int)mouseX, (int)mouseY };

            Position = mouseCoord;
            
        }
        private void MouseLBDown(System.Windows.IInputElement DateCanvas)
        {
            _selected = true;
            var mouseX = Mouse.GetPosition(DateCanvas).X;
            var mouseY = Mouse.GetPosition(DateCanvas).Y;
            int[] mouseCoord = { _position[0] - (int)mouseX, _position[1] - (int)mouseY };
            _positionDX =  mouseCoord;
        }

        private void MouseLBUp()
        {
            _selected = false;
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
