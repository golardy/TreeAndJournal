namespace TreeAndJournal.Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public string Error { get; }
        public CustomValidationException(string error)
        {
            Error = error;
        }
    }
}
