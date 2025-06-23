using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing;
using FalconEngine.Models;
using FalconEngineTest.Utils;

namespace FalconEngineTest.DomParsing
{
    public class DoctypeParseTest
    {
        [Theory]
        [InlineData("<!DOCTYPE html>")]
        [InlineData("<!DocType html>")]
        [InlineData("<!Doctype html>")]
        [InlineData("<!doctype html>")]
        public void ParseModernDoctype(string doctypeHtml)
        {
            var doctypeParse = new DoctypeParse();

            var tagHtml = doctypeParse.Parse(doctypeHtml);

            Assert.Equal(NameTagEnum.doctype, tagHtml.NameTag);
            Assert.Equal(string.Empty, tagHtml.Content);
            Assert.Equal(TagFamilyEnum.NoEnd, tagHtml.TagFamily);
            Assert.Equal(doctypeHtml, tagHtml.TagStart);
            Assert.True(tagHtml.IsValid);
            Assert.Null(tagHtml.TagEnd);
        }


        [Theory]
        [InlineData("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">")]
        [InlineData("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\">")]
        [InlineData("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Frameset//EN\" \"http://www.w3.org/TR/html4/frameset.dtd\">")]
        public void ParseDoctypeHtml401(string doctypeHtml)
        {
            var doctypeParse = new DoctypeParse();

            var tagHtml = doctypeParse.Parse(doctypeHtml);

            Assert.Equal(NameTagEnum.doctype, tagHtml.NameTag);
            Assert.Equal(string.Empty, tagHtml.Content);
            Assert.Equal(TagFamilyEnum.NoEnd, tagHtml.TagFamily);
            Assert.Equal(doctypeHtml, tagHtml.TagStart);
            Assert.True(tagHtml.IsValid);
            Assert.Null(tagHtml.TagEnd);
        }
    }
}