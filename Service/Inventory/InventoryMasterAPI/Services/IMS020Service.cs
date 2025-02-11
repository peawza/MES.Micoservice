using InventoryMasterAPI.Repositories;
using static InventoryMasterAPI.Models.IMS020;

namespace InventoryMasterAPI.Services
{

    public partial interface IIms020Service
    {

        Task<List<IMS020_Search_Result>>? IMS020Search(IMS020_Search_Criteria criteria);
        Task<IMS020_Insert_Result>? IMS020Insert(List<IMS020_Insert_Model> info);
        Task<IMS020_Delete_Result> IMS020Delete(IMS020_Delete_Criteria criteria);
    }



    public class IMS020Service : IIms020Service
    {
        private readonly IIMS020Repository _repository;

        public IMS020Service(IIMS020Repository repository)
        {
            _repository = repository;
        }
        public async Task<List<IMS020_Search_Result>>? IMS020Search(IMS020_Search_Criteria criteria)
        {
            try
            {
                return await _repository.IMS020Search(criteria);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IMS020_Insert_Result>? IMS020Insert(List<IMS020_Insert_Model> info)
        {
            try
            {
                return await _repository.IMS020Save(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IMS020_Delete_Result>? IMS020Delete(IMS020_Delete_Criteria criteria)
        {
            try
            {
                return await _repository.IMS020Delete(criteria);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
