﻿using api_VS.Data;
using Assignment_PRN231_API.DTOs.Owner;
using Assignment_PRN231_API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN231_API.Controllers
{
    [Route("owner")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ApplicationDBContext _context;
        public OwnerController(IOwnerRepository ownerRepository,IMapper mapper, ApplicationDBContext context)
        {
            _mapper = mapper;
            _ownerRepository = ownerRepository;
            _context = context;

        }

        [HttpGet("revenue-monthly")]
        public async Task<IActionResult> GetMonthlyRevenue()
        {
            var startDate = DateTime.UtcNow.AddMonths(-12).Date; // Lấy 12 tháng gần nhất
            var endDate = DateTime.UtcNow.Date;

            var revenueData = await _context.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month }) // Nhóm theo tháng
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalRevenue = g.Sum(o => o.TotalAmount)
                })
                .OrderBy(g => g.Year).ThenBy(g => g.Month)
                .ToListAsync();

            // Chuyển đổi dữ liệu thành format "YYYY-MM" để dễ hiển thị
            var labels = revenueData.Select(r => $"{r.Year}-{r.Month:D2}").ToArray();
            var data = revenueData.Select(r => r.TotalRevenue).ToArray();

            return Ok(new { labels, data });
        }
        [HttpGet("revenue-daily")]
        public async Task<IActionResult> GetDailyRevenue()
        {
            var startDate = DateTime.UtcNow.AddDays(-30).Date;  
            var endDate = DateTime.UtcNow.Date;  

            var revenueData = await _context.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .GroupBy(o => o.OrderDate.Date)  
                .Select(g => new
                {
                    Date = g.Key,
                    TotalRevenue = g.Sum(o => o.TotalAmount)
                })
                .OrderBy(g => g.Date)
                .ToListAsync();

      
            var labels = revenueData.Select(r => r.Date.ToString("yyyy-MM-dd")).ToArray();
            var data = revenueData.Select(r => r.TotalRevenue).ToArray();

            return Ok(new { labels, data });
        }
        [HttpGet("get-all-shop")]
        public async Task<IActionResult> GetAllShop()
        {
            var shops = await _ownerRepository.GetAllShop();

            if (shops == null)
            {
                return NotFound();
            }
            foreach (var shop in shops)
            {
                foreach (var staff in shop.Staffs)
                {
                    staff.Avatar = $"{Request.Scheme}://{Request.Host}/{staff.Avatar}";
                }
            }
            return Ok(shops);
        }
        [HttpGet("get-all-staff")]
        public async Task<IActionResult> GetAllStaff()
        {
            var staffs = await _ownerRepository.GetAllStaff();
            if (staffs == null)
            {
                return NotFound();
            }
            foreach (var staff in staffs)
            {
                staff.Avatar = $"{Request.Scheme}://{Request.Host}/{staff.Avatar}";

            }
            return Ok(staffs);
        }
        [HttpGet("get-all-manager")]
        public async Task<IActionResult> GetAll() {
            var managers = await _ownerRepository.GetAllManager();
            foreach (var manager in managers)
            {
                manager.Avatar =  $"{Request.Scheme}://{Request.Host}/{manager.Avatar}";

            }
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
            manager.AvatarUrl = $"{Request.Scheme}://{Request.Host}/{manager.AvatarUrl}";
            
            return Ok(manager);
        }
        [HttpPut("update-manager/{id:guid}")]
        public async Task<IActionResult> EditManager([FromRoute]Guid id, [FromForm] ManagerEditDto manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid data." });
            }

            var updatedManager = await _ownerRepository.UpdateUser(manager,id);

            if (updatedManager == null)
            {
                return BadRequest(new { Message = "Update failed." });
            }

            return Ok(new { Message = "Update manager successfully.", Email = updatedManager.Email });
        }

    }
}
