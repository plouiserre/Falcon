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

        public static DeterminateContent InitDeterminateContent()
        {
            return new DeterminateContent();
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
            return new IdentifyTagName();
        }

        public static IdentifyTag InitIdentifyTag()
        {
            var identifyTag = new IdentifyTag(InitDeleteUselessSpace(), InitAttributeTagParser(), InitIdentifyTagName(),
                            InitIdentifyStartTagEndTag(), InitDeterminateContent());
            return identifyTag;
        }

        public static InitiateParser InitInitiateParser()
        {
            var identifyTag = InitIdentifyTag();
            var initiate = new InitiateParser(InitDeleteUselessSpace(), identifyTag, InitIdentifyStartTagEndTag(),
                InitAttributeTagParser(), InitDeterminateContent());
            return initiate;
        }

        public static HeadParser InitHeadParser()
        {
            var headParser = new HeadParser(InitDeleteUselessSpace(), InitIdentifyTag(), InitIdentifyStartTagEndTag(),
            InitAttributeTagParser(), InitDeterminateContent());
            return headParser;
        }


    }
}