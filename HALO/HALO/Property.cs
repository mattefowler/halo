using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HALO
{
    public class Property<T>: 
        Named,
        Valued<T>,
        Subscribable<T>
        where T:IComparable<T>
    {
        public Property(T value = default(T))
        {
            this.value = value;
            OnUpdate = delegate { };
        }

        public Property(string name = null, T value = default(T)) :
            this(value)
        {
            Name = name;
        }

        public string Name { get; protected set; }

        public Action<T> OnUpdate{ get; set; }

        public T Value
        {
            get{ return value; }
            set
            {
                this.value = value;
                OnUpdate(value);
            }
        }

        public static Condition<T> operator <(Property<T> lhs, T rhs)
        {
            return new Condition<T>
            (
                lhs.value, 
                (T value) => value.CompareTo(rhs) < 0, 
                lhs
            );
        }

        public static Condition<T> operator >(Property<T> lhs, T rhs)
        {
            return new Condition<T>
            (
                lhs.value, 
                (T value) => value.CompareTo(rhs) > 0, 
                lhs
            );
        }

        public static Condition<T> operator <=(Property<T> lhs, T rhs)
        {
            return new Condition<T>
            (
                lhs.value, 
                (T value) => value.CompareTo(rhs) <= 0, 
                lhs
            );
        }

        public static Condition<T> operator >=(Property<T> lhs, T rhs)
        {
            return new Condition<T>
            (
                lhs.value, 
                (T value) => value.CompareTo(rhs) >= 0, 
                lhs
            );
        }

        public static Condition<T> operator ==(Property<T> lhs, T rhs)
        {
            return new Condition<T>
            (
                lhs.value, 
                (T value) => value.CompareTo(rhs) == 0,
                lhs
            );
        }

        public static Condition<T> operator !=(Property<T> lhs, T rhs)
        {
            return new Condition<T>
            (
                lhs.value, 
                (T value) => value.CompareTo(rhs) != 0, 
                lhs
            );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        private T value;
    }
}

