using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace SystemInformationLibrary
{
    public class SystemSpecs
    {
        public class SystemSpecifications
        {
            public string NetworkAdapter;
            public string Processor;
            public string TotalRAMAvailable;
            public string VideoCard;
            public string SoundCard;
            public string Motherboard;
            public string CDRom;
            public string Monitor;
            public string OperatingSystem;


            public SystemSpecifications()
            {
                string network = GetComponent("Win32_NetworkAdapter", "Name");
                string proc = GetComponent("Win32_Processor", "Name");
                string clockSpeed = GetComponent("Win32_Processor", "MaxClockSpeed");
                string arch = GetComponent("Win32_Processor", "Architecture");
                string totalRam = GetComponent("Win32_ComputerSystem", "TotalPhysicalMemory");
                string vidCard = GetComponent("Win32_VideoController", "Name");
                string vidCard_RAM = GetComponent("Win32_VideoController", "AdapterRAM");
                string soundCard = GetComponent("Win32_SoundDevice", "Caption");
                string moboIdent = GetComponent("Win32_BaseBoard", "Product");
                string cdRom = GetComponent("Win32_CDROMDrive", "Name");
                string monitor = GetComponent("Win32_DesktopMonitor", "Name");
                Microsoft.VisualBasic.Devices.ComputerInfo ci = new Microsoft.VisualBasic.Devices.ComputerInfo();
                string osVersion = ci.OSFullName;
                bool is64bit = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"));
                //
                float totalRam_b = float.Parse(totalRam);
                float totalRam_mb = (totalRam_b / 1024f) / 1024f;
                float maxClockSpeedMhz = float.Parse(clockSpeed);
                float maxClockSpeedGhz = maxClockSpeedMhz / 1000f;
                float totalVidRam_b = float.Parse(vidCard_RAM);
                float totalVidRam_mb = (totalVidRam_b / 1024f) / 1024f;
                string mb_gb = string.Format("{1} GB ({0} MB)", totalRam_mb, Math.Round(totalRam_mb / 1024f, 2));
                //
                string proc_FINAL = string.Format("{0} @ {1}ghz; {2}-Bit", proc, maxClockSpeedGhz.ToString(), returnArchitecture(arch));
                string vid_FINAL = string.Format("{0} {1}GB ({2} MB)", vidCard, totalVidRam_mb / 1024f, totalVidRam_mb);
                string OS;
                if (Environment.Is64BitOperatingSystem)
                    OS = osVersion + " 64-Bit";
                else
                    OS = osVersion + " 32-Bit";
                //
                NetworkAdapter = network;
                Processor = proc_FINAL;
                TotalRAMAvailable = mb_gb;
                VideoCard = vid_FINAL;
                SoundCard = soundCard;
                Motherboard = moboIdent;
                CDRom = cdRom;
                Monitor = monitor;
                OperatingSystem = OS;
            }
            private static string GetComponent(string hwclass, string syntax)
            {
                try
                {
                    ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM " + hwclass);
                    foreach (ManagementObject mj in mos.Get())
                    {
                        return mj[syntax].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: Error while trying to retrieve {0} for {1}\n\tException: {2}", syntax, hwclass, ex.Message);
                }
                return "NULL";
            }
            private static string returnArchitecture(string toDetect)
            {
                int bit = int.Parse(toDetect);
                switch (bit)
                {
                    case (9):
                        return "64";
                    case (6):
                        return "IA64";
                    case (0):
                        return "32";
                }

                return null;
            }
            //
        }
    }
}
