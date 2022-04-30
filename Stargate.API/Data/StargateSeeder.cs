/*using System;
using System.Linq;
using Stargate.API.Data.Entities;

namespace Stargate.API.Data
{
    public class StargateSeeder
    {
        private readonly StargateContext _context;
        public StargateSeeder(StargateContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            if (_context.Files.Any() == false)
            {
                var txtFile = new File()
                {
                    FileName = "myfile.txt",
                    FileExtension = "txt",
                    ContentType = "text/plain",
                    ExternalUri = "http://azurefiles.com/blob/uploads/myfile",
                    ShortUri = "http://stargate.archlt.com/myfile",
                    FileSizeBytes = 1024,
                    CreatedAtUtc = DateTime.UtcNow
                };

                var cssFile = new File()
                {
                    FileName = "style.css",
                    FileExtension = "css",
                    ContentType = "text/css",
                    ExternalUri = "http://azurefiles.com/blob/uploads/style",
                    ShortUri = "http://stargate.archlt.com/style",
                    FileSizeBytes = 2048,
                    CreatedAtUtc = DateTime.UtcNow
                };

                _context.Files.Add(txtFile);
                _context.Files.Add(cssFile);

                _context.SaveChanges();
            }
        }
    }
}*/