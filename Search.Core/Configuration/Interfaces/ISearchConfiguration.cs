namespace Search.Core.Configuration.Interfaces
{
    public interface ISearchConfiguration
    { 
        string Hostname { get; } 
        string Username { get; }
        string Password { get; }
        int Fuzziness { get; }
    }
}