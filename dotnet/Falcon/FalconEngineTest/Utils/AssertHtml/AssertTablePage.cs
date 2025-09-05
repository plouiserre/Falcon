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
        public static void AssertTable(TagModel table)
        {
            string content = HtmlPageTableData.GetHtml(TagHtmlTable.postTable).Replace("<table>", string.Empty).Replace("</table>", string.Empty);
            Assert.Equal(NameTagEnum.table, table.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, table.TagFamily);
            Assert.Equal("<table>", table.TagStart);
            Assert.Equal("</table>", table.TagEnd);
            Assert.Equal(content, table.Content);
            Assert.Null(table.Attributes);
            AssertThead(table.Children[0]);
            AssertTBody(table.Children[1]);
        }

        public static void AssertThead(TagModel thead)
        {
            string content = "<tr><th scope=\"col\">Title</th><th scope=\"col\">Description</th><th scope=\"col\">Type</th><th scope=\"col\">Level</th></tr>";
            Assert.Equal(NameTagEnum.thead, thead.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, thead.TagFamily);
            Assert.Equal("<thead>", thead.TagStart);
            Assert.Equal("</thead>", thead.TagEnd);
            Assert.Equal(content, thead.Content);
            Assert.Null(thead.Attributes);
            AssertTrHead(thead.Children[0]);
        }

        private static void AssertTrHead(TagModel tr)
        {
            string content = "<th scope=\"col\">Title</th><th scope=\"col\">Description</th><th scope=\"col\">Type</th><th scope=\"col\">Level</th>";
            Assert.Equal(NameTagEnum.tr, tr.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tr.TagFamily);
            Assert.Equal("<tr>", tr.TagStart);
            Assert.Equal("</tr>", tr.TagEnd);
            Assert.Equal(content, tr.Content);
            Assert.Null(tr.Attributes);
            AssertThHead(tr.Children[0], "Title");
            AssertThHead(tr.Children[1], "Description");
            AssertThHead(tr.Children[2], "Type");
            AssertThHead(tr.Children[3], "Level");
        }

        private static void AssertThHead(TagModel th, string label)
        {
            Assert.Equal(NameTagEnum.th, th.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, th.TagFamily);
            Assert.Equal("<th scope=\"col\">", th.TagStart);
            Assert.Equal("</th>", th.TagEnd);
            Assert.Equal(label, th.Content);
            Assert.Equal("scope", th.Attributes[0].FamilyAttribute);
            Assert.Equal("col", th.Attributes[0].Value);
        }

        public static void AssertTBody(TagModel tbody)
        {
            string content = "<tr><td>Software Engineer</td><td>Make software from specifications</td><td>Technical</td><td>1</td></tr><tr><td>Product Owner</td><td>Create and ordered features from the wishes of the business</td><td>Product</td><td>1</td></tr><tr><td>Technical Leader</td><td>Help developer to build software for the business in the a good way</td><td>Technical</td><td>2</td></tr><tr><td>Engineer Manager</td><td>Manager of a team</td><td>Management</td><td>2</td></tr><tr><td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td></tr><tr><td>Director</td><td>Manager of a departement</td><td>Management</td><td>3</td></tr>";
            Assert.Equal(NameTagEnum.tbody, tbody.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tbody.TagFamily);
            Assert.Equal("<tbody>", tbody.TagStart);
            Assert.Equal("</tbody>", tbody.TagEnd);
            Assert.Equal(content, tbody.Content);
            Assert.Null(tbody.Attributes);
            AssertSoftwareEngineerTr(tbody.Children[0]);
            AssertProductOwnerTr(tbody.Children[1]);
            AssertTechnicalLeaderTr(tbody.Children[2]);
            AssertEngineerManagerTr(tbody.Children[3]);
            AssertArchitecteTr(tbody.Children[4]);
            AssertDirectorTr(tbody.Children[5]);
        }

        private static void AssertSoftwareEngineerTr(TagModel tr)
        {
            string content = "<td>Software Engineer</td><td>Make software from specifications</td><td>Technical</td><td>1</td>";
            Assert.Equal(NameTagEnum.tr, tr.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tr.TagFamily);
            Assert.Equal("<tr>", tr.TagStart);
            Assert.Equal("</tr>", tr.TagEnd);
            Assert.Null(tr.Attributes);
            Assert.Equal(content, tr.Content);
            AssertTd(tr.Children[0], "Software Engineer");
            AssertTd(tr.Children[1], "Make software from specifications");
            AssertTd(tr.Children[2], "Technical");
            AssertTd(tr.Children[3], "1");
        }

        private static void AssertProductOwnerTr(TagModel tr)
        {
            string content = "<td>Product Owner</td><td>Create and ordered features from the wishes of the business</td><td>Product</td><td>1</td>";
            Assert.Equal(NameTagEnum.tr, tr.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tr.TagFamily);
            Assert.Equal("<tr>", tr.TagStart);
            Assert.Equal("</tr>", tr.TagEnd);
            Assert.Null(tr.Attributes);
            Assert.Equal(content, tr.Content);
            AssertTd(tr.Children[0], "Product Owner");
            AssertTd(tr.Children[1], "Create and ordered features from the wishes of the business");
            AssertTd(tr.Children[2], "Product");
            AssertTd(tr.Children[3], "1");
        }

        private static void AssertTechnicalLeaderTr(TagModel tr)
        {
            string content = "<td>Technical Leader</td><td>Help developer to build software for the business in the a good way</td><td>Technical</td><td>2</td>";
            Assert.Equal(NameTagEnum.tr, tr.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tr.TagFamily);
            Assert.Equal("<tr>", tr.TagStart);
            Assert.Equal("</tr>", tr.TagEnd);
            Assert.Null(tr.Attributes);
            Assert.Equal(content, tr.Content);
            AssertTd(tr.Children[0], "Technical Leader");
            AssertTd(tr.Children[1], "Help developer to build software for the business in the a good way");
            AssertTd(tr.Children[2], "Technical");
            AssertTd(tr.Children[3], "2");
        }

        private static void AssertEngineerManagerTr(TagModel tr)
        {
            string content = "<td>Engineer Manager</td><td>Manager of a team</td><td>Management</td><td>2</td>";
            Assert.Equal(NameTagEnum.tr, tr.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tr.TagFamily);
            Assert.Equal("<tr>", tr.TagStart);
            Assert.Equal("</tr>", tr.TagEnd);
            Assert.Null(tr.Attributes);
            Assert.Equal(content, tr.Content);
            AssertTd(tr.Children[0], "Engineer Manager");
            AssertTd(tr.Children[1], "Manager of a team");
            AssertTd(tr.Children[2], "Management");
            AssertTd(tr.Children[3], "2");
        }

        public static void AssertArchitecteTr(TagModel tr)
        {
            string content = "<td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td>";
            Assert.Equal(NameTagEnum.tr, tr.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tr.TagFamily);
            Assert.Equal("<tr>", tr.TagStart);
            Assert.Equal("</tr>", tr.TagEnd);
            Assert.Null(tr.Attributes);
            Assert.Equal(content, tr.Content);
            AssertTd(tr.Children[0], "Architect");
            AssertTd(tr.Children[1], "Responsible of the quality and the durability of the tech");
            AssertTd(tr.Children[2], "Technical");
            AssertTd(tr.Children[3], "3");
        }

        public static void AssertDirectorTr(TagModel tr)
        {
            string content = "<td>Director</td><td>Manager of a departement</td><td>Management</td><td>3</td>";
            Assert.Equal(NameTagEnum.tr, tr.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, tr.TagFamily);
            Assert.Equal("<tr>", tr.TagStart);
            Assert.Equal("</tr>", tr.TagEnd);
            Assert.Null(tr.Attributes);
            Assert.Equal(content, tr.Content);
            AssertTd(tr.Children[0], "Director");
            AssertTd(tr.Children[1], "Manager of a departement");
            AssertTd(tr.Children[2], "Management");
            AssertTd(tr.Children[3], "3");
        }

        private static void AssertTd(TagModel td, string label)
        {
            Assert.Equal(NameTagEnum.td, td.NameTag);
            Assert.Equal(TagFamilyEnum.WithEnd, td.TagFamily);
            Assert.Equal("<td>", td.TagStart);
            Assert.Equal("</td>", td.TagEnd);
            Assert.Null(td.Attributes);
            Assert.Equal(label, td.Content);
            Assert.Null(td.Children);
        }
    }
}