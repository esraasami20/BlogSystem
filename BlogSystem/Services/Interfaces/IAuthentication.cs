using BlogSystem.DTO;
using BlogSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.Services.Interfaces
{
    public interface IAuthentication
    {
        public Task<ResponseAuth> RegisterVistorAsync(RegisterModel model);
        public Task<ResponseAuth> RegisterModeratorAsync(RegisterModel model);
        public Task<ResponseAuth> RegisterAdminAsync(RegisterModel model);
        public Task<ResponseAuth> LoginAsync(LoginModel model);
        public Task<ResponseAuth> LoginWithFacebookAsync(string accessToken);
    }
}

