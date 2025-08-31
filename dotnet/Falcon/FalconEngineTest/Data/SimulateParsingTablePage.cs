using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;
using FalconEngineTest.Utils.HtmlData;

namespace FalconEngineTest.Data
{
    public class SimulateParsingTablePage
    {
        private static TagModel? _doctypeTag;
        private static TagModel? _htmlTag;
        private static TagModel? _headTag;
        private static HtmlPage? _htmlPage { get; set; }

        public static HtmlPage InitHtmlPage()
        {
            _doctypeTag = GetDoctypeTag();
            _htmlTag = GetTagHtml();
            var tags = new List<TagModel>() { _doctypeTag, _htmlTag };
            _htmlPage = new HtmlPage() { Tags = tags };
            return _htmlPage;
        }
        private static TagModel GetDoctypeTag()
        {
            var doctypeTag = new TagModel()
            {
                NameTag = NameTagEnum.doctype,
                TagStart = "<!DOCTYPE html>",
                TagFamily = TagFamilyEnum.NoEnd
            };
            return doctypeTag;
        }

        private static TagModel GetTagHtml()
        {
            var attributLang = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.lang.ToString(), Value = "en" };
            var attributDir = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.dir.ToString(), Value = "auto" };
            var attributXmlns = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.xmlns.ToString(), Value = "http://www.w3.org/1999/xhtml" };
            _headTag = GetHeadTag();
            var body = GetBodyTag();
            var scriptJs = GetScriptJs();
            var test = HtmlPageTableData.GetHtml(TagHtmlTable.htmlTable);
            var htmlTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributLang, attributDir, attributXmlns },
                NameTag = NameTagEnum.html,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlPageTableData.GetHtml(TagHtmlTable.htmlTable).Replace("<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">", string.Empty).Replace("</html>", string.Empty),
                TagStart = "<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">",
                TagEnd = "</html>",
                Children = new List<TagModel>() { _headTag, body, scriptJs }
            };
            return htmlTag;
        }

        private static TagModel GetHeadTag()
        {
            var metaCharsetTag = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() { new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.charset.ToString(), Value = "UTF-8" } },
                NameTag = NameTagEnum.meta,
                TagStart = HtmlPageTableData.GetHtml(TagHtmlTable.metaCharset)
            };
            var metaViewPort = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() {
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.name.ToString(), Value = "viewport" } ,
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.content.ToString(), Value = "width=device-width, initial-scale=1.0" }
                },
                NameTag = NameTagEnum.meta,
                TagStart = HtmlPageTableData.GetHtml(TagHtmlTable.metaViewPort)
            };
            var title = new TagModel()
            {
                TagFamily = TagFamilyEnum.WithEnd,
                NameTag = NameTagEnum.title,
                Content = "Document",
                TagStart = "<title>",
                TagEnd = "</title>"
            };
            var link = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() {
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.rel.ToString(), Value = "stylesheet" } ,
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.href.ToString(), Value = "main.css" },
                        new AttributeModel(){ FamilyAttribute = "data-preload", Value = "true"}
                },
                NameTag = NameTagEnum.link,
                TagStart = "<link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\">",
            };
            var headTag = new TagModel()
            {
                NameTag = NameTagEnum.head,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlPageTableData.GetHtml(TagHtmlTable.head).Replace("<head>", string.Empty).Replace("</head>", string.Empty),
                Children = new List<TagModel>() { metaCharsetTag, metaViewPort, title, link },
                TagStart = "<head>",
                TagEnd = "</head>"
            };
            return headTag;
        }

        private static TagModel GetBodyTag()
        {
            var nav = GetNav();
            var article = GetArticle();
            var main = GetMain();
            string content = string.Concat(HtmlPageTableData.GetHtml(TagHtmlTable.navTag), HtmlPageTableData.GetHtml(TagHtmlTable.articleTag), HtmlPageTableData.GetHtml(TagHtmlTable.mainTag));
            var bodyTag = new TagModel()
            {
                NameTag = NameTagEnum.body,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                Children = new List<TagModel>() { nav, article, main },
                TagStart = "<body>",
                TagEnd = "</body>"
            };
            return bodyTag;
        }

        private static TagModel GetNav()
        {
            var ul = GetUlMenu();
            var nav = new TagModel()
            {
                NameTag = NameTagEnum.nav,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlPageTableData.GetHtml(TagHtmlTable.ulMenu),
                Children = new List<TagModel>() { ul },
                TagStart = "<nav>",
                TagEnd = "</nav>"
            };
            return nav;
        }

        private static TagModel GetUlMenu()
        {
            string content = string.Concat(HtmlPageTableData.GetHtml(TagHtmlTable.liHome), HtmlPageTableData.GetHtml(TagHtmlTable.liNews),
                                HtmlPageTableData.GetHtml(TagHtmlTable.liOrganisation));
            var liHome = GetLiHome();
            var liNews = GetLiNews();
            var liOrganisation = GetLiOrganisation();
            var ulMenu = new TagModel()
            {
                NameTag = NameTagEnum.ul,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                Children = new List<TagModel>() { liHome, liNews, liOrganisation },
                TagStart = "<ul>",
                TagEnd = "</ul>"
            };
            return ulMenu;
        }

        private static TagModel GetLiHome()
        {
            var liHome = new TagModel()
            {
                NameTag = NameTagEnum.li,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Home",
                TagStart = "<li>",
                TagEnd = "</li>"
            };
            return liHome;
        }

        private static TagModel GetLiNews()
        {
            var liNews = new TagModel()
            {
                NameTag = NameTagEnum.li,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "News",
                TagStart = "<li>",
                TagEnd = "</li>"
            };
            return liNews;
        }

        private static TagModel GetLiOrganisation()
        {
            var liOrganisation = new TagModel()
            {
                NameTag = NameTagEnum.li,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "New organisation",
                TagStart = "<li>",
                TagEnd = "</li>"
            };
            return liOrganisation;
        }

        private static TagModel GetArticle()
        {
            var section = GetSection();
            var article = new TagModel()
            {
                NameTag = NameTagEnum.article,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlPageTableData.GetHtml(TagHtmlTable.sectionTag),
                Children = new List<TagModel>() { section },
                TagStart = "<article>",
                TagEnd = "</article>"
            };
            return article;
        }

        private static TagModel GetSection()
        {
            var h1 = GetH1();
            var p = GetP();
            string content = string.Concat(HtmlPageTableData.GetHtml(TagHtmlTable.h1News), HtmlPageTableData.GetHtml(TagHtmlTable.pNews));
            var article = new TagModel()
            {
                NameTag = NameTagEnum.section,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                Children = new List<TagModel>() { h1, p },
                TagStart = "<section>",
                TagEnd = "</section>"
            };
            return article;
        }

        private static TagModel GetH1()
        {
            var h1 = new TagModel()
            {
                NameTag = NameTagEnum.h1,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "News!!!",
                TagStart = "<h1>",
                TagEnd = "</h1>"
            };
            return h1;
        }

        private static TagModel GetP()
        {
            var p = new TagModel()
            {
                NameTag = NameTagEnum.p,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "The direction decide to present you the news roles in the organisation.",
                TagStart = "<p>",
                TagEnd = "</p>"
            };
            return p;
        }

        private static TagModel GetMain()
        {
            var table = GetTable();
            var main = new TagModel()
            {
                NameTag = NameTagEnum.main,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = HtmlPageTableData.GetHtml(TagHtmlTable.postTable),
                Children = new List<TagModel>() { table },
                TagStart = "<main>",
                TagEnd = "</main>"
            };
            return main;
        }

        private static TagModel GetTable()
        {
            var thead = GetThead();
            var tbody = GetTbody();
            var content = string.Concat(HtmlPageTableData.GetHtml(TagHtmlTable.thead), HtmlPageTableData.GetHtml(TagHtmlTable.tbody));
            var table = new TagModel()
            {
                NameTag = NameTagEnum.table,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                Children = new List<TagModel>() { thead, tbody },
                TagStart = "<table>",
                TagEnd = "</table>"
            };
            return table;
        }

        private static TagModel GetThead()
        {
            var trThead = GetTrThead();
            string content = string.Concat("<tr>", HtmlPageTableData.GetHtml(TagHtmlTable.titleTable), HtmlPageTableData.GetHtml(TagHtmlTable.descriptionTable),
            HtmlPageTableData.GetHtml(TagHtmlTable.typeTable), HtmlPageTableData.GetHtml(TagHtmlTable.levelTable), "</tr>");
            var thead = new TagModel()
            {
                NameTag = NameTagEnum.thead,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                Children = new List<TagModel>() { trThead },
                TagStart = "<thead>",
                TagEnd = "</thead>"
            };
            return thead;
        }

        private static TagModel GetTrThead()
        {
            var firstTh = GetFirstTh();
            var secondTh = GetSecondTh();
            var thirdTh = GetThirdTh();
            var fourthTh = GetFourth();
            string content = string.Concat(HtmlPageTableData.GetHtml(TagHtmlTable.titleTable), HtmlPageTableData.GetHtml(TagHtmlTable.descriptionTable),
            HtmlPageTableData.GetHtml(TagHtmlTable.typeTable), HtmlPageTableData.GetHtml(TagHtmlTable.levelTable));
            var trThead = new TagModel()
            {
                NameTag = NameTagEnum.tr,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                Children = new List<TagModel>() { firstTh, secondTh, thirdTh, fourthTh },
                TagStart = "<tr>",
                TagEnd = "</tr>"
            };
            return trThead;
        }

        private static TagModel GetFirstTh()
        {
            var th = new TagModel()
            {
                NameTag = NameTagEnum.th,
                Attributes = new List<AttributeModel>() { new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.scope.ToString(), Value = "col" } },
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Title",
                TagStart = "<th scope=\"col\">",
                TagEnd = "</th>"
            };
            return th;
        }

        private static TagModel GetSecondTh()
        {
            var th = new TagModel()
            {
                NameTag = NameTagEnum.th,
                Attributes = new List<AttributeModel>() { new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.scope.ToString(), Value = "col" } },
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Description",
                TagStart = "<th scope=\"col\">",
                TagEnd = "</th>"
            };
            return th;
        }

        private static TagModel GetThirdTh()
        {
            var th = new TagModel()
            {
                NameTag = NameTagEnum.th,
                Attributes = new List<AttributeModel>() { new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.scope.ToString(), Value = "col" } },
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Type",
                TagStart = "<th scope=\"col\">",
                TagEnd = "</th>"
            };
            return th;
        }

        private static TagModel GetFourth()
        {
            var th = new TagModel()
            {
                NameTag = NameTagEnum.th,
                Attributes = new List<AttributeModel>() { new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.scope.ToString(), Value = "col" } },
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Level",
                TagStart = "<th scope=\"col\">",
                TagEnd = "</th>"
            };
            return th;
        }

        //"Tbody":"<tbody>{DeveloperTable}{ProductOwnerTable}{TechnicalLeaderTable}{ManagerTable}{ArchitectTable}{DirectorTable}</tbody>",
        private static TagModel GetTbody()
        {
            var developerTable = GetDeveloperTable();
            var productOwnerTable = GetProductOwnerTable();
            var technicalLeaderTable = GetTechnicalLeaderTable();
            var managerTable = GetManagerTable();
            var architectTable = GetArchitectTable();
            var directorTable = GetDirectorTable();
            string content = string.Concat(HtmlPageTableData.GetHtml(TagHtmlTable.developerTable), HtmlPageTableData.GetHtml(TagHtmlTable.productOwnerTable),
                            HtmlPageTableData.GetHtml(TagHtmlTable.technicalLeaderTable), HtmlPageTableData.GetHtml(TagHtmlTable.managerTable),
                                HtmlPageTableData.GetHtml(TagHtmlTable.architectTable), HtmlPageTableData.GetHtml(TagHtmlTable.directorTable));
            var tbody = new TagModel()
            {
                NameTag = NameTagEnum.tbody,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                Children = new List<TagModel>() { developerTable, productOwnerTable, technicalLeaderTable,
                                managerTable, architectTable, directorTable },
                TagStart = "<tbody>",
                TagEnd = "</tbody>"
            };
            return tbody;
        }

        private static TagModel GetDeveloperTable()
        {
            var developerLabel = GetDeveloperLabel();
            var developerDescription = GetDeveloperDescription();
            var developerType = GetDeveloperType();
            var developerLevel = GetDeveloperLevel();
            string content = string.Concat(HtmlPageTableData.GetHtml(TagHtmlTable.developerLabel), HtmlPageTableData.GetHtml(TagHtmlTable.developerDescription),
                           HtmlPageTableData.GetHtml(TagHtmlTable.developerType), HtmlPageTableData.GetHtml(TagHtmlTable.developerLevel));
            var developerTable = new TagModel()
            {
                NameTag = NameTagEnum.tr,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                Children = new List<TagModel>() { developerLabel, developerDescription, developerType,
                                developerLevel},
                TagStart = "<tr>",
                TagEnd = "</tr>"
            };
            return developerTable;
        }
        private static TagModel GetDeveloperLabel()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Software Engineer",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }
        private static TagModel GetDeveloperDescription()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Make software from specifications",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetDeveloperType()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Technical",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetDeveloperLevel()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "1",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetProductOwnerTable()
        {
            var productownerLabel = GetProductOwnerLabel();
            var productownerDescription = GetProductOwnerDescription();
            var productownerType = GetProductOwnerType();
            var productownerLevel = GetProductOwnerLevel();
            string content = string.Concat(HtmlPageTableData.GetHtml(TagHtmlTable.productOwnerLabel), HtmlPageTableData.GetHtml(TagHtmlTable.productOwnerDescription),
                           HtmlPageTableData.GetHtml(TagHtmlTable.productOwnerType), HtmlPageTableData.GetHtml(TagHtmlTable.productOwnerLevel));
            var productownerTable = new TagModel()
            {
                NameTag = NameTagEnum.tr,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                Children = new List<TagModel>() { productownerLabel, productownerDescription, productownerType, productownerLevel },
                TagStart = "<tr>",
                TagEnd = "</tr>"
            };
            return productownerTable;
        }

        private static TagModel GetProductOwnerLabel()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Product Owner",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetProductOwnerDescription()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Create and ordered features from the wishes of the business",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetProductOwnerType()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Product",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetProductOwnerLevel()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "1",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetTechnicalLeaderTable()
        {
            var technicalLeaderLabel = GetTechnicalLeaderLabel();
            var technicalLeaderDescription = GetTechnicalLeaderDescription();
            var technicalLeaderType = GetTechnicalLeaderType();
            var technicalLeaderLevel = GetTechnicalLeaderLevel();
            string content = string.Concat(HtmlPageTableData.GetHtml(TagHtmlTable.technicalLeaderLabel), HtmlPageTableData.GetHtml(TagHtmlTable.technicalLeaderDescription),
                           HtmlPageTableData.GetHtml(TagHtmlTable.technicalLeaderType), HtmlPageTableData.GetHtml(TagHtmlTable.technicalLeaderLevel));
            var technicalLeaderTable = new TagModel()
            {
                NameTag = NameTagEnum.tr,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                Children = new List<TagModel>() { technicalLeaderLabel, technicalLeaderDescription, technicalLeaderType, technicalLeaderLevel },
                TagStart = "<tr>",
                TagEnd = "</tr>"
            };
            return technicalLeaderTable;
        }

        private static TagModel GetTechnicalLeaderLabel()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Technical Leader",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetTechnicalLeaderDescription()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Help developer to build software for the business in the a good way",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetTechnicalLeaderType()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Technical",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetTechnicalLeaderLevel()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "2",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetManagerTable()
        {
            var engineerManagerLabel = GetEngineerManagerLabel();
            var engineerManagerDescription = GetEngineerManagerrDescription();
            var engineerManagerType = GetEngineerManagerType();
            var engineerManagerLevel = GetEngineerManagerLevel();
            string content = string.Concat(HtmlPageTableData.GetHtml(TagHtmlTable.managerLabel), HtmlPageTableData.GetHtml(TagHtmlTable.managerDescription),
                           HtmlPageTableData.GetHtml(TagHtmlTable.managerType), HtmlPageTableData.GetHtml(TagHtmlTable.managerLevel));
            var engineerManagerTable = new TagModel()
            {
                NameTag = NameTagEnum.tr,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                Children = new List<TagModel>() { engineerManagerLabel, engineerManagerDescription, engineerManagerType, engineerManagerLevel },
                TagStart = "<tr>",
                TagEnd = "</tr>"
            };
            return engineerManagerTable;
        }

        private static TagModel GetEngineerManagerLabel()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Engineer Manager",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetEngineerManagerrDescription()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Manager of a team",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetEngineerManagerType()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Management",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetEngineerManagerLevel()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "2",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetArchitectTable()
        {
            var architecteLabel = GetArchitectLabel();
            var architecteDescription = GetArchitectDescription();
            var architecterType = GetArchitectType();
            var architecteLevel = GetArchitectLevel();
            string content = string.Concat(HtmlPageTableData.GetHtml(TagHtmlTable.architectLabel), HtmlPageTableData.GetHtml(TagHtmlTable.architectDescription),
                           HtmlPageTableData.GetHtml(TagHtmlTable.architectType), HtmlPageTableData.GetHtml(TagHtmlTable.architectLevel));
            var architecteTable = new TagModel()
            {
                NameTag = NameTagEnum.tr,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                Children = new List<TagModel>() { architecteLabel, architecteDescription, architecterType, architecteLevel },
                TagStart = "<tr>",
                TagEnd = "</tr>"
            };
            return architecteTable;
        }

        private static TagModel GetArchitectLabel()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Architect",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetArchitectDescription()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Responsible of the quality and the durability of the tech",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetArchitectType()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Technical",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetArchitectLevel()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "3",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetDirectorTable()
        {
            var directorLabel = GetDirectorLabel();
            var directorDescription = GetDirectorDescription();
            var directorType = GetDirectorType();
            var directorLevel = GetDirectorLevel();
            string content = string.Concat(HtmlPageTableData.GetHtml(TagHtmlTable.directorLabel), HtmlPageTableData.GetHtml(TagHtmlTable.directorDescription),
                           HtmlPageTableData.GetHtml(TagHtmlTable.directorType), HtmlPageTableData.GetHtml(TagHtmlTable.directorLevel));
            var directorTable = new TagModel()
            {
                NameTag = NameTagEnum.tr,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                Children = new List<TagModel>() { directorLabel, directorDescription, directorType, directorLevel },
                TagStart = "<tr>",
                TagEnd = "</tr>"
            };
            return directorTable;
        }



        private static TagModel GetDirectorLabel()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Director",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetDirectorDescription()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Manager of a departement",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetDirectorType()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "Management",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetDirectorLevel()
        {
            var td = new TagModel()
            {
                NameTag = NameTagEnum.td,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "3",
                TagStart = "<td>",
                TagEnd = "</td>"
            };
            return td;
        }

        private static TagModel GetScriptJs()
        {
            var scriptJs = new TagModel()
            {
                NameTag = NameTagEnum.script,
                TagFamily = TagFamilyEnum.WithEnd,
                TagStart = "<script src=\"javascript.js\">",
                TagEnd = "</script>"
            };
            return scriptJs;
        }
    }
}