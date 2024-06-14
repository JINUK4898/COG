using Cognex.VisionPro;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using OpenCvSharp;
using static COG.JasAIToolBase;
using nrt;
using System.Windows.Media;
using System.Web.UI.HtmlControls;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using System.Windows.Documents;
using OpenCvSharp.Flann;
using static COG.JasAIResultparam;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Windows.Media.Animation;
using System.ServiceModel.Syndication;
using ViDi2.VisionPro;
using ViDi2;
using ViDi2.VisionPro;
using ViDi2.VisionPro.Records;
using IControl = ViDi2.Runtime.IControl;
using IStream = ViDi2.Runtime.IStream;
using IWorkspace = ViDi2.Runtime.IWorkspace;

using ViDi2.Runtime;
using ViDi2.Training;
using System.Text.RegularExpressions;
using Google.Protobuf.WellKnownTypes;
using ViDi2.UI;
//using ViDi2.UI;


namespace COG
{    
    [Serializable]
    public abstract class JasAIToolBase //: //ISerializable
    {

        #region Constructor       
        public JasAIToolBase()// : //base()
        {
            this.Draw = false;
            this.Initial();
        }
        public JasAIToolBase(JasAIToolBase Other) //: //base(Other)
        {
            this.Draw = Other.Draw;
            this.Initial();
        }
        #endregion //Constructor 생성자


        #region Event & Delegate

        #endregion //Event & Delegate


        #region  Field

        #endregion //Field 필드


        #region Property

        #endregion //Property 속성


        #region Method

        #endregion //Method 메서드

        public string Subjectname { get; set; } = null;


        private ViDi2.ITool _subject;
        public ViDi2.ITool Subject 
        { 
            get
            {
                return this._subject;
            }
            set
            {
                this._subject = value;
            }
        } 

        public Object status { get; set; } = null;

        public bool Draw { get; set; } = false;

        public ImageParam Input { get; set; } = new ImageParam();

        public ImageParam Output { get; set; } = new ImageParam();
        
        public abstract void Initial();

        public abstract void Clear();

        public abstract void Load(string filePath);

        public abstract void Save(string filePath);

        public abstract void Run(int IndexNum , Object ResultData);

        public abstract void GetResultsGraphic(int IndexNum);
        public abstract void SetInputImage(object _InputImage);
        public abstract void SetInputImage(ImageParam _InputImage);

        public abstract void SetInputImage(object _InputImage , List<RegionParam> _Region);

        public JasAIResultparam[] ResultParam { get; set; }  = new JasAIResultparam[Main.ToolMaxCount];

        public JasAIRunparam RunParam { get; set; } = new JasAIRunparam ();

        public double ProcessTime;

        public abstract void Dispose();

    }

    public partial class NeurocleAITool : JasAIToolBase //, IDisposable
    {
        public NeurocleAITool()
        {
            this.Initial();
        }
        public NeurocleAITool(string _subjectname)
        {
            this.Initial();
            this.Subjectname = _subjectname;    
        }
        public override void Clear()
        {          
            this.status = new nrt.Status();
            this.Output.Clear();
        }
        public override void Initial()
        {
            this.Subjectname = string.Empty;
        //    this.Subject = new nrt.Predictor();
            this.status = new nrt.Status();


            this.Input = new ImageParam();
            this.Output = new ImageParam();



            this.ResultParam = new JasAIResultparam[Main.ToolMaxCount]; 
            foreach(var item in this.ResultParam.Select((value , index )=> (value, index)))
            {
                this.ResultParam[item.index] = new JasAIResultparam();
            }

            this.RunParam = new JasAIRunparam();
        }
        public override void Load(string filePath)
        {
            //var workspace = Subject.Control.Workspaces.Add(System.IO.Path.GetFileNameWithoutExtension(filePath), filePath);
            //
            //if (Subject.IsRuntimeWorkspaceValid(workspace))
            //{
            //    Subject.Workspace = workspace;
            //    Subject.WorkspaceName = System.IO.Path.GetFileName(filePath);
            //}
            //else
            //{
            //    Subject.Control.Workspaces.Remove(workspace.UniqueName);
            //
            //}
        }
        public override void Save(string filePath)
        {
            //nrt.Status status = new nrt.Status();
            //status = ((nrt.Predictor)this.Subject).save_predictor(filePath);
            //if (status != nrt.Status.STATUS_SUCCESS)
            //{
            //    throw new Exception("Predictor save failed");
            //}
        }
        public override void Run(int indexNum , Object ResultData = null )      
        {
            this.Clear();     
            this.ResultParam[indexNum].Clear();

            if (ResultData == null)
            {
                var isample = this.Subject.Process(this.Input.Image);
                this.ResultParam[indexNum].results = isample.Markings;
            }
            else
            {
                this.ResultParam[indexNum].results = (Dictionary<string, IMarking>)ResultData;
            }



            this.ResultParam[indexNum].Input = new ImageParam(Input);

           if (this.ResultParam[indexNum].results.Count <= 0)   
           {
               
           }
           else
           {
               this.ResultParam[indexNum].SearchResultRun = true;
           }
        }

