using Newtonsoft.Json.Linq;
using nrt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Cognex.VisionPro;

namespace COG
{
    public partial class UC_NGVIEW : UserControl
    {
        public UC_NGVIEW()
        {
            InitializeComponent();

            foreach (var item in this.Controls)
            {
                if (item.GetType().Name.ToString() == "Label")
                {
                    colors.Add(((Label)item).BackColor);
                }
            }
        }
        
        private List<Color> colors = new List<Color>();


        bool controlPosSwap;
        public bool ControlPosSwap
        {
            get
            {
                return controlPosSwap;
            }
            set
            {
                controlPosSwap = value;
                this.ControlPostionSwap(value);
            }
        }

        private void ControlPostionSwap(bool Value)
        {
            if(Value)
            {
                ControlSwap(LB_SIDE_00 , LB_SIDE_01);
                ControlSwap(LB_SEG_TEXT_00, LB_SEG_TEXT_02);
                ControlSwap(LB_SEG_00, LB_SEG_02);
            }     

        }
        private void ControlSwap(Control control1 , Control control2)
        {
            System.Drawing.Point point1 = control1.Location;
            System.Drawing.Point point2 = control2.Location;

            control1.Location = new System.Drawing.Point(point2.X, point2.Y);
            control2.Location = new System.Drawing.Point(point1.X, point1.Y);
        }

        private void UpdateLabelColor(Label label, bool Value)
        {
            if (!Value)
                label.BackColor = Color.Red;
            //else
                //label.BackColor = Color.Green;
        }

        private void UpdateLabelText(Label label, double Value, Color color)
        {
            label.ForeColor = color;
            label.Text = Value.ToString();
        }
        private void UpdateLabelText(Label label, int Value , Color color )
        {
            label.ForeColor = color;
            label.Text = Value.ToString();
        }
        private void UpdateLabelText(Label label, string Value, Color color)
        {
            label.ForeColor = color;
            label.Text = Value.ToString();
        }
        private void UpdateLabelText(Label label, double Value)
        {
            //    Value = Math.Truncate(Value);
            label.Text = Value.ToString();
        }
        private void UpdateLabelText(Label label, int Value)
        {
            label.Text = Value.ToString();
        }
        private void UpdateLabelText(Label label, string Value)
        {
            label.Text = Value.ToString();
        }
        public void ClearControl()
        {
            int index = 0;
            foreach (var item in this.Controls) 
            {
                if(item.GetType().Name.ToString() == "Label")
                {
                    ((Label)item).BackColor = colors[index];
                    ((Label)item).Text = "";
                    index ++;
                }
            }
        }
        public void UpdateControl_OBD(bool Value)
        {
           UpdateLabelColor(LB_OBD0, Value);
        }

        public void UpdateControl_SEG1(int Value)
        {
            UpdateLabelText(LB_OBD0, Value);
        }
        public void UpdateControl_Percent_Carbon(PercentData ResultData)
        {
            foreach (var itemDetail in ResultData.Result.Select((value2, index2) => (value2, index2)))
            {
                var _Key = itemDetail.value2.Key;
                var _Value = itemDetail.value2.Value;
                if (_Key == 0)
                {
                    UpdateLabelColor(LB_SEG_00, _Value);

                    UpdateLabelText(LB_SEG_TEXT_00, ResultData.Data[_Key] , Color.White);
                }
                if (_Key == 1)
                {
                    UpdateLabelColor(LB_SEG_01, _Value);

                    UpdateLabelText(LB_SEG_TEXT_01, ResultData.Data[_Key], Color.White);
                }
                if (_Key == 2)
                {
                   UpdateLabelColor(LB_SEG_02, _Value);

                    UpdateLabelText(LB_SEG_TEXT_02, ResultData.Data[_Key], Color.White);
                }
            }
        }
        public void UpdateControl_Percent_CarbonALL(PercentData ResultData)
        {
            foreach (var itemDetail in ResultData.Result.Select((value2, index2) => (value2, index2)))
            {
                var _Key = itemDetail.value2.Key;
                var _Value = itemDetail.value2.Value;
                if (_Key == 0)
                {
                    UpdateLabelColor(LB_SEGALL_00, _Value);
                    UpdateLabelText(LB_SEGALL_00, ResultData.Data[_Key]);
                }
            }
        }

        public void UpdateControl_Percent_CarbontoCarbon(PercentData ResultData)
        {
            foreach (var itemDetail in ResultData.Result.Select((value2, index2) => (value2, index2)))
            {
                var _Key = itemDetail.value2.Key;
                var _Value = itemDetail.value2.Value;
                if (_Key == 0) 
                { 
                    UpdateLabelColor(LB_SEG2SEG_00_00, _Value);

                    UpdateLabelText(LB_SEG2SEG_00_00, ResultData.Data[_Key] );
                }
                if (_Key == 1) 
                {
                    UpdateLabelColor(LB_SEG2SEG_00_01, _Value);

                    UpdateLabelText(LB_SEG2SEG_00_01, ResultData.Data[_Key]);
                }
                if (_Key == 2) 
                {
                    UpdateLabelColor(LB_SEG2SEG_00_02, _Value);

                    UpdateLabelText(LB_SEG2SEG_00_02, ResultData.Data[_Key]);
                }
                if (_Key == 3)
                {
                    UpdateLabelColor(LB_SEG2SEG_00_03, _Value);

                    UpdateLabelText(LB_SEG2SEG_00_03, ResultData.Data[_Key]);
                }
            }
        }

        public void UpdateControl_Percent_CarbonSide(PercentData ResultData)
        {
            foreach (var itemDetail in ResultData.Result.Select((value2, index2) => (value2, index2)))
            {
                var _Key = itemDetail.value2.Key;
                var _Value = itemDetail.value2.Value;
                if (_Key == 0) 
                { 
                    UpdateLabelColor(LB_SIDE_00, _Value);

                    UpdateLabelText(LB_SIDE_00, ResultData.Data[_Key]);
                }
                if (_Key == 1) 
                {
                    UpdateLabelColor(LB_SIDE_01, _Value);
                    UpdateLabelText(LB_SIDE_01, ResultData.Data[_Key]);
                }
                if (_Key == 2) 
                {
                    UpdateLabelColor(LB_SIDE_02, _Value);
                    UpdateLabelText(LB_SIDE_02, ResultData.Data[_Key]);
                }
                if (_Key == 3) 
                {
                    UpdateLabelColor(LB_SIDE_03, _Value);
                    UpdateLabelText(LB_SIDE_03, ResultData.Data[_Key]);
                }

            }
        }

        public void SetResults(bool results)
        {
            UpdateLabelColor(LB_RESULT, results);

            if(results)
                UpdateLabelText(LB_RESULT, "OK");
            else
                UpdateLabelText(LB_RESULT, "NG");

        }
       


    }
}
