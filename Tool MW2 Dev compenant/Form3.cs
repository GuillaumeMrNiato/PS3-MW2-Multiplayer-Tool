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
using MW2Lib;

namespace Tool_MW2_Dev_compenant
{
    public partial class Form3 : Form
    {
        public static PS3API PS3 = new PS3API(SelectAPI.TargetManager);
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (PS3.ConnectTarget() && PS3.AttachProcess())
                {
                    PS3.Extension.WriteInt32(0x10030000, 0x00724C38);
                    PS3.Extension.WriteInt32(0x10030004, 0x00734BE8);
                    PS3.Extension.WriteBool(0x01D0CE6C, false);
                    MessageBox.Show("Devkit Connected", "Stats All Clients MW2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed", "Stats All Clients MW2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed", "Stats All Clients MW2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MW2.SetClientDvars(-1, "loc_warnings 0");
            MW2.SetClientDvars(-1, "loc_warningsAsErrors 0");
            MW2.SetClientDvars(-1, "cg_fov 95");
            uint FirstElem = MW2.HudElemAlloc();
            uint SecondElem = MW2.HudElemAlloc();
            uint ThirdElem = MW2.HudElemAlloc();
            uint FourElem = MW2.HudElemAlloc();
            uint FirstIndexString = MW2.G_LocalizedString("This is an example by iMCSx");
            uint SecondIndexString = MW2.G_LocalizedString("Have Fun Guys!");
            MW2.SetShader(0, FirstElem, MW2.Material.Black, 1000, 30, 350, 380, 1, 0, 255, 255, 255, 150);
            MW2.SetText(0, SecondElem, FirstIndexString, 6, 0.6f, 235, 375, 1, 1, 255, 255, 255, 255, 5, 240, 240, 255);
            MW2.SetText(0, ThirdElem, SecondIndexString, 3, 1, 275, 388, 1, 1, 255, 255, 255, 255);
            MW2.SetShader(0, FourElem, MW2.Material.Prestige9, 50, 50, 320, 334, 1, 0, 255, 255, 255, 255);
            MW2.SetElement(FirstElem, MW2.HudTypes.Shader);
            MW2.SetElement(SecondElem, MW2.HudTypes.Text);
            MW2.SetElement(ThirdElem, MW2.HudTypes.Text);
            MW2.SetElement(FourElem, MW2.HudTypes.Shader);
            MW2.iPrintln(0, "^2Elems Spawned! :)");
        }
    }
}
