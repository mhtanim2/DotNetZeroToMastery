using AutoMapper;
using InventoryApiAspCore.Dto.Response;
using InventoryApiAspCore.Models.Brands;
using InventoryApiAspCore.Models.Catagories;
using InventoryApiAspCore.Models.Customers;
using InventoryApiAspCore.Models.Expenses;
using InventoryApiAspCore.Models.Suppliers;

namespace InventoryApiAspCore.Helper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<ExpenseType, ExpenseTypeDto>().ReverseMap();
            CreateMap<Expense, ExpenseDto>().ReverseMap();
        }
    }
}
