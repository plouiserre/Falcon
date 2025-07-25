using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Engine;
using FalconEngine.Models;
using FalconEngineTest.Data;
using FalconEngineTest.Utils;
using Xunit;

namespace FalconEngineTest.Engine
{
    public class HtmlEngineTest
    {

        private HtmlParsing _htmlParsing { get; set; }

        public HtmlEngineTest()
        {
            var deleteUselessSpace = TestFactory.InitDeleteUselessSpace();
            var determinateChildren = TestFactory.InitDeterminateChildren();
            var identifyTag = TestFactory.InitIdentifyTag();
            var doctypeParser = new DoctypeParser(identifyTag);
            var htmlTagParser = TestFactory.InitHtmlTagParser(true);
            var extractHtmlRemaining = new ExtractHtmlRemaining();
            var attributeTagManager = new AttributeTagManager();
            var manageChildrenTag = TestFactory.InitDeterminateChildren();
            var headParser = new HeadParser(deleteUselessSpace, identifyTag, determinateChildren);
            var spanParse = new SpanParser(identifyTag, attributeTagManager, manageChildrenTag, NameTagEnum.span);
            _htmlParsing = new HtmlParsing(doctypeParser, htmlTagParser, headParser,
                        extractHtmlRemaining, TestFactory.InitAttributeTagManager(true), spanParse);
        }

        [Fact]
        public void IsSimplePageIsAnalyzedCorrectly()
        {
            HtmlPage htmlPage = HtmlPageData.InitHtmlPage();
            var engine = new HtmlEngine(_htmlParsing);

            var engineResult = engine.Calculate(HtmlData.HtmlSimpleWithSpaceDoctype);

            Assert.True(CompareTags(htmlPage.Tags, engineResult.Tags));
        }


        private bool CompareTags(List<TagModel> allExpected, List<TagModel> results)
        {
            if (allExpected.Count != results.Count)
                return false;
            for (int i = 0; i < allExpected.Count; i++)
            {
                if (i == 5)
                {
                    var test = "aezf";
                }
                var expectedTag = allExpected[i];
                var resultTag = results[i];
                if (!CheckTagModel(expectedTag, resultTag))
                    return false;
            }
            return true;
        }

        private bool CheckTagModel(TagModel expected, TagModel result)
        {
            if (expected.Content != result.Content)
                return false;
            if (expected.NameTag != result.NameTag)
                return false;
            if (expected.TagFamily != result.TagFamily)
                return false;
            if (expected.TagStart != result.TagStart)
                return false;
            if (expected.TagEnd != result.TagEnd)
                return false;
            if (expected.Attributes != null && result.Attributes != null)
            {
                if (expected.Attributes.Count != result.Attributes.Count)
                    return false;
                for (int j = 0; j < expected.Attributes.Count; j++)
                {
                    if (!CheckAttribute(expected.Attributes[j], result.Attributes[j]))
                        return false;
                }
            }
            if (expected.Children != null && result.Children != null)
            {
                if (expected.Children.Count != result.Children.Count)
                    return false;
                for (int j = 0; j < expected.Children.Count; j++)
                {
                    if (!CheckTagModel(expected.Children[j], result.Children[j]))
                        return false;
                }
            }
            else if ((expected.Attributes == null && result.Attributes != null) ||
                (expected.Attributes != null && result.Attributes == null))
                return false;
            return true;
        }

        private bool CheckAttribute(AttributeModel expectedAttribute, AttributeModel resultAttribute)
        {
            if (expectedAttribute.FamilyAttribute != resultAttribute.FamilyAttribute)
                return false;
            if (expectedAttribute.Value != resultAttribute.Value)
                return false;
            return true;
        }

    }
}