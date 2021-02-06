using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Server.Commons
{
    public struct Errors
    {
        // Login errors
        public static string DuplicateLoginError() => "User with this login already exists.";
        public static string LongLoginError() => "Login length can not be longer than 15 characters.";
        public static string ShortLoginError() => "Login length can not be shorter than 6 characters.";
        public static string InvalidLoginError() => "Login does not match the pattern.";

        // Password errors
        public static string LongPasswordError() => "Password length can not be longer than 15 characters.";
        public static string ShortPasswordError() => "Password length can not be shorter than 6 characters.";

        //Name errors
        public static string LongFirstnameError() => "Firstname length can not be longer than 30 characters.";
        public static string EmptyFirstnameError() => "Firstname can not be empty.";
        public static string InvalidFirstnameError() => "Firstname does not match the pattern.";
        public static string LongLastnameError() => "Lastname length can not be longer than 30 characters.";
        public static string EmptyLastnameError() => "Lastname can not be empty.";
        public static string InvalidLastnameError() => "Login does not match the pattern.";

        // Auth errors
        public static string AuthError() => "Authorization failed. Please check the login and password.";

        //Chat errors
        public static string LongMessageError() => "Message length can not be greater than 256 characters.";
        public static string EmptyMessageError() => "Message can not be empty.";
        public static string DuplicateMessageError() => "Message should be different from your previous message.";
    }
}
