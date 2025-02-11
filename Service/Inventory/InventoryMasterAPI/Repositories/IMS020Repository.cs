using AutoMapper;
using InventoryMasterPostgreSQLDB;
using InventoryMasterPostgreSQLDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static InventoryMasterAPI.Models.IMS020;

namespace InventoryMasterAPI.Repositories
{
    public interface IIMS020Repository
    {
        Task<List<IMS020_Search_Result>>? IMS020Search(IMS020_Search_Criteria criteria);
        Task<IMS020_Insert_Result>? IMS020Save(List<IMS020_Insert_Model> info);
        Task<IMS020_Delete_Result> IMS020Delete(IMS020_Delete_Criteria criteria);
    }

    public class IMS020Repository : IIMS020Repository
    {
        private readonly InventoryMasterDbContext db;
        private readonly IMapper _mapper;

        public IMS020Repository(InventoryMasterDbContext db, IMapper mapper)
        {
            this.db = db;
            _mapper = mapper;
        }

        public async Task<List<IMS020_Search_Result>>? IMS020Search(IMS020_Search_Criteria criteria)
        {
            try
            {
                var query = from location in db.TmLocations
                            join area in db.TmAreas on location.AreaId equals area.AreaId
                            where
                              (string.IsNullOrEmpty(criteria.Location) || location.LocationCode.Contains(criteria.Location)) &&
                              (!criteria.Status.HasValue || location.ActiveFlag == criteria.Status) &&
                              (location.DeleteFlag == false)
                            select new
                            {
                                location,
                                area
                            };

                var res = await query.ToListAsync();
                List<(TmLocation location, TmArea area)> result = res.Select(res => (res?.location, res?.area)).ToList();
                List<IMS020_Search_Result> list = _mapper.Map<List<IMS020_Search_Result>>(result);
                return list;


            }
            catch
            {


                throw;
            }
        }

       public async Task<IMS020_Insert_Result>? IMS020Save(List<IMS020_Insert_Model> info)
         {
             try
             {
                var result = new IMS020_Insert_Result();
                if (info.Any())
                {
                    foreach (var item in info)
                    {
                        var existsLocation = await db.TmLocations
                                            .Where(p => p.LocationCode == item.LocationCode)
                                            .FirstOrDefaultAsync();

                        // Create
                        if (existsLocation == null)
                        {
                            var location = new TmLocation
                            {
                                LocationCode = item.LocationCode,
                                LocationName = item.LocationName,
                                AreaId = item.AreaId,
                                RmFlag = item.RmFlag,
                                FgFlag = item.FgFlag,
                                WipFlag = item.WipFlag,
                                ActiveFlag = item.ActiveFlag,
                                CreatedDate = DateTime.Now,
                                CreatedBy = item.CreatedBy,
                                UpdatedDate = DateTime.Now,
                                UpdatedBy = item.CreatedBy,
                                DeleteFlag = false
                            };
                            db.TmLocations.Add(location);
                            await db.SaveChangesAsync();

                            result = new IMS020_Insert_Result()
                            {
                                StatusCode = "Ok",
                                StatusName = "✅ Insert complete"
                            };
                        }
                        else
                        {
                            // Update
                            await db.TmLocations
                                   .Where(d => d.LocationId == item.LocationId)
                                   .ExecuteUpdateAsync(setters => setters
                                       .SetProperty(d => d.LocationName, item.LocationName)
                                       .SetProperty(d => d.RmFlag, item.RmFlag)
                                       .SetProperty(d => d.FgFlag, item.FgFlag)
                                       .SetProperty(d => d.WipFlag, item.WipFlag)
                                       .SetProperty(d => d.ActiveFlag, item.ActiveFlag)
                                       .SetProperty(d => d.UpdatedDate, DateTime.Now)
                                       .SetProperty(d => d.UpdatedBy, item.CreatedBy));

                            result = new IMS020_Insert_Result()
                            {
                                StatusCode = "Ok",
                                StatusName = "✅ Update complete"
                            };
                        }
                    } 
                }

                return result;
            }
             catch (Exception)
             {

                 throw;
             }
         }


        public async Task<IMS020_Delete_Result> IMS020Delete(IMS020_Delete_Criteria criteria)
        {
            try
            {
                await db.TmLocations
                           .Where(d => d.LocationId == criteria.LocationId)
                           .ExecuteUpdateAsync(setters => setters
                           .SetProperty(d => d.DeleteFlag, true)
                           .SetProperty(d => d.UpdatedDate, DateTime.Now)
                           .SetProperty(d => d.UpdatedBy, criteria.DeletedBy));

                return new IMS020_Delete_Result()
                {
                    StatusCode = "Ok",
                    StatusName = "StatusName",

                };
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
