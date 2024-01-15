namespace TreeAndJournal.Api.Exceptions
{
    public record ExceptionDetails(int StatusCode, string Type, long Id,string Data);
}
