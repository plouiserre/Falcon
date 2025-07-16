using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing.Parser
{
    public class DoctypeParserTest
    {
        private IdentifyTag _identifyTag;

        public DoctypeParserTest()
        {
            _identifyTag = TestFactory.InitIdentifyTag();
        }

        [Theory]
        [InlineData("<!DOCTYPE html>")]
        [InlineData("<!DocType html>")]
        [InlineData("<!Doctype html>")]
        [InlineData("<!doctype html>")]
        public void ParseModernDoctype(string doctypeHtml)
        {
            var doctypeParser = new DoctypeParser(_identifyTag);

            var tagHtml = doctypeParser.Parse(doctypeHtml);
            bool isValid = doctypeParser.IsValid();

            Assert.Equal(NameTagEnum.doctype, tagHtml.NameTag);
            Assert.Null(tagHtml.Content);
            Assert.Equal(TagFamilyEnum.NoEnd, tagHtml.TagFamily);
            Assert.Equal(doctypeHtml, tagHtml.TagStart);
            Assert.True(isValid);
            Assert.Null(tagHtml.TagEnd);
        }


        [Theory]
        [InlineData("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">")]
        [InlineData("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\">")]
        [InlineData("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Frameset//EN\" \"http://www.w3.org/TR/html4/frameset.dtd\">")]
        public void ParseDoctypeHtml401(string doctypeHtml)
        {
            var doctypeParser = new DoctypeParser(_identifyTag);

            var tagHtml = doctypeParser.Parse(doctypeHtml);
            bool isValid = doctypeParser.IsValid();

            Assert.Equal(NameTagEnum.doctype, tagHtml.NameTag);
            Assert.Null(tagHtml.Content);
            Assert.Equal(TagFamilyEnum.NoEnd, tagHtml.TagFamily);
            Assert.Equal(doctypeHtml, tagHtml.TagStart);
            Assert.True(isValid);
            Assert.Null(tagHtml.TagEnd);
        }


        [Theory]
        [InlineData("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">")]
        [InlineData("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">")]
        [InlineData("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Frameset//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd\">")]
        public void ParseDoctypeXHtml1(string doctypeHtml)
        {
            var doctypeParse = new DoctypeParser(_identifyTag);

            var tagHtml = doctypeParse.Parse(doctypeHtml);
            bool isValid = doctypeParse.IsValid();

            Assert.Equal(NameTagEnum.doctype, tagHtml.NameTag);
            Assert.Null(tagHtml.Content);
            Assert.Equal(TagFamilyEnum.NoEnd, tagHtml.TagFamily);
            Assert.Equal(doctypeHtml, tagHtml.TagStart);
            Assert.True(isValid);
            Assert.Null(tagHtml.TagEnd);
        }

        [Fact]
        public void ValidationFail()
        {
            var doctypeParse = new DoctypeParser(_identifyTag);

            var exception = Assert.Throws<ValidationParsingException>(() => doctypeParse.IsValid());

            Assert.Equal("Doctype validation is failing because parsing is not did", exception.Message);
            Assert.Equal(ErrorTypeParsing.validation, exception.ErrorType);
        }
    }
}