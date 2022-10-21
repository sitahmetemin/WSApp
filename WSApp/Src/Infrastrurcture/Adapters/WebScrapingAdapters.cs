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

                var prodModel = new List<SitePropertiesModel>();

                Parallel.ForEach(selectedAElement, item =>
                {
                    var linkNode = item.ChildNodes[0];
                    string productLink = string.Concat(baseUrl, linkNode.GetAttributeValue("href", linkIsEmpty));
                    if (linkIsEmpty == productLink)
                        return;

                    HtmlWeb subWeb = new HtmlWeb();
                    htmlNodes = subWeb.Load(productLink);

                    var model = new SitePropertiesModel
                    {
                        ProductUrl = productLink,
                        SiteName = "Trendyol",
                        Brand = GetInnerTextByClass("detail-name"),
                        DisplaySize = GetInnerTextByXPath("//*[@id=\"product-detail-app\"]/div/section/div/ul/li[6]/span[2]/b"),
                        ImageUrl = GetImageByClassName("detail-section-img"),
                        ModelName = GetInnerTextByXPath("//*[@id=\"poExpander\"]/div[1]/div/table/tbody/tr[2]/td[2]/span"),
                        ModelNo = GetInnerTextByXPath("//*[@id=\"poExpander\"]/div[1]/div/table/tbody/tr[2]/td[2]/span"),
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

                var prodModel = new List<SitePropertiesModel>();

                Parallel.ForEach(selectedAElement, item =>
                {
                    string productLink = item.GetAttributeValue("href", linkIsEmpty);
                    if (linkIsEmpty == productLink)
                        return;

                    HtmlWeb subWeb = new HtmlWeb();
                    htmlNodes = subWeb.Load(productLink);

                    var model = new SitePropertiesModel
                    {
                        ProductUrl = productLink,
                        SiteName = "N11",
                        Brand = GetInnerTextByXPath("//*[@id=\"unf-prop\"]/div/ul/li[24]/p[2]/a"),
                        DisplaySize = GetInnerTextByXPath("//*[@id=\"unf-prop\"]/div/ul/li[16]/p[2]"),
                        ImageUrl = GetImageByClassName("lazy unf-p-img"),
                        ModelName = GetInnerTextByXPath("//*[@id=\"unf-prop\"]/div/ul/li[22]/p[2]"),
                        ModelNo = GetInnerTextByClass("unf-p-summary-title"),
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
                web.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
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

                var prodModel = new List<SitePropertiesModel>();

                Parallel.ForEach(selectedAElement, item =>
                {
                    string productLink = string.Concat(baseUrl, item.GetAttributeValue("href", linkIsEmpty));
                    if (linkIsEmpty == productLink)
                        return;
                    try
                    {
                        HtmlWeb subWeb = new HtmlWeb();
                        subWeb.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
                        subWeb.PreRequest += request =>
                        {
                            request.CookieContainer = new System.Net.CookieContainer();
                            return true;
                        };

                        htmlNodes = subWeb.Load(productLink);

                        var model = new SitePropertiesModel
                        {
                            ProductUrl = productLink,
                            SiteName = "TeknoSa",
                            Brand = GetInnerTextByXPath("//*[@id=\"pdp-main\"]/div[2]/div[1]/h1/b"),
                            DisplaySize = GetInnerTextByXPath("//*[@id=\"pdp-technical\"]/div/div[1]/div/table[1]/tbody/tr[2]/td[4]"),
                            ImageUrl = GetAttributeValueByXPath("srcset", "//*[@id=\"swiper-wrapper-fc9bedb091ac1867\"]/div[1]/figure/img"),
                            ModelName = GetInnerTextByXPath("//*[@id=\"pdp-main\"]/div[2]/div[1]/h1"),
                            ModelNo = GetInnerTextByXPath("//*[@id=\"pdp-technical\"]/div/div[1]/div/table[3]/tbody/tr[2]/td[3]"),
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

        public async Task<List<SitePropertiesModel>> GetScrapingData()
        {
            await GetProductsByTrendyolAsync();
            await GetProductsByN11Async();
            await GetProductsByTeknosaAsync();

            return _sourceSiteModel;
        }
    }
}
