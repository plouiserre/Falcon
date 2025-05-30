using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.Engine
{
    public interface IHtmlEngine
    {
        HtmlPage Calculate(string html);
    }
}