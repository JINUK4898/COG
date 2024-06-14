using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ToolBlock;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


using OpenCvSharp;
using OpenCvSharp.Internal.Vectors;
using OpenCvSharp.Extensions;
using System.Web;
using System.Windows.Media.Media3D;
using nrt;
using static OpenCvSharp.XImgProc.CvXImgProc;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Documents;
using System.Windows;
using OpenCvSharp.Internal;
using System.Windows.Input;
using JAS;
using System.Drawing.Drawing2D;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using ViDi2.UI;
using CognexAI;

namespace COG
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {


            ViDi2.VisionPro.Compatibility.EnableVisionProVersionCompatibility();

        

            var control = new ViDi2.Runtime.Local.Control();


           // if (Form1._vProIntegration != null)
           // {
           //     Form1._vProIntegration.Control = control;
           // }
            if (iNSPECT.VProAIControl != null)
            {
                iNSPECT.VProAIControl.Control = control;
            }


            InitializeComponent();
            Displays.Add(cogRecordDisplay1);
            Displays.Add(cogRecordDisplay2);
            Displays.Add(cogRecordDisplay3);
            Displays.Add(cogRecordDisplay4);

            cogDisplayStatusBar00.Display = MainDisplay;


            LB_SideBox.Add(LB_SIDE_00);
            LB_SideBox.Add(LB_SIDE_01);
            LB_SideBox.Add(LB_SIDE_02);
            LB_SideBox.Add(LB_SIDE_03);
            LB_SideBox.Add(LB_SIDE_04);
            LB_SideBox.Add(LB_SIDE_05);
            LB_SideBox.Add(LB_SIDE_06);
            LB_SideBox.Add(LB_SIDE_07);
            LB_SideBox.Add(LB_SIDE_08);
            LB_SideBox.Add(LB_SIDE_09);
            LB_SideBox.Add(LB_SIDE_10);
            LB_SideBox.Add(LB_SIDE_11);
            LB_SideBox.Add(LB_SIDE_12);
            LB_SideBox.Add(LB_SIDE_13);
            LB_SideBox.Add(LB_SIDE_14);
            LB_SideBox.Add(LB_SIDE_15);


            const int ITEM1 = 0b0000000000001111; // 항목 1: 에러 유형 1~4 (비트 0~3)
            const int ITEM2 = 0b0000000011110000; // 항목 2: 에러 유형 5~8 (비트 4~7)
            const int ITEM3 = 0b0000111100000000; // 항목 3: 에러 유형 9~12 (비트 8~11)
            const int ITEM4 = 0b1111000000000000; // 항목 4: 에러 유형 13~16 (비트 12~15)


            int CCC = 15 ;



            var A1 = CheckErrorForItem(ITEM1, CCC);
            var A2 = CheckErrorForItem(ITEM2, CCC);
            var A3 = CheckErrorForItem(ITEM3, CCC);
            var A4 = CheckErrorForItem(ITEM4, CCC);
        }
        static bool CheckErrorForItem(int errorValue, int itemMask)
        {
            if ((errorValue & itemMask) != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }




        // static VisionProIntegration _vProIntegration = new VisionProIntegration();



        public List<Label> LB_SideBox = new List<Label>();

        private static INSPECT iNSPECT = new INSPECT();


        public List<CogRecordDisplay> Displays = new List<CogRecordDisplay>();


        SystemFile _Systemfile = new SystemFile();

        private void Form1_Load(object sender, EventArgs e)
        {
            Main.ImageScale = (double)NUD_IMAGE_SCALE.Value;

            JsonConvertHelper.LoadToExistingTarget(_Systemfile.FileName, ref _Systemfile);

            imageFileLoadTool1.ButtonClick += ImageFileLoadTool1_ButtonClick;


            try
            {
                System.IO.DirectoryInfo directoryInfo = new DirectoryInfo(_Systemfile.ImageFolderPath);

                if (directoryInfo.Exists)
                {
                    imageFileLoadTool1.Load(_Systemfile.ImageFolderPath);
                    ImageFileLoadTool1_ButtonClick(null,null);
                }
            }
            catch
            {

            }

            try
            {
                System.IO.FileInfo directoryInfo = new System.IO.FileInfo(_Systemfile.Toolpath1);
                if (directoryInfo.Exists)
                {
                    iNSPECT.VProAIControl.Load(directoryInfo.FullName);
                    BTN_TOOL_1.BackColor = Color.Green;


                   var tools = iNSPECT.VProAIControl.GetTools();

                   if(tools.Count > 0)
                    {


                        foreach( var tool in tools )
                        {

                            if(NeurocleTool0.Subjectname == tool.Name)
                            {
                                NeurocleTool0.Subject = tool;
                            }
                            if (NeurocleTool1.Subjectname == tool.Name)
                            {
                                NeurocleTool1.Subject = tool;
                            }
                            if (NeurocleTool2.Subjectname == tool.Name)
                            {
                                NeurocleTool2.Subject = tool;
                            }

                        }

                        
                    }


                }
            }
            catch
            {

            }

#if false
            try
            {
                System.IO.FileInfo directoryInfo = new FileInfo(_Systemfile.Toolpath2);
                if (directoryInfo.Exists)
                {
                    PredictorLoad(NeurocleTool1, directoryInfo.FullName);
                    BTN_TOOL_2.BackColor = Color.Green;
                }
            }
            catch
            {

            }
            try
            {
                System.IO.FileInfo directoryInfo = new FileInfo(_Systemfile.Toolpath3);
                if (directoryInfo.Exists)
                {
                    PredictorLoad(NeurocleTool2, directoryInfo.FullName); ;
                    BTN_TOOL_3.BackColor = Color.Green;
                }
            }
            catch
            {

            }
#endif
            try
            {
                BTN_LOAD_INSPECT_Click(null,null);
            }
            catch
            {

            }


            uC_ResultView1.LogFolderPath =  "D:\\Systemdata_OHC_InLine_BGM_INSPECTION_PC1\\logdata\\";

        }

        private void UC_ResultView1_ClickEventHandle(int CamNo, int Index, int TotalIndex)
        {

        }

        private ICogImage GetImage(string _ImagePath)
        {
            CogImageFileTool _cogImageFileTool = new CogImageFileTool();
            _cogImageFileTool.Operator.Open(_ImagePath, CogImageFileModeConstants.Read);
           _cogImageFileTool.Run();
            return _cogImageFileTool.OutputImage;
        }

        private void  DisplaySetImage (Cognex.VisionPro.CogRecordDisplay _Display , ICogImage _Image)
        {
            _Display.Image = _Image;           
        }
        private void DisplayClear(Cognex.VisionPro.CogRecordDisplay _Display)
        {
            _Display.StaticGraphics.Clear();
        }
        private void DisplaySetGraphic(Cognex.VisionPro.CogRecordDisplay _Display , CogGraphicCollection cogGraphicCollection , ICogImage cogImage  = null)
        {

            if(cogImage != null)
            {
                _Display.Image = cogImage;
            }

            foreach (ICogGraphic item in cogGraphicCollection)
            {
                item.Color = CogColorConstants.Red;
                _Display.StaticGraphics.Add(item, "");
            }
        }
        private void DisplaySetGraphic(List<BoundingBox> _Box , CogGraphicCollection cogGraphicCollection , JasAIResultparam jasAIResultparam)
        {
            Cognex.VisionPro.CogRecordDisplay _Display = null;
            int i = 0;

            for (int j = 0; j < Displays.Count; j++)
            {
     //           Displays[j].StaticGraphics.Clear();
            }

            int Count = cogGraphicCollection.Count;

            if (Count == 0) return;
            var distinctNumbersWithCounts = _Box
                                .GroupBy(x => x.batch_index)
                                .Select(group => new { BatchID = group.Key, Count = group.Count() });


            int DIVData = cogGraphicCollection.Count / distinctNumbersWithCounts.Count();
            foreach (ICogGraphic item in cogGraphicCollection)
            {


                // if (BatchTest_Mode) 

                double data = 0;
                try
                {
                    data = i / DIVData;
                }
                catch
                {

                }




         //       int AAA = jasAIResultparam.BatchID[i];
                int AAA = jasAIResultparam.BatchID[i];
                try
                {
                    if (AAA == 0)
                    {
                        _Display = cogRecordDisplay1;
          //              item.Color = CogColorConstants.Red;
                    }
                    if (AAA == 1)
                    {
                        _Display = cogRecordDisplay2;
           //             item.Color = CogColorConstants.Green;
                    }
                    if (AAA == 2)
                    {
                        _Display = cogRecordDisplay3;
           //             item.Color = CogColorConstants.Yellow;
                    }
                    if (AAA == 3)
                    {
                        _Display = cogRecordDisplay4;
           //             item.Color = CogColorConstants.Purple;
                    }
                    _Display.StaticGraphics.Add(item, "");
             
                    _Display.Fit();
              //      _Display.AutoFitWithGraphics = false;
              //      _Display.AutoFitWithGraphics = true;
                }
                catch
                {

                }
                i++;
            }
        }
        private void DisplaySetGraphic2(int _Box, CogGraphicCollection cogGraphicCollection, JasAIResultparam jasAIResultparam)
        {
            Cognex.VisionPro.CogRecordDisplay _Display = null;
            int i = 0;

            for (int j = 0; j < Displays.Count; j++)
            {
                Displays[j].StaticGraphics.Clear();
            }

            int Count = cogGraphicCollection.Count;

            if (Count == 0) return;
           // var distinctNumbersWithCounts = _Box
           //                     .GroupBy(x => x.batch_index)
           //                     .Select(group => new { BatchID = group.Key, Count = group.Count() });


       //     int DIVData = cogGraphicCollection.Count / distinctNumbersWithCounts.Count();
            int DIVData = cogGraphicCollection.Count / _Box;
            foreach (ICogGraphic item in cogGraphicCollection)
            {


                // if (BatchTest_Mode) 

                double data = 0;
                try
                {
                    data = i / DIVData;
                }
                catch
                {

                }




                int AAA = jasAIResultparam.BatchID[i];
                try
                {
                    if (AAA == 0)
                    {
                        _Display = cogRecordDisplay5;
                        item.Color = CogColorConstants.Red;
                    }
                    if (AAA == 1)
                    {
                        _Display = cogRecordDisplay2;
                        item.Color = CogColorConstants.Green;
                    }
                    if (AAA == 2)
                    {
                        _Display = cogRecordDisplay3;
                        item.Color = CogColorConstants.Yellow;
                    }
                    if (AAA == 3)
                    {
                        _Display = cogRecordDisplay4;
                        item.Color = CogColorConstants.Purple;
                    }
                    _Display.StaticGraphics.Add(item, "");
                    _Display.Fit();
                }
                catch
                {

                }
                i++;
            }
        }
        private void ImageFileLoadTool1_ButtonClick(object seder, EventArgs e)
        {
            if (imageFileLoadTool1.InputImagePath == "") return;

            System.IO.FileInfo file = new System.IO.FileInfo(imageFileLoadTool1.InputImagePath);
            _Systemfile.ImageFolderPath = file.Directory.FullName;
            JsonConvertHelper.Save(_Systemfile.FileName, _Systemfile);


            if(NUD_IMAGE_SCALE.Value == 1)
                DisplaySetImage(MainDisplay , GetImage(imageFileLoadTool1.InputImagePath));

            if (CB_TOORUN.Checked)
            {
                var items = seder;
                if(((Button)items).Text != "INPUT IMAGES ->")
                    BTN_AUTO_RUN_Click(null , null);
            }
        }

        string _ModelFileName = string.Empty;
        string _PredictorFileName = string.Empty;
        string _PredictorSaveFileName = string.Empty;


        public static bool BatchTest_Mode;
        public static int BatchTest_Count;


        public static NeurocleAITool NeurocleTool0 = new NeurocleAITool("Locate");
        public static NeurocleAITool NeurocleTool1 = new NeurocleAITool("카본");
        public static NeurocleAITool NeurocleTool2 = new NeurocleAITool("용액");







       
        private Main.MTickTimer m_Timer = new Main.MTickTimer();
        private Main.MTickTimer m_Timer_ALL = new Main.MTickTimer();





        private void WriteLog(string _Log)
        {

            try
            {


                listBox1.Invoke((MethodInvoker)delegate () {
                    listBox1.Items.Add(_Log);
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                });

            }
            catch (Exception ex)
            {

            }
        }
        private void WriteLog2(string _Log, bool Saveflag = false)
        {

            try
            {
                listBox2.Invoke((MethodInvoker)delegate () {
                    listBox2.Items.Add(_Log);
                    listBox2.SelectedIndex = listBox2.Items.Count - 1;
                });





                //if (Saveflag == true)
                //{
                //
                //    Save_Log(_Log);
                //}
            }
            catch(Exception ex) 
            {

            }
        }

        private void BTN_CLEAR_Click(object sender, EventArgs e)
        {
            DisplayClear(MainDisplay);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            DisplayClear(cogRecordDisplay1);
            DisplayClear(cogRecordDisplay2);
            DisplayClear(cogRecordDisplay3);
            DisplayClear(cogRecordDisplay4);
            DisplayClear(cogRecordDisplay5);
        }
        private void BTN_SUB_DISPLAY_CLEAR_Click(object sender, EventArgs e)
        {
            cogRecordDisplay1.Image  = null;
            cogRecordDisplay2.Image  = null;
            cogRecordDisplay3.Image  = null;
            cogRecordDisplay4.Image  = null;
            cogRecordDisplay5.Image = null;        
                }



        Main.NeurocleMachine NTool = new Main.NeurocleMachine();
        private void BTN_LOAD_MODEL_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("동작?", "정보", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".net",
                Filter = "Model File Select (*.net)|*.net",
            };

            bool read = false;
            if ((bool)dialog.ShowDialog() == true)
            {
                using (var fs = new System.IO.FileStream(dialog.FileName, System.IO.FileMode.Open, FileAccess.Read))
                {
                    _ModelFileName = fs.Name;
                    read = true;
                }
            }



            var dialog2 = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = ".nrpd",
                Filter = "Predict File Select (*.nrpd)|*.nrpd",
            };

            bool read2 = false;
            if ((bool)dialog2.ShowDialog() == true)
            {
                _PredictorSaveFileName = dialog2.FileName;
                    read2 = true;
            }



            if (_ModelFileName != string.Empty && read)
            {
                if (_PredictorSaveFileName != string.Empty && read2)
                {
                    NTool.SetModelIntiial(_ModelFileName, 0, _PredictorSaveFileName);
                }
            }

        }

        private void BTN_DISPLAYIMAGE_Click(object sender, EventArgs e)
        {

       

        }
        private void DISPLAYIMAGE_()
        {

            if (NeurocleTool0.Output.Region == null) return;
       //     for (int i = 0; i < (int)((nrt.Input)NeurocleTool0.Output.Image).get_count(); i++)
            {
          //      ICogImage _Image = VisionProHelper.GetCogimage(((nrt.Input)NeurocleTool0.Output.Image).get_org_input_ndbuff(i));

          //      int index = 0;

         //       int indexNum = NeurocleTool0.Output.Region[i].RegionBox.Index_OBD;

               // if (indexNum == 0) cogRecordDisplay1.Image = _Image;
               // if (indexNum == 1) cogRecordDisplay2.Image = _Image;
               // if (indexNum == 2) cogRecordDisplay3.Image = _Image;
               // if (indexNum == 3) cogRecordDisplay4.Image = _Image;
               //
            }

        }



        #region Image처리







        public class SafeMalloc : SafeBuffer
        {
            public SafeMalloc(int size)
                : base(true)
            {
                this.SetHandle(Marshal.AllocHGlobal(size));
                this.Initialize((ulong)size);
            }
            protected override bool ReleaseHandle()
            {
                Marshal.FreeHGlobal(this.handle);
                return true;
            }
            public static implicit operator IntPtr(SafeMalloc h)
            {
                return h.handle;
            }
        }
        public static ICogImage CovertImage(byte[] dataR, byte[] dataG, byte[] dataB, int width, int height)
        {
            var dataSize = width * height;

            var bufferB = new SafeMalloc(dataSize);
            Marshal.Copy(dataB, 0, bufferB, dataSize);

            var bufferG = new SafeMalloc(dataSize);
            Marshal.Copy(dataG, 0, bufferG, dataSize);

            var bufferR = new SafeMalloc(dataSize);
            Marshal.Copy(dataR, 0, bufferR, dataSize);


            var cogRootR = new CogImage8Root();
            cogRootR.Initialize(width, height, bufferR, width, bufferR);

            var cogRootG = new CogImage8Root();
            cogRootG.Initialize(width, height, bufferG, width, bufferG);

            var cogRootB = new CogImage8Root();
            cogRootB.Initialize(width, height, bufferB, width, bufferB);

            var cogImage = new CogImage24PlanarColor();
            cogImage.SetRoots(cogRootR, cogRootG, cogRootB);

            return cogImage;
        }

        #endregion


        private void PredictorLoad(object _Tool , string Filepath = "")
        {

            if (Filepath == "")
            {
                var dialog = new Microsoft.Win32.OpenFileDialog
                {
                    DefaultExt = ".nrpd",
                    Filter = "Predictor File Select (*.nrpd)|*.nrpd",
                };

                if ((bool)dialog.ShowDialog() == true)
                {
                    using (var fs = new System.IO.FileStream(dialog.FileName, System.IO.FileMode.Open, FileAccess.Read))
                    {
                        _PredictorFileName = fs.Name;
                        ((NeurocleAITool)_Tool).Load(_PredictorFileName);
                    }
                }
            } 
            else 
            {
                _PredictorFileName = Filepath;
                ((NeurocleAITool)_Tool).Load(_PredictorFileName);
            }

        }

        private void openWorkspace_Load(object _Tool , string Filepath = "")
        {

            string _Filepath = "";

            _Filepath = Filepath;


            if (_Filepath == "")
            {
                var dialog = new Microsoft.Win32.OpenFileDialog
                {
                    DefaultExt = ".vrws",
                    Filter = "Cognex Deep Learning Studio Runtime Workspaces (*.vrws)|*.vrws",
                    ValidateNames = false
                };

                if (dialog.ShowDialog() == true)
                {
                    using (var fs = new System.IO.FileStream(dialog.FileName, System.IO.FileMode.Open))
                    {

                        _Filepath = dialog.FileName;
                    }
                }
            }


    //        CognexTool0.Load(_Filepath);


           // var workspace = _vProIntegration.Control.Workspaces.Add(System.IO.Path.GetFileNameWithoutExtension(_Filepath), _Filepath);
           //
           // if (_vProIntegration.IsRuntimeWorkspaceValid(workspace))
           // {
           //     CognexTool0.Subject.Workspace = workspace;
           //     CognexTool0.Subject.WorkspaceName = Path.GetFileName(_Filepath);
           //     _PredictorFileName = _Filepath;
           // }
           // else
           // {
           //     _vProIntegration.Control.Workspaces.Remove(workspace.UniqueName);
           // }

        }


        private void BTN_TOOL_1_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("동작?", "정보", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

           // PredictorLoad(NeurocleTool0);
           // openWorkspace_Load(CognexTool0);
            _Systemfile.Toolpath1 = _PredictorFileName;
            JsonConvertHelper.Save(_Systemfile.FileName, _Systemfile);
        }

        private void BTN_TOOL_2_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("동작?", "정보", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            PredictorLoad(NeurocleTool1);

            _Systemfile.Toolpath2 = _PredictorFileName;
            JsonConvertHelper.Save(_Systemfile.FileName, _Systemfile);
        }

        private void BTN_TOOL_3_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("동작?", "정보", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            PredictorLoad(NeurocleTool2);

            _Systemfile.Toolpath3 = _PredictorFileName;
            JsonConvertHelper.Save(_Systemfile.FileName, _Systemfile);
        }
        public ICogImage TempImage;// = new ICogImage();


        private void BTN_TOOL_RUN_1_Click(object sender, EventArgs e)
        {

            GetRunparameter();

            m_Timer.StartTimer();
            NeurocleTool0.SetInputImage(imageFileLoadTool1.InputImagePath);

            WriteLog("InpuImage:" + m_Timer.GetEllapseTime().ToString());
            NeurocleTool0.Run(0);
            NeurocleTool0.GetResultsGraphic(0);
            NeurocleTool0.ProcessTime = m_Timer.GetEllapseTime();
            WriteLog("Run :" + NeurocleTool0.ProcessTime.ToString() + " , " + NUD_RUNCOUNT.Value.ToString() + "Point: " + (NeurocleTool0.ProcessTime * ((int)NUD_RUNCOUNT.Value)).ToString());

            DisplayClear(MainDisplay);
            DisplaySetGraphic(MainDisplay, NeurocleTool0.ResultParam[0].ResultsGraphicOffset);
            DISPLAYIMAGE_();

            UpdateControl_OBD(0);


        }

        private void BTN_TOOL_RUN_2_Click(object sender, EventArgs e)
        {

            m_Timer.StartTimer();
            NeurocleTool1.SetInputImage(imageFileLoadTool1.InputImagePath);
            WriteLog("InpuImage:" + m_Timer.GetEllapseTime().ToString());

            NeurocleTool1.Run(0);
            NeurocleTool1.GetResultsGraphic(0);
            NeurocleTool1.ProcessTime = m_Timer.GetEllapseTime();
            WriteLog("Run :" + NeurocleTool1.ProcessTime.ToString() + " , " + NUD_RUNCOUNT.Value.ToString() + "Point: " + (NeurocleTool1.ProcessTime * ((int)NUD_RUNCOUNT.Value)).ToString());


            DisplayClear(cogRecordDisplay1);
            DisplayClear(cogRecordDisplay2);
            DisplayClear(cogRecordDisplay3);
            DisplayClear(cogRecordDisplay4);
            DisplayClear(MainDisplay);

            DisplaySetGraphic(MainDisplay, NeurocleTool1.ResultParam[0].ResultsGraphicOffset);
            DisplaySetGraphic(NeurocleTool1.ResultParam[0].Get_CarbonAddAllBoxs(), NeurocleTool1.ResultParam[0].ResultsGraphic , NeurocleTool1.ResultParam[0]);


            UpdateControl_SEG1(0);
        }
        private void BTN_TOOL_RUN_3_Click(object sender, EventArgs e)
        {

            m_Timer.StartTimer();
            NeurocleTool2.SetInputImage(imageFileLoadTool1.InputImagePath);
            WriteLog("InpuImage:" + m_Timer.GetEllapseTime().ToString());

            NeurocleTool2.Run(0);
            NeurocleTool2.GetResultsGraphic(0);
            NeurocleTool2.ProcessTime = m_Timer.GetEllapseTime();
            WriteLog("Run :" + NeurocleTool2.ProcessTime.ToString() + " , " + NUD_RUNCOUNT.Value.ToString() + "Point: " + (NeurocleTool2.ProcessTime * ((int)NUD_RUNCOUNT.Value)).ToString());


            DisplayClear(cogRecordDisplay1);
            DisplayClear(cogRecordDisplay2);
            DisplayClear(cogRecordDisplay3);
            DisplayClear(cogRecordDisplay4);


            DisplaySetGraphic(MainDisplay, NeurocleTool2.ResultParam[0].ResultsGraphicOffset);
            DisplaySetGraphic(NeurocleTool2.ResultParam[0].Boxs, NeurocleTool2.ResultParam[0].ResultsGraphic , NeurocleTool2.ResultParam[0]);
        }

        private void Save_Log(string Msg)
        {
            string nFolder;
            string LogdataPath;
            string FileName = "";


            string Startpath = System.Windows.Forms.Application.StartupPath;



            LogdataPath = "C:\\SystemData\\NeuroLog";
            nFolder = "C:\\SystemData\\NeuroLog" + DateTime.Now.ToString("yyyyMMdd") + "\\";

            FileName = "NeuroLog.txt";

            if (!Directory.Exists(LogdataPath)) Directory.CreateDirectory(LogdataPath);
            if (!Directory.Exists(nFolder)) Directory.CreateDirectory(nFolder);

            StreamWriter SW = new StreamWriter(nFolder + FileName, true, Encoding.Unicode);
            SW.WriteLine(Msg);

            SW.Close();
            
        }










        private void BTN_AUTO_RUN_Click(object sender, EventArgs e)
        {
           BTN_SUB_DISPLAY_CLEAR_Click( null,null);
           AutoRun(imageFileLoadTool1.InputImagePath , 0);
           AutoRunGetsults(0);
           AutoRunDraw(imageFileLoadTool1.InputImagePath,0);

        }



        private void GetRunparameter()
        {
           NeurocleTool0.RunParam.SpecWidth.Min = (int)US_SPEC.uC_OBD_TOOL_RUNPARAM_WIDTH.MinValue;
           NeurocleTool0.RunParam.SpecWidth.Max = (int)US_SPEC.uC_OBD_TOOL_RUNPARAM_WIDTH.MaxValue;
          
           iNSPECT.RunParam = US_SPEC.GetRunparam();

        }
        private void AutoRun(string ImageFilePath , int indexNum)
        {
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            GetRunparameter();
            m_Timer.StartTimer();
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            NeurocleTool0.SetInputImage(ImageFilePath);
            iNSPECT.Run(indexNum);
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //iNSPECT.Run(indexNum);
            WriteLog2("3 Tool RunTime:" + m_Timer.GetEllapseTime().ToString() + " ," + NUD_RUNCOUNT.Value.ToString() + " PointTime: " + (m_Timer.GetEllapseTime() * (int)NUD_RUNCOUNT.Value).ToString());
        }
        string m_Cell_ID = "";
        private void AutoRunDraw(string ImageFilePath, int indexNum)
        {
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            DisplayClear(MainDisplay);
            DisplayClear(cogRecordDisplay1);
            DisplayClear(cogRecordDisplay2);
            DisplayClear(cogRecordDisplay3);
            DisplayClear(cogRecordDisplay4);
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------     
            DisplaySetGraphic(MainDisplay, NeurocleTool0.ResultParam[indexNum].ResultsGraphicOffset);
            DISPLAYIMAGE_();
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            DisplaySetGraphic(MainDisplay, NeurocleTool1.ResultParam[indexNum].ResultsGraphicOffset);
            DisplaySetGraphic(NeurocleTool1.ResultParam[indexNum].Get_CarbonAddAllBoxs(), NeurocleTool1.ResultParam[indexNum].ResultsGraphic, NeurocleTool1.ResultParam[indexNum]);
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------       
            DisplaySetGraphic(MainDisplay, NeurocleTool2.ResultParam[indexNum].ResultsGraphicOffset);
            DisplaySetGraphic(NeurocleTool2.ResultParam[indexNum].Boxs, NeurocleTool2.ResultParam[indexNum].ResultsGraphic, NeurocleTool2.ResultParam[indexNum]);
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        UpdateControl_OBD(indexNum);
        UpdateControl_SEG1(indexNum);
        UpdateControl_Percent(indexNum);


            int nTabnum = indexNum % 25;
            int camNo = indexNum / 25;

            string overlaypath = m_Cell_ID + "\\"+"TabNum_" + nTabnum.ToString("00") + "\\" + "CamNum_" + (camNo + 1) + "_Overlay.abs";


     //
     //       JasOverlayList jasOverlay1 = new JasOverlayList();
     //
     //
     //
     //       jasOverlay1.Image = VisionProHelper.GetCogimage(ImageFilePath);
     //       jasOverlay1.ResultsGraphic.Clear();
     //
     //       jasOverlay1.CamNo = camNo;
     //       jasOverlay1.TabNum = nTabnum;
     //
     //       jasOverlay1.ResultsGraphic.Add(iNSPECT.CARBON_OBD.ResultParam[indexNum].ResultsGraphic);
     //       jasOverlay1.ResultsGraphic.Add(iNSPECT.CARBON_SEG.ResultParam[indexNum].ResultsGraphicOffset);
     //       jasOverlay1.ResultsGraphic.Add(iNSPECT.SOLUTION_SEG.ResultParam[indexNum].ResultsGraphicOffset);
     //       jasOverlay1.ResultsGraphic.Add(iNSPECT.ResultParam[indexNum].BlobGraphic);
     //       jasOverlay1.ResultData = iNSPECT.ResultParam[indexNum];
     //       JasHelper.Serializing(overlaypath, jasOverlay1);
     //
     //

      //      Task.Run(() =>
      //       uC_ResultView1.SETDATA(indexNum, iNSPECT.ResultParam[indexNum].ResultALL)
      //      );
            
        }
        private void AutoRunOnly(string ImageFilePath, int indexNum , string msg)
        {
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            m_Timer.StartTimer();
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            NeurocleTool0.SetInputImage(VisionProHelper.GetCogimage(ImageFilePath));
           // WriteLog("InpuImage:" + m_Timer.GetEllapseTime().ToString());
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
           // iNSPECT.RunOnly(indexNum);
           // WriteLog2("3 Tool RunTime:" + m_Timer.GetEllapseTime().ToString() + " ," + NUD_RUNCOUNT.Value.ToString() + " PointTime: " + (m_Timer.GetEllapseTime() * (int)NUD_RUNCOUNT.Value).ToString());
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
           //DisplayClear(MainDisplay);
           //DisplayClear(cogRecordDisplay1);
           //DisplayClear(cogRecordDisplay2);
           //DisplayClear(cogRecordDisplay3);
           //DisplayClear(cogRecordDisplay4);
           ////------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------     
           //DisplaySetGraphic(MainDisplay, NeurocleTool0.ResultParam.ResultsGraphic);
           //BTN_DISPLAYIMAGE_Click(null, null);
           ////------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
           //DisplaySetGraphic(MainDisplay, NeurocleTool1.ResultParam.ResultsGraphicOffset);
           //DisplaySetGraphic(NeurocleTool1.ResultParam.Get_batchGroupBoxs(), NeurocleTool1.ResultParam.ResultsGraphic, NeurocleTool1.ResultParam);
           ////------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------       
           //DisplaySetGraphic(MainDisplay, NeurocleTool2.ResultParam.ResultsGraphicOffset);
           //DisplaySetGraphic(NeurocleTool2.ResultParam.Boxs, NeurocleTool2.ResultParam.ResultsGraphic, NeurocleTool2.ResultParam);
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 WriteLog2(msg +" 3 Tool DisplayTime:" + m_Timer.GetEllapseTime().ToString() + " ," + NUD_RUNCOUNT.Value.ToString() + " PointTime: " + (m_Timer.GetEllapseTime() * (int)NUD_RUNCOUNT.Value).ToString());
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        }
        private static void  AutoRunGetsults(int IndexNum)
        {
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
           // m_Timer.StartTimer();
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
           // NeurocleTool0.SetInputImage(VisionProHelper.GetCogimage(ImageFilePath));
          //  WriteLog("InpuImage:" + m_Timer.GetEllapseTime().ToString());
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            iNSPECT.Getresult(IndexNum);

   //         Trace.WriteLine("index:" + IndexNum.ToString());
            //WriteLog2("3 Tool RunTime:" + m_Timer.GetEllapseTime().ToString() + " ," + NUD_RUNCOUNT.Value.ToString() + " PointTime: " + (m_Timer.GetEllapseTime() * (int)NUD_RUNCOUNT.Value).ToString());
            ////------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //DisplayClear(MainDisplay);
            //DisplayClear(cogRecordDisplay1);
            //DisplayClear(cogRecordDisplay2);
            //DisplayClear(cogRecordDisplay3);
            //DisplayClear(cogRecordDisplay4);
            ////------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------     
            //DisplaySetGraphic(MainDisplay, NeurocleTool0.ResultParam.ResultsGraphic);
            //BTN_DISPLAYIMAGE_Click(null, null);
            ////------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //DisplaySetGraphic(MainDisplay, NeurocleTool1.ResultParam.ResultsGraphicOffset);
            //DisplaySetGraphic(NeurocleTool1.ResultParam.Get_batchGroupBoxs(), NeurocleTool1.ResultParam.ResultsGraphic, NeurocleTool1.ResultParam);
            ////------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------       
            //DisplaySetGraphic(MainDisplay, NeurocleTool2.ResultParam.ResultsGraphicOffset);
            //DisplaySetGraphic(NeurocleTool2.ResultParam.Boxs, NeurocleTool2.ResultParam.ResultsGraphic, NeurocleTool2.ResultParam);
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //     WriteLog2("3 Tool DisplayTime:" + m_Timer.GetEllapseTime().ToString() + " ," + NUD_RUNCOUNT.Value.ToString() + " PointTime: " + (m_Timer.GetEllapseTime() * (int)NUD_RUNCOUNT.Value).ToString());
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        }
        List <string> Imagename = new List <string>();
        private void button1_Click(object sender, EventArgs e)
        {
            button17_Click(null,null);
            Imagename.Clear();
            for (int i = 0; i < (int)NUD_RUNCOUNT.Value; i++)
            {
                imageFileLoadTool1.RunNext(sender, e);
                Imagename.Add(imageFileLoadTool1.InputImagePath);
            }
        }
        PathData _PathData   = new PathData();
        private void BTN_CYCLERUN_ALLIMAGE_Click(object sender, EventArgs e)
        {

            if(System.Windows.Forms.MessageBox.Show("동작?" , "정보"  , MessageBoxButtons.YesNo, MessageBoxIcon.Question)  == DialogResult.No)
            {
                return;
            }


            CB_TOORUN.Checked = false;
            button1_Click(sender, e);
           
            m_Timer_ALL.StartTimer();
            foreach (var item in Imagename.Select((value, index) => (value, index)))
            {
                //         BTN_TOOL_RUN_1_Click(null,null);
                AutoRun(item.value, item.index);
            }
            double temp = m_Timer_ALL.GetEllapseTime();
            double temp2 = temp * 7;

            WriteLog2(NUD_RUNCOUNT.Value.ToString() + " ALL PointTime: " + temp.ToString() + "   ," + NUD_RUNCOUNT.Value.ToString() + "*7 ALL PointTime: " + temp2.ToString());


         
           Parallel.For(0, Imagename.Count, (i) =>
           {
               AutoRunGetsults(i);
           });

            temp = m_Timer_ALL.GetEllapseTime();
            temp2 = temp * 7;

            WriteLog2(NUD_RUNCOUNT.Value.ToString() + " ALL Getresult: " + temp.ToString() + "   ," + NUD_RUNCOUNT.Value.ToString() + "*7 ALL PointTime: " + temp2.ToString());

            m_Cell_ID = Main.DEFINE.SYS_DATADIR + Main.DEFINE.LOG_DATADIR + DateTime.Now.ToString("yyyyMMdd") + "\\Overlayser\\" + DateTime.Now.ToString("yyMMddHHmmss") + "DRY_INSPECT";




            _PathData.Data.Add(m_Cell_ID);

            LB_LOG_PATH.Invoke((MethodInvoker)delegate () {
                LB_LOG_PATH.Items.Add(m_Cell_ID);
            });




            foreach (var item in Imagename.Select((value, index) => (value, index)))
            {
                AutoRunDraw(item.value , item.index);
            }

            temp = m_Timer_ALL.GetEllapseTime();
            temp2 = temp * 7;

            WriteLog2(NUD_RUNCOUNT.Value.ToString() + " ALL Draw: " + temp.ToString() + "   ," + NUD_RUNCOUNT.Value.ToString() + "*7 ALL PointTime: " + temp2.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
          // if (System.Windows.Forms.MessageBox.Show("동작?", "정보", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
          // {
          //     return;
          // }
          // CB_TOORUN.Checked = false;
          // button1_Click(sender, e);
          //
          // m_Timer_ALL.StartTimer();
          //
          // AutoRunOnly("first");
          //
          // double temp = m_Timer_ALL.GetEllapseTime();
          // double temp2 = temp * 7;
          //
          // WriteLog(NUD_RUNCOUNT.Value.ToString() + " ALL PointTime: " + temp.ToString() + "   ," + NUD_RUNCOUNT.Value.ToString() + "*7 ALL PointTime: " + temp2.ToString());
          //
          //
          // Parallel.For(0, Imagename.Count, (Index) =>
          // {           
          //     // 연산 작업
          //     AutoRunGetsults(Index);        
          // });
          //
          //temp = m_Timer_ALL.GetEllapseTime();
          //temp2 = temp * 7;
          //
          // WriteLog2(NUD_RUNCOUNT.Value.ToString() + " Getresult PointTime: " + temp.ToString() + "   ," + NUD_RUNCOUNT.Value.ToString() + "*7 ALL PointTime: " + temp2.ToString());
          //
          // foreach (var item in Imagename.Select((value, index) => (value, index)))
          // {
          //     AutoRunDraw(item.index);
          // }


            //  foreach (var item in Imagename.Select((value, index) => (value, index)))
            //  {
            //      AutoRunDraw(item.value, item.index);
            //  }
            //
            //   temp = m_Timer_ALL.GetEllapseTime();
            //   temp2 = temp * 7;
            //
            //   WriteLog2(NUD_RUNCOUNT.Value.ToString() + " ALL Draw: " + temp.ToString() + "   ," + NUD_RUNCOUNT.Value.ToString() + "*7 ALL PointTime: " + temp2.ToString());

        }

        private void AutoRunOnly(string msg)
        {
            foreach (var item in Imagename.Select((value, index) => (value, index)))
            {
                AutoRunOnly(item.value, item.index , msg);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Mat mat = new Mat();

     //       mat = VisionProHelper.GetMatimage(((nrt.Input)NeurocleTool0.Input.Image).get_org_input_ndbuff(0));

            Mat mat2 = new Mat();
            mat2 = VisionProHelper.GetMatimage8BitG(mat);
            Cv2.ImShow("Threshold:", mat2);

            //    Cv2.Threshold(mat2, mat2, 100, 255, ThresholdTypes.Otsu);


            cogRecordDisplay5.Image = VisionProHelper.GetCogimageB(mat2);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DisplayClear(cogRecordDisplay5);
            DisplaySetGraphic2(NeurocleTool1.ResultParam[0].Get_batchGroupBoxsCount, NeurocleTool1.ResultParam[0].ResultsGraphic, NeurocleTool1.ResultParam[0]);



        }



        JAS.Serializer serializer = new JAS.Serializer();

       JasOverlay jasOverlay = new JasOverlay();

        JasOverlay jasOverlay2 = new JasOverlay();

        JasOverlayList jasOverlay3 = new JasOverlayList();
        private void button6_Click(object sender, EventArgs e)
        {
            m_Timer.StartTimer();


#if false
            jasOverlay.ResultsGraphic = NeurocleTool2.ResultParam[0].ResultsGraphic;
            jasOverlay.Image = cogRecordDisplay1.Image;
            serializer.Save(overlaypath, jasOverlay);

#else
            jasOverlay3.ResultsGraphic.Clear();

    

            jasOverlay3.ResultsGraphic.Add(new CogGraphicCollection(NeurocleTool0.ResultParam[0].ResultsGraphic));
            jasOverlay3.ResultsGraphic.Add(new CogGraphicCollection(NeurocleTool1.ResultParam[0].ResultsGraphic));
            jasOverlay3.ResultsGraphic.Add(new CogGraphicCollection(NeurocleTool2.ResultParam[0].ResultsGraphic));

            jasOverlay3.ResultsGraphic.Add(new CogGraphicCollection(NeurocleTool0.ResultParam[0].ResultsGraphicOffset));
            jasOverlay3.ResultsGraphic.Add(new CogGraphicCollection(NeurocleTool1.ResultParam[0].ResultsGraphicOffset));
            jasOverlay3.ResultsGraphic.Add(new CogGraphicCollection(NeurocleTool2.ResultParam[0].ResultsGraphicOffset));


            jasOverlay3.Image = cogRecordDisplay1.Image;
            serializer.Save(overlaypath2, jasOverlay3);
#endif
            WriteLog("Serializing:" + m_Timer.GetEllapseTime().ToString() + " , 175:"  + (175 * m_Timer.GetEllapseTime()).ToString());
        }
        string overlaypath = "D:\\ser\\Overlay.abs";
        string overlaypath2 = "D:\\ser\\Overlay2.abs";
        private void button7_Click(object sender, EventArgs e)
        {
            m_Timer.StartTimer();
#if false
            jasOverlay2 =  (JasOverlay)JasHelper.Deserializing(overlaypath);
#else

            jasOverlay3 = (JasOverlayList)JasHelper.Deserializing(overlaypath2);

            cogRecordDisplay5.Image = jasOverlay3.Image;

            foreach (var item in jasOverlay3.ResultsGraphic)
            {
                cogRecordDisplay5.StaticGraphics.AddList(item, "");
            }

#endif
            WriteLog("Deserializing:" + m_Timer.GetEllapseTime().ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //  foreach (ICogGraphic item in jasOverlay2.ResultsGraphic)
            //   {
            cogRecordDisplay5.Image = jasOverlay2.Image;
                cogRecordDisplay5.StaticGraphics.AddList(jasOverlay2.ResultsGraphic , "");
          //  }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button11_Click(null,null);


        }
        private void button5_Click(object sender, EventArgs e)
        {
            cogRecordDisplay6.StaticGraphics.Clear();
            cogRecordDisplay7.StaticGraphics.Clear();
            cogRecordDisplay8.StaticGraphics.Clear();
            cogRecordDisplay9.StaticGraphics.Clear();


            Mat src = new Mat();
      //      src = VisionProHelper.GetMatimage(((nrt.Input)NeurocleTool0.Input.Image).get_org_input_ndbuff(0));

            Mat mat2 = new Mat();
            mat2 = VisionProHelper.GetMatimage8BitG(src);

       //           Cv2.ImShow("IntersectionImage:" , mat2);

            Cv2.Threshold(mat2, mat2, (int)US_SPEC.uC_CarbonToCarGray.MaxValue, 255, ThresholdTypes.TozeroInv);
            if (true)
            {
                var contour = NeurocleTool1.ResultParam[0].Get_CarbonToCarbonBox(0, (int)NeurocleTool1.RunParam.CarbonToCarbon_DIV.Max , iNSPECT.RunParam.PercentSpecCarbontoCarbon);

                    foreach (var box in contour.Select((value, index) => (value, index)))
                    {
                        OpenCvSharp.Rect roi = new OpenCvSharp.Rect((int)(box.value.X + box.value.OffSetX), (int)(box.value.Y + box.value.OffSetY), (int)box.value.Width, (int)box.value.Height);
                        Mat TempCropImage = new Mat(mat2, roi).Clone();

                        //      Cv2.ImShow("IntersectionImage:" + box.index, TempCropImage);

                        double Carbon_ZeroArea = (int)box.value.Width * (int)box.value.Height;

                        ///발린 영역의 %값 
                        double OverLapArea = Cv2.CountNonZero(TempCropImage);
                        double overlapRatio = Math.Round(OverLapArea / Carbon_ZeroArea * 100.0, 2);

                        CogGraphicLabel label = new CogGraphicLabel();
                        label.BackgroundColor = CogColorConstants.Grey;
                        label.SetXYText(0, 0, "미도포영역:" + overlapRatio.ToString());

                        if (box.index == 0)
                        {
                            cogRecordDisplay6.Image = VisionProHelper.GetCogimage(TempCropImage);
                            cogRecordDisplay6.StaticGraphics.Add(label, "");
                        }
                        if (box.index == 1)
                        {
                            cogRecordDisplay7.Image = VisionProHelper.GetCogimage(TempCropImage);
                            cogRecordDisplay7.StaticGraphics.Add(label, "");
                        }

                        if (box.index == 2)
                        {
                            cogRecordDisplay8.Image = VisionProHelper.GetCogimage(TempCropImage);
                            cogRecordDisplay8.StaticGraphics.Add(label, "");
                        }
                        if (box.index == 3)
                        {
                            cogRecordDisplay9.Image = VisionProHelper.GetCogimage(TempCropImage);
                            cogRecordDisplay9.StaticGraphics.Add(label, "");
                        }

                        label.Dispose();
                        TempCropImage.Dispose();
                    }
                

            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
          
     

            ICogImage  cogImage = VisionProHelper.GetCogimage(imageFileLoadTool1.InputImagePath);

            m_Timer.StartTimer();

            Mat Original = VisionProHelper.GetMatimage(cogImage);
            Original = VisionProHelper.GetMatResize(Original, (double)NUD_IMAGE_SCALE.Value);

            WriteLog("IMAGE_SCALE:" + m_Timer.GetEllapseTime().ToString());


            cogRecordDisplay5.Image = VisionProHelper.GetCogimage(Original);



        }

        private void NUD_IMAGE_SCALE_ValueChanged(object sender, EventArgs e)
        {
            Main.ImageScale = (double)NUD_IMAGE_SCALE.Value;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            m_Timer.StartTimer();
            //----------------------------------------------------------------------------------------------------------------
            var Box2 = iNSPECT.ResultParam[0].Get_BoxsCarbonAll;
            CogGraphicCollection ResultsGraphicOffset = new CogGraphicCollection();

            foreach (var items in Box2)
            {
                foreach (var itemsbox in items)
                {
                    CogRectangle _RegionBox = new CogRectangle();
                    _RegionBox.Color = CogColorConstants.Green;
                    _RegionBox.SetXYWidthHeight(itemsbox.X + itemsbox.OffSetX, itemsbox.Y + itemsbox.OffSetY, itemsbox.Width, itemsbox.Height);
                    ResultsGraphicOffset.Add(new CogRectangle(_RegionBox));
                    _RegionBox.Dispose();
                }
            }
            //----------------------------------------------------------------------------------------------------------------
            WriteLog("For:" + m_Timer.GetEllapseTime().ToString());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            m_Timer.StartTimer();

            WriteLog("For:" + m_Timer.GetEllapseTime().ToString());
        }


        public void UpdateControl_OBD(int indexNum)
        {
            if (NeurocleTool0.ResultParam[indexNum].ResultOBD.Count > 0)
            {
                UpdateLabelColor(LB_OBD0, NeurocleTool0.ResultParam[indexNum].ResultOBD[0]);
                UpdateLabelColor(LB_OBD1, NeurocleTool0.ResultParam[indexNum].ResultOBD[1]);
                UpdateLabelColor(LB_OBD2, NeurocleTool0.ResultParam[indexNum].ResultOBD[2]);
                UpdateLabelColor(LB_OBD3, NeurocleTool0.ResultParam[indexNum].ResultOBD[3]);
            }
        }
        private void UpdateLabelColor(Label label , bool Value)
        {
            if (Value)
            {
                label.Invoke((MethodInvoker)delegate () {
                    label.BackColor = Color.Green;
                });

            }
            else
            {

                label.Invoke((MethodInvoker)delegate () {
                    label.BackColor = Color.Red;
                });
            }





        }
        private void UpdateLabelText(Label label, int Value)
        {
      
            label.Invoke((MethodInvoker)delegate () {
                label.Text = Value.ToString();
            });
        }

        public int[] GetBlobCount(List<BoundingBox> _Box)
        {
            int[] ReturnValue = new int[4];

            foreach(var item in _Box)
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



       //     var counts  =   _Box.GroupBy(x => x).Select(group => new { OBDGroup = group.Key.Index_OBD, Count = group.Count() });
       //     var counts2 = counts.GroupBy(x => x).Select(group2 => new { OBDGroup2 = group2.Key.OBDGroup, Count = group2.Count() });

          return  ReturnValue;
        }
        public void UpdateControl_SEG1(int indexNum)
        {
            var returnValue = NeurocleTool1.ResultParam[indexNum].GetBlobCount();


            foreach (var item in returnValue.Select((value , index) => (value, index)))            
            { 
           
                if (item.index == 0) UpdateLabelText(LB_OBD0, item.value);
                if (item.index == 1) UpdateLabelText(LB_OBD1, item.value);
                if (item.index == 2) UpdateLabelText(LB_OBD2, item.value);
                if (item.index == 3) UpdateLabelText(LB_OBD3, item.value);
            }
        }
        public void UpdateControl_Percent(int indexNum)
        {
            foreach(var item in LB_SideBox)
            {
                UpdateLabelColor(item, true);
            }
   
            var returnValue = iNSPECT.ResultParam[indexNum];

            foreach (var item in returnValue.Percent_Carbon.Select((value, index) => (value, index)))
            {
                foreach (var itemDetail in item.value.Data.Select((value2, index2) => (value2, index2)))
                {
                    var _Key = itemDetail.value2.Key;
                    var _Value = itemDetail.value2.Value;

                    if (item.index == 0)
                    {
                        if (_Key == 0) UpdateLabelColor(LB_SEG_00, item.value.Result[_Key]);
                        if (_Key == 1) UpdateLabelColor(LB_SEG_01, item.value.Result[_Key]);
                        if (_Key == 2) UpdateLabelColor(LB_SEG_02, item.value.Result[_Key]);
                    }
                    if (item.index == 1)
                    {
                        if (_Key == 0) UpdateLabelColor(LB_SEG_03, item.value.Result[_Key]);
                        if (_Key == 1) UpdateLabelColor(LB_SEG_04, item.value.Result[_Key]);
                        if (_Key == 2) UpdateLabelColor(LB_SEG_05, item.value.Result[_Key]);
                    }
                    if (item.index == 2)
                    {
                        if (_Key == 0) UpdateLabelColor(LB_SEG_06, item.value.Result[_Key]);
                        if (_Key == 1) UpdateLabelColor(LB_SEG_07, item.value.Result[_Key]);
                        if (_Key == 2) UpdateLabelColor(LB_SEG_08, item.value.Result[_Key]);
                    }
                    if (item.index == 3)
                    {
                        if (_Key == 0) UpdateLabelColor(LB_SEG_09, item.value.Result[_Key]);
                        if (_Key == 1) UpdateLabelColor(LB_SEG_10, item.value.Result[_Key]);
                        if (_Key == 2) UpdateLabelColor(LB_SEG_11, item.value.Result[_Key]);
                    }
                }
            }
   
            foreach (var item in returnValue.Percent_CarbonAll.Select((value, index) => (value, index)))
            {
                foreach (var itemDetail in item.value.Data.Select((value2, index2) => (value2, index2)))
                {
                    var _Key = itemDetail.value2.Key;
                    var _Value = itemDetail.value2.Value;

                    if (item.index == 0)
                    {
                        if (_Key == 0) UpdateLabelColor(LB_SEGALL_00, item.value.Result[_Key]);
                    }
                    if (item.index == 1)
                    {
                        if (_Key == 0) UpdateLabelColor(LB_SEGALL_01, item.value.Result[_Key]);
                    }
                    if (item.index == 2)
                    {
                        if (_Key == 0) UpdateLabelColor(LB_SEGALL_02, item.value.Result[_Key]);
                    }
                    if (item.index == 3)
                    {
                        if (_Key == 0) UpdateLabelColor(LB_SEGALL_03, item.value.Result[_Key]);
                    }
                }
            }
 
            foreach (var item in returnValue.Percent_CarbontoCarbon.Select((value, index) => (value, index)))
            {
                foreach (var itemDetail in item.value.Data.Select((value2, index2) => (value2, index2)))
                {
                    var _Key = itemDetail.value2.Key;
                    var _Value = itemDetail.value2.Value;

                    if (item.index == 0)
                    {
                        if (_Key == 0) UpdateLabelColor(LB_SEG2SEG_00_00, item.value.Result[_Key]);
                        if (_Key == 1) UpdateLabelColor(LB_SEG2SEG_00_01, item.value.Result[_Key]);
                        if (_Key == 2) UpdateLabelColor(LB_SEG2SEG_00_02, item.value.Result[_Key]);
                        if (_Key == 3) UpdateLabelColor(LB_SEG2SEG_00_03, item.value.Result[_Key]);
                    }
                    if (item.index == 1)
                    {
                        if (_Key == 0) UpdateLabelColor(LB_SEG2SEG_01_00, item.value.Result[_Key]);
                        if (_Key == 1) UpdateLabelColor(LB_SEG2SEG_01_01, item.value.Result[_Key]);
                        if (_Key == 2) UpdateLabelColor(LB_SEG2SEG_01_02, item.value.Result[_Key]);
                        if (_Key == 3) UpdateLabelColor(LB_SEG2SEG_01_03, item.value.Result[_Key]);
                    }
                    if (item.index == 2)
                    {
                        if (_Key == 0) UpdateLabelColor(LB_SEG2SEG_02_00, item.value.Result[_Key]);
                        if (_Key == 1) UpdateLabelColor(LB_SEG2SEG_02_01, item.value.Result[_Key]);
                        if (_Key == 2) UpdateLabelColor(LB_SEG2SEG_02_02, item.value.Result[_Key]);
                        if (_Key == 3) UpdateLabelColor(LB_SEG2SEG_02_03, item.value.Result[_Key]);
                    }
                    if (item.index == 3)
                    {
                        if (_Key == 0) UpdateLabelColor(LB_SEG2SEG_03_00, item.value.Result[_Key]);
                        if (_Key == 1) UpdateLabelColor(LB_SEG2SEG_03_01, item.value.Result[_Key]);
                        if (_Key == 2) UpdateLabelColor(LB_SEG2SEG_03_02, item.value.Result[_Key]);
                        if (_Key == 3) UpdateLabelColor(LB_SEG2SEG_03_03, item.value.Result[_Key]);
                    }
                }
            }
            return;
            foreach (var item in returnValue.Percent_CarbonSide.Select((value, index) => (value, index)))
            {
                foreach (var itemDetail in item.value.Data.Select((value2, index2) => (value2, index2)))
                {
                    var _Key = itemDetail.value2.Key;
                    var _Value = itemDetail.value2.Value;

                    int Number = (item.index * 4) + _Key;

                    UpdateLabelColor(LB_SideBox[Number], item.value.Result[_Key]);
                    UpdateLabelText(LB_SideBox[Number], (int)returnValue.Percent_CarbonSide[item.index].Data[_Key]);
                }
            }

        }
        private void button10_Click_1(object sender, EventArgs e)
        {

        
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button10_Click_2(object sender, EventArgs e)
        {
  //          DisplayClear(MainDisplay);

            CogGraphicCollection ResultsGraphicOffset = new CogGraphicCollection();


            var Box = iNSPECT.ResultParam[0].Get_BoxsCarbontoCarbon;

            foreach (var Box1 in Box)
            {
                foreach (var items in Box1)
                {
                    foreach (var itemsbox in items)
                    {
                        CogRectangle _RegionBox = new CogRectangle();
                        _RegionBox.Color = CogColorConstants.Green;
                        _RegionBox.SetXYWidthHeight(itemsbox.X + itemsbox.OffSetX, itemsbox.Y + itemsbox.OffSetY, itemsbox.Width, itemsbox.Height);
         //               ResultsGraphicOffset.Add(new CogRectangle(_RegionBox));
                        _RegionBox.Dispose();
                    }
                }
            }


            var Box2 = iNSPECT.ResultParam[0].Get_BoxsCarbonAll;


            foreach(var items in Box2)
            {
                foreach (var itemsbox in items)
                {
                    CogRectangle _RegionBox = new CogRectangle();
                    _RegionBox.Color = CogColorConstants.Green;
                    _RegionBox.SetXYWidthHeight(itemsbox.X+ itemsbox.OffSetX, itemsbox.Y + itemsbox.OffSetY, itemsbox.Width, itemsbox.Height);
          //          ResultsGraphicOffset.Add(new CogRectangle(_RegionBox));
                    _RegionBox.Dispose();
                }
            }


            var Boxside333 = iNSPECT.ResultParam[0].Get_BoxsCarbonSide;

            foreach (var Boxside in Boxside333)
            {

                foreach (var items in Boxside)
                {
                    foreach (var itemsbox in items)
                    {
                        CogRectangle _RegionBox = new CogRectangle();
                        _RegionBox.Color = CogColorConstants.Purple;
                        _RegionBox.SetXYWidthHeight(itemsbox.X + itemsbox.OffSetX, itemsbox.Y + itemsbox.OffSetY, itemsbox.Width, itemsbox.Height);
                        ResultsGraphicOffset.Add(new CogRectangle(_RegionBox));
                        _RegionBox.Dispose();
                    }
                }
            }
                

        



            DisplaySetGraphic(MainDisplay, ResultsGraphicOffset);

        }

        private void CB_PercentView_CheckedChanged(object sender, EventArgs e)
        {
            Main.PERCENT_VIEW = ((CheckBox)sender).Checked;
        }
        string InspectParameterpath = "D:\\ser\\InsparamCognex.param";
        private void BTN_SAVE_INSPECT_Click(object sender, EventArgs e)
        {
            m_Timer.StartTimer();

            iNSPECT.RunParam = US_SPEC.GetRunparam();
            JasHelper.Serializing(InspectParameterpath, iNSPECT.RunParam);

            WriteLog("Serializing:" + m_Timer.GetEllapseTime().ToString());
        }

        private void BTN_LOAD_INSPECT_Click(object sender, EventArgs e)
        {
            m_Timer.StartTimer();

            iNSPECT.RunParam = (InspectRunparam)JasHelper.Deserializing(InspectParameterpath);



            US_SPEC.SetRunparam(iNSPECT.RunParam);

            WriteLog("Deserializing:" + m_Timer.GetEllapseTime().ToString());
        }

        private void button11_Click_1(object sender, EventArgs e)
        {

                     uC_NGVIEW_ALL1.UC_NGVIEW_ALL_Load(iNSPECT ,0);
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        string Imagepath = "D:\\ser\\ImagePath.param";


        private void button14_Click(object sender, EventArgs e)
        {
            m_Timer.StartTimer();


            JasHelper.Serializing(Imagepath, _PathData);

            WriteLog("Serializing:" + m_Timer.GetEllapseTime().ToString());
        }

        private void button16_Click(object sender, EventArgs e)
        {
            m_Timer.StartTimer();

            _PathData = (PathData)JasHelper.Deserializing(Imagepath);

            foreach (var item in _PathData.Data.Select((value, index) => (value, index)))
            {
                LB_LOG_PATH.Invoke((MethodInvoker)delegate () {
                    LB_LOG_PATH.Items.Add(item.value);
                });
            }




            WriteLog("Deserializing:" + m_Timer.GetEllapseTime().ToString());
        }

        private void LB_LOG_PATH_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string curItem = ((ListBox)sender).SelectedItem.ToString();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            uC_ResultView1.Clear();
        }

        private void LB_LOG_PATH_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (((ListBox)sender).IndexFromPoint(e.Location) != -1) // 빈 공간이 아닌 곳을 더블클릭 했을 때.
            {
                // 선택된 항목 저장
                string curItem = ((ListBox)sender).SelectedItem.ToString();



             uC_ResultView1.Invoke((MethodInvoker)delegate () {
                 uC_ResultView1.Clear();
                 uC_ResultView1.SETFOLDER_PATH(curItem);
             });

            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
     
        }
    }///
}/////
