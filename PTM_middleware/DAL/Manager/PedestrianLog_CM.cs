using MongoDB.Driver;
using PTM_middleware.DAL.Repository;
using PTM_middleware.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTM_middleware.DAL.Manager
{
    public class PedestrianLog_CM : IPedestrianLog
    {
        private readonly MongoContext _context;

        public PedestrianLog_CM(MongoContext context)
        {
            _context = context;
        }

        public string _collectionCodePrefix
        {
            get
            {
                return "PDL";
            }
        }

        public async Task<bool> AddAsync(PedestrianLog document)
        {
            try
            {
                Random _rdm = new Random();
                int id2 = _rdm.Next(000000, 1000000);
                int id1 = Convert.ToInt32(DateTime.Now.ToString("mmssff"));
                var id = id1 | id2;

                document.Code = _collectionCodePrefix + id.ToString();
                await _context.PedestrianLogCollection.InsertOneAsync(document);
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

        public async Task<List<PedestrianLog>> GetAllAsync(string getDocs = "active")
        {
            try
            {
                return await _context.PedestrianLogCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<PedestrianLog> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(PedestrianLog document)
        {
            throw new NotImplementedException();
        }
    }
}
