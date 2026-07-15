public class Despachador
{
    private Dictionary<Type, Action<object>> handlers = [];

    public void Registrar<T>(ICommandHandler<T> handler)
    {
        handlers.Add(
            typeof(T),
            (object comando) => handler.Handle((T)comando)
        );
    }

    public void Ejecutar(object comando)
    {
        handlers[comando.GetType()](comando);
    }
}










