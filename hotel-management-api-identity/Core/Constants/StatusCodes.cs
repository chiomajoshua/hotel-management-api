namespace hotel_management_api_identity.Core.Constants
{
    public class StatusCodes
    {
        public const string Successful = "00";
        public const string AuditTrailException = "01";
        public const string SQLException = "02";
        public const string SQLTimeoutException = "03";
        public const string EndpointTimeoutException = "04";
        public const string AuditTrailObjectEmpty = "05";
        public const string GeneralError = "06";
        public const string InvalidElasticTableName = "07";
        public const string CacheDataRetrievalError = "08";
        public const string ModelValidationError = "09";
        public const string FatalError = "96";
        public const string NoRecordFound = "25";
        public const string RecordExist = "26";
        public const string EndpointTimeout = "91";

        public const string SampleErrorCode = "TMP-xx";
        public const string SpecificDeplicateCode = "TMP-26.X";
    }
}