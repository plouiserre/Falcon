using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.Parser
{
    public interface IAttributeTagParser
    {
        public List<AttributeModel> Parse(string html);
    }
}