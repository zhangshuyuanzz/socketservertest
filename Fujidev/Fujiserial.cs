using Common.log;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace Fujidev
{
    public class Fujiserial
    {
        private static readonly NLOG logger = new NLOG("serial_server");
        private SerialPort serial_comm = new SerialPort();

        public delegate void SerialDataEventHandler(byte[] data);
        public SerialDataEventHandler Serial_read_handle;
        public bool Get_serial_name(string srl)
        {
            string[] serial_name = SerialPort.GetPortNames();
            if (serial_name == null)
            {
                return false;
            }

            foreach (string str in serial_name)
            {
                logger.Debug("-------------str:{}-----------------------", str);
                if (srl == str.ToUpper()) {
                    logger.Debug("------tttt-------srl:{}-----------------------", srl);
                    return true;
                }
            }
            return false;
        }
        public bool Open_serial_port()
        {
            bool ret_open = false;
            try
            {
                if (!serial_comm.IsOpen)
                {
                    serial_comm.Open();
                }
                else
                {
                    logger.Error("this [{}] had opened", serial_comm.PortName);
                }
            }
            catch (Exception)
            {
                ret_open = false;
            }
            return ret_open;
        }
        public bool Colse_serial()
        {
            try
            {
                if (serial_comm.IsOpen)
                {
                    serial_comm.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Set_serial_port_attr(string port_name, int baud_rate, int data_bit, float stop_bit, int parity)
        {
            logger.Info("---por attr---{}--{}--{}---{}--{}------------", port_name, baud_rate, data_bit, stop_bit, parity);
            bool ret_falg = false;
            serial_comm.PortName = port_name;
            serial_comm.BaudRate = baud_rate;
            serial_comm.DataBits = data_bit;
            serial_comm.Encoding = Encoding.GetEncoding("Utf-8");
            serial_comm.ReceivedBytesThreshold = 1;
            serial_comm.DataReceived += new SerialDataReceivedEventHandler(Serail_read_byte);
            switch (stop_bit)
            {
                case 0:
                    serial_comm.StopBits = StopBits.None;
                    break;
                case 1.5f:
                    serial_comm.StopBits = StopBits.OnePointFive; ;
                    break;
                case 1:
                    serial_comm.StopBits = StopBits.One; ;
                    break;
                default:
                    serial_comm.StopBits = StopBits.Two;
                    break;
            }
            switch (parity)
            {
                case 0:
                    serial_comm.Parity = Parity.None;
                    break;
                case 1:
                    serial_comm.Parity = Parity.Odd;
                    break;
                case 2:
                    serial_comm.Parity = Parity.Even;
                    break;
                default:
                    serial_comm.Parity = Parity.None;
                    break;
            }
            // sp.ReadTimeout = 1000;//设置超时读取时间
            // sp.WriteTimeout = 1000;//超时写入时间                       no 

            return ret_falg;
        }
        public bool Serial_write_date(byte[] src_data)
        {
            bool ret_write = false;
            try
            {
                serial_comm.Write(src_data, 0, src_data.Length);
            }
            catch (Exception e)
            {
                logger.Error(" error [{}]",e.ToString());
            }

            return ret_write;
        }
        private void Serail_read_byte(object sender,
                        SerialDataReceivedEventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            byte[] read_byte = new byte[serial_comm.BytesToRead];
            int read_str = serial_comm.Read(read_byte, 0, read_byte.Length);
            logger.Info("Serail_read_byte ------read data  -len-[{}]", read_str);
            Serial_read_handle?.Invoke(read_byte);
        }
        public void ClearSerialInData()
        {
            if (!serial_comm.IsOpen)
            {
                serial_comm.DiscardInBuffer();
            }
            else
            {
                logger.Error("this [{}] had opened", serial_comm.PortName);
            }
        }
    }
}
