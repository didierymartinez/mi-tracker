public interface ICommandHandler<T>
{
    public Task Handle(T comando);
}