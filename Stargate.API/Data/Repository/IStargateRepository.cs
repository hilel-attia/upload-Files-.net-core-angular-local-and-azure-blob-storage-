using System.Collections.Generic;
using System.Threading.Tasks;
using Stargate.API.Data.Entities;

namespace Stargate.API.Data.Repository
{
    public interface IStargateRepository
    {
        Task<IEnumerable<File>> GetFiles();
        Task<File> GetFileByIdAsync (int id);
        Task<File> GetFileByFileNameAsync (string fileName);
        Task AddFileAsync(File file);

    }
}