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
           => _logic.CreateBalls(ballNumber, 640, 320);
         public override void Start(IList balls) => _logic.Start();
        public ModelApi() : this(LogicAPI.CreateBallAPI()) { }
        public ModelApi(LogicAPI logic) {
            _logic = logic; }
    }
}
