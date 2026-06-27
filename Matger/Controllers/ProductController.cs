using AutoMapper;
using Matger.Core;
using Matger.Core.Entities;
using Matger.Core.Repository;
using Matger.Core.Specifications;
using Matger.DTOs;
using Matger.Errors;
using Matger.Helpers;
using Matger.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matger.Controllers
{
  
    public class ProductController : APIBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
       
        public ProductController(IUnitOfWork unitOfWork ,IMapper autoMapper )
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            
        }   

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IReadOnlyList<ProductToReturnDto>), 200)]      //to improve swagger duc if success
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]//to improve swagger duc if notfound
        public async Task<ActionResult<IReadOnlyList<Pagination<ProductToReturnDto>>>> GetProducts([FromQuery]ProductSpecPrams prams)
        {
            var spec = new ProductWithrBrandandTypeSpecifications(prams);
            var products= await  _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            var mappedProducts = _autoMapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var SpecCount = new ProductWithFiltrationForCountAsync(prams);
            var count = await _unitOfWork.Repository<Product>().CountWithSpecAsync(SpecCount);
            var returnedProducts = new Pagination<ProductToReturnDto>()
            {
                PageIndex = prams.PageIndex,
                PageSize = prams.PageSize,
                Data = mappedProducts,
                Count= count
            };
            
            return Ok(returnedProducts);
        }

        [HttpGet("{Id}")]
        [Authorize]
        [ProducesResponseType(typeof(ProductToReturnDto), 200)]                   //to improve swagger duc if success
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]//to improve swagger duc if notfound
        public async Task<ActionResult<Product>> GetProductById(int Id)
        {
            var spec = new ProductWithrBrandandTypeSpecifications(Id);
            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(spec);
            if (product == null)  return NotFound(new ApiResponse(404));
            
            var mappedProduct = _autoMapper.Map<Product, ProductToReturnDto>(product);

            return Ok(mappedProduct);
        }




        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductTypes>>> GetTypes()
        {
            var types = await _unitOfWork.Repository<ProductTypes>().GetAllAsync();
            return Ok(types);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands);
        }
    }
}
