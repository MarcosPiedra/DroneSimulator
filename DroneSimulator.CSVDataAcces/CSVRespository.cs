using DroneSimulator.Configuration;
using DroneSimulator.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DroneSimulator.CSVDataAccess
{
    public class CSVRespository<T> : IRepository<T> where T : class
    {
        private readonly Func<CSVFileType, object> getLineParser;
        private List<T> data = new List<T>();

        public CSVRespository(Func<CSVFileType, object> getLineParser)
        {
            this.getLineParser = getLineParser;
        }

        public async Task Load(string basePath, CSVFileType type)
        {
            if (!Directory.Exists(basePath))
                throw new Exception($"Directory not extist: ${basePath}");

            IEnumerable<string> files = Enumerable.Empty<string>();
            if (type == CSVFileType.Tube)
            {
                files = Directory.GetFiles(basePath, "tube.csv").AsEnumerable();
            }
            else if (type == CSVFileType.LocationDrone)
            {
                files = Directory.GetFiles(basePath, "????.csv")
                                 .AsEnumerable()
                                 .Where(f => int.TryParse(Path.GetFileNameWithoutExtension(f), out _)).ToList();
            }

            var parser = getLineParser(type) as ILineParser<T>;

            foreach (var file in files)
            {
                using var reader = File.OpenText(file);
                var line = "";
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    data.Add(parser.Parse(line));
                }
            }
        }

        public IQueryable<T> Query => this.data.AsQueryable();

        // Below methods not needed... at the moment :)

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(object id1, object id2)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
