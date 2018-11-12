using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineArgumentParser
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var args1 = new string[] { "-l", "true", "-d", "praveen", "-p", "123" };
                Args arg = new Args("l,p#,d*", args1);
                bool logging = arg.getBoolean('l');
                int port = arg.getInt('p');
                String directory = arg.getString('d');

                executeApplication(logging, port, directory);
            }
            catch (ArgsException e)
            {
                Console.WriteLine("Argument error: {0}", e.getErrorMessage());
            }

            Console.ReadKey();
        }

        private static void executeApplication(bool logging, int port, string directory)
        {
            Console.WriteLine($"Executing application with parameters: {logging}, {port}, {directory}.");
        }
    }
}
