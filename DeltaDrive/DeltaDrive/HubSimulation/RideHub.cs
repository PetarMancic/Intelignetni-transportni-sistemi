
using Microsoft.AspNetCore.SignalR;
namespace DeltaDrive.HubSimulation
{
    public class RideHub : Hub
    {
        public async Task JoinRide(int rideId)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"ride-{rideId}");
                Console.WriteLine($"Klijent {Context.ConnectionId} joined ride-{rideId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JoinRide greska: {ex.Message}");
                throw;
            }
        }

        public async Task LeaveRide(int rideId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"ride-{rideId}");
        }
    }
}
