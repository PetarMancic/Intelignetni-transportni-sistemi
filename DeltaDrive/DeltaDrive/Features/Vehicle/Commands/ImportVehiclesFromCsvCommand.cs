using MediatR;

public sealed record ImportVehiclesFromCsvCommand(string FilePath) : IRequest;
