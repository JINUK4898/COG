using Cognex.VisionPro;
using Cognex.VisionPro.Exceptions;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.SearchMax;
using CognexAI;
using nrt;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using ViDi2;
using ViDi2.VisionPro;
using static OpenCvSharp.Stitcher;
using Exception = System.Exception;

namespace COG
{

    [Serializable]
    public partial class InspectRunparam 
    {

        public InspectRunparam() 
        {
            Clear();



            if (Main.DEFINE.OPEN_F)
            {
                this.GrayValueSpecCarbontoCarbon.Max = 235;

                this.OBD_CountSpec = 4;
                this.Carbon_CountSpec = 3;


                this.PercentSpecCarbon[0].Max = 50;
                this.PercentSpecCarbon[1].Max = 80;
                this.PercentSpecCarbon[2].Max = 50;


                this.PercentSpecCarbonALL[0].Max = 70;

                this.PercentSpecCarbontoCarbon[0].Max = 50;
                this.PercentSpecCarbontoCarbon[1].Max = 50;

            }
        }
        public InspectRunparam(InspectRunparam Other)
        {
            Clear();



            this.OBD_CountSpec = Other.OBD_CountSpec;
            this.Carbon_CountSpec = Other.Carbon_CountSpec;


            this.Carbon_AreaSpec = new SpecData[Other.Carbon_AreaSpec.Count()];
            foreach (var item in Other.Carbon_AreaSpec.Select((value,Index) => (value, Index)))
            {
                this.Carbon_AreaSpec[item.Index] = new SpecData(item.value);
            }

            this.PercentSpecCarbon = new SpecData[Other.PercentSpecCarbon.Count()];
            foreach (var item in Other.PercentSpecCarbon.Select((value, Index) => (value, Index)))
            {
                this.PercentSpecCarbon[item.Index] = new SpecData(item.value);
            }

            this.PercentSpecCarbonALL = new SpecData[Other.PercentSpecCarbonALL.Count()];
            foreach (var item in Other.PercentSpecCarbonALL.Select((value, Index) => (value, Index)))
            {
                this.PercentSpecCarbonALL[item.Index] = new SpecData(item.value);
            }

            this.PercentSpecCarbontoCarbon = new SpecData[Other.PercentSpecCarbontoCarbon.Count()];
            foreach (var item in Other.PercentSpecCarbontoCarbon.Select((value, Index) => (value, Index)))
            {
                this.PercentSpecCarbontoCarbon[item.Index] = new SpecData(item.value);
            }

            this.PercentSpecCarbonSide = new SpecData[Other.PercentSpecCarbonSide.Count()];
            foreach (var item in Other.PercentSpecCarbonSide.Select((value, Index) => (value, Index)))
            {
                this.PercentSpecCarbonSide[item.Index] = new SpecData(item.value);
            }



            this.GrayValueSpecCarbontoCarbon = new SpecData(Other.GrayValueSpecCarbontoCarbon);


            this.RunParam_OBD = new JasAIRunparam(Other.RunParam_OBD);

            this.RunParam_CARBON = new JasAIRunparam(Other.RunParam_CARBON);

            this.RunParam_SOLUTION = new JasAIRunparam(Other.RunParam_SOLUTION);
        }
        public void Clear()
        {
            this.OBD_CountSpec = 4;
            this.Carbon_CountSpec = 3;

            this.Carbon_AreaSpec = new SpecData[3];
            this.PercentSpecCarbon = new SpecData[3];
            this.PercentSpecCarbonALL = new SpecData[1];
            this.PercentSpecCarbontoCarbon = new SpecData[2];
            this.PercentSpecCarbonSide = new SpecData[4];

            foreach (var item in this.Carbon_AreaSpec.Select((value, index) => (value, index)))
            {
                this.Carbon_AreaSpec[item.index] = new SpecData();
            }
            foreach (var item in this.PercentSpecCarbon.Select((value, index) => (value, index)))
            {
                this.PercentSpecCarbon[item.index] = new SpecData();
            }
            foreach (var item in this.PercentSpecCarbonALL.Select((value, index) => (value, index)))
            {
                this.PercentSpecCarbonALL[item.index] = new SpecData();
            }
            foreach (var item in this.PercentSpecCarbontoCarbon.Select((value, index) => (value, index)))
            {
                this.PercentSpecCarbontoCarbon[item.index] = new SpecData();
            }


            foreach (var item in this.PercentSpecCarbonSide.Select((value, index) => (value, index)))
            {
                this.PercentSpecCarbonSide[item.index] = new SpecData();
            }
            



            this.GrayValueSpecCarbontoCarbon = new SpecData();


            this.RunParam_OBD.Clear();
            this.RunParam_CARBON.Clear();
            this.RunParam_SOLUTION.Clear();

        }

        public JasAIRunparam RunParam_OBD { get; set; } = new JasAIRunparam();
        public JasAIRunparam RunParam_CARBON     { get; set; }= new JasAIRunparam();
        public JasAIRunparam RunParam_SOLUTION   { get; set; }= new JasAIRunparam();

        //해당값을 기준으로 화면을 Y방향으로 등분함. 
        //카본 사이사이에 용액빠짐을 검색하기 위함임. 
        public int BetweenBoxsDIV 
        { 
            get
            {
               return (int) this.RunParam_CARBON.CarbonToCarbon_DIV.Max;
            }
        }

        /// <summary>
        /// OBD Tool로 찾을 수량에 대한 스펙  OK = 4개
        /// </summary>
        public int OBD_CountSpec { get; set; } = new int();

        /// <summary>
        /// CarbonTool의 카본 갯수 3개
        /// </summary>
        public int Carbon_CountSpec { get; set; } = new int();





        /// <summary>
        /// 찾은 Carbon의 영역을 기준으로 NG구분
        /// </summary>
        public SpecData[] Carbon_AreaSpec { get; set; } = new SpecData[3];

        /// <summary>
        /// 카본위에 용액이 차지하는 비율에 대한 SPEC값
        /// </summary>
        public SpecData[] PercentSpecCarbon { get; set; } = new SpecData[3];

        /// <summary>
        /// 카본3개합 사각형의 용액이 차지하는 비율에 대한 SPEC값
        /// </summary>
        public SpecData[] PercentSpecCarbonALL { get; set; } = new SpecData[1];

        /// <summary>
        /// 카본사이사이 영역의 용액이 차지하는 비율에 대한 SPEC값
        /// </summary>
        public SpecData[] PercentSpecCarbontoCarbon { get; set; } = new SpecData[2];

        /// <summary>
        /// 카본Side 4개 영역의 용액이 차지하는 비율에 대한 SPEC값
        /// </summary>
        public SpecData[] PercentSpecCarbonSide { get; set; } = new SpecData[4];




        /// <summary>
        /// 카본사이에 용액 검사시 Blob으로 검사시 
        ///해당 그레이값 밑으로는 검은색(0)으로 처리함 
        /// </summary>
        public SpecData GrayValueSpecCarbontoCarbon { get; set; } = new SpecData();

    }

    [Serializable]
    public class SpecData
    {
        public  SpecData()
        {
            this.Max = 0;
            this.Min = 0;
            this.Use = false;
            this.Standard = 0;
            this.Tolerance = 0;
            this.Offset = new OffsetPos();
        }
        public SpecData(SpecData Other)
        {
            if (Other == null) return;
            this.Max = Other.Max;
            this.Min = Other.Min;
            this.Use = Other.Use;
            this.Tolerance=Other.Tolerance;
            this.Standard =Other.Standard;
            this.Offset = new OffsetPos(Other.Offset);
        }
        public void SetOffset(OffsetPos Other)
        {
            this.Offset = new OffsetPos(Other);
        }
        public OffsetPos GetOffset()
        {
            return this.Offset;
        }

        public double Max { get; set; } = new double();
        public double Min { get; set; } = new double();

        public double Standard { get; set; } = new double();
        public double Tolerance { get; set; } = new double();
        public bool Use { get; set; } = new bool();

        public OffsetPos Offset { get; set; } = new OffsetPos();
    }

