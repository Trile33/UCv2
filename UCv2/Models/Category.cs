using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comic.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Comic> Comics { get; set; }
    }
}