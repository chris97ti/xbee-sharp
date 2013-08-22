using System;
using System.Linq;

namespace XBee
{
    public class XBeeChecksum
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static byte Calculate(byte[] data)
        {
            var checksum = data.Aggregate(0, (current, b) => current + b);

            // discard values > 1 byte
            checksum = 0xff & checksum;
            // perform 2s complement
            checksum = 0xff - checksum;

            logger.Debug(String.Format("Computed checksum is 0x{0:X2}", checksum));
            return (byte)checksum;
        }

        public static bool Verify(byte[] data)
        {
            int checksum = Calculate(data);
            checksum = checksum & 0xff;

            logger.Debug(String.Format("Verify checksum is 0x{0:X2}", checksum));

            return checksum == 0x00;
        }
    }
}