    [Serializable]
    public class OffsetPos
    {
        public OffsetPos()
        {
            this.OFFSET_X = 0;
            this.OFFSET_Y = 0;
            this.WIDTH = 0;
            this.HEIGHT = 0;
        }
        public OffsetPos(OffsetPos Other)
        {
            if (Other == null) return;
            
                this.OFFSET_X = Other.OFFSET_X;
                this.OFFSET_Y = Other.OFFSET_Y;
                this.WIDTH = Other.WIDTH;
                this.HEIGHT = Other.HEIGHT;
            
        }
        public double OFFSET_X { get; set; } = new double();
        public double OFFSET_Y { get; set; } = new double();
        public double WIDTH { get; set; } = new double();
        public double HEIGHT { get; set; } = new double();
    }


    [Serializable]
    public class PercentData
    {
        public PercentData() 
        {
            Clear();
        }
        public PercentData(PercentData Other)
        {
            Clear();

            foreach(var item in Other.Data)
            {
                this.Data.Add(item.Key,item.Value);
            }

            foreach(var item in Other.Result)
            {
                this.Result.Add(item.Key,item.Value);
            }
        }

        public void Clear()
        {
            this.Data.Clear();
            this.Result.Clear();
        }


        /// <summary>
        /// (인덱스 , 측정된 % 데이터)
        /// </summary>
        public SortedList<int,double> Data { get; set; } = new SortedList<int, double>();
        public SortedList<int, bool> Result { get; set; } = new SortedList< int, bool>();

    }


    [Serializable]
    public partial class InspectResultData
    {
        public InspectResultData()
        {
            this.Clear();
        }

        public  void Clear()
        {
            Get_BoxsCarbonAll.Clear();
            Get_BoxsCarbontoCarbon.Clear();
            Get_BoxsCarbonSide.Clear();

            this.Result.Clear();
            this.Result = new List<bool>();

            this.OBD_BoolResults.Clear();
            this.OBD_BoolResults = new List<bool>();

            foreach (var item in this.Percent_Carbon.Select((value, index) => (value, index)))
            {
                if (this.Percent_Carbon[item.index] == null) this.Percent_Carbon[item.index] = new PercentData();
                this.Percent_Carbon[item.index].Clear();
            }

            foreach (var item in this.Percent_CarbonAll.Select((value, index) => (value, index)))
            {
                if (this.Percent_CarbonAll[item.index] == null) this.Percent_CarbonAll[item.index] = new PercentData();
                this.Percent_CarbonAll[item.index].Clear();
            }

            foreach (var item in this.Percent_CarbontoCarbon.Select((value, index) => (value, index)))
            {
                if (this.Percent_CarbontoCarbon[item.index] == null) this.Percent_CarbontoCarbon[item.index] = new PercentData();
                this.Percent_CarbontoCarbon[item.index].Clear();
            }

            foreach (var item in this.Percent_CarbonSide.Select((value, index) => (value, index)))
            {
                if (this.Percent_CarbonSide[item.index] == null) this.Percent_CarbonSide[item.index] = new PercentData();
                this.Percent_CarbonSide[item.index].Clear();
            }

            BlobGraphic.Clear();
            this.BlobResult.Clear();

        }

        /// <summary>
        /// 카본위에 용액이 차지하는 비율에 대한 결과값 
        /// </summary>
        public PercentData[] Percent_Carbon { get; set; } = new PercentData[4];

        /// <summary>
        /// 카본3개에 대한 사각박스 에 대한 검사 
        /// |----------------------|
        /// | 1        2         3 |
        /// |----------------------|
        /// </summary>
        public PercentData[] Percent_CarbonAll { get; set; } = new PercentData[4];

        /// <summary>
        /// 카본 사이 사이에 대한 영역 검사 
        /// </summary>
        public PercentData[] Percent_CarbontoCarbon { get; set; } = new PercentData[4];


        /// <summary>
        /// 카본 Side에 대한 영역 검사 
        /// </summary>
        public PercentData[] Percent_CarbonSide { get; set; } = new PercentData[4];




        /// <summary>
        /// OBD Tool로 찾은 수량
        /// </summary>
        public int OBD_CountResultsOK
        {
            get
            {
                return this.OBD_BoolResults.Count(vlaue => vlaue == true);
            }
        }
        public List<bool> OBD_BoolResults { get; set; } = new List<bool>();


#if false
        /// <summary>
        /// 카본3개에 대한 사각박스 에 대한 검사 
        /// |----------------------|
        /// | 1        2         3 |
        /// |----------------------|
        /// </summary>
        public List<List<double>> PercentResultCarbonAll { get; set; } = new List<List<double>>();
        public List<bool> PercentResultCarbonAllBool { get; set; } = new List<bool>();



        /// <summary>
        /// 카본 사이 사이에 대한 영역 검사 
        /// </summary>
        public List<List<double>> PercentResultCarbontoCarbon { get; set; } = new List<List<double>>();
        public List<bool> PercentResultCarbontoCarbonBool { get; set; } = new List<bool>();

#endif




        /// <summary>
        ///  모든 결과값 통칭해서  리턴. 
        /// </summary>
        public List<bool> Result { get; set; } = new List<bool>();
        public int ResultNGCount
        {
            get
            {
                return this.Result.Count(vlaue => vlaue == false);
            }
        }

        public List<bool> BlobResult { get; set; } = new List<bool>();

