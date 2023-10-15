using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.StaticClass
{
    static class SessionStatic
    {
        private static string _phone_number;
        private static string _task;
        
        public static string GetPhoneNumber { get { return _phone_number; } }
        public static string SetPhoneNumber { set { _phone_number = value; } }

        public static string GetTask { get { return _task; } }
        public static string SetTask { set { _task = value; } }
    }
}
