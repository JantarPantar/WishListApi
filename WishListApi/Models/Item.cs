using System;
using System.Collections.Generic;

#nullable disable

namespace WishListApi.Models
{
    public partial class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Listid { get; set; }
        public string Color { get; set; }
        public byte[] Image { get; set; }
        public string Url { get; set; }

        public virtual Wishlist List { get; set; }
    }
}
