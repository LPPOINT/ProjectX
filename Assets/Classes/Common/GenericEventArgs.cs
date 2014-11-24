using System;

namespace Assets.Classes.Common
{
    public class GenericEventArgs<T> : EventArgs
    {
        public GenericEventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }
}
