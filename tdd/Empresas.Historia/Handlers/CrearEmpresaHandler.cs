using Marten;

public record CrearEmpresaCommand(string aggregateId, string Nombre, string Nit);

public class CrearEmpresaHandler(IDocumentSession store) : ICommandHandler<CrearEmpresaCommand>
{
    public async Task Handle(CrearEmpresaCommand cmd)
    {
        var empresa = new Empresa();
        var evento = empresa.CrearEmpresa(cmd.Nombre, cmd.Nit);
        store.Events.StartStream<Empresa>(evento);
        await store.SaveChangesAsync();
    }
}
