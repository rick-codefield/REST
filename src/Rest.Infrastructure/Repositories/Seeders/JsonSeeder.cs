using Rest.Application.Entities;
using Rest.Application.Repositories;
using System.Reflection;
using System.Text.Json;

namespace Rest.Infrastructure.Repositories;

public sealed class JsonSeeder : ISeeder
{
    public async Task Seed(IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "Rest.Infrastructure.Repositories.Seeders.Seed.json";

        using var stream = assembly.GetManifestResourceStream(resourceName)!;
        var jsonCompanies = JsonSerializer.Deserialize<JsonCompany[]>(stream, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        })!;
        
        foreach (var jsonCompany in jsonCompanies)
        {
            var company = new Company
            {
                Name = jsonCompany.Name,
            };

            await unitOfWork.CompanyRepository.Add(company);

            foreach (var jsonBrand in jsonCompany.Brands)
            {
                var brand = new Brand
                {
                    Name = jsonBrand.Name,
                    Company = company.Id,
                };

                await unitOfWork.BrandRepository.Add(brand);

                foreach (var jsonProduct in jsonBrand.Products)
                {
                    int baseLength = Math.Max(1, jsonProduct.Length - 5);
                    baseLength = baseLength * baseLength * baseLength;
                    var price = Math.Max(1, Math.Floor(baseLength / 5m)) * 5 - 0.05m;

                    var product = new Product
                    {
                        Name = jsonProduct,
                        Brand = brand.Id,
                        Price = price
                    };

                    await unitOfWork.ProductRepository.Add(product);
                }
            }
        }

        await unitOfWork.Commit(cancellationToken);
    }

    private sealed class JsonCompany
    {
        public required string Name { get; init; }
        public required JsonBrand[] Brands { get; init; }
    }

    private sealed class JsonBrand
    {
        public required string Name { get; init; }
        public required string[] Products { get; init; }
    }
}
