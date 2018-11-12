using System;
using System.Collections.Generic;

namespace CommandLineArgumentParser
{
    internal class IntegerArgumentMarshaler : IArgumentMarshaler
    {
        private int value = 0;
        public void set(IEnumerator<string> enumerator)
        {
            try
            {
                enumerator.MoveNext();
                value = Int32.Parse(enumerator.Current);
            }
            catch (FormatException ex)
            {
                throw new ArgsException(ErrorCode.INVALID_ARGUMENT_FORMAT);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgsException(ErrorCode.MISSING_INTEGER);
            }
        }

        public static int getValue(IArgumentMarshaler marshaler)
        {
            if (marshaler != null && marshaler is IntegerArgumentMarshaler)
                return ((IntegerArgumentMarshaler)marshaler).value;
            else
                return 0;
        }
    }
}