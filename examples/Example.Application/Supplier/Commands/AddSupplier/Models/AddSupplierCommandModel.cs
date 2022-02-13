namespace Example.Application.Supplier.Commands.AddSupplier.Models;

public class AddSupplierCommandModel
{
    public AddSupplierCommandModel()
    {
        Contact = new SupplierContactModel();
    }

    public string SupplierName { get; set; }

    public SupplierContactModel Contact { get; set; }
}