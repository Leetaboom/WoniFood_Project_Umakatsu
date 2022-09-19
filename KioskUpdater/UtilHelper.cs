using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KioskUpdater
{
    public static class UtilHelper
    {
        public static string Root
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }
    }
}
