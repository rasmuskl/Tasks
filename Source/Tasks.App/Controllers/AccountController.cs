using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Tasks.App.Models;
using Tasks.Model;
using Tasks.Model.CommandHandlers;
using Tasks.Model.Commands;

namespace Tasks.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly CommandBus _commandBus = new CommandBus();

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string storedPasswordHash;

            if(!Storage.PasswordHashes.TryGetValue(model.Email.ToLowerInvariant(), out storedPasswordHash))
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View(model);
            }

            var passwordHash = Sha1HashPassword(model.Password);

            if(!string.Equals(passwordHash, storedPasswordHash, StringComparison.InvariantCultureIgnoreCase))
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);

            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if(Storage.RegisteredEmails.Contains(model.Email))
                {
                    ModelState.AddModelError("", "A user with the given email is already registered.");
                    return View(model);
                }

                FormsAuthentication.SetAuthCookie(model.Email, false);
                string passwordSha1 = Sha1HashPassword(model.Password);

                var command = new RegisterUserCommand(model.Email, passwordSha1);

                _commandBus.Handle(command);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        private string Sha1HashPassword(string password)
        {
            SHA1 hasher = SHA1.Create();
            byte[] hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
            string passwordHash = BitConverter.ToString(hashBytes);
            return passwordHash;
        }
    }
}
