using BAL.Interface;
using BAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEST.Models;

namespace TEST.Controllers
{
    public class UserRegistrationController : Controller
    {
        private IUserBAL _userBAL = null;

        public UserRegistrationController(IUserBAL userBAL)
        {
            _userBAL = userBAL;
        }

        [Authorize]
        // GET: UserRegistration
        public ActionResult Index()
        {
            List<UserViewModel> userViewModels = new List<UserViewModel>();

            try
            {
                List<UserModel> userModels = _userBAL.Get();
                userViewModels = userModels.ConvertAll(e => new UserViewModel()
                {
                    UserId = e.UserId,
                    UserName = e.UserName,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DOB = e.DOB
                }).ToList();
                
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(userViewModels);
        }

        // GET: UserRegistration/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserRegistration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRegistration/Create
        [HttpPost]
        public ActionResult Create(UserViewModel userViewModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    UserModel userModel = new UserModel()
                    {
                        UserName = userViewModel.UserName,
                        Password = userViewModel.Password,
                        FirstName = userViewModel.FirstName,
                        LastName = userViewModel.LastName,
                        DOB = userViewModel.DOB
                    };

                    _userBAL.Add(userModel);
                }
                
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserRegistration/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserRegistration/Edit/5
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

        // GET: UserRegistration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserRegistration/Delete/5
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
