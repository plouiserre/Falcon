using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.Engine
{
    public class HtmlTagParseTest
    {
        public HtmlTagParseTest()
        {

        }

        [Fact]
        public void ParseSimpleHtmlOneLine()
        {
            string html = HtmlData.HtmlSimple;
            var htmlTagParse = new HtmlTagParse();

            var tagHtml = htmlTagParse.Parse(html);

            Assert.Equal(NameTagEnum.html, tagHtml.NameTag);
            Assert.Equal(HtmlData.ContentHtmlSimple, tagHtml.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tagHtml.TagFamily);
        }


        //TODO decomment this UT and correct it in next US
        [Fact]
        public void ParseSimpleHtmlWithSpaceAndEscapment()
        {
            string html = HtmlData.HtmlSimpleWithSpace;
            var htmlTagParse = new HtmlTagParse();

            var tagHtml = htmlTagParse.Parse(html);

            Assert.Equal(NameTagEnum.html, tagHtml.NameTag);
            Assert.Equal(HtmlData.ContentHtmlSimpleWithSpace, tagHtml.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tagHtml.TagFamily);
        }

    }
}