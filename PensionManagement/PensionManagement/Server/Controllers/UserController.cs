using PensionManagement.Server.Interfaces;
using PensionManagement.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace PensionManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _IUser;
        public UserController(IUser iUser)
        {
            _IUser = iUser;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                return await Task.FromResult(_IUser.GetUserDetails());
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occured : {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                User user = _IUser.GetUserData(id);
                if (user != null)
                {
                    return Ok(user);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occured :{ex.Message}");
            }

        }
        [HttpPost]
        public ActionResult Post(User user)
        {
            try
            {
                _IUser.AddUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occured: {ex.Message}");
            }

        }
        [HttpPut]
        public ActionResult Put(User user)
        {
            try
            {
                _IUser.UpdateUserDetails(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occured: {ex.Message}");
            }

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _IUser.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occured : {ex.Message}");
            }
        }
        [HttpGet("DownloadReport")]
        public async Task<IActionResult> DownloadReport()
        {
            try
            {
                var users = _IUser.GetUserDetails();
                var csv = new StringBuilder();
                csv.AppendLine("User Id,Username,Address,Mobile number,Email");
                foreach (var user in users)
                {
                    var line = string.Join(",", user.Userid, user.Username, user.Address, user.Mobilenumber, user.Email);
                    csv.AppendLine(line);
                }
                var bytes = Encoding.UTF8.GetBytes(csv.ToString());
                var memoryStream = new MemoryStream(bytes);
                memoryStream.Position = 0;

                return File(memoryStream, "text/csv", "users.csv");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}