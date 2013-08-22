using System.IO;
using System.IO.Ports;
using XBee.Utils;

namespace XBee
{
    public class SerialConnection : IXBeeConnection
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly SerialPort serialPort;
        private IPacketReader reader;

        public SerialConnection(string port, int baudRate)
        {
            serialPort = new SerialPort(port, baudRate);
            serialPort.DataReceived += ReceiveData;
        }

        private void ReceiveData(object sender, SerialDataReceivedEventArgs e)
        {
            var length = serialPort.BytesToRead;
            var buffer = new byte[length];

            serialPort.Read(buffer, 0, length);

            logger.Debug("Receiving data: [" + ByteUtils.ToBase16(buffer) + "]");
            reader.ReceiveData(buffer);
        }

        public void Write(byte[] data)
        {
            logger.Debug("Sending data: [" + ByteUtils.ToBase16(data) + "]");
            serialPort.Write(data, 0, data.Length);
        }

        public Stream GetStream()
        {
            return serialPort.BaseStream;
        }

        public void Open()
        {
            serialPort.Open();
        }

        public void Close()
        {
            serialPort.Close();
        }

        public void SetPacketReader(IPacketReader reader)
        {
            this.reader = reader;
        }
    }
}
