using Marten;
using JasperFx.Events;

var store = DocumentStore.For(opts =>
{
    opts.Connection("Host=localhost;Port=5432;Database=gestion_eventstore;Username=gestion;Password=dev_local_pwd");
    opts.Events.StreamIdentity = StreamIdentity.AsString;
    opts.Events.EventNamingStyle = EventNamingStyle.SmarterTypeName;
});

var session = store.LightweightSession();
session.Events.StartStream<Empresa>("emp-t", new EmpresaCreada("Andes", "Nit123"));
await session.SaveChangesAsync();

var cambiar = new CambiarPlanHandler(session);
await cambiar.Handle(new CambiarPlanCommand("emp-t", "Menor"));


var query = store.QuerySession();
var empresa = await query.Events.AggregateStreamAsync<Empresa>("emp-t");

Console.WriteLine(empresa!.Plan);


/*
var store = new EventStore();
var empresaId = "miId1";

var despachador = new Despachador();
despachador.Registrar(new CrearEmpresaHandler(store));
despachador.Registrar(new SuspenderEmpresaHandler(store));
despachador.Registrar(new CambiarPlanHandler(store));
despachador.Registrar(new ReactivarHandler(store));


var crearCmd = new CrearEmpresaCommand(empresaId, "Construtora1", "Nit123");
var suspen = new SuspenderEmpresaCommand(empresaId, "Falta Pago");
var reactCmd = new ReactivarCommand(empresaId);
var nuevaSusp = new SuspenderEmpresaCommand(empresaId, "Nada que paga");
var cambiarPlan = new CambiarPlanCommand(empresaId, "Ultra");

despachador.Ejecutar(crearCmd);
despachador.Ejecutar(suspen);
despachador.Ejecutar(reactCmd);
despachador.Ejecutar(nuevaSusp);
despachador.Ejecutar(cambiarPlan);

var empresa1 = store.AbrirStream<Empresa>(empresaId).Get();

Console.WriteLine($"Nombre: {empresa1.Nombre}, Nit: {empresa1.Nit}, Plan: {empresa1.Plan}, Suspendida: {empresa1.Suspendida}, Reactivada {empresa1.Reactivaciones} vez/veces");
*/

