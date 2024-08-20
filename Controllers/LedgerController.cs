using AIM.Dtos.EntityDtos;
using AIM.Models.Entities;
using Microsoft.AspNetCore.Mvc;

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
   public async Task<IActionResult> CreateDepartment(LedgerDto ledgerDto)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);

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
         capacity = ledgerDto.capacity
      };
      await _unitOfWork.Ledgers.AddAsync(ledger);
      await _unitOfWork.CompleteAsync();

      return CreatedAtAction(nameof(GetAllLedger), new { id = ledger.id }, ledger);
   }
   
   
   

   [HttpGet]
   public async Task<IActionResult> GetAllLedger()
   {
      var ledgers = await _unitOfWork.Ledgers.GetAllAsync();
      return Ok(ledgers);
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
}