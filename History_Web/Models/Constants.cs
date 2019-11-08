using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace History_Web
{
    public class Constants
    {

        //default known user lowest level
        public const int StudentID = 14;
        public const string Student = "Student,Historian,Admin,Developer";
        //this is a user that's allowed to create and publish an article
        public const int HistorianID = 15;
        public const string Historian = "Historian,Admin,Developer";
        // everything but crud on roles and users
        public const int AdminID = 16;
        public const string Admin = "Admin,Developer";
        // god-like power
        public const int DeveloperID = 19;
        public const string Developer = "Developer";

        //for my views
        public const string Stu = "Student";
        public const string Hist = "Historian";
        public const string Ad = "Admin";
        public const string Dev = "Developer";
        

        public const int MinPasswordLength = 6;
        public const int MaxPasswordLength = 18;

        public const int SaltSize = 25;
        public const string PasswordRequirementsMessage = "The Password must contain at Least One Capital letter, One Lowercase letter, One Number and One Special Character.";
        public const string PasswordRequirements = @"^((?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()+=:;.,\-])(?=.*[0-9])).+$";

        public const int MinUserNameLength = 5;
        public const int MaxUserNameLength = 25;
    }
}