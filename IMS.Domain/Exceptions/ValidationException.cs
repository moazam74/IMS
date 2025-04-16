namespace IMS.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, string[]> Errors { get; }

        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(Dictionary<string, string[]> errors) : this()
        {
            Errors = errors;
        }

        public ValidationException(string message) : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(string message, Dictionary<string, string[]> errors) : base(message)
        {
            Errors = errors;
        }
    }
} 