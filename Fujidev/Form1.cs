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
        public float ParsedevValue(byte para_mark, byte[] framedata)
        {
            float f = 0;
            if (framedata == null || para_mark == 0)
            {
                return f;
            }
            byte[] parse_point = framedata;

            while ((para_mark - 1) != 0)
            {
                parse_point = (uint8*)strchr((sint8*)parse_point, 0x0d) + 1;
                if (parse_point == NULL)
                {
                    return 0;
                }
                QL_Ddebug_log(RGB_BLUE, "parse_point[%s]", parse_point)
                para_mark--;
            }
            sint32 pos = atoi((sint8*)parse_point);
            parse_point++;
            sint32 neg = 0;
            parse_point = ql_strchr((sint8*)parse_point, 0x2d, 0x2b);

            parse_point++;
            neg = atoi((sint8*)parse_point);
            f = pos * ql_pow(10, neg);
            QL_Ddebug_log(RGB_BLUE, "f[%f]neg[%d]pos[%d]", f, neg, pos)



            return f;
        }
    }
}
