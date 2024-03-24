using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Products.Contracts.Domain
{
    public class Category
    {
        protected Category()
        {
        }

        public static Category Create(string name, string description)
        {
            return new Category()
            {
                Name = name,
                Description = description
            };
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
