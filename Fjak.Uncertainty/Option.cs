using System;

namespace Fjak.Uncertainty
{
    public abstract class Option<T>
    {
        public abstract bool IsNone { get; }
        public abstract bool IsSome { get; }

        public abstract Option<T> Where(Predicate<T> p);

        public static implicit operator Option<T>(Optional _)
        {
            return new None<T>();
        }

        public static implicit operator Option<T>(T v)
        {
            return v != null ? new Some<T>(v) : (Option<T>) Optional.None;
        }

        public abstract T OrDefault(T defaultValue);

        public abstract Option<TR> Select<TR>(Func<T, TR> f);
        public abstract Option<TR> Bind<TR>(Func<T, Option<TR>> f);
    }
}