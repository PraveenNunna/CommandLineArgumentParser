using System;
using System.Collections.Generic;

namespace CommandLineArgumentParser
{
    internal class BooleanArgumentMarshaler : IArgumentMarshaler
    {
        private bool value = false;
        public void set(IEnumerator<string> enumerator)
        {
            enumerator.MoveNext();
            var str = enumerator.Current;

            Boolean.TryParse(str, out value);
        }

        public static bool getValue(IArgumentMarshaler marshaler)
        {
            if (marshaler != null && marshaler is BooleanArgumentMarshaler)
                return ((BooleanArgumentMarshaler)marshaler).value;
            else
                return false;
        }
    }
}