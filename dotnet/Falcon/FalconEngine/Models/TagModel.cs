using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconEngine.Models
{
    public class TagModel
    {
        public List<AttributeModel> Attributes { get; set; }
        public NameTagEnum NameTag { get; set; }
        public string Content { get; set; }
        public TagFamilyEnum TagFamily { get; set; }
    }
}