        /// <summary>
        /// 4개의 카본에 대한 전체 결과 NG , OK 
        /// </summary>
        public bool ResultALL
        {
            get
            {
                if (ErrorCodeDL() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        [NonSerialized]
        /// <summary>
        /// 카본 영역 3개와 3개를 합칙 박스 영역 하나. 
        /// </summary>
        /// 
        public List<List<BoundingBox>>          Get_BoxsCarbonAll           = new List<List<BoundingBox>>();

        [NonSerialized]
        /// <summary>
        /// 카본 사이의 영역을 나누어서 가져온다. 
        /// </summary>
        public List<List<List<BoundingBox>>>    Get_BoxsCarbontoCarbon    = new List<List<List<BoundingBox>>>();

        [NonSerialized]
        /// <summary>
        /// 카본 영역 3개와 3개를 합칙 박스 영역 하나. 
        /// </summary>
        public List<List<List<BoundingBox>>> Get_BoxsCarbonSide = new List<List<List<BoundingBox>>>();


        public int Cam_num { get; set; } = 0;

        public int ErrorCodeBlob()
        {
            int nErrCode = 0;

            for (int i = 0; i < this.BlobResult.Count; i++)
            {
                if (!this.BlobResult[i]) // If the data point is not OK (i.e., NG)
                {
                    nErrCode |= 0x01 << i;
                }
            }
            return nErrCode;
        }

        public int ErrorCodeDL()
        {
            int nErrCode = 0;


            bool UseBlobResult = false;
            if (this.BlobResult != null)
            {
                if (this.BlobResult.Count > 0)
                {
                    UseBlobResult = true;
                }
            }

            if (!UseBlobResult)
            {
                for (int i = 0; i < this.Result.Count; i++)
                {
                    if (!this.Result[i]) // If the data point is not OK (i.e., NG)
                    {
                        nErrCode |= 0x01 << i;
                    }
                }
            }
            else
            {

                try
                {
                    List<bool> ResultPoint = new List<bool>();

                    for (int i = 0; i < 4; i++)// 카본갯수
                    {
                        if (this.Result[i] && this.BlobResult[i])
                        {
                            ResultPoint.Add(true);
                        }
                        else
                        {
                            ResultPoint.Add(false);
                        }

                    }
                    for (int i = 0; i < ResultPoint.Count; i++)
                    {
                        if (!ResultPoint[i]) // If the data point is not OK (i.e., NG)
                        {
                            nErrCode |= 0x01 << i;
                        }
                    }
                }
                catch
                {

                }
            }
            return nErrCode;
        }

        public int ErrorCodeSum()
        {
            int nErrCode = 0;
            try
            {
                List<bool> ResultPoint = new List<bool>();

                for (int i = 0; i < 4; i++)// 카본갯수
                {
                    if (this.Result[i] && this.BlobResult[i])
                    {
                        ResultPoint.Add(true);
                    }
                    else
                    {
                        ResultPoint.Add(false);
                    }

                }
                for (int i = 0; i < ResultPoint.Count; i++)
                {
                    if (!ResultPoint[i]) // If the data point is not OK (i.e., NG)
                    {
                        nErrCode |= 0x01 << i;
                    }
                }
            }
            catch
            {

            }

            return nErrCode;
        }
        
        public  CogGraphicCollection BlobGraphic { get; set; } = new CogGraphicCollection();
    }

    public abstract class VisionAlgorithms:IDisposable
    {
        public void Initial()
        {
            this.RunParam = new InspectRunparam();
            for (int i = 0; i < this.ResultParam.Count(); i++)
            {
                this.ResultParam[i] = new InspectResultData();
            }
        }
        public  void Clear()
        {
            this.RunParam.Clear();
           
            foreach(var item in this.ResultParam)
            {
                item.Clear();
            }
        }


        public abstract void SetImage_OBD(object InputImage);
        public abstract void Run(int indexNum);
        public abstract void RunTools(int indexNum);

        // public abstract void RunOnly(int indexNum);
        public abstract void Getresult(int indexNum);


        //    public ICogImage TempImage;





#if false
        public NeurocleAITool CARBON_OBD
        {
            get
            {
                return Main.AlignUnit[0].DETECTION_CARBON_Tool;
            }
        }
        public NeurocleAITool CARBON_SEG
        {
            get
            {
                return Main.AlignUnit[0].SEGMENTATION_CARBON_Tool;
            }
        }
        public NeurocleAITool SOLUTION_SEG
        {
            get
            {
                return Main.AlignUnit[0].SEGMENTATION_SOLUTION_Tool;
            }
        }
#else
        public NeurocleAITool CARBON_OBD
        {
            get
            {
                return Form1.NeurocleTool0;
            }
        }
        public NeurocleAITool CARBON_SEG
        {
            get
            {
                return Form1.NeurocleTool1;
            }
        }
        public NeurocleAITool SOLUTION_SEG
        {
            get
            {
                return Form1.NeurocleTool2;
            }
        }
#endif
        public CognexAI.CognexAIControl VProAIControl = new CognexAI.CognexAIControl();

        private static Main.MTickTimer m_Timer_ALL = new Main.MTickTimer();


        /// <summary>
        /// 공통 항목이나, 두개 이상에 툴에 대한 파라미터에 대한 값 조절 
        /// </summary>
        public  InspectRunparam RunParam { get; set; } = new InspectRunparam();

        public InspectResultData[] ResultParam { get; set; } = new InspectResultData[Main.ToolMaxCount];

        public class Seq
        {

            public const int OK_SEQ = 100;
            public const int NG_SEQ = 200;

            public const int COMPLET_SEQ    = 1000;
        }

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.Clear();

            //        VisionProHelper.DisposeICogmage(this.TempImage);

                    if (this.CARBON_OBD != null)
                        this.CARBON_OBD.Dispose();

                    if (this.CARBON_SEG != null)
                        this.CARBON_SEG.Dispose();

                    if (this.SOLUTION_SEG != null)
                        this.SOLUTION_SEG.Dispose();             
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
            }
        }

        // // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        // ~INSPECT()
        // {
        //     // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public class INSPECT : VisionAlgorithms 
    {

        public INSPECT()
        {
            this.Initial();            
        }
        
        public void CalculationPercentage(int indexNum, JasAIResultparam _SolutionResultParam, JasAIResultparam _CarbonResultParam)
        {
            try
            {
                List<bool> returnbool = new List<bool>();
                //int RunCount = (int)_SolutionResultParam.Input.Region.Count();
           


                var OBDInfo = _SolutionResultParam.Boxs
                                .GroupBy(x => x.Index_OBD)
                                .Select(group => new { Index_OBD = group.Key, Count = group.Count() });
                int RunCount = OBDInfo.Count();



          //      int RunCount = (int)_SolutionResultParam.Input.InputOBDCount;

                for (int i = 0; i < RunCount; i++)
                {
                    List<double> returnValueCarbon = new List<double>();
                    List<double> returnValueCarbonAll = new List<double>();
                    List<double> returnValueCarbontoCarbon = new List<double>();
                    List<double> returnValueCarbonSide = new List<double>();
                    
                    bool result = true;

                    //int Index_OBD = _SolutionResultParam.Input.Region[i].RegionBox.Index_OBD;

                    int Index_OBD = OBDInfo.ElementAt(i).Index_OBD;


                    


                    if (_SolutionResultParam.Boxs.Count > 0)
                    {
                        BoundingBox InputRegion = _SolutionResultParam.Input.Region[0].RegionBox;

                        try
                        {
                            #region 
                            List<BoundingBox> Solutions = new List<BoundingBox>();
                            foreach (var item in _SolutionResultParam.Boxs.Select((value, index) => (value, index)))
                            {
                                if (Index_OBD == item.value.Index_OBD)
                                {
                                    Solutions.Add(new BoundingBox(item.value));
                                }
                            }

                            List<BoundingBox> Carbons = new List<BoundingBox>();
                            foreach (var item in _CarbonResultParam.Boxs.Select((value, index) => (value, index)))
                            {
                                if (Index_OBD == item.value.Index_OBD)
                                {
                                    Carbons.Add(new BoundingBox(item.value));
                                }
                            }

                            // 01.용액 이미지 생성 
                            Mat SolutionImage = new Mat((int)InputRegion.Height, (int)InputRegion.Width, MatType.CV_8U, Scalar.All(0));

                            /// 용액의 영역 부분에 255값을 넣어 위치를 표시 한다. 
                            /// 카본의 비교 하기 위한 기준 이미지 생성 한다고 보면 됨 
                            foreach (var Solutionitem in Solutions.Select((value, Index) => (value, Index)))
                            {
                                var contour = Solutionitem.value.RegionPoint;
                                Cv2.DrawContours(SolutionImage, new List<List<OpenCvSharp.Point>> { contour }, -1, Scalar.All(255), -1);
                                //Cv2.ImShow("SolutionImage:" + Solutionitem.Index, SolutionImage);

                            }
                            #endregion

                            if (RunParam.PercentSpecCarbon.Count(value => value.Use == true) > 0)
                            {
                                ///02. Carbon의 영역을 갯수에 맞게 넣으면서 오버랩 되는 부분 영역의 값을 추출 하기. 
                                foreach (var Carbonitem in Carbons.Select((value, Index) => (value, Index)))
                                {
                                    int Mod = Carbonitem.Index;// % RunParam.Carbon_CountSpec;

                                    if (RunParam.PercentSpecCarbon[Mod].Use)
                                    {

                                        Mat CarbonImage = new Mat((int)InputRegion.Height, (int)InputRegion.Width, MatType.CV_8U, Scalar.All(0));
                                        var contour = Carbonitem.value.RegionPoint;
                                        Cv2.DrawContours(CarbonImage, new List<List<OpenCvSharp.Point>> { contour }, -1, Scalar.All(255), -1);

                                        Mat OverLapImage = new Mat();
                                        Cv2.BitwiseAnd(SolutionImage, CarbonImage, OverLapImage);
                                        //Cv2.ImShow("IntersectionImage:" + Carbonitem.Index, OverLapImage);


                                        // 겹치는 영역의 비율 구하기
                                        double Carbon_ZeroArea = Cv2.CountNonZero(CarbonImage);
                                        double OverLapArea = Cv2.CountNonZero(OverLapImage);
                                        double overlapRatio = Math.Round(OverLapArea / Carbon_ZeroArea * 100.0, 0);
                                        returnValueCarbon.Add(overlapRatio);
                                        ResultParam[indexNum].Percent_Carbon[Index_OBD].Data.Add(Carbonitem.Index, overlapRatio);

                                        //----------------------------------------------------------------------------------------------------------------------
                                        bool tempret = true;


                                        if (Mod < RunParam.Carbon_CountSpec) //Carbon 123 | 321 각각의 영역 스펙에 대한  NG 판정 하기 
                                        {
                                            if (RunParam.PercentSpecCarbon[Mod].Use)
                                            {
                                                if (overlapRatio < RunParam.PercentSpecCarbon[Mod].Max)
                                                {
                                                    result = false;
                                                    tempret = false;
                                                }
                                            }
                                            ResultParam[indexNum].Percent_Carbon[Index_OBD].Result.Add(Carbonitem.Index, tempret);
                                        }
                                        
                                        #region
                                        if (Main.PERCENT_VIEW)
                                        {
                                            CogGraphicLabel label = new CogGraphicLabel();
                                            if (tempret == false)
                                                label.Color = CogColorConstants.Red;
                                            else
                                                label.Color = CogColorConstants.Green;

                                            label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9);
                                            label.BackgroundColor = CogColorConstants.Orange;

                                            //카본 내 용액 퍼센트 색상

                                            int yGAP = 5;

                                            int TempIndex = Carbonitem.Index + (i * RunParam.Carbon_CountSpec);

                                            yGAP *= (Carbonitem.Index % 2);
                                            
                                            double X_Pos = Carbonitem.value.X += Carbonitem.value.OffSetX;
                                            double Y_Pos = Carbonitem.value.Y += Carbonitem.value.OffSetY - yGAP;

                                            //double X_Pos = _CarbonResultParam.Boxs[TempIndex].X += _CarbonResultParam.Boxs[TempIndex].OffSetX;
                                            //double Y_Pos = _CarbonResultParam.Boxs[TempIndex].Y += _CarbonResultParam.Boxs[TempIndex].OffSetY - yGAP;

                                            label.SetXYText(X_Pos, Y_Pos, "비율: " + overlapRatio.ToString() + "%");

                                            label.Alignment = CogGraphicLabelAlignmentConstants.TopLeft;
                                            _CarbonResultParam.ResultsGraphicOffset.Add(new CogGraphicLabel(label));
                                            //_CarbonResultParam.BatchID.Add(Index_OBD);

                                            label.Dispose();
                                        }
                                        #endregion

                                        //----------------------------------------------------------------------------------------------------------------------

                                        CarbonImage.Dispose();
                                        OverLapImage.Dispose();

                                    }// if (RunParam.PercentSpecCarbon[Mod].Use)
                                }
                            }

                            ///03.Carbon 연결(1~3번카본) BOX와 용액의 퍼센트값 구하기          
                            if (RunParam.PercentSpecCarbonALL.Count(value => value.Use == true) > 0)
                            {
                                int Mod = 0;
                                if (RunParam.PercentSpecCarbonALL[Mod].Use)
                                {
                                        Mat CarbonImage = new Mat((int)InputRegion.Height, (int)InputRegion.Width, MatType.CV_8U, Scalar.All(0));
                                        var contour = _CarbonResultParam.Get_CarbonAddAllBox(Index_OBD).RegionPoint;
                                        //      Cv2.ImShow("CarbonImage:", CarbonImage);
                                        Cv2.DrawContours(CarbonImage, new List<List<OpenCvSharp.Point>> { contour }, -1, Scalar.All(255), -1);
                                        //         Cv2.ImShow("DrawContours:", CarbonImage);
                                        Mat OverLapImage = new Mat();
                                        Cv2.BitwiseAnd(SolutionImage, CarbonImage, OverLapImage);
                                        //Cv2.ImShow("IntersectionImage:" + Carbonitem.Index, intersection);

                                        // 겹치는 영역의 비율 구하기
                                        double Carbon_ZeroArea = Cv2.CountNonZero(CarbonImage);
                                        double OverLapArea = Cv2.CountNonZero(OverLapImage);
                                        double overlapRatio = Math.Round(OverLapArea / Carbon_ZeroArea * 100.0, 0);
                                        returnValueCarbonAll.Add(overlapRatio);

                                        ResultParam[indexNum].Percent_CarbonAll[Index_OBD].Data.Add(0, overlapRatio);

                                        bool tempret = true;

                                        if (RunParam.PercentSpecCarbonALL[Mod].Use)
                                        {
                                            if (overlapRatio < RunParam.PercentSpecCarbonALL[Mod].Max)
                                            {
                                                result = false;
                                                tempret = false;
                                            }
                                        }
                                        ResultParam[indexNum].Percent_CarbonAll[Index_OBD].Result.Add(Mod, tempret);

                                        #region
                                    
                                        if (Main.PERCENT_VIEW)
                                        {
                                            CogGraphicLabel label = new CogGraphicLabel();
                                            if (tempret == false)
                                                label.Color = CogColorConstants.Red;
                                            else
                                                label.Color = CogColorConstants.Green;

                                            label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9);
                                            label.BackgroundColor = CogColorConstants.Magenta;
                                           //큰 카본박스 색상

                                            int yGAP = 20;

                                            int TempIndex = Index_OBD;
                                        
                                        //      yGAP *= (item.index % 2);
                                            double X_Pos = _CarbonResultParam.Get_CarbonAddAllBox(TempIndex).X += _CarbonResultParam.Get_CarbonAddAllBox(TempIndex).OffSetX;
                                            double Y_Pos = _CarbonResultParam.Get_CarbonAddAllBox(TempIndex).Y += _CarbonResultParam.Get_CarbonAddAllBox(TempIndex).OffSetY + yGAP;

                                            label.SetXYText(X_Pos, Y_Pos, "비율: " + overlapRatio.ToString() + "%");

                                            label.Alignment = CogGraphicLabelAlignmentConstants.TopCenter;
                                            _CarbonResultParam.ResultsGraphicOffset.Add(new CogGraphicLabel(label));
                                            //_CarbonResultParam.BatchID.Add(Index_OBD);

                                            label.Dispose();
                                        }
                                        #endregion

                                        CarbonImage.Dispose();
                                        OverLapImage.Dispose();
                                 }
                            }

#if false
                                //Batch Image 기준 (영역으로잘린 이미지) 로 찾기 ( 시간을 짧으나. 영역에 대한 제한이있음)
                             if (RunParam.PercentSpecCarbontoCarbon.Count(value => value.Use == true) > 0) 
                            {
                                try
                                {

                                    int GrayValue = (int)RunParam.GrayValueSpecCarbontoCarbon.Max;

                                    Mat OrgInputmat = VisionProHelper.GetMatimage(((nrt.Input)_SolutionResultParam.Input.Image).get_org_input_ndbuff(i));
                                    OrgInputmat = VisionProHelper.GetMatimage8BitG(OrgInputmat);

                                    //해당 그레이값 밑으로는 검은색(0)으로 처리함 
                                    Cv2.Threshold(OrgInputmat, OrgInputmat, GrayValue, 255, ThresholdTypes.TozeroInv);
                                    var contour = _CarbonResultParam.Get_CarbonToCarbonBox(Index_OBD, RunParam.BetweenBoxsDIV , RunParam.PercentSpecCarbontoCarbon);

                                    foreach (var box in contour.Select((value, index) => (value, index)))
                                    {
                                        int Mod = box.index % RunParam.BetweenBoxsDIV;

                                        if (RunParam.PercentSpecCarbontoCarbon[Mod].Use)
                                        {
                                            OpenCvSharp.Rect roi = new OpenCvSharp.Rect((int)box.value.X, (int)box.value.Y, (int)box.value.Width, (int)box.value.Height);
                                            Mat TempCropImage = new Mat(OrgInputmat, roi).Clone();
                                            //      Cv2.ImShow("TempCropImage:" + box.index, TempCropImage);
                                            double Carbon_ZeroArea = (int)box.value.Width * (int)box.value.Height;
                                            double OverLapArea = Cv2.CountNonZero(TempCropImage);
                                            double overlapRatio = Math.Round(OverLapArea / Carbon_ZeroArea * 100.0,0);
                                            returnValueCarbontoCarbon.Add(overlapRatio);

                                            ResultParam[indexNum].Percent_CarbontoCarbon[Index_OBD].Data.Add(box.index, overlapRatio);

                                            bool tempret = true;


                                            if (RunParam.PercentSpecCarbontoCarbon[Mod].Use)
                                            {
                                                if (overlapRatio < RunParam.PercentSpecCarbontoCarbon[Mod].Max)
                                                {
                                                    result = false;
                                                    tempret = false;
                                                }
                                            }
                                            ResultParam[indexNum].Percent_CarbontoCarbon[Index_OBD].Result.Add(box.index, tempret);
                                            #region
                                            if (Main.PERCENT_VIEW)
                                            {

                                                CogGraphicLabel label = new CogGraphicLabel();
                                                if (tempret == false)
                                                    label.Color = CogColorConstants.Red;
                                                else
                                                    label.Color = CogColorConstants.Green;

                                                label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9);
                                                label.BackgroundColor = CogColorConstants.Purple;

                                                int yGAP = 13;

                                                int TempIndex = Mod;

                                                if (Mod == 1)
                                                    yGAP = (int)box.value.Height - yGAP;

                                                double X_Pos = box.value.X + box.value.OffSetX;
                                                double Y_Pos = box.value.Y + box.value.OffSetY + yGAP;

                                                label.SetXYText(X_Pos, Y_Pos, "비율: " + overlapRatio.ToString() + "%");


                                                label.Alignment = CogGraphicLabelAlignmentConstants.TopCenter;

                                                //_CarbonResultParam.ResultsGraphic.Add(new CogGraphicLabel(label));
                                                _CarbonResultParam.ResultsGraphicOffset.Add(new CogGraphicLabel(label));
                                                _CarbonResultParam.BatchID.Add(Index_OBD);

                                                label.Dispose();
                                            }
                                            #endregion

                                            TempCropImage.Dispose();
                                        }
                                    }
                                
                                    OrgInputmat.Dispose();

                                }
                                catch (Exception ex)
                                {

                                }
                            }

#else

                            if (RunParam.PercentSpecCarbontoCarbon.Count(value => value.Use == true) > 0) // OBD입력 이미지를 기준으로 하기때문에 영역에 대한 제한이 자유로움. 
                            {
                                
                                int GrayValue = (int)RunParam.GrayValueSpecCarbontoCarbon.Max;
                                Mat OrgInputmat = new Mat();
                                OrgInputmat = VisionProHelper.GetMatimage(_SolutionResultParam.Input.Image.ToCogImage());

                       //         Cv2.ImShow("Color:", OrgInputmat);
                                OrgInputmat = VisionProHelper.GetMatimage8BitG(OrgInputmat);
                     //            Cv2.ImShow("Gray:", OrgInputmat);

                                //해당 그레이값 밑으로는 검은색(0)으로 처리함 
                                Cv2.Threshold(OrgInputmat, OrgInputmat, GrayValue, 255, ThresholdTypes.TozeroInv);
                                var contour = _CarbonResultParam.Get_CarbonToCarbonBox(Index_OBD, RunParam.BetweenBoxsDIV , RunParam.PercentSpecCarbontoCarbon);
                           
                                foreach (var box in contour.Select((value, index) => (value, index)))
                                {
                                    int Mod = box.index % RunParam.BetweenBoxsDIV;
                           
                                    if (RunParam.PercentSpecCarbontoCarbon[Mod].Use)
                                    {
                                        OpenCvSharp.Rect roi = new OpenCvSharp.Rect((int)(box.value.X + box.value.OffSetX), (int)(box.value.Y + box.value.OffSetY), (int)box.value.Width, (int)box.value.Height);
                                        Mat TempCropImage = new Mat(OrgInputmat, roi).Clone();
                                        //      Cv2.ImShow("TempCropImage:" + box.index, TempCropImage);
                                        double Carbon_ZeroArea = (int)box.value.Width * (int)box.value.Height;
                                        double OverLapArea = Cv2.CountNonZero(TempCropImage);
                                        double overlapRatio = Math.Round(OverLapArea / Carbon_ZeroArea * 100.0, 0);
                                        returnValueCarbontoCarbon.Add(overlapRatio);
                           
                                        ResultParam[indexNum].Percent_CarbontoCarbon[Index_OBD].Data.Add(box.index, overlapRatio);
                           
                                        bool tempret = true;
                           
                           
                                        if (RunParam.PercentSpecCarbontoCarbon[Mod].Use)
                                        {
                                            if (overlapRatio < RunParam.PercentSpecCarbontoCarbon[Mod].Max)
                                            {
                                                result = false;
                                                tempret = false;
                                            }
                                        }
                                        ResultParam[indexNum].Percent_CarbontoCarbon[Index_OBD].Result.Add(box.index, tempret);
                                        #region
                                      if (Main.PERCENT_VIEW)
                                      {
                           
                                          CogGraphicLabel label = new CogGraphicLabel();
                                          if (tempret == false)
                                              label.Color = CogColorConstants.Red;
                                          else
                                              label.Color = CogColorConstants.Green;
                           
                                          label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9);
                                          label.BackgroundColor = CogColorConstants.Purple;   // 카본사이검사 
                           
                                          int yGAP = 13;
                           
                                          int TempIndex = Mod;
                           
                                          if (Mod == 1)
                                              yGAP = (int)box.value.Height - yGAP;
                           
                                          double X_Pos = box.value.X += box.value.OffSetX;
                                          double Y_Pos = box.value.Y += box.value.OffSetY + yGAP;
                           
                                          label.SetXYText(X_Pos, Y_Pos, "비율: " + overlapRatio.ToString() + "%");
                           
                           
                                          label.Alignment = CogGraphicLabelAlignmentConstants.TopCenter;
                           
                                          _CarbonResultParam.ResultsGraphicOffset.Add(new CogGraphicLabel(label));
                                          //_CarbonResultParam.BatchID.Add(Index_OBD);
                           
                                          label.Dispose();
                                      }
                                      #endregion
                           
                                        TempCropImage.Dispose();
                                    }
                                }
                           
                                OrgInputmat.Dispose();
                            }

                            if (RunParam.PercentSpecCarbonSide.Count(value => value.Use == true) > 0) // OBD입력 이미지를 기준으로 하기때문에 영역에 대한 제한이 자유로움. 
                            {

                                int GrayValue = (int)RunParam.GrayValueSpecCarbontoCarbon.Max;
                                Mat OrgInputmat = new Mat();

                                OrgInputmat = VisionProHelper.GetMatimage(_SolutionResultParam.Input.Image.ToCogImage());
                                OrgInputmat = VisionProHelper.GetMatimage8BitG(OrgInputmat);

                                //해당 그레이값 밑으로는 검은색(0)으로 처리함 
                                Cv2.Threshold(OrgInputmat, OrgInputmat, GrayValue, 255, ThresholdTypes.TozeroInv);
                                var contour = _CarbonResultParam.Get_CarbonSideBox(Index_OBD , RunParam.PercentSpecCarbonSide);

                                foreach (var box in contour.Select((value, index) => (value, index)))
                                {
                                    int Mod = box.index;// % RunParam.BetweenBoxsDIV;

                                    if (RunParam.PercentSpecCarbonSide[Mod].Use)
                                    {
                                        OpenCvSharp.Rect roi = new OpenCvSharp.Rect((int)(box.value.X + box.value.OffSetX), (int)(box.value.Y + box.value.OffSetY), (int)box.value.Width, (int)box.value.Height);
                                        Mat TempCropImage = new Mat(OrgInputmat, roi).Clone();
                                        //      Cv2.ImShow("TempCropImage:" + box.index, TempCropImage);
                                        double Carbon_ZeroArea = (int)box.value.Width * (int)box.value.Height;
                                        double OverLapArea = Cv2.CountNonZero(TempCropImage);
                                        double overlapRatio = Math.Round(OverLapArea / Carbon_ZeroArea * 100.0, 0);
                                        returnValueCarbonSide.Add(overlapRatio);

                                        ResultParam[indexNum].Percent_CarbonSide[Index_OBD].Data.Add(box.index, overlapRatio);

                                        bool tempret = true;


                                        if (RunParam.PercentSpecCarbonSide[Mod].Use)
                                        {
                                            if (overlapRatio > RunParam.PercentSpecCarbonSide[Mod].Min)
                                            {
                                                result = false;
                                                tempret = false;
                                            }
                                        }
                                        ResultParam[indexNum].Percent_CarbonSide[Index_OBD].Result.Add(box.index, tempret);
                                        #region
                                        if (Main.PERCENT_VIEW)
                                        {

                                            CogGraphicLabel label = new CogGraphicLabel();
                                            if (tempret == false)
                                                label.Color = CogColorConstants.Red;
                                            else
                                                label.Color = CogColorConstants.Green;

                                            label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9);
                                            label.BackgroundColor = CogColorConstants.Yellow;

                                            int yGAP = 0;

                                            int TempIndex = Mod;

                                       //     if (Mod == 1)
                                      //          yGAP = (int)box.value.Height - yGAP;

                                            double X_Pos = box.value.X += box.value.OffSetX;
                                            double Y_Pos = box.value.Y += box.value.OffSetY + yGAP;

                                            label.SetXYText(X_Pos, Y_Pos, "비율: " + overlapRatio.ToString() + "%");


                                            label.Alignment = CogGraphicLabelAlignmentConstants.TopCenter;

                                            _CarbonResultParam.ResultsGraphicOffset.Add(new CogGraphicLabel(label));
                                            //_CarbonResultParam.BatchID.Add(Index_OBD);

                                            label.Dispose();
                                        }
                                        #endregion

                                        TempCropImage.Dispose();
                                    }
                                }

                                OrgInputmat.Dispose();
                            }
#endif




                            SolutionImage.Dispose();
                            Solutions.Clear();
                            Carbons.Clear();
                        }
                        catch (System.Exception ex)
                        {

                        }

                   //     ResultParam[indexNum].PercentResultCarbon.Add(returnValueCarbon);
                   //      ResultParam[indexNum].PercentResultCarbonAll.Add(returnValueCarbonAll);
                   //       ResultParam[indexNum].PercentResultCarbontoCarbon.Add(returnValueCarbontoCarbon);
#if false

                        foreach (var item in returnValueCarbonAll.Select((value, index) => (value, index)))
                        {
                            bool tempret = true;
                            int Mod = 0;
                            if (RunParam.PercentSpecCarbonALL[Mod].Use) 
                            {
                                if (item.value < RunParam.PercentSpecCarbonALL[Mod].Max)
                                {
                                    result = false;
                                    tempret = false;
                                }
                            }
                            ResultParam[indexNum].PercentResultCarbonAllBool.Add(tempret);
                            #region
                            if (Main.PERCENT_VIEW)
                            {
                                CogGraphicLabel label = new CogGraphicLabel();
                                if (tempret == false)
                                    label.Color = CogColorConstants.Red;
                                else
                                    label.Color = CogColorConstants.Green;

                                label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9);
                                label.BackgroundColor = CogColorConstants.DarkGrey;

                                int yGAP = 20;

                                int TempIndex = i;

                                //      yGAP *= (item.index % 2);
                                double X_Pos = _CarbonResultParam.Get_batchGroupBoxs()[TempIndex].X;
                                double Y_Pos = _CarbonResultParam.Get_batchGroupBoxs()[TempIndex].Y + yGAP;

                                label.SetXYText(X_Pos, Y_Pos, "비율: " + item.value.ToString() + "%");

                                label.Alignment = CogGraphicLabelAlignmentConstants.TopCenter;
                                _CarbonResultParam.ResultsGraphic.Add(new CogGraphicLabel(label));
                                _CarbonResultParam.BatchID.Add(Index_OBD);

                                label.Dispose();
                            }
                            #endregion
                        }
                        foreach (var item in returnValueCarbon.Select((value, index) => (value, index)))
                        {
                            bool tempret = true;
                            int Mod = item.index % RunParam.Carbon_CountSpec;

                            if (Mod < RunParam.Carbon_CountSpec) //Carbon 123 | 321 각각의 영역 스펙에 대한  NG 판정 하기 
                            {
                                if (RunParam.PercentSpecCarbon[Mod].Use)
                                {
                                    if (item.value < RunParam.PercentSpecCarbon[Mod].Max)
                                    {
                                        result = false;
                                        tempret = false;
                                    }
                                }
                                ResultParam[indexNum].PercentResultCarbonBool.Add(tempret);
                            }

                            #region
                            if (false)
                            {
                                CogGraphicLabel label = new CogGraphicLabel();
                                if (tempret == false)
                                    label.Color = CogColorConstants.Red;
                                else
                                    label.Color = CogColorConstants.Green;

                                label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9);
                                label.BackgroundColor = CogColorConstants.LightGrey;

                                int yGAP = 5;

                                int TempIndex = item.index + (i * RunParam.Carbon_CountSpec);

                                yGAP *= (item.index % 2);

                                double X_Pos = _CarbonResultParam.Boxs[TempIndex].X;
                                double Y_Pos = _CarbonResultParam.Boxs[TempIndex].Y - yGAP;

                                label.SetXYText(X_Pos, Y_Pos, "비율: " + item.value.ToString() + "%");

                                label.Alignment = CogGraphicLabelAlignmentConstants.TopLeft;
                                _CarbonResultParam.ResultsGraphic.Add(new CogGraphicLabel(label));
                                _CarbonResultParam.BatchID.Add(Index_OBD);

                                label.Dispose();
                            }
                            #endregion
                        }

