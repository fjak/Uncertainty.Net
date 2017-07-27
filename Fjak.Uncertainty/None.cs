using System;

namespace Fjak.Uncertainty
{
    internal sealed class None<T> : Option<T>
    {
        public override bool IsNone => true;
        public override bool IsSome => false;

        public override Option<T> Where(Predicate<T> p)
        {
            return this;
        }

        public override Option<TR> Select<TR>(Func<T, TR> f)
        {
            return Optional.None;
        }

        public override Option<TR> Bind<TR>(Func<T, Option<TR>> f)
        {
            return Optional.None;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            var cast = obj as None<T>;
            return cast != null;
        }

        public override int GetHashCode()
        {
            return Optional.None.GetHashCode();
        }

        public override T OrDefault(T defaultValue)
        {
            return defaultValue;
        }

        public override string ToString()
        {
            return "None";
        }
    }
}