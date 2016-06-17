using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HALO
{
    public interface ICondition : 
        Subscribable<bool>, 
        Valued<bool>
    {
    }

    public class Condition<T>:
        ICondition where T: IComparable<T>
    {
        public Condition(T currentValue, Func<T, bool> criterion, params Subscribable<T>[] subscriptions)
        {
            this.criterion = criterion;
            subscriptions.ToList().ForEach(s => s.OnUpdate += evaluate);
            Value = criterion(currentValue);
        }

        private void evaluate(T newValue)
        {
            if(criterion(newValue) != Value)
            {
                Value = !Value;
                OnUpdate(Value);
            }
        }

        public static Condition<bool> operator &(Condition<T> lhs, Condition<T> rhs)
        {
            return new Condition<bool>
            (
                lhs.Value & rhs.Value, 
                (_) => lhs.Value && rhs.Value, 
                lhs, rhs
            );
        }

        public static Condition<bool> operator |(Condition<T> lhs, Condition<T> rhs)
        {
            return new Condition<bool>
            (
                lhs.Value | rhs.Value, 
                (_) => lhs.Value || rhs.Value, 
                lhs, rhs
            );
        }

        public static Condition<bool> operator ^ (Condition<T> lhs, Condition<T> rhs)
        {
            return new Condition<bool>
            (
                lhs.Value ^ rhs.Value,
                (_) => lhs.Value ^ rhs.Value,
                lhs, rhs
            );
        }

        public static Condition<bool> operator !(Condition<T> condition)
        {
            return new Condition<bool>
            (
                !condition.Value,
                (value) => !value, 
                condition
            );
        }

        public Action<bool> OnUpdate { get; set; } = delegate { };
        private readonly Func<T, bool> criterion;
        public bool Value { get; protected set; }
    }
}
