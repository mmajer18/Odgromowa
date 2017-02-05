using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using netDxf;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vector3 pk1 = new Vector3(3,5,1);
            Vector3 pk2 = new Vector3(11,7, 3);
            Vector3 pk3 = new Vector3(5,11,1);
            Vector3 pk4,pk5,pk6;
            Boolean war1=false, war2=false, war3=false;
            pk4 = new Vector3(1, 1, 1);
            pk5 = str_ochronna.Punkty(pk1, pk3, pk2, pk4, 30);
            double[] r1, r2, r3 = new double[2];
            r1 = str_ochronna.Prosta(pk1, pk2);
            r2 = str_ochronna.Prosta(pk1, pk3);
            r3 = str_ochronna.Prosta(pk2, pk3);
            pk6 = Vector3.Normalize(pk3 - Vector3.MidPoint(pk1, pk2));
            DxfDocument dxf = new DxfDocument();
            netDxf.Entities.Point punkt;
            // add your entities here
            
            // save to file



            double x = Math.Min(pk1.X,Math.Min(pk2.X,pk3.X));
            double y =  Math.Min(pk1.Y,Math.Min(pk2.Y,pk3.Y));
            while (x<Math.Max(pk1.X,Math.Max(pk2.X,pk3.X)))
            {
                y = Math.Min(pk1.Y, Math.Min(pk2.Y, pk3.Y));
                while (y<Math.Max(pk1.Y,Math.Max(pk2.Y,pk3.Y)))
                {
                    if (Vector3.Normalize(pk3 - Vector3.MidPoint(pk1, pk2)).Y > 0)
                    {
                        war1 = (y - r1[0] * x > r1[1]);
                    }
                    else
                    {
                        war1 = (y - r1[0] * x < r1[1]);
                    }
                    if (Vector3.Normalize(pk2 - Vector3.MidPoint(pk1, pk3)).Y > 0)
                    {
                        war2 = (y - r2[0] * x > r2[1]);
                    }
                    else
                    {
                        war2 = (y - r2[0] * x < r2[1]);
                    }
                    if (Vector3.Normalize(pk1 - Vector3.MidPoint(pk2, pk3)).Y > 0)
                    {
                        war3 = (y - r3[0] * x > r3[1]);
                    }
                    else
                    {
                        war3 = (y - r3[0] * x < r3[1]);
                    }
                    if (war1 & war2 & war3)
                    {
                        punkt = new netDxf.Entities.Point(x, y, 0);
                        dxf.AddEntity(punkt);
                    }
                    y = y + 0.1;
                }
                x=x+0.1;

            }

            dxf.Save("test_2.dxf");
            
        }
    }
}
