// See https://aka.ms/new-console-template for more information

using Companies.Parser;

var parser = new Parser();
var exporter = new CsvExporter();

var all = new List<Company>();


var urls = new[]
{
    "https://companies.rbc.ru/category/841-proizvodstvo_chaya_i_kofe",
    "https://companies.rbc.ru/category/1051-myasnoe_delo",
    "https://companies.rbc.ru/category/937-proizvodstvo_molochnoj_produkcii",
    "https://companies.rbc.ru/category/950-rybnaya_produkciya",
    "https://companies.rbc.ru/category/958-proizvodstvo_prochih_pischevyh_produktov",
    "https://companies.rbc.ru/category/921-produkciya_iz_fruktov_i_ovoschej",
    "https://companies.rbc.ru/category/985-mukomolno-krupyanoe_proizvodstvo",
    "https://companies.rbc.ru/category/835-proizvodstvo_kormov_dlya_zhivotnyh",
    "https://companies.rbc.ru/category/980-proizvodstvo_konditerskih_izdelij",
    "https://companies.rbc.ru/category/928-proizvodstvo_masel_i_zhirov",
    "https://companies.rbc.ru/category/850-proizvodstvo_polufabrikatov",
    "https://companies.rbc.ru/category/841-proizvodstvo_chaya_i_kofe",
    "https://companies.rbc.ru/category/844-proizvodstvo_priprav_i_pryanostej",
    "https://companies.rbc.ru/category/857-proizvodstvo_detskogo_pitaniya_i_dieticheskih_produktov",
    "https://companies.rbc.ru/category/971-proizvodstvo_sahara",
    "https://companies.rbc.ru/category/954-proizvodstvo_zhmyha_i_muki",
};

foreach (var url in urls)
{
    
    Page page;
    var current = 1;
    do
    {
        page = await parser.LoadPage(url, current++);
        all.AddRange(page.Companies);
    } while (page.HasNext);

}


exporter.ExportCompanyToCsv(all, "result.csv");