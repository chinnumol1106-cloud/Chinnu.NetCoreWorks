using imageupload.Models;
using Microsoft.EntityFrameworkCore;

namespace imageupload.Requestobject.Auth
{
    public class LoginRequest
    {
       public string Email {  get; set; }
        public string Password { get; set; }


    }
}
