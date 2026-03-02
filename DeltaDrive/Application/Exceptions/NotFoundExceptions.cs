
namespace Application.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string name, object key)
            : base($"Entity '{name}' with key '{key}' is not found.") { }
    }
}
