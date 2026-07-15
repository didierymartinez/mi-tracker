using JasperFx.Events;
using Marten;

public record CambiarPlanCommand(string EmpresaId, string NuevoPlan);

public class CambiarPlanHandler(IDocumentSession store) : ICommandHandler<CambiarPlanCommand>
{
    public async Task Handle(CambiarPlanCommand cmd)
    {
        var empresa = await store.Events.AggregateStreamAsync<Empresa>(cmd.EmpresaId);
        var evento = empresa.CambiarPlan(cmd.NuevoPlan);
        store.Events.Append(cmd.EmpresaId, evento);
        await store.SaveChangesAsync();
    }
}