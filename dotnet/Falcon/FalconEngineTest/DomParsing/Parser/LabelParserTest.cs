using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.DomParsing.Parser
{
    public class LabelParserTest
    {
        [Fact]
        public void LabelDateParser()
        {
            string html = HtmlPageFormData.GetHtml(TagHtmlForm.labelDate);
            var labelParse = TestFactory.InitLabelParser();

            var tag = labelParse.Parse(html);

            Assert.Equal("forAttr", tag.Attributes[0].FamilyAttribute);
            Assert.Equal("dBirthday", tag.Attributes[0].Value);
            Assert.Equal(NameTagEnum.label, tag.NameTag);
            Assert.Equal("Birthday", tag.Content);
            Assert.Null(tag.Children);
        }
    }
}