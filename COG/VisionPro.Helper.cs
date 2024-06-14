using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ImageFile;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using static COG.Form1;


namespace COG
{
    public static class VisionProHelper
    {
        #region "Neon 카메라 버퍼생성"
        /// <summary>
        /// Neon 카메라 버퍼생성
        /// </summary>
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
        #endregion







        private static CogImageFileTool cogImageFileTool = new CogImageFileTool();
        public static ICogImage GetCogimage(string _FileName)
        {
            cogImageFileTool.Operator.Open(_FileName, CogImageFileModeConstants.Read);
            cogImageFileTool.Run();

            var cogImage = cogImageFileTool.OutputImage;

            return cogImage;
        }

        /// <summary>
        /// Mat to Cogimage
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static ICogImage GetCogimage(Mat _mat)
        {

            var channels = _mat.Channels();

            if (channels == 3)
            {

                Mat mat = new Mat(_mat.Height, _mat.Width, MatType.CV_8UC3);
                mat = _mat.Clone();

                Mat matR = ColorChannelSeperate(mat, ColorChannel.R);
                Mat matG = ColorChannelSeperate(mat, ColorChannel.G);
                Mat matB = ColorChannelSeperate(mat, ColorChannel.B);

                byte[] dataR = new byte[matR.Width * matR.Height];
                Marshal.Copy(matR.Data, dataR, 0, matR.Width * matR.Height);

                byte[] dataG = new byte[matG.Width * matG.Height];
                Marshal.Copy(matG.Data, dataG, 0, matG.Width * matG.Height);

                byte[] dataB = new byte[matB.Width * matB.Height];
                Marshal.Copy(matB.Data, dataB, 0, matB.Width * matB.Height);

                var cogImage = CovertImage(dataR, dataG, dataB, matB.Width, matB.Height);

                matR.Dispose();
                matG.Dispose();
                matB.Dispose();
                mat.Dispose();
                return cogImage;
            }
            else
            {
                Mat mat = new Mat(_mat.Height, _mat.Width, MatType.CV_8UC1);
                mat = _mat.Clone();

                byte[] dataR = new byte[mat.Width * mat.Height];
                Marshal.Copy(mat.Data, dataR, 0, mat.Width * mat.Height);
                var cogImage = CovertImage(dataR , mat.Width, mat.Height);

                mat.Dispose();
                return cogImage;
            }

        }


            /// <summary>
            /// Mat to Cogimage_Bitmap
            /// </summary>
            /// <param name="mat"></param>
            /// <returns></returns>
            public static ICogImage GetCogimageB(Mat mat)
        {
            if (mat.Channels() == 3)
            {
                CogImage24PlanarColor cogImage = new CogImage24PlanarColor(mat.ToBitmap());
                return cogImage;
            }
            else
            {
                CogImage8Grey cogImage = new CogImage8Grey(mat.ToBitmap());
                return cogImage;
            }
        }
#if false

        /// <summary>
        /// NDbuffer to CogImage
        /// </summary>
        /// <param name="_NDBuffer"></param>
        /// <returns></returns>
        public static ICogImage GetCogimage(nrt.NDBuffer _NDBuffer)
        {
            CogImage24PlanarColor cogImage = new CogImage24PlanarColor(GetMatimage(_NDBuffer).ToBitmap());
            return cogImage;
        }


        /// <summary>
        /// NDbuffertoMat
        /// </summary>
        /// <param name="_NDBuffer"></param>
        /// <returns></returns>
        public static Mat GetMatimage(nrt.NDBuffer _NDBuffer)
        {
            nrt.NDBuffer nDBuffer = _NDBuffer.clone();

            nrt.Shape shape = new nrt.Shape();
            shape = nDBuffer.get_shape();
            int crop_h = shape.get_axis(0);
            int crop_w = shape.get_axis(1);
            int crop_c = shape.get_axis(2);

            var sizeOfMat = crop_h * crop_w * crop_c;

            byte[] byte_buff = new byte[nDBuffer.get_total_size()];
            Mat mat = new Mat(crop_h, crop_w, MatType.CV_8UC3);

            nDBuffer.copy_to_buffer_uint8(byte_buff, (ulong)byte_buff.Length);
            //       nDBuffer.copy_to_buffer(byte_buff, (ulong)sizeOfMat);

            Marshal.Copy(byte_buff, 0, mat.Data, crop_h * crop_w * crop_c);

            nDBuffer.Dispose();
            return mat;
        }

#endif
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ICogImage"></param>
        /// <returns></returns>
        public static Mat GetMatimage(ICogImage _ICogImage)
        {
            if(_ICogImage.GetType().Name == "CogImage8Grey")
            {
                var colorimg = ((CogImage8Grey)_ICogImage).ToBitmap();
                Mat mat = new Mat();
                mat = BitmapConverter.ToMat(colorimg).Clone();
                colorimg.Dispose();
                return mat;
            }
            else if(_ICogImage.GetType().Name == "CogImage24PlanarColor")
            {
                var colorimg = ((CogImage24PlanarColor)_ICogImage).ToBitmap();
                Mat mat = new Mat();
                mat = BitmapConverter.ToMat(colorimg).Clone();


                mat = VisionProHelper.GetMatResize(mat, Main.ImageScale);

                colorimg.Dispose();
                return mat;
            }
            else
            {
                Mat mat = new Mat();
                return mat;
            }
        }


