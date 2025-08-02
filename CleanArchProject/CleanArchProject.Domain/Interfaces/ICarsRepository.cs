using System;
using BuildingBlocks.Domain.Interfaces;
using CleanArchProject.Domain.Models;

namespace CleanArchProject.Domain.Interfaces;

public class ICarsRepository : IRepository<carModel>
{
    // ICleanArchProjectRepository, Clean Architecture projesi için kullanıcı ve araç modelleri üzerinde CRUD işlemleri yapabilen bir arayüzdür.
    // Bu arayüz, IRepository<TEntity> arayüzünden türetilmiştir ve kullanıcı ve araç modelleri için özel işlemler içerebilir.
    // Örnek olarak, kullanıcı ve araç ekleme, güncelleme, silme gibi işlemler burada tanımlanabilir.
}


