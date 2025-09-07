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
        private ITagParser _tableParser;
        private ITagParser _articleParser;
        private ITagParser _liParser;
        private IExtractHtmlRemaining _extractHtmlRemaining;
        private IAttributeTagManager _attributeTagManager;
        private string _html;
        private bool _isValidDoctype;
        private bool _isValidHtmlTag;
        private bool _isValidPage;

        public HtmlParsing(ITagParser doctypeParse, ITagParser htmlParse, ITagParser tableParser, ITagParser articleParser,
                            ITagParser liParser, IExtractHtmlRemaining extractHtmlRemaining, IAttributeTagManager attributeTagManager)
        {
            _doctypeParse = doctypeParse;
            _htmlParse = htmlParse;
            _tableParser = tableParser;
            _articleParser = articleParser;
            _liParser = liParser;
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

        private TagModel GetNav()
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

        private TagModel GetUlMenu()
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

        private TagModel GetLiHome()
        {
            string html = "<li>Home</li>";
            var liHome = _liParser.Parse(html);
            return liHome;
        }

        private TagModel GetLiNews()
        {
            string html = "<li>News</li>";
            var liNews = _liParser.Parse(html);
            return liNews;
        }

        private TagModel GetLiOrganisation()
        {
            string html = "<li>New organisation</li>";
            var liOrganisation = _liParser.Parse(html);
            return liOrganisation;
        }

        private TagModel GetArticle()
        {
            string html = "<article><section><h1>News!!!</h1><p>The direction decide to present you the news roles in the organisation.</p></section></article>";
            var article = _articleParser.Parse(html);
            return article;
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
            var html = "<table><thead><tr><th scope=\"col\">Title</th><th scope=\"col\">Description</th><th scope=\"col\">Type</th><th scope=\"col\">Level</th></tr></thead><tbody><tr><td>Software Engineer</td><td>Make software from specifications</td><td>Technical</td><td>1</td></tr><tr><td>Product Owner</td><td>Create and ordered features from the wishes of the business</td><td>Product</td><td>1</td></tr><tr><td>Technical Leader</td><td>Help developer to build software for the business in the a good way</td><td>Technical</td><td>2</td></tr><tr><td>Engineer Manager</td><td>Manager of a team</td><td>Management</td><td>2</td></tr><tr><td>Architect</td><td>Responsible of the quality and the durability of the tech</td><td>Technical</td><td>3</td></tr><tr><td>Director</td><td>Manager of a departement</td><td>Management</td><td>3</td></tr></tbody></table>";
            var table = _tableParser.Parse(html);
            return table;
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