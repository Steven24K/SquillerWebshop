using System;

namespace SquillerWebshop.DesignPatterns
{
    public interface IOption<T>
    {
        void Visit(Action<T> onSome, Action onNone);
        U Visit<U>(Func<T,U> onSome, Func<U> onNone);
    }

    public class Some<T> : IOption<T>
    {
        private T Value;
        public Some(T value){this.Value = value;}

        public void Visit(Action<T> onSome, Action onNone)
        {
            onSome(this.Value);
        }

        public U Visit<U>(Func<T, U> onSome, Func<U> onNone)
        {
            return onSome(this.Value);
        }
    }

    public class None<T> : IOption<T>
    {
        public void Visit(Action<T> onSome, Action onNone)
        {
            onNone();
        }

        public U Visit<U>(Func<T, U> onSome, Func<U> onNone)
        {
            return onNone();
        }
    }
}