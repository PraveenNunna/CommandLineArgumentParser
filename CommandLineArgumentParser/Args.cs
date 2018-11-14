using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CommandLineArgumentParser
{
    public class Args
    {
        private IDictionary<char, IArgumentMarshaler> marshalers;
        private IEnumerator<string> argumentsIterator;
        private HashSet<char> argsFound;

        internal bool getBoolean(char arg)
        {
            IArgumentMarshaler marshaller;

            if (marshalers.TryGetValue(arg, out marshaller))
            {
                return BooleanArgumentMarshaler.getValue(marshaller);
            }
            return false;
        }

        internal string getString(char arg)
        {
            IArgumentMarshaler marshaller;

            if (marshalers.TryGetValue(arg, out marshaller))
            {
                return StringArgumentMarshaler.getValue(marshaller);
            }
            return string.Empty;
        }

        internal int getInt(char arg)
        {
            IArgumentMarshaler marshaller;

            if (marshalers.TryGetValue(arg, out marshaller))
            {
                return IntegerArgumentMarshaler.getValue(marshaller);
            }
            return 0;
        }

        public Args(string schema, string[] args)
        {
            marshalers = new Dictionary<Char, IArgumentMarshaler>();
            argsFound = new HashSet<char>();
            parseSchema(schema);
            parseArgumentStrings(args.ToList());
        }

        private void parseArgumentStrings(List<string> arguments)
        {
            argumentsIterator = arguments.GetEnumerator();
            while (argumentsIterator.MoveNext())
            {
                String argString = argumentsIterator.Current;
                if (argString.StartsWith("-"))
                {
                    parseArgumentCharacters(argString.Substring(1));
                }
                else
                {
                    break;
                }
            }
        }

        private void parseArgumentCharacters(string argChars)
        {
            var firstCharacter = argChars.ElementAt(0);
            IArgumentMarshaler argumentMarshaler;
            var isValid = marshalers.TryGetValue(firstCharacter, out argumentMarshaler);

            if (isValid)
            {
                argsFound.Add(firstCharacter);

                try
                {
                    argumentMarshaler.set(argumentsIterator);
                }
                catch (ArgsException e)
                {
                    Console.WriteLine($"Exception caught.{e.Message}");
                    e.setErrorArgumentId(firstCharacter);
                    throw e;
                }
            }
            else
            {
                throw new ArgsException(ErrorCode.UNEXPECTED_ARGUMENT, firstCharacter, null);
            }
        }
        
        private void parseSchema(string schema)
        {
            var elements = schema.Split(',');
            foreach (var element in elements)
            {
                if (element.Length > 0)
                {
                    parseSchemaElement(element.Trim());
                }
            }
        }

        private void parseSchemaElement(String element)
        {
            char elementId = element.ElementAt(0);
            String elementTail = element.Substring(1);
            validateSchemaElementId(elementId);
            if (elementTail.Length == 0)
            {
                marshalers.Add(elementId, new BooleanArgumentMarshaler());
            }
            else if (elementTail.Equals("*"))
            {
                marshalers.Add(elementId, new StringArgumentMarshaler());
            }
            else if (elementTail.Equals("#"))
            {
                marshalers.Add(elementId, new IntegerArgumentMarshaler());
            }
            else if (elementTail.Equals("##"))
            {
                marshalers.Add(elementId, new DoubleArgumentMarshaler());
            }
            else if (elementTail.Equals("[*]"))
            {
                marshalers.Add(elementId, new StringArrayArgumentMarshaler());
            }
            else
            {
                throw new ArgsException(ErrorCode.INVALID_ARGUMENT_FORMAT, elementId, elementTail);
            }
        }

        private void validateSchemaElementId(char elementId)
        {
            if (!Char.IsLetter(elementId))
            {
                throw new ArgsException(ErrorCode.INVALID_ARGUMENT_NAME, elementId, null);
            }
        }
    }
}
