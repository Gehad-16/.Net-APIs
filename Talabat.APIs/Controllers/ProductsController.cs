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

        public ProductsController(IGenericRepository<Product> ProductRepo ,IMapper imapper)
        {
            _productRepo = ProductRepo;
            _imapper = imapper;
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
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var Spec = new ProductWithBrandAndTypeSpecifications();
            var Products = await _productRepo.GetAllWithSpecAsync(Spec);
            var MappedProducts=_imapper.Map<IEnumerable<Product> , IEnumerable<ProductTOReturnDTO>>(Products);
            return Ok(MappedProducts);
        }

        #endregion

        #region Get By ID With Spec
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(id);
            var Products = await _productRepo.GetByIdWithSpecAsync(Spec);
            var MappedProducts = _imapper.Map<Product,ProductTOReturnDTO>(Products);
            return Ok(MappedProducts);
        }

        #endregion

    }
}
