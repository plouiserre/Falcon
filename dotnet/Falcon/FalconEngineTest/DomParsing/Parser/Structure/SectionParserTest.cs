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

        [Fact]
        public void ParseSectionAndNoValidateIt()
        {
            string html = "<section alt=\"title\"><h1>Hello world</h1></section>";
            var sectionParser = TestFactory.InitSectionParser();

            sectionParser.Parse(html);
            bool isValid = sectionParser.IsValid();

            Assert.False(isValid);
        }



        [Fact]
        public void ParseSectionAndNoValidateItBecauseH1NotValid()
        {
            string html = "<section><h1 alt=\"title\">Hello world</h1></section>";
            var sectionParser = TestFactory.InitSectionParser();

            sectionParser.Parse(html);
            bool isValid = sectionParser.IsValid();

            Assert.False(isValid);
        }
    }
}