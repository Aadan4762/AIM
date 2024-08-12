using AIM.Dtos.EntityDtos;
using AIM.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurnitureController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FurnitureController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFurnitures()
        {
            var furnitures = await _unitOfWork.Furnitures.GetAllAsync();
            return Ok(furnitures);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFurniture(int id)
        {
            var furniture = await _unitOfWork.Furnitures.GetByIdAsync(id);
            if (furniture == null)
            {
                return NotFound();
            }
            return Ok(furniture);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFurniture(FurnitureDto furnitureDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map FurnitureDto to Furniture entity
            var furniture = new Furniture
            {
                type = furnitureDto.type,
                tag = furnitureDto.tag,
                material = furnitureDto.material,
                color = furnitureDto.color,
                dimension = furnitureDto.dimension,
                cost = furnitureDto.cost,
                status = furnitureDto.status,
                upload_image = furnitureDto.upload_image,
                date_recorded = furnitureDto.date_recorded
            };

            await _unitOfWork.Furnitures.AddAsync(furniture);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetFurniture), new { id = furniture.id }, furniture);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFurniture(int id, FurnitureDto furnitureDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingFurniture = await _unitOfWork.Furnitures.GetByIdAsync(id);
            if (existingFurniture == null)
            {
                return NotFound();
            }

            // Map FurnitureDto to existing Furniture entity
            existingFurniture.type = furnitureDto.type;
            existingFurniture.tag = furnitureDto.tag;
            existingFurniture.material = furnitureDto.material;
            existingFurniture.color = furnitureDto.color;
            existingFurniture.dimension = furnitureDto.dimension;
            existingFurniture.cost = furnitureDto.cost;
            existingFurniture.status = furnitureDto.status;
            existingFurniture.upload_image = furnitureDto.upload_image;
            existingFurniture.date_recorded = furnitureDto.date_recorded;

            _unitOfWork.Furnitures.Update(existingFurniture);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFurniture(int id)
        {
            var furniture = await _unitOfWork.Furnitures.GetByIdAsync(id);
            if (furniture == null)
            {
                return NotFound();
            }

            _unitOfWork.Furnitures.Remove(furniture);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
