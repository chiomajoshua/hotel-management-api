namespace hotel_management_api_identity.Core.Constants
{
    public class ResponseMessages
    {
        public const string InvalidUssdTransactionPin = "Sorry, that's not your ussd transaction PIN. Please, check and try again.";
        public const string LimitExceeded = "Sorry, you have reached your limit for today";
        public const string NotEnrolledForService = "Customer not enrolled for this service.";
        public const string DuplicteEnrollmentError = "Duplicate Enrollment";
        public const string EnrollmentSuccessful = "Enrollment successful.";
        public const string DuplicateKeyMessage = "duplicate key";
        public const string UpdateSuccessful = "Record successfully updated";
        public const string UpdateFailed = "Update operation failed. Kindly refer to the StatusCode.";
        public const string OperationSuccessful = "Operation successful!";
        public const string OperationFailed = "Operation Failed";
        public const string FailedProcessResponse = "Sorry, there was an error processing your request.";
        public const string CustomerCreatedSuccessfully = "Congratulations! Customer Information Has Been Successfully Added To Our Records";
        public const string CustomerAlreadyExists = "Sorry, The Customer Already Exists";
        public const string CustomerCreationFailed = "Sorry, We Could Not Create This Customer";

        public const string NoKycJobForUser = "No KYC Job was created for the user";
        public const string NoKYCToken = "Unable to get token from Mati";
        public const string UnableToCreateJob = "Unable to create kyc job on mati";
        public const string VerificationDataProcessing = "User Verification Process is still in progress";
        public const string DocumentAlreadyUploaded = "The User's document has been uploaded earlier";
        public const string UploadLimitExceeded = "You've exceeded the number of trials allowed in the last ";

        public const string ConsumerTopicNotHandled = "Subscriber topic not handled.";
        public const string ConsumeExceptionGotten = "Error occurred while processing stream data.";
        public const string ConsumerRetrialLogExceptionGotten = "Error occurred while retrying failed stream database log.";

        public const string ConsumerMessageReceived = "Consumer message received.";
        public const string ErrorConsuming = "Error occurred while consuming stream...";
        public const string ProducerRetrialLogExceptionGotten = "Error occurred while attempting to log failed producer message to temporal database";
        public const string ProducerExceptionGotten = "ProducerException occurred";
        public const string ProducerMessageReceived = "Message published...";
        public const string ErrorPublishing = "Error occurred while publishing stream...";

        public const string CachePersisterLogExceptionGotten = "Oops! A error occurred while attempting to log failed cache object to temporal database";
        public const string ErrorPerformingCacheServiceOperation = "Oops! An error occurred while performing cache operation.";

        public const string SQlTransactionNotInitialized = "Oops! An error occurred while processing your request. If this persists after three(3) trials, kindly contact your administrator.";
        public const string InvalidElasticTableName = "Could not locate entity for search. Kindly contact your administrator";
        public const string NoRecordFound = "No record was found";
        public const string GeneralError = "Oops! An error occurred while processing your request. If this persists after three(3) trials, kindly contact your administrator.";
        public const string SQlException = "Oops! A database error occurred while processing your request. If this persists after three(3) trials, kindly contact your administrator.";
        public const string AuditLogObjectEmpty = "Oops! A error occurred while preparing a trail for your request. If this persists after three(3) trials, kindly contact your administrator.";
        public const string TinNotPassed = "TIN not entered";
        public const string CACNotPassed = "CAC not entered";
    }
}