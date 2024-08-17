using Microsoft.AspNetCore.Mvc;
using AIM.Dtos.EntityDtos;
using AIM.Models.Entities;
using AutoMapper;

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
      public async Task<IActionResult> GetAllLands()
      {
         var lands = await _unitOfWork.Lands.GetAllAsync();
         return Ok(lands);
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