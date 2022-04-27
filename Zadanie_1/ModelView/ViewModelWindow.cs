using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ModelView
{
    public class ViewModelWindow : MainViewModel
    {
        private int _ballNumber;
        private IList _balls;


        public int BallNumber
        {
            get => _ballNumber;
            set
            {
                if (value.Equals(_ballNumber))
                    return;
                if (value < 0)
                    value = 0;
                if (value > 2000)
                    value = 2000;
                _ballNumber = value;
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
                OnPropertyChanged();
            }
        }

    }
}
