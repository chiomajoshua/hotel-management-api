using hotel_management_api_identity.Core.Helpers.Models;

namespace hotel_management_api_identity.Core.Constants
{
    public class Enums
    {
        public enum Id
        {
            [EnumDisplay(Name = "None", Description = "None")]
            NONE = 0,
            [EnumDisplay(Name = "Passport", Description = "Internation Passport")]
            PASSPORT,
            [EnumDisplay(Name = "NIN_Slip", Description = "NIN Slip")]
            NIN_SLIP,
            [EnumDisplay(Name = "Drivers_license", Description = "Drivers License")]
            DRIVERS_LICENSE,
            [EnumDisplay(Name = "Voter_Id", Description = "Voters ID")]
            VOTER_ID,
            [EnumDisplay(Name = "National_Id", Description = "National ID")]
            NATIONAL_ID
        }

        public enum User
        {
            [EnumDisplay(Name = "Administrator", Description = "Administrator")]
            Administrator = 1,
            [EnumDisplay(Name = "Manager", Description = "Manager")]
            Manager,
            [EnumDisplay(Name = "Supervisor", Description = "Supervisor")]
            Supervisor
        }

        public enum Gender
        {
            [EnumDisplay(Name = "Male", Description = "Male")]
            Male = 1,
            [EnumDisplay(Name = "Female", Description = "Female")]
            Female
        }

        public enum Category
        {
            [EnumDisplay(Name = "Bar", Description = "Bar")]
            Bar = 1,
            [EnumDisplay(Name = "Kitchen", Description = "Kitchen")]
            Kitchen
        }
    }
}