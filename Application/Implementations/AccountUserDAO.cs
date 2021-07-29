using Application.Common.Models.Responses;
using Application.DataAccessObjects;
using Application.Interfaces;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public class AccountUserDAO : BaseDAO, IAccountUserDAO
    {
        private readonly ILogger _log;
        public AccountUserDAO(IDbConnection con, ILoggerFactory logger) : base(con) 
        {
            _log = logger.CreateLogger(typeof(AccountUserDAO));
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

        public async Task<IEnumerable<UserListsResponseModel>> GetAllUsers() // get based user login
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

        public async Task<object> IsUserExist(string username, string password)
        {
            try
            {
                _log.LogInformation($"Validating user through {nameof(IsUserExist)} method at", DateTime.UtcNow);
                var query = "SELECT * FROM AspNetUsers WHERE UserName = @username AND PasswordHash = @password";
                var data = await _con.ExecuteScalarAsync(query, new { username, password }, UnitOfWorkSession?.GetTransaction());
                return data;
            }
            catch (Exception ex)
            {
                _log.LogError($"Exception occured on {nameof(GetAllUsers)} method with error message {ex}");
                return false;
            }
        }
    }
}

    
     
     
     
     
     
     
     
     
    
