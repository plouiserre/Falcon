using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public interface IIdentifyTagName
    {
        NameTagEnum FindTagName(string html);
    }
}