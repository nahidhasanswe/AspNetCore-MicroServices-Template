using System;
using System.Runtime.Serialization;

namespace AspCoreMicroservice.Core.Data
{
    [Serializable]
    public class DbConcurrencyException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="DbConcurrencyException"/> object.
        /// </summary>
        public DbConcurrencyException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="Exception"/> object.
        /// </summary>
        public DbConcurrencyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="DbConcurrencyException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public DbConcurrencyException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="DbConcurrencyException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public DbConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
