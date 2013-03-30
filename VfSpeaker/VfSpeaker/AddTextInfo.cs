using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VfSpeaker
{
    public interface IAddTextInfo
    {
        Guid DocumentId { get; }
        string Text { get; }
        IProgressCallback ProgressCallback { get; }
    }

    public class AddTextInfo : IAddTextInfo
    {
        public AddTextInfo(Guid documentId, string text, IProgressCallback progressCallback)
        {
            DocumentId = documentId;
            Text = text;
            ProgressCallback = progressCallback;
        }

        public Guid DocumentId { get; private set; }

        public string Text { get; private set; }

        public IProgressCallback ProgressCallback { get; private set; }
    }
}
