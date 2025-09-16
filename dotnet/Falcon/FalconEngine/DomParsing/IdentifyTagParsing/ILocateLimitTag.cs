using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public interface ILocateLimitTag
    {
        public int? Search(string startTag, string html);
    }
}
