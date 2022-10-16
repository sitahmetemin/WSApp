using AutoMapper;
using HtmlAgilityPack;
using System.Reflection;
using System;
using WSApp.Src.Application.DTOs;
using WSApp.Src.Application.Services.Base;
using WSApp.Src.Domain.Entities;
using WSApp.Src.Domain.Repositories;
using WSApp.Src.Domain.Repositories.Base;
using WSApp.Src.Domain.Services.Base;
using System.Security.Policy;
using static WSApp.Src.Application.Utils.Enums;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using System.Reflection.PortableExecutable;
using RestSharp;
using Method = RestSharp.Method;
using WSApp.Src.Application.Models;

namespace WSApp.Src.Application.Services
{
    public class ProductService : BaseService<Product, ProductDTO>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private SourceSiteModel _sourceSiteModel;


        public ProductService(IBaseRepositories<Product> baseRepositories, IMapper mapper, IProductRepository productRepository) : base(baseRepositories, mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        private async Task<string> CallUrl(string fullUrl)
        {
            try
            {
                var _client = new RestClient(fullUrl);
                var request = new RestRequest();
                var response = await _client.ExecuteGetAsync(request);

                return response.Content;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private HtmlNodeCollection ScrapingHtml(string htmlValue, string nodeName)
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(htmlValue);
            var computers = htmlDoc.DocumentNode.SelectNodes(nodeName);
            return computers;

        }

        private async Task GetProductsAsync()
        {
            string[] siteList = new string[]
            {
                "https://www.amazon.com.tr/s?bbn=12466440031&rh=n%3A12466439031%2Cn%3A12601898031&dc&qid=1665939997&rnid=12466440031&ref=lp_12466440031_nr_n_5",
                "https://www.n11.com/bilgisayar/dizustu-bilgisayar",
                "https://www.trendyol.com/laptop-x-c103108",
            };

            await GetProductsByAmazonAsync(siteList[0]);
            await GetProductsByN11Async(siteList[1]);
            await GetProductsByTrendyolAsync(siteList[2]);
        }

        private async Task GetProductsByTrendyolAsync(string siteUrl)
        {
            var response = await CallUrl(siteUrl);
            var nodeList = ScrapingHtml(response, "//div[data-test-id=\"product-card-name\"]");
            throw new NotImplementedException();
        }

        private async Task GetProductsByN11Async(string siteUrl)
        {
            var response = await CallUrl(siteUrl);
            var nodeList = ScrapingHtml(response, "//div[data-test-id=\"product-card-name\"]");
            throw new NotImplementedException();
        }

        private async Task GetProductsByAmazonAsync(string siteUrl)
        {
            var response = await CallUrl(siteUrl);
            var nodeList = ScrapingHtml(response, "//div[data-test-id=\"product-card-name\"]");
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateProductList()
        {
            GetProductsAsync();

            return true;
        }

        
    }
}