        public override void SetInputImage(object _InputImage)
        {
            this.SetInputImage(_InputImage, new List<RegionParam>());
        }
        public override void SetInputImage(ImageParam _Input)
        {
            this.SetInputImage(_Input.Image, _Input.Region);
        }
        public override void SetInputImage(object _InputImage, List<RegionParam> _Region)
        {
            this.Input.Clear();

            if (_InputImage.GetType().Name == "String")
            {
                Input.Image = new WpfImage(_InputImage.ToString());
            }
            else if (_InputImage.GetType().Name == "WpfImage")
            {
                Input.Image = (ViDi2.UI.WpfImage)_InputImage;
            }







            //if (_InputImage.GetType().Name == "String")
            // {
            //     ((nrt.Input)Input.Image).extend(_InputImage.ToString(), 3);
            // }
            // else if (_InputImage.GetType().Name == "NDBuffer")
            // {
            //     ((nrt.Input)Input.Image).extend((nrt.NDBuffer)_InputImage);
            //
            // }
            // else if(_InputImage.GetType().Name ==  "CogImage24PlanarColor")
            // {
            //     ((nrt.Input)Input.Image).extend((nrt.NDBuffer) VisionProHelper.GetNDbuffer((ICogImage)_InputImage));
            // }
            // else
            // {
            //     Input.Image = new nrt.Input((nrt.Input)_InputImage);
            // }
            //

            try
            {
                if (_Region != null &&  _Region.Count() > 0)
                {
                    if (_Region.Count() > 0)
                    {
                        foreach (var item in _Region)
                        {
                            this.Input.Region.Add(new RegionParam(item));
                        }
                    }
                    else
                    {
                        this.Input.Region.Add(new RegionParam());
                    }
                }
                else
                {
                    int Height = Input.Image.Height;
                    int _Width = Input.Image.Width;
                    this.Input.Region.Add(new RegionParam(new BoundingBox(0, 0, _Width, Height)));
                }
            }
            catch
            {

            }


        }

        private Main.MTickTimer m_Timer_index = new Main.MTickTimer();

