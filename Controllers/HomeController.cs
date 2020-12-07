using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Exam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Exam.Controllers 
{
    public class HomeController : Controller
    {
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost("Register")]
        public IActionResult Register(User newUser)
        {
            if(ModelState.IsValid)
            {
                if(_context.Users.Any(e => e.UserName == newUser.UserName))
                {
                    ModelState.AddModelError("UserName", "UserName is already taken");
                    return View("Index");
                } else {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    Console.WriteLine(newUser.Password);
                    _context.Add(newUser);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("loggedIn", newUser.UserId);
                    return RedirectToAction("Dashboard");
                }
                
            } else {
                return View("Index");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(LUser Login)
        {
            if(ModelState.IsValid)
            {
                User userInDb = _context.Users.FirstOrDefault(u => u.UserName == Login.LUserName);
                if(userInDb == null)
                {
                    ModelState.AddModelError("LUserName", "Invaild login attempt");
                    return View("Index");
                } else {
                    var hasher = new PasswordHasher<LUser>();
                    var result = hasher.VerifyHashedPassword(Login, userInDb.Password, Login.LPassword);
                    if(result == 0)
                    {
                        ModelState.AddModelError("LUserName", "Invalid login attempt");
                        return View("Index");
                    }
                    HttpContext.Session.SetInt32("loggedIn", userInDb.UserId);
                    return RedirectToAction("Dashboard");
                }                
            } else {
                return View("Index");
            }
        }

        [HttpGet("Dashboard")]
        public IActionResult Dashboard(int FunId)
        {
            ViewBag.AllFun = _context.TheFuns.Include(y =>y.HobbyBy).Include(q => q.Owner).ToList();
            ViewBag.Hobbies = _context.Hobbies.Include(y =>y.User);             
            return View();
        }

        [HttpGet("New")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create(TheFun FunStuff)
        {
            if(ModelState.IsValid)
            
            {
                int? loggedIn = HttpContext.Session.GetInt32("loggedIn");
                FunStuff.UserId = (int)loggedIn;
                _context.Add(FunStuff);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");                
            } else {
                return View("New");
            }
        }
            
            [HttpGet("hobby/{funId}")]
            public IActionResult HobbyInfo(int funId)
        {
            if (HttpContext.Session.GetInt32("loggedIn") != null )
            {
                int? loggedIn = HttpContext.Session.GetInt32("loggedIn");
                ViewBag.OneUser = _context.Users.SingleOrDefault(b => b.UserId == loggedIn);
                ViewBag.FunTime = _context.TheFuns.Include(x => x.HobbyBy).ThenInclude(z =>z.User).SingleOrDefault(a => a.FunId == funId);
                ViewBag.Hobbies = _context.Hobbies.Include(y =>y.User);

            }
            return View();                    
        }

        [HttpPost("AddHobby")]
        public IActionResult AddHobby(Hobby theHobby)
        {
            _context.Add(theHobby);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}
