using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using GroupChat.Models;

namespace GroupChat.Controllers
{
    [Route("api/message")]
    public class MessageController : Controller
    {
        private readonly GroupChatContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MessageController(GroupChatContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("{groupId}")]
        public IActionResult GetByGroupId(int groupId)
        {
            return Ok(_context.Message.Where(m => m.GroupId == groupId).ToList());
        }

        [HttpPost]
        public IActionResult Create([FromBody] MessageViewModel message)
        {
            if (message == null)
                return BadRequest(ModelState);

            var newMessage = new Message { 
                Text = message.Text, 
                AddedBy = _userManager.GetUserName(User), 
                GroupId = message.GroupId };

            _context.Message.Add(newMessage);
            _context.SaveChanges();

            return Ok(newMessage);
        }
    }
}