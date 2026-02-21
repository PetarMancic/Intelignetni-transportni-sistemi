using Microsoft.AspNetCore.Identity;

public class Passenger
    {
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
   
    //Todo : Add navigation properties for related entities(e.g., Ride, Reviews)
}

