using Dapper;
using DataAccess.Interface;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class AddGasStationRepository : IAddGasStationRepository
    {
        private IDbConnection _db;
        private IDbTransaction _transaction;
        private readonly int _timeOut;
        private bool _disposed;
        public AddGasStationRepository(string connectionString)
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
		/// add a new GasStation
		/// </summary>
		/// <param name="gasStation"></param>
		/// <returns></returns>
        public async Task<int> addGasStation(GasStation gasStation)
        {
            try
            {
                var sql = $"INSERT INTO [dbo].[GasStation] (GasStationName, Address, District, OpeningTime, Longitude, Latitude, Rating, InsertedAt, InsertedBy, UpdatedAt, UpdatedBy,DeletedAt,DeletedBy) " +
                    $"VALUES(@GasStationName, @Address, @District, @OpeningTime, @Longitude, @Latitude, @Rating, @InsertedAt, @InsertedBy, @UpdatedAt, @UpdatedBy, @DeletedAt, @DeletedBy)";
                var result = _db.Execute(sql, new
                {
                    GasStationName = gasStation.GasStationName,
                    Address = gasStation.Address,
                    District = gasStation.District,
                    OpeningTime = gasStation.OpeningTime,
                    Longitude = gasStation.Longitude,
                    Latitude = gasStation.Latitude,
                    Rating = gasStation.Rating,
                    InsertedAt = gasStation.InsertedAt,
                    InsertedBy = gasStation.InsertedBy,
                    UpdatedAt = gasStation.UpdatedAt,
                    UpdatedBy = gasStation.UpdatedBy,
                    DeletedAt = gasStation.DeletedAt,
                    DeletedBy = gasStation.DeletedBy
                }, transaction: _transaction);
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// when add a new GasStation then add a new GasStationGasType
        /// </summary>
        /// <param name="gasStationGasType"></param>
        /// <returns></returns>
        public async Task<int> addGasStationGasType(GasStationGasType gasStationGasType)
        {
            try
            {
                var sql = $"INSERT INTO GasStationGasType (GasStationId, GasType)" +
                    $"VALUES(@GasStationId, @GasType)";
                var result = _db.Execute(sql, new
                {
                    @GasStationId = gasStationGasType.GasStationId,
                    @GasType = gasStationGasType.GasType,
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
