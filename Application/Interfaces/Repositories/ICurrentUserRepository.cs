namespace Application.Interfaces.Repositories
{
    public interface ICurrentUserRepository
    {
        string Id { get; }
        string Email { get; }
        string Role { get; }
    }
}