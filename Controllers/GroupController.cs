using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using GroupChat.Models;
using System.Collections.Generic;
using System.Linq;

namespace GroupChat.Controllers
{
    [Authorize]
    [Route("api/group")]
    public class GroupController : Controller
    {
        private readonly GroupChatContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        
        public GroupController(GroupChatContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<UserGroupViewModel> GetAll()
        {
            var groups = _context.UserGroup
                            .Where(g => g.UserName == _userManager.GetUserName(User))
                            .Join(_context.Groups, ug => ug.GroupId, g => g.Id, (ug, g) =>
                                new UserGroupViewModel
                                {
                                    UserName = ug.UserName,
                                    GroupId = g.Id,
                                    GroupName = g.GroupName
                                })
                            .ToList();

            return groups;
        }

        [HttpGet("{name}", Name = "GetGroupByName")]
        public IActionResult GetByName(string name)
        {
            var group = _context.Groups
                            .Where(g => g.GroupName == name)
                            .Join(_context.UserGroup, g => g.Id, ug => ug.GroupId, (g, ug) =>
                                new UserGroupViewModel
                                {
                                    UserName = ug.UserName,
                                    GroupId = g.Id,
                                    GroupName = g.GroupName
                                })
                            .FirstOrDefault();

            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        [HttpPost()]
        public IActionResult Create([FromBody] NewGroupViewModel group)
        {
            if (group == null || group.GroupName == "")
            {
                return BadRequest();
            }

            if (_context.Groups.Any(g => g.GroupName == group.GroupName))
            {
                ModelState.AddModelError("GroupName", "Group already exists");
                return BadRequest(ModelState);
            }

            var newGroup = new Group { GroupName = group.GroupName };

            // insert new group to DB
            _context.Groups.Add(newGroup);
            _context.SaveChanges();

            // insert group-user mapping in UserGroup table
            foreach (var user in group.UserNames)
            {
                _context.UserGroup.Add(new UserGroup { UserName = user, GroupId = newGroup.Id });
            }
            _context.SaveChanges();

            return Created("GetGroupByName", new { name = group.GroupName });
        }
    }
}