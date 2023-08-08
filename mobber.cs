using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Memory;

namespace CRadar
{
    public partial class Form1 : Form
    {
        Mem m = new Mem();
        public Form1()
        {
            InitializeComponent();
        }
        Pen m_iYeni = new Pen(Color.Yellow, 3);
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics; SolidBrush redBrush = new SolidBrush(Color.Red);
            try
            {
                for (int i = 0; i <164; i += 4)
                {
                    // m_iVecOrigin Pointer > (0x2C4);
                    float m_iVecOriginY = m.ReadFloat($"metin2client.bin+0114EBCC,{i},2C4");
                    float Gelen1Y = Math.Abs(m_iVecOriginY) / 100;listBox1.Items.Add(m_iVecOriginY);
                    float m_iVecOriginX = m.ReadFloat($"metin2client.bin+0114EBCC,{i},2C0");
                    float Gelen1X = Math.Abs(m_iVecOriginX) / 100; listBox2.Items.Add(m_iVecOriginX);


                    // Mob Lock XD
                    float playersY = m.ReadFloat("metin2client.bin+011D3404,c,1B18");
                    float playersX = m.ReadFloat("metin2client.bin+011D3404,c,1b14");
                    float playersZ = m.ReadFloat("metin2client.bin+011D3404,c,1B1C");
                    if (checkBox1.Checked)
                    {
                        m.WriteMemory($"metin2client.bin+0114EBCC,{i},1B18", "float", playersY.ToString());
                        m.WriteMemory($"metin2client.bin+0114EBCC,{i},1b14", "float", playersX.ToString());
                        m.WriteMemory($"metin2client.bin+0114EBCC,{i},1B1C", "float", playersZ.ToString());
                    }


                    //e.Graphics.DrawRectangle(m_iYeni, Gelen1Y + 10, Gelen1X + 10, 3, 3);

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int PID = int.Parse("15900");
            if (PID > 0)
            {
                m.OpenProcess(PID);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.Refresh();
        }
    }
}
