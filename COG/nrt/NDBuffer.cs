using System;

namespace nrt
{
    internal class NDBuffer
    {
        private Shape shape;
        private object dTYPE_UINT8;

        public NDBuffer(Shape shape, object dTYPE_UINT8)
        {
            this.shape = shape;
            this.dTYPE_UINT8 = dTYPE_UINT8;
        }

        internal NDBuffer clone()
        {
            throw new NotImplementedException();
        }

        internal void copy_from_buffer_uint8(byte[] managedArray, ulong length)
        {
            throw new NotImplementedException();
        }

        internal void copy_to_buffer_uint8(byte[] byte_buff, ulong length)
        {
            throw new NotImplementedException();
        }

        internal void Dispose()
        {
            throw new NotImplementedException();
        }

        internal Shape get_shape()
        {
            throw new NotImplementedException();
        }

        internal int get_total_size()
        {
            throw new NotImplementedException();
        }
    }
}