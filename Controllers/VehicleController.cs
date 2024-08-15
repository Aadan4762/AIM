using AIM.Dtos.EntityDtos;
using AIM.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AIM.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehicleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicles(
            //check for null in each query before applying filter
            [FromQuery] string number_plate = null,
            [FromQuery] string vin = null,
            [FromQuery] string model = null
            )
        {
            var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
                // Apply filtering based on query parameters
            if (!string.IsNullOrEmpty(number_plate))
            {
                vehicles = vehicles.Where(v => v.number_plate.Contains(number_plate, StringComparison.OrdinalIgnoreCase));
            }
    
            if (!string.IsNullOrEmpty(vin))
            {
                vehicles = vehicles.Where(v => v.vin.Contains(vin, StringComparison.OrdinalIgnoreCase));
            }
    
            if (!string.IsNullOrEmpty(model))
            {
                vehicles = vehicles.Where(v => v.model.Contains(model, StringComparison.OrdinalIgnoreCase));
            }
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicles(int id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicles(VehiclesDto vehiclesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map FurnitureDto to Furniture entity
            var vehicle = new Vehicle
            {
                vin = vehiclesDto.vin,
                number_plate = vehiclesDto.number_plate,
                engine_type  = vehiclesDto.engine_type,
                color = vehiclesDto.color,
                make = vehiclesDto.make,
                model = vehiclesDto.model,
                year = vehiclesDto.year,
                body = vehiclesDto.body,
                fuel_type = vehiclesDto.fuel_type,
                drive_train = vehiclesDto.drive_train,
                sitting_capacity = vehiclesDto.sitting_capacity,
                warranty = vehiclesDto.warranty,
                price = vehiclesDto.price,
                insuarance = vehiclesDto.insuarance,
                status = vehiclesDto.status,
                date_recorded = vehiclesDto.date_recorded
               
            };

            await _unitOfWork.Vehicles.AddAsync(vehicle);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetVehicles), new { id = vehicle.id }, vehicle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, VehiclesDto vehiclesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingVehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            if (existingVehicle == null)
            {
                return NotFound();
            }

            // Map VehiclesDto to existing Vehicle entity
            existingVehicle.vin = vehiclesDto.vin;
            existingVehicle.number_plate = vehiclesDto.number_plate;
            existingVehicle.color = vehiclesDto.color;
            existingVehicle.make = vehiclesDto.make;
            existingVehicle.model = vehiclesDto.model;
            existingVehicle.year = vehiclesDto.year;
            existingVehicle.engine_type = vehiclesDto.engine_type;
            existingVehicle.body = vehiclesDto.body;
            existingVehicle.fuel_type = vehiclesDto.fuel_type;
            existingVehicle.drive_train = vehiclesDto.drive_train;
            existingVehicle.sitting_capacity = vehiclesDto.sitting_capacity;
            existingVehicle.warranty = vehiclesDto.warranty;
            existingVehicle.price = vehiclesDto.price;
            existingVehicle.insuarance = vehiclesDto.insuarance;
            existingVehicle.status = vehiclesDto.status;
            existingVehicle.date_recorded= vehiclesDto.date_recorded;

            // Update the vehicle in the database
            _unitOfWork.Vehicles.Update(existingVehicle);
            await _unitOfWork.CompleteAsync();

            return NoContent(); // 204 No Content
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _unitOfWork.Vehicles.Remove(vehicle);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }