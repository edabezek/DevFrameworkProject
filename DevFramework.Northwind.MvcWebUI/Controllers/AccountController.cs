using DevFramework.Core.CrossCuttingConcerns.Security.Web;
using DevFramework.Northwind.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevFramework.Northwind.MvcWebUI.Controllers
{
    //public class AccountController : Controller//burada bir login operasyonu oluşturup,kontrolleri yapacağız.
    //{
    //    //veritabanı olmadan gönderim 
    //    // GET: Account/Login
    //    public string Login()
    //    {
    //        AuthenticationHelper.CreateAuthCookie(//normalde db'den kontrol edip kullanıcı varsa yazmamız lazım 
    //            new Guid(),
    //            "edb",
    //            "eda_bezek@hotmail.com",
    //            DateTime.Now.AddDays(15),
    //            new[] {"Student"},//{"Admin"} olmalı
    //            false,
    //            "eda",
    //            "bezek" 
    //            );
    //        return "User is authenticated";
    //    }
    //}
    public class AccountController : Controller //db'den çekim
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        //// GET: Account/Login?username=engin&password=12345
        ////product/index'e giremez enginin student rolu yok
        //public string Login(string userName,string password) 
        //{
        //    var user = _userService.GetByUserNameAndPassword(userName,password);
        //    if (user != null)//böyle bir kullanıcı var ise
        //    {
        //        AuthenticationHelper.CreateAuthCookie(
        //        new Guid(),
        //        user.UserName,
        //        user.Email,
        //        DateTime.Now.AddDays(15),
        //        new[] { "Student" },
        //        false,
        //        user.FirstName,
        //        user.LastName
        //        );
        //        return "User is Authenticated!";
        //    }
        //    return "User is not Authenticated";//yoksa
        //}


        // GET: Account/Login?username=engin&password=12345
        //product/index'e girer enginin student rolu var
        public string Login(string userName, string password)
        {
            var user = _userService.GetByUserNameAndPassword(userName, password);
            if (user != null)//böyle bir kullanıcı var ise
            {
                AuthenticationHelper.CreateAuthCookie(
                new Guid(),
                user.UserName,
                user.Email,
                DateTime.Now.AddDays(15),
                _userService.GetUserRoles(user).Select(u=>u.RoleName).ToArray(),//join işlemi sonucu elle yazmamıza gerek olmayacak.
                false,
                user.FirstName,
                user.LastName
                );
                return "User is Authenticated!";
            }
            return "User is not Authenticated";//yoksa
        }
    }
}