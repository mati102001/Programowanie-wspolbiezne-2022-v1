using ModelAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ModelView
{
    public class ViewModelWindow : MainViewModel
    {
        private int _ballNumber;
        private readonly Model _model;
        private IList _balls;
        private bool _isStartEnable = false;
        private double _boardWidth;
        private double _boardHeight;

        public ViewModelWindow()
        {
            _model = Model.CreateApi();
            Start = new RelayCommand(StartAction);
            _boardHeight = _model.BoardHeight;
            _boardWidth = _model.BoardWidth;
        }

        private void StartAction(object obj)
        {
            Balls = _model.Balls(_ballNumber);
            _model.Start(Balls);
            BallNumber = 0;
            IsStartEnable = false;
        }

        public bool IsStartEnable
        {
            get => _isStartEnable;
            set
            {
                _isStartEnable = value;
                OnPropertyChanged(nameof(IsStartEnable));
            }
        }

        public int BallNumber
        {
            get => _ballNumber;
            set
            {
                if (value > 0)
                    IsStartEnable = true;
                if (value.Equals(_ballNumber))
                    return;
                if (value < 0)
                    value = 0;
                if (value > 2000)
                    value = 2000;
                _ballNumber = value;
                OnPropertyChanged(nameof(BallNumber));
            }
        }

        public ICommand Start { get; set; }



        public double BoardHeight
        {
            get => _boardHeight;
            set
            {
                if (value.Equals(_boardHeight)) return;
                _boardHeight = value;
                OnPropertyChanged();

            }
        }
        public double BoardWidth
        {
            get => _boardWidth;
            set
            {
                if (value.Equals(_boardWidth)) return;
                _boardWidth = value;
                OnPropertyChanged();

            }
        }
        public IList Balls
        {
            get => _balls;
            set
            {
                if (value.Equals(_balls))
                    return;
                _balls = value;
                OnPropertyChanged(nameof(Balls));
            }
        }

    }
}
