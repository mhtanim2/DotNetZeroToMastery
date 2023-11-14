using InventoryApiAspCore.Data;
using InventoryApiAspCore.Interfaces.BrandInterface;
using InventoryApiAspCore.Interfaces.Common;
using InventoryApiAspCore.Models.Brands;
using InventoryApiAspCore.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InventoryApiAspCore.Services.BrandServices
{
    public class BrandService : IBrandService
    {
        //private readonly CommonService<Brand> _commonService;

        //public BrandService(DataContext context)
        //{
        //    _commonService = new CommonService<Brand>(context);
        //}
        private readonly ICommonService<Brand> _commonService;

        public BrandService(ICommonService<Brand> commonService)
        {
            _commonService = commonService;
        }
        public async Task<Brand> AddAsync(Brand brand)
        {
            return await _commonService.AddAsync(brand);
        }

        public async Task<Brand> DeleteAsync(Guid id)
        {
            return await _commonService.DeleteAsync(id);
        }

        public async Task<IEnumerable<Brand>> GetAllAsync(string userEmail)
        {
            return await _commonService.GetAllAsync(userEmail);
        }

        public async Task<Brand> GetAsync(Guid id)
        {
            return await _commonService.GetAsync(id);
        }

        public async Task<bool> IfExist(Guid id)
        {
            return await _commonService.IfExist(id);
        }

        public async Task<Brand> UpdateAsync(Guid id, Brand brand)
        {
            return await _commonService.UpdateAsync(id,brand);
        }
    }
}
