using Marten;

public interface IEventStore
{
    Task<T?> GetAgggegate<T>(string id);
    void Append(string id, object evento);
    Task SaveChangesAsync();
}

public record SuspenderEmpresaCommand(string EmpresaId, string Motivo);

public class SuspenderEmpresaHandler(IEventStore store) : ICommandHandler<SuspenderEmpresaCommand>
{
    public async Task Handle(SuspenderEmpresaCommand cmd)
    {
        var empresa = await store.GetAgggegate<Empresa>(cmd.EmpresaId);

        var evento = empresa.Suspender(cmd.Motivo);
        store.Append(cmd.EmpresaId, evento);

        await store.SaveChangesAsync();
    }
}
