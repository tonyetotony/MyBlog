using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Data.Entities;

namespace MyBlog.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        public IActionResult Index() 
        { 
            var posts = _context.Posts.OrderByDescending(p => p.CreatedAt).ToList();
            return View(posts); 
        }

        [HttpGet]             
        public IActionResult Create() 
        { 
            return View(); 
        }

        // POST: Posts/Create
        [HttpPost] 
        public IActionResult Create(Post post) 
        { 
            if (ModelState.IsValid)
            { 
                post.CreatedAt = DateTime.Now;
                _context.Posts.Add(post); 
                _context.SaveChanges(); 
                return RedirectToAction(nameof(Index));
            } 
            return View(post); 
        }

        // GET: Posts/Delete/5
        // Удаление поста
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var post = _context.Posts.Find(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // Переадресация на страницу подтверждения удаления
        public IActionResult Delete(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }


    }
}
