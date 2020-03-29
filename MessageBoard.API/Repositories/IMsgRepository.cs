using MessageBoard.API.Infrastructure.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Repositories
{
    public interface IMsgRepository
    {
        IEnumerable<Message> GetAllMsg();
        Task Create(Message entity);
        Task Delete(Message entity);
        Task<Message> GetMsg(int id);
    }
}
