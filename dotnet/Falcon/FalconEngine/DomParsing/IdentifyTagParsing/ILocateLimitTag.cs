using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public interface ILocateLimitTag
    {
        public int? Search(LimitMode mode, string startTag, string html);
        bool IndicateIsMultipleStartTag();
    }
}
