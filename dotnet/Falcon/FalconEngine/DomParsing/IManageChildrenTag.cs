using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{
    public interface IManageChildrenTag
    {
        List<TagModel> Identify(string html);
        bool ValidateChildren();
    }
}