                        foreach (var item in returnValueCarbontoCarbon.Select((value, index) => (value, index)))
                        {
                            bool tempret = true;
                            int Mod = item.index % RunParam.BetweenBoxsDIV;

                            if (Mod < RunParam.BetweenBoxsDIV) //Carbon 사이사이 영역 스펙에 대한  NG 판정 하기 
                            {
                                if (RunParam.PercentSpecCarbontoCarbon[Mod].Use) 
                                { 
                                    if (item.value < RunParam.PercentSpecCarbontoCarbon[Mod].Max)
                                    {
                                        result = false;
                                        tempret = false;
                                    }
                                }
                                ResultParam[indexNum].PercentResultCarbontoCarbonBool.Add(tempret);
                            }

                            #region
                            if (Main.PERCENT_VIEW)
                            {
                                var CarbontoCarbonBoxs = _CarbonResultParam.Get_batchGroupBetweenBoxsDIV(Index_OBD, RunParam.BetweenBoxsDIV);
                                CogGraphicLabel label = new CogGraphicLabel();
                                if (tempret == false)
                                    label.Color = CogColorConstants.Red;
                                else
                                    label.Color = CogColorConstants.Green;

                                label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9);
                                label.BackgroundColor = CogColorConstants.Grey;

                                int yGAP = 13;

                                int TempIndex = item.index + (i * RunParam.BetweenBoxsDIV);


                                if (Mod == 1)
                                    yGAP = (int)CarbontoCarbonBoxs[0][item.index].Height - yGAP;

                                double X_Pos = CarbontoCarbonBoxs[0][item.index].X;
                                double Y_Pos = CarbontoCarbonBoxs[0][item.index].Y + yGAP;

                                label.SetXYText(X_Pos, Y_Pos, "비율: " + item.value.ToString() + "%");


                                label.Alignment = CogGraphicLabelAlignmentConstants.TopCenter;

                                _CarbonResultParam.ResultsGraphic.Add(new CogGraphicLabel(label));
                                _CarbonResultParam.BatchID.Add(Index_OBD);

                                label.Dispose();
                            }
                            #endregion
                        }
