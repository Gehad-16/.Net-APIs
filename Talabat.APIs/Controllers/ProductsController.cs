using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _imapper;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;

        public ProductsController(IGenericRepository<Product> ProductRepo ,IMapper imapper , 
                                  IGenericRepository<ProductBrand> BrandRepo,
                                  IGenericRepository<ProductType> TypeRepo)
        {
            _productRepo = ProductRepo;
            _imapper = imapper;
            _brandRepo = BrandRepo;
            _typeRepo = TypeRepo;
        }

        #region Get All Products
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        //{
        //    var Products = await _productRepo.GetAllAsync();
        //    return Ok(Products);
        //}

        #endregion

        #region Get Product By ID
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Product>> GetProduct(int id)
        //{
        //    var product = await _productRepo.GetByIdAsynk(id);
        //    return Ok(product);
        //}

        #endregion

        #region Get All With Spec
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductTOReturnDTO>>> GetAllProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(productSpecParams);
            var Products = await _productRepo.GetAllWithSpecAsync(Spec);
            var MappedProducts=_imapper.Map<IEnumerable<Product> , IEnumerable<ProductTOReturnDTO>>(Products);
            return Ok(MappedProducts);
        }

        #endregion

        #region Get By ID With Spec
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTOReturnDTO>> GetProduct(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(id);
            var Products = await _productRepo.GetByIdWithSpecAsync(Spec);
            var MappedProducts = _imapper.Map<Product,ProductTOReturnDTO>(Products);
            return Ok(MappedProducts);
        }

        #endregion


        #region Get All Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAllBrands()
        {
            var Brands = await _brandRepo.GetAllAsync();
            return Ok(Brands);
        }

        #endregion

        #region Get All Types
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetAllTypes()
        {
            var Types = await _typeRepo.GetAllAsync();
            return Ok(Types);
        }

        #endregion
    }
}
