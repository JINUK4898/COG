using OpenCvSharp.Flann;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Media;

namespace COG
{
    public partial class UC_ResultView_Label : UserControl
    {
        public UC_ResultView_Label()
        {
            InitializeComponent();
            CreatButton();
        }

        private List< System.Drawing.Color> colors = new List<System.Drawing.Color>();

        public int MaxCount { get; set; } = 25;

        private int camindex;
        public int CamIndex
        {
            get 
            {
                return camindex;
            }
            set 
            {
                camindex = value;

                LB_NAME.Text = "CAM" + (camindex + 1).ToString();
            }
        }


        private List<RadioButton> BTN_SET = new List<RadioButton>();
        public delegate void ClickEvent(int CamNo, int Index);
        public event ClickEvent ClickEventHandle;


        private void CreatButton()
        {
            colors.Clear();
            BTN_SET.Clear();
            PN_PANEL.Controls.Clear();

            int _Width = PN_PANEL.Width;
            int _Height = PN_PANEL.Height;// - MaxCount;


            int Pos_X = 0;
            int Pos_Y = 0;


            _Height /= MaxCount;

            for (int j = 0; j < MaxCount; j++)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Appearance = Appearance.Button;
                radioButton.FlatStyle = FlatStyle.Flat;
                radioButton.FlatAppearance.BorderColor = Color.Black;
                radioButton.FlatAppearance.BorderSize = 1;
                radioButton.Text = (j + 1).ToString();
                radioButton.TextAlign = ContentAlignment.MiddleCenter;
                radioButton.BackColor = Color.AliceBlue;
                radioButton.Font = new Font("Fixsys", 8, FontStyle.Bold);

           //     if(j > 0) Pos_Y += (_Height + 2);
                Pos_Y = (j * (_Height));

                radioButton.Location = new Point(Pos_X, Pos_Y);

                radioButton.Width = _Width; /*/ 2 - 5*/;
                radioButton.Height = _Height; /*12 - 4*/;


                radioButton.Tag = CamIndex;
                radioButton.Name = j.ToString();

                radioButton.Click -= new EventHandler(ImageViewLoad);
                radioButton.Click += new EventHandler(ImageViewLoad);

                colors.Add(radioButton.BackColor);
                BTN_SET.Add(radioButton);
                PN_PANEL.Controls.Add(BTN_SET[j]);

            }
        }

        private void ImageViewLoad(object sender, EventArgs e)
        {
            ClickEventHandle?.Invoke((int)((RadioButton)sender).Tag, Convert.ToInt16(((RadioButton)sender).Name));
        }

        private void UC_ResultView_Label_ClientSizeChanged(object sender, EventArgs e)
        {
            CreatButton();
        }
        public void SETDATA(int CamNum, int Index, bool result)
        {        
           if (result)
            {
                this.BTN_SET[Index].Invoke((MethodInvoker)delegate () {
                    this.BTN_SET[Index].BackColor = Color.Lime;
                });
            }
            else
            {
                this.BTN_SET[Index].Invoke((MethodInvoker)delegate () {
                    this.BTN_SET[Index].BackColor = Color.Red;
                });
            }

        }
        public void Clear()
        {
            int index = 0;
            foreach (var item in this.BTN_SET)
            {
               //item.BackColor = colors[index];

                item.Invoke((MethodInvoker)delegate () {
                    item.BackColor = colors[index];
                });
            }
        }

    }
}
