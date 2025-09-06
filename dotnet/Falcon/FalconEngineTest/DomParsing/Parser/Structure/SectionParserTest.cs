using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.AssertHtml;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser.Structure
{
    public class SectionParserTest
    {
        [Fact]
        public void ParseSectionAndValidateIt()
        {
            string html = HtmlPageTableData.GetHtml(TagHtmlTable.sectionTag);
            var sectionParser = TestFactory.InitSectionParser();

            var tag = sectionParser.Parse(html);
            bool isValid = sectionParser.IsValid();

            AssertTablePage.AssertSection(tag);
            Assert.True(isValid);
        }
    }
}