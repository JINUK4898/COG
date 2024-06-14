using System;

namespace nrt
{
    internal class Shape
    {
        private int crop_h;
        private int crop_w;
        private int crop_c;

        public Shape()
        {
        }

        public Shape(int crop_h, int crop_w, int crop_c)
        {
            this.crop_h = crop_h;
            this.crop_w = crop_w;
            this.crop_c = crop_c;
        }

        internal void Dispose()
        {
            throw new NotImplementedException();
        }

        internal int get_axis(int v)
        {
            throw new NotImplementedException();
        }
    }
}