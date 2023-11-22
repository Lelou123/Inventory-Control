namespace InventoryControl.Domain.Dtos.Requests.Supplier;

public class PostSupplierRequest
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public PostSupplierRequest(string name, string email, string phone, string address)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
    }
}