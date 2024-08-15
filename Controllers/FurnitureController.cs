using System.Linq.Expressions;
using AIM.Dtos.EntityDtos;
using AIM.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAllFurnitures(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "id",
            [FromQuery] string sortOrder = "asc",
            [FromQuery] string type = null,
            [FromQuery] string status = null)
        {
            var query = _unitOfWork.Furnitures.Query();

            // Apply filters
            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(f => f.type.Contains(type, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(f => f.status.Contains(status, StringComparison.OrdinalIgnoreCase));
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

        private static Expression<Func<Furniture, object>> GetSortExpression(string sortBy)
        {
            return sortBy.ToLower() switch
            {
                "type" => f => f.type,
                "tag" => f => f.tag,
                "material" => f => f.material,
                "color" => f => f.color,
                "dimension" => f => f.dimension,
                "cost" => f => f.cost,
                "status" => f => f.status,
                "upload_image" => f => f.upload_image,
                "date_recorded" => f => f.date_recorded,
                _ => f => f.id
            };
        }
    }
}