#endif
                    }
                    else
                    {
                        //해당 함수를 호출 하기 전에 확인 필요. 
                        //용액이 없으면 NG임 
                        result = false;
                    }

                    returnbool.Add(result);
                    ResultParam[indexNum].Result.Add(result);
                }

                //         DeepCopy 가 아니라서 클리어 하면 결과 날라감. 
                //         DeepCopy 하면 혹시 시간 더 걸릴까 싶어서 안함.
                //        _SolutionResultParam.Clear();
                //        _CarbonResultParam.Clear();
                //        _SolutionResultParam.Dispose();
                //        _CarbonResultParam.Dispose();

            }
            catch(Exception ex) 
            {

            }
    //        return returnValues;
        }

        public override void RunTools(int indexNum)
        {
            try
            {
                CARBON_OBD.RunParam = this.RunParam.RunParam_OBD;

                CARBON_SEG.RunParam = this.RunParam.RunParam_CARBON;
                CARBON_SEG.SetInputImage(CARBON_OBD.Input);

                SOLUTION_SEG.RunParam = this.RunParam.RunParam_SOLUTION;
                SOLUTION_SEG.SetInputImage(CARBON_OBD.Input);

                ISample samples = VProAIControl.Stream.Process(CARBON_OBD.Input.Image);
                foreach (var item in samples.Markings)
                {
                    if (CARBON_OBD.Subject.Name == item.Key)
                    {                                      
                        CARBON_OBD.Run(indexNum , new Dictionary<string, IMarking>() { { item.Key, item.Value } } ); 
                    }
                    if (CARBON_SEG.Subject.Name == item.Key)
                    {
                        CARBON_SEG.Run(indexNum, new Dictionary<string, IMarking>() { { item.Key, item.Value } });
                    }
                    if (SOLUTION_SEG.Subject.Name == item.Key)
                    {
                        SOLUTION_SEG.Run(indexNum, new Dictionary<string, IMarking>() { { item.Key, item.Value } });
                    }
                }


            }
            catch
            {

            }
        }
        public  override void Run(int indexNum)
        {
            int seq = 0;
            bool LoopFlag = true;
            bool Ret = false;

            bool[] ObjectDetect   = new bool[RunParam.OBD_CountSpec];
            bool[] CarbonDetect   = new bool[RunParam.OBD_CountSpec];
            bool[] SolutionDetect = new bool[RunParam.OBD_CountSpec];


            bool[] ret = new bool[RunParam.OBD_CountSpec];

            try
            {
                while (LoopFlag)
                {
                    switch (seq)
                    {
                        case 0:
                            ResultParam[indexNum].Clear();
                            CARBON_OBD.ResultParam[indexNum].Clear();
                            CARBON_SEG.ResultParam[indexNum].Clear();
                            SOLUTION_SEG.ResultParam[indexNum].Clear();


       

                            seq++;
                            break;

                        case 1:
                            RunTools(indexNum);

                            //     CARBON_OBD.Run(indexNum);
                            CARBON_OBD.GetResultsGraphic(indexNum);
                            if (!CARBON_OBD.ResultParam[indexNum].SearchResultRun)
                            {
                                seq = Seq.NG_SEQ;
                                break;
                            }
                            ResultParam[indexNum].OBD_BoolResults = CARBON_OBD.ResultParam[indexNum].ResultOBD.ToList();
                            seq++;
                            break;

                        case 2:
                            if (ResultParam[indexNum].OBD_CountResultsOK == RunParam.OBD_CountSpec)
                            {
                                //설정된 Spec만큼의 Object Detect 를 하면  OK
                                foreach (var item in ObjectDetect.Select((value, index) => (value, index)))
                                {
                                    ObjectDetect[item.index] = true;
                                }
                            }
                            else if (ResultParam[indexNum].OBD_CountResultsOK == 0 || ResultParam[indexNum].OBD_CountResultsOK < RunParam.OBD_CountSpec)
                            {
                                foreach (var item in ResultParam[indexNum].OBD_BoolResults.Select((value, index) => (value, index)))
                                {
                                    ObjectDetect[item.index] = item.value;
                                }
                            }
                            else  //  this.ResultParam.Count_OBD > this.RunParam.Count_OBD
                            {
                                //설정 보다 더 많이 찾으면 어떻게 조건을 걸어야되는건지 모르겠네. 
                            }
                            if (ResultParam[indexNum].OBD_CountResultsOK == 0)
                            {
                                seq = Seq.NG_SEQ;
                                break;
                            }
                            seq++;
                            break;

                        case 3:

                     //       CARBON_SEG.Run(indexNum);
                            CARBON_SEG.GetResultsGraphic(indexNum);

                            CARBON_SEG.ResultParam[indexNum].Input.InputOBDCount = CARBON_OBD.ResultParam[indexNum].ResultOBDCount;


                            seq++;
                            break;

                        case 4:
                            var CarbonBlobCount = CARBON_SEG.ResultParam[indexNum].GetBlobCount();
                            for (int i = 0; i < CarbonDetect.Length;i++)
                            {
                                if (ObjectDetect[i])
                                {
                                    bool tempRet = true;

                                    ///01.검출된 카본의 수량 스펙설정 3개 여야함. 
                                   if(CarbonBlobCount[i] != RunParam.Carbon_CountSpec)
                                    {
                                        tempRet = false;
                                    }
                                   ///02. 검출된 카본의 영역값 스펙 설정 1 , 2  , 3번에 대한 설정
                                    if (CarbonBlobCount[i] == RunParam.Carbon_CountSpec)
                                    {
                                        var CarbonBlobs = CARBON_SEG.ResultParam[indexNum].Boxs.GroupBy(x => x.Index_OBD);                                

                                        foreach (var item in CarbonBlobs.Select((value, index) => (value, index)))
                                        {
                                            if (item.value.Key == i)
                                            {
                                                foreach (var item2 in item.value.Select((value2, index2) => (value2, index2)))
                                                {

                                                    int Mod = item2.value2.Index_Carbon % RunParam.Carbon_CountSpec;

                                                    if (Mod < 3) //Carbon 123 | 321 각각의 영역 스펙에 대한  NG 판정 하기 
                                                    {
                                                        if (RunParam.Carbon_AreaSpec[Mod].Use)
                                                        {
                                                            if (item2.value2.Area < RunParam.Carbon_AreaSpec[Mod].Max)
                                                            {
                                                                tempRet = false;
                                                            }
                                                            if (item2.value2.Area > RunParam.Carbon_AreaSpec[Mod].Min)
                                                            {
                                                                tempRet = false;
                                                            }
                                                        }
                                                    }
#if false
                                                    if (Mod == 0) // 맨좌측 , 맨우측 
                                                    { // 값을 다르게 해서 Spec을 걸려면 .. 
                                                        if (item2.value2.Area < RunParam.Carbon_AreaSpec[0].Max)
                                                        {
                                                            tempRet = false;
                                                        }
                                                        if (item2.value2.Area > RunParam.Carbon_AreaSpec[0].Min)
                                                        {
                                                            tempRet = false;
                                                        }
                                                    }
                                                    if (Mod == 1) // 중간 , 중간 
                                                    {// 값을 다르게 해서 Spec을 걸려면 .. 
                                                        if (item2.value2.Area < RunParam.Carbon_AreaSpec[1].Max)
                                                        {
                                                            tempRet = false;
                                                        }
                                                        if (item2.value2.Area > RunParam.Carbon_AreaSpec[1].Min)
                                                        {
                                                            tempRet = false;
                                                        }
                                                    }
                                                    if (Mod == 2) // 젤안쪽, 젤안쪽
                                                    {// 값을 다르게 해서 Spec을 걸려면 .. 
                                                        if (item2.value2.Area < RunParam.Carbon_AreaSpec[2].Max)
                                                        {
                                                            tempRet = false;
                                                        }
                                                        if (item2.value2.Area > RunParam.Carbon_AreaSpec[2].Min)
                                                        {
                                                            tempRet = false;
                                                        }
                                                    }
#endif
                                                }
                                            }
                                        }
                                    }

                                    ///03. 카본 사이 수량 스펙 조건 ( 찾은 수량  == (카본 수량 * 나누기수량))
                                    //var CarbonToCarbonBox = CARBON_SEG.ResultParam[indexNum].Get_CarbonToCarbonBox(i, RunParam.BetweenBoxsDIV);
                                    //if (CarbonToCarbonBox.Count != (RunParam.BetweenBoxsDIV * RunParam.Carbon_CountSpec) )
                                    //{
                                    //    tempRet = false;
                                    //}

                                    CarbonDetect[i] = tempRet;
                                }
                            }
                            seq++;
                            break;

                        case 5:

                     //       SOLUTION_SEG.Run(indexNum);
                            SOLUTION_SEG.GetResultsGraphic(indexNum);
                            SOLUTION_SEG.ResultParam[indexNum].Input.InputOBDCount = CARBON_OBD.ResultParam[indexNum].ResultOBDCount;
                            seq++;
                            break;

                        case 6:
                            for (int i = 0; i < SolutionDetect.Length; i++)
                            {
                                if (ObjectDetect[i] && CarbonDetect[i])
                                {



                                }
                            }
                            seq++;
                            break;

                        case 7:
#if false
                            CalculationPercentage(indexNum, SOLUTION_SEG.ResultParam[indexNum], CARBON_SEG.ResultParam[indexNum]);
                            ResultParam[indexNum].Get_BoxsCarbonAll.Add(CARBON_SEG.ResultParam[indexNum].Get_CarbonAddAllBoxs());
                            ResultParam[indexNum].Get_BoxsCarbontoCarbon.Add(CARBON_SEG.ResultParam[indexNum].Get_CarbonToCarbonBoxs((int)RunParam.RunParam_CARBON.CarbonToCarbon_DIV.Max));

                            List<List<BoundingBox>> SideBoxs = new List<List<BoundingBox>>();
                            for (int i = 0; i < CARBON_SEG.ResultParam[indexNum].Get_batchGroupBoxsCount; i++)
                            {
                                SideBoxs.Add(CARBON_SEG.ResultParam[indexNum].Get_CarbonSideBox(CARBON_SEG.ResultParam[indexNum].Convertbatch_to_OBDNumber(i) , RunParam.PercentSpecCarbonSide));
                                ResultParam[indexNum].Get_BoxsCarbonSide.Add(SideBoxs);
                            }

                            if (Main.PERCENT_VIEW && Main.machine.OPENCV_UseCheck)
                            {
                                if (RunParam.PercentSpecCarbonSide.Count(value => value.Use == true) > 0)
                                {
                                    foreach (var itemss in this.ResultParam[indexNum].Get_BoxsCarbonSide)
                                    {
                                        foreach (var items in itemss)
                                        {
                                            foreach (var item in items)
                                            {

                                                CogRectangle _RegionBox = new CogRectangle();
                                                _RegionBox.SetXYWidthHeight(item.X + item.OffSetX, item.Y + item.OffSetY, item.Width, item.Height);
                                                _RegionBox.Color = CogColorConstants.Yellow;
                                                // 카본외곽 검사 색상
                                                CARBON_SEG.ResultParam[indexNum].ResultsGraphicOffset.Add(new CogRectangle(_RegionBox));


                                                _RegionBox.Dispose();
                                            }
                                        }

                                    }
                                }
                                if (RunParam.PercentSpecCarbontoCarbon.Count(value => value.Use == true) > 0)
                                {
                                    foreach (var itemss in this.ResultParam[indexNum].Get_BoxsCarbontoCarbon)
                                    {
                                        foreach (var items in itemss)
                                        {
                                            foreach (var item in items)
                                            {
                                                CogRectangle _RegionBox = new CogRectangle();
                                                _RegionBox.SetCenterWidthHeight(item.CenterX + item.OffSetX, item.CenterY + item.OffSetY, item.Width, item.Height);
                                                _RegionBox.Color = CogColorConstants.Purple;
                                                // 카본 사이 검사 색상

                                                //_RegionBox.SetXYWidthHeight(item.X + item.OffSetX, item.Y + item.OffSetY, item.Width, item.Height);
                                                CARBON_SEG.ResultParam[indexNum].ResultsGraphicOffset.Add(new CogRectangle(_RegionBox));


                                                _RegionBox.Dispose();
                                            }
                                        }

                                    }
                                }
                            }
#endif

                            seq = Seq.OK_SEQ;
                            break;

                        case Seq.NG_SEQ:

                            seq = Seq.COMPLET_SEQ;
                            break;

                        case Seq.OK_SEQ:

                            seq = Seq.COMPLET_SEQ;
                            break;

                        case Seq.COMPLET_SEQ:
                            LoopFlag = false;
                            break;
                    }
                }
            }
            catch(System.Exception ex)
            {

            }
        }
