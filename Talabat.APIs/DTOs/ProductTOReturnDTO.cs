﻿using Talabat.Core.Entities;

namespace Talabat.APIs.DTOs
{
    public class ProductTOReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int ProductBrandId { get; set; }
        public string productBrand { get; set; }
        public int ProductTypeId { get; set; }
        public string productType { get; set; }
    }
}
