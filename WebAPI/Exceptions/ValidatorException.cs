namespace WebAPI.Exceptions
{
    public class ValidatorException : Exception
    {
        public ValidatorException(string message) : base(message)
        {

        }

        public ValidatorException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
