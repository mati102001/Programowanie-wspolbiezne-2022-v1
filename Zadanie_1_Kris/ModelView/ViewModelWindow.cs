using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ModelView
{
    public class ViewModelWindow : MainViewModelBase
    {
        private ModelApi Model { get; set; }

        public int _ballNumber { get => Model.StartBalls; set => Model.StartBalls = value; }

        public Canvas Canvas { get => Model.Canvas; set => Model.Canvas = value; }

        public ViewModelWindow()
        {
            Model = new ModelApi(500, 350);
            CreateBalls = new RelayCommand(Model.CreateBalls);
            Start = new RelayCommand(Model.Start);
            Stop = new RelayCommand(Model.Stop);
        }

        public RelayCommand CreateBalls { protected get; set; }

        public RelayCommand Start { protected get; set; }

        public RelayCommand Stop { protected get; set; }

        public int BallNumber
        {
            get { return _ballNumber; }
            set
            {
                if (value < 0)
                    value = 0;
                if (value > 200)
                    value = 200;
                _ballNumber = value;
                OnPropertyChanged();
            }

        }
       
    }
}
