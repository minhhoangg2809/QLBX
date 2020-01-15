using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBX.Models.Custom
{
    public class UserCustom : User
    {
        private string _Role;

        public string Role
        {
            get
            {
                _Role = user_role == 1 ? "Quản trị" : "Nhân viên";
                return _Role;
            }
            set { _Role = value; }
        }

        private string _Born;

        public string Born
        {
            get
            {
                if (born != null)
                {
                    _Born = born.Value.ToShortDateString();
                }
                return _Born;
            }
            set { _Born = value; }
        }

        public UserCustom()
        {
            //
        }

        public static User MapUser(UserCustom userCus)
        {
            User user = new User();
            user.users_id = userCus.users_id;
            user.name = userCus.name;
            user.born = Convert.ToDateTime(userCus.Born);
            user.user_address = userCus.user_address;
            user.phone = userCus.phone;
            user.acc = userCus.acc;
            user.pass = userCus.pass;
            user.user_role = userCus.Role == "Quản trị" ? 1 : 0;
            return user;
        }

        public static UserCustom MapUserCus(User user)
        {
            UserCustom userCus = new UserCustom();
            userCus.users_id = user.users_id;
            userCus.born = user.born;
            userCus.name = user.name;
            userCus.user_address = user.user_address;
            userCus.phone = user.phone;
            userCus.acc = user.acc;
            userCus.pass = user.pass;
            userCus.user_role = user.user_role;
            return userCus;
        }
    }
}
