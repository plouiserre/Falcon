using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;

namespace FalconEngine.CleanData
{
    public interface IExtractHtmlRemaining
    {
        string Extract(TagModel tag, string html, ExtractionMode extractionMode);
    }
}