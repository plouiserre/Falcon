using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FalconEngineTest.Utils
{
    public static class AssertUtils
    {

        //TODO delete this methods because html must be identical 
        public static string DeleteUselessSpace(string html)
        {
            return Regex.Replace(html, @"\s+", " ");
        }
    }
}