using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageBoard.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessageBoard.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseController : ControllerBase
    {
        //記得用成 protected 不然 swagger 會顯示這裡到頁面上...
        protected OkObjectResult ErrOk(ResultModel result, Exception ex)
        {
            ErrorCodeModel errModel = new ErrorCodeModel();
            result.IsSuccess = false;
            result.ReturnCode = errModel.IsNull;
            result.ReturnMessage = ex.Message;
            return Ok(result);
        }
    }
}