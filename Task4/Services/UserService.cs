using Task4.Areas.Identity.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Task4.Data;

namespace Task4.Services
{
    public interface IUserService
    {
        public List<User> GetUsers();
        public void ToggleStatusById(string[] ids, ClaimsPrincipal userPrincipal, bool status);
        public void DeleteById(string[] ids, ClaimsPrincipal userPrincipal);
    }
    public class UserService : IUserService
    {
        private readonly Context db;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserService(Context db, UserManager<User> userManager, SignInManager<User> signInManager) 
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public List<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public void ToggleStatusById(string[] ids, ClaimsPrincipal userPrincipal, bool status)
        {
            var currentUserId = userPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
            foreach(var id in ids)
            {
                var user = db.Users.FirstOrDefault(x => x.Id == id);
                if (user == null) continue;
                user.Status = status;
                if (status) user.SecurityStamp = user.SecurityStamp + "asd";
                if (status && id == currentUserId) signInManager.SignOutAsync();
            }
            db.SaveChanges();
        }

        public void DeleteById(string[] ids, ClaimsPrincipal userPrincipal)
        {
            var currentUserId = userPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
            foreach (var id in ids)
            {
                var userToDelete = userManager.FindByIdAsync(id).Result;
                db.Remove(userToDelete);
                if (id == currentUserId) signInManager.SignOutAsync();
            }
            db.SaveChanges();
        }

    }
}
