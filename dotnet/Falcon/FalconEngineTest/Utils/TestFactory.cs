using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.DomParsing.Parser.Attribute;
using FalconEngine.DomParsing.Parser.List;
using FalconEngine.DomParsing.Parser.Structure;
using FalconEngine.DomParsing.Parser.Table;
using FalconEngine.Models;
using FalconEngineTest.DomParsing.Parser;

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

        public static LabelParser InitLabelParser()
        {
            return new LabelParser(TestFactory.InitIdentifyTag(), InitAttributeTagManager(true));
        }

        public static OptionParser InitOptionParser()
        {
            return new OptionParser(TestFactory.InitIdentifyTag(), InitAttributeTagManager(true));
        }

        public static SelectParser InitSelectParser()
        {
            return new SelectParser(TestFactory.InitIdentifyTag(), TestFactory.InitManageChildren(), InitAttributeTagManager(true));
        }

        public static SpanParser InitSpanParser()
        {
            return new SpanParser(InitIdentifyTag(), InitAttributeTagManager(true), InitManageChildren());
        }

        public static H1Parser InitH1Parser()
        {
            return new H1Parser(InitIdentifyTag(), InitManageChildren(), InitAttributeTagManager(true));
        }

        public static LiParser InitLiParser()
        {
            return new LiParser(InitIdentifyTag(), InitManageChildren(), InitAttributeTagManager(true));
        }

        public static UlParser InitUlParser()
        {
            return new UlParser(InitIdentifyTag(), InitManageChildren(), InitAttributeTagManager(true));
        }

        public static PParser InitPParser()
        {
            return new PParser(TestFactory.InitIdentifyTag(), TestFactory.InitManageChildren(),
                                TestFactory.InitAttributeTagManager(true));
        }

        public static TdParser InitTdParser()
        {
            return new TdParser(TestFactory.InitIdentifyTag(), TestFactory.InitAttributeTagManager(true));
        }

        public static ThParser InitThParser()
        {
            return new ThParser(TestFactory.InitIdentifyTag(), TestFactory.InitAttributeTagManager(true));
        }

        public static TrParser InitTrParser()
        {
            return new TrParser(TestFactory.InitIdentifyTag(), TestFactory.InitManageChildren(),
                TestFactory.InitAttributeTagManager(true));
        }

        public static TBodyParser InitTbodyParser()
        {
            return new TBodyParser(TestFactory.InitIdentifyTag(), TestFactory.InitManageChildren(),
                TestFactory.InitAttributeTagManager(true));
        }

        public static TheadParser InitTheadParser()
        {
            return new TheadParser(InitIdentifyTag(), InitManageChildren(), InitAttributeTagManager(true));
        }

        public static TableParser InitTableParser()
        {
            return new TableParser(InitIdentifyTag(), InitManageChildren(), InitAttributeTagManager(true));
        }

        public static DivParser InitDivParser()
        {
            return new DivParser(InitIdentifyTag(), InitManageChildren(), InitAttributeTagManager(true));
        }

        public static SectionParser InitSectionParser()
        {
            return new SectionParser(InitIdentifyTag(), InitManageChildren(), InitAttributeTagManager(true));
        }

        public static ArticleParser InitArticleParser()
        {
            return new ArticleParser(InitIdentifyTag(), InitManageChildren(), InitAttributeTagManager(true));
        }

        public static FormParser InitFormParser()
        {
            return new FormParser(InitIdentifyTag(), InitManageChildren(), InitAttributeTagManager(true));
        }

        public static BodyParser InitBodyParser()
        {
            return new BodyParser(InitIdentifyTag(), InitManageChildren(), InitAttributeTagManager(true));
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
            return new HtmlTagParser(TestFactory.InitIdentifyTag(), TestFactory.InitManageChildren(),
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
                InitDeterminateContent(), InitManageChildren(), InitAttributeTagManager(false));
            return initiate;
        }

        public static HeadParser InitHeadParser()
        {
            var headParser = new HeadParser(InitDeleteUselessSpace(), InitIdentifyTag(), InitManageChildren());
            return headParser;
        }

        public static ManageChildrenTag InitManageChildren()
        {
            var manageChildrenTag = new ManageChildrenTag(InitDeleteUselessSpace(), InitIdentifyTag(),
            InitIdentifyStartTagEndTag(), InitDeterminateContent(), InitAttributeTagManager(false));
            return manageChildrenTag;
        }
    }
}