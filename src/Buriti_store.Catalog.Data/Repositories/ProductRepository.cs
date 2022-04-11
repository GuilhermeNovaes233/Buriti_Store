using Buriti_store.Catalog.Domain;
using Buriti_store.Catalog.Domain.Interfaces;
using Buriti_Store.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buriti_store.Catalog.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;
        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategory(int code)
        {
            return await _context.Products.AsNoTracking().Include(p => p.Category).Where(c => c.Category.Code == code).ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
