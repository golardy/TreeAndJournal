namespace TreeAndJournal.Api.Exceptions
{
    public record ExceptionDetails(int StatusCode, string Type, int Id,string Data);
}
