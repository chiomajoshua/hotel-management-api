namespace hotel_management_api_identity.Core.Constants
{
    public class ResponseMessages
    {
       
        public const string OperationSuccessful = "Operation successful!";
        public const string OperationFailed = "Operation Failed";
        public const string FailedProcessResponse = "Sorry, there was an error processing your request.";

        public const string CustomerCreatedSuccessfully = "Congratulations! Customer Information Has Been Successfully Added To Our Records";
        public const string CustomerAlreadyExists = "Sorry, The Customer Already Exists";
        public const string CustomerCreationFailed = "Sorry, We Could Not Create This Customer";

        public const string EmployeeCreatedSuccessfully = "Congratulations! Employee Information Has Been Successfully Added To Our Records";
        public const string EmployeeAlreadyExists = "Sorry, The Employee Already Exists";
        public const string EmployeeCreationFailed = "Sorry, We Could Not Create This Employee";

        public const string RoomCreatedSuccessfully = "Congratulations! Room Information Has Been Successfully Added To Our Records";
        public const string RoomAlreadyExists = "Sorry, The Room Already Exists";
        public const string RoomCreationFailed = "Sorry, We Could Not Create This Room";

        public const string MenuCreatedSuccessfully = "Congratulations! Menu Information Has Been Successfully Added To Our Records";
        public const string MenuAlreadyExists = "Sorry, The Item Already Exists In The Menu";
        public const string MenuCreationFailed = "Sorry, We Could Not Add This Item To The Menu";

        public const string BookingCreatedSuccessfully = "Congratulations! Booking Information Has Been Successfully Added To Our Records";
        public const string BookingAlreadyExists = "Sorry, The Room is Already In-Use For The Duration Selected";
        public const string BookingCreationFailed = "Sorry, We Could Not Check-In this Customer";


        public const string NoRecordFound = "No record was found";
        public const string GeneralError = "Oops! An error occurred while processing your request. If this persists after three(3) trials, kindly contact your administrator.";

        public const string SQlTransactionNotInitialized = "Oops! An error occurred while processing your request. If this persists after three(3) trials, kindly contact your administrator.";
    }
}