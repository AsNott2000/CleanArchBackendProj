using MediatR;

namespace BuildingBlocks.Application.Features;

public interface ICommandQuery<TResult> : IRequest<Result<TResult>>, IBaseRequest
{
}

public interface ICommandQuery : IRequest<Result>, IBaseRequest
{
}

/*
Standart response: Her yerde Result veya Result<T> ile çalışılır, hata yönetimi ve başarıyı API seviyesinde merkezi yapabilirsin.

Tüm Command/Query’ler ortak interface’e sahip olur (mesela: log, audit, tenant, user context için property’ler ekleyebilirsin).

MediatR ile doğrudan uyumlu olur, handler’ların yazımı pratikleşir.
*/
