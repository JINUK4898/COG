using System;

namespace nrt
{
    internal class Result
    {
        internal object model_type;
        internal object bboxes;
        internal object blobs;
        private Result results;

        public Result(object results)
        {
        }

        public Result(Result results)
        {
            this.results = results;
        }

        internal void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}