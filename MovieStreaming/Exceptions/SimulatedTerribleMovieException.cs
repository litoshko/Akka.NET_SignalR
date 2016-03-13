using System;
using System.Runtime.Serialization;

namespace MovieStreaming.Exceptions
{
    [Serializable]
    internal class SimulatedTerribleMovieException : Exception
    {
        public SimulatedTerribleMovieException()
        {
        }

        public SimulatedTerribleMovieException(string message) : base(message)
        {
        }

        public SimulatedTerribleMovieException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SimulatedTerribleMovieException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}