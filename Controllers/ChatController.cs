using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using GroupChat.Models;

namespace GroupChat.Controllers
{
    [Authorize]
    public class ChatController: Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly GroupChatContext _context;
        public ChatController(
            UserManager<IdentityUser> userManager, 
            GroupChatContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var groups = _context.UserGroup
                .Where(ug => ug.UserName == _userManager.GetUserName(User))
                .Join(_context.Groups, ug => ug.GroupId, g => g.Id, (ug, g) => 
                    new UserGroupViewModel
                    {
                        UserName = ug.UserName,
                        GroupId = g.Id,
                        GroupName = g.GroupName
                    })
                .ToList();

            ViewData["UserGroups"] = groups;

            ViewData["Users"] = _userManager.Users;

            return View();
        }
    }
}