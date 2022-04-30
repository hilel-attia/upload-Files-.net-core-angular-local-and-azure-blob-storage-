using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Stargate.API.Data.Entities;
using Stargate.API.Services;

namespace Stargate.API.Data.Repository
{
    public class StargateRepository : IStargateRepository
    {
        private readonly IMongoCollection<File> _files;
        public StargateRepository(IFilestoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _files = database.GetCollection<File>(settings.FilesCollectionName);
        }
        public async Task<IEnumerable<File>> GetFiles()
        {
            return await _files.Find(x => true).ToListAsync();
        }

        public async Task<File> GetFileByIdAsync(int id)
        {
            var banner = Builders<File>.Filter.Eq("Id", id);
            return await _files.Find(banner).FirstOrDefaultAsync();
        }

        public async Task<File> GetFileByFileNameAsync(string fileName)
        {
            var banner = Builders<File>.Filter.Eq("FileName", fileName);
            return await _files.Find(banner).FirstOrDefaultAsync();
        }



      

        /// <summary>
        /// Add's a file to the database if does not exist. 
        /// If it already exists this will return to you the existing file Id
        /// </summary>
        /// <param name="file">File Entity</param>
        /// <returns></returns>
        public async Task AddFileAsync(File file)
        {
           

            await _files.InsertOneAsync(file);
          
            
        }
    }
}