using Microsoft.AspNetCore.Mvc;
using SystemAPI.Models;

namespace SystemAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private MyDbContext _db;

        public UserController(MyDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db)); 
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _db.Users.ToList();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Add(User user)
        {
            var users = _db.Users.Add(user);
            _db.SaveChanges();

            return Ok(users.Entity);
        }
    }
}
