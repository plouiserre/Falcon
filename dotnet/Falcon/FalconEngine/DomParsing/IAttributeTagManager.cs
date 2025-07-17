using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FalconEngine.Models;

namespace FalconEngine.DomParsing
{
    public interface IAttributeTagManager
    {
        List<FamilyAttributeEnum> GetAttributes(NameTagEnum key);
        void SetAttributes();
    }
}