using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Logic;

namespace ModelAPI
{
    internal class ModelApi : Model
    {
        private readonly LogicAPI _logic;

        public override double BoardWidth => _logic.BoardWidth;

        public override double BoardHeight => _logic.BoardHeight;

        public override IList Balls(int ballNumber)
           => _logic.CreateBalls(ballNumber);
        public override void Start(IList balls) => _logic.Start();
        public ModelApi() : this(LogicAPI.CreateBallAPI()) { }
        public ModelApi(LogicAPI logic) {
            _logic = logic; }
    }
}
