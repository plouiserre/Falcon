using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.DomParsing;
using FalconEngine.Models;

namespace FalconEngine.Engine
{
    public class HtmlEngine : IHtmlEngine
    {
        private IHtmlParsing _htmlParsing;

        public HtmlEngine(IHtmlParsing htmlParsing)
        {
            _htmlParsing = htmlParsing;
        }

        //TODO update this method because the argument is useless or the method is calling inside!!!
        public HtmlPage Calculate(string html)
        {
            return _htmlParsing.Parse(html);
        }
    }
}