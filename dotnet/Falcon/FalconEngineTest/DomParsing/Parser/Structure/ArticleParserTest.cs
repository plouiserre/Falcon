using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser.Structure
{
    public class ArticleParserTest
    {
        [Fact]
        public void ArticleParseAndValidateIt()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.articleTag);
            var articleParser = TestFactory.InitArticleParser();

            var tag = articleParser.Parse(html);
            var isValid = articleParser.IsValid();

            AssertTablePage.AssertArticle(tag);
            Assert.True(isValid);
        }
    }
}