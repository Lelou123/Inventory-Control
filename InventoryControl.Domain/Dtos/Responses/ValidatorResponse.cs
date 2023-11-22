namespace InventoryControl.Domain.Dtos.Responses;

public class ValidatorResponse
{
    public List<string>? ErrorsMessages { get; set; }
    public bool IsValid { get; init; }
}