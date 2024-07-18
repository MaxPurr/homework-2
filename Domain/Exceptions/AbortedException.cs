namespace Domain.Exceptions
{
    public class AbortedException : Exception
    {
        public AbortedException(string? message = null) : base(message) { }
    }
}