using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using QLC.Models.RepuestViewModel;
using Request.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace QLC.Business.RequestBusiness
{
    public class RequestBusiness:IRequestBusiness
    {
        private readonly RequestDbContext _context;
        public RequestBusiness()
        {
            _context = new RequestDbContext();
        }

        public List<RequestViewModel> GetAll()
        {
            var lstRequest = from request in _context.Requests
                             join status in _context.Status
                             on request.StateId equals status.StatusId
                             select new
                             {
                                 request.UserId,
                                 request.RequestId,
                                 request.ResquestName,
                                 request.StartDate,
                                 request.FinishDate,
                                 request.StateId,
                                 status.StatusName,
                                 request.Amount,
                                 request.Price
                             };
            var requests = new List<RequestViewModel>();
            foreach (var item in lstRequest)
            {
                RequestViewModel request = new RequestViewModel()
                {
                    RequestId = item.RequestId,
                    ResquestName = item.ResquestName,
                    UserId = item.UserId,
                    StartDate = item.StartDate,
                    FinishDate = item.FinishDate,
                    StateId = item.StateId,
                    StatusName = item.StatusName,
                    Money = item.Amount*item.Price
                };
                requests.Add(request);
            }
            return requests;
        }


        public bool AddRequest(RequestViewModel requestView)
        {
            if (requestView != null)
            {
                Requests requests = new Requests()
                {
                    ResquestName = requestView.ResquestName,
                    StartDate = requestView.StartDate,
                    FinishDate = requestView.FinishDate,
                    Amount = requestView.Amount,
                    Price = requestView.Price,
                    Deleted = false,
                    ContentName = requestView.ContentName,
                    Note = requestView.Note,
                    StateId = 1,
                    UserId = requestView.UserId
                };
                _context.Add(requests);
            }
            return _context.SaveChanges() > 0 ? true : false;
        }

        public List<RequestViewModel> GetAllStatusNew()
        {
            var lstRequest = from request in _context.Requests
                             join status in _context.Status
                             on request.StateId equals status.StatusId
                             orderby status.StatusId ascending
                             select new
                             {
                                 request.UserId,
                                 request.RequestId,
                                 request.ResquestName,
                                 request.StartDate,
                                 request.FinishDate,
                                 status.StatusName,
                             };
            var requests = new List<RequestViewModel>();
            foreach (var item in lstRequest)
            {
                RequestViewModel request = new RequestViewModel()
                {
                    RequestId = item.RequestId,
                    ResquestName = item.ResquestName,
                    UserId = item.UserId,
                    StartDate = item.StartDate,
                    FinishDate = item.FinishDate,
                    StatusName = item.StatusName
                };
                requests.Add(request);
            }
            return requests;
        }

        public List<StatusViewModel> GetStatus()
        {
           var lstStatus = _context.Status.ToList();
            var lstStatusView = new List<StatusViewModel>();
            foreach(var item in lstStatus)
            {
                StatusViewModel statusView = new StatusViewModel()
                {
                    StatusId = item.StatusId,
                    StatusName = item.StatusName
                };
                lstStatusView.Add(statusView);
            }
            return lstStatusView;
        }

        public RequestViewModel GetByIdRequest(int RequestId)
        {
            var request = _context.Requests.FirstOrDefault(x => x.RequestId == RequestId);
            RequestViewModel requestView = new RequestViewModel()
            {
                RequestId = request.RequestId,
                ResquestName = request.ResquestName,
                ContentName = request.ContentName,
                Note = request.Note,
                Amount = request.Amount,
                Price = request.Price,
                StateId = request.StateId,
                StartDate = request.StartDate,
                FinishDate = request.FinishDate,
                UserId = request.UserId
            };
            return requestView;
        }

        public bool EditRequest(RequestViewModel requestView)
        {
            if (requestView != null)
            {
                Requests requests = new Requests()
                {
                    StateId = requestView.StateId,
                    RequestId = requestView.RequestId,
                    ResquestName = requestView.ResquestName,
                    ContentName = requestView.ContentName,
                    Note = requestView.Note,
                    Amount = requestView.Amount,
                    Price = requestView.Price,
                    Deleted = requestView.Deleted,
                    FinishDate = requestView.FinishDate,
                    StartDate = requestView.StartDate,
                    UserId = requestView.UserId 
                };
                _context.Update(requests); 
            }
            return _context.SaveChanges() > 0 ? true : false;
        }

        public void SendGmail(string FromGmail, params string[] ToGmail)
        {
            foreach(string item in ToGmail)
            {
                MailMessage mail = new MailMessage(FromGmail, item);
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("user@gmail.com", "password");
                client.Host = "smtp.gmail.com";
                mail.BodyEncoding = UTF8Encoding.UTF8;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.Subject = "this is a test mail";
                mail.Body = "this is my test email body";
                client.Send(mail);
            }
            
        }
    }
}
