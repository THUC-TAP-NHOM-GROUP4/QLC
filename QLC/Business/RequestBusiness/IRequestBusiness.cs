using QLC.Models.RepuestViewModel;
using Request.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLC.Business.RequestBusiness
{
    interface IRequestBusiness
    {
        List<RequestViewModel> GetAll();
        bool AddRequest(RequestViewModel request);
        List<RequestViewModel> GetAllStatusNew();
        List<StatusViewModel> GetStatus();
        RequestViewModel GetByIdRequest(int RequestId);
        bool EditRequest(RequestViewModel requestView);
        void SendGmail(string FromGmail, params string[] ToGmail);
    }
}
