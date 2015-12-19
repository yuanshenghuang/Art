using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kunst.Models;
using System.Web.Security;


 
namespace Kunst.Controllers
{
    //https://msdn.microsoft.com/en-us/magazine/dn605872.aspx
   //http://stackoverflow.com/questions/23130795/how-does-a-new-asp-net-mvc-5-application-know-how-to-create-a-database-and-how-d

    [Authorize]
    public class AccountController : BaseController
    {   
        //De Identity system van microsoft is gebasseerd op UserManager en UserStore
        //UserManager class : gebruikers in en uit schrijven
        //UserStore class : verifiëren van verificatiegegevens(gebruikersnaam, paswoord....)
        //UserManager wordt geïnjecteerd in de constructor van AccountController
        //ApplicationUser : gebruikers
        //in de constructor van UserManager bevat UserStore en in de constructor van UserStore bevat DbContext();

        //:this() : in de contructor van AccountController wordt nieuw object van UserManager()... gecreërd.

        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }


        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }


        public UserManager<ApplicationUser> UserManager { get; private set; }


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // [ValidateAntiForgeryToken] :Anti-vervalsing ondersteuning van MVC, schrijft een unieke waarde aan een HTTP-Cookie en vervolgens dezelfde waarde wordt weggeschreven naar de FORM.
        //Wanneer de pagina wordt ingediend, wordt er een fout geconstateerd als de cookie-waarde niet overeenkomt met de FORM waarde.

        //Het is belangrijk om kruis site request te voorkomen. Dat wil zeggen, een FORM van een andere site die berichten naar uw site in een poging om in de verborgen inhoud binnen te dringen met behulp van geverifieerde gebruikersgegevens.

        //ModelState.IsValid tells you if any model errors have been added to ModelState.
        //The sample DataAnnotations model binder will fill model state with validation errors taken from the DataAnnotations attributes on your model.
        //RedirectToLocal() : Redirect to requested page after authentication
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginviewmodel, string returnUrl)
        {      
            if (ModelState.IsValid)
            {
                
                var user = await UserManager.FindAsync(loginviewmodel.UserName, loginviewmodel.Password);

                if (user != null)
                { 
                    FormsAuthentication.SetAuthCookie(user.UserName, false); 
  

                    //SignInAsync() is een helper methode
                    await SignInAsync(user, loginviewmodel.RememberMe);
                   
                    // RedirectToLocal() is een helper methode
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
                   
            }              
            return View(loginviewmodel);                
        }

               
        [Authorize(Roles = "admin")]
        public ActionResult Register()
        {
            return View();
        }

            
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var user = model.GetUser();
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Account");
                }

                          
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //Url.Action : creërt een url, maar ga daar niet naar toe.
        //ManageMessageId zie helper dan Enum
        //HasPassword(); zie helper

        [Authorize(Roles = "admin")]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                   
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                    
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                   
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

           
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var Db = new ApplicationDbContext();
           
            var users = Db.Users;
            var model = new List<EditUserViewModel>();
            foreach (var user in users)
            {
                var u = new EditUserViewModel(user);
                model.Add(u);
            }
            return View(model);
           
           
        }


        [Authorize(Roles = "admin")]
        public ActionResult Edit(string id, ManageMessageId? Message = null)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.UserName == id);
            var model = new EditUserViewModel(user);
            ViewBag.MessageId = Message;
            return View(model);
            
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Db = new ApplicationDbContext();
                var user = Db.Users.First(u => u.UserName == model.UserName);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                Db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                await Db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [Authorize(Roles = "admin")]
        public ActionResult Delete(string id = null)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.UserName == id);
            var model = new EditUserViewModel(user);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.UserName == id);
            Db.Users.Remove(user);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

             
        [Authorize(Roles = "admin")]
        public ActionResult UserRoles(string id)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.UserName == id);
            var model = new SelectUserRolesViewModel(user);            
            return View(model);
        }

              
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UserRoles(SelectUserRolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var idManager = new IdentityManager();
                var Db = new ApplicationDbContext();
                var user = Db.Users.First(u => u.UserName == model.UserName);
                idManager.ClearUserRoles(user.Id);
                foreach (var role in model.Roles)
                {
                    if (role.Selected)
                    {
                        idManager.AddUserToRole(user.Id, role.RoleName);
                    }
                }
                return RedirectToAction("Index");
            }
            return View();
        }


        #region Helpers

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        // isPersistent : indicating whether the cookie that contains the forms-authentication ticket information is persistent(blijvende data).
        //helper methode voor login
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            //the external cookie should get cleared before sign in
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        //helpermethode voor login
        //RedirectToAction lets you construct a redirect url to a specific action/controller in your application.
        //Redirect requires that you provide a full URL to redirect to.
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private bool HasPassword()
        {//IPrincipal User is een interface declareerd in Controller die we erven
            //IIdentity is een interface bij IPrincipal
            //GetUserId is een extended methode bij IIdentity
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }



        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        //example ModelState.AddModelError
        //ModelState.AddModelError("PROGRAM_ID", "Access for this program already exists.");
        //display error in the view :  @Html.ValidationMessage("PROGRAM_ID")
        #endregion
    }
}
//vergelijk
//ChbKorting.Checked ? "10% korting" : ""
//ViewBag.StatusMessage =
//                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
//                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
//                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
//                : message == ManageMessageId.Error ? "An error has occurred."
//                : "";