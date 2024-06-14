using System;

namespace nrt
{
    internal class Input
    {
        public Input()
        {
        }

        public Input(Input inputImage)
        {
            InputImage = inputImage;
        }

        public Input InputImage { get; }

        internal void clear()
        {
      //      throw new NotImplementedException();
        }

        internal void Dispose()
        {
      //      throw new NotImplementedException();
        }

        internal void extend(string v1, int v2)
        {
      //      throw new NotImplementedException();
        }

        internal void extend(NDBuffer inputImage)
        {
       //     throw new NotImplementedException();
        }




    }
}