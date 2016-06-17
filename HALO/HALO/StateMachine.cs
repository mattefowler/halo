using HALO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HALO
{
    public class StateMachine
    {
        public class State : ICondition, Named
        {
            internal State(string name, ICondition condition)
            {
                Name = name;
                this.condition = condition;
                condition.OnUpdate += (newValue) => {
                    if (newValue) this.OnEnter(this);
                    else this.OnExit(this);
                };
            }

            public string Name{ get; private set; }

            public bool Value
            {
                get{ return condition.Value; }
            }

            Action<bool> Subscribable<bool>.OnUpdate { get; set; }
            public Action<State> OnEnter { get; set; } = delegate { };
            public Action<State> OnExit { get; set; } = delegate { };
            private ICondition condition;
        }

        public IEnumerable<State> States
        {
            get
            {
                return states.Values.AsEnumerable();
            }
        }

        public State DefineState(string name, ICondition stateCondition)
        {
            var state = new State(name, stateCondition);
            states.Add(name, state);
            return state;
        }

        Dictionary<String, State> states = new Dictionary<string, State>();
    }
}
