using System;
using System.Collections.Generic;

#nullable disable

namespace WishListApi.Models
{
    public partial class Wishlist
    {
        public Wishlist()
        {
            Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
