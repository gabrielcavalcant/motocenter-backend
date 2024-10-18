namespace OficinaMotocenter.Application.Exceptions
{
    /// <summary>
    /// Base exception class for invalid argument exceptions scenarios.
    /// </summary>
    public class InvalidArgumentException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidArgumentException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidArgumentException(string message)
            : base(message)
        {
        }
    }
}
