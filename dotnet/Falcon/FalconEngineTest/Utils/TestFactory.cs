using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;

namespace FalconEngineTest.Utils
{
    public static class TestFactory
    {


        private static IdentifyStartTagEndTag InitIdentifyStartTagEndTag()
        {
            return new IdentifyStartTagEndTag();
        }

        public static DeleteUselessSpace InitDeleteUselessSpace()
        {
            return new DeleteUselessSpace(InitIdentifyStartTagEndTag());
        }

        public static AttributeTagParser InitAttributeTagParser()
        {
            return new AttributeTagParser(InitIdentifyStartTagEndTag());
        }

        public static IdentifyTagName InitIdentifyTagName()
        {
            return new IdentifyTagName(InitIdentifyStartTagEndTag());
        }

        public static IdentifyTag InitIdentifyTag()
        {
            var attributeTagParser = InitAttributeTagParser();
            var identifyTag = new IdentifyTag(InitDeleteUselessSpace(), attributeTagParser, InitIdentifyTagName(), InitIdentifyStartTagEndTag());
            return identifyTag;
        }

        public static InitiateParser InitInitiateParser()
        {
            var identifyTag = InitIdentifyTag();
            var initiate = new InitiateParser(InitDeleteUselessSpace(), identifyTag, InitIdentifyStartTagEndTag(), InitAttributeTagParser());
            return initiate;
        }

    }
}