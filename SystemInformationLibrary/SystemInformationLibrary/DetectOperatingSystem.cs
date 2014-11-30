using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemInformationLibrary
{
    public class DetectOperatingSystem
    {
        public enum OSFriendly { Windows95, Windows98, WindowsME, Windows2000, WindowsXP, WindowsVista, Windows7, Windows8, Windows81, Windows10, Linux, Unknown }

        public OSFriendly OSName()
        {
            double osVersion = double.Parse(Environment.OSVersion.Version.Major.ToString() + "." + Environment.OSVersion.Version.Minor.ToString());

            if (osVersion == 5.0)
                return OSFriendly.Windows2000;
            else if (osVersion == 5.1)
                return OSFriendly.WindowsXP;
            else if (osVersion == 6.0)
                return OSFriendly.WindowsVista;
            else if (osVersion == 6.1)
                return OSFriendly.Windows7;
            else if (osVersion == 6.2)
                return OSFriendly.Windows8;
            else if (osVersion == 6.3)
                return OSFriendly.Windows81;
            else
                return OSFriendly.Unknown;

        }
    }
}
