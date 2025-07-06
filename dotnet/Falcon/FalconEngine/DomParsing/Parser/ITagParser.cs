using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public interface ITagParser
    {
        TagModel Parse(string html);
        bool IsValid(TagModel tag);
    }
}