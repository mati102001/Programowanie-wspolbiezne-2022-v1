using System;
using System.Collections;
using Data;
using System.Threading;


namespace Logic
{
    public abstract class LogicAPI
    {
        public static LogicAPI CreateBallAPI() => new BallFactory();
        public abstract IList CreateBalls(int number);
        public abstract void Start();
    }

    internal class BallFactory : LogicAPI
    {
        private readonly DataAbstractApi _data;
        private readonly BallService service;

        public BallFactory() : this(DataAbstractApi.CreateDataLayer()) { }
        public BallFactory(DataAbstractApi data) { _data = data; service = new BallService(_data); }

        private IList balls => _data.GetAll();

        private CancellationTokenSource cancellationTokenSource;

        private CancellationToken cancellationToken;

        public override IList CreateBalls(int number)
        {
            _data.createBoard(number);
            return _data.GetAll();
        }

        public override void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;
            for (int i = 0; i < balls.Count; i++)
                _data.Test(i);
        }

    }
}
