namespace FakerCore.Exceptions
{
    public class NoSuitableConstructorException: Exception
    {
        public NoSuitableConstructorException()
            : base()
        {

        }

        public NoSuitableConstructorException(string? message)
            : base(message)
        {

        }
    }
}
