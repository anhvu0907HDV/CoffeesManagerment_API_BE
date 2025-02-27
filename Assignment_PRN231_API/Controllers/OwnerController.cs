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

    }
}
