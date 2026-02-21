using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeltaDrive.Features.Passengers.Queries
{
    public sealed class GetPassengerByIdQueryHandler
       : IRequestHandler<GetPassengedByIdQuery, Passenger?>
    {
        private readonly DeltaDriveDbContext _context;

        public GetPassengerByIdQueryHandler(DeltaDriveDbContext context)
        {
            _context = context;
        }

        public async Task<Passenger?> Handle(
            GetPassengedByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Passengers
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        }
    }
}
