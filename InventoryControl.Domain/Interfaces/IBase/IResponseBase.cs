namespace InventoryControl.Domain.Interfaces;

public interface IResponseBase<T>
{
    T? Data { get; set; }
    bool IsSuccess { get; set; }
    string? Message { get; set; }
}