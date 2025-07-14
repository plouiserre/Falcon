using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class TitleParserTest
    {
        [Theory]
        [InlineData("My blog")]
        [InlineData("My website")]
        public void ParseTitleTag(string titleName)
        {
            var identifyTag = TestFactory.InitIdentifyTag();
            var determinateContent = TestFactory.InitDeterminateContent();
            string html = $"<title>{titleName}</title>";
            var parseTitle = new TitleParser(identifyTag, determinateContent);

            var tag = parseTitle.Parse(html);

            Assert.Null(tag.Attributes);
            Assert.Equal(NameTagEnum.title, tag.NameTag);
            Assert.Equal(titleName, tag.Content);
            Assert.Equal(TagFamilyEnum.WithEnd, tag.TagFamily);
            Assert.Equal("<title>", tag.TagStart);
            Assert.Equal("</title>", tag.TagEnd);
        }
    }
}