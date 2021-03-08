using ShivFactory.Business.Model;
using ShivFactory.Business.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace ShivFactory.Controllers
{
    public class SystemController : ApiController
    {
        public ResultModel BlockUser(string userId)
        {
            try
            {
                RepoUser repoUser = new RepoUser();
                var isBlock = repoUser.BlockUserByUserId(userId);
                return new ResultModel
                {
                    ResultFlag = isBlock,
                    Data = null,
                    Message = isBlock ? "User blocked successfully!!" : "Failled to block user."
                };
            }
            catch (Exception ex)
            {
                return new ResultModel
                {
                    ResultFlag = false,
                    Data = null,
                    Message = ex.Message
                };
            }
        }
    }
}