#if false
        public override void  RunOnly(int indexNum)
        {
            int seq = 0;
            bool LoopFlag = true;
            bool Ret = false;
            try
            {
                while (LoopFlag)
                {
                    switch (seq)
                    {
                        case 0:
                            ResultParam[indexNum].Clear();
                            seq++;
                            break;

                        case 1:
                            CARBON_OBD.RunParam = this.RunParam.RunParam_OBD;
                            CARBON_OBD.Run(indexNum);
                            if (!CARBON_OBD.ResultParam[indexNum].SearchResultRun)
                            {
                                Ret = false;
                                seq = Seq.NG_SEQ;
                                break;
                            }
                            CARBON_OBD.GetResultsGraphic(indexNum);
                            seq++;
                            break;

                        case 2:
                            CARBON_SEG.SetInputImage(CARBON_OBD.Output);
                            CARBON_SEG.RunParam = this.RunParam.RunParam_CARBON;
                            CARBON_SEG.Run(indexNum);
                            if (!CARBON_SEG.ResultParam[indexNum].SearchResultRun)
                            {
                                Ret = false;
                                seq = Seq.NG_SEQ;
                                break;
                            }
                            seq++;
                            break;

                        case 3:
                            SOLUTION_SEG.SetInputImage(CARBON_OBD.Output);
                            SOLUTION_SEG.RunParam = this.RunParam.RunParam_SOLUTION;
                            SOLUTION_SEG.Run(indexNum);
                            if (!SOLUTION_SEG.ResultParam[indexNum].SearchResultRun)
                            {
                                Ret = false;
                                seq = Seq.NG_SEQ;
                                break;
                            }
                            seq++;
                            break;

                        case 4:
                            if (Ret)
                            {
                                seq = Seq.OK_SEQ;
                            }
                            else
                            {
                                seq = Seq.NG_SEQ;
                            }
                            break;

                        case Seq.NG_SEQ:

                            seq = Seq.COMPLET_SEQ;
                            break;

                        case Seq.OK_SEQ:

                            seq = Seq.COMPLET_SEQ;
                            break;

                        case Seq.COMPLET_SEQ:
                            LoopFlag = false;
                            break;
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
#endif
        public override void  Getresult(int indexNum)
        {
            int seq = 0;
            bool LoopFlag = true;
            bool Ret = false;

          //  bool[] CarbonDetect = new bool[RunParam.OBD_CountSpec];

            try
            {
                while (LoopFlag)
                {
                    switch (seq)
                    {
                        case 0:
                            
                            seq++;

                            break;


                        case 1:

                            CalculationPercentage(indexNum, SOLUTION_SEG.ResultParam[indexNum], CARBON_SEG.ResultParam[indexNum]);
                            ResultParam[indexNum].Get_BoxsCarbonAll.Add(CARBON_SEG.ResultParam[indexNum].Get_CarbonAddAllBoxs());
                            ResultParam[indexNum].Get_BoxsCarbontoCarbon.Add(CARBON_SEG.ResultParam[indexNum].Get_CarbonToCarbonBoxs((int)RunParam.RunParam_CARBON.CarbonToCarbon_DIV.Max, RunParam.PercentSpecCarbontoCarbon));

                            List<List<BoundingBox>> SideBoxs = new List<List<BoundingBox>>();
                            for (int i = 0; i < CARBON_SEG.ResultParam[indexNum].Get_batchGroupBoxsCount; i++)
                            {
                                SideBoxs.Add(CARBON_SEG.ResultParam[indexNum].Get_CarbonSideBox(CARBON_SEG.ResultParam[indexNum].Convertbatch_to_OBDNumber(i), RunParam.PercentSpecCarbonSide));
                                ResultParam[indexNum].Get_BoxsCarbonSide.Add(SideBoxs);
                            }

                           // if (Main.PERCENT_VIEW && Main.machine.OPENCV_UseCheck)
                                if (Main.PERCENT_VIEW )
                                {
                                if (RunParam.PercentSpecCarbonSide.Count(value => value.Use == true) > 0)
                                {
                                    foreach (var itemss in this.ResultParam[indexNum].Get_BoxsCarbonSide)
                                    {
                                        foreach (var items in itemss)
                                        {
                                            foreach (var item in items)
                                            {

                                                CogRectangle _RegionBox = new CogRectangle();
                                                _RegionBox.SetXYWidthHeight(item.X + item.OffSetX, item.Y + item.OffSetY, item.Width, item.Height);
                                                _RegionBox.Color = CogColorConstants.Yellow;
                                                // 카본외곽 검사 색상
                                                CARBON_SEG.ResultParam[indexNum].ResultsGraphicOffset.Add(new CogRectangle(_RegionBox));


                                                _RegionBox.Dispose();
                                            }
                                        }

                                    }
                                }
                                if (RunParam.PercentSpecCarbontoCarbon.Count(value => value.Use == true) > 0)
                                {
                                    foreach (var itemss in this.ResultParam[indexNum].Get_BoxsCarbontoCarbon)
                                    {
                                        foreach (var items in itemss)
                                        {
                                            foreach (var item in items)
                                            {
                                                CogRectangle _RegionBox = new CogRectangle();
                                                _RegionBox.SetCenterWidthHeight(item.CenterX + item.OffSetX, item.CenterY + item.OffSetY, item.Width, item.Height);
                                                _RegionBox.Color = CogColorConstants.Purple;
                                                // 카본 사이 검사 색상

                                                //_RegionBox.SetXYWidthHeight(item.X + item.OffSetX, item.Y + item.OffSetY, item.Width, item.Height);
                                                CARBON_SEG.ResultParam[indexNum].ResultsGraphicOffset.Add(new CogRectangle(_RegionBox));


                                                _RegionBox.Dispose();
                                            }
                                        }

                                    }
                                }
                            }

                            seq = Seq.COMPLET_SEQ;
                            break;

                        case Seq.NG_SEQ:

                            seq = Seq.COMPLET_SEQ;
                            break;

                        case Seq.OK_SEQ:

                            seq = Seq.COMPLET_SEQ;
                            break;

                        case Seq.COMPLET_SEQ:
                            LoopFlag = false;
                            break;
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
        public override void SetImage_OBD(object InputImage)
        {
              CARBON_OBD.SetInputImage(InputImage);  
        }


    }






    [Serializable]
    public class PathData
    {
        public PathData()
        {
            Clear();
        }
        public PathData(PathData Other)
        {
            Clear();

            foreach (var item in Other.Data)
            {
                this.Data.Add(item);
            }
        }

        public void Clear()
        {
            this.Data.Clear();
        }

        public List<string> Data { get; set; } = new List<string>();
    }
    
}
