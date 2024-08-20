using System.Linq.Expressions;
using AIM.Dtos.EntityDtos;
using AIM.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DefaultNamespace;


[Route("api/[controller]")]
[ApiController]
public class LedgerController : Controller
{
   private readonly IUnitOfWork _unitOfWork;


   public LedgerController(IUnitOfWork unitOfWork)
   {
      _unitOfWork = unitOfWork;
   }

   [HttpPost]
   public async Task<IActionResult> CreateLedger(LedgerDto ledgerDto)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      // Validate capacity
      if (ledgerDto.capacity != "requisition" && ledgerDto.capacity != "supplier")
      {
         return BadRequest("Invalid capacity. Only 'requisition' or 'supplier' are allowed.");
      }

      // Check if the department exists
      var department = await _unitOfWork.Departments.GetAllAsync();
      if (department == null || !department.Any(d => d.name == ledgerDto.departmentName))
      {
         return NotFound($"Department with name '{ledgerDto.departmentName}' does not exist.");
      }

      var ledger = new Ledger
      {
         voucherNumber = ledgerDto.voucherNumber,
         cardNumber = ledgerDto.cardNumber,
         qtyReceipt = ledgerDto.qtyReceipt,
         invoiceUnitPriceReceipt = ledgerDto.invoiceUnitPriceReceipt,
         valueReceipt = ledgerDto.valueReceipt,
         qtyIssues = ledgerDto.qtyIssues,
         averageUnitPriceIssue = ledgerDto.averageUnitPriceIssue,
         valueIssues = ledgerDto.valueIssues,
         qtyBalances = ledgerDto.qtyBalances,
         valueBalances = ledgerDto.valueBalances,
         itemCodeNumber = ledgerDto.itemCodeNumber,
         description = ledgerDto.description,
         ministry = ledgerDto.ministry,
         unitOfIssue = ledgerDto.unitOfIssue,
         location = ledgerDto.location,
         capacity = ledgerDto.capacity,
         departmentName = ledgerDto.departmentName
      };
      await _unitOfWork.Ledgers.AddAsync(ledger);
      await _unitOfWork.CompleteAsync();

      return CreatedAtAction(nameof(GetAllLedger), new { id = ledger.id }, ledger);
   }


   [HttpGet]
   public async Task<IActionResult> GetAllLedger(
      [FromQuery] int pageNumber = 1,
      [FromQuery] int pageSize = 10,
      [FromQuery] string sortBy = "id",
      [FromQuery] string sortOrder = "asc",
      [FromQuery] string voucherNumber = null,
      [FromQuery] string cardNumber = null)

   {
      var query = _unitOfWork.Ledgers.Query();

      // Apply filters
      if (!string.IsNullOrEmpty(voucherNumber))
      {
         query = query.Where(f => f.voucherNumber.Contains(voucherNumber, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(cardNumber))
      {
         query = query.Where(f => f.cardNumber.Contains(cardNumber, StringComparison.OrdinalIgnoreCase));
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

   public async Task<IActionResult> GetLedgerById(int id)
   {
      var ledger = await _unitOfWork.Ledgers.GetByIdAsync(id);
      if (ledger == null)
      {
         return NotFound();
      }

      return Ok(ledger);
   }

   [HttpPut("{id}")]
   public async Task<IActionResult> UpdateLedgerById(int id, LedgerDto ledgerDto)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      var existingLedger = await _unitOfWork.Ledgers.GetByIdAsync(id);
      if (existingLedger == null)
      {
         return NotFound();
      }

      // Map Dto to Entity
      existingLedger.voucherNumber = ledgerDto.voucherNumber;
      existingLedger.cardNumber = ledgerDto.cardNumber;
      existingLedger.qtyReceipt = ledgerDto.qtyReceipt;
      existingLedger.invoiceUnitPriceReceipt = ledgerDto.invoiceUnitPriceReceipt;
      existingLedger.valueReceipt = ledgerDto.valueReceipt;
      existingLedger.qtyIssues = ledgerDto.qtyIssues;
      existingLedger.averageUnitPriceIssue = ledgerDto.averageUnitPriceIssue;
      existingLedger.valueIssues = ledgerDto.valueIssues;
      existingLedger.qtyBalances = ledgerDto.qtyBalances;
      existingLedger.valueBalances = ledgerDto.valueBalances;
      existingLedger.itemCodeNumber = ledgerDto.itemCodeNumber;
      existingLedger.description = ledgerDto.description;
      existingLedger.ministry = ledgerDto.ministry;
      existingLedger.unitOfIssue = ledgerDto.unitOfIssue;
      existingLedger.location = ledgerDto.location;
      existingLedger.capacity = ledgerDto.capacity;

      _unitOfWork.Ledgers.Update(existingLedger);
      await _unitOfWork.CompleteAsync();
      return NoContent();
   }


   [HttpDelete("{id}")]

   public async Task<IActionResult> DeleteLedgerById(int id)
   {
      var ledger = await _unitOfWork.Ledgers.GetByIdAsync(id);
      if (ledger == null)
      {
         return NotFound();

      }

      _unitOfWork.Ledgers.Remove(ledger);
      await _unitOfWork.CompleteAsync();
      return NoContent();
   }

   private static Expression<Func<Ledger, object>> GetSortExpression(string sortBy)
   {
      return sortBy.ToLower() switch
      {
         "vouchernumber" => f => f.voucherNumber,
         "cardnumber" => f => f.cardNumber,
         "qtyreceipt" => f => f.qtyReceipt,
         "invoiceunitpricereceipt" => f => f.invoiceUnitPriceReceipt,
         "valuereceipt" => f => f.valueReceipt,
         "qtyissues" => f => f.qtyIssues,
         "averageunitpriceissue" => f => f.averageUnitPriceIssue,
         "valueissues" => f => f.valueIssues,
         "qtybalances" => f => f.qtyBalances,
         "valuebalances" => f => f.valueBalances,
         "itemcodenumber" => f => f.itemCodeNumber,
         "description" => f => f.description,
         "ministry" => f => f.ministry,
         "unitofissue" => f => f.unitOfIssue,
         "location" => f => f.location,
         "capacity" => f => f.capacity,
         "departmentname" => f => f.departmentName,
         "daterecorded" => f => f.dateRecorded,
         _ => f => f.id
      };
   }

}