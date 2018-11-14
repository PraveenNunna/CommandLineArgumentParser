using CommandLineArgumentParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CmdLineParserTest
{
    [TestClass]
    public class ArgsTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgsException))]
        public void InvalidArgumentShouldThrowException()
        {
            var args = new Args("l,p#,d*,n", new string[] { "-p" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgsException))]
        public void InvalidArgumentsCountShouldThrowException()
        {
            var args = new Args("l,p#", new string[] { "-d", "123" });
        }


    }
}
