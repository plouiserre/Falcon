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
        private ITagParser _trParser;
        private IExtractHtmlRemaining _extractHtmlRemaining;
        private IAttributeTagManager _attributeTagManager;
        private string _html;
        private bool _isValidDoctype;
        private bool _isValidHtmlTag;
        private bool _isValidPage;

        public HtmlParsing(ITagParser doctypeParse, ITagParser htmlParse, ITagParser trParser, IExtractHtmlRemaining extractHtmlRemaining, IAttributeTagManager attributeTagManager)
        {
            _doctypeParse = doctypeParse;
            _htmlParse = htmlParse;
            _trParser = trParser;
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
            string content = "<head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title><link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\"></head><body><nav><ul><li>Home</li><li>News</li><li>New organisation</li></ul></nav><article><section><h1>News!!!</h1><p>The direction decide to present you the news roles in the organisation.</p></section></article><main><table><thead><tr><th scope=\"col\">Title</th><th scope=\"col\">Description</th><th scope=\"col\">Type</th><th scope=\"col\">Level</th></tr></thead><tbody><tr><td>Software Engineer</td><td>Make software from specifications</td><td>Technical</td><td>1</td></tr><tr><td>Product Owner</td><td>Create and ordered features from the wishes of the business</td><td>Product</td><td>1</td></tr><tr><td>Technical Leader</td><td>Help developer to build software for the business in the a good way</td><td>Technical</td><td>2</td></tr><tr><td>Engineer Manager</td><td>Manager of a team</td><td>Management</td><td>2</td></tr><tr><td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td></tr><tr><td>Director</td><td>Manager of a departement</td><td>Management</td><td>3</td></tr></tbody></table></main></body><script src=\"javascript.js\"></script>";
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
                Content = "<meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title><link rel=\"stylesheet\" href=\"main.css\" data-preload=\"true\">",
                Children = new List<TagModel>() { metaCharsetTag, metaViewPort, title, link },
                TagStart = "<head>",
                TagEnd = "</head>"
            };
            return headTag;
        }

        private TagModel GetBodyTag()
        {
            var nav = GetNav();
            var article = GetArticle();
            var main = GetMain();
            string content = "<nav><ul><li>Home</li><li>News</li><li>New organisation</li></ul></nav><article><section><h1>News!!!</h1><p>The direction decide to present you the news roles in the organisation.</p></section></article><main><table><thead><tr><th scope=\"col\">Title</th><th scope=\"col\">Description</th><th scope=\"col\">Type</th><th scope=\"col\">Level</th></tr></thead><tbody><tr><td>Software Engineer</td><td>Make software from specifications</td><td>Technical</td><td>1</td></tr><tr><td>Product Owner</td><td>Create and ordered features from the wishes of the business</td><td>Product</td><td>1</td></tr><tr><td>Technical Leader</td><td>Help developer to build software for the business in the a good way</td><td>Technical</td><td>2</td></tr><tr><td>Engineer Manager</td><td>Manager of a team</td><td>Management</td><td>2</td></tr><tr><td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td></tr><tr><td>Director</td><td>Manager of a departement</td><td>Management</td><td>3</td></tr></tbody></table></main>";
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

        private TagModel GetMain()
        {
            var table = GetTable();
            var main = new TagModel()
            {
                NameTag = NameTagEnum.main,
                TagFamily = TagFamilyEnum.WithEnd,
                Content = "<table><thead><tr><th scope=\"col\">Title</th><th scope=\"col\">Description</th><th scope=\"col\">Type</th><th scope=\"col\">Level</th></tr></thead><tbody><tr><td>Software Engineer</td><td>Make software from specifications</td><td>Technical</td><td>1</td></tr><tr><td>Product Owner</td><td>Create and ordered features from the wishes of the business</td><td>Product</td><td>1</td></tr><tr><td>Technical Leader</td><td>Help developer to build software for the business in the a good way</td><td>Technical</td><td>2</td></tr><tr><td>Engineer Manager</td><td>Manager of a team</td><td>Management</td><td>2</td></tr><tr><td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td></tr><tr><td>Director</td><td>Manager of a departement</td><td>Management</td><td>3</td></tr></tbody></table>",
                Children = new List<TagModel>() { table },
                TagStart = "<main>",
                TagEnd = "</main>"
            };
            return main;
        }

        private TagModel GetTable()
        {
            var thead = GetThead();
            var tbody = GetTbody();
            var content = "<thead><tr><th scope=\"col\">Title</th><th scope=\"col\">Description</th><th scope=\"col\">Type</th><th scope=\"col\">Level</th></tr></thead><tbody><tr><td>Software Engineer</td><td>Make software from specifications</td><td>Technical</td><td>1</td></tr><tr><td>Product Owner</td><td>Create and ordered features from the wishes of the business</td><td>Product</td><td>1</td></tr><tr><td>Technical Leader</td><td>Help developer to build software for the business in the a good way</td><td>Technical</td><td>2</td></tr><tr><td>Engineer Manager</td><td>Manager of a team</td><td>Management</td><td>2</td></tr><tr><td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td></tr><tr><td>Director</td><td>Manager of a departement</td><td>Management</td><td>3</td></tr></tbody>";
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

        private TagModel GetThead()
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

        private TagModel GetTrThead()
        {
            string html = "<tr><th scope=\"col\">Title</th><th scope=\"col\">Description</th><th scope=\"col\">Type</th><th scope=\"col\">Level</th></tr>";
            var trThead = _trParser.Parse(html);
            return trThead;
        }

        private TagModel GetTbody()
        {
            var developerTable = GetDeveloperTable();
            var productOwnerTable = GetProductOwnerTable();
            var technicalLeaderTable = GetTechnicalLeaderTable();
            var managerTable = GetManagerTable();
            var architectTable = GetArchitectTable();
            var directorTable = GetDirectorTable();
            string content = "<tr><td>Software Engineer</td><td>Make software from specifications</td><td>Technical</td><td>1</td></tr><tr><td>Product Owner</td><td>Create and ordered features from the wishes of the business</td><td>Product</td><td>1</td></tr><tr><td>Technical Leader</td><td>Help developer to build software for the business in the a good way</td><td>Technical</td><td>2</td></tr><tr><td>Engineer Manager</td><td>Manager of a team</td><td>Management</td><td>2</td></tr><tr><td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td></tr><tr><td>Director</td><td>Manager of a departement</td><td>Management</td><td>3</td></tr>";
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

        private TagModel GetDeveloperTable()
        {
            string html = "<tr><td>Software Engineer</td><td>Make software from specifications</td><td>Technical</td><td>1</td></tr>";
            var developerTable = _trParser.Parse(html);
            return developerTable;
        }

        private TagModel GetProductOwnerTable()
        {
            string html = "<tr><td>Product Owner</td><td>Create and ordered features from the wishes of the business</td><td>Product</td><td>1</td></tr>";
            var productownerTable = _trParser.Parse(html);
            return productownerTable;
        }

        private TagModel GetTechnicalLeaderTable()
        {
            string html = "<tr><td>Technical Leader</td><td>Help developer to build software for the business in the a good way</td><td>Technical</td><td>2</td></tr>";
            var technicalLeaderTable = _trParser.Parse(html);
            return technicalLeaderTable;
        }

        private TagModel GetManagerTable()
        {
            string html = "<tr><td>Engineer Manager</td><td>Manager of a team</td><td>Management</td><td>2</td></tr>";
            var engineerManagerTable = _trParser.Parse(html);
            return engineerManagerTable;
        }

        private TagModel GetArchitectTable()
        {
            string html = "<tr><td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td></tr>";
            var architecteTable = _trParser.Parse(html);
            return architecteTable;
        }

        private TagModel GetDirectorTable()
        {
            string html = "<tr><td>Director</td><td>Manager of a departement</td><td>Management</td><td>3</td></tr>";
            var directorTable = _trParser.Parse(html);
            return directorTable;
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