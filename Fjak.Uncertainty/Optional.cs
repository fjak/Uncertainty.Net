namespace Fjak.Uncertainty
{
    public sealed class Optional
    {
        public static readonly Optional None = new Optional();

        private Optional()
        {
        }

        public override string ToString()
        {
            return "None";
        }

        public static Some<T> Some<T>(T v)
        {
            return new Some<T>(v);
        }

        public static Option<T> Option<T>(T? v) where T : struct
        {
            return !v.HasValue ? None : Option(v.Value);
        }

        public static Option<T> Option<T>(Option<T> o)
        {
            return o == null ? None : Option(o.OrDefault(default(T)));
        }

        public static Option<T> Option<T>(T o)
        {
            if (o == null) return None;
            return new Some<T>(o);
        }
    }
}