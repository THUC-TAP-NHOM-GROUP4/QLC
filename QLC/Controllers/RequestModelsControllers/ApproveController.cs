using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLC.Business.RequestBusiness;
using QLC.Models.RepuestViewModel;

namespace QLC.Controllers.RequestModelsControllers
{
    public class ApproveController : Controller
    {
        private IRequestBusiness _request;
        public ApproveController()
        {
            _request = new RequestBusiness();
        }

        public IActionResult Index()
        {
            return View(_request.GetAllStatusNew().Where(x =>x.Deleted == false));
        }
        [HttpGet]
        public IActionResult EditRequest(int RequestId)
        {
            ViewData["Status"] = _request.GetStatus();
            var request = _request.GetByIdRequest(RequestId);
            return PartialView("_EditRequest",request);
        }
        public IActionResult EditRequest(RequestViewModel requestView)
        {
            if(requestView != null)
            {
                if (_request.EditRequest(requestView))
                    return RedirectToAction("Index");
            }
            return View();
        }
    }
}