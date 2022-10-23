using HtmlAgilityPack;
using WSApp.Src.Application.Models;

namespace WSApp.Src.Infrastrurcture.Adapters
{
    public class WebScrapingAdapters
    {
        private List<SitePropertiesModel> _sourceSiteModel = new List<SitePropertiesModel>();
        private HtmlDocument htmlNodes;
        private string classNamePattern = "//*[contains(@class,'{0}')]";

        private string? GetInnerTextByClass(string className)
        {
            return htmlNodes?
                .DocumentNode?
                .SelectNodes(string.Format(classNamePattern, className))?
                .FirstOrDefault()?
                .InnerText;
        }

        private string? GetInnerTextByXPath(string xpath)
        {
            return htmlNodes?
                .DocumentNode?
                .SelectNodes(xpath)?
                .FirstOrDefault()?
                .InnerText;
        }

        private string? GetImageByClassName(string className)
        {
            return htmlNodes?
                .DocumentNode?
                .SelectNodes(string.Format(classNamePattern, className))?
                .FirstOrDefault()?
                .Attributes?
                .FirstOrDefault(q => q.Name == "src")?
                .Value;
        }

        private string? GetAttributeValueByXPath(string attributeName, string xpath)
        {
            return htmlNodes?
                .DocumentNode?
                .SelectNodes(xpath)?
                .FirstOrDefault()?
                .Attributes?
                .FirstOrDefault(q => q.Name == attributeName)?
                .Value;
        }

