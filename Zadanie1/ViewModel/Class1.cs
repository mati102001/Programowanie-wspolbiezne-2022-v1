using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;

using Model;

using System;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class ViewModelMainWindow : BindableBase
    {
        private string _ballsNumber;
        private bool _beginSimulationClicked;
        private ModelAbstractApi _modelAbstractApi;
        public ICommand SimulationButtonClicked { get; set; }

        public ObservableCollection<Ball> Balls // TODO to chyba nie jest dobrze?
        {
            //get => _modelAbstractApi.GetBalls().ConvertAll(ball => new Ball { BallX = ball.x, BallY = ball.y, BallSize = ball.size });
            get => _modelAbstractApi.GetBalls();
        }

        public string BallsNumber
        {
            get { return _ballsNumber; }
            set { _ballsNumber = value; }
        }

        public bool BeginSimulationClicked
        {
            get { return _beginSimulationClicked; }
            set { _beginSimulationClicked = value; }
        }

        public ViewModelMainWindow()
    {
            SimulationButtonClicked = new CommandHandler(StartSimulation, CanStartSimulation);
            BallsNumber = "0";

            _modelAbstractApi = ModelAbstractApi.CreateApi(200, 200);

            //_balls = _modelAbstractApi.GetBalls();
            //foreach (var element in _modelAbstractApi.GetBalls())
            //{
            //    _balls.Add(new Ball { BallX = element.x, BallY = element.y, BallSize = element.size });
            //}
    }

        private void StartSimulation(object value)
        {
            BeginSimulationClicked = true;
            _modelAbstractApi.CreateBalls(Convert.ToInt32(BallsNumber));
            //_balls.Add(_modelAbstractApi.GetBalls());
            //foreach (var element in _modelAbstractApi.GetBalls())
            //{
            //    _balls.Add(new Ball { BallX = element.x, BallY = element.y, BallSize = element.size });
            //}
            int i = 2;
        }

        private bool CanStartSimulation(object value)
        {
            return !BeginSimulationClicked;
        }
    }


}
