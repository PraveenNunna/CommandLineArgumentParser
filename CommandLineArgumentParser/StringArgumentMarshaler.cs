using System;
using System.Collections.Generic;

namespace CommandLineArgumentParser
{
    internal class StringArgumentMarshaler : IArgumentMarshaler
    {
        private String stringValue = "";

        public void set(IEnumerator<string> enumerator)
        {
            try
            {
                enumerator.MoveNext();
                stringValue = enumerator.Current;
            }
            catch (InvalidOperationException ex)
            {
                throw new ArgsException(ErrorCode.MISSING_STRING);
            }
        }

        internal static string getValue(IArgumentMarshaler marshaler)
        {
            if (marshaler != null && marshaler is StringArgumentMarshaler)
                return ((StringArgumentMarshaler)marshaler).stringValue;
            else
                return "";
        }
    }
}