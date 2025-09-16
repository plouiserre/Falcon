using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.CustomException;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.DomParsing.Parser.Attribute;
using FalconEngine.DomParsing.Parser.List;
using FalconEngine.DomParsing.Parser.Structure;
using FalconEngine.DomParsing.Parser.Table;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{
    public class InitiateParser
    {
        private string _html;
        private string _startTag;
        private IDeleteUselessSpace _deleteUselessSpace;
        private IIdentifyTag _identifyTag;
        private IIdentifyStartTagEndTag _identifyStartTagEndTag;
        private ILocateLimitTag _locateLimitTag;
        private IDeterminateContent _determinateContent;
        private IManageChildrenTag _manageChildrenTag;
        private IAttributeTagManager _attributeTagManager;        

        public InitiateParser(IDeleteUselessSpace deleteUselessSpace, IIdentifyTag identifyTag, IIdentifyStartTagEndTag identifyStartTagEndTag, 
            ILocateLimitTag locateLimitTag, IDeterminateContent determinateContent, IManageChildrenTag manageChildrenTag, IAttributeTagManager attributeTagManager)
        {
            _deleteUselessSpace = deleteUselessSpace;
            _identifyTag = identifyTag;
            _identifyStartTagEndTag = identifyStartTagEndTag;
            _locateLimitTag = locateLimitTag;
            _determinateContent = determinateContent;
            _manageChildrenTag = manageChildrenTag;
            _attributeTagManager = attributeTagManager;
        }

        public List<ITagParser> GetTagParsers(string html)
        {
            _html = html;
            _html = RemoveUselessHtml();
            var parsers = new List<ITagParser>();
            while (_html.Length > 0)
            {
                var parser = GetTagParser();
                parsers.Add(parser);
            }
            return parsers;
        }

        private string RemoveUselessHtml()
        {
            string htmlCleaned = string.Empty;
            bool isBeginTag = false;
            for (int i = 0; i < _html.Length; i++)
            {
                char caracter = _html[i];
                if (caracter == '<')
                    isBeginTag = true;
                if (isBeginTag)
                    htmlCleaned += caracter;
            }
            return htmlCleaned;
        }

        private ITagParser GetTagParser()
        {
            _identifyStartTagEndTag.DetermineStartEndTags(_html);
            _startTag = _identifyStartTagEndTag.StartTag;
            var endTag = _identifyStartTagEndTag.EndTag;
            RemoveUselessHtml(_startTag, endTag);
            return ChooseGoodTagParser();
        }

        private ITagParser ChooseGoodTagParser()
        {
            switch (_startTag)
            {
                case string tag when tag.ToLower().Contains("doctype"):
                    return new DoctypeParser(_identifyTag);
                case string tag when tag.ToLower().Contains("<html"):
                    return new HtmlTagParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<head"):
                    return new HeadParser(_deleteUselessSpace, _identifyTag, _manageChildrenTag);
                case string tag when tag.ToLower().Contains("<meta"):
                    return new MetaParser(_identifyTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<link"):
                    return new LinkParser(_identifyTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<title"):
                    return new TitleParser(_identifyTag, _determinateContent);
                case string tag when tag.ToLower().Contains("<option"):
                    return new OptionParser(_identifyTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<select"):
                    return new SelectParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<h1"):
                    return new H1Parser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<label"):
                    return new LabelParser(_identifyTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<input"):
                    return new InputParser(_identifyTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<span"):
                    return new SpanParser(_identifyTag, _attributeTagManager, _manageChildrenTag);
                case string tag when tag.ToLower().Contains("<li"):
                    return new LiParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<ul"):
                    return new UlParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<p"):
                    return new PParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<td"):
                    return new TdParser(_identifyTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<th "):
                    return new ThParser(_identifyTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<tr"):
                    return new TrParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<thead"):
                    return new TheadParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<tbody"):
                    return new TBodyParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<table"):
                    return new TableParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<div"):
                    return new DivParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<section"):
                    return new SectionParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<article"):
                    return new ArticleParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<nav"):
                    return new NavParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<main"):
                    return new MainParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<form"):
                    return new FormParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<body"):
                    return new BodyParser(_identifyTag, _manageChildrenTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("<a"):
                    return new AParser(_identifyTag, _attributeTagManager, _deleteUselessSpace);
                case string tag when tag.ToLower().Contains("<script"):
                    return new ScriptParser(_identifyTag, _attributeTagManager);
                default:
                    string message = string.Format($"We cannot find a parser for {_startTag} Tag");
                    throw new ParserNotFoundException(_startTag, ErrorTypeParsing.parserNotFoundException, message);
            }
        }

        private void RemoveUselessHtml(string startTag, string endTag)
        {
            if (string.IsNullOrEmpty(endTag))
                _html = _html.Replace(startTag, string.Empty);
            else
            {
                var startTagIndex = _html.IndexOf(startTag);
                var endTagIndex = _html.IndexOf(endTag);
                string htmlToRemove = _html.Substring(startTagIndex, endTagIndex - startTagIndex + endTag.Length);
                _html = _html.Replace(htmlToRemove, string.Empty);
            }
            _html = CleanHtml();
        }

        private string CleanHtml()
        {
            int goodStartHtml = LocateFirstCaracter();
            return _html.Substring(goodStartHtml, _html.Length - goodStartHtml);
        }

        private int LocateFirstCaracter()
        {
            int Localisation = 0;
            bool IsOpenBracketPresent = false;
            for (int i = 0; i < _html.Length; i++)
            {
                char caracter = _html[i];
                if (caracter != ' ')
                {
                    Localisation = i;
                    IsOpenBracketPresent = true;
                    break;
                }
            }
            if (!IsOpenBracketPresent)
                Localisation = _html.Length;
            return Localisation;
        }
    }
}