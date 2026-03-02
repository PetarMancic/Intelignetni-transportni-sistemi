using MediatR;
using System.Globalization;
using DeltaDrive.Repository.Interfaces;


namespace DeltaDrive.Features.Vehicle.Commands
{
    public class ImportVehiclesFromCsvCommandHandler : IRequestHandler<ImportVehiclesFromCsvCommand>
    {
        private readonly IMediator _mediator;
        private readonly IVehicleRepository _vehicleRepository;

        public ImportVehiclesFromCsvCommandHandler(IMediator mediator, IVehicleRepository vehicleRepository)
        {
            _mediator = mediator;
            _vehicleRepository = vehicleRepository;

        }

        public async Task Handle(
            ImportVehiclesFromCsvCommand request,
            CancellationToken cancellationToken)
        {
            if (!File.Exists(request.FilePath))
                throw new FileNotFoundException("CSV file not found.");

            var lines = await File.ReadAllLinesAsync(request.FilePath, cancellationToken);
            List<VehicleItem> vehicles = new();

            // Preskoči header
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var columns = line.Split(',');

                var location = new Location(
                    double.Parse(columns[3], CultureInfo.InvariantCulture),
                    double.Parse(columns[4], CultureInfo.InvariantCulture)
                );

                vehicles.Add(new VehicleItem
                {
                    Brand = columns[0],
                    DriverName = columns[1],
                    DriverSurname = columns[2],
                    Location = location,
                    StartPrice = double.Parse(columns[5].Replace("EUR", "").Trim(), CultureInfo.InvariantCulture),
                    pricePerKM = double.Parse(columns[6].Replace("EUR", "").Trim(), CultureInfo.InvariantCulture)
                });
            }

            await _vehicleRepository.AddRangeAsync(vehicles);

           
        }
    }
}
