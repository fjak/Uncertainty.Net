using System;

namespace Fjak.Uncertainty
{
    public sealed class Some<T> : Option<T>
    {
        private readonly T _value;

        public Some(T value)
        {
            if (value != null)
                _value = value;
            else
                throw new ArgumentNullException(nameof(value));
        }

        public override bool IsNone => false;
        public override bool IsSome => true;

        public override Option<T> Where(Predicate<T> p)
        {
            if (p(_value)) return this;
            return Optional.None;
        }

        public override Option<TR> Select<TR>(Func<T, TR> f)
        {
            return Optional.Option(f(_value));
        }

        public override Option<TR> Bind<TR>(Func<T, Option<TR>> f)
        {
            return Optional.Option(f(_value));
        }

        public override T OrDefault(T defaultValue)
        {
            return _value;
        }

        public override string ToString()
        {
            return $"Some({_value})";
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            var cast = obj as Some<T>;
            return cast != null && Equals(cast);
        }

        private bool Equals(Some<T> other)
        {
            return _value.Equals(other._value);
        }
    }
}