using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Penta.Service.Services;
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
        private readonly StudentService _studentService;

        public int UserId
        {
            get
            {
                return Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId).Value);
            }
        }

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        [Route(PolicyConstants.Search)]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
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
        public async Task<IActionResult> GetAll([FromRoute] string search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
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
        public async Task<IActionResult> Add(StudentViewModel studentViewModel)
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
            var result = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Iat);

            if (result != null)
            {
                return result.Value.Split(",").Contains(name);
            }

            return false;
        }
    }
}
