using System.Runtime.InteropServices;
using System.Text;

namespace SoftwareLock
{
    public class HDDSerial
    {
        [DllImport("Kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int GetVolumeInformation(
       string lpRootPathName,
       StringBuilder lpVolumeNameBuffer,
       int nVolumeNameSize,
       out int lpVolumeSerialNumber,
       out int lpMaximumComponentLength,
       out int lpFileSystemFlags,
       StringBuilder lpFileSystemNameBuffer,
       int nFileSystemNameSize);

        public static string SerialNumber()
        {
            int serial; // Serial number
            var vName = new System.Text.StringBuilder(255); // Volume name
            var fsName = new System.Text.StringBuilder(255); // File system
            string drive = GetCurrentDrive();

            // Get the volume information
            GetVolumeInformation(drive, vName, 255, out serial, out _, out _, fsName, 255);

            // Return the serial number as a string
            return serial.ToString().Trim();
        }

        public static string GetCurrentDrive()
        {
            return System.IO.Directory.GetCurrentDirectory().Substring(0, 3);
        }
    }
}
