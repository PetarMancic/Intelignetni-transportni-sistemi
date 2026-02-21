using MediatR;
using System.Globalization;
using DeltaDrive.Domain;


namespace DeltaDrive.Features.Vehicle.Commands
{
    public class ImportVehiclesFromCsvCommandHandler : IRequestHandler<ImportVehiclesFromCsvCommand>
    {
        private readonly IMediator _mediator;
        private readonly DeltaDriveDbContext _context;

        public ImportVehiclesFromCsvCommandHandler(IMediator mediator, DeltaDriveDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task Handle(
            ImportVehiclesFromCsvCommand request,
            CancellationToken cancellationToken)
        {
            if (!File.Exists(request.FilePath))
                throw new FileNotFoundException("CSV file not found.");

            var lines = await File.ReadAllLinesAsync(request.FilePath, cancellationToken);

            // Preskoči header
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var columns = line.Split(',');

                var startPriceText = columns[5].Replace("EUR", "").Trim();
                var pricePerKMText = columns[6].Replace("EUR", "").Trim();

                Location location = new Location
                {
                    Latitude = double.Parse(columns[3], CultureInfo.InvariantCulture),
                    Longitude = double.Parse(columns[4], CultureInfo.InvariantCulture)
                };
                var vehicle = new DeltaDrive.Domain.Vehicle
                {
                    Brand = columns[0],
                    DriverName = columns[1],
                    DriverSurname = columns[2],
                    Location= location,
                    StartPrice = double.Parse(columns[5].Replace("EUR", "").Trim(), CultureInfo.InvariantCulture),
                    pricePerKM = double.Parse(columns[6].Replace("EUR", "").Trim(), CultureInfo.InvariantCulture)
                };

                _context.Vehicles.Add(vehicle);
                
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
