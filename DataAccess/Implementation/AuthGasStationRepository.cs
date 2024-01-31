using Dapper;
using DataAccess.Interface;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.Implementation
{
    public class AuthGasStationRepository : IAuthGasStationRepository
    {
        private IDbConnection _db;
        private IDbTransaction _transaction;
        private readonly int _timeOut;
        private bool _disposed;
        public AuthGasStationRepository(string connectionString)
        {
            this._db = new SqlConnection(connectionString);
            _db.Open();
            _transaction = _db.BeginTransaction();
            _timeOut = 300; //second
            _disposed = false;
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _db.Dispose();
                _db.Close();
                _transaction.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }

                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }

                _disposed = true;
            }
        }

        /// <summary>
        /// handle Login with Email, Password and role
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public UserDTO Login(string Email, string Password)
        {
            try
            {
                var sql = "SELECT * FROM [dbo].[User] u WHERE u.Email = @email and u.Password = @password and u.UserType = '00001'";
                var result = _db.QueryFirstOrDefault<UserDTO>(sql, new
                {
                    @email = Email,
                    @password = Password
                }, transaction: _transaction);
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
