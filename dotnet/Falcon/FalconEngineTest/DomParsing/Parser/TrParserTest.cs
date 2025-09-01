using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser
{
    public class TrParserTest
    {
        [Fact]
        public void ParseAndValidateTrWithManyTexts()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.architectTable);
            var parser = TestFactory.InitTrParser();

            var tag = parser.Parse(html);
            bool isValid = parser.IsValid();

            AssertTablePage.AssertArchitecteTr(tag);
            Assert.True(isValid);
        }

        //faire un test avec validation attributs
        //faire un test avec validation ko lvl 1
        //faire un test avec validation ko lvl 2
    }
}