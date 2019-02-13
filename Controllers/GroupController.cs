using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using GroupChat.Models;

namespace GroupChat.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        public GroupController(GroupChatContext context, UserManager<IdentityUser> userManager)
        {
            
        }
    }
}