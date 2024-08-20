namespace DefaultNamespace;

public class LedgerDto
{
   
    public string voucherNumber { get; set; }
    public string cardNumber { get; set; }
    public int qtyReceipt { get; set; }
    public int invoiceUnitPriceReceipt { get; set; }
    public int valueReceipt { get; set; }
    public int qtyIssues { get; set; }
    public int averageUnitPriceIssue { get; set; }
    public int valueIssues { get; set; }
    public int qtyBalances { get; set;  }
    public int valueBalances { get; set; }
    public string itemCodeNumber { get; set; }
    public string description { get; set; }
    public string ministry { get; set; }
    public string unitOfIssue { get; set; }
    public string location { get; set; }
    public string capacity { get; set; }
    public string departmentName { get; set; }
}