using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageBoard.API.Models;
using MessageBoard.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessageBoard.API.Controllers
{
    /// <summary>
    /// 留言板 API
    /// </summary>
    [Route("api")]
    [ApiController]
    public class MessageController : BaseController
    {
        private readonly IMsgService _service;

        /// <summary>
        /// MessageController
        /// </summary>
        /// <param name="service">注入 IMsgService</param>
        public MessageController(IMsgService service)
        {
            _service = service;
        }
        /// <summary>
        /// 根據 Id 回傳一筆詳細資料
        /// </summary>
        /// <param name="id">Message 的 ID</param>
        /// <returns>Ok(new { result, result.Data })</returns>
        [HttpGet("detail")]
        public async Task<IActionResult> GetMessage(int id)
        {
            ResultModel result = new ResultModel();
            try
            {
                var message = await _service.GetMsg(id);
                result.Data = message;
                return Ok(new { result, result.Data });
            }
            catch (Exception ex)
            {
                return ErrOk(result, ex);
            }
        }
        /// <summary>
        /// 根據 keyword 回傳多筆
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost("filter")]
        public IActionResult PostFilterMessage(string keyword, PageModel page)
        {
            ResultModel result = new ResultModel();
            try
            {
                // 沒輸入的話就回傳全部 Msg
                if (string.IsNullOrEmpty(keyword))
                {
                    var AllMessage = _service.GetAllMsg();
                    return Ok(AllMessage);
                }
                var filteredMessage = _service.GetMsgBySearchWord(keyword);

                int oCurPage = 1;
                int oPageSize = 2;
                //驗證 page 資訊
                //如果都不是數字
                if (!(int.TryParse(page.CurrentPage, out oCurPage) && int.TryParse(page.PageSize, out oPageSize)))
                {
                    oCurPage = 1;
                    oPageSize = 2;
                }

                DataModel d = new DataModel();
                d.TotalCount = filteredMessage.Count();
                d.PageSize = Convert.ToInt32(oPageSize);
                d.PageNumber = Convert.ToInt32(oCurPage);
                d.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(d.TotalCount / d.PageSize)));

                d.Items = filteredMessage;
                result.Data = d;
                return Ok(new { result, result.Data });
            }
            catch (Exception ex)
            {
                return ErrOk(result, ex);
            }
        }

        /// <summary>
        /// 新增一筆
        /// </summary>
        /// <param name="msgModel"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> PostCreateMessage([Bind("Name,Title,Content")]MsgModel msgModel)
        {
            if (!ModelState.IsValid)
            {
                var errModelStateMessage = ModelState.Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(k => k.Key, k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                return Ok(errModelStateMessage);
            }

            ResultModel result = new ResultModel();
            try
            {
                await _service.Create(msgModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ErrOk(result, ex);
            }
        }

        /// <summary>
        /// 根據 Id 刪除一筆
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            ResultModel result = new ResultModel();
            try
            {
                var message = await _service.GetMsg(id);
                await _service.Delete(message);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ErrOk(result, ex);
            }
        }
    }
}
