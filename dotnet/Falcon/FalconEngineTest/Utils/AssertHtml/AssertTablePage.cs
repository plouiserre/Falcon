using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.Utils.AssertHtml
{
    public static class AssertTablePage
    {
        public static void AssertArchitecteTr(TagModel tr)
        {
            string content = "<td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td>";
            Assert.Equal(NameTagEnum.tr, tr.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tr.TagFamily);
            Assert.Equal("<tr>", tr.TagStart);
            Assert.Equal("</tr>", tr.TagEnd);
            Assert.Null(tr.Attributes);
            Assert.Equal(content, tr.Content);
            AssertLabelArchitecte(tr.Children[0]);
            AssertDescriptionArchitecte(tr.Children[1]);
            AssertTypeArchitecte(tr.Children[2]);
            AssertLevelArchitecte(tr.Children[3]);
        }

        private static void AssertLabelArchitecte(TagModel td)
        {
            Assert.Equal(NameTagEnum.td, td.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, td.TagFamily);
            Assert.Equal("<td>", td.TagStart);
            Assert.Equal("</td>", td.TagEnd);
            Assert.Null(td.Attributes);
            Assert.Equal("Architect", td.Content);
            Assert.Null(td.Children);
        }

        private static void AssertDescriptionArchitecte(TagModel td)
        {
            Assert.Equal(NameTagEnum.td, td.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, td.TagFamily);
            Assert.Equal("<td>", td.TagStart);
            Assert.Equal("</td>", td.TagEnd);
            Assert.Null(td.Attributes);
            Assert.Equal("Responsible of the quality and the durability of the tech", td.Content);
            Assert.Null(td.Children);
        }

        private static void AssertTypeArchitecte(TagModel td)
        {
            Assert.Equal(NameTagEnum.td, td.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, td.TagFamily);
            Assert.Equal("<td>", td.TagStart);
            Assert.Equal("</td>", td.TagEnd);
            Assert.Null(td.Attributes);
            Assert.Equal("Technical", td.Content);
            Assert.Null(td.Children);
        }

        private static void AssertLevelArchitecte(TagModel td)
        {
            Assert.Equal(NameTagEnum.td, td.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, td.TagFamily);
            Assert.Equal("<td>", td.TagStart);
            Assert.Equal("</td>", td.TagEnd);
            Assert.Null(td.Attributes);
            Assert.Equal("3", td.Content);
            Assert.Null(td.Children);
        }
    }
}