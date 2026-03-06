namespace DeltaDrive.Repository.Interfaces
{
    public interface  IPassengerRepository : IBaseRepository<Passenger>
    {
        Task<Passenger> GetByEmailAsync(string email);
        Task<Passenger> GetByVerificationTokenAsync(string token);

    }
}
