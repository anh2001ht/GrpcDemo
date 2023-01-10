using Grpc.Core;
using GrpcEF.Data;
using GrpcEF.Protos;

public class ProductService : ProductSrv.ProductSrvBase
{
    private readonly ProductContext _context;

    public ProductService(ProductContext context)
    {
        _context = context;
    }

    public override Task<Products> GetAll(Empty request, ServerCallContext context)
    {
        var temp1 = new GrpcEF.Entites.Product()
        {
            Name = "p1",
            Price = 1234,
            Id = 1,
        };
        var temp2 = new GrpcEF.Entites.Product()
        {
            Name = "p2",
            Price = 2234,
            Id = 2,
        };
        _context.Product.Add(temp1);
        _context.SaveChanges();
        _context.Product.Add(temp2);
        _context.SaveChanges();
        var response = new Products();
        var products = (from c in _context.Product
                        select new Product
                        {
                            ProductId = c.Id,
                            Name = c.Name,
                            Price = c.Price,
                        }).ToArray();

        response.Items.AddRange(products);
        return Task.FromResult(response);

    }
    public override Task<Product> GetById(ProductRowIdFilter request, ServerCallContext context)
    {
        var product = _context.Product.Where(w => w.Id == request.ProductRowId).FirstOrDefault();
        var sProduct = new Product
        {
            Name = product.Name,
            Price = product.Price,
            ProductId = product.Id
        };
        return Task.FromResult(sProduct);
    }
}