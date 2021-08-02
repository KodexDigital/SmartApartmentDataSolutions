using Application.Common.Models.Responses;
using Application.DataAccessObjects;
using Application.Helper;
using Application.Interfaces;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Secure.Hash.Algorithm.SDK.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public class AccountUserDAO : BaseDAO, IAccountUserDAO
    {
        private readonly ILogger _log;
        private readonly PasswordHasherHelper hasher;

        public AccountUserDAO(IDbConnection con, ILoggerFactory logger, PasswordHasherHelper hasher, 
            SecureData secureData) : base(con) 
        {
            _log = logger.CreateLogger(typeof(AccountUserDAO));
            this.hasher = hasher;
        }
        public async Task<bool> CreateAccount(AppUser user)
        {
            try
            {
                string sql = @"INSERT INTO [dbo].[AspNetUsers]
                               ([Id]
                               ,[Email]
                               ,[EmailConfirmed]
                               ,[PasswordHash]
                               ,[SecurityStamp]
                               ,[PhoneNumber]
                               ,[PhoneNumberConfirmed]
                               ,[TwoFactorEnabled]
                               ,[LockoutEndDateUtc]
                               ,[LockoutEnabled]
                               ,[AccessFailedCount]
                               ,[UserName])
                                VALUES
                               (@Id,
                               @Email,
                               @EmailConfirmed,
                               @PasswordHash,
                               @SecurityStamp,
                               @PhoneNumber,
                               @PhoneNumberConfirmed,
                               @TwoFactorEnabled,
                               @LockoutEndDateUtc,
                               @LockoutEnabled,
                               @AccessFailedCount,
                               @UserName)";
                var result = await _con.ExecuteAsync(sql, user);
                return result > 0;
            }
            catch (Exception ex)
            {
                _log.LogError($"Exception occured on {nameof(CreateAccount)} method with error message {ex}");
                throw;
            }
        }

        public async Task<IEnumerable<UserListsResponseModel>> GetAllUsers()
        {
            try
            {
                _log.LogInformation($"Fetching records through {nameof(GetAllUsers)} method at", DateTime.UtcNow);
                return await _con.QueryAsync<UserListsResponseModel>("SELECT * FROM AspNetUsers");
            }
            catch (Exception ex)
            {
                _log.LogError($"Exception occured on {nameof(GetAllUsers)} method with error message {ex}");
                return null;
            }
        }
        public async Task<object> IsUserExist(string username)
        {
            try
            {
                _log.LogInformation($"Validating user through {nameof(IsUserExist)} method at", DateTime.UtcNow);
                var query = "SELECT * FROM AspNetUsers WHERE UserName = @username";
                var data = await _con.ExecuteScalarAsync(query, new { username }, UnitOfWorkSession?.GetTransaction());
                return data;
            }
            catch (Exception ex)
            {
                _log.LogError($"Exception occured on {nameof(IsUserExist)} method with error message {ex}");
                return false;
            }
        }
        public async Task<string> HashedPassword(string username)
        {
            try
            {
                _log.LogInformation($"Getting the core value user through {nameof(HashedPassword)} method at", DateTime.UtcNow);
                var query = "SELECT [PasswordHash] FROM AspNetUsers WHERE UserName = @username";
                var data = await _con.ExecuteScalarAsync(query, new { username });
                return data.ToString();
            }
            catch (Exception ex)
            {
                _log.LogError($"Exception occured on {nameof(HashedPassword)} method with error message {ex}");
                return null;
            }
        }

        public async Task<Microsoft.AspNet.Identity.PasswordVerificationResult> Login(string username, string password)
        {
            var user = await IsUserExist(username);
            if (user != null)
            {
                var hashPassword = await HashedPassword(username);
                return hasher.VerifyPasswordHashed(hashPassword, password);
            }
            else
                return Microsoft.AspNet.Identity.PasswordVerificationResult.Failed;
        }
    }
}
