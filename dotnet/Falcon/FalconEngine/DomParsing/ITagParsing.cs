using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{
    public interface ITagParsing
    {
        TagModel Parse(string html);
        bool IsValid(TagModel tag);
        string CleanHtml(TagModel tag, string html);
    }
}