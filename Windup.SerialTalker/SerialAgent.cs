using System;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace Windup.SerialTalker
{
    public sealed class SerialAgent
    {
        SerialPort serial;
        TransferDataDelegate transferDataDelegate;
        TransferDataByLinesDelegate transferDataByLinesDelegate = null;
        string platform = "";
        string runtime = "";
        Thread readRunner = null;
        object lock_s = new object ();

        #region Define SerialAgent Attribute
        public string AgentPortName {
            set { if (!serial.IsOpen) serial.PortName = value; }
            get { return serial.PortName; }
        }

        public int AgentBaudRate
        {
            set { if (!serial.IsOpen) serial.BaudRate = value; }
            get { return serial.BaudRate; }
        }

        public Parity AgentParity
        {
            set { if (!serial.IsOpen) serial.Parity = value; }
            get { return serial.Parity; }
        }

        public int AgentDataBits
        {
            set { if (!serial.IsOpen) serial.DataBits = value; }
            get { return serial.DataBits; }
        }

        public StopBits AgentStopBits
        {
            set { if (!serial.IsOpen) serial.StopBits = value; }
            get { return serial.StopBits; }
        }

        public Handshake AgentHandshake
        {
            set { if (!serial.IsOpen) serial.Handshake = value; }
            get { return serial.Handshake; }
        }

        public int AgentReadTimeout
        {
            set { if (!serial.IsOpen) serial.ReadTimeout = value; }
            get { return serial.ReadTimeout; }
        }

        public int AgentWriteTimeout
        {
            set { if (!serial.IsOpen) serial.WriteTimeout = value; }
            get { return serial.WriteTimeout; }
        }
        #endregion

        #region Define private member funtion
        void JudgePlatform()
        {
            platform = Platform.IsMac ? "Mac OSX" : (Platform.IsWindows ? "Windows" : "Linux");
        }

        void JudgeRuntime()
        {
            Type t = Type.GetType("Mono.Runtime");
            runtime = t != null ? "Mono" : ".NET";
        }

        void DefaultTimeoutSet()
        {
            serial.ReadTimeout = 500;
            serial.WriteTimeout = 500;
        }

        void ReadDataToExternalVector (Int32 data)
        {
            transferDataDelegate(data);
        }

        void ReadThread()
        {
            Debug.WriteLine("Enter ReadThread");
            do {
                if (serial.BytesToRead > 0) {
                    lock (lock_s) {
                        var result = (Int32)serial.ReadByte();
                        ReadDataToExternalVector(result);
                        Debug.WriteLine(result);
                    }
                }
                Thread.Sleep(10);
            } while (true);
        }

        void DataReceviedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            var sp = sender as SerialPort;
            if (null != sp) {
                var result = sp.ReadByte();
                ReadDataToExternalVector(result);
                Debug.WriteLine(result.ToString());
            }
        }

        WriteFlagEnum WriteWindows(byte[] what)
        {
            var flag = WriteFlagEnum.Successed;
            if (!serial.IsOpen)
                flag = WriteFlagEnum.NotOpen;
            else {
                try {
                    serial.Write(what, 0, what.Length);
                } catch {
                    flag = WriteFlagEnum.Exception;
                }
            }
            return flag;
        }

        WriteFlagEnum WriteUnix(byte[] what)
        {
            var flag = WriteFlagEnum.Successed;
            if (!serial.IsOpen)
                flag = WriteFlagEnum.NotOpen;
            else {
                try {
                    lock (lock_s) {
                        serial.Write(what, 0, what.Length);
                    }
                } catch {
                    flag = WriteFlagEnum.Exception;
                }
            }
            return flag;
        }
        #endregion

        #region Define public member funtion
        public SerialAgent()
            : this("", 9600, Parity.None, 8, StopBits.One)
        {
        }

        public SerialAgent(string portName)
            : this(portName, 9600, Parity.None, 8, StopBits.One)
        {
        }

        public SerialAgent(string portName, int baudRate)
            : this(portName, baudRate, Parity.None, 8, StopBits.One)
        {
        }

        public SerialAgent(string portName, int baudRate, Parity parity)
            : this(portName, baudRate, parity, 8, StopBits.One)
        {
        }

        public SerialAgent(string portName, int baudRate, Parity parity, int dataBits)
            : this(portName, baudRate, parity, dataBits, StopBits.One)
        {
        }


        public SerialAgent(string portName = "", int baudRate = 9600, Parity parity = Parity.None,
                           int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            //Debug.Assert(!string.IsNullOrEmpty(portName));
            if (string.IsNullOrEmpty(portName))
                serial = new SerialPort();
            else
                serial = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            DefaultTimeoutSet();
            JudgePlatform();
            JudgeRuntime();
        }

        public static bool TouchAgentPort(string portName, int baudRate)
        {
            SerialPort sp = null;
            bool result = true;
            try {
                sp = new SerialPort(portName, baudRate);
                if (!sp.IsOpen)
                    sp.Open();
                sp.Close();
            }catch (Exception) {
                result = false;
            }
            return result;
        }

        public void BindDataDelegate(TransferDataDelegate tdd)
        {
            transferDataDelegate = tdd;
        }

        public void BindTransferDataByLinesDelegate(TransferDataByLinesDelegate tdbd)
        {
            transferDataByLinesDelegate = tdbd;
        }

        public void SetTerminationCharArray(Int32[] charArray)
        {
        }

        public void AgentOpen()
        {
            if (platform == "Windows" && ".NET" == runtime) {
                serial.DataReceived += new SerialDataReceivedEventHandler(DataReceviedHandler);
                serial.Open();
            } else {
                serial.Open();
                readRunner = new Thread(new ThreadStart(ReadThread));
                readRunner.Start();
            }
        }

        public WriteFlagEnum AgentWrite(byte[] what)
        {
            if (platform == "Windows")
                return WriteWindows(what);
            else
                return WriteUnix(what);
        }

        public void AgentClose()
        {
            if (null != readRunner && readRunner.IsAlive)
                readRunner.Abort();
            if (serial.IsOpen)
                serial.Close();
        }

        public override string ToString()
        {
            return string.Format("[SerialAgent: AgentPortName={0}, AgentBaudRate={1}, " +
                "AgentParity={2}, AgentDataBits={3}, AgentStopBits={4}, AgentHandshake={5}, " +
                "AgentReadTimeout={6}, AgentWriteTimeout={7}]",
                                  AgentPortName, AgentBaudRate, AgentParity, AgentDataBits,
                                  AgentStopBits, AgentHandshake, AgentReadTimeout, AgentWriteTimeout);
        }
        #endregion
    }
}
