using MessageBoard.API.Infrastructure.Entites;
using MessageBoard.API.Models;
using MessageBoard.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MessageBoard.API.Services
{
    public class MsgService : IMsgService
    {
        private readonly IMsgRepository _repostitory;

        public MsgService(IMsgRepository repository)
        {
            _repostitory = repository;
        }
        public async Task Create(MsgModel entity)
        {
            //用到 DAO 好像怪怪的
            Message msgEntity = new Message()
            {
                Name = entity.Name,
                Title = entity.Title,
                Content = entity.Content,
                CreateTime = DateTime.Now
            };
            await _repostitory.Create(msgEntity);
        }

        public async Task Delete(Message entity)
        {
            await _repostitory.Delete(entity);
        }

        public IEnumerable<Message> GetAllMsg()
        {
            return _repostitory.GetAllMsg();
        }

        public async Task<Message> GetMsg(int id)
        {
            return await _repostitory.GetMsg(id);
        }

        public IEnumerable<Message> GetMsgBySearchWord(string keyword)
        {
            string pattern = $"[{keyword}]";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            var allMsg = _repostitory.GetAllMsg();
            IEnumerable<Message> msgModel = allMsg.Where(x => r.IsMatch(x.Name) || r.IsMatch(x.Title)).ToList();
            return msgModel;
        }
    }
}
