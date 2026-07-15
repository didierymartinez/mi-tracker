

empezamos con un listado de records (hechos) en orden de ocurrencia, una lista de object luego empiezo a recorrer esa lista y dependiendo del tipo se modifican las variables al colocar esas variables como propiedades de una clase "Empresa" y el foreach en el constructor, y un método Aplicar que tiene los if y recibe cada hecho del foreach se crea el concepto de agregado

después crear la clase abstracta AggregateRoot con un método Load(List<object> hechos) y un método abstacto Aplicar(object hecho), 
heredar "Empresa" de AggregateRoot llamar load en el constructor y hacer override del método Aplicar, estas listas no pueden estar sueltas y la solución es crear una clase EventStream que reciba un tipo aggregateRoot, una lista privada de object, un método Get() que devuelve un nuevo objeto "Empresa" haciendo Load(lista) (ya no se hace en el constructor), y otro método Append que inserta a la lista un object

hasta acá AggregateRoot -> EventStream con métodos Get y Append

Crear métodos en "Empresa" que retornen los eventos, SuspenderEmpresa -> EmpresaSuspendida, estos son comandos para no estar haciendo suelto Get -> Act -> Append, Crear una clase Handler por cada Acción, SuspenderHandler(stream) este siempre tiene un método Handle() que recibe un comando recupera la entidad con stream.Get(), ejecuta método del aggregate pasando los parámetros desde el comando y el aggregate retorna hecho con excepción o idempotente, si este retorna él inserta el hecho en el stream con Append

Se crea una Interface ICommandHandler<TCommand> que define que todos deben tener el método Handle y que el tipo TCommand es es tipo que le llega al método Handle, Desapachador: es un diccionario que la clave es el tipo del comando y el valor es una función que ejecuta el método handle (object comando) =>  handler.Handle((T)comando), un método Registrar<T>(ICommandHandler<T> handler) donde ese T sirve para saber el tipo del comando y para el cast del comando cuando se haga el handle. 

si el desapachador registra por tipo y el parámetro de un handler es el stream, siempre se modificaría la misma empresa, para evitar eso se crea un Store que almacena multiples Streams y el handler recibe todo el store con todos los streams y el Id del Aggregate que debe abrir. el método AbrirStream(id) retorna el stream útil, y también se guarda son el método store.AppendEvent(id, evento)

EventStore: es un diccionario que encapsula el EventStream, clave: AggregateId, value: List<objects>, tres métodos, AbrirStream<T>, Get(id), AppendEvent(id, evento)  