using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public interface IDeterminateContent
    {
        string FindContent(string html, string tagStart, string tagEnd);
    }
}