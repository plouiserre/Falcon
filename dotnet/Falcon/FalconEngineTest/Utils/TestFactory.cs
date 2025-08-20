using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.DomParsing.Parser.Attribute;
using FalconEngine.Models;

namespace FalconEngineTest.Utils
{
    public static class TestFactory
    {
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

        public static InputParser InitInputParser()
        {
            return new InputParser(InitIdentifyTag(), InitAttributeTagManager(true));
        }

        public static OptionParser InitOptionParser()
        {
            return new OptionParser(TestFactory.InitIdentifyTag(), InitAttributeTagManager(true));
        }

        public static SelectParser InitSelectParser()
        {
            return new SelectParser(TestFactory.InitIdentifyTag(), TestFactory.InitDeterminateChildren(), InitAttributeTagManager(true));
        }

        public static SpanParser InitSpanParser()
        {
            return new SpanParser(InitIdentifyTag(), InitAttributeTagManager(true), InitDeterminateChildren(), NameTagEnum.span);
        }

        public static PParser InitPParser()
        {
            return new PParser(TestFactory.InitIdentifyTag(), TestFactory.InitDeterminateChildren(),
                                TestFactory.InitAttributeTagManager(true));
        }

        public static DivParser InitDivParser()
        {
            return new DivParser(InitIdentifyTag(), InitDeterminateChildren(), InitAttributeTagManager(true));
        }

        public static BodyParser InitBodyParser()
        {
            return new BodyParser(InitIdentifyTag(), InitDeterminateChildren(), InitAttributeTagManager(true));
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
            return new HtmlTagParser(TestFactory.InitIdentifyTag(), TestFactory.InitDeterminateChildren(),
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
            InitIdentifyStartTagEndTag(), InitDeterminateContent(), InitAttributeTagManager(false));
            return manageChildrenTag;
        }
    }
}