using Marten;

public record ReactivarCommand(string EmpresaId);

public class ReactivarHandler(IDocumentSession store) : ICommandHandler<ReactivarCommand>
{
    public async Task Handle(ReactivarCommand cmd)
    {
        var empresa = await store.Events.AggregateStreamAsync<Empresa>(cmd.EmpresaId);
        var evento = empresa.Reactivar();
        store.Events.Append(cmd.EmpresaId, evento);
        await store.SaveChangesAsync();
    }
}
