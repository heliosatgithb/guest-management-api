using System;

namespace GuestManagementApi.CQRS.Interfaces;

public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> Handle(TQuery query);
}