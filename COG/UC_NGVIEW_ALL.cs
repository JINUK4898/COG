using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;

namespace COG
{
    public partial class UC_NGVIEW_ALL : UserControl
    {
        public UC_NGVIEW_ALL()
        {
            InitializeComponent();

            uC_NGVIEW.Add(uC_NGVIEW1);
            uC_NGVIEW.Add(uC_NGVIEW2);
            uC_NGVIEW.Add(uC_NGVIEW3);
            uC_NGVIEW.Add(uC_NGVIEW4);
        }


        private CogRecordDisplay CogRecordDisplay = new CogRecordDisplay();
        private List<UC_NGVIEW> uC_NGVIEW = new List<UC_NGVIEW>();


        public void UpdateControl_OBD(INSPECT iNSPECT, int indexNum)
        {


            try
            {
                if (iNSPECT.CARBON_OBD.ResultParam[indexNum].ResultOBD.Count > 0)
                {
                    uC_NGVIEW1.UpdateControl_OBD(iNSPECT.CARBON_OBD.ResultParam[indexNum].ResultOBD[0]);
                    uC_NGVIEW2.UpdateControl_OBD(iNSPECT.CARBON_OBD.ResultParam[indexNum].ResultOBD[1]);
                    uC_NGVIEW3.UpdateControl_OBD(iNSPECT.CARBON_OBD.ResultParam[indexNum].ResultOBD[2]);
                    uC_NGVIEW4.UpdateControl_OBD(iNSPECT.CARBON_OBD.ResultParam[indexNum].ResultOBD[3]);
                }
            }
            catch
            {

            }
        }
        public void UpdateControl_SEG1(INSPECT iNSPECT, int indexNum)
        {
            var returnValue = iNSPECT.CARBON_SEG.ResultParam[indexNum].GetBlobCount();

            foreach (var item in returnValue.Select((value, index) => (value, index)))
            {
                if (item.index == 0) uC_NGVIEW1.UpdateControl_SEG1(item.value);
                if (item.index == 1) uC_NGVIEW2.UpdateControl_SEG1(item.value);
                if (item.index == 2) uC_NGVIEW3.UpdateControl_SEG1(item.value);
                if (item.index == 3) uC_NGVIEW4.UpdateControl_SEG1(item.value);
            }
        }
        public void UpdateControl_Percent(INSPECT iNSPECT, int indexNum)
        {
            var returnValue = iNSPECT.ResultParam[indexNum];
            UpdateControl_Percent(returnValue , indexNum);
        }
        public void UpdateControl_Percent(InspectResultData iNSPECT_ResultParam, int indexNum)
        {
            var returnValue = iNSPECT_ResultParam; //  iNSPECT.ResultParam[indexNum];

            foreach (var item in returnValue.Percent_Carbon.Select((value, index) => (value, index)))
            {
                foreach (var itemDetail in item.value.Data.Select((value2, index2) => (value2, index2)))
                {
                    var _Key = itemDetail.value2.Key;
                    var _Value = itemDetail.value2.Value;

                    uC_NGVIEW[item.index].UpdateControl_Percent_Carbon(item.value);
                }
            }

            foreach (var item in returnValue.Percent_CarbonAll.Select((value, index) => (value, index)))
            {
                foreach (var itemDetail in item.value.Data.Select((value2, index2) => (value2, index2)))
                {
                    var _Key = itemDetail.value2.Key;
                    var _Value = itemDetail.value2.Value;

                    uC_NGVIEW[item.index].UpdateControl_Percent_CarbonALL(item.value);

                }
            }

            foreach (var item in returnValue.Percent_CarbontoCarbon.Select((value, index) => (value, index)))
            {
                foreach (var itemDetail in item.value.Data.Select((value2, index2) => (value2, index2)))
                {
                    var _Key = itemDetail.value2.Key;
                    var _Value = itemDetail.value2.Value;

                    uC_NGVIEW[item.index].UpdateControl_Percent_CarbontoCarbon(item.value);

                }
            }

            foreach (var item in returnValue.Percent_CarbonSide.Select((value, index) => (value, index)))
            {
                foreach (var itemDetail in item.value.Data.Select((value2, index2) => (value2, index2)))
                {
                    var _Key = itemDetail.value2.Key;
                    var _Value = itemDetail.value2.Value;

                    uC_NGVIEW[item.index].UpdateControl_Percent_CarbonSide(item.value);

                }
            }

        }
        public void SetResults(bool[] results , int ErrorCode = 0)
        {
            foreach (var item in results.Select((value, index) => (value, index)))
            {
                uC_NGVIEW[item.index].SetResults(item.value);

            }

            LB_ERRORCODE.Text = ErrorCode.ToString();
        }

        public void SetDisplay(CogRecordDisplay cogRecordDisplay)
        {
            CogRecordDisplay = cogRecordDisplay;
        }
        public void ClearControl()
        {
            uC_NGVIEW1.ClearControl();
            uC_NGVIEW2.ClearControl();
            uC_NGVIEW3.ClearControl();
            uC_NGVIEW4.ClearControl();
        }
        public void UC_NGVIEW_ALL_Load(INSPECT iNSPECT, int indexNum)
        {
            ClearControl();

            UpdateControl_OBD(iNSPECT,indexNum);
            UpdateControl_SEG1(iNSPECT,indexNum);
            UpdateControl_Percent(iNSPECT,indexNum);
        }
        public void UC_NGVIEW_ALL_Load(InspectResultData iNSPECT_ResultParam, int indexNum)
        {
            ClearControl();
            UpdateControl_Percent(iNSPECT_ResultParam, indexNum);
        }
        private void Display_Pan(CogRecordDisplay cogRecordDisplay1, int num)
        {
            try
            {
                cogRecordDisplay1.Zoom = 2.5;

                switch (num)
                {
                    case 0:
                        cogRecordDisplay1.PanX = 130;
                        cogRecordDisplay1.PanY = 100;
                        break;
                    case 1:
                        cogRecordDisplay1.PanX = -100;
                        cogRecordDisplay1.PanY = 100;
                        break;
                    case 2:
                        cogRecordDisplay1.PanX = 130;
                        cogRecordDisplay1.PanY = -110;
                        break;
                    case 3:
                        cogRecordDisplay1.PanX = -100;
                        cogRecordDisplay1.PanY = -110;
                        break;
                }


            }
            catch (System.Exception ex)
            {

            }

        }
        private void uC_NGVIEW1_Click(object sender, EventArgs e)
        {
            Display_Pan(CogRecordDisplay, 0);
        }

        private void uC_NGVIEW2_Click(object sender, EventArgs e)
        {

            Display_Pan(CogRecordDisplay, 1);
        }

        private void uC_NGVIEW3_Click(object sender, EventArgs e)
        {

            Display_Pan(CogRecordDisplay, 2);
        }

        private void uC_NGVIEW4_Click(object sender, EventArgs e)
        {

            Display_Pan(CogRecordDisplay, 3);
        }
    }
}
