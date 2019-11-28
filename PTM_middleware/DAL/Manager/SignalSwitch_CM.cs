using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using PTM_middleware.DAL.Repository;
using PTM_middleware.Models.DB;

namespace PTM_middleware.DAL.Manager
{
    public class SignalSwitch_CM : ISignalSwitchLog
    {

        private readonly MongoContext _context;

        public SignalSwitch_CM(MongoContext context)
        {
            _context = context;
        }

        public string _collectionCodePrefix
        {
            get
            {
                return "SS";
            }
        }

        public async Task<bool> AddAsync(SignalSwitch document)
        {
            try
            {
                Random _rdm = new Random();
                int id2 = _rdm.Next(000000, 1000000);
                int id1 = Convert.ToInt32(DateTime.Now.ToString("mmssff"));
                var id = id1 | id2;

                await _context.SignalSwitchCollection.InsertOneAsync(document);
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

        public async Task<List<SignalSwitch>> GetAllAsync(string getDocs = "active")
        {
            try
            {
                return await _context.SignalSwitchCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<SignalSwitch> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(SignalSwitch document)
        {
            throw new NotImplementedException();
        }
    }
}
