using System.Collections.Generic;

namespace CommandLineArgumentParser
{
    public interface IArgumentMarshaler
    {
        void set(IEnumerator<string> currentArgument);
    }
}
