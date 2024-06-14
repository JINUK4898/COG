using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace COG
{
    public partial class UC_ResultView : UserControl
    {
        public UC_ResultView()
        {
            InitializeComponent();

            BTN_SET.Add(uC_ResultView_Label1);
            BTN_SET.Add(uC_ResultView_Label2);
            BTN_SET.Add(uC_ResultView_Label3);
            BTN_SET.Add(uC_ResultView_Label4);
            BTN_SET.Add(uC_ResultView_Label5);
            BTN_SET.Add(uC_ResultView_Label6);
            BTN_SET.Add(uC_ResultView_Label7);

            foreach (var item in BTN_SET)
            {
                item.ClickEventHandle += Item_ClickEventHandle;
            }



            MainDisplay2.Visible = false;
            MainDisplay2.Visible = true;

        }
        private List<COG.UC_ResultView_Label> BTN_SET = new List<UC_ResultView_Label>();


        public delegate void ClickEvent(int CamNo, int Index , int TotalIndex);
        public event ClickEvent ClickEventHandle;

        private void Item_ClickEventHandle(int CamNo, int Index)
        {
            int TempTotalIndex = 0;
            TempTotalIndex = (CamNo * BTN_SET[0].MaxCount) + Index;
            ClickEventHandle?.Invoke(CamNo, Index , TempTotalIndex);

            if(jasOverlayList.Count > 0)
            {
                try
                {
                    JasOverlayList founditem = jasOverlayList.FirstOrDefault(p => p.ResultData.Cam_num == CamNo && p.TabNum -1 == Index);
                    if (founditem != null)
                    {
                        MainDisplay.Image = founditem.Image;

                   MainDisplay2.Image = founditem.Image;
                   MainDisplay2.StaticGraphics.Clear();
                   foreach (var item in founditem.ResultsGraphic)
                       MainDisplay2.StaticGraphics.AddList(item, "");

                        uC_NGVIEW_ALL1.UC_NGVIEW_ALL_Load(founditem.ResultData , Index);
                    }
                }
                catch (Exception ex)
                {


                }

            }
        }
      
        public void Clear()
        {
            foreach (var item in BTN_SET)
            {
                item.Clear();
            }
            MainDisplay.Image = null;
            MainDisplay2.Image = null;

        }
        public void SETDATA(int CamNum , int Index , bool result)
        {
            BTN_SET[CamNum].SETDATA(CamNum , Index , result);
        }
        public void SETDATA(int SumIndex, bool result)
        {
            double Temp = SumIndex / BTN_SET[0].MaxCount;
            double _Index = SumIndex % BTN_SET[0].MaxCount;

            double _CamNo =  System.Math.Truncate(Temp);

            BTN_SET[(int)_CamNo].SETDATA((int)_CamNo, (int)_Index, result);
        }


        public string LogFolderPath { get; set; } = string.Empty;
        public string FolderPath { get; set; } = string.Empty;
        public void SETFOLDER_PATH(string _path)
        {
            this.FolderPath = _path;
            LB_FOLDER_PATH.Invoke((MethodInvoker)delegate () {
                LB_FOLDER_PATH.Text = _path;
                                });


            Task.Run(()=>
                 GetPathData()
            );
       
        }

        List<JasOverlayList> jasOverlayList  = new List<JasOverlayList> (); 
        private void GetPathData()
        {
            jasOverlayList.Clear();

            DirectoryInfo directoryInfo = new DirectoryInfo(FolderPath);
            try
            {
                List<string> FileFullNames = new List<string>();
                if (directoryInfo.GetDirectories().Count() > 0)
                {
                    foreach (var item in directoryInfo.GetDirectories())
                    {
                        foreach (var File in item.GetFiles())
                        {
                            FileFullNames.Add(File.FullName);
                        }
                    }
                }
         
                Parallel.For(0, FileFullNames.Count(), (i) =>
                {
                    JasOverlayList item = new JasOverlayList();
                    item = (JasOverlayList)JasHelper.Deserializing(FileFullNames[i]);

                    SETDATA(item.ResultData.Cam_num, item.TabNum -1, item.ResultData.ResultALL);
                    jasOverlayList.Add(item);
                });


               //foreach (var item in jasOverlayList)
               //{
               //    _CamNo = item.ResultData.Cam_num;
               //    _TabNum = item.TabNum;
               //    SETDATA(item.ResultData.Cam_num, item.TabNum - 1, item.ResultData.ResultALL);
               //}
            }
            catch (Exception ex)
            {


            }
        }
        public void BTN_GetPath_Run(string SearchPath)
        {
            LIB_LOG_PATH.Invoke((MethodInvoker)delegate ()
            {
                LIB_LOG_PATH.Items.Clear();
            });
            DirectoryInfo directoryInfo = new DirectoryInfo(SearchPath);
            // 폴더가 존재하는지 확인
            if (!directoryInfo.Exists)
            {
                LIB_LOG_PATH.Invoke((MethodInvoker)delegate ()
                {
                    LIB_LOG_PATH.Items.Add("The specified folder does not exist.");
                });
                return;
            }
            int GetCount = (int)NUD_COUNT.Value;
            // 최근 생성된 폴더 20개 가져오기
            var recentFolders = directoryInfo.GetDirectories()
                                             .OrderByDescending(d => d.CreationTime)
                                             .Take(GetCount);

            foreach (var folderpath in recentFolders)
            {
                LIB_LOG_PATH.Invoke((MethodInvoker)delegate ()
                {
                    LIB_LOG_PATH.Items.Add(SearchPath + "\\" + folderpath);
                });
            }

        }
        private void BTN_GetPath_Click(object sender, EventArgs e)
        {
            BTN_GetPath_Run(LogFolderPath + DateTime.Now.ToString("yyyyMMdd") + "\\Overlayser");
        }

        private void LIB_LOG_PATH_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (((ListBox)sender).IndexFromPoint(e.Location) != -1) // 빈 공간이 아닌 곳을 더블클릭 했을 때.
            {
                // 선택된 항목 저장
                string curItem = ((ListBox)sender).SelectedItem.ToString();

                this.Invoke((MethodInvoker)delegate () {
                    this.Clear();
                    this.SETFOLDER_PATH(curItem);
                });

            }
        }
    }
}
