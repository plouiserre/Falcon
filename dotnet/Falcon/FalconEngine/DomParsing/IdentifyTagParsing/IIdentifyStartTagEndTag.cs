using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public interface IIdentifyStartTagEndTag
    {
        public string StartTag { get; set; }

        public string EndTag { get; set; }
        void DetermineStartEndTags(string html);
    }
}