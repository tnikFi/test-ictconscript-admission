using Cortex.Mediator.Commands;
using Cortex.Mediator.Queries;

namespace Common.Interfaces;

/// <summary>
/// Alias for the <see cref="IQuery{TResult}"/> interface for use
/// with commands that return values. This is not pure CQRS, so the
/// <see cref="ICommand"/> type doesn't allow return values.
/// </summary>
public interface IValueCommand<out TResult> : IQuery<TResult>;