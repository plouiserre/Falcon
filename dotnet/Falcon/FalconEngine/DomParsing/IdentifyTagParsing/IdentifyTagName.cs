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
                case "div":
                    return NameTagEnum.div;
                case "meta":
                    return NameTagEnum.meta;
                case "span":
                    return NameTagEnum.span;
                case "body":
                    return NameTagEnum.body;
                case "p":
                    return NameTagEnum.p;
                case "link":
                    return NameTagEnum.link;
                case "title":
                    return NameTagEnum.title;
                default:
                    return NameTagEnum.a;
            }
        }
    }
}