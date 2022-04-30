using Microsoft.EntityFrameworkCore;
using Stargate.API.Data.Entities;

namespace Stargate.API.Data

{
    public class FilestoreDatabaseSettings : IFilestoreDatabaseSettings
    {
    public string FilesCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}

public interface IFilestoreDatabaseSettings
    {
    string FilesCollectionName { get; set; }
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
}
}