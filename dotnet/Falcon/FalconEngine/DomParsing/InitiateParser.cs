using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.CleanData;
using FalconEngine.DomParsing.IdentifyTagParsing;
using FalconEngine.DomParsing.Parser;
using FalconEngine.DomParsing.Parser.Attribute;

namespace FalconEngine.DomParsing
{
    public class InitiateParser
    {
        private string _html;
        private string _startTag;
        private IDeleteUselessSpace _deleteUselessSpace;
        private IIdentifyTag _identifyTag;
        private IIdentifyStartTagEndTag _identifyStartTagEndTag;
        private IAttributeTagParser _attributeTagParser;
        private IDeterminateContent _determinateContent;
        private IDeterminateChildren _determinateChildren;
        private IExtractHtmlRemaining _extractHtmlRemaining;
        private IAttributeTagManager _attributeTagManager;

        public InitiateParser(IDeleteUselessSpace deleteUselessSpace, IIdentifyTag identifyTag,
            IIdentifyStartTagEndTag identifyStartTagEndTag, IAttributeTagParser attributeTagParser,
            IDeterminateContent determinateContent, IDeterminateChildren determinateChildren,
            IExtractHtmlRemaining extractHtmlRemaining, IAttributeTagManager attributeTagManager)
        {
            _deleteUselessSpace = deleteUselessSpace;
            _identifyTag = identifyTag;
            _identifyStartTagEndTag = identifyStartTagEndTag;
            _attributeTagParser = attributeTagParser;
            _determinateContent = determinateContent;
            _determinateChildren = determinateChildren;
            _extractHtmlRemaining = extractHtmlRemaining;
            _attributeTagManager = attributeTagManager;
        }

        public List<ITagParser> GetTagParsers(string html)
        {
            _html = html;
            var parsers = new List<ITagParser>();
            while (_html.Length > 0)
            {
                var parser = GetTagParser();
                parsers.Add(parser);
            }
            return parsers;
        }

        private ITagParser GetTagParser()
        {
            string htmlCleaned = _deleteUselessSpace.PurgeUselessCaractersAroundTag(_html);
            _identifyStartTagEndTag.DetermineStartEndTags(htmlCleaned);
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
                case string tag when tag.ToLower().Contains("html"):
                    return new HtmlTagParser(_identifyTag, _determinateContent, _attributeTagManager);
                case string tag when tag.ToLower().Contains("head"):
                    return new HeadParser(_deleteUselessSpace, _identifyTag, _determinateChildren);
                case string tag when tag.ToLower().Contains("meta"):
                    return new MetaParser(_identifyTag, _attributeTagParser, _attributeTagManager);
                case string tag when tag.ToLower().Contains("link"):
                    return new LinkParser(_identifyTag, _attributeTagManager);
                case string tag when tag.ToLower().Contains("title"):
                    return new TitleParser(_identifyTag, _determinateContent);
                default:
                    return null;
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