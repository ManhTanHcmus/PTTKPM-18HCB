using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageRoles.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseConnection")
        {
        }

        public DbSet<MenuMaster> MenuMaster { get; set; }
        public DbSet<SubMenuMaster> SubMenuMasters { get; set; }
        public DbSet<RoleMaster> RoleMasters { get; set; }
        public DbSet<Usermaster> Usermasters { get; set; }
        public DbSet<SavedMenuRoles> SavedMenuRoles { get; set; }
        public DbSet<SavedSubMenuRoles> SavedSubMenuRoles { get; set; }
        public DbSet<PasswordMaster> PasswordMaster { get; set; }
        public DbSet<SavedAssignedRoles> SavedAssignedRoles { get; set; }
		public DbSet<MonThi> MonThis { get; set; }
		public DbSet<DeThi> DeThis { get; set; }
		public DbSet<CauHoi> CauHois { get; set; }
		public DbSet<ToChucThi> ToChucThis { get; set; }


	}
}
