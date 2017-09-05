﻿using Admin.Common.Dto;
using Cashier.Service.MicroServices.Product.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cashier.Service.Tests
{
    public class Products
    {
        readonly Admin.ReadModels.Client.IProductsView adminProductView;
        readonly ProductView productView;

        public Products()
        {
            adminProductView = new FakeAdminProductView();
            productView = new ProductView(adminProductView);
        }

        [Fact]
        public void Should_retrieve_a_list_of_all_products_via_admin_client_when_view_is_created()
        {
            Assert.Equal(2, productView.GetAll().Count());
        }

        [Fact]
        public void Should_retrieve_product_when_given_a_known_product_id()
        {
            var id = productView.GetAll().First().Id;
            var p = productView.GetById(id);
            Assert.Equal(id, p.Id);
        }
    }

    public class FakeAdminProductView : Admin.ReadModels.Client.IProductsView
    {
        private readonly List<ProductDto> products = new List<ProductDto>();

        public FakeAdminProductView()
        {
            products.Add(new ProductDto
            {
                Id = Guid.NewGuid(),
                Name = "FW",
                DisplayName = "Flat White",
                Description = "Great Coffee",
                Price = 3.60m,
                Version = 1,
            });
            products.Add(new ProductDto
            {
                Id = Guid.NewGuid(),
                Name = "BB",
                DisplayName = "Banana Bread",
                Description = "Delicious, slightly toasted goodness",
                Price = 4.70m,
                Version = 1,
            });
        }

        public ProductDto GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            return products.AsEnumerable();
        }

        public void Initialise()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void UpdateLocalCache(ProductDto newValue)
        {
            throw new NotImplementedException();
        }
    }
}
