using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;

namespace FalconEngine.CleanData
{
    //TODO rename interface class and methods!!!!!!
    public interface IExtractHtmlRemaining
    {
        string Extract(TagModel tag, string html, ExtractionMode extractionMode);
        string Extract(string html, NameTagEnum nameTag);
        string FindHtmlParse(string html);
        NameTagEnum GetNameTag(string html);
    }
}