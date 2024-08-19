using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
	public class ProductRepository : Repository<Product>, IProductRepository
    {
		private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
			_db = db;
        }
		public void update(Product obj)
		{
			//_db.Products.Update(obj);
			var productFromDb = _db.Products.FirstOrDefault(u=>u.Id == obj.Id);
			if(productFromDb !=null )
			{
				productFromDb.Title = obj.Title;
				productFromDb.Description = obj.Description;
			    productFromDb.ISBN = obj.ISBN;
				productFromDb.Author = obj.Author;
				productFromDb.Price100 = obj.Price100;
				productFromDb.CategoryId = obj.CategoryId;
				productFromDb.ListPrice = obj.ListPrice;
				productFromDb.Price50 = obj.Price50;
				productFromDb.Price = obj.Price;
				if (obj.ImageUrl != null)
				{
					productFromDb.ImageUrl = obj.ImageUrl;
				}

			}
		}
	}
}
