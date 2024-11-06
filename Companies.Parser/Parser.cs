using System.Globalization;
using AngleSharp;
using AngleSharp.Dom;

namespace Companies.Parser;

public class Parser
{
    IConfiguration _config = Configuration.Default.WithDefaultLoader();


    public async Task<Page> LoadPage(string url, int page)
    {
        var p = page > 1 ? page.ToString() : "1";
        var address = $"{url}/{p}/";
        var context = BrowsingContext.New(_config);
        var document = await context.OpenAsync(address);

        var companies = document.QuerySelectorAll(".company-card").Select(ParseCompanyInfo);

        return new Page()
        {
            Companies = companies.ToArray(),
            HasNext = document.QuerySelector(".pagination__item--active+.pagination__item") != null
        };
    }


    private static Company ParseCompanyInfo(IElement element)
    {
        var company = new Company();

        company.Status = element.QuerySelector(".company-status-badge")?.TextContent?.Trim();

        // Краткое и полное название компании
        company.Name = element.QuerySelector(".company-name-highlight span")?.TextContent?.Trim();
        company.FullName = element.QuerySelector(".company-name-highlight span")?.GetAttribute("title")?.Trim();

        // Индустрия, подиндустрия и тип продукта
        var breadcrumbItems = element.QuerySelectorAll(".category-breadcrumb__item");
        company.Industry = breadcrumbItems.Length > 0 ? breadcrumbItems[0].TextContent.Trim() : null;
        company.SubIndustry = breadcrumbItems.Length > 1 ? breadcrumbItems[1].TextContent.Trim() : null;
        company.ProductType = breadcrumbItems.Length > 2 ? breadcrumbItems[2].TextContent.Trim() : null;

        // Генеральный директор
        company.GeneralDirector = ParseProperty(element, "Директор:");
        //element.QuerySelector(".company-card__info span + span")?.TextContent?.Trim();


        
        
        
        
        company.LegalAddress = ParseProperty(element, "Юридический адрес:");

        // Дата регистрации
        var registrationDateText = ParseProperty(element, "Дата регистрации:");
        

        // Уставной капитал
        var capitalText = ParseProperty(element, "Уставной капитал:");
        company.AuthorizedCapital = ParseDecimal(capitalText);
        
        var innText = ParseProperty(element, "ИНН:");
        company.INN = innText;
        var ogrnText = ParseProperty(element, "ОГРН:");
        company.OGRN = ogrnText;
        
        var revenueText = ParseProperty(element, "Выручка:");
        company.Revenue = ParseDecimal(revenueText);
        var growthRateText = ParseProperty(element, "Темп прироста:");
        company.GrowthRate = growthRateText.Replace("%", "");


        company.RegistrationDate = registrationDateText;
        //company.RegistrationDate = DateTime.ParseExact(registrationDateText, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        return company;
    }

    private static string ParseProperty(IElement element, string prop)
    {
        var item = element.QuerySelector($".company-card__info:contains('{prop}')");

        item?.QuerySelector("span")?.Remove();
        return item?.TextContent.Trim() ?? "";
    }

    private static string ParseDecimal(string input)
    {
        return input.Replace("₽", "").Replace(" ", "").Trim();
    }
}