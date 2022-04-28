using ModelPresentation;
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
     


            public ViewModelWindow()
            {
            _model = Model.CreateApi();
            Start = new RelayCommand(StartAction);

             }

        private void StartAction(object obj)
        {
            Balls = _model.Balls(_ballNumber);
            _model.Start(Balls);
        }

        public int BallNumber
            {
                get => _ballNumber;
                set
                {
                    if (value.Equals(_ballNumber))
                        return;
                    if (value < 0)
                        value = 0;
                    if (value > 2137)
                        value = 2137;
                    _ballNumber = value;
                    OnPropertyChanged(nameof(BallNumber));
                }
            }

        public ICommand Start { get; set; }
       

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
