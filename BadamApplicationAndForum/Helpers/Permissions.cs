using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Helpers
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
        {
            $"ایجاد {module}",
            $"مشاهده {module}",
            $"ویرایش {module}",
            $"حذف {module}",
        };
        }
        public static class Users
        {
            public const string View = "مشاهده کاربران";
            public const string Create = "ایجاد کاربران";
            public const string Edit = "ویرایش کاربران";
            public const string Delete = "حذف کاربران";
        }
        public static class Feeds
        {
            public const string View = "مشاهده اخبار";
            public const string Create = "ایجاد اخبار";
            public const string Edit = "ویرایش اخبار";
            public const string Delete = "حذف اخبار";

        }
        public static class Banners
        {
            public const string View = "مشاهده بنر";
            public const string Create = "ایجاد بنر";
            public const string Edit = "ویرایش بنر";
            public const string Delete = "حذف بنر";

        }

        public static class Alarms
        {
            public const string View = "مشاهده اعلان";
            public const string Create = "ایجاد اعلان";
            public const string Edit = "ویرایش اعلان";
            public const string Delete = "حذف اعلان";

        }
        public static class Personels
        {
            public const string View = "مشاهده پرسنل";
            public const string Create = "ایجاد پرسنل";
            public const string Edit = "ویرایش پرسنل";
            public const string Delete = "حذف پرسنل";

        }

    }
}