        public static Mat GetMatimage8BitG(Mat _mat)
        {
            Mat mat = new Mat(_mat.Height, _mat.Width, MatType.CV_8UC3);
            mat = _mat.Clone();

            //      Mat matR = ColorChannelSeperate(mat, ColorChannel.R);
            Mat matG = ColorChannelSeperate(mat, ColorChannel.G);
            //    Mat matB = ColorChannelSeperate(mat, ColorChannel.B);

            return matG;
        }
        public static Mat GetMatResize(Mat _mat , double _Scale)
        {
            Mat mat = new Mat(_mat.Height, _mat.Width, MatType.CV_8UC3);
            mat = _mat.Clone();
            Cv2.Resize(mat, mat, new OpenCvSharp.Size(0,0), _Scale, _Scale);
            return mat;
        }

#if false
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Mat"></param>
        /// <returns></returns>
        public static nrt.NDBuffer GetNDbuffer(Mat _mat)
        {
            ///-----------------------------------------------------------------------------------------------------------------\
            Mat mat = new Mat(_mat.Height, _mat.Width, MatType.CV_8UC3);
            mat = _mat.Clone();


            int crop_w = mat.Cols;
            int crop_h = mat.Rows;
            int crop_c = mat.Channels();

            var sizeOfMat = crop_h * crop_w * crop_c;
            byte[] managedArray = new byte[sizeOfMat];
            Marshal.Copy(mat.Data, managedArray, 0, (int)sizeOfMat);

            nrt.NDBuffer nDBuffer = new nrt.NDBuffer(new nrt.Shape(crop_h, crop_w, crop_c), nrt.DType.DTYPE_UINT8);
            nDBuffer.copy_from_buffer_uint8(managedArray, (ulong)managedArray.Length);

            mat.Dispose();
            return nDBuffer;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ICogImage"></param>
        /// <returns></returns>
        public static nrt.NDBuffer GetNDbuffer(ICogImage _ICogImage)
        {
            nrt.NDBuffer nDBuffe = GetNDbuffer(GetMatimage(_ICogImage).Clone());
            return nDBuffe;
        }
#endif




        public static bool IntersectsWithPoint(CogRectangle rect, CogPointMarker cogPointMarker)
        {
            return (cogPointMarker.X >= rect.X) && (cogPointMarker.X <= (rect.X + rect.Width)) && (cogPointMarker.Y >= rect.Y) && (cogPointMarker.Y <= (rect.Y + rect.Height));
        }



#if false
        public static nrt.NDBuffer CovertImage(ICogImage _cogImage)
        {
            var width = _cogImage.Width;
            var height = _cogImage.Height;






            int crop_w = width;
            int crop_h = height;
            int crop_c = 3;

            var sizeOfMat = crop_h * crop_w * crop_c;
            byte[] managedArray = new byte[sizeOfMat];

            ((CogImage24PlanarColor)_cogImage).GetRoots(out ICogImage8Root cogRootR, out ICogImage8Root cogRootG, out ICogImage8Root cogRootB);

            cogRootR.GetRawPixelMemory(CogImageDataModeConstants.Read, 0, 0, width, height, out ICogImage8PixelMemory cogImage8PixelMemory);
            cogRootG.GetRawPixelMemory(CogImageDataModeConstants.Read, 0, 0, width, height, out ICogImage8PixelMemory cogImage8PixelMemory1);
            cogRootB.GetRawPixelMemory(CogImageDataModeConstants.Read, 0, 0, width, height, out ICogImage8PixelMemory cogImage8PixelMemory2);


            var rawSize = (width * height);
            Marshal.Copy(cogImage8PixelMemory.Scan0 , managedArray , 0          , rawSize);
            Marshal.Copy(cogImage8PixelMemory1.Scan0, managedArray , rawSize    , rawSize);
            Marshal.Copy(cogImage8PixelMemory2.Scan0, managedArray , rawSize * 2, rawSize);

            nrt.NDBuffer nDBuffer = new nrt.NDBuffer(new nrt.Shape(crop_h, crop_w, crop_c), nrt.DType.DTYPE_UINT8);
            nDBuffer.copy_from_buffer_uint8(managedArray, (ulong)managedArray.Length);
            
            return nDBuffer;
        }
        public static ICogImage Convert8BitRawImageToCognexImage(byte[] imageData, int width, int height)
        {
            var rawSize = width * height;
            var buf = new SafeMalloc(rawSize);
            Marshal.Copy(imageData, 0, buf, rawSize);

            var cogRoot = new CogImage8Root();
            cogRoot.Initialize(width, height, buf, width, buf);
            var cogImage = new CogImage8Grey();
            cogImage.SetRoot(cogRoot);

            return cogImage;
        }
        public static ICogImage ConvertColorImageToCognexImage(byte[] colorImageByte, int width, int height)
        {
            var rawSize = (width * height);
            var bufR = new SafeMalloc(rawSize);
            Marshal.Copy(colorImageByte, 0, bufR, rawSize);

            var bufG = new SafeMalloc(rawSize);
            Marshal.Copy(colorImageByte, rawSize, bufG, rawSize);

            var bufB = new SafeMalloc(rawSize);
            Marshal.Copy(colorImageByte, rawSize * 2, bufB, rawSize);

            var cogRootR = new CogImage8Root();
            cogRootR.Initialize(width, height, bufR, width, bufR);
            var cogRootG = new CogImage8Root();
            cogRootG.Initialize(width, height, bufG, width, bufG);
            var cogRootB = new CogImage8Root();
            cogRootB.Initialize(width, height, bufB, width, bufB);
            var cogImage = new CogImage24PlanarColor();
            cogImage.SetRoots(cogRootR, cogRootB, cogRootG);
            return cogImage;
        }
        public static ICogImage ConvertColorImageToCognexImage3(byte[] colorImageByte, int width, int height)
        {
            var rawSize = (width * height);
            var bufR = new SafeMalloc(rawSize);
            Marshal.Copy(colorImageByte, 0, bufR, rawSize);

            var bufG = new SafeMalloc(rawSize);
            Marshal.Copy(colorImageByte, rawSize, bufG, rawSize);

            var bufB = new SafeMalloc(rawSize);
            Marshal.Copy(colorImageByte, rawSize * 2, bufB, rawSize);

            var cogRootR = new CogImage8Root();
            cogRootR.Initialize(width, height, bufR, width, bufR);
            var cogRootG = new CogImage8Root();
            cogRootG.Initialize(width, height, bufG, width, bufG);
            var cogRootB = new CogImage8Root();
            cogRootB.Initialize(width, height, bufB, width, bufB);
            var cogImage = new CogImage24PlanarColor();
            cogImage.SetRoots(cogRootR, cogRootG, cogRootB);
            return cogImage;
        }
#endif
        private enum ColorChannel
        {
            B, G, R
        }
        private static Mat ColorChannelSeperate(Mat mat, ColorChannel colorChannel)
        {
            if (mat == null) return null;
            Mat rtMat = new Mat();


            if (mat.Depth() == MatType.CV_8UC1)// && mat.Channels() == 3)
            {

                Mat[] vctMat;
                Cv2.Split(mat, out vctMat);

                if (colorChannel == ColorChannel.B)
                {
                    rtMat = vctMat[(int)ColorChannel.B].Clone();
                }
                if (colorChannel == ColorChannel.G)
                {
                    rtMat = vctMat[(int)ColorChannel.G].Clone();
                }
                if (colorChannel == ColorChannel.R)
                {
                    rtMat = vctMat[(int)ColorChannel.R].Clone();
                }

                foreach (Mat matMat in vctMat)
                {
                    matMat.Dispose();
                }

                return rtMat;
            }
            return null;
        }

        private static ICogImage CovertImage(byte[] dataR, byte[] dataG, byte[] dataB, int width, int height)
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

        private static ICogImage CovertImage(byte[] dataMono, int width, int height)
        {
            var dataSize = width * height;

            var buffer = new SafeMalloc(dataSize);
            Marshal.Copy(dataMono, 0, buffer, dataSize);

            var cogRootR = new CogImage8Root();
            cogRootR.Initialize(width, height, buffer, width, buffer);

            var cogImage = new CogImage8Grey();
            cogImage.SetRoot(cogRootR);

            return cogImage;
        }
        public static void DisposeICogmage(ICogImage cogImage)
        {
            if (cogImage is CogImage8Grey grayImage)
            {
                grayImage.Dispose();
                grayImage = null;
            }
            if (cogImage is CogImage24PlanarColor colorImage)
            {
                colorImage.Dispose();
                colorImage = null;
            }
        }

        public static void DisposeDisplay(CogRecordDisplay display)
        {
            if (display.Image is CogImage8Grey grayImage)
            {
                grayImage.Dispose();
                grayImage = null;
            }
            if (display.Image is CogImage24PlanarColor colorImage)
            {
                colorImage.Dispose();
                colorImage = null;
            }
        }
        public static void DisposeDisplay(CogDisplay display)
        {
            if (display.Image is CogImage8Grey grayImage)
            {
                grayImage.Dispose();
                grayImage = null;
            }
            if (display.Image is CogImage24PlanarColor colorImage)
            {
                colorImage.Dispose();
                colorImage = null;
            }
        }
     
    }
}
