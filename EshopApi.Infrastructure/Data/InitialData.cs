using EshopApi.Domain;
using Microsoft.EntityFrameworkCore;
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

        public async Task InitializeDatabaseAsync(EshopDbContext context)
        {
            if (await context.Products.AnyAsync())
                return;

            var products = GetInitialProducts();
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }

        public List<Product> GetInitialProducts() => new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Moderní hodinky",
                Description = "Elegantní pánské hodinky s černým řemínkem a LED displejem. Vodotěsné do 50 metrů.",
                Price = 2999.00m,
                PictureUri = GetProductImageUrl("watches_PNG9863-3949376704.png"),
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Bezdrátová sluchátka",
                Description = "Profesionální bezdrátová sluchátka s aktivním potlačením šumu a výdrží 24 hodin.",
                Price = 4999.00m,
                PictureUri = GetProductImageUrl("Beats-by-Dre-Limited-Edition-Gloss-Gold-Headphones-and-Pill-2-studio-999965710.jpg"),
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Smartphone",
                Description = "Nejnovější model smartphone s 6GB RAM, 128GB úložištěm a trojitým fotoaparátem.",
                Price = 14999.00m,
                PictureUri = GetProductImageUrl("4764970338_96037b36c3_b-1-2581187507.jpg"),
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kávovar",
                Description = "Automatický kávovar s širokou škálou funkcí a možností nastavení.",
                Price = 8999.00m,
                PictureUri = GetProductImageUrl("Krups_Nespresso_Prodigio_cafetera-4119988827.jpg"),
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Dámská taška",
                Description = "Elegantní kožená taška s několika kapsami a odnímatelným ramínkem.",
                Price = 3999.00m,
                PictureUri = GetProductImageUrl("DSC_0073-2108993918.JPG"),
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Manželská postel",
                Description = "Moderní manželská postel 200cm x 180cm s úložným prostorem a matrací.",
                Price = 5999.00m,
                PictureUri = GetProductImageUrl("ubytovani-usti-nad-labem-penzion-komtesa-Bianka-1.jpg"),
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Powerbanka",
                Description = "Výkonná powerbanka s kapacitou 20000mAh a dvěma USB porty.",
                Price = 1299.00m,
                PictureUri = GetProductImageUrl("anker-powerbank-2649890983.jpg"),
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Dámské boty",
                Description = "Elegantní černé boty na podpatku s pohodlnou stélkou a kvalitním materiálem.",
                Price = 2999.00m,
                PictureUri = GetProductImageUrl("podvazek-bile-boty-na-podpatku-70167993.jpg"),
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Lednice s mrazákem",
                Description = "Kompaktní lednička s mrazákem a automatickým odtáváním.",
                Price = 15999.00m,
                PictureUri = GetProductImageUrl("Double-Door-Fridge-PNG-Download-Image-3854761405.png"),
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Mikrovlnná trouba",
                Description = "Mikrovlnná trouba s dotykovým ovládáním a funkcí grilování.",
                Price = 3999.00m,
                PictureUri = GetProductImageUrl("microwave_PNG15704-4137692812.png"),
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Pračka",
                Description = "Automatická pračka s předpíráním a funkcí sušení.",
                Price = 24999.00m,
                PictureUri = GetProductImageUrl("waschmaschine-von-galanz-4077569996.jpeg"),
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "LCD Televize",
                Description = "64'' LCD televize s rozlišením 4K, HDR a operačním systémem Android TV.",
                Price = 34999.00m,
                PictureUri = GetProductImageUrl("Full-HD-LED-TV-PNG-Download-Image-935071104.png"),
            }
        };
    }
}