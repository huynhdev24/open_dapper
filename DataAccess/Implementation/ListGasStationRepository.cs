using Dapper;
using DataAccess.Interface;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class ListGasStationRepository : IListGasStationRepository
    {
        private IDbConnection _db;
        private IDbTransaction _transaction;
        private readonly int _timeOut;
        private bool _disposed;
        public ListGasStationRepository(string connectionString)
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
        /// Get a list District was ordered by District Name
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MDistrict>> GetListDistrict()
        {
            try
            {
                var sql = "SELECT * FROM [dbo].[M_District] district ORDER BY district.DistrictId";
                var result = _db.Query<MDistrict>(sql, transaction: _transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get a list MType where TypeType = 3
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MType>> GetType()
        {
            try
            {
                var sql = "SELECT * FROM [dbo].[GasType] gastype";
                var result = _db.Query<MType>(sql, transaction: _transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get list Rating
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MType>> GetRating()
        {
            try
            {
                var sql = "SELECT * FROM [dbo].[RatingType] rating";
                var result = _db.Query<MType>(sql, transaction: _transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get GasType by ID
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MType>> GetGasTypeById(string typeId)
        {
            try
            {
                var sql = "SELECT * FROM [dbo].[M_Type] mtype WHERE mtype.TypeId = @typeId";
                var result = _db.Query<MType>(sql, new
                {
                    @typeId = typeId
                },
                transaction: _transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get list GasStation
        /// </summary>
        /// <param name="searchGasName"></param>
        /// <param name="searchDistrict"></param>
        /// <param name="StringListGasType"></param>
        public async Task<IEnumerable<GasStationViewModel>> getGasStation(string searchGasName, string searchDistrict, string StringListGasType)
        {
            try
            {
                string[] gasTypes = StringListGasType.Split(',');
                
                // create a table has a column with field GasType INT
                var sql = $"DECLARE @tempTable TABLE (GasType INT);" +
                          $"INSERT INTO @tempTable (GasType)" +
                          $"SELECT value FROM STRING_SPLIT(@temp, ',') " +
                          $"SELECT gas.GasStationId, gas.GasStationName, gas.Address, gas.OpeningTime, gas.District, district.DistrictName, gas.Longitude, gas.Latitude, gas.Rating, STRING_AGG(gtype.TypeText,', ') AS GasTypes  " +
                          $"FROM [dbo].[GasStation] gas " +
                          $"LEFT JOIN [dbo].[M_District] district ON gas.District = district.DistrictId " +
                          $"LEFT JOIN [dbo].[GasStationGasType] gastype ON gas.GasStationId = gastype.GasStationId " +
                          $"LEFT JOIN [dbo].[GasType] gtype ON gtype.TypeId = gastype.GasType " +
                          $"WHERE (gas.DeletedBy IS NULL AND gas.DeletedAt IS NULL) ";

                if (searchGasName != null)
                {
                    // The percent sign % represents zero, one, or multiple characters
                    // The underscore sign _ represents one, single character
                    sql += " AND gas.GasStationName like N'%'+TRIM(@searchGasName)+'%'";
                }
                if (searchDistrict != null)
                {
                    sql += " AND gas.District = @searchDistrict";
                }
                if (StringListGasType != null && StringListGasType != "")
                {
                    sql += $" AND gas.GasStationId IN (SELECT _gastype.GasStationId " +
                           $"FROM [dbo].[GasStationGasType] _gastype " +
                           $"WHERE _gastype.GasType IN (SELECT GasType " +
                           $"FROM @tempTable)) ";
                }
                
                sql +=  $" GROUP BY gas.GasStationId, gas.GasStationName, gas.Address, gas.OpeningTime, gas.District, district.DistrictName, gas.Longitude, gas.Latitude, gas.Rating " +
                        $" ORDER BY gas.GasStationName";

                var result = _db.Query<GasStationViewModel>(sql,
                new
                {
                    @searchGasName = searchGasName,
                    @searchDistrict = searchDistrict,
                    @temp = string.Join(',', gasTypes)
                }, transaction: _transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get District by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<MDistrict> getMDistrictById(long Id)
        {
            try
            {
                var sql = "SELECT * FROM [dbo].[MDistrict] mdistrict WHERE mdistrict.DistrictId = @Id";
                var result = _db.QueryFirstOrDefault<MDistrict>(sql, new
                {
                    @Id = Id
                }, transaction: _transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get GasStaion by name
        /// </summary>
        /// <param name="gasStationName"></param>
        /// <returns></returns>
        public GasStation getGasStationByName(string gasStationName)
        {
            try
            {
                //var searchGasName = gasStationName.Replace("'", "''");
                var sql = $"SELECT * FROM [dbo].[GasStation] gas " +
                //$"WHERE g.GasStationName = N'@gasStationName'";
                $"WHERE gas.GasStationName = N'{gasStationName}'";
                var result = _db.QueryFirstOrDefault<GasStation>(sql, new
                {
                    //@gasStationName = gasStationName
                }, transaction: _transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get list GasType is not null
        /// </summary>
        /// <returns></returns>
        public async Task<int> getListGasTypeIsNotNull()
        {
            try
            {
                var sql = "SELECT COUNT(*) FROM [dbo].[GasType] gtype WHERE gtype.TypeId IS NOT NULL AND gtype.TypeText IS NOT NULL AND gtype.TypeType IS NOT NULL AND gtype.TypeCode IS NOT NULL";
                var result = _db.Query(sql, transaction: _transaction);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get TypeText from GasType
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public async Task<string> getTypeTextFromMType(string typeCode)
        {
            try
            {
                var sql = "SELECT TypeText FROM [dbo].[GasType] gtype WHERE gtype.TypeId = @typeCode";
                //var sql = "SELECT g.*, gt.* FROM GasStation g INNER JOIN GasStationGasType gt ON g.GasStationId = gt.GasStationId";
                var result = _db.QueryFirstOrDefault<string>(sql, new
                {
                    @typeCode = typeCode
                }, transaction: _transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get District by ID
        /// </summary>
        /// <param name="MDistrictId"></param>
        /// <returns></returns>
        public async Task<MDistrict> GetDistrictById(long MDistrictId)
        {
            try
            {
                var sql = "SELECT mdistrict.DistrictId, mdistrict.DistrictName FROM [dbo].[M_District] mdistrict WHERE mdistrict.DistrictId = @mdistrict";
                var result = _db.QueryFirstOrDefault<MDistrict>(sql, new
                {
                    @mdistrict = MDistrictId
                }, transaction: _transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get the GasStation lastest
        /// </summary>
        /// <returns></returns>
        public async Task<GasStation> GetGasStationLast()
        {
            try
            {
                var sql = "SELECT TOP 1 * FROM GasStation ORDER BY GasStationId DESC";
                var result = _db.QueryFirstOrDefault<GasStation>(sql, transaction: _transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
		/// delete a GasStation by ID
		/// </summary>
		/// <param name="gasStationId"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
        public async Task<int> deleteGasStation(long gasStationId, long userId)
        {
            try
            {
                var sql = $"UPDATE GasStation SET DeletedAt = GETDATE(), DeletedBy = @userId  WHERE GasStationId = @gasStationId";
                var result = _db.Execute(sql, new
                {
                    @gasStationId = gasStationId,
                    @userId = userId
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
