using System;

namespace nrt
{
    internal class Predictor
    {
        private string model_path;
        private object mODELIO_OUT_PROB;
        private int v1;
        private int batchSize;
        private bool v2;
        private bool v3;

        public Predictor()
        {
        }

        public Predictor(string model_path, object mODELIO_OUT_PROB, int v1, int batchSize, bool v2, bool v3)
        {
            this.model_path = model_path;
            this.mODELIO_OUT_PROB = mODELIO_OUT_PROB;
            this.v1 = v1;
            this.batchSize = batchSize;
            this.v2 = v2;
            this.v3 = v3;
        }

        internal void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}