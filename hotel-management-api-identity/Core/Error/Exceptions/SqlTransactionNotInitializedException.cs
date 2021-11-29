namespace hotel_management_api_identity.Core.Error.Exceptions
{
    public class SqlTransactionNotInitializedException : Exception
    {
        public SqlTransactionNotInitializedException() { }
        public SqlTransactionNotInitializedException(string message) : base(message) { }
    }
}