        private async Task GetProductsByTrendyolAsync()
        {
            const string baseUrl = "https://www.trendyol.com";
            const string urlPath = "/laptop-x-c103108";

            try
            {
                HtmlWeb web = new HtmlWeb();
                var listUrl = string.Concat(baseUrl, urlPath);
                var mainHtmlDoc = await web.LoadFromWebAsync(listUrl);
                string linkIsEmpty = "Link yok!!";

                var selectedAElement = mainHtmlDoc
                    .DocumentNode
                    .SelectNodes(string.Format(classNamePattern, "p-card-chldrn-cntnr card-border"));

                if (selectedAElement is null || selectedAElement.Count == 0)
                    return;

                var prodModel = new List<SitePropertiesModel>();

                Parallel.ForEach(selectedAElement, item =>
                {
                    var linkNode = item.ChildNodes[0];
                    string productLink = string.Concat(baseUrl, linkNode.GetAttributeValue("href", linkIsEmpty));
                    if (linkIsEmpty == productLink)
                        return;

                    HtmlWeb subWeb = new HtmlWeb();
                    htmlNodes = subWeb.Load(productLink);

                    var brand = GetInnerTextByClass("detail-name");
                    string brandName = string.Empty;
                    string modelName = string.Empty;
                    string modelNo = string.Empty;
                    if (!string.IsNullOrEmpty(brand))
                    {
                        var brandInfo = brand.Split(' ');
                        brandName = brandInfo[0];
                        modelName = brandInfo[1];
                        modelNo = brandInfo[2];

                    }

                    var model = new SitePropertiesModel
                    {
                        ProductUrl = productLink,
                        Site = "Trendyol",
                        Brand = brandName,
                        ModelName = modelName,
                        ModelNo = modelNo,
                        DisplaySize = GetInnerTextByXPath("//*[@id=\"product-detail-app\"]/div/section/div/ul/li[6]/span[2]/b"),
                        ImageUrl = GetImageByClassName("detail-section-img"),
                        OS = GetInnerTextByXPath("//*[@id=\"product-detail-app\"]/div/section/div/ul/li[3]/span[2]/b"),
                        Prices = GetInnerTextByClass("prc-dsc"),
                        ProcessorBrand = GetInnerTextByXPath("//*[@id=\"product-detail-app\"]/div/section/div/ul/li[1]/span[2]/b"),
                        ProcessorType = GetInnerTextByXPath("//*[@id=\"product-detail-app\"]/div/section/div/ul/li[14]/span[2]/b"),
                        ProcessorVersion = GetInnerTextByXPath("//*[@id=\"product-detail-app\"]/div/section/div/ul/li[31]/span[2]/b"),
                        Ram = GetInnerTextByXPath("//*[@id=\"product-detail-app\"]/div/section/div/ul/li[5]/span[2]/b"),
                        Score = GetInnerTextByXPath("//*[@id=\"product-detail-app\"]/div/div[2]/div[1]/div[2]/div[2]/div[2]/div/div/div[3]/div/div[1]/div[3]/div/span/span"),
                        StorageSize = GetInnerTextByXPath("//*[@id=\"product-detail-app\"]/div/section/div/ul/li[2]/span[2]/b"),
                        StorageType = GetInnerTextByXPath("//*[@id=\"product-detail-app\"]/div/section/div/ul/li[2]/span[2]/b")
                    };
                    prodModel.Add(model);
                });

                _sourceSiteModel.AddRange(prodModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task GetProductsByN11Async()
        {
            const string baseUrl = "https://www.n11.com";
            const string urlPath = "/bilgisayar/dizustu-bilgisayar";

            try
            {
                HtmlWeb web = new HtmlWeb();
                var listUrl = string.Concat(baseUrl, urlPath);
                var mainHtmlDoc = await web.LoadFromWebAsync(listUrl);
                string linkIsEmpty = "Link yok!!";

                var selectedAElement = mainHtmlDoc
                    .DocumentNode
                    .SelectNodes(string.Format(classNamePattern, "plink"));

                if (selectedAElement is null || selectedAElement.Count == 0)
                    return;

                var prodModel = new List<SitePropertiesModel>();

                Parallel.ForEach(selectedAElement, item =>
                {
                    string productLink = item.GetAttributeValue("href", linkIsEmpty);
                    if (linkIsEmpty == productLink)
                        return;

                    HtmlWeb subWeb = new HtmlWeb();
                    htmlNodes = subWeb.Load(productLink);
                    var brand = GetInnerTextByClass("unf-p-summary-title");
                    string brandName = string.Empty;
                    string modelName = string.Empty;
                    string modelNo = string.Empty;
                    if (!string.IsNullOrEmpty(brand))
                    {
                        var brandInfo = brand.Split(' ');
                        brandName = brandInfo[0];
                        modelName = brandInfo[1];
                        modelNo = brandInfo[2];
                    }

                    var model = new SitePropertiesModel
                    {
                        ProductUrl = productLink,
                        Site = "N11",
                        Brand = brandName,
                        ModelName = modelName,
                        ModelNo = modelNo,
                        DisplaySize = GetInnerTextByXPath("//*[@id=\"unf-prop\"]/div/ul/li[16]/p[2]"),
                        ImageUrl = GetImageByClassName("lazy unf-p-img"),
                        OS = GetInnerTextByXPath("//*[@id=\"unf-prop\"]/div/ul/li[18]/p[2]"),
                        Prices = GetInnerTextByXPath("//*[@id=\"unfSummary\"]/div/div[2]/div[1]"),
                        ProcessorBrand = GetInnerTextByXPath("//*[@id=\"unf-prop\"]/div/ul/li[11]/p[2]"),
                        ProcessorType = GetInnerTextByXPath("//*[@id=\"unf-prop\"]/div/ul/li[11]/p[2]"),
                        ProcessorVersion = GetInnerTextByXPath("//*[@id=\"unf-prop\"]/div/ul/li[11]/p[2]"),
                        Ram = GetInnerTextByXPath("//*[@id=\"unf-prop\"]/div/ul/li[9]/p[2]"),
                        Score = GetInnerTextByClass("ratingScore r100"),
                        StorageSize = GetInnerTextByXPath("//*[@id=\"unf-prop\"]/div/ul/li[12]/p[2]"),
                        StorageType = GetInnerTextByXPath("//*[@id=\"unf-prop\"]/div/ul/li[6]/p[2]")
                    };
                    prodModel.Add(model);
                });

                _sourceSiteModel.AddRange(prodModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task GetProductsByTeknosaAsync()
        {
            const string baseUrl = "https://www.teknosa.com";
            const string urlPath = "/laptop-notebook-c-116004";

            try
            {
                HtmlWeb web = new HtmlWeb() { UseCookies = true };
                var listUrl = string.Concat(baseUrl, urlPath);
                web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.47";
                web.PreRequest += request =>
                {
                    request.CookieContainer = new System.Net.CookieContainer();
                    return true;
                };

                var mainHtmlDoc = await web.LoadFromWebAsync(listUrl);
                string linkIsEmpty = "Link yok!!";

                var selectedAElement = mainHtmlDoc
                    .DocumentNode
                    .SelectNodes(string.Format(classNamePattern, "prd-link"));

                if (selectedAElement is null || selectedAElement.Count == 0)
                    return;

                var prodModel = new List<SitePropertiesModel>();

                Parallel.ForEach(selectedAElement, item =>
                {
                    string productLink = string.Concat(baseUrl, item.GetAttributeValue("href", linkIsEmpty));
                    if (linkIsEmpty == productLink)
                        return;
                    try
                    {
                        HtmlWeb subWeb = new HtmlWeb();
                        subWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.47";
                        subWeb.PreRequest += request =>
                        {
                            request.CookieContainer = new System.Net.CookieContainer();
                            return true;
                        };

                        htmlNodes = subWeb.Load(productLink);
                        var brand = GetInnerTextByXPath("//*[@id=\"pdp-description\"]/div[2]/div[1]/h2");
                        string brandName = string.Empty;
                        string modelName = string.Empty;
                        string modelNo = string.Empty;
                        if (!string.IsNullOrEmpty(brand))
                        {
                            var brandInfo = brand.Split(' ');
                            brandName = brandInfo[0];
                            modelName = brandInfo[1];
                            modelNo = brandInfo[2];
                        }

                        var model = new SitePropertiesModel
                        {
                            ProductUrl = productLink,
                            Site = "TeknoSa",
                            Brand = brandName,
                            ModelName = modelName,
                            ModelNo = modelNo,
                            DisplaySize = GetInnerTextByXPath("//*[@id=\"pdp-technical\"]/div/div[1]/div/table[1]/tbody/tr[2]/td[4]"),
                            ImageUrl = GetAttributeValueByXPath("alt", GetInnerTextByXPath("//*[@id=\"pdp-main\"]/div[2]/div[1]/h1")),
                            OS = GetInnerTextByClass("//*[@id=\"pdp-technical\"]/div/div[1]/div/table[5]/tbody/tr[2]/td[4]"),
                            Prices = GetInnerTextByClass("prc prc-last"),
                            ProcessorBrand = GetInnerTextByXPath("//*[@id=\"pdp-technical\"]/div/div[1]/div/table[4]/tbody/tr[2]/td[4]"),
                            ProcessorType = GetInnerTextByXPath("//*[@id=\"pdp-technical\"]/div/div[1]/div/table[4]/tbody/tr[2]/td[4]"),
                            ProcessorVersion = GetInnerTextByXPath("//*[@id=\"pdp-technical\"]/div/div[1]/div/table[5]/tbody/tr[2]/td[3]"),
                            Ram = GetInnerTextByXPath("//*[@id=\"pdp-technical\"]/div/div[1]/div/table[6]/tbody/tr[2]/td[2]"),
                            Score = GetInnerTextByClass("bv_numReviews_text"),
                            StorageSize = GetInnerTextByXPath("//*[@id=\"pdp-technical\"]/div/div[1]/div/table[1]/tbody/tr[2]/td[1]"),
                            StorageType = GetInnerTextByXPath("//*[@id=\"pdp-technical\"]/div/div[1]/div/table[1]/tbody/tr[2]/td[1]")
                        };
                        prodModel.Add(model);
                    }
                    catch (Exception)
                    {
                        return;
                    }
                    
                });

                _sourceSiteModel.AddRange(prodModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task GetProductsByAmazonAsync()
        {
            const string baseUrl = "https://www.amazon.com.tr";
            const string urlPath = "/s?bbn=12601898031&rh=n%3A12466439031%2Cn%3A12601898031%2Cn%3A30117740031&dc&qid=1666445388&rnid=12601898031&ref=lp_12601898031_nr_n_1";

            try
            {
                HtmlWeb web = new HtmlWeb() { UseCookies = true };
                var listUrl = string.Concat(baseUrl, urlPath);
                web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.47";
                web.PreRequest += request =>
                {
                    request.CookieContainer = new System.Net.CookieContainer();
                    return true;
                };

                var mainHtmlDoc = await web.LoadFromWebAsync(listUrl);
                string linkIsEmpty = "Link yok!!";

                var selectedAElement = mainHtmlDoc
                    .DocumentNode
                    .SelectNodes(string.Format(classNamePattern, "a-link-normal octopus-pc-item-link"));

                if (selectedAElement is null || selectedAElement.Count == 0)
                    return;

                var prodModel = new List<SitePropertiesModel>();

                Parallel.ForEach(selectedAElement, item =>
                {
                    string productLink = string.Concat(baseUrl, item.GetAttributeValue("href", linkIsEmpty));
                    if (linkIsEmpty == productLink)
                        return;
                    try
                    {
                        HtmlWeb subWeb = new HtmlWeb();
                        subWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.47";
                        subWeb.PreRequest += request =>
                        {
                            request.CookieContainer = new System.Net.CookieContainer();
                            return true;
                        };

                        htmlNodes = subWeb.Load(productLink);
                        var brand = GetInnerTextByXPath("//*[@id=\"productTitle\"]");
                        string brandName = string.Empty;
                        string modelName = string.Empty;
                        string modelNo = string.Empty;
                        if (!string.IsNullOrEmpty(brand))
                        {
                            var brandInfo = brand.Split(' ');
                            brandName = brandInfo[0];
                            modelName = brandInfo[1];
                            modelNo = brandInfo[2];
                        }


                        var model = new SitePropertiesModel
                        {
                            ProductUrl = productLink,
                            Site = "Amazon",
                            Brand = brandName,
                            ModelName = modelName,
                            ModelNo = modelNo,
                            DisplaySize = GetInnerTextByXPath("//*[@id=\"productDetails_techSpec_section_1\"]/tbody/tr[9]/td"),
                            ImageUrl = GetAttributeValueByXPath("id", "landingImage"),
                            OS = GetInnerTextByXPath("//*[@id=\"productDetails_techSpec_section_1\"]/tbody/tr[32]/td"),
                            Prices = GetInnerTextByXPath("//*[@id=\"corePrice_feature_div\"]/div/span/span[1]"),
                            ProcessorBrand = GetInnerTextByXPath("//*[@id=\"productDetails_techSpec_section_1\"]/tbody/tr[12]/td"),
                            ProcessorType = GetInnerTextByXPath("//*[@id=\"productDetails_techSpec_section_1\"]/tbody/tr[13]/td"),
                            ProcessorVersion = GetInnerTextByXPath("//*[@id=\"productDetails_techSpec_section_1\"]/tbody/tr[13]/td"),
                            Ram = GetInnerTextByXPath("//*[@id=\"productDetails_techSpec_section_1\"]/tbody/tr[16]/td"),
                            Score = GetInnerTextByXPath("//*[@id=\"a-popover-content-2\"]/div/div/div/div[1]/span"),
                            StorageSize = GetInnerTextByXPath("//*[@id=\"productDetails_techSpec_section_1\"]/tbody/tr[21]/td"),
                            StorageType = GetInnerTextByXPath("//*[@id=\"productDetails_techSpec_section_1\"]/tbody/tr[22]/td")
                        };
                        prodModel.Add(model);
                    }
                    catch (Exception)
                    {
                        return;
                    }

                });

                _sourceSiteModel.AddRange(prodModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task GetProductsByHepsiburadaAsync()
        {
            const string baseUrl = "https://www.hepsiburada.com";
            const string urlPath = "/laptop-notebook-dizustu-bilgisayarlar-c-98";

            try
            {
                HtmlWeb web = new HtmlWeb() { UseCookies = true };
                var listUrl = string.Concat(baseUrl, urlPath);
                web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.47";
                web.PreRequest += request =>
                {
                    request.CookieContainer = new System.Net.CookieContainer();
                    return true;
                };

                var mainHtmlDoc = await web.LoadFromWebAsync(listUrl);
                string linkIsEmpty = "Link yok!!";

                var selectedAElement = mainHtmlDoc
                    .DocumentNode
                    .SelectNodes(string.Format(classNamePattern, "moria-ProductCard-gyqBb cJYqn suy6gqxycpt backToTopVisible"));

                if (selectedAElement is null || selectedAElement.Count == 0)
                    return;

                var prodModel = new List<SitePropertiesModel>();

                Parallel.ForEach(selectedAElement, item =>
                {
                    string productLink = string.Concat(baseUrl, item.GetAttributeValue("href", linkIsEmpty));
                    if (linkIsEmpty == productLink)
                        return;
                    try
                    {
                        HtmlWeb subWeb = new HtmlWeb();
                        subWeb.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.47";
                        subWeb.PreRequest += request =>
                        {
                            request.CookieContainer = new System.Net.CookieContainer();
                            return true;
                        };

                        htmlNodes = subWeb.Load(productLink);
                        var brand = GetInnerTextByXPath("//*[@id=\"tabProductDesc\"]/h2");
                        string brandName = string.Empty;
                        string modelName = string.Empty;
                        string modelNo = string.Empty;
                        if (!string.IsNullOrEmpty(brand))
                        {
                            var brandInfo = brand.Split(' ');
                            brandName = brandInfo[0];
                            modelName = brandInfo[1];
                            modelNo = brandInfo[2];
                        }


                        var model = new SitePropertiesModel
                        {
                            ProductUrl = productLink,
                            Site = "HepsiBurada",
                            Brand = brandName,
                            ModelName = modelName,
                            ModelNo = modelNo,
                            DisplaySize = GetInnerTextByXPath("//*[@id=\"productTechSpecContainer\"]/table[2]/tbody/tr[5]/td/a"),
                            ImageUrl = GetInnerTextByXPath("//*[@id=\"productDetailsCarousel\"]/div[1]/div/div[1]/a/picture/img"),
                            OS = GetInnerTextByXPath("//*[@id=\"productTechSpecContainer\"]/table[2]/tbody/tr[18]/td/a"),
                            Prices = GetInnerTextByXPath("//*[@id=\"offering-price\"]/span[1]"),
                            ProcessorBrand = GetInnerTextByXPath("///*[@id=\"productTechSpecContainer\"]/table[2]/tbody/tr[16]/td/a"),
                            ProcessorType = GetInnerTextByXPath("//*[@id=\"productTechSpecContainer\"]/table[2]/tbody/tr[17]/td/a"),
                            ProcessorVersion = GetInnerTextByXPath("//*[@id=\"productTechSpecContainer\"]/table[2]/tbody/tr[15]/td/a"),
                            Ram = GetInnerTextByXPath("//*[@id=\"productTechSpecContainer\"]/table[2]/tbody/tr[25]/td/a"),
                            Score = GetInnerTextByXPath("//*[@id=\"hermes-voltran-comments\"]/div[2]/div[2]/div[2]/div[1]/span[1]"),
                            StorageSize = GetInnerTextByXPath("//*[@id=\"productTechSpecContainer\"]/table[2]/tbody/tr[28]/td/a"),
                            StorageType = GetInnerTextByXPath("//*[@id=\"productTechSpecContainer\"]/table[2]/tbody/tr[28]/td/a")
                        };
                        prodModel.Add(model);
                    }
                    catch (Exception)
                    {
                        return;
                    }

                });

                _sourceSiteModel.AddRange(prodModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SitePropertiesModel>> GetScrapingData()
        {
            await GetProductsByTrendyolAsync();
            await GetProductsByN11Async();
            await GetProductsByTeknosaAsync();
            await GetProductsByAmazonAsync();
            await GetProductsByHepsiburadaAsync();

            return _sourceSiteModel;
        }
    }
}
