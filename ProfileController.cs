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

        public ProfileController(ProfileContext context)
        {
            this.database = context;
        }

        [HttpGet]
        [Route("getprofileinfo")]
        public ActionResult<List<ProfileDto>> SendUserProfileInformation(string searchPhrase)
        {
            var users = database.Profiles
                .Where(u => u.UserName.Contains(searchPhrase))
                .Select(u => new ProfileDto
                {
                    Username = u.UserName,
                    Email = u.Email,
                    Address = u.Address
                })
                .ToList();

            if (users.Any())
            {
                return Ok(users);
            }

            return BadRequest("No users found.");
        }
    }
}
