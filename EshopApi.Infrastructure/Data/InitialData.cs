using EshopApi.Domain;
using Microsoft.Extensions.Configuration;

namespace EshopApi.Infrastructure.Data
{
    public class InitialData
    {
        private readonly string? _basePath;

        public InitialData(IConfiguration configuration)
        {
            _basePath = configuration["DEVELOPMENT_IMAGE_PATH"];
            if (string.IsNullOrEmpty(_basePath))
            {
                throw new InvalidOperationException(
                    "DEVELOPMENT_IMAGE_PATH is not set in .env file. Please set the path to the folder with images.");
            }
        }

        private string GetProductImageUrl(string fileName)
        {
            string basePath = _basePath!;
            return $"{basePath}{fileName}";
        }


        public List<Product> GetInitialProducts() => new List<Product>
        {
            new Product
            {
                Id = Guid.Parse("1a2b3c4d-5e6f-7g8h-9i0j-1k2l3m4n5o6p"),
                Name = "Moderní hodinky",
                Description = "Elegantní pánské hodinky s černým řemínkem a LED displejem. Vodotěsné do 50 metrů.",
                Price = 2999.00m,
                PictureUri = GetProductImageUrl("watches_PNG9863-3949376704.png"),
            },
            new Product
            {
                Id = Guid.Parse("2b3c4d5e-6f7g-8h9i-0j1k-2l3m4n5o6p"),
                Name = "Bezdrátová sluchátka",
                Description = "Profesionální bezdrátová sluchátka s aktivním potlačením šumu a výdrží 24 hodin.",
                Price = 4999.00m,
                PictureUri = GetProductImageUrl("Beats-by-Dre-Limited-Edition-Gloss-Gold-Headphones-and-Pill-2-studio-999965710.jpg"),
            },
            new Product
            {
                Id = Guid.Parse("3c4d5e6f-7g8h-9i0j-1k2l-3m4n5o6p7q"),
                Name = "Smartphone",
                Description = "Nejnovější model smartphone s 6GB RAM, 128GB úložištěm a trojitým fotoaparátem.",
                Price = 14999.00m,
                PictureUri = GetProductImageUrl("4764970338_96037b36c3_b-1-2581187507.jpg"),
            },
            new Product
            {
                Id = Guid.Parse("4d5e6f7g-8h9i-0j1k-2l3m-4n5o6p7q8r"),
                Name = "Kávovar",
                Description = "Automatický kávovar s širokou škálou funkcí a možností nastavení.",
                Price = 8999.00m,
                PictureUri = GetProductImageUrl("Krups_Nespresso_Prodigio_cafetera-4119988827.jpg"),
            },
            new Product
            {
                Id = Guid.Parse("5e6f7g8h-9i0j-1k2l-3m4n-5o6p7q8r9s"),
                Name = "Dámská taška",
                Description = "Elegantní kožená taška s několika kapsami a odnímatelným ramínkem.",
                Price = 3999.00m,
                PictureUri = GetProductImageUrl("DSC_0073-2108993918.JPG"),
            },
            new Product
            {
                Id = Guid.Parse("6f7g8h9i-0j1k-2l3m-4n5o-6p7q8r9s0t"),
                Name = "Manželská postel",
                Description = "Moderní manželská postel 200cm x 180cm s úložným prostorem a matrací.",
                Price = 5999.00m,
                PictureUri = GetProductImageUrl("ubytovani-usti-nad-labem-penzion-komtesa-Bianka-1.jpg"),
            },
            new Product
            {
                Id = Guid.Parse("7g8h9i0j-1k2l-3m4n-5o6p-7q8r9s0t1u"),
                Name = "Powerbanka",
                Description = "Výkonná powerbanka s kapacitou 20000mAh a dvěma USB porty.",
                Price = 1299.00m,
                PictureUri = GetProductImageUrl("anker-powerbank-2649890983.jpg"),
            },
            new Product
            {
                Id = Guid.Parse("8h9i0j1k-2l3m-4n5o-6p7q-8r9s0t1u2v"),
                Name = "Dámské boty",
                Description = "Elegantní černé boty na podpatku s pohodlnou stélkou a kvalitním materiálem.",
                Price = 2999.00m,
                PictureUri = GetProductImageUrl("podvazek-bile-boty-na-podpatku-70167993.jpg"),
            },
            new Product
            {
                Id = Guid.Parse("9i0j1k2l-3m4n-5o6p-7q8r-9s0t1u2v3w"),
                Name = "Lednice s mrazákem",
                Description = "Kompaktní lednička s mrazákem a automatickým odtáváním.",
                Price = 15999.00m,
                PictureUri = GetProductImageUrl("Double-Door-Fridge-PNG-Download-Image-3854761405.png"),
            },
            new Product
            {
                Id = Guid.Parse("0j1k2l3m-4n5o-6p7q-8r9s-0t1u2v3w4x"),
                Name = "Mikrovlnná trouba",
                Description = "Mikrovlnná trouba s dotykovým ovládáním a funkcí grilování.",
                Price = 3999.00m,
                PictureUri = GetProductImageUrl("microwave_PNG15704-4137692812.png"),
            },
            new Product
            {
                Id = Guid.Parse("1k2l3m4n-5o6p-7q8r-9s0t-1u2v3w4x5y"),
                Name = "Pračka",
                Description = "Automatická pračka s předpíráním a funkcí sušení.",
                Price = 24999.00m,
                PictureUri = GetProductImageUrl("waschmaschine-von-galanz-4077569996.jpeg"),
            },
            new Product
            {
                Id = Guid.Parse("2l3m4n5o-6p7q-8r9s-0t1u-2v3w4x5y6z"),
                Name = "LCD Televize",
                Description = "64'' LCD televize s rozlišením 4K, HDR a operačním systémem Android TV.",
                Price = 34999.00m,
                PictureUri = GetProductImageUrl("Full-HD-LED-TV-PNG-Download-Image-935071104.png"),
            }
        };
    }
}