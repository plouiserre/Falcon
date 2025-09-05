using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing.IdentifyTagParsing
{
    public class IdentifyTagName : IIdentifyTagName
    {
        private string _startTag;

        public NameTagEnum FindTagName(string startTag)
        {
            _startTag = startTag;
            string simpleTag = GetSimpleTag();
            var nameTag = GetNameTagEnum(simpleTag);
            return nameTag;
        }

        private string GetSimpleTag()
        {
            string startTagWorking = _startTag.Replace("<", string.Empty).Replace("!", string.Empty).Replace(">", string.Empty);
            string tag = startTagWorking.Split(' ')[0];
            return tag;
        }

        //TODO try with parsing
        private NameTagEnum GetNameTagEnum(string simpleTag)
        {
            switch (simpleTag.ToLower())
            {
                case "doctype":
                    return NameTagEnum.doctype;
                case "html":
                    return NameTagEnum.html;
                case "head":
                    return NameTagEnum.head;
                case "meta":
                    return NameTagEnum.meta;
                case "link":
                    return NameTagEnum.link;
                case "title":
                    return NameTagEnum.title;
                case "body":
                    return NameTagEnum.body;
                case "form":
                    return NameTagEnum.form;
                case "div":
                    return NameTagEnum.div;
                case "table":
                    return NameTagEnum.table;
                case "thead":
                    return NameTagEnum.thead;
                case "tbody":
                    return NameTagEnum.tbody;
                case "tr":
                    return NameTagEnum.tr;
                case "th":
                    return NameTagEnum.th;
                case "td":
                    return NameTagEnum.td;
                case "p":
                    return NameTagEnum.p;
                case "h1":
                    return NameTagEnum.h1;
                case "span":
                    return NameTagEnum.span;
                case "input":
                    return NameTagEnum.input;
                case "label":
                    return NameTagEnum.label;
                case "option":
                    return NameTagEnum.option;
                case "select":
                    return NameTagEnum.select;
                default:
                    //throw new Exception($"unknown tag {simpleTag.ToLower()}");
                    return NameTagEnum.a;
            }
        }
    }
}