using MessageBoard.API.Infrastructure.Entites;
using MessageBoard.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Services
{
    public interface IMsgService
    {
        Task<Message> GetMsg(int id);
        IEnumerable<Message> GetAllMsg();
        Task Create(MsgModel entity);
        Task Delete(Message entity);
        IEnumerable<Message> GetMsgBySearchWord(string searchWord);
    }
}
