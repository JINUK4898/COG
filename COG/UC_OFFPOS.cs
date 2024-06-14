using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COG
{
    public partial class UC_OFFPOS : UserControl
    {
        public UC_OFFPOS()
        {
            InitializeComponent();
        }


        public void SetOffsetPos(OffsetPos Other)
        {
            this.OffsetX_Value = Other.OFFSET_X;
            this.OffsetY_Value = (int)Other.OFFSET_Y;
            this.Width_Value = (int)Other.WIDTH;
            this.Height_Value = (int)Other.HEIGHT;
        }
        public OffsetPos GetOffsetPos()
        {
            OffsetPos Other = new OffsetPos();
            Other.OFFSET_X = this.OffsetX_Value;
            Other.OFFSET_Y = this.OffsetY_Value;
            Other.WIDTH = this.Width_Value;
            Other.HEIGHT = this.Height_Value;
            return Other;
        }

        public string NameValue
        {
            get
            {
                return this.LB_NAME.Text;
            }

            set
            {
                this.LB_NAME.Text = value;
            }
        }



        public double OffsetX_Value
        {
            get
            {
                return (double)this.NUD_OFF_X.Value;
            }

            set
            {
                this.NUD_OFF_X.Value = (decimal)value;
            }
        }

        public bool VisibleOffset_X
        {
            get
            {
                return this.NUD_OFF_X.Visible;
            }

            set
            {
                this.NUD_OFF_X.Visible = value;
                this.LB_X.Visible = value;
            }
        }


        public String TextOffset_X
        {
            get
            {
                return this.LB_X.Text;
            }

            set
            {
                this.LB_X.Text = value;
            }
        }
        public double OffsetX_Increment
        {
            get
            {
                return (double)this.NUD_OFF_X.Increment;
            }

            set
            {
                this.NUD_OFF_X.Increment = (decimal)value;
            }
        }






        public int OffsetY_Value
        {
            get
            {
                return (int)this.NUD_OFF_Y.Value;
            }

            set
            {
                this.NUD_OFF_Y.Value = value;

            }
        }
        public bool VisibleOffset_Y
        {
            get
            {
                return this.NUD_OFF_Y.Visible;
            }

            set
            {
                this.NUD_OFF_Y.Visible = value;
                this.LB_Y.Visible = value;
            }
        }
        public String TextOffset_Y
        {
            get
            {
                return this.LB_Y.Text;
            }

            set
            {
                this.LB_Y.Text = value;
            }
        }



        public int Width_Value
        {
            get
            {
                return (int)this.NUD_WIDTH.Value;
            }

            set
            {
                this.NUD_WIDTH.Value = value;
            }
        }
        public bool VisibleWidth
        {
            get
            {
                return this.NUD_WIDTH.Visible;
            }

            set
            {
                this.NUD_WIDTH.Visible = value;
                this.LB_W.Visible = value;
            }
        }
        public String TextWidth
        {
            get
            {
                return this.LB_W.Text;
            }

            set
            {
                this.LB_W.Text = value;
            }
        }




        public int Height_Value
        {
            get
            {
                return (int)this.NUD_HEIGHT.Value;
            }

            set
            {
                this.NUD_HEIGHT.Value = value;
            }
        }
        public bool VisibleHeight
        {
            get
            {
                return this.NUD_HEIGHT.Visible;
            }

            set
            {
                this.NUD_HEIGHT.Visible = value;
                this.LB_H.Visible = value;
            }
        }
        public String TextHeight
        {
            get
            {
                return this.LB_H.Text;
            }

            set
            {
                this.LB_H.Text = value;
            }
        }


    }
}
