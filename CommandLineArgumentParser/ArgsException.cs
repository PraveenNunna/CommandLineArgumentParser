using System;
using System.Runtime.Serialization;

namespace CommandLineArgumentParser
{
    [Serializable]
    internal class ArgsException : Exception
    {
        private ErrorCode _errorCode = ErrorCode.OK;
        private char _errorArgumentId = '\0';
        private string _errorParameter = "";

        public ArgsException()
        {
        }

        public ArgsException(string message) : base(message)
        {

        }

        public ArgsException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public ArgsException(ErrorCode errorCode)
        {
            this._errorCode = errorCode;
        }

        public ArgsException(ErrorCode errorCode, String errorParameter)
        {
            this._errorCode = errorCode;
            this._errorParameter = errorParameter;
        }

        public ArgsException(ErrorCode errorCode, char elementId, string elementTail)
        {
            this._errorCode = errorCode;
            this._errorArgumentId = elementId;
            this._errorParameter = elementTail;
        }

        protected ArgsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        public char getErrorArgumentId()
        {
            return _errorArgumentId;
        }
        public void setErrorArgumentId(char errorArgumentId)
        {
            this._errorArgumentId = errorArgumentId;
        }
        public String getErrorParameter()
        {
            return _errorParameter;
        }
        public void setErrorParameter(String errorParameter)
        {
            this._errorParameter = errorParameter;
        }
        public ErrorCode getErrorCode()
        {
            return _errorCode;
        }
        public void setErrorCode(ErrorCode errorCode)
        {
            this._errorCode = errorCode;
        }

        public String getErrorMessage()
        {
            switch (_errorCode)
            {
                case ErrorCode.OK:
                    return "TILT: Should not get here.";
                case ErrorCode.UNEXPECTED_ARGUMENT:
                    return String.Format("Argument {0} unexpected.", _errorArgumentId);
                case ErrorCode.MISSING_STRING:
                    return String.Format("Could not find string parameter for {0}.",
                    _errorArgumentId);
                case ErrorCode.INVALID_INTEGER:
                    return String.Format("Argument -%c expects an integer but was {0}.",
                    _errorArgumentId, _errorParameter);
                case ErrorCode.MISSING_INTEGER:
                    return String.Format("Could not find integer parameter for {0}.",_errorArgumentId);
                case ErrorCode.INVALID_DOUBLE:
                    return String.Format("Argument -%c expects a double but was {0}.",
                    _errorArgumentId, _errorParameter);
                case ErrorCode.MISSING_DOUBLE:
                    return String.Format("Could not find double parameter for {0}.",
                    _errorArgumentId);
                case ErrorCode.INVALID_ARGUMENT_NAME:
                    return String.Format("{0} is not a valid argument name.",
                    _errorArgumentId);
                case ErrorCode.INVALID_ARGUMENT_FORMAT:
                    return String.Format("{0} is not a valid argument format.",
                    _errorParameter);
            }
            return "";
        }
    }
}