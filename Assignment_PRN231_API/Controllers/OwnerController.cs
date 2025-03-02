using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_PRN231_API.Controllers
{
    [Route("owner")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOwnerRepository _ownerRepository;
        public OwnerController(IOwnerRepository ownerRepository,IMapper mapper)
        {
            _mapper = mapper;
            _ownerRepository = ownerRepository;

        }
        [HttpGet("get-all-manager")]
        public async Task<IActionResult> GetAll() {
            var managers = await _ownerRepository.GetAllManager();
            return Ok(managers);
        }
        [HttpGet("get-manager/{id}")]
        public async Task<IActionResult> GetManager(Guid id)
        {
            var manager = await _ownerRepository.GetManager(id);
            if (manager == null)
            {
                return NotFound();
            }
            return Ok(manager);
        }
        [HttpPost("create-manager")]
        public async Task<IActionResult> CreateManager([FromForm] ManagerEditDto manager)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var newManager = await _ownerRepository.CreateUser(manager);
            if (newManager == null)
            {
                return BadRequest();
            }
            return Ok(newManager);
        }


    }
}
