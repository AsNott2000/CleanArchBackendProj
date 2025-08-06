using BuildingBlocks.Domain.Interfaces;

namespace BuildingBlocks.Infrastructure.Implementations;

public class BaseUnitOfWork(DBContext dbContext) : IBaseUnitOfWork
{
    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        /*
            UnitOfWork, birden fazla repository’de yapılan işlemlerin tek bir transaction (işlem bloğu) olarak yönetilmesini sağlar.

            Yani, birkaç farklı veri tablosunda işlem yapıp, en son hepsini bir seferde kaydedersin.

            Her şey yolundaysa hepsi işlenir, bir yerde hata varsa hiçbir şey veritabanına yazılmaz (ACID garantisi).
        */
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}

