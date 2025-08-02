using System;

namespace BuildingBlocks.Domain.Interfaces;

public class ICurrentUser
{
    //ICurrentUser, uygulamada o anki kullanıcının kimlik ve IP bilgilerini standart şekilde elde etmek için 
    // kullanılan bir arayüzdür. Gerçek implementasyonu, genellikle HTTP context veya token’dan bu bilgileri çeker.

    public string IPAddress { get; }
    public string UserName { get; }

}
