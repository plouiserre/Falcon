using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;

namespace FalconEngineTest.Utils
{
    public static class TestFactory
    {
        public static IdentifyTag InitIdentifyTag()
        {
            var deleteUselessSpace = new DeleteUselessSpace();
            var attributeTagParser = new AttributeTagParser();
            var identifyTagName = new IdentifyTagName();
            var identifyStartTagEndTag = new IdentifyStartTagEndTag();
            var identifyTag = new IdentifyTag(deleteUselessSpace, attributeTagParser, identifyTagName, identifyStartTagEndTag);
            return identifyTag;
        }

    }
}