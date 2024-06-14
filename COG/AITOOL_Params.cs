using Cognex.VisionPro;
using nrt;
using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Media.Media3D;


using ViDi2;
using ViDi2.UI;
using OpenCvSharp.WpfExtensions;
namespace COG
{

    [Serializable]
    public abstract class JasRunparamsBase : IDisposable    
    {
        private bool disposedValue;

        public JasRunparamsBase()
        {
            this.ClearParameter();
        }
        public void ClearParameter()
        {
            this.SpecWidth = new SpecData();

            this.SpecArea = new SpecData();
            this.SpecArea.Min = 100;

            this.CarbonToCarbon_DIV = new SpecData();
            this.CarbonToCarbon_DIV.Max = 2;
        }
        public abstract void Clear();

        public SpecData CarbonToCarbon_DIV { get; set; } = new SpecData();
        public SpecData SpecWidth { get; set; } = new SpecData();
        public SpecData SpecArea { get; set; } = new SpecData();


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리형 상태(관리형 개체)를 삭제합니다.
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
            }
        }

        // // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        // ~JasRunparamsBase()
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

    [Serializable]
    public partial class JasAIRunparam : JasRunparamsBase //, ISerializable
    {

        public JasAIRunparam()
        {
            this.Clear();
        }
        public JasAIRunparam(JasAIRunparam Other)
        {
            this.Clear();

            this.SpecWidth = new SpecData(Other.SpecWidth);
            this.SpecArea  = new SpecData(Other.SpecArea);
            this.CarbonToCarbon_DIV = new SpecData(Other.CarbonToCarbon_DIV);
        }
        public override void Clear()
        {
            this.ClearParameter();
        }
    }

    [Serializable]
    public partial class JasAIResultparam : JasResultparamsBase //, ISerializable
    {


        Dictionary<string, IMarking> _results;
        public Dictionary<string, IMarking> results
        {
            get
            {
                return this._results;
            }
            set
            {
                this._results = value;
            }
        }
       



        //  public Result results;

        public JasAIResultparam()
        {
            this.Clear();
        }
        public JasAIResultparam(JasAIResultparam other, object results)
        {
            this.Clear();
            this.Clone(other);

            foreach (var item in other.ResultOBD)
            {
                this.ResultOBD.Add(item);
            }
            this.results = null;
        }

        /// <summary>
        /// OBD Tool에서 서치영역 찾은결과
        /// </summary>
        public List<bool> ResultOBD { get; set; } = new List<bool>();

        /// <summary>
        /// OBD 에서 True 인 값의 수량 
        /// </summary>
        public int ResultOBDCount 
        { 
            get
            {
                return this.ResultOBD.Count(vlaue => vlaue == true);
            }
        }

  //      public nrt.Result results { get; set; } = null;
       



        public override void Clear()
        {
            this.ClearParameter();

            if (this.ResultOBD != null)
                this.ResultOBD.Clear();
            this.ResultOBD = new List<bool>();

      //      if (this.results != null)
     //           this.results.Dispose();
     //       this.results = new nrt.Result();




        }
        public override void Dispose()
        {
            this.ResultsGraphic.Dispose();
            this.ResultsGraphicOffset.Dispose();

            this.Input.Dispose();
        //    this.results.Dispose();

            foreach (var item in this.Boxs)
            {
                item.Dispose();
            }
        }
    }

    [Serializable]
    public abstract class JasResultparamsBase 
    {
     
        public JasResultparamsBase()
        {
            this.ClearParameter();
        }
        public void Clone(JasResultparamsBase other)
        {
            foreach (var item in other.BatchID)
            {
                this.BatchID.Add(item);
            }
            foreach (var item in other.ResultsGraphic)
            {
                this.ResultsGraphic.Add((ICogGraphic)item);
            }
            foreach (var item in other.ResultsGraphicOffset)
            {
                this.ResultsGraphicOffset.Add((ICogGraphic)item);
            }
            foreach (var item in other.Boxs)
            {
                this.Boxs.Add(new BoundingBox(item));
            }
            this.SearchResultRun = other.SearchResultRun;

            this.Input = new ImageParam(other.Input);
        }



        public ICogRecord RecordData { get; set; } = null;

        /// <summary>
        /// 입력으로 받은 이미지정보. 
        /// </summary>
        public ImageParam Input { get; set; } = new ImageParam();

        /// <summary>
        /// Run 했을때 결과값 Status에 대한값이 성공이면 OK , 실패면 NG
        /// </summary>
        public bool SearchResultRun { get; set; } = false;
        /// <summary>
        /// 오버레이 그릴때 화면 나눠서 뿌릴려고 추가 한거. 
        /// </summary>
        public List<int> BatchID { get; set; } = new List<int>();
        public CogGraphicCollection ResultsGraphic { get; set; } = new CogGraphicCollection();
        /// <summary>
        /// Offset 더한거 , 원본이미지
        /// </summary>
        public CogGraphicCollection ResultsGraphicOffset { get; set; } = new CogGraphicCollection();

        public List<BoundingBox> Boxs { get; set; } = new List<BoundingBox>();

        public abstract void Clear();
        public void ClearParameter()
        {

            if (this.ResultsGraphicOffset != null)
                this.ResultsGraphicOffset.Clear();
            else
                this.ResultsGraphicOffset = new CogGraphicCollection();

            if (this.ResultsGraphic != null)
                this.ResultsGraphic.Clear();
            else
                this.ResultsGraphic = new CogGraphicCollection();

            if (this.Boxs != null)
                this.Boxs.Clear();
            else
                this.Boxs = new List<BoundingBox>();


            if (this.BatchID != null)
                this.BatchID.Clear();
            this.BatchID = new List<int>();


            this.SearchResultRun = false;
            this.Input.Clear();

            this.RecordData = null;
        }

        /// <summary>
        /// 용액 검출이나 , 카본 검출시 , OBD 4개 기준에 각각 찾은 수량 
        /// 맞는 값은 카본은 3개임. 
        /// </summary>
        /// <returns></returns>
        public int[] GetBlobCount()
        {
            int[] ReturnValue = new int[4];

            if (this.Boxs.Count > 0)
            {
                foreach (var item in this.Boxs)
                {
                    if (item.Index_OBD == 0)
                    {
                        ReturnValue[0]++;
                    }
                    if (item.Index_OBD == 1)
                    {
                        ReturnValue[1]++;
                    }
                    if (item.Index_OBD == 2)
                    {
                        ReturnValue[2]++;
                    }
                    if (item.Index_OBD == 3)
                    {
                        ReturnValue[3]++;
                    }
                }
            }

          //  var ReturnData = this.Boxs.GroupBy(x => x.Index_OBD).Select(group => new { Index_OBD = group.Key, Count = group.Count() });
            return ReturnValue;
        }

        public List<int> SortingOfIndex(List<PointF> Sortpoints)
        {
            int GetCount = Sortpoints.Count;


            //리턴 해야될 결과
            List<int> SortIndex = new List<int>();


            // Y방향으로 먼저 정렬함 
            var sortedBy = Sortpoints.OrderBy(p => p.Y).ToList();

            List<PointF> First = new List<PointF>();
            List<PointF> Second = new List<PointF>();


            // 세로 방향으로 2개의 군으로 분류 하고, 그 군들 끼리 X 방향으로 정렬함 
            //  12
            //  34 
            //  이런식으로 
            if (sortedBy.Count > 2)
            {
                int Divi_Num = sortedBy.Count / 2;

                for (int i = 0; i < sortedBy.Count; i++)
                {
                    if (i < Divi_Num)
                    {
                        First.Add(new PointF(sortedBy[i].X, sortedBy[i].Y));
                    }
                    else
                    {
                        Second.Add(new PointF(sortedBy[i].X, sortedBy[i].Y));
                    }
                }
                // X 좌표를 기준으로 정렬합니다.
                First = First.OrderBy(p => p.X).ToList();
                Second = Second.OrderBy(p => p.X).ToList();
            }
            else
            {
                for (int i = 0; i < sortedBy.Count; i++)
                {
                    First.Add(new PointF(sortedBy[i].X, sortedBy[i].Y));
                }
                First = First.OrderBy(p => p.X).ToList();
            }
            List<PointF> Total = new List<PointF>();
            foreach (var point in First)
            {
                Total.Add(new PointF(point.X, point.Y));
            }
            foreach (var point in Second)
            {
                Total.Add(new PointF(point.X, point.Y));
            }
            //--------------------------------------------------------------------------------------
            First.Clear();
            Second.Clear();

            /// 정렬한 포인트값과 정렬 하기전 포인트 값과 비교 하여 
            /// 정렬한 순서대로의 인덱스를 가져온다. 
            for (int i = 0; i < GetCount; i++)
            {
                for (int j = 0; j < GetCount; j++)
                {
                    if (Total[i].X == Sortpoints[j].X && Total[i].Y == Sortpoints[j].Y)
                    {
                        SortIndex.Add(j);
                    }
                }
            }


            return SortIndex;
        }

        /// <summary>
        /// carbon 3개있는거 정렬하기위함. 
        /// 1 2 3  ||   6 5 4
        /// 7 8 9  ||   12 11 10  
        /// </summary>
        /// <param name="Sortpoints"></param>
        /// <param name="batch_idx"></param>
        /// <returns></returns>
        public List<int> SortingOfIndex(List<PointF> Sortpoints, List<int> batch_idx , List<int> OBD_Index)
        {

            //리턴 해야될 결과
            List<int> SortIndex = new List<int>();

            try
            {

                var counts = batch_idx.GroupBy(x => x).Select(group => new { BatIDGroup = group.Key, Count = group.Count() });



                List<int> OBD_INDEX = new List<int>();
                var counts2 = OBD_Index.GroupBy(x => x).Select(group => new { BatIDGroup = group.Key, Count = group.Count() });




                foreach (var item in counts2.Select((value, Index) => (value, Index)))
                {
                    OBD_INDEX.Add(item.value.BatIDGroup);
                }




                List<List<PointF>> pointFs = new List<List<PointF>>();
                List<List<PointF>> SortpointFs = new List<List<PointF>>();

                foreach (var item in counts.Select((value, Index) => (value, Index)))
                {
                    pointFs.Add(new List<PointF>());
                }

                foreach (var item in Sortpoints.Select((value, Index) => (value, Index)))
                {
                    pointFs[batch_idx[item.Index]].Add(new PointF(item.value.X, item.value.Y));
                }

                //foreach (var item in pointFs.Select((value, Index) => (value, Index)))
                //{
                //    if (item.Index % 2 == 1)  // 짝수 0부터 시작이라 
                //    {
                //        SortpointFs.Add(item.value.OrderByDescending(p => p.X).ToList());
                //    }
                //    else // 홀수 0부터 시작이라 
                //    {
                //        SortpointFs.Add(item.value.OrderBy(p => p.X).ToList());
                //    }
                //}
                foreach (var item in pointFs.Select((value, Index) => (value, Index)))
                {
                    if (OBD_INDEX[item.Index] % 2 == 1)  // 짝수 0부터 시작이라 
                    {
                        SortpointFs.Add(item.value.OrderByDescending(p => p.X).ToList());
                    }
                    else // 홀수 0부터 시작이라 
                    {
                        SortpointFs.Add(item.value.OrderBy(p => p.X).ToList());
                    }
                }




                /// 정렬한 포인트값과 정렬 하기전 포인트 값과 비교 하여 
                /// 정렬한 순서대로의 인덱스를 가져온다. 
                /// 
                for (int i = 0; i < SortpointFs.Count; i++)
                {
                    for (int j = 0; j < SortpointFs[i].Count; j++)
                    {
                        foreach (var item in Sortpoints.Select((value, Index) => (value, Index)))
                        {
                            if (SortpointFs[i][j].X == item.value.X && SortpointFs[i][j].Y == item.value.Y)
                            {
                                SortIndex.Add(item.Index);
                            }

                        }
                    }
                }

                foreach (var item in pointFs.Select((value, Index) => (value, Index)))
                {
                    item.value.Clear();
                }
                foreach (var item in SortpointFs.Select((value, Index) => (value, Index)))
                {
                    item.value.Clear();
                }
            }
            catch(System.Exception ex)
            {


            }

            return SortIndex;
        }



        public int Get_batchGroupBoxsCount
        {
            get
            {
                return this.Boxs.GroupBy(x => x.Index_OBD).Select(group => new { Index_OBD = group.Key, Count = group.Count() }).Count();
            }
        }
        /// <summary>
        /// BatchID를OBDID로변환
        /// </summary>
        public int Convertbatch_to_OBDNumber(int _Batchid) 
        {     
            int returnValue = 0;
            var collectionitem = this.Boxs.GroupBy(x => x.Index_OBD).Select(group => new { Index_OBD = group.Key, Count = group.Count() });

            foreach (var item in collectionitem.Select((value,index) => (value,index)))
            {
                if (item.index == _Batchid)
                {
                    returnValue = item.value.Index_OBD;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// CARBON_ALL 묶은 BOX
        /// 카본 3개를 묶은 영역의 박스값
        /// 배치 아이디를 기준으로 정렬한 총 배치 아이디 만큼이 만들어 질꺼임\\ 이거도 유연하게 배치 이미지에 따라서. 
        /// </summary>
        /// <returns></returns>
        public List<BoundingBox> Get_CarbonAddAllBoxs()
        {
            List<BoundingBox> returnBox = new List<BoundingBox>();

            if (this.Boxs.Count() > 0)
            {
                //배치 아이디를 기준으로 정렬한 총 배치 아이디 만큼이 만들어 질꺼임 (ex  입력은 12개 일때는 4개 ,또는 입력이 6개일때는 2개 ) 이거도 유연하게 배치 이미지에 따라서. 
                var distinctNumbersWithCounts = this.Boxs
                                                .GroupBy(x => x.Index_OBD)
                                                .Select(group => new { Index_OBD = group.Key, Count = group.Count() });
                if (distinctNumbersWithCounts.Count() > 0)
                {
                    foreach (var item in distinctNumbersWithCounts)
                    {
                      returnBox.Add(new BoundingBox(Get_CarbonAddAllBox(item.Index_OBD)));                       
                    }
                }
            }
            return returnBox;
        }
        /// <summary>
        /// CARBON_ALL 묶은 BOX
        /// 카본 3개를 묶은 영역의 박스값
        /// 배치 아이디를 기준으로 회신 1개박스
        /// </summary>
        /// <returns></returns>
        public BoundingBox Get_CarbonAddAllBox(int _Index_OBD)
        {
            BoundingBox returnBox = new BoundingBox();

            if (this.Boxs.Count() > 0)
            {
                List<BoundingBox> BatchGroupBox = new List<BoundingBox>();

                for (int i = 0; i < this.Boxs.Count(); i++)
                {
                    if (_Index_OBD == this.Boxs[i].Index_OBD)
                    {
                        BatchGroupBox.Add(new BoundingBox(this.Boxs[i])); //그룹으로 뭉치기 Batch되서 입력된 아이디를 기준으로 
                    }
                }

                //그룹으로 뭉치고 나서, 첫번째 위치랑 마지막 위치의 값으로 큰 박스를 만들기 
                if (BatchGroupBox.Count > 0)
                {
                    BatchGroupBox = BatchGroupBox.OrderBy(p => p.X).ToList();

                    int SecondIndex = BatchGroupBox.Count() - 1;
                    double _Width = ((BatchGroupBox[SecondIndex].X + BatchGroupBox[SecondIndex].Width) - BatchGroupBox[0].X);
                    double _Height = BatchGroupBox[SecondIndex].Height;
                    double _Area = _Width * _Height;

                    double _XX = BatchGroupBox[0].X;
                    double _YY = BatchGroupBox[0].Y;

                    var contourPoints = new List<OpenCvSharp.Point>();


#if false
            
                    contourPoints.Add(new OpenCvSharp.Point(BatchGroupBox[0].X, BatchGroupBox[0].Y));
                    contourPoints.Add(new OpenCvSharp.Point(BatchGroupBox[0].X, BatchGroupBox[0].Y + _Height));
                    contourPoints.Add(new OpenCvSharp.Point(BatchGroupBox[0].X + (int)_Width, BatchGroupBox[0].Y + _Height));
                    contourPoints.Add(new OpenCvSharp.Point(BatchGroupBox[0].X + (int)_Width, BatchGroupBox[0].Y));


                    foreach (BoundingBox box in BatchGroupBox)
                    {
                        if (_Height < box.Height)
                        {
                            _Height = box.Height; //제일 큰 높이 기준으로 맞출려고 해놓은거임. 
                            _YY = box.Y;
                        }
                    }
#else

                    // OpenCV Rect 합치는걸로해서  계산해두됨
                    OpenCvSharp.Rect rect_First = new OpenCvSharp.Rect((int)BatchGroupBox[0].X, (int)BatchGroupBox[0].Y, (int)BatchGroupBox[0].Width, (int)BatchGroupBox[0].Height);
                    OpenCvSharp.Rect rect_Last = new OpenCvSharp.Rect((int)BatchGroupBox[SecondIndex].X, (int)BatchGroupBox[SecondIndex].Y, (int)BatchGroupBox[SecondIndex].Width, (int)BatchGroupBox[SecondIndex].Height);
                    OpenCvSharp.Rect rect_All = rect_First | rect_Last;

                    _Width = rect_All.Width;
                    _Height = rect_All.Height;
                    _Area = _Width * _Height;
                    _XX = rect_All.X;
                    _YY = rect_All.Y;

                    contourPoints.Clear();

                    contourPoints.Add(new OpenCvSharp.Point(_XX, _YY));
                    contourPoints.Add(new OpenCvSharp.Point(_XX, _YY + _Height));
                    contourPoints.Add(new OpenCvSharp.Point(_XX + (int)_Width, _YY + _Height));
                    contourPoints.Add(new OpenCvSharp.Point(_XX + (int)_Width, _YY));
#endif

                    returnBox = new BoundingBox(BatchGroupBox[0].Index_OBD, BatchGroupBox[0].Index_OBD, BatchGroupBox[0].batch_index, BatchGroupBox[0].class_number, _XX, _YY, _Width, _Height, BatchGroupBox[0].OffSetX, BatchGroupBox[0].OffSetY, contourPoints, _Area);
                }
                    
            }
            return returnBox;
        }


        /// <summary>
        /// 카본3개의 사이에 있는 2개 영역의 박스값. 
        /// 배치 개별 단위로 카본 사이에 ( 2 * DIV 수) 기본 4개
        /// 배치 이미지 전체 단위
        /// </summary>
        /// <param name="_BatchID"></param>
        /// <returns></returns>
        /// 
        public List<List<BoundingBox>> Get_CarbonToCarbonBoxs(int _DivCount , SpecData[] _SpecData)
        {
            List<List<BoundingBox>> BetweenBoxs = new List<List<BoundingBox>>();
            for (int i = 0; i < this.Get_batchGroupBoxsCount; i++)
            {            
                BetweenBoxs.Add(Get_CarbonToCarbonBox(Convertbatch_to_OBDNumber(i) , _DivCount , _SpecData));
            }
            return BetweenBoxs;
        }


        /// <summary>
        /// 카본3개의 사이에 있는 2개 영역의 박스값. 
        /// 배치 개별 단위로 카본 사이에 ( 2 * DIV 수) 기본 4개
        /// 배치 이미지 각각 단위
        /// </summary>
        /// <param name="_Index_OBD"></param>
        /// <param name="_DivCount"></param>
        /// <returns></returns>
        public List<BoundingBox> Get_CarbonToCarbonBox(int _Index_OBD, int _DivCount , SpecData[] _SpecData)
        {
            List<BoundingBox> returnBox = new List<BoundingBox>();

            if (this.Boxs.Count() > 0)
            {
                List<BoundingBox> BatchGroupBox = new List<BoundingBox>();

                for (int i = 0; i < this.Boxs.Count(); i++)
                {
                    if (_Index_OBD == this.Boxs[i].Index_OBD)
                    {
                        BatchGroupBox.Add(new BoundingBox(this.Boxs[i])); //그룹으로 뭉치기 Batch되서 입력된 아이디를 기준으로 
                    }
                }
                BatchGroupBox = BatchGroupBox.OrderBy(p => p.X).ToList();

                if (BatchGroupBox.Count > 1)
                {
                    for (int i = 0; i < BatchGroupBox.Count; i++)
                    {

                        int FirstIndex = i;
                        int SecondIndex = i + 1;

                        if (SecondIndex < BatchGroupBox.Count)
                        {
                            for (int KK = 0; KK < _DivCount; KK++)
                            {
                                int Y_BoxINDEX = 0; // 기존 FirstIndex 에서 같은 Y 위치 할려고 0으로 변경함
                            double XX = BatchGroupBox[FirstIndex].X + BatchGroupBox[FirstIndex].Width;
                            double YY = BatchGroupBox[Y_BoxINDEX].Y;

                            double _Width  = (BatchGroupBox[SecondIndex].X - (BatchGroupBox[FirstIndex].X + BatchGroupBox[FirstIndex].Width));
                            double _Height = BatchGroupBox[Y_BoxINDEX].Height / _DivCount;



                            double DifferentSize_W = 0;
                            double DifferentSize_H = 0;
#if true
                            double ResizeScale_X = 0.6;
                            double ResizeScale_Y = _SpecData[KK].Offset.OFFSET_X;

                            double Resize_Width  = _Width * ResizeScale_X;
                            double Resize_Height = _Height * ResizeScale_Y;
                      
                             DifferentSize_W = (_Width  - Resize_Width ) /2;
                             DifferentSize_H = (_Height - Resize_Height);

                            XX += DifferentSize_W;
                            YY += DifferentSize_H;

                            _Width  = Resize_Width;
                            _Height = Resize_Height;




                           double limit_Y = YY + (Resize_Height * 2);

                            if (YY < 0)
                            {
                                bool NG = true;
                            }
                        //    if(limit_Y > this.Input.Region[BatchGroupBox[0].batch_index].RegionBox.Height)
                                    if (limit_Y > this.Input.Region[0].RegionBox.Height)
                                    {
                              bool NG = true;
                               DifferentSize_H = 0;
                               YY = BatchGroupBox[FirstIndex].Y;
                               _Height = BatchGroupBox[FirstIndex].Height / _DivCount;
                            }
#endif



                                int Dir = -1;
                                if (KK == 1)
                                {
                                    Dir = 1;
                                }
                                returnBox.Add(new BoundingBox(BatchGroupBox[FirstIndex].Index_OBD, BatchGroupBox[FirstIndex].batch_index, BatchGroupBox[FirstIndex].class_number,
                                                            XX ,
                                                            YY + ((KK * _Height)) + (_SpecData[KK].Offset.OFFSET_Y * Dir),
                                                            _Width,
                                                            _Height,
                                                            BatchGroupBox[FirstIndex].OffSetX,
                                                            BatchGroupBox[FirstIndex].OffSetY));

                            }
                        }
                    }
                }


            }
            return returnBox;
        }


#if false
        /// <summary>
        /// 카본3개의 합박스를 기준으로 LEFT , RIGHT , BOM , TOP 사각형
        ///  /// LEFT , RIGHT , BOTTOM , TOP
        /// 배치 개별 단위로  기본 4개
        /// 배치 이미지 총합으로 줌 ( 4*4)
        /// <returns></returns>
        /// 
        public List<List<BoundingBox>> Get_CarbonSideBoxs()
        {
            List<List<BoundingBox>> SideBoxs = new List<List<BoundingBox>>();
            for (int i = 0; i < this.Get_batchGroupBoxsCount; i++)
            {
                SideBoxs.Add(Get_CarbonSideBox(Convertbatch_to_OBDNumber(i) , ));
            }
            return SideBoxs;
        }
#endif
        /// <summary>
        /// 카본3개의 합박스를 기준으로 LEFT , RIGHT , BOM , TOP 사각형
        ///  /// LEFT , RIGHT , BOTTOM , TOP
        /// 배치 개별 단위로  기본 4개
        /// 배치 이미지 각각 단위
        /// </summary>
        /// <param name="_Index_OBD"></param>
        /// <param name="_DivCount"></param>
        /// <returns></returns>
        public List<BoundingBox> Get_CarbonSideBox(int _Index_OBD , SpecData[] _SpecData)
        { /// LEFT , RIGHT , BOTTOM , TOP
            List<BoundingBox> returnBox = new List<BoundingBox>();

            if (this.Boxs.Count() > 0)
            {


                try
                {

                BoundingBox CarbonAllRectBox = Get_CarbonAddAllBox(_Index_OBD);



               
                const int LEFT = 0; const int RIGHT = 1;
                const int BOTTOM = 2; const int TOP = 3;


                double _Width  = 4;
                double _Height = 4;

                double XX = 0;
                double YY = 0;

                double Gap = 1;

                for (int i = 0; i < 4; i++)
                {
                    int Pos = i;
                   OffsetPos offsetPos = new OffsetPos(_SpecData[Pos].Offset);

                        int Dir_OffsetX = 1;
            //            int Dir_OffsetY = 1;

                    if ((_Index_OBD == 1 || _Index_OBD == 3))
                    {
                         Dir_OffsetX = -1;
                        if (Pos == LEFT)
                        {
                            Pos = RIGHT;
                        }
                        else if (Pos == RIGHT)
                        {
                            Pos = LEFT;
                        }
                    }
                    else
                    {
             //               Dir_OffsetY = -1;
                    }

         
                    switch (Pos)
                    {
                        case LEFT:
                        case RIGHT:
                           _Width = 4;
                            _Height = CarbonAllRectBox.Height;
                             YY     = CarbonAllRectBox.Y;



                            if (Pos == LEFT)
                            {
                                XX = CarbonAllRectBox.X - _Width - Gap;

                                XX -= offsetPos.WIDTH;
                                _Width += offsetPos.WIDTH;
                            }
                            if (Pos == RIGHT)
                            {
                                XX = CarbonAllRectBox.X + CarbonAllRectBox.Width + Gap;

                                _Width += offsetPos.WIDTH;
                            }

                            if(true)
                            {
                                YY -= (offsetPos.HEIGHT / 2);
                                _Height += offsetPos.HEIGHT;

                                YY += offsetPos.OFFSET_Y;


                                XX += (offsetPos.OFFSET_X * Dir_OffsetX);
                            }
                            break;

 
                        case BOTTOM:
                        case TOP:
                            _Width = CarbonAllRectBox.Width;
                            _Height = 4;
                            XX     = CarbonAllRectBox.X;

                            if (Pos == TOP)
                            {
                                YY = CarbonAllRectBox.Y - _Height - Gap;
                                YY -= offsetPos.HEIGHT;                  
                                _Height += offsetPos.HEIGHT;

                                XX += offsetPos.OFFSET_X;
                            }
                            if (Pos == BOTTOM)
                            {
                                YY = CarbonAllRectBox.Y + CarbonAllRectBox.Height + Gap;

                                _Height += offsetPos.HEIGHT;
               
                                XX += offsetPos.OFFSET_X;

                            }
                            if (true)
                            {
                                    
                                YY +=offsetPos.OFFSET_Y ;

                            }
                            break;
                    }

                    returnBox.Add(new BoundingBox(CarbonAllRectBox.Index_OBD, CarbonAllRectBox.batch_index, CarbonAllRectBox.class_number,
                              XX,
                              YY,
                              _Width,
                              _Height,
                              CarbonAllRectBox.OffSetX,
                              CarbonAllRectBox.OffSetY));
                }

                }
                catch (System.Exception ex) 
                { 

                }
            }
            return returnBox;
        }




        /// <summary>
        /// 4개의 위치에서 속한 포지션에 TRUE를 줌 
        /// 1, 2
        /// 3, 4
        /// </summary>
        /// <param name="InputRegionBox"></param>
        /// <param name="ResultBoxs"></param>
        /// <returns></returns>
        public List<bool> GetResultPosition(BoundingBox InputRegionBox , List<BoundingBox> SearchBoxs)
        {
            List<bool> returnvalue = new List<bool>();

            try
            {
                returnvalue.Add(false);
                returnvalue.Add(false);
                returnvalue.Add(false);
                returnvalue.Add(false);

                double RECT_X = InputRegionBox.X;
                double RECT_Y = InputRegionBox.Y;
                double RECT_WIDTH = InputRegionBox.Width / 2;
                double RECT_HEIGHT = InputRegionBox.Height / 2;

                OpenCvSharp.Rect RECT_1 = new OpenCvSharp.Rect((int)(RECT_X)                , (int)(RECT_Y)              , (int)(RECT_WIDTH) , (int)(RECT_HEIGHT));
                OpenCvSharp.Rect RECT_2 = new OpenCvSharp.Rect((int)(RECT_X + RECT_WIDTH)   , (int)(RECT_Y)              , (int)(RECT_WIDTH) , (int)(RECT_HEIGHT));
                OpenCvSharp.Rect RECT_3 = new OpenCvSharp.Rect((int)(RECT_X)                , (int)(RECT_Y + RECT_HEIGHT), (int)(RECT_WIDTH) , (int)(RECT_HEIGHT));
                OpenCvSharp.Rect RECT_4 = new OpenCvSharp.Rect((int)(RECT_X + RECT_WIDTH)   , (int)(RECT_Y + RECT_HEIGHT), (int)(RECT_WIDTH) , (int)(RECT_HEIGHT));

                foreach (var item in SearchBoxs.Select((Value, index) => (Value, index)))
                {
                    try
                    {
                        OpenCvSharp.Rect rect = new OpenCvSharp.Rect((int)(item.Value.X), (int)(item.Value.Y), (int)(item.Value.Width), (int)(item.Value.Height));

                        double OverlapArea = 0;
                        int MatchingPosition = 0;

                        if (rect.IntersectsWith(RECT_1))
                        {
                            OpenCvSharp.Rect OverlapRect = rect.Intersect(RECT_1);

                            if (OverlapArea < (OverlapRect.Width * OverlapRect.Height))
                            {
                                OverlapArea = (OverlapRect.Width * OverlapRect.Height);
                                MatchingPosition = 0;
                            }
                        }
                        if (rect.IntersectsWith(RECT_2))
                        {
                            OpenCvSharp.Rect OverlapRect = rect.Intersect(RECT_2);

                            if (OverlapArea < (OverlapRect.Width * OverlapRect.Height))
                            {
                                OverlapArea = (OverlapRect.Width * OverlapRect.Height);
                                MatchingPosition = 1;
                            }
                        }
                        if (rect.IntersectsWith(RECT_3))
                        {
                            OpenCvSharp.Rect OverlapRect = rect.Intersect(RECT_3);

                            if (OverlapArea < (OverlapRect.Width * OverlapRect.Height))
                            {
                                OverlapArea = (OverlapRect.Width * OverlapRect.Height);
                                MatchingPosition = 2;
                            }
                        }
                        if (rect.IntersectsWith(RECT_4))
                        {
                            OpenCvSharp.Rect OverlapRect = rect.Intersect(RECT_4);

                            if (OverlapArea < (OverlapRect.Width * OverlapRect.Height))
                            {
                                OverlapArea = (OverlapRect.Width * OverlapRect.Height);
                                MatchingPosition = 3;
                            }
                        }

                        if (OverlapArea > 0) returnvalue[MatchingPosition] = true;
                    }
                    catch
                    {



                    }
                }
            }
            catch
            {



            }
            return returnvalue;
        }

        public abstract void Dispose();
    }

    public partial class RegionParam : IDisposable
    {
        public PointF Offset { get; set; } = new PointF();
        public BoundingBox RegionBox { get; set; } = new BoundingBox();
        public RegionParam()
        {
            this.Clear();
        }
        public RegionParam(RegionParam _Other)
        {
            this.Clear();
            if (_Other.RegionBox != null)
            {
                this.RegionBox = new BoundingBox(_Other.RegionBox);
            }


        }

        public RegionParam(BoundingBox boundingBox)
        {
            this.RegionBox = new BoundingBox(boundingBox);
        }
        public void Clear()
        {
            if (this.RegionBox != null)
                this.RegionBox = new BoundingBox();
            else
                this.RegionBox = new BoundingBox();

            this.Offset = new PointF(0, 0);
        }

        public void Dispose()
        {
            this.RegionBox.Dispose();
        }
    }

    public class ImageSize
    {
        public ImageSize()
        {
            Clear();
        }
        public ImageSize(ImageSize Other)
        {
            Clear();
            this.X = Other.X;
            this.Y = Other.Y;
            this.Width = Other.Width;
            this.Height = Other.Height;
        }
        public ImageSize(double _X, double _Y, double _Width, double _Height)
        {
            this.Clear();
            this.X = _X;
            this.Y = _Y;
            this.Width = _Width;
            this.Height = _Height;
        }
        public ImageSize(BoundingBox _BoundingBox)
        {
            this.Clear();
            this.X = _BoundingBox.X;
            this.Y = _BoundingBox.Y;
            this.Width = _BoundingBox.Width;
            this.Height = _BoundingBox.Height;
        }
        public void Clear()
        {
            this.X = 0;
            this.Y = 0;
            this.Width = 0;
            this.Height = 0;
        }
        public double X { get; set; } = new double();
        public double Y { get; set; } = new double();

        public double Height { get; set; } = new double();
        public double Width { get; set; } = new double();
    }

    public class BoundingBox : IDisposable
    {
        private bool disposedValue;


        /// <summary>
        /// Image Batch 순서에 따른 Index
        /// </summary>
        public int batch_index { get; set; } = new int();


        public int class_number { get; set; } = new int();


        /// <summary>
        /// Carbon OBD 했을때 사사 분면 기준에서 위치값
        /// 0   1
        /// 2   3 
        /// </summary>
        public int Index_OBD { get; set; } = new int();

        /// <summary>
        /// Index_OBD 기준에서 CarBon에 인덱스 
        /// 123 | 321
        /// 123 | 321
        /// </summary>
        public int Index_Carbon { get; set; } = new int();

        public double X { get; set; } = new double();
        public double Y { get; set; } = new double();

        public double OffSetX { get; set; } = new double();
        public double OffSetY { get; set; } = new double();


        public double Width { get; set; } = new double();
        public double Height { get; set; } = new double();
        public double Area { get; set; } = new double();

        public ImageSize InputImageInfo { get; set; } = new ImageSize();
        public List<OpenCvSharp.Point> RegionPoint { get; set; } = new List<OpenCvSharp.Point>();


        public double CenterX
        {
            get
            {
                return X + (Width / 2.0);
            }
        }
        public double CenterY
        {
            get
            {
                return Y + (Height / 2.0);
            }
        }


        public void Clear()
        {
            this.batch_index = 0;
            this.class_number = 0;
            this.X = 0;
            this.Y = 0;
            this.Width = 0;
            this.Height = 0;
            this.Area = 0;
            this.Index_OBD = 0;
            this.Index_Carbon = 0;
            this.OffSetX = 0;
            this.OffSetY = 0;

            if (this.RegionPoint != null)
                this.RegionPoint.Clear();
            else
                this.RegionPoint = new List<OpenCvSharp.Point>();

            this.InputImageInfo = new ImageSize();
        }
        public BoundingBox()
        {
            Clear();
        }
        public BoundingBox(BoundingBox Other)
        {
            this.Clear();
            this.batch_index = Other.batch_index;
            this.class_number = Other.class_number;
            this.X = Other.X;
            this.Y = Other.Y;
            this.Width = Other.Width;
            this.Height = Other.Height;
            this.Area = Other.Area;
            this.Index_OBD = Other.Index_OBD;
            this.Index_Carbon = Other.Index_Carbon;

            this.OffSetX = Other.OffSetX;
            this.OffSetY = Other.OffSetY;
            this.RegionPoint = new List<OpenCvSharp.Point>(Other.RegionPoint);
            this.InputImageInfo = new ImageSize(Other.InputImageInfo);
        }
        public BoundingBox(int _index_OBD, int _batchIndex, int _classNum, double _X, double _Y, double _Width, double _Height)
        {
            this.Clear();
            this.Index_OBD = _index_OBD;
            this.batch_index = _batchIndex;
            this.class_number = _classNum;
            this.X = _X;
            this.Y = _Y;
            this.Width = _Width;
            this.Height = _Height;
            this.Area = 0;
            this.OffSetX = 0;
            this.OffSetY = 0;
        }
        public BoundingBox(int _index_OBD, int _batchIndex, int _classNum, double _X, double _Y, double _Width, double _Height, List<OpenCvSharp.Point> _Regionpoints)
        {
            this.Clear();
            this.Index_OBD = _index_OBD;
            this.batch_index = _batchIndex;
            this.class_number = _classNum;
            this.X = _X;
            this.Y = _Y;
            this.Width = _Width;
            this.Height = _Height;
            this.Area = 0;
            this.OffSetX = 0;
            this.OffSetY = 0;
            this.RegionPoint = new List<OpenCvSharp.Point>(_Regionpoints);
        }
        public BoundingBox(int _index_OBD, int _batchIndex, int _classNum, double _X, double _Y, double _Width, double _Height, double _offsetX, double _offsetY)
        {
            this.Clear();
            this.Index_OBD = _index_OBD;
            this.batch_index = _batchIndex;
            this.class_number = _classNum;
            this.X = _X;
            this.Y = _Y;
            this.Width = _Width;
            this.Height = _Height;
            this.Area = 0;
            this.OffSetX = _offsetX;
            this.OffSetY = _offsetY;
        }
        public BoundingBox(int _index_OBD , int _Index_Carbon, int _batchIndex, int _classNum, double _X, double _Y, double _Width, double _Height, double _offsetX, double _offsetY, List<OpenCvSharp.Point> _Regionpoints , double _Area )
        {
            this.Clear();
            this.Index_OBD = _index_OBD;
            this.Index_Carbon = _Index_Carbon;
            this.batch_index = _batchIndex;
            this.class_number = _classNum;
            this.X = _X;
            this.Y = _Y;
            this.Width = _Width;
            this.Height = _Height;
            this.Area = _Area;
            this.OffSetX = _offsetX;
            this.OffSetY = _offsetY;
            this.RegionPoint = new List<OpenCvSharp.Point>(_Regionpoints);
        }
        public BoundingBox(int _index_OBD, int _batchIndex, int _classNum, double _X, double _Y, double _Width, double _Height, double _offsetX, double _offsetY, List<OpenCvSharp.Point> _Regionpoints, ImageSize _InputImageInfo)
        {
            this.Clear();
            this.Index_OBD = _index_OBD;
            this.batch_index = _batchIndex;
            this.class_number = _classNum;
            this.X = _X;
            this.Y = _Y;
            this.Width = _Width;
            this.Height = _Height;
            this.Area = 0;
            this.OffSetX = _offsetX;
            this.OffSetY = _offsetY;
            this.RegionPoint = new List<OpenCvSharp.Point>(_Regionpoints);
            this.InputImageInfo = new ImageSize(_InputImageInfo);
        }
        public BoundingBox(double _X, double _Y, double _Width, double _Height)
        {
            this.Clear();
            this.X = _X;
            this.Y = _Y;
            this.Width = _Width;
            this.Height = _Height;
            this.OffSetX = 0;
            this.OffSetY = 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.RegionPoint.Clear();
                    // TODO: 관리형 상태(관리형 개체)를 삭제합니다.
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
            }
        }

        // // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        // ~BoundingBox()
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
    };


    [Serializable]
    public class JasOverlay : IDisposable  
    {
        public JasOverlay()
        {
            this.ResultsGraphic.Clear();
            this.Image = null;
        }
        public JasOverlay(JasOverlay Other)
        {
          //  this.Clear();
            this.ResultsGraphic = new CogGraphicCollection(Other.ResultsGraphic);
            this.Image = Other.Image;
            this.FilePath = Other.FilePath;
        }


        [NonSerialized]
        public string FilePath = string.Empty;


        public ICogImage Image;


        public CogGraphicCollection ResultsGraphic { get; set; } = new CogGraphicCollection();
        public void Clear()
        {
            this.ResultsGraphic.Clear();
            if (this.ResultsGraphic != null) this.ResultsGraphic.Dispose();
            this.ResultsGraphic = new CogGraphicCollection();


            if (this.Image != null)
            {
                if (this.Image.GetType().Name == "CogImage8Grey")
                {
                    ((CogImage8Grey)this.Image).Dispose();
                    this.Image = null;
                }
                else if (this.Image.GetType().Name == "CogImage24PlanarColor")
                {
                    ((CogImage24PlanarColor)this.Image).Dispose();
                    this.Image = null;
                }
            }
        }


        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.ResultsGraphic.Dispose();
                    if (this.Image.GetType().Name == "CogImage8Grey")
                    {
                        ((CogImage8Grey)this.Image).Dispose();
                        this.Image = null;
                    }
                    else if (this.Image.GetType().Name == "CogImage24PlanarColor")
                    {
                        ((CogImage24PlanarColor)this.Image).Dispose();
                        this.Image = null;
                    }
                    // TODO: 관리형 상태(관리형 개체)를 삭제합니다.
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
            }
        }

        // // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        // ~JasOverlay()
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
    [Serializable]
    public class JasOverlayList : IDisposable
    {
        public JasOverlayList()
        {
            this.ResultsGraphic.Clear();
            this.Image = null;
        }
        public JasOverlayList(JasOverlayList Other)
        {
            //  this.Clear();
            this.ResultsGraphic.Clear();
            foreach(var item in Other.ResultsGraphic)
            {
                this.ResultsGraphic.Add(new CogGraphicCollection(item));
            }

       //     this.ResultsGraphic = new CogGraphicCollection(Other.ResultsGraphic);
            this.Image = Other.Image;
            this.FilePath = Other.FilePath;
            this.CamNo = Other.CamNo;
            this.TabNum = Other.TabNum;

        }


        [NonSerialized]
        public string FilePath = string.Empty;
        public ICogImage Image;


        public int CamNo { get; set; } = 0;
        public int TabNum { get; set; } = 0;


        public InspectResultData ResultData { get; set; } = new InspectResultData();    

        public List<CogGraphicCollection> ResultsGraphic { get; set; } = new List<CogGraphicCollection>();
        public void Clear()
        {
            this.ResultsGraphic.Clear();
            if (this.ResultsGraphic != null)
            {
                foreach (var item in this.ResultsGraphic)
                {
                    item.Dispose();
                }
            }

            this.ResultsGraphic = new List<CogGraphicCollection>();


            if (this.Image != null)
            {
                if (this.Image.GetType().Name == "CogImage8Grey")
                {
                    ((CogImage8Grey)this.Image).Dispose();
                    this.Image = null;
                }
                else if (this.Image.GetType().Name == "CogImage24PlanarColor")
                {
                    ((CogImage24PlanarColor)this.Image).Dispose();
                    this.Image = null;
                }
            }

            this.CamNo = 0;
            this.TabNum = 0;
            this.ResultData.Clear();
        }


        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (this.ResultsGraphic != null)
                    {
                        foreach (var item in this.ResultsGraphic)
                        {
                            item.Dispose();
                        }
                    }
                    if (this.Image.GetType().Name == "CogImage8Grey")
                    {
                        ((CogImage8Grey)this.Image).Dispose();
                        this.Image = null;
                    }
                    else if (this.Image.GetType().Name == "CogImage24PlanarColor")
                    {
                        ((CogImage24PlanarColor)this.Image).Dispose();
                        this.Image = null;
                    }
                    // TODO: 관리형 상태(관리형 개체)를 삭제합니다.
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
            }
        }

        // // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        // ~JasOverlay()
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

    public class ImageParam : IDisposable 
    {
        private bool disposedValue;

        public ImageParam()
        {
            this.Clear();
        }
        public ImageParam(ImageParam other)
        {
            this.Clear();
            this.Image = new WpfImage(other.Image.Bitmap.ToBitmapSource());

            if (other.Region.Count > 0)
            {
                foreach (var item in other.Region)
                {
                    this.Region.Add(new RegionParam(item));
                }
            }
            else 
            {
                this.Region = new List<RegionParam>();
            }

        }
        public List<RegionParam> Region { get; set; } = new List<RegionParam>();
        public WpfImage Image { get;  set; }

        public int InputOBDCount { get; set; }

        public void Clear()
        {
            if (this.Image != null) this.Image.Bitmap.Dispose();
            this.Image = null ;

            if (this.Region != null) this.Region.Clear();
            this.Region = new List<RegionParam>();
            this.InputOBDCount = 0;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Clear();
                    if (this.Image != null) this.Image.Bitmap.Dispose();
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
            }
        }

        // // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        // ~ImageParam()
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



}
