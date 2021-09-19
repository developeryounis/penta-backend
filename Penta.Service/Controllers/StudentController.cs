using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Penta.Service.Services.Interface;
using Penta.Service.ViewModels;
using Penta.Service.ViewModels.Validation;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Penta.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public int UserId
        {
            get
            {
                return Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
            }
        }

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        [Route(PolicyConstants.Search)]
        [Authorize]
        public async Task<ActionResult<StudentPage>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (HasAccess(PolicyConstants.Search))
            {
                var result = await _studentService.GetAllByUser(UserId, page, pageSize);
                return Ok(result);
            }
            return Forbid();
        }


        [HttpGet]
        [Route(PolicyConstants.Search + "/{search}")]
        public async Task<ActionResult<StudentPage>> GetAll([FromRoute] string search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (HasAccess(PolicyConstants.Search))
            {
                var result = await _studentService.Search(UserId, search, page, pageSize);
                return Ok(result);
            }
            return Forbid();
        }

        [HttpPost]
        [Route(PolicyConstants.Add)]
        public async Task<ActionResult<StudentViewModel>> Add(StudentViewModel studentViewModel)
        {
            if (HasAccess(PolicyConstants.Add))
            {
                var validationResult = new StudentValidator().Validate(studentViewModel);
                if (validationResult.IsValid)
                {
                    var result = await _studentService.Add(studentViewModel);
                    return Ok(result);
                }
                else
                {
                    return Ok(validationResult.Errors);
                }

            }
            return Forbid();
        }


        [HttpPut]
        [Route(PolicyConstants.Update)]
        public async Task<IActionResult> Update(StudentViewModel studentViewModel)
        {
            if (HasAccess(PolicyConstants.Update))
            {
                var validationResult = new StudentValidator().Validate(studentViewModel);
                if (validationResult.IsValid)
                {
                    await _studentService.Update(studentViewModel);
                    return Ok();
                }
                else
                {
                    return Ok(validationResult.Errors);
                }

            }
            return Forbid();
        }

        [HttpDelete]
        [Route(PolicyConstants.Delete + "/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            if (HasAccess(PolicyConstants.Delete))
            {
                await _studentService.Delete(Id);
                return Ok();

            }
            return Forbid();
        }

        private bool HasAccess(string name)
        {
            return User.Claims.Any(x => x.Type == JwtRegisteredClaimNames.Iat && x.Value == name);
        }
    }
}
