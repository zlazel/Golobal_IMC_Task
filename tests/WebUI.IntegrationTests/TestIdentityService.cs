﻿using Golobal_IMC_Task.Application;
using Golobal_IMC_Task.Application.Common.Models;
using Golobal_IMC_Task.Application.Products.Commands.CreateProduct;
using System;
using System.Threading.Tasks;

namespace Golobal_IMC_Task.WebUI.IntegrationTests
{
    public class TestIdentityService : IIdentityService
    {
        public Task<string> GetUserNameAsync(string userId)
        {
            return Task.FromResult("ahmed1@CA");
        }

        public Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> Login(LoginCommand command)
        {
            return Task.FromResult("ahmed1@CA");
        }
    }
}
