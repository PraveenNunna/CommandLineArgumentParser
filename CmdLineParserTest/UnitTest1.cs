using System;
using CommandLineArgumentParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CmdLineParserTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MarshallersCountMustMatch()
        {
            var args = new Args("l,p#,d*,n", new string[] { "" });
            var marshallers = args.marshalers.Count;

            Assert.AreEqual(marshallers, 4);
        }
    }
}
