using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Constants
{
    public class MAIL
    {
        public static string MAIL_SERVER { get; private set; }
        public static int? MAIL_PORT { get; private set; }
        public static bool MAIL_USE_DEFAULT_CREDENTIALS { get; private set; }
        public static string MAIL_CREDENTAIL_USERNAME { get; private set; }
        public static string MAIL_CREDENTAIL_PASSWORD { get; private set; }
        public static bool MAIL_SSL { get; private set; }
        public static string MAIL_SENDER { get; private set; }
        public static string MAIL_SENDER_NAME { get; private set; }
    }
}
