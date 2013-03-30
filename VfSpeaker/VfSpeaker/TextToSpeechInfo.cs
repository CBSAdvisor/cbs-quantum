using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VfSpeaker
{
    public interface ITextToSpeechInfo
    {
        Guid DocumentId { get; }
        IProgressCallback ProgressCallback { get; }
    }

    public class TextToSpeechInfo : ITextToSpeechInfo
    {
        public TextToSpeechInfo(Guid documentId, IProgressCallback progressCallback)
        {
            DocumentId = documentId;
            ProgressCallback = progressCallback;
        }

        public Guid DocumentId { get; private set; }
        public IProgressCallback ProgressCallback { get; private set; }
    }
}
