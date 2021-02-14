using System;

namespace Search.Core.Configuration.Interfaces
{
    public interface ISearchConfiguration
    { 
        Uri Uri { get; } 
        int Fuzziness { get; }
    }
}