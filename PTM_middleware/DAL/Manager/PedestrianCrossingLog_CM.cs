using MongoDB.Driver;
using PTM_middleware.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTM_middleware.DAL.Manager
{
    public class PedestrianCrossingLog_CM : IDataRepository<PedestrianCrossingLog>
    {
        public string _collectionCodePrefix
        {
            get
            {
                return "PDL";
            }
        }

        private readonly MongoContext _context;

        public PedestrianCrossingLog_CM(MongoContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(PedestrianCrossingLog document)
        {
            try
            {
                Random _rdm = new Random();
                int id2 = _rdm.Next(000000, 1000000);
                int id1 = Convert.ToInt32(DateTime.Now.ToString("mmssff"));
                var id = id1 | id2;

                document.Code = _collectionCodePrefix + id.ToString();
                await _context.PedestrianCrossingLogCollection.InsertOneAsync(document);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> DeleteAsync(string id, string userCode, string reason = "")
        {
            throw new NotImplementedException();
        }

        public async Task<List<PedestrianCrossingLog>> GetAllAsync(string getDocs = "active")
        {
            try
            {
                return await _context.PedestrianCrossingLogCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<PedestrianCrossingLog> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(PedestrianCrossingLog document)
        {
            throw new NotImplementedException();
        }
    }
}
