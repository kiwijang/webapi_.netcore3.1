using MessageBoard.API.Infrastructure.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Repositories
{
    public class MsgRepository : IMsgRepository
    {
        private readonly MessageDBContext _context;

        public MsgRepository(MessageDBContext context)
        {
            _context = context;
        }
        public async Task Create(Message entity)
        {
            await _context.Message.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Message entity)
        {
            _context.Message.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Message> GetAllMsg()
        {
            return _context.Message.AsEnumerable();
        }

        public async Task<Message> GetMsg(int id)
        {
            return await _context.Message.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
