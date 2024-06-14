using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JAS.Controls.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ImageFileLoadTool : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ImageFileLoadTool()
        {
            InitializeComponent();
            loadToolBlock();
        }

        private FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
        public CogToolBlock CogImageBlock = new CogToolBlock();
        private CircularList<string> LoadImageList = new CircularList<string>();

        public bool EditToolVisible
        {
            get
            {
                return this.CS_ToolBlockEdit.Visible;
            }
            set
            {
                this.CS_ToolBlockEdit.Visible = value;
            }
        }
        public event EventHandler ButtonClick;


        /// <summary>
        /// 
        /// </summary>
        public string InputImagePath
        {
//             set
//             {
//                 try
//                 {
//                     ((CogImageFileTool)CogImageBlock.Tools["CogImageFileTool1"]).Operator.Open(value, CogImageFileModeConstants.Read);
//                 }
//                 catch (System.Exception ex)
//                 {
//                 	
//                 }
//             }
            get
            {
                return ((CogImageFileTool)CogImageBlock.Tools["CogImageFileTool1"]).Operator.FileName;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public string InputFolderPath
        {
            set
            {

                try
                {
                    ReadImageFolder(value);
                    RunCurrent();

                }
                catch (System.Exception ex)
                {
                	
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public CogImage8Grey OutputImage
        {
            get
            {
                return CogImageBlock.Outputs["OutputImage"].Value as CogImage8Grey;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_FilePath"></param>
        public void Load(string _FilePath = "")
        {
            if (_FilePath == "")
            {
                System.Windows.Forms.DialogResult result = this.folderBrowserDialog1.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    _FilePath = this.folderBrowserDialog1.SelectedPath;
                }
            }
            try
            {
                InputFolderPath = _FilePath;
            }
            catch (System.Exception ex)
            {
            	
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void RunPrevious()
        {
            Run(LoadImageList.MovePrevious);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RunCurrent()
        {
            Run(LoadImageList.Current);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RunNext()
        {
            Run(LoadImageList.MoveNext);
        }

        /// <summary>
        /// 
        /// </summary>
        private void Run(string _FileName)
        {
            if (LoadImageList.Count > 0)
            {
             //   InputImagePath = _FileName;

                try
                {
                    ((CogImageFileTool)CogImageBlock.Tools["CogImageFileTool1"]).Operator.Open(_FileName, CogImageFileModeConstants.Read);
                    CogImageBlock.Run();
                    TB_Current_Image.Text = LoadImageList.CurrentIndex.ToString();
                    LB_IMAGEFILE_SELECT_NAME.Text = InputImagePath;
                }
                catch (System.Exception ex)
                {
                	
                }

            }
        }

        private void loadToolBlock()
        {
            #region Camera VPP initialize

            CogImageBlock.Tools.Clear();
            CogImageBlock.Inputs.Clear();
            CogImageBlock.Outputs.Clear();

            #region CogImageFileTool1

            CogImageFileTool temptool1 = new CogImageFileTool();

            temptool1.Name = "CogImageFileTool1";
            CogImageBlock.Tools.Add(temptool1);


            #endregion

            #region  CogImageConvertTool1

            //---------------------------------------------------------------------------------------------------------------------------------------------------------
            CogImageConvertTool temptool2 = new CogImageConvertTool();
            temptool2.Name = "CogImageConvertTool1";
            CogImageBlock.Tools.Add(temptool2);
            #endregion

            #region ToolBlock DataBinding 

            string desPath = "";

            CogImageBlock.Tools["CogImageConvertTool1"].DataBindings.Add("InputImage", CogImageBlock.Tools["CogImageFileTool1"], "OutputImage");

            Cognex.VisionPro.CogImage8Grey nOutPutImage = new CogImage8Grey();

            CogToolBlockTerminal nOutPut = new CogToolBlockTerminal("OutputImage", nOutPutImage);
            


            CogImageBlock.Outputs.Add(nOutPut);

            desPath = DataBindingPath(CogImageBlock.Outputs["OutputImage"]);
            CogImageBlock.Outputs.DataBindings.Add(desPath, CogImageBlock.Tools["CogImageConvertTool1"], "OutputImage");

            CS_ToolBlockEdit.Subject = CogImageBlock;
            #endregion

            #endregion

        }
        private static string DataBindingPath(Cognex.VisionPro.ToolBlock.CogToolBlockTerminal Terminal)
        {
            CogToolBlockTerminal inputTerminal;
            string sourcePath;

            try
            {
                inputTerminal = Terminal;
                sourcePath = "Item[\"" + inputTerminal.ID + "\"].Value.(" + inputTerminal.ValueType.FullName + ")";
                sourcePath = Cognex.VisionPro.Implementation.Internal.CogToolTerminals.RemoveExtraAssemblyInfoFromPath(sourcePath);
            }
            catch (System.Exception ex)
            {
                return sourcePath = null;
            }
            return sourcePath;
        }

        [Serializable]
        private class CircularList<T> : List<T>
        {
            private int _currentIndex = 0;
            public int CurrentIndex
            {
                get
                {
                    if (_currentIndex > Count - 1) { _currentIndex = 0; }
                    if (_currentIndex < 0) { _currentIndex = Count - 1; }
                    return _currentIndex;
                }
                set => _currentIndex = value;
            }

            public int NextIndex
            {
                get
                {
                    if (_currentIndex == Count - 1) return 0;
                    return _currentIndex + 1;
                }
            }

            public int PreviousIndex
            {
                get
                {
                    if (_currentIndex == 0) return Count - 1;
                    return _currentIndex - 1;
                }
            }

            public T Next => this[NextIndex];

            public T Previous => this[PreviousIndex];

            public T MoveNext
            {
                get
                {
                    try
                    {
                        _currentIndex++;
                        return this[CurrentIndex];
                    }
                    catch (System.Exception ex)
                    {
                        return default(T);
                    }

                }
            }

            public T MovePrevious
            {
                get
                {
                    try
                    {
                        _currentIndex--; return this[CurrentIndex];
                    }
                    catch (System.Exception ex)
                    {
                        return default(T);
                    }

                }
            }

            public T Current => this[CurrentIndex];


        }
        private void ReadImageFolder(string nPath)
        {
            string[] TempimgList;
            CircularList<string> TempimgList_IMG = new CircularList<string>();
            TempimgList_IMG.Clear();

            TempimgList = System.IO.Directory.GetFiles(nPath);
            if (TempimgList != null && TempimgList.GetLongLength(0) > 0)
            {
                for (int i = 0; i < TempimgList.GetLongLength(0); i++)
                {
                    string FileExtension = Path.GetExtension(TempimgList[i]).ToLower();
                    if (FileExtension.StartsWith(".jpg") || FileExtension.StartsWith(".png") || FileExtension.StartsWith(".bmp")/* || FileExtension.StartsWith(".idb")*/)
                    {
                        TempimgList_IMG.Add(TempimgList[i]);
                    }
                }
            }

            if (TempimgList_IMG.Count > 0)
            {
                LoadImageList = DeepCopy(TempimgList_IMG);
            }
            if (TempimgList_IMG != null && TempimgList_IMG.Count > 0)
            {
                TB_IMAGEFILE_FOLDER_NAME.Text = nPath;
                LB_load_Image_Cnt.Text = (TempimgList_IMG.Count - 1).ToString();
                TB_Current_Image.Text = "1";
            }
            else
            {
         //       TB_Current_Image.Text = "0";
         //       LB_load_Image_Cnt.Text = "0";

            }
        }
        private static T DeepCopy<T>(T obj)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Position = 0;

                return (T)formatter.Deserialize(stream);
            }
        }

        //--------------------------------------------------------------------------------------------------------
        private void BTN_IMG_LOAD_Click(object sender, EventArgs e)
        {
            Load();
            if (ButtonClick != null)
                ButtonClick(((Button)sender), e);
        }
        private void BTN_PRE_IMG_Click(object sender, EventArgs e)
        {
            RunPrevious();
            if (ButtonClick != null)
                ButtonClick(((Button)sender), e);
        }
        private void BTN_NEXT_IMG_Click(object sender, EventArgs e)
        {
            RunNext();
            if (ButtonClick != null)
                ButtonClick(((Button)sender), e);
        }

    }//
}///
