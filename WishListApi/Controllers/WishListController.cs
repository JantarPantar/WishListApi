using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WishListApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WishListApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class WishListController : Controller
    {

        public static string RandomString()
        {
            Context db = new();
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string url;
            url = new string(Enumerable.Repeat(chars, 30).Select(s => s[random.Next(s.Length)]).ToArray());

            return (url);

        }

        // GET: api/<WishListController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { RandomString(), RandomString() };
        }

        // GET api/<WishListController>/5
        [HttpGet("wishlist/{url}")]
        public IActionResult Get(string url)
        {
            Context db = new Context();
            return Json(db.Wishlists.Where(x => x.Url == url).FirstOrDefault());
        }

        [HttpGet("items/{url}")]
        public IActionResult Getaa(string url)
        {
            Context db = new Context();
            return Json(db.Items.Where(x => x.Listid == db.Wishlists.Where(x=>x.Url == url).FirstOrDefault().Id).ToList());
        }


        [HttpPost("createwishlist")]
        public IActionResult Createwl([FromHeader] string name, [FromHeader] string description, [FromHeader] string color)
        {
            Context db = new Context();
            var returnos = new Wishlist()
            {
                Name = name,
                Description = description,
                Color = color,
                Created = DateTime.Now,
                Url = RandomString()
            };
            db.Wishlists.Add(returnos);
            db.SaveChanges();
            return Json(new JsonFormater() { _id = returnos.Url, _color = returnos.Color,_desc = returnos.Description, _name = returnos.Name});
        }

        [HttpPost("wishlist/{url}/createitem")]
        public IActionResult CreateItem([FromHeader] string name, string url, [FromHeader] string color, [FromHeader] byte[] image , [FromHeader] string hrefurl)
        {
            Context db = new Context();

            if (db.Wishlists.Where(x => x.Url == url).FirstOrDefault() != null)
            {
                var returnos = new Item()
                {
                    Name = name,
                    Color = color,
                    Url = hrefurl,
                    Image = image,
                    Listid = db.Wishlists.Where(x => x.Url == url).FirstOrDefault().Id
                };
                db.Items.Add(returnos);
                db.SaveChanges();
            }
            return Json("wishlistneexistuje");

        }




    }
    class JsonFormater
    {
        public string _name { get; set; }
        public string _desc { get; set; }
        public string _id { get; set; }
        public string _color { get; set; }
    }


}
