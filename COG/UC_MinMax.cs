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
    public partial class UC_MinMax : UserControl
    {
        public UC_MinMax()
        {
            InitializeComponent();
        }



       //public SpecData specData
       //{
       //    get
       //    {
       //        return GetSpecData();
       //    }
       //    set
       //    {
       //        SetSpecData(value);
       //    }
       //}
    
       public void SetSpecData(SpecData Other)
       {
           this.MaxValue = (int)Other.Max;
           this.MinValue = (int)Other.Min;
           this.Use = Other.Use;
       }
       public SpecData GetSpecData()
       {
           SpecData Other = new SpecData();
           Other.Max = this.MaxValue;
           Other.Min = this.MinValue;
           Other.Use = this.Use;
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
        public int MaxValue 
        { 
            get
            {
                return (int)this.NUD_MAX.Value;
            }
            
            set
            {
                this.NUD_MAX.Value = value;
            }
        }
        public int MinValue
        {
            get
            {
                return (int)this.NUD_MIN.Value;
            }

            set
            {
                this.NUD_MIN.Value = value;
            }
        }

        public bool Use
        {
            get
            {
                return this.CB_USE.Checked;
            }

            set
            {
                this.CB_USE.Checked = value;


            }
        }
        public bool VisibleMax
        {
            get
            {
                return this.NUD_MAX.Visible;
            }

            set
            {
                this.NUD_MAX.Visible = value;
            }
        }
        public bool VisibleMin
        {
            get
            {
                return this.NUD_MIN.Visible;
            }

            set
            {
                this.NUD_MIN.Visible = value;
            }
        }
        public bool VisibleUse
        {
            get
            {
                return this.CB_USE.Visible;
            }

            set
            {
                this.CB_USE.Visible = value;
            }
        }
        public Color ColorSet
        {
            get
            {
                return this.BackColor;
            }

            set
            {
                this.BackColor = value;
            }
        }

        private void CB_USE_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CB_USE.Checked)
            {
                this.CB_USE.BackColor = Color.Green;
            }
            else
            {
                this.CB_USE.BackColor = Color.Transparent;
            }
        }

        private void NUD_MAX_VisibleChanged(object sender, EventArgs e)
        {
            LB_MAX.Visible = NUD_MAX.Visible;
        }

        private void NUD_MIN_VisibleChanged(object sender, EventArgs e)
        {
            LB_MIN.Visible = NUD_MIN.Visible;
        }
    }





}
