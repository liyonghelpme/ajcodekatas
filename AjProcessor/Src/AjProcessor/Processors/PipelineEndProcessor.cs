namespace AjProcessor.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class PipelineEndProcessor : BaseProcessor
    {
        private PipelineProcessor pipeline;

        internal PipelineEndProcessor(PipelineProcessor pipeline)
        {
            this.pipeline = pipeline;
        }

        public override void ProcessMessage(Message message)
        {
            this.pipeline.PostMessage(message);
        }
    }
}
