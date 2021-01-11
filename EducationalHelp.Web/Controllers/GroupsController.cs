using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalHelp.Core.Entities;
using EducationalHelp.Services.Groups;
using EducationalHelp.Services.Profile;
using EducationalHelp.Web.Controllers.Extensions;
using EducationalHelp.Web.Models.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalHelp.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly GroupService _groupsService;
        private readonly UserService _usesrsService;
        
        public GroupsController(GroupService groupsService, UserService usersService)
        {
            _groupsService = groupsService;
            _usesrsService = usersService;
        }

        [HttpGet("groups")]
        [Authorize]
        public IActionResult GetListOfGroups()
        {
            var groups = _groupsService.GetAllGroups().Select(g => new GroupsListItemOutputModel(g, _groupsService.GetGroupUsers(g.Id).Count()));

            return Ok(groups);
        }

        [HttpGet("groups/{groupId}")]
        [Authorize]
        public IActionResult GetGroupInfo(Guid groupId)
        {
            var group =_groupsService.GetGroupById(groupId);
            return Ok(group);
        }

        [HttpPost("groups")]
        [Authorize]
        public IActionResult CreateGroup(GroupAddModel model)
        {
            var user = _usesrsService.GetUserById(this.GetUserId());
            var group = new Group()
            {
                Title = model.Title,
                Description = model.Description,
            };

            _groupsService.CreateGroup(group, user);

            return Ok(group);
        }
    }
}
