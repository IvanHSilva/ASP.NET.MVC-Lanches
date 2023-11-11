using Microsoft.AspNetCore.Identity;

namespace VendasLanches.Services {
    public class SeedRolerInitial : ISeedRolerInitial {

        private readonly UserManager<IdentityUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public SeedRolerInitial(UserManager<IdentityUser> user, 
            RoleManager<IdentityRole> role) {
            _user = user;
            _role = role;
        }

        public void SeedRoles() {
            if (!_role.RoleExistsAsync("Member").Result) {
                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";
                IdentityResult result = _role.CreateAsync(role).Result;
            }
            if (!_role.RoleExistsAsync("Admin").Result) {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult result = _role.CreateAsync(role).Result;
            }
        }

        public void SeedUsers() {
            
            string email = "usuario@localhost";
            string pass = "Vector86+";

            if (_user.FindByEmailAsync(email).Result == null) {
                IdentityUser user = new IdentityUser();
                user.UserName = email;
                user.Email = email;
                user.NormalizedUserName = email.ToUpper();
                user.NormalizedEmail = email.ToUpper();
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _user.CreateAsync(user, pass).Result;

                if (result.Succeeded) {
                    _user.AddToRoleAsync(user, "Member").Wait();
                }
            }

            email = "admin@localhost";
            pass = "Adm_2024";

            if (_user.FindByEmailAsync(email).Result == null) {
                IdentityUser user = new IdentityUser();
                user.UserName = email;
                user.Email = email;
                user.NormalizedUserName = email.ToUpper();
                user.NormalizedEmail = email.ToUpper();
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _user.CreateAsync(user, pass).Result;

                if (result.Succeeded) {
                    _user.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
