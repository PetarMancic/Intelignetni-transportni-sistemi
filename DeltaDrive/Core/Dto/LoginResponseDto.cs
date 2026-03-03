namespace Core.Dto
{ public sealed record LoginResponseDto(
        int PassengerId,
        string Email,
        string FirstName,
        string LastName,
        string Token
    );
}
