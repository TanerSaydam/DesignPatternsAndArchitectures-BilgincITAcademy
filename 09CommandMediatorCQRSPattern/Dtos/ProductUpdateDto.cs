namespace _09CommandMediatorCQRSPattern.Dtos;

public sealed record ProductUpdateDto(
    Guid Id,
    string Name,
    decimal Price);