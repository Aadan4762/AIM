using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using AIM.Dtos.EntityDtos;
using AIM.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIM.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class LandController : ControllerBase
   {
      private readonly IUnitOfWork _unitOfWork;
      public LandController(IUnitOfWork unitOfWork)
      {
         _unitOfWork = unitOfWork;
      }

      
      [HttpPost]
      public async Task<IActionResult> CreateLand(LandDto landDto)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);

         }

         var land = new Land
         {
            name = landDto.name,
            location = landDto.location,
            county = landDto.county,
            subCounty = landDto.subCounty,
            titleDeed = landDto.titleDeed,
            
         };
         await _unitOfWork.Lands.AddAsync(land);
         await _unitOfWork.CompleteAsync();

         return CreatedAtAction(nameof(GetAllLands), new { id = land.id }, land);
      }




      [HttpGet]
      public async Task<IActionResult> GetAllLands(
         [FromQuery] int pageNumber = 1,
         [FromQuery] int pageSize = 10,
         [FromQuery] string sortBy = "id",
         [FromQuery] string sortOrder = "asc",
         [FromQuery] string type = null,
         [FromQuery] string status = null
         )
      {
         var query = _unitOfWork.Lands.Query();

         // Apply filters
         if (!string.IsNullOrEmpty(type))
         {
            query = query.Where(f => f.name.Contains(type, StringComparison.OrdinalIgnoreCase));
         }

         if (!string.IsNullOrEmpty(status))
         {
            query = query.Where(f => f.titleDeed.Contains(status, StringComparison.OrdinalIgnoreCase));
         }

         // Apply sorting
         query = sortOrder.ToLower() == "desc"
            ? query.OrderByDescending(GetSortExpression(sortBy))
            : query.OrderBy(GetSortExpression(sortBy));

         // Apply pagination
         var totalCount = await query.CountAsync();
         var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

         // Prepare response
         var response = new
         {
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Items = items
         };

         return Ok(response);
      }
      
      private static Expression<Func<Land, object>> GetSortExpression(string sortBy)
      {
         return sortBy.ToLower() switch
         {
            "name" => f => f.name,
            "county" => f => f.county,
            "subCounty" => f => f.subCounty,
            "location" => f => f.location,
            "titleDeed" => f => f.titleDeed,
            _ => f => f.id
         };
      }
      
      
      
      

      [HttpGet("{id}")]

      public async Task<IActionResult> GetLandById(int id)
      {
         var land  = await _unitOfWork.Lands.GetByIdAsync(id);
         if (land == null)
         {
            return NotFound();
         }

         return Ok(land);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> UpdateLandById(int id, LandDto landDto)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         var existingLand  = await _unitOfWork.Lands.GetByIdAsync(id);
         if (existingLand == null)
         {
            return NotFound();
         }

         // Map Dto to Entity
         existingLand.name = landDto.name;
         existingLand.county = landDto.county;
         existingLand.subCounty = landDto.subCounty;
         existingLand.titleDeed = landDto.titleDeed;
         existingLand.location = landDto.location;

         _unitOfWork.Lands.Update(existingLand);
         await _unitOfWork.CompleteAsync();
         return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteById(int id)
      {
         var land = await _unitOfWork.Lands.GetByIdAsync(id);
         if (land == null)
         {
            return NotFound();

         }

         _unitOfWork.Lands.Remove(land);
         await _unitOfWork.CompleteAsync();
         return NoContent();
      }
   }
}