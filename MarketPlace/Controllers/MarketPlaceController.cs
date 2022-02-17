using MarketPlace.Helper;
using MarketPlace.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MarketPlace.Controllers
{
    public class MarketPlaceController : Controller
    {
        BookApiConnection _api = new BookApiConnection();
       
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Book> book = new List<Book>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Book");
            if(res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<List<Book>>(result);
            }
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var book = new Book();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Book/{id}" );
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<Book>(result);
            }
            return View(book);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            HttpClient client = _api.Initial();
            var postBook = client.PostAsJsonAsync<Book>("api/book", book);
            var result = postBook.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/Book/{id}");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = new Book();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Book/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<Book>(result);
            }
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            HttpClient client = _api.Initial();
            var responce = await client.PutAsJsonAsync<Book>($"api/Book/{book.Id}", book);
           
            if (responce.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
         }
    }
}