        public override void GetResultsGraphic(int indexNum)
        {


            switch (this.Subject.Name)
            {
                case "Blue":
                case "Locate":
                    if (true) 
                    { 
                        var MarkingTool = this.ResultParam[indexNum].results[this.Subject.Name] as IBlueMarking;
                        foreach (var Markingitems in MarkingTool.Views)
                        {
                            int GetCount = Markingitems.Features.Count;

                            if (GetCount > 0)
                            {
                                #region Sorting Image Index
                                List<PointF> BeforeSortpoints = new List<PointF>();
                                List<int> OBD_Position = new List<int>();

                                for (int i = 0; i < GetCount; i++)
                                {
                                    var items = Markingitems.Features[i];

                                    BeforeSortpoints.Add(new PointF((float)(items.Position.X - (items.Size.Width / 2)), (float)(items.Position.Y - (items.Size.Height / 2))));

                                    List<BoundingBox> TempBox = new List<BoundingBox>();
                                    TempBox.Add(new BoundingBox(i, Convert.ToInt16(items.Name), 0, items.Position.X - (items.Size.Width / 2), items.Position.Y - (items.Size.Height / 2), items.Size.Width, items.Size.Height));
                                    var returnValue = this.ResultParam[indexNum].GetResultPosition(this.Input.Region[0].RegionBox, TempBox);

                                    foreach (var itembool in returnValue.Select((value, Index) => (value, Index)))
                                    {
                                        if (itembool.value == true)
                                        {
                                            OBD_Position.Add(itembool.Index);
                                        }
                                    }
                                    TempBox.Clear();


                                    //items.Dispose();
                                }
                                List<int> AfterSortIndex = this.ResultParam[indexNum].SortingOfIndex(BeforeSortpoints);
                                #endregion

                                for (int i = 0; i < GetCount; i++)
                                {
                                    var items = Markingitems.Features[AfterSortIndex[i]];


                                    int imageIndex = 0;

                                    //     PointF Offset = new PointF((float)this.Input.Region[imageIndex].RegionBox.X, (float)this.Input.Region[imageIndex].RegionBox.Y);
                                    PointF Offset = new PointF();// (float)this.Input.Region[imageIndex].RegionBox.X, (float)this.Input.Region[imageIndex].RegionBox.Y);

                                    var contourPoints = new List<OpenCvSharp.Point>();

                                    double AreaValue = items.Size.Width * items.Size.Height;

                                    if (AreaValue > this.RunParam.SpecWidth.Min && this.RunParam.SpecWidth.Min < items.Size.Width && this.RunParam.SpecWidth.Max > items.Size.Width)
                                    {
                                        #region CropImage

                                        contourPoints.Add(new OpenCvSharp.Point(items.Position.X - (items.Size.Width / 2), items.Position.Y - (items.Size.Height / 2)));
                                        contourPoints.Add(new OpenCvSharp.Point(items.Position.X - (items.Size.Width / 2), items.Position.Y + (items.Size.Height / 2)));
                                        contourPoints.Add(new OpenCvSharp.Point(items.Position.X + (items.Size.Width / 2), items.Position.Y + (items.Size.Height / 2)));
                                        contourPoints.Add(new OpenCvSharp.Point(items.Position.X + (items.Size.Width / 2), items.Position.Y - (items.Size.Height / 2)));
                                        ///-----------------------------------------------------------------------------------------------------------------
                                        CogGraphicLabel label = new CogGraphicLabel();
                                        label.SetXYText(items.Position.X, items.Position.Y, OBD_Position[AfterSortIndex[i]].ToString());

                                        this.ResultParam[indexNum].ResultsGraphicOffset.Add(new CogGraphicLabel(label));
                                        this.ResultParam[indexNum].ResultsGraphic.Add(new CogGraphicLabel(label));
                                        this.ResultParam[indexNum].BatchID.Add(OBD_Position[AfterSortIndex[i]]);
                                        label.Dispose();
                                        ///-----------------------------------------------------------------------------------------------------------------
                                        OpenCvSharp.Rect roi = new OpenCvSharp.Rect((int)(items.Position.X - (items.Size.Width / 2)), (int)(items.Position.Y - (items.Size.Height / 2)), (int)items.Size.Width, (int)items.Size.Height);
                                        //Mat TempCropImage = new Mat(VisionProHelper.GetMatimage(((nrt.Input)this.Input.Image).get_org_input_ndbuff(items.batch_idx)), roi).Clone();
                                        this.Output.Image = new WpfImage(this.Input.Image.BitmapSource);

                                        ImageSize InputImageboundingBox = new ImageSize(this.Input.Region[0].RegionBox);
                                        this.Output.Region.Add(new RegionParam(new BoundingBox(OBD_Position[AfterSortIndex[i]], Convert.ToInt16(items.Name), 0, items.Position.X - (items.Size.Width / 2), items.Position.Y - (items.Size.Height / 2), items.Size.Width, items.Size.Height, items.Position.X - (items.Size.Width / 2), items.Position.Y - (items.Size.Height / 2), contourPoints, InputImageboundingBox)));


                                        //TempCropImage.Dispose();








                                        #endregion

                                        ///-----------------------------------------------------------------------------------------------------------------
                                        ///
                                        CogRectangle _RegionBox = new CogRectangle();
                                        _RegionBox.Color = CogColorConstants.Orange;
                                        _RegionBox.SetXYWidthHeight(items.Position.X - (items.Size.Width / 2), items.Position.Y - (items.Size.Height / 2), items.Size.Width, items.Size.Height);
                                        _RegionBox.TipText = items.Name;
                                        this.ResultParam[indexNum].ResultsGraphicOffset.Add(new CogRectangle(_RegionBox));
                                        this.ResultParam[indexNum].ResultsGraphic.Add(new CogRectangle(_RegionBox));
                                        this.ResultParam[indexNum].BatchID.Add(OBD_Position[AfterSortIndex[i]]);
                                        _RegionBox.Dispose();
                                        this.ResultParam[indexNum].Boxs.Add(new BoundingBox(OBD_Position[AfterSortIndex[i]], i, Convert.ToInt16(items.Name), 0, items.Position.X - (items.Size.Width / 2), items.Position.Y - (items.Size.Height / 2), items.Size.Width, items.Size.Height, 0, 0, contourPoints, AreaValue));
                                    }
                                    contourPoints.Clear();
                                    //items.Dispose();



                                }
                            }
                            this.ResultParam[indexNum].ResultOBD = this.ResultParam[indexNum].GetResultPosition(this.Input.Region[0].RegionBox, this.ResultParam[indexNum].Boxs);
                        }
                    }
                    break;


                case "RedHighDetail":
                case "용액":
                case "카본":

                    if (true)
                    {
                        var MarkingTool = this.ResultParam[indexNum].results[this.Subject.Name] as IRedMarking;
                        List<int> AfterSortIndex = new List<int>();
                        List<PointF> BeforeSortpoints = new List<PointF>();
                        List<int> batch_idx = new List<int>();
                        List<int> OBD_Position = new List<int>();

                        List<IRegion> ResultRegions = new List<IRegion>();

                        foreach (var Markingitems in MarkingTool.Views.Select((value, index) => (value, index)))
                        {
                            foreach (var MarkingRegion in Markingitems.value.Regions.Select((value2, index2) => (value2, index2)))
                            {
                                ResultRegions.Add(MarkingRegion.value2);
                            }
                        }


                //       CogLine cogLineW = new CogLine();
                //       cogLineW.SetXYRotation(this.Input.Image.Width / 2, this.Input.Image.Height / 2, 0);
                //       CogLine cogLineH = new CogLine();
                //       cogLineH.SetXYRotation(this.Input.Image.Width / 2, this.Input.Image.Height / 2, CogMisc.DegToRad( 90));
                //
                //       this.ResultParam[indexNum].ResultsGraphicOffset.Add(new CogLine(cogLineW));
                //       this.ResultParam[indexNum].ResultsGraphicOffset.Add(new CogLine(cogLineH));



                        int i = 0;
                        foreach (var Markingitems in MarkingTool.Views.Select((value, index) => (value, index)))
                        {
                            foreach (var MarkingRegion in Markingitems.value.Regions.Select((value2, index2) => (value2, index2)))
                            {
                                var items = ResultRegions[i];
                                int _batchID = Convert.ToInt16(Markingitems.index);


                                PointF Offset = new PointF((float)Markingitems.value.Pose.OffsetX, (float)(float)Markingitems.value.Pose.OffsetY);
                                BeforeSortpoints.Add(new PointF((float)(items.Center.X - (items.Width / 2)), (float)(items.Center.Y - (items.Height / 2))));
                                batch_idx.Add(_batchID);

                                List<BoundingBox> TempBox = new List<BoundingBox>();
                                TempBox.Add(new BoundingBox(i, _batchID, 0, items.Center.X - (items.Width / 2) + Offset.X, items.Center.Y - (items.Height / 2) + Offset.Y, items.Width, items.Height));

                                BoundingBox InpuImageInfo = new BoundingBox(
                                                                            this.Input.Region[0].RegionBox.X,
                                                                            this.Input.Region[0].RegionBox.Y,
                                                                            this.Input.Region[0].RegionBox.Width,
                                                                            this.Input.Region[0].RegionBox.Height);

                                var returnValue = this.ResultParam[indexNum].GetResultPosition(InpuImageInfo, TempBox);

                                foreach (var itembool in returnValue.Select((value, Index) => (value, Index)))
                                {
                                    if (itembool.value == true)
                                    {
                                        OBD_Position.Add(itembool.Index);
                                    }
                                }
                                TempBox.Clear();                               
                                //items.Dispose();
                                AfterSortIndex.Add(i);
                                i++;
                            }
                        }

                        if (this.Subjectname == "카본")
                            AfterSortIndex = this.ResultParam[indexNum].SortingOfIndex(BeforeSortpoints, batch_idx, OBD_Position);

                        //------------------------
                         i = 0;
                        //------------------------
                        foreach (var Markingitems in MarkingTool.Views.Select((value, index) => (value, index)))
                        {
                            foreach (var MarkingRegion in Markingitems.value.Regions.Select((value2, index2) => (value2, index2)))
                            {
                                var items = ResultRegions[AfterSortIndex[i]];
                                var Contours = ResultRegions[AfterSortIndex[i]].Outer;
               
                                int _batchID = Convert.ToInt16(Markingitems.index);
                     
                                //---------------------------------------------------------------------------------------------------------------------------------------------------------
                                PointF Offset = new PointF((float)Markingitems.value.Pose.OffsetX, (float)(float)Markingitems.value.Pose.OffsetY);

                                    double AreaValue = items.Area;
                                    if (AreaValue > this.RunParam.SpecArea.Min)
                                    {
                                        var contourPoints = new List<OpenCvSharp.Point>();

                                        bool CarbonregionBox = false;

                                        if (this.Subjectname == "카본" && CarbonregionBox)
                                        {
                                            contourPoints.Add(new OpenCvSharp.Point(items.Center.X - (items.Width / 2), items.Center.Y - (items.Height / 2)));
                                            contourPoints.Add(new OpenCvSharp.Point(items.Center.X - (items.Width / 2), items.Center.Y + (items.Height / 2)));
                                            contourPoints.Add(new OpenCvSharp.Point(items.Center.X + (items.Width / 2), items.Center.Y + (items.Height / 2)));
                                            contourPoints.Add(new OpenCvSharp.Point(items.Center.X + (items.Width / 2), items.Center.Y - (items.Height / 2)));

                                            CogRectangle _RegionBox = new CogRectangle();
                                            _RegionBox.SetXYWidthHeight(items.Center.X - (items.Width / 2), items.Center.Y - (items.Height / 2), items.Width, items.Height);
                                            //           this.ResultParam[indexNum].ResultsGraphic.Add(new CogRectangle(_RegionBox));
                                            //           this.ResultParam[indexNum].BatchID.Add(OBD_Position[AfterSortIndex[i]]);
                                            _RegionBox.Dispose();

                                            CogRectangle _RegionBoxOffset = new CogRectangle();
                                            _RegionBoxOffset.Color = CogColorConstants.Magenta;
                                            _RegionBoxOffset.SetXYWidthHeight((items.Center.X - (items.Width / 2)) + Offset.X, (items.Center.Y - (items.Height / 2)) + Offset.Y, items.Width, items.Height);
                                            //             this.ResultParam[indexNum].ResultsGraphicOffset.Add(new CogRectangle(_RegionBoxOffset));
                                            _RegionBoxOffset.Dispose();

                                            CogGraphicLabel label = new CogGraphicLabel();
                                            label.BackgroundColor = CogColorConstants.Grey;
                                            label.SetXYText((items.Center.X - (items.Width / 2)) + Offset.X, (items.Center.Y - (items.Height / 2)) + Offset.Y, OBD_Position[AfterSortIndex[i]].ToString() + " , " + i.ToString());
                                                   this.ResultParam[indexNum].ResultsGraphicOffset.Add(new CogGraphicLabel(label));

                                        //            this.ResultParam[indexNum].ResultsGraphic.Add(new CogGraphicLabel(label));
                                        //            this.ResultParam[indexNum].BatchID.Add(OBD_Position[AfterSortIndex[i]]);
                                        label.Dispose();
                                        }
                                        else
                                        {
                                            CogPolygon _Region = new CogPolygon();
                                            CogPolygon _Region_Offset = new CogPolygon();
                                            _Region_Offset.Color = CogColorConstants.Blue;
                                            //용액 색상 


                                            //        m_Timer_index.StartTimer();

                                            for (int j = 0; j < (int)Contours.Count(); j++)
                                            {
                                                _Region.AddVertex(Contours[j].X + items.Center.X, Contours[j].Y + items.Center.Y, j);
                                                _Region_Offset.AddVertex(Contours[j].X + Offset.X, Contours[j].Y + Offset.Y, j);
                                                contourPoints.Add(new OpenCvSharp.Point(Contours[j].X, Contours[j].Y));
                                            }


                                            CogGraphicLabel label = new CogGraphicLabel();
                                            label.BackgroundColor = CogColorConstants.Grey;
                                 //           label.SetXYText(items.Center.X - (items.Width / 2), items.Center.Y - (items.Height / 2), OBD_Position[AfterSortIndex[i]].ToString() + " , " + i.ToString());
                                            label.SetXYText((items.Center.X - (items.Width / 2)) + Offset.X, (items.Center.Y - (items.Height / 2)) + Offset.Y, OBD_Position[AfterSortIndex[i]].ToString() + " , " + i.ToString());
                                //        this.ResultParam[indexNum].ResultsGraphicOffset.Add(new CogGraphicLabel(label));
                                        //               this.ResultParam[indexNum].ResultsGraphic.Add(new CogGraphicLabel(label));
                                        //               this.ResultParam[indexNum].BatchID.Add(OBD_Position[AfterSortIndex[i]]);
                                        label.Dispose();

                                            #region 
#if true
                                            // Use ConvexHull
                                            if (this.Subjectname == "카본")
                                            {
                                                var CVConvexPoints = Cv2.ConvexHull(contourPoints, true);
                                                _Region.Dispose();
                                                _Region = new CogPolygon();
                                                _Region_Offset.Dispose();
                                                _Region_Offset = new CogPolygon();
                                                // 카본 색상
                                                _Region_Offset.Color = CogColorConstants.Orange;

                                                contourPoints.Clear();
                                                contourPoints = new List<OpenCvSharp.Point>();
                                                for (int j = 0; j < (int)CVConvexPoints.Length; j++)
                                                {
                                                    _Region.AddVertex(CVConvexPoints[j].X, CVConvexPoints[j].Y, j);
                                                    _Region_Offset.AddVertex(CVConvexPoints[j].X + Offset.X, CVConvexPoints[j].Y + Offset.Y, j);

                                                    contourPoints.Add(new OpenCvSharp.Point(CVConvexPoints[j].X, CVConvexPoints[j].Y));
                                                }
                                            }
#endif
                                            #endregion

                                            //        this.ResultParam[indexNum].ResultsGraphic.Add(new CogPolygon(_Region));
                                            //        this.ResultParam[indexNum].BatchID.Add(OBD_Position[AfterSortIndex[i]]);
                                            this.ResultParam[indexNum].ResultsGraphicOffset.Add(new CogPolygon(_Region_Offset));

                                            _Region.Dispose();
                                            _Region_Offset.Dispose();
                                            //            Trace.WriteLine("Time: " + m_Timer_index.GetEllapseTime());
                                        }


                                        this.ResultParam[indexNum].Boxs.Add(new BoundingBox(OBD_Position[AfterSortIndex[i]], i, _batchID, 0, items.Center.X - (items.Width / 2), items.Center.Y - (items.Height / 2), items.Width, items.Height, Offset.X, Offset.Y, contourPoints, AreaValue));

                                        contourPoints.Clear();
                                    }

                                    //Contours.Dispose();
                                    //items.Dispose();
                                
                                i++;
                            }
                        }

                        if (this.Subjectname == "카본")
                        {
#if true
                            foreach (var itemsbox in this.ResultParam[indexNum].Get_CarbonAddAllBoxs().Select((value, index) => (value, index)))
                            {
                                CogRectangle _RegionBox = new CogRectangle();
                                _RegionBox.SetXYWidthHeight(itemsbox.value.X, itemsbox.value.Y, itemsbox.value.Width, itemsbox.value.Height);
                                _RegionBox.Color = CogColorConstants.Magenta;
                                // 큰 카본 박스 색상
                                this.ResultParam[indexNum].ResultsGraphic.Add(new CogRectangle(_RegionBox));
                                this.ResultParam[indexNum].BatchID.Add(itemsbox.value.Index_OBD);



                                _RegionBox.SetXYWidthHeight(itemsbox.value.X + itemsbox.value.OffSetX, itemsbox.value.Y + itemsbox.value.OffSetY, itemsbox.value.Width, itemsbox.value.Height);
                                _RegionBox.Color = CogColorConstants.Green;
                                this.ResultParam[indexNum].ResultsGraphicOffset.Add(new CogRectangle(_RegionBox));

                                _RegionBox.Dispose();
                                ///-----------------------------------------------------------------------------------------------------------------
                                ///
                                CogGraphicLabel label = new CogGraphicLabel();
                                label.SetXYText(itemsbox.value.X, itemsbox.value.Y, itemsbox.value.Index_OBD.ToString() + itemsbox.index.ToString());
                                //           this.ResultParam[indexNum].ResultsGraphic.Add(new CogGraphicLabel(label));
                                //           this.ResultParam[indexNum].BatchID.Add(itemsbox.value.Index_OBD);
                                label.Dispose();
                                ///-----------------------------------------------------------------------------------------------------------------
                                ///                            
                                /// 
                                /// 
                            }
#if false
                                for (int jj = 0; jj < this.ResultParam[indexNum].Get_batchGroupBoxsCount; jj++)
                                {
                                    int Index_OBD = this.ResultParam[indexNum].Convertbatch_to_OBDNumber(jj);
                                    foreach (var itemsbox in this.ResultParam[indexNum].Get_CarbonToCarbonBox(Index_OBD, (int)RunParam.CarbonToCarbon_DIV.Max).Select((Value2, index2) => (Value2, index2)))
                                    { 
                                        CogRectangle _RegionBox = new CogRectangle();
                                        _RegionBox.Color = CogColorConstants.Green;
                                        _RegionBox.SetXYWidthHeight(itemsbox.Value2.X, itemsbox.Value2.Y, itemsbox.Value2.Width, itemsbox.Value2.Height);
                    //                    this.ResultParam[indexNum].ResultsGraphic.Add(new CogRectangle(_RegionBox));
                    //                    this.ResultParam[indexNum].BatchID.Add(itemsbox.Value2.Index_OBD);

                                        _RegionBox.Dispose();
                                        ///-----------------------------------------------------------------------------------------------------------------
                                        ///
                                        CogGraphicLabel label = new CogGraphicLabel();
                                        label.SetXYText(itemsbox.Value2.X, itemsbox.Value2.Y, itemsbox.Value2.Index_OBD.ToString() +","+ itemsbox.index2.ToString());
                    //                     this.ResultParam[indexNum].ResultsGraphic.Add(new CogGraphicLabel(label));
                    //                     this.ResultParam[indexNum].BatchID.Add(itemsbox.Value2.Index_OBD);
                                        label.Dispose();
                                    }
                                        ///-----------------------------------------------------------------------------------------------------------------
                                    
                                }
#endif
#endif
                        }

                    }
                    break;

            }
        }
        private bool disposedValue;



        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                   // ((nrt.Predictor)this.Subject).Dispose();

                    this.Input.Dispose(); 
                    this.Output.Dispose();

                    foreach (var item in this.ResultParam.Select((value, index) => (value, index)))
                    {
             //           this.ResultParam[item.index].results.Dispose();
                        this.ResultParam[item.index].Clear();
                    }
                    this.RunParam.Dispose();
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;
            }
        }

        // // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        // ~NeurocleAITool()
        // {
        //     // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
        //     Dispose(disposing: false);
        // }

        public override void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

}//
