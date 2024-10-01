using MicroServicesProject.Data;
using MicroServicesProject.Models;
using Microsoft.AspNetCore.Mvc;


namespace CloudProjectMicro.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileContext database;
        private readonly MessageService messageService;

        public ProfileController(ProfileContext context, MessageService messageservice)
        {
            this.database = context;
            this.messageService = messageservice;
        }

        [HttpGet]
        [Route("getprofileinfo")]
        public ActionResult<List<ProfileDto>> SendUserProfileInformation(string searchPhrase)
        {
            var users = database.Profiles
                .Where(u => u.Username.Contains(searchPhrase))
                .Select(u => new ProfileDto
                {
                    Username = u.Username,
                    Email = u.Email,
                    Address = u.Address
                })
                .ToList();

            if (users.Any())
            {
                messageService.SendLoggingActions($"Search for '{searchPhrase}' found {users.Count} users.");
                return Ok(users);
            }

            return BadRequest("No users found.");
        }
    }
}
