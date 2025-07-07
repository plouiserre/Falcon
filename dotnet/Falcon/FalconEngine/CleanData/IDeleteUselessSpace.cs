using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.CleanData
{
    public interface IDeleteUselessSpace
    {
        string CleanContent(string text);
        string RemoveSpecialCaracter(string html);
        string RemoveUselessSpace(string content);
        string CleanTagStart(string html);
    }
}