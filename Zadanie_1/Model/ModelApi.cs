using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Logic;

namespace ModelPresentation
{
    public class ModelApi : Model
    {
        private readonly LogicAPI _logic;

        public override IList Balls(int ballNumber)
           => _logic.CreateBalls(ballNumber, 500, 300);

        public ModelApi() : this(LogicAPI.CreateBallAPI()) { }
        public ModelApi(LogicAPI logic) {
            _logic = logic; }
    }
}
