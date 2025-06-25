using FalconEngine.DomParsing;
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
            var htmlTagParse = new HtmlTagParse();
            _htmlParsing = new HtmlParsing(htmlTagParse, HtmlData.HtmlSimpleWithSpace);
        }

        [Fact]
        public void IsSimplePageIsAnalyzedCorrectly()
        {
            HtmlPage htmlPage = HtmlPageData.InitHtmlPage();
            var engine = new HtmlEngine(_htmlParsing);

            var engineResult = engine.Calculate(HtmlData.HtmlSimpleWithSpace);

            Assert.True(CompareTags(htmlPage.Tags, engineResult.Tags));
        }


        //TODO externalize in a new file
        private bool CompareTags(List<TagModel> allExpected, List<TagModel> results)
        {
            if (allExpected.Count != results.Count)
                return false;
            for (int i = 0; i < allExpected.Count; i++)
            {
                TagModel expected = allExpected[i];
                TagModel result = results[i];
                string expectedContent = AssertUtils.DeleteUselessSpace(expected.Content);
                string resultContent = AssertUtils.DeleteUselessSpace(result.Content);
                if (expectedContent != resultContent)
                    return false;
                if (expected.NameTag != result.NameTag)
                    return false;
                if (expected.TagFamily != result.TagFamily)
                    return false;
                if (expected.Attributes != null && result.Attributes != null)
                {
                    if (expected.Attributes.Count != result.Attributes.Count)
                        return false;
                    for (int j = 0; j < expected.Attributes.Count; j++)
                    {
                        var expectedAttribute = expected.Attributes[j];
                        var resultAttribute = result.Attributes[j];
                        if (expectedAttribute.FamilyAttribute != resultAttribute.FamilyAttribute)
                            return false;
                        if (expectedAttribute.Value != resultAttribute.Value)
                            return false;
                    }
                }
                else if ((expected.Attributes == null && result.Attributes != null) ||
                    (expected.Attributes != null && result.Attributes == null))
                    return false;
            }
            return true;
        }

    }
}