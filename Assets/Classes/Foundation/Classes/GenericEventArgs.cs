using System;

namespace Assets.Classes.Foundation.Classes
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
