using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comic.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Role { get; set; }
        public string Username  { get; set; }
        public string Password { get; set; }
        public virtual List<Comic> OwnedComics { get; set; }
        public virtual List<Comic> LoanedComics { get; set; }
    }
}