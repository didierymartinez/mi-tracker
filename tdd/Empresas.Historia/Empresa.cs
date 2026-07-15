
public class Empresa
{
    public string Id { get; set; }
    public string Nombre = "";
    public string Nit = "";
    public string Plan = "";
    public bool Suspendida = false;
    public string MotivoSuspension = "";
    public int Reactivaciones = 0;

    public Empresa()
    {

    }

    public void Apply(EmpresaCreada c)
    {
        Nombre = c.Nombre;
        Nit = c.Nit;
        Plan = "Basico";
    }

    public void Apply(EmpresaSuspendida s)
    {
        Suspendida = true;
        MotivoSuspension = s.Motivo;
    }

    public void Apply(EmpresaReactivada e)
    {
        Suspendida = false;
        MotivoSuspension = "";
        Reactivaciones++;
    }

    public void Apply(PlanCambiado p)
    {
        Plan = p.NuevoPlan;
    }

    public EmpresaCreada CrearEmpresa(string Nombre, string Nit)
    {
        return new(Nombre, Nit);
    }

    public EmpresaSuspendida Suspender(string Motivo)
    {
        return new("Falta de pago");
    }

    public EmpresaReactivada Reactivar()
    {
        return new();
    }

    public PlanCambiado CambiarPlan(string NuevoPlan)
    {
        return new(NuevoPlan);
    }

}




