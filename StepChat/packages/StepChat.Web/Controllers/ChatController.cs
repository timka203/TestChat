using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StepChat.Server;
using StepChat.Server.DB;
using StepChat.Web.DB;
using DB_Model = StepChat.Server.DB.DB_Model;

namespace StepChat.Web.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Success()
        {
            return View();
        }

        // GET: Chat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Chat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chat/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            //try
            //{
                using (StepChat.Web.DB.DB_Model dB= new DB.DB_Model())
                {
                     User user = new User();
                    user.ContactNickname = collection["ContactNickname"];
               
                    user.Password = collection["Password"];
                dB.User.Add(new User() { ContactNickname = "test2", Password = "test2" });
                    dB.SaveChanges();

                }


                    return RedirectToAction("Success");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Chat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Chat/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Chat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
