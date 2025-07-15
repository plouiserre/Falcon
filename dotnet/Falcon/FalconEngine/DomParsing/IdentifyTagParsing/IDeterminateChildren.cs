using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public interface IDeterminateChildren
    {
        List<TagModel> Find(string html);
    }
}