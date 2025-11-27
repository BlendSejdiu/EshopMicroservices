namespace Ordering.Application.DTO;

public record PaymentDTO
    (
     string CardName,
     string CardNumber,
     string Expiration,
     string Cvv,
     int PaymentMethod 
    );