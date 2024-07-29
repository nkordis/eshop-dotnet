using EShop.DataAccess.Data;
using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;

namespace EShop.DataAccess.Repository;

public class CompanyRepository(ApplicationDbContext db) 
    : Repository<Company>(db), ICompanyRepository
{
    public void Update(Company company)
    {
        db.Update(company);
    }
}

