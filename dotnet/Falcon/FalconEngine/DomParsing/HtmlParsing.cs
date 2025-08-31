using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.Parser;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{
    public class HtmlParsing : IHtmlParsing
    {
        private ITagParser _doctypeParse;
        private ITagParser _htmlParse;
        private IExtractHtmlRemaining _extractHtmlRemaining;
        private IAttributeTagManager _attributeTagManager;
        private string _html;
        private bool _isValidDoctype;
        private bool _isValidHtmlTag;
        private bool _isValidPage;

        public HtmlParsing(ITagParser doctypeParse, ITagParser htmlParse, IExtractHtmlRemaining extractHtmlRemaining, IAttributeTagManager attributeTagManager)
        {
            _doctypeParse = doctypeParse;
            _htmlParse = htmlParse;
            _extractHtmlRemaining = extractHtmlRemaining;
            _attributeTagManager = attributeTagManager;
        }

        public HtmlPage Parse(string html, bool isThirdTest)
        {
            _html = html;
            _attributeTagManager.SetAttributes();
            var doctypeTag = GetDoctypeTag();
            if (!isThirdTest)
            {
                try
                {
                    RemoveUselessHtml(doctypeTag);

                    var htmlTag = GetTagHtml();
                    RemoveTagOpenCloed(htmlTag);
                    DeterminateIsValidPage();
                    var tags = new List<TagModel>() { doctypeTag, htmlTag };
                    var htmlPage = new HtmlPage() { Tags = tags, IsValid = _isValidPage };
                    return htmlPage;
                }
                catch (ParserNotFoundException ex)
                {
                    string message = string.Format($"{ex.NameTag} tag is unknown");
                    throw new UnknownTagException(message);
                }
                catch (AttributeTagParserException ex)
                {
                    string message = string.Format($"{ex.AttributeTagUnknown} attribute in {ex.StartTag} tag is unknown");
                    throw new UnknownAttributeException(message);
                }
                catch (StartTagBadFormattedException ex)
                {
                    string message = string.Format($"{ex.TagBadFormatting} is bad formatting");
                    throw new TagBadFormattingException(message);
                }
            }
            else
            {
                var htmlTag = GetTagHtmlStatic();
                var tags = new List<TagModel>() { doctypeTag, htmlTag };
                var htmlPage = new HtmlPage() { Tags = tags, IsValid = true };
                return htmlPage;
            }
        }

        private TagModel GetDoctypeTag()
        {
            var doctypeTag = _doctypeParse.Parse(_html);
            _isValidDoctype = _doctypeParse.IsValid();
            return doctypeTag;
        }

        private TagModel GetTagHtml()
        {
            var htmlTag = _htmlParse.Parse(_html);
            _isValidHtmlTag = _htmlParse.IsValid();
            return htmlTag;
        }

        private void RemoveUselessHtml(TagModel tag)
        {
            _html = _extractHtmlRemaining.Extract(tag, _html, ExtractionMode.ASide);
        }

        private void RemoveTagOpenCloed(TagModel tag)
        {
            _html = _extractHtmlRemaining.Extract(tag, _html, ExtractionMode.Inside);
        }

        private void DeterminateIsValidPage()
        {
            _isValidPage = _isValidDoctype && _isValidHtmlTag;
        }

        private TagModel GetTagHtmlStatic()
        {
            var attributLang = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.lang.ToString(), Value = "en" };
            var attributDir = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.dir.ToString(), Value = "auto" };
            var attributXmlns = new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.xmlns.ToString(), Value = "http://www.w3.org/1999/xhtml" };
            var headTag = GetHeadTag();
            var body = GetBodyTag();
            var scriptJs = GetScriptJs();
            string content = "<html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Breaking News</title><link rel=\"stylesheet\" href=\"main.css\"></head><body><nav><ul><li>Home</li><li>News</li><li>New organisation</li></ul></nav><article><section><h1>News!!!</h1><p>The direction decide to present you the news roles in the organisation.</p></section></article><table><thead><tr><th scope=\"col\">Title</th><th scope=\"col\">Description</th><th scope=\"col\">Type</th><th scope=\"col\">Level</th></tr></thead><tbody><tr><td>Software Engineer</td><td>Make software from specifications</td><td>Technical</td><td>1</td></tr><tr><td>Product Owner</td><td>Create and ordered features from the wishes of the business </td><td>Product</td><td>1</td></tr><tr><td>Technical Leader</td><td>Help developer to build software for the business in the a good way</td><td>Technical</td><td>2</td></tr><tr><td>Engineer Manager</td><td>Manager of a team</td><td>Management</td><td>2</td></tr><tr><td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td></tr><tr><td>Director</td><td>Manager of a departement</td><td>Management</td><td>3</td></tr></tbody></table></body><script src=\"javascript.js\"></script></html>";
            var htmlTag = new TagModel()
            {
                Attributes = new List<AttributeModel>() { attributLang, attributDir, attributXmlns },
                NameTag = NameTagEnum.html,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = content,
                TagStart = "<html lang=\"en\" dir=\"auto\" xmlns=\"http://www.w3.org/1999/xhtml\">",
                TagEnd = "</html>",
                Children = new List<TagModel>() { headTag, body, scriptJs }
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
                TagStart = "<meta charset=\"UTF-8\">"
            };
            var metaViewPort = new TagModel()
            {
                TagFamily = TagFamilyEnum.NoEnd,
                Attributes = new List<AttributeModel>() {
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.name.ToString(), Value = "viewport" } ,
                        new AttributeModel() { FamilyAttribute = FamilyAttributeEnum.content.ToString(), Value = "width=device-width, initial-scale=1.0" }
                },
                NameTag = NameTagEnum.meta,
                TagStart = "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">"
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
                Content = "<meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Breaking News</title><link rel=\"stylesheet\" href=\"main.css\">",
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
            string content = "<nav><ul><li>Home</li><li>News</li><li>New organisation</li></ul></nav><article><section><h1>News!!!</h1><p>The direction decide to present you the news roles in the organisation.</p></section></article><table><thead><tr><th scope=\"col\">Title</th><th scope=\"col\">Description</th><th scope=\"col\">Type</th><th scope=\"col\">Level</th></tr></thead><tbody><tr><td>Software Engineer</td><td>Make software from specifications</td><td>Technical</td><td>1</td></tr><tr><td>Product Owner</td><td>Create and ordered features from the wishes of the business </td><td>Product</td><td>1</td></tr><tr><td>Technical Leader</td><td>Help developer to build software for the business in the a good way</td><td>Technical</td><td>2</td></tr><tr><td>Engineer Manager</td><td>Manager of a team</td><td>Management</td><td>2</td></tr><tr><td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td></tr><tr><td>Director</td><td>Manager of a departement</td><td>Management</td><td>3</td></tr></tbody></table>";
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
                Content = "<ul><li>Home</li><li>News</li><li>New organisation</li></ul>",
                Children = new List<TagModel>() { ul },
                TagStart = "<nav>",
                TagEnd = "</nav>"
            };
            return nav;
        }

        private static TagModel GetUlMenu()
        {
            string content = "<li>Home</li><li>News</li><li>New organisation</li>";
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
                Content = "<section><h1>News!!!</h1><p>The direction decide to present you the news roles in the organisation.</p></section>",
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
            string content = "<h1>News!!!</h1><p>The direction decide to present you the news roles in the organisation.</p>";
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
                Content = "<table><thead><tr><th scope=\"col\">Title</th><th scope=\"col\">Description</th><th scope=\"col\">Type</th><th scope=\"col\">Level</th></tr></thead><tbody><tr><td>Software Engineer</td><td>Make software from specifications</td><td>Technical</td><td>1</td></tr><tr><td>Product Owner</td><td>Create and ordered features from the wishes of the business </td><td>Product</td><td>1</td></tr><tr><td>Technical Leader</td><td>Help developer to build software for the business in the a good way</td><td>Technical</td><td>2</td></tr><tr><td>Engineer Manager</td><td>Manager of a team</td><td>Management</td><td>2</td></tr><tr><td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td></tr><tr><td>Director</td><td>Manager of a departement</td><td>Management</td><td>3</td></tr></tbody></table>",
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
            var content = "<thead><tr><th scope=\"col\">Title</th><th scope=\"col\">Description</th><th scope=\"col\">Type</th><th scope=\"col\">Level</th></tr></thead><tbody><tr><td>Software Engineer</td><td>Make software from specifications</td><td>Technical</td><td>1</td></tr><tr><td>Product Owner</td><td>Create and ordered features from the wishes of the business </td><td>Product</td><td>1</td></tr><tr><td>Technical Leader</td><td>Help developer to build software for the business in the a good way</td><td>Technical</td><td>2</td></tr><tr><td>Engineer Manager</td><td>Manager of a team</td><td>Management</td><td>2</td></tr><tr><td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td></tr><tr><td>Director</td><td>Manager of a departement</td><td>Management</td><td>3</td></tr></tbody>";
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
            var thead = new TagModel()
            {
                NameTag = NameTagEnum.thead,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "<tr><th scope=\"col\">Title</th><th scope=\"col\">Description</th><th scope=\"col\">Type</th><th scope=\"col\">Level</th></tr>",
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
            string content = "<th scope=\"col\">Title</th><th scope=\"col\">Description</th><th scope=\"col\">Type</th><th scope=\"col\">Level</th>";
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
            string content = "<tr><td>Software Engineer</td><td>Make software from specifications</td><td>Technical</td><td>1</td></tr><tr><td>Product Owner</td><td>Create and ordered features from the wishes of the business </td><td>Product</td><td>1</td></tr><tr><td>Technical Leader</td><td>Help developer to build software for the business in the a good way</td><td>Technical</td><td>2</td></tr><tr><td>Engineer Manager</td><td>Manager of a team</td><td>Management</td><td>2</td></tr><tr><td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td></tr><tr><td>Director</td><td>Manager of a departement</td><td>Management</td><td>3</td></tr>";
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
            string content = "<td>Software Engineer</td><td>Make software from specifications</td><td>Technical</td><td>1</td>";
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
            string content = "<td>Product Owner</td><td>Create and ordered features from the wishes of the business </td><td>Product</td><td>1</td>";
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
                Content = "Create and ordered features from the wishes of the business ",
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
            string content = "<td>Technical Leader</td><td>Help developer to build software for the business in the a good way</td><td>Technical</td><td>2</td>";
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
            string content = "<td>Engineer Manager</td><td>Manager of a team</td><td>Management</td><td>2</td>";
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
            string content = "<td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td>";
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
            string content = "<td>Director</td><td>Manager of a departement</td><td>Management</td><td>3</td>";
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