using FalconEngine.DomParsing;
using FalconEngine.Models;

namespace FalconEngineTest.DomParsing
{
    public class DiscoverAttributsTest
    {
        public DiscoverAttributsTest()
        {

        }

        [Fact]
        public void DetectAttributLangInHtmlTag()
        {
            var discoverAttributs = new DiscoverAttributs();

            string tagHtmlStart = "<html lang=\"en\">";
            var attributs = discoverAttributs.Find(tagHtmlStart);

            Assert.Single(attributs);
            Assert.Equal(FamilyAttributeEnum.lang, attributs[0].FamilyAttribute);
            Assert.Equal("en", attributs[0].Value);
        }


        [Fact]
        public void DetectAttributsLangAndDirInHtmlTag()
        {
            var discoverAttributs = new DiscoverAttributs();

            string tagHtmlStart = "<html lang=\"en\" dir=\"auto\">";
            var attributs = discoverAttributs.Find(tagHtmlStart);

            Assert.Equal(2, attributs.Count);
            Assert.Equal(FamilyAttributeEnum.lang, attributs[0].FamilyAttribute);
            Assert.Equal("en", attributs[0].Value);
            Assert.Equal(FamilyAttributeEnum.dir, attributs[1].FamilyAttribute);
            Assert.Equal("auto", attributs[1].Value);
        }


        [Fact]
        public void DetectAttributsLangDirAndXmlnsInHtmlTag()
        {
            var discoverAttributs = new DiscoverAttributs();

            string tagHtmlStart = "<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">";
            var attributs = discoverAttributs.Find(tagHtmlStart);

            Assert.Equal(3, attributs.Count);
            Assert.Equal(FamilyAttributeEnum.lang, attributs[0].FamilyAttribute);
            Assert.Equal("en", attributs[0].Value);
            Assert.Equal(FamilyAttributeEnum.dir, attributs[1].FamilyAttribute);
            Assert.Equal("auto", attributs[1].Value);
            Assert.Equal(FamilyAttributeEnum.xmlns, attributs[2].FamilyAttribute);
            Assert.Equal("http://www.w3.org/1999/xhtml", attributs[2].Value);
        }




        [Fact]
        public void DetectAttributsLangDirAndXmlnsInHtmlTagWithMultipleSpace()
        {
            var discoverAttributs = new DiscoverAttributs();

            string tagHtmlStart = "<html lang=\"en\"  dir=\"auto\"   xmlns=\"http://www.w3.org/1999/xhtml\">";
            var attributs = discoverAttributs.Find(tagHtmlStart);

            Assert.Equal(3, attributs.Count);
            Assert.Equal(FamilyAttributeEnum.lang, attributs[0].FamilyAttribute);
            Assert.Equal("en", attributs[0].Value);
            Assert.Equal(FamilyAttributeEnum.dir, attributs[1].FamilyAttribute);
            Assert.Equal("auto", attributs[1].Value);
            Assert.Equal(FamilyAttributeEnum.xmlns, attributs[2].FamilyAttribute);
            Assert.Equal("http://www.w3.org/1999/xhtml", attributs[2].Value);
        }
    }
}