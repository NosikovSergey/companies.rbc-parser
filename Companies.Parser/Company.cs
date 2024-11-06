namespace Companies.Parser;

public class Page
{
    public ICollection<Company> Companies { get; set; }
    public bool HasNext { get; set; }
}

public class Company
{
    public string Status { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Industry { get; set; }
    public string SubIndustry { get; set; }
    public string ProductType { get; set; }
    public string GeneralDirector { get; set; }
    public string LegalAddress { get; set; }
    public string RegistrationDate { get; set; }
    public string AuthorizedCapital { get; set; }
    public string INN { get; set; }
    public string OGRN { get; set; }
    public string Revenue { get; set; }
    public string GrowthRate { get; set; }
}