using System;
using System.Runtime.Serialization;

namespace VI_OblsugaBledow
{
    [Serializable]
    class TurtleException : Exception
    {
        public TurtleException()
        { }

        public TurtleException(string message) : base(message)
        { }

        public TurtleException(string message, Exception innerException) : base(message, innerException)
        { }

        protected TurtleException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
