using Application.Specifications;
using Domain.Entities;

namespace Domain.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) : base(x=>
            (string.IsNullOrEmpty(productParams.Search)||x.Name.ToLower().Contains(productParams.Search)))
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Color);
            AddInclude(x => x.Images);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize*(productParams.PageIndex-1),productParams.PageSize);

            if (!string.IsNullOrWhiteSpace(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    case "category":
                        AddOrderBy(ca=>ca.Category.Name); 
                        break;
                    case "color":
                        AddOrderBy(co => co.Color.Name);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }
    }
}
