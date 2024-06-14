using Cognex.VisionPro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COG
{
    public partial class Main
    {


        public static bool PERCENT_VIEW = true;
        public static double ImageScale { get; set; } = 1;

        public static string NeurocleModelPath = AppDomain.CurrentDomain.BaseDirectory + "\\NeurocleModel\\Model.net";
        public static string NeuroclePredictPath = AppDomain.CurrentDomain.BaseDirectory + "\\NeurocleModel\\Predict.nrpd";

        public const int ToolMaxCount = 175;

#if true
        public partial class NeurocleMachine
        {
            //   Stopwatch stopwatch;

            //   MTickTimer m_Timer = new MTickTimer();

            public List<FileInfo> files;

         //   string modelPath;
         //   string predictorPath;
            static string ImagePath = "../dry_image/";

            public int nPatNo;
            public int IMGIdx = 0;
            public int imageChannels = 3;

            bool IsDetected = false;

            double ProcessTime;

            public List<PredictResults_Detect> Results = new List<PredictResults_Detect>();

           public CogGraphicCollection ResultsGraphic = new CogGraphicCollection();


            public List<string> LogMSG = new List<string>();



            /// <summary>
            /// Tool 
            /// </summary>
            nrt.Predictor predictor;


            /// <summary>
            /// Results data parameter
            /// </summary>
            nrt.Result results;


            /// <summary>
            /// Run parameter
            /// </summary>
     //       public nrt.Input inputs = new nrt.Input();


            /// <summary>
            /// results Status parameter
            /// </summary>
            nrt.Status status = new nrt.Status();





            //     public ICogImage InputImage;

            public struct PredictResults_Detect
            {
                public string ClassName;

                public float Prob;

                public int BoundingBoxLTX;
                public int BoundingBoxLTY;
                public int BoundingBoxWidth;
                public int BoundingBoxHeight;

            }
            public NeurocleMachine()
            {

            }



            /// <summary>
            /// 
            /// </summary>
            /// <param name="model_path"></param>
            /// <param name="modelio_flag"></param>
            /// <param name="device_idx"></param>
            /// <param name="batch_size"></param>
            /// <param name="fp16_flag"></param>
            /// <param name="threshold_flag"></param>
            /// <param name="PatMax"></param>
            public void SetModelIntiial(string model_path, int device_idx)//, bool fp16_flag, bool threshold_flag, int PatMax = 0)
            {
                //int batchSize = 3; //jsh
                //string Type = "Segmentation";
                //if (Type == "Segmentation") batchSize = 1;
                //predictor = new nrt.Predictor(model_path, nrt.Model.MODELIO_OUT_PROB, 0, batchSize, false, false);
                //
                //SAVE(NeuroclePredictPath);

            }
            public void SetModelIntiial(string model_path, int device_idx , string Predictor_Path)//, bool fp16_flag, bool threshold_flag, int PatMax = 0)
            {   //
                //int batchSize = 3;
                //string Type = "Segmentation";
                //if (Type == "Segmentation") batchSize = 1;
                //predictor = new nrt.Predictor(model_path, nrt.Model.MODELIO_OUT_PROB, 0, batchSize, false, false);
                //
                //SAVE(Predictor_Path);

            }
            public void SAVE(string _Filepath)
            {
                   // status = predictor.save_predictor(_Filepath);
                   // if (status != nrt.Status.STATUS_SUCCESS)
                   // {
                   //     throw new Exception("Predictor save failed");
                   // }
            }
            public void LOAD(string _Filepath)
            {
                //predictor = new nrt.Predictor(0, _Filepath);
            }





        }
#endif

    }
}
