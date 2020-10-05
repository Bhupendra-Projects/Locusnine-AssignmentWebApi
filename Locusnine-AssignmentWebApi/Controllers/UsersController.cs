using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using locusnine.Service.Service;
using Locusnine.Data;
using Newtonsoft.Json;

namespace Locusnine_AssignmentWebApi.Controllers
{
    public class UsersController : ApiController
    {
        private LocusnineUserDataEntities db = new LocusnineUserDataEntities();
        int count = 0;
        // GET: api/Users
        public string GetUsers()
        {
            return JsonConvert.SerializeObject(db.Users.ToList());
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public string PutUser(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            try
            {
                count = db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
            if (count > 0)
                return JsonConvert.SerializeObject(HttpStatusCode.OK);
            else
                return JsonConvert.SerializeObject(HttpStatusCode.InternalServerError);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public string PostUser(User user)
        {
            EmailValidation ev = new EmailValidation();
            string email = ev.AppendEmailSuffix(user.Name);
            user.Email = email;
            db.Users.Add(user);
            try
            {
                count = db.SaveChanges();
            }
            catch (Exception e)
            {

                throw;
            }
            if (count > 0)
                return JsonConvert.SerializeObject(HttpStatusCode.OK);
            else
                return JsonConvert.SerializeObject(HttpStatusCode.InternalServerError);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public string DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return JsonConvert.SerializeObject(HttpStatusCode.NotFound);
            }

            db.Users.Remove(user);
            count = db.SaveChanges();

            if (count > 0)
                return JsonConvert.SerializeObject(HttpStatusCode.OK);
            else
                return JsonConvert.SerializeObject(HttpStatusCode.InternalServerError);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}