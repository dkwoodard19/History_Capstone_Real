using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace History_Web
{
    public class Constants
    {
        public const int DefaultRoleID = 14;
        public const string DefaultRoleName = "Student";
        public const int PowerRoleID = 15;
        public const string PowerRoleName = "Historian";
        public const int AdminID = 16;
        public const string Admin = "Administrator";
        public const int AnonID = 17;
        public const string AnonName = "Unknown";

        public const int MinPasswordLength = 6;
        public const int MaxPasswordLength = 18;

        public const int SaltSize = 25;
        public const string PasswordRequirementsMessage = "The Password must contain at Least One Capital letter, One Lowercase letter and One Number.";
        public const string PasswordRequirements = @"^((?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()+=:;.,\-])(?=.*[0-9])).+$";

        public const int MinUserNameLength = 5;
        public const int MaxUserNameLength = 25;
    }
}