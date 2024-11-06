using System.Globalization;
using System.Text;
using Companies.Parser;
using CsvHelper;
using CsvHelper.Configuration;

public class CompanyMap : ClassMap<Company>
{
    public CompanyMap()
    {
        Map(m => m.Status).Name("Status");
        Map(m => m.Name).Name("Name");
        Map(m => m.FullName).Name("Full Name");
        Map(m => m.Industry).Name("Industry");
        Map(m => m.SubIndustry).Name("Sub Industry");
        Map(m => m.ProductType).Name("Product Type");
        Map(m => m.GeneralDirector).Name("General Director");
        Map(m => m.LegalAddress).Name("Legal Address");
        Map(m => m.RegistrationDate).Name("Registration Date").TypeConverterOption.Format("yyyy-MM-dd");
        Map(m => m.AuthorizedCapital).Name("Authorized Capital");
        Map(m => m.INN).Name("INN");
        Map(m => m.OGRN).Name("OGRN");
        Map(m => m.Revenue).Name("Revenue");
        Map(m => m.GrowthRate).Name("Growth Rate");
    }
}

public class CsvExporter
{
    public void ExportCompanyToCsv(IEnumerable<Company> companies, string filePath)
    {
        using var writer = new StreamWriter(filePath, Encoding.UTF8, new FileStreamOptions(){Access = FileAccess.Write, Mode = FileMode.Create});
        using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            Quote = '\"',
            NewLine = Environment.NewLine
        });
        // Регистрация маппинга
        
        csv.Context.RegisterClassMap<CompanyMap>();

        // Запись заголовка и данных
        csv.WriteHeader<Company>();
        csv.NextRecord();
        
        foreach (var company in companies)
        {
            csv.WriteRecord(company);
            csv.NextRecord();
        }
        
    }
}