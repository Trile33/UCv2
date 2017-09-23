using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comic.Models
{
    public class Comic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int OwnerId{ get; set; }
        public int? LenderId{ get; set;}
        public int EpisodeNumber { get; set; }
        public string LoanedTo { get; set; }
        public virtual User Owner { get; set;}
        public virtual User Lender { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<FilePath> FilePaths { get; set; }

    }
}