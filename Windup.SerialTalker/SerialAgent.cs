using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace Windup.SerialTalker
{
    public sealed class SerialAgent
    {
        SerialPort serial;
        //int queueFlag = 0;
        //int delegateFlag = 0;
        TransferDataDelegate transferDataDelegate;
		Thread readRunner = null;
		object lock_s = new object();

        void defaultTimeoutSet()
        {
            serial.ReadTimeout = 500;
            serial.WriteTimeout = 500;
        }

		void ReadThread ()
		{
			Debug.WriteLine("Enter ReadThread");
			do{
				if(serial.BytesToRead > 0){
					lock(lock_s){
						var result = (char)serial.ReadByte();
						Debug.WriteLine(result);
					}
				}
				Thread.Sleep(10);
			}while(true);
		}

        public SerialAgent()
        {
            serial = new SerialPort();
            defaultTimeoutSet();
        }

        public SerialAgent(string portName)
        {
            Debug.Assert(!string.IsNullOrEmpty(portName));
            serial = new SerialPort(portName);
            defaultTimeoutSet();
        }

        public SerialAgent(string portName, int baudRate)
        {
            Debug.Assert(!string.IsNullOrEmpty(portName));
            serial = new SerialPort(portName, baudRate);
            defaultTimeoutSet();
        }

        public SerialAgent(string portName, int baudRate, Parity parity)
        {
            Debug.Assert(!string.IsNullOrEmpty(portName));
            serial = new SerialPort(portName, baudRate, parity);
            defaultTimeoutSet();
        }

        public SerialAgent(string portName, int baudRate, Parity parity, int dataBits)
        {
            Debug.Assert(!string.IsNullOrEmpty(portName));
            serial = new SerialPort(portName, baudRate, parity, dataBits);
            defaultTimeoutSet();
        }


        public SerialAgent(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            Debug.Assert(!string.IsNullOrEmpty(portName));
            serial = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            defaultTimeoutSet();
        }

        public static bool TouchAgentPort(string portName, int baudRate)
        {
            SerialPort sp = null;
            bool result = true;
            try{
                sp = new SerialPort(portName, baudRate);
                if(!sp.IsOpen)
                    sp.Open();
                sp.Close();
            }
            catch(Exception){
                result = false;
            }
            return result;
        }

        public void BindDataDelegate(TransferDataDelegate tdd)
        {
            transferDataDelegate = tdd;
        }

        public Queue<Int32> DataQueue
        {
            get;
            private set;
        }

        public string AgentPortName
        {
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
            set { if(!serial.IsOpen) serial.ReadTimeout = value; }
            get { return serial.ReadTimeout; }
        }

        public int AgentWriteTimeout
        {
            set { if (!serial.IsOpen) serial.WriteTimeout = value; }
            get { return serial.WriteTimeout; }
        }

        public void AgentOpen()
        {
            //serial.DataReceived += new SerialDataReceivedEventHandler(DataReceviedHandler);
            serial.Open();
			readRunner = new Thread(new ThreadStart(ReadThread));
			readRunner.Start();
        }

        public WriteFlagEnum AgentWrite (byte[] what)
		{
			var flag = WriteFlagEnum.Successed;
			if (!serial.IsOpen)
				flag = WriteFlagEnum.NotOpen;
			else {
				try {
					lock (lock_s) {
						serial.Write (what, 0, what.Length);
					}
				} catch {
					flag = WriteFlagEnum.Exception;
				}
			}
            return flag;
        }

        public void AgentClose()
        {
			if(readRunner.IsAlive)
				readRunner.Abort();
            if (serial.IsOpen)
                serial.Close();
        }


		/*
        private void DataReceviedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            var sp = sender as SerialPort;
            if (null != sp) {
                var temp = sp.ReadByte();
                if (1 == queueFlag) {
                    if(-1 != temp)
                        DataQueue.Enqueue(temp);
                }
                if (1 == delegateFlag) {
                    if(-1 != temp)
                        transferDataDelegate(temp);
                }
            }
        }
*/
    }
}
