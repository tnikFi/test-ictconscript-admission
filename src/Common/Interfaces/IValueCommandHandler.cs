using Cortex.Mediator.Queries;

namespace Common.Interfaces;

/// <summary>
/// Alias for <see cref="IQueryHandler{TQuery,TResult}"/> to handle
/// commands with return types as they're not pure CQRS.
/// </summary>
/// <typeparam name="TValueCommand"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface IValueCommandHandler<in TValueCommand, TResult> : IQueryHandler<TValueCommand, TResult>
    where TValueCommand : IValueCommand<TResult>;