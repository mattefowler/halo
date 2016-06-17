using NUnit.Framework;
using HALO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HALO.StateMachine;

namespace Test
{
    [TestFixture]
    class StateMachineTest
    {
        [Test]
        public void statemachine()
        {
            StateMachine stateMachine = new StateMachine();
            Property<double> speed = new Property<double>(0);
            Property<bool> doorInterlock = new Property<bool>(false);

            State emergencyStop = stateMachine.DefineState(
                "emergency stop", 
                doorInterlock == true
            );
            State moving = stateMachine.DefineState(
                "moving",
                speed != 0
            );
            emergencyStop.OnEnter += (state) => { if (moving.Value) speed.Value = 0; };
            Assert.False(emergencyStop.Value);
            Assert.False(moving.Value);
            speed.Value = 1;
            Assert.False(emergencyStop.Value);
            Assert.True(moving.Value);
            doorInterlock.Value = true;
            Assert.True(emergencyStop.Value);
            Assert.False(moving.Value);
            doorInterlock.Value = false;
            Assert.False(emergencyStop.Value);
            Assert.False(moving.Value);

        }
    }
}
