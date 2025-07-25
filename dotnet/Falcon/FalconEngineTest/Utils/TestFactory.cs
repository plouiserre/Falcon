using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.DomParsing.Parser.Attribute;

namespace FalconEngineTest.Utils
{
    public static class TestFactory
    {

        private static IExtractHtmlRemaining InitExtractHtmlRemaining()
        {
            return new ExtractHtmlRemaining();
        }

        private static IdentifyStartTagEndTag InitIdentifyStartTagEndTag()
        {
            return new IdentifyStartTagEndTag();
        }

        public static AnalyzeAttributes InitAnalyzeAttributes()
        {
            return new AnalyzeAttributes();
        }

        public static LinkParser InitLinkParser()
        {
            return new LinkParser(InitIdentifyTag(), InitAttributeTagManager(true));
        }

        public static AParser InitAParser()
        {
            return new AParser(InitIdentifyTag(), InitAttributeTagManager(true), InitDeleteUselessSpace());
        }

        public static SpanParser InitSpanParser()
        {
            return new SpanParser(TestFactory.InitIdentifyTag(), TestFactory.InitAttributeTagManager(true),
                            TestFactory.InitDeterminateChildren(), FalconEngine.Models.NameTagEnum.span);
        }

        public static PParser InitPParser()
        {
            return new PParser(TestFactory.InitIdentifyTag(), TestFactory.InitDeterminateChildren(),
                                TestFactory.InitAttributeTagManager(true), FalconEngine.Models.NameTagEnum.p);
        }

        public static AttributeTagManager InitAttributeTagManager(bool isSettingAttributes)
        {
            var attributeTagManager = new AttributeTagManager();
            if (isSettingAttributes)
                attributeTagManager.SetAttributes();
            return attributeTagManager;
        }

        public static HtmlTagParser InitHtmlTagParser(bool isSettingAttributes)
        {
            return new HtmlTagParser(TestFactory.InitIdentifyTag(), TestFactory.InitDeterminateContent(),
                TestFactory.InitAttributeTagManager(isSettingAttributes));
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
            return new AttributeTagParser(InitIdentifyStartTagEndTag(), InitAnalyzeAttributes());
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
                InitDeterminateContent(), InitDeterminateChildren(), InitAttributeTagManager(false));
            return initiate;
        }

        public static HeadParser InitHeadParser()
        {
            var headParser = new HeadParser(InitDeleteUselessSpace(), InitIdentifyTag(), InitDeterminateChildren());
            return headParser;
        }

        public static ManageChildrenTag InitDeterminateChildren()
        {
            var manageChildrenTag = new ManageChildrenTag(InitDeleteUselessSpace(), InitIdentifyTag(),
            InitIdentifyStartTagEndTag(), InitAttributeTagParser(), InitDeterminateContent(), InitExtractHtmlRemaining(),
            InitAttributeTagManager(false));
            return manageChildrenTag;
        }

    }
}