using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab5.Models
{
    public class CollectionViewModel
    {
        public Person Person { get; set; }
        public Contact Contact { get; set; }

        public CollectionViewModel()
        {
            Person = new Person();
            Contact = new Contact();
        }
    }
}