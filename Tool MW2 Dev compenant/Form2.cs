using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS3Lib;
using DevComponents.DotNetBar.Metro;

namespace Tool_MW2_Dev_compenant
{
    public partial class Form2 : MetroForm
    {
        public static PS3API PS3 = new PS3API();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            PS3.Extension.WriteString(0x01CA2F3C, textBox1.Text);
            PS3.Extension.WriteString(0x01Ca597C, textBox2.Text);
            PS3.Extension.WriteString(0x01CA83BC, textBox3.Text);
            PS3.Extension.WriteString(0x01CAADFC, textBox4.Text);
            PS3.Extension.WriteString(0x01CAD83C, textBox8.Text);
            PS3.Extension.WriteString(0x01CB027C, textBox7.Text);
            PS3.Extension.WriteString(0x01CB2CAC, textBox6.Text);
            PS3.Extension.WriteString(0x01CB56FC, textBox5.Text);
            PS3.Extension.WriteString(0x01CB813C, textBox12.Text);
            PS3.Extension.WriteString(0x01CBAB7C, textBox11.Text);
            PS3.Extension.WriteString(0x01CBD5BC, textBox10.Text);
            PS3.Extension.WriteString(0x01CBFFFC, textBox9.Text);
            PS3.Extension.WriteString(0x01CC2A3C, textBox16.Text);
            PS3.Extension.WriteString(0x01CC546C, textBox15.Text);
            PS3.Extension.WriteString(0x01CC7EBC, textBox14.Text);
            PS3.Extension.WriteString(0x01CCA8FC, textBox13.Text);
            PS3.Extension.WriteString(0x01CCFD7C, textBox20.Text);
            PS3.Extension.WriteString(0x01CD27BC, textBox19.Text);
            PS3.Extension.WriteString(0x01CCD33C, textBox17.Text);
        }
    }
}
