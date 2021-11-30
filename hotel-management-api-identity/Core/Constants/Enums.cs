namespace hotel_management_api_identity.Core.Constants
{
    public class Enums
    {
        public enum Id
        {
            NONE = 0,
            PASSPORT,
            NIN_SLIP,
            DRIVERS_LICENSE,
            VOTER_ID,
            NATIONAL_ID
        }

        public enum User
        {
            Administrator = 1,
            Manager,
            Supervisor
        }

        public enum Gender
        {
            Male = 1,
            Female
        }

        public enum Category
        {
            Bar = 1,
            Kitchen
        }
    }
}