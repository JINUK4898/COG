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
    public partial class UC_AISetting : UserControl
    {
        public UC_AISetting()
        {
            InitializeComponent();
        }

        private InspectRunparam iNSPECT_RunParam = new InspectRunparam();


        public void SetRunparam(InspectRunparam Other)
        {
            if (Other == null)
                this.iNSPECT_RunParam = new InspectRunparam();
            else
                this.iNSPECT_RunParam = new InspectRunparam(Other);

            UpdateSetValue();
        }
        public InspectRunparam GetRunparam()
        {
            UpdateGetValue();
            return iNSPECT_RunParam;
        }

        private void UpdateGetValue()
        {
            iNSPECT_RunParam.GrayValueSpecCarbontoCarbon = uC_CarbonToCarGray.GetSpecData();
            iNSPECT_RunParam.PercentSpecCarbon[0] = uC_CarbonPercent_1.GetSpecData();
            iNSPECT_RunParam.PercentSpecCarbon[1] = uC_CarbonPercent_2.GetSpecData();
            iNSPECT_RunParam.PercentSpecCarbon[2] = uC_CarbonPercent_3.GetSpecData();
            iNSPECT_RunParam.PercentSpecCarbonALL[0] = uC_CarbonallPercent_1.GetSpecData();

            iNSPECT_RunParam.PercentSpecCarbontoCarbon[0] = uC_CarbonToCarPercent_1.GetSpecData();
            iNSPECT_RunParam.PercentSpecCarbontoCarbon[0].SetOffset(uC_CarbonToCar_OFFPOS1.GetOffsetPos());

            iNSPECT_RunParam.PercentSpecCarbontoCarbon[1] = uC_CarbonToCarPercent_2.GetSpecData();
            iNSPECT_RunParam.PercentSpecCarbontoCarbon[1].SetOffset(uC_CarbonToCar_OFFPOS2.GetOffsetPos());

            iNSPECT_RunParam.Carbon_AreaSpec[0] = uC_CarbonArea_1.GetSpecData();
            iNSPECT_RunParam.Carbon_AreaSpec[1] = uC_CarbonArea_2.GetSpecData();
            iNSPECT_RunParam.Carbon_AreaSpec[2] = uC_CarbonArea_3.GetSpecData();


            iNSPECT_RunParam.RunParam_OBD.SpecWidth = uC_OBD_TOOL_RUNPARAM_WIDTH.GetSpecData();
            iNSPECT_RunParam.RunParam_CARBON.SpecArea = uC_CARBON_TOOL_RUNPARAM_AREA.GetSpecData();




            iNSPECT_RunParam.PercentSpecCarbonSide[0] = uC_CarbonSidePercent_1.GetSpecData();
            iNSPECT_RunParam.PercentSpecCarbonSide[0].SetOffset(uC_CarbonSide_Offset_1.GetOffsetPos());

            iNSPECT_RunParam.PercentSpecCarbonSide[1] = uC_CarbonSidePercent_2.GetSpecData();
            iNSPECT_RunParam.PercentSpecCarbonSide[1].SetOffset(uC_CarbonSide_Offset_2.GetOffsetPos());

            iNSPECT_RunParam.PercentSpecCarbonSide[2] = uC_CarbonSidePercent_3.GetSpecData();
            iNSPECT_RunParam.PercentSpecCarbonSide[2].SetOffset(uC_CarbonSide_Offset_3.GetOffsetPos());

            iNSPECT_RunParam.PercentSpecCarbonSide[3] = uC_CarbonSidePercent_4.GetSpecData();
            iNSPECT_RunParam.PercentSpecCarbonSide[3].SetOffset(uC_CarbonSide_Offset_4.GetOffsetPos());




        }
        private void UpdateSetValue()
        {
             uC_CarbonToCarGray.SetSpecData(iNSPECT_RunParam.GrayValueSpecCarbontoCarbon);
             uC_CarbonPercent_1.SetSpecData(iNSPECT_RunParam.PercentSpecCarbon[0]);
             uC_CarbonPercent_2.SetSpecData(iNSPECT_RunParam.PercentSpecCarbon[1]);
             uC_CarbonPercent_3.SetSpecData(iNSPECT_RunParam.PercentSpecCarbon[2]);
             uC_CarbonallPercent_1.SetSpecData(iNSPECT_RunParam.PercentSpecCarbonALL[0]);


             uC_CarbonToCarPercent_1.SetSpecData(iNSPECT_RunParam.PercentSpecCarbontoCarbon[0]);
             uC_CarbonToCar_OFFPOS1.SetOffsetPos(iNSPECT_RunParam.PercentSpecCarbontoCarbon[0].GetOffset());


            uC_CarbonToCarPercent_2.SetSpecData(iNSPECT_RunParam.PercentSpecCarbontoCarbon[1]);
            uC_CarbonToCar_OFFPOS2.SetOffsetPos(iNSPECT_RunParam.PercentSpecCarbontoCarbon[1].GetOffset());

            uC_CarbonArea_1.SetSpecData(iNSPECT_RunParam.Carbon_AreaSpec[0]);
             uC_CarbonArea_2.SetSpecData(iNSPECT_RunParam.Carbon_AreaSpec[1]);
             uC_CarbonArea_3.SetSpecData(iNSPECT_RunParam.Carbon_AreaSpec[2]);

             uC_OBD_TOOL_RUNPARAM_WIDTH.SetSpecData(iNSPECT_RunParam.RunParam_OBD.SpecWidth);
             uC_CARBON_TOOL_RUNPARAM_AREA.SetSpecData(iNSPECT_RunParam.RunParam_CARBON.SpecArea);
             uC_SOLUTION_TOOL_RUNPARAM_AREA.SetSpecData(iNSPECT_RunParam.RunParam_SOLUTION.SpecArea);



            uC_CarbonSidePercent_1.SetSpecData(iNSPECT_RunParam.PercentSpecCarbonSide[0]);
            uC_CarbonSide_Offset_1.SetOffsetPos(iNSPECT_RunParam.PercentSpecCarbonSide[0].GetOffset());

            uC_CarbonSidePercent_2.SetSpecData(iNSPECT_RunParam.PercentSpecCarbonSide[1]);
            uC_CarbonSide_Offset_2.SetOffsetPos(iNSPECT_RunParam.PercentSpecCarbonSide[1].GetOffset());

            uC_CarbonSidePercent_3.SetSpecData(iNSPECT_RunParam.PercentSpecCarbonSide[2]);
            uC_CarbonSide_Offset_3.SetOffsetPos(iNSPECT_RunParam.PercentSpecCarbonSide[2].GetOffset());

            uC_CarbonSidePercent_4.SetSpecData(iNSPECT_RunParam.PercentSpecCarbonSide[3]);
            uC_CarbonSide_Offset_4.SetOffsetPos(iNSPECT_RunParam.PercentSpecCarbonSide[3].GetOffset());


        }
    }
}
