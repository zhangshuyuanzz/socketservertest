using Common.log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fujidev
{
    public partial class Form1 : Form
    {
        readonly NLOG logger = new NLOG("Fujidev");
        Fujiserial Devserial = new Fujiserial();
        public Form1()
        {
            logger.Debug("InitializeComponent--");
            InitializeComponent();
            
            Devserial.Serial_read_handle += new Fujiserial.SerialDataEventHandler(CBserialData);
        }

        private void But_Click(object sender, EventArgs e)
        {
            logger.Debug("send-frame [{}]------", this.framein.Text);
            Dev_send_Frame();

        }
        private void Dev_addr_in(object sender, EventArgs e)
        {
            logger.Debug("addrin -Text--[{}]------", addrin.Text);
            byte c = 0;
            byte.TryParse(addrin.Text, out c);
            logger.Debug("addrin -int--[{}]------", c);
            if (c == 0)
            {
                addrin.Text = "error";
            }
            else {
                Dev_set_Frame();
            }
        }
        private void Dev_serial_in(object sender, EventArgs e)
        {
            logger.Debug("serialin -Text--[{}]------", serialin.Text);
            int c = -1;
            logger.Debug("serialin -int--[{}]------", int.TryParse(serialin.Text,out c));
            byte Dserial = (byte)c;
            logger.Debug("Dserial-[{}]------", Dserial);

        }
        private void Dev_set_Frame()
        {
            string sendframe = "W" + addrin.Text + "PDQH&PDI+" + "\r";
            logger.Debug("-----------------------sendframe[{}]--------", sendframe);
            this.framein.Text = sendframe;
        }
        void Dev_send_Frame()
        {
            byte[] bframe = System.Text.Encoding.ASCII.GetBytes(this.framein.Text);
            byte[] bframe2 = System.Text.Encoding.Default.GetBytes(this.framein.Text);
            foreach (byte s in bframe)
            {
                logger.Debug("---------1--------------s[{:x2}]--------", s);
            }
            foreach (byte s in bframe2)
            {
                logger.Debug("------------2-----------s[{:x2}]--------", s);
            }

            Devserial.Serial_write_date(bframe);
            test_try_parse_float();
        }
        void CBserialData(byte[] data)
        {
            foreach (byte s in data) {
                logger.Debug("----cb--------[{:X2}]-----",s);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string ff = "COM" + this.serialin.Text;
            logger.Debug("-------Button1_Click---serialcert.Text--[{}]--", ff);
            if (Devserial.Get_serial_name(ff))
            {
                Devserial.Set_serial_port_attr(ff,1200,8,1,0);
                Devserial.Open_serial_port();
            }
            else {
                this.serialin.Text = "error";
            }
        }
        public void Form1Closed(object sender, EventArgs e)
        {
            logger.Debug("form1_closed");
            Devserial.Colse_serial();
            System.Environment.Exit(System.Environment.ExitCode);
            this.Dispose();
            this.Close();
        }
        public void test_try_parse_float()
        {
            string ss = "-1.000000E+03m3/d!AC\r+2.000000E+08m/s!88\r";
            byte[] frame = System.Text.Encoding.ASCII.GetBytes(ss);
            int count = 1;
            ParsedevValue((byte)count, frame);
        }


        private string[] Split(byte[] data, int c)
        {
            int idx = 0;
            string[] res = new string[c];
            for (int i = 0; i < c; i++)
            {
                int c1 = idx;
                int c2 = idx;
                byte crc = 0;
                for (; idx < data.Length; idx++)
                {
                    byte v = data[idx];
                    if (c1 == c2 && (v < 43 || (v > 57 && v != 69)))
                    {
                        c2 = idx;
                    }
                    if (data[idx] == 33)
                    {
                        idx += 4;
                        logger.Debug("idx [{}]  Length[{}]", idx, data.Length);
                        if (idx >= data.Length) {
                            break;
                        }
                        if (data[idx] == 10)
                            idx++;
                        break;
                    }
                    crc += data[idx];
                }

                res[i] = System.Text.Encoding.ASCII.GetString(data, c1, c2 - c1 + 1);
                logger.Debug("res [{}]",res[i]);
            }

            return res;
        }

        private float ChangeDataToD(String data)
        {
            decimal v = 0.0M;
            try
            {
                if (data.Contains("E") || data.Contains("e"))
                {
                    data = data.Substring(0, data.Length - 1).Trim();
                    v = Convert.ToDecimal(Decimal.Parse(data.ToString(), System.Globalization.NumberStyles.Float));
                    logger.Debug("vvvvvvvvvvv[{}]",v);
                }
                else
                {
                    logger.Debug("---------------------------------------------");
                    v = decimal.Parse(data);
                }
            }
            catch (Exception e)
            {

            }
            logger.Debug("data[{}]v[{}]ToString[{}]", data, v, v.ToString());

            logger.Debug("to[{}]v[{}]", float.Parse(v.ToString()),(float)v);

            return float.Parse(v.ToString());
        }

        public float ParsedevValue(byte para_mark, byte[] framedata)
        {
            logger.Debug("ParsedevValue");
            string[] values = Split(framedata, 2);
            int a =0;
            float[] dd = new float[2];
            while (a != 2 ) {
                dd[a] = ChangeDataToD(values[a]);
                logger.Debug("ChangeDataToD   [{}]",dd);
                a++;
            }
            Dev_set_parse_value(dd);
            return 0;
        }
        public void Dev_set_parse_value(float[] value)
        {
            int cont = 0;
            foreach (float s in value)
            {
                logger.Debug("s[{}]cont[{}]",s,cont);
                if (cont == 0)
                {
                    this.currentin.Text = ((decimal)s).ToString();
                }
                else {
                    this.totalin.Text = ((decimal)s).ToString();
                }
                cont++;
            }
        }
    }
}
