using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLC.Business;
using QLC.Business.RequestBusiness;
using QLC.Models.RepuestViewModel;

namespace QLC.Controllers.RequestModelsControllers
{
    public class RequestsController : Controller
    {
        private  IRequestBusiness _request;
        public RequestsController()
        {
            _request = new RequestBusiness();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_request.GetAll().Where(x=> x.UserId== HttpContext.Session.GetString("username") && x.Deleted==false));
        }
         [HttpGet]
        public  IActionResult AddRequest()
        {
            RequestViewModel request = new RequestViewModel();
           
            return PartialView("_AddRequestPartial", request);
        }

        [HttpPost]
        public IActionResult AddRequest(RequestViewModel requestViewModel)
        {
            if (ModelState.IsValid)
            {
                if (requestViewModel.ResquestName != null)
                {
                    requestViewModel.UserId = HttpContext.Session.GetString("username");
                   if(_request.AddRequest(requestViewModel))
                    {
                        _request.SendGmail("ngonga16bg.mta@gmail.com", "nga.ngo@nccsoft.vn");
                        return RedirectToAction("Index");
                    }           
                }
            }
            return PartialView("_AddRequestPartial", requestViewModel);
        }

        [HttpGet]
        public IActionResult EditRequest(int RequestId)
        {
            var request = _request.GetByIdRequest(RequestId);
            return PartialView("_EditRequest", request);
        }

        [HttpPost]
        public IActionResult EditRequest(RequestViewModel requestView)
        {
            if (requestView != null)
            {
                if (_request.EditRequest(requestView))
                    return RedirectToAction("Index");
            }
            return View();
        }
    }
}