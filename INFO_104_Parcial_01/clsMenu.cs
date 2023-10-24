using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
//10/24/2023. Wilton Benedict
//UH. Prueba Parcial I. INFO-104
//Clase Menú
/* Metodos Requeridos:
 Mostrar el menú, agregar empleados, consultar empleados, borrar empleados, 
 modificar empleados, inicializar arreglos y generar reportes.
 */
namespace INFO_104_Parcial_01
{
    internal class clsMenu
    {
        //Variables Globales
        public clsEmpleado[] empleado = new clsEmpleado[10]; //arreglo con objetos
        public bool estadoArreglo = false;//variable que indica si se ha inicializado o no los arreglos.

        //Parte 01: Metodos Principales requeridos
        //========================================================
        public void MostrarMenu()
        {
            //Metodo Mostrar Menu
            //Redirige al usuario a otros metodos segun su seleccion.
            int opcion;
            bool estado = true;
            while (estado)
            {
                Console.WriteLine("--Menu Principal--");
                Console.WriteLine("1. Agregar Empleados");
                Console.WriteLine("2. Consultar Empleados");
                Console.WriteLine("3. Modificar Empleados");
                Console.WriteLine("4. Borrar Empleados");
                Console.WriteLine("5. Inicializar Arreglos");
                Console.WriteLine("6. Reportes");
                Console.WriteLine("7. Salida");
                Console.WriteLine("Ingrese su seleccion: ");
                int.TryParse(Console.ReadLine(), out opcion);
                if(estadoArreglo == false)
                {
                    if (opcion >= 1 && opcion <= 4 || opcion == 6)//Previene usar metodos que resultaran en errores hasta que se inicie el arreglo
                    {
                        Console.WriteLine("Accion no posible. Se necesita iniciar el arreglo primero.");
                    }
                    else if(opcion == 5) { IniciarArreglo(); }
                    else if(opcion == 7) { estado = false; }
                    else { Console.WriteLine("Seleccion invalida. Intente de nuevo."); }
                
                }
                else
                {
                    if (opcion == 1) { AgregarEmpleado(); }
                    else if (opcion == 2) { ConsultarEmpleado(); }
                    else if (opcion == 3) { ModificarEmpleado(); }
                    else if (opcion == 4) { BorrarEmpleado(); }
                    else if (opcion == 5) { IniciarArreglo(); }
                    else if (opcion == 6) { GenerarReporte(); }
                    else if (opcion == 7) { estado = false; }
                    else { Console.WriteLine("Seleccion invalida. Intente de nuevo."); }

                }
            }
        }

        public void IniciarArreglo()
        {
            //Metodo Iniciar Arreglo
            //Utiliza la funcion Resize tanto para reducir como extender la longitud del arreglo
            //El metodo "CrearArreglo" se ejecuta al final de la eleccion debido a que este crea una instancia de los nuevos objetos.
            //La variable "estadoArreglo" sirve para prevenir el uso del programa en caso que no se hayan inicializado el arreglo
            char opcion = ' ';
            int longitud = 0;

            if (estadoArreglo == false)
            {
                Console.WriteLine("Iniciando arreglo...");
                Console.WriteLine($"Longitud por defecto: {empleado.Length} empleados");
                Console.WriteLine("Desea modificar la longitud del arreglo? (S para modificar, otra tecla para continuar)");
                char.TryParse(Console.ReadLine().ToUpper(), out opcion);
                if (opcion == 'S')
                {
                    Console.WriteLine("Ingrese la nueva longitud del arreglo: ");
                    int.TryParse(Console.ReadLine(), out longitud);
                    if (longitud - 10 <= 0)
                    {
                        Array.Resize(ref empleado, longitud);
                    }
                    else
                    {
                        int sumaArreglo = longitud - 10;
                        Array.Resize(ref empleado, empleado.Length+sumaArreglo);
                    }
                    CrearArreglo();
                    Console.WriteLine($"Arreglo Inicializado...\nLongitud de arreglo: {empleado.Length}");
                    
                }
                else
                {
                    CrearArreglo();
                    Console.WriteLine($"Arreglo Inicializado...\nLongitud de arreglo: {empleado.Length}");
                }
                estadoArreglo = true;
            }
            else
            {
                Console.WriteLine("Arreglos ya han sido inicializados");
                Console.WriteLine("Desea desbloquear opcion para modificar longitud de arreglo? (S para modificar, otra tecla para continuar).");
                Console.WriteLine("NOTA: Utilizar una longitud menor a la actual causara la perdida de datos en los indices utilizados.");
                char.TryParse(Console.ReadLine().ToUpper(), out opcion);
                if (opcion == 'S')
                {
                    estadoArreglo = false;
                    Console.WriteLine("Opcion reestablecida. Por favor ingrese de nuevo para incializar los arreglos.");
                }
                else
                {
                    Console.WriteLine("Redirigiendo...");
                }
            }
        }

        public void AgregarEmpleado()
        {
            //Metodo Agregar Empleados
            //Previo a intentar agregar un dato, primero valida si hay espacios disponibles
            //Por defecto, el constructor asigna el salario con -1.Esto indica que esta disponible para uso.
            //Si la estructura selectiva no detecta el -1 en la seccion del salario, asume que esta en uso y no lo considera para guardar los datos.
            //En caso que no encuentre un arreglo con salario -1, indica que el arreglo ya esta lleno.
            Console.WriteLine("--Agregar Empleado--");
            string tempCedula, tempNombre, tempDireccion, tempTelefono;
            int tempSalario = 0;
            int control = 0;
            char confirmacion = ' ';
            bool estado = false;
            if (estadoArreglo == true)
            {
                for (int i = 0; i < empleado.Length; i++)
                {
                    if (empleado[i].salario != -1) 
                    {
                        control++;//Esta variable acumula la cantidad de datos en uso. Se determina que el arreglo esta lleno si
                        //su valor es igual a la longitud del arreglo.
                        if (control == empleado.Length) { Console.WriteLine("NOTA: No es posible agregar el empleado. Arreglo lleno."); break; }
                        continue; 
                    
                    } //Salta Arreglos con valores ya agregados

                    else
                    {
                        Console.WriteLine($"Agregando nuevo empleado #{i + 1}");
                        Console.WriteLine("1. Ingrese cedula del nuevo Empleado: ");
                        tempCedula = Console.ReadLine();
                        //Valida si la cedula ingresada es valida ya que no pueden existir dos empleados con la misma cedula.
                        tempCedula = ValidarCedula(tempCedula);
                        Console.WriteLine("2. Ingrese nombre del nuevo Empleado: ");
                        tempNombre = Console.ReadLine();
                        Console.WriteLine("3. Ingrese direccion del nuevo Empleado: ");
                        tempDireccion = Console.ReadLine();
                        Console.WriteLine("4. Ingrese # telefonico del nuevo Empleado: ");
                        tempTelefono = Console.ReadLine();
                        Console.WriteLine("5. Ingrese salario del nuevo Empleado: ");
                        int.TryParse(Console.ReadLine(), out tempSalario);
                        //Valida si el salario ingresado es un numero valido. Salarios no validos son considerados igual a 0 o negativo.
                        tempSalario = ValidarSalario(tempSalario);

                        Console.WriteLine("--Previsuaizacion del Nuevo Empleado--");
                        Console.WriteLine($"Cedula: {tempCedula}\nNombre: {tempNombre}\nDireccion: {tempDireccion}");
                        Console.WriteLine($"Telefono: {tempTelefono}\nSalario: {tempSalario}");
                        //Agregar Datos
                        empleado[i].cedula = tempCedula;
                        empleado[i].nombre = tempNombre;
                        empleado[i].direccion = tempDireccion;
                        empleado[i].telefono = tempTelefono;
                        empleado[i].salario = tempSalario;
                        Console.WriteLine("Desea agregar otro nuevo Empleado? (S/N)");
                        char.TryParse(Console.ReadLine().ToUpper(), out confirmacion);
                        if (confirmacion == 'N')
                        {
                            break;
                        }
                        else
                        {
                            estado = ValidarEspacio();
                            if (estado == true) { Console.WriteLine("No es posible agregar mas empleados. El arreglo esta lleno"); break; }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No se puede agregar empleados nuevos. Arreglo no ha sido inicializado.");
            }

        }

        public void ConsultarEmpleado()
        {
            //Metodo Consultar. Se puede consultar por un empleado por medio de cedula, nombre o # telefonico.
            //Captura el dato a buscar y lo envia a otra funcion. En caso que el empleado sea encontrado, retorna el indice.
            //En caso que no, retorna -1 para indicar que el dato no se encontro.
            int opcion = 0;
            bool estado = true;
            string valor = " ";
            int indice = -1;
            while (estado)
            {
                Console.WriteLine("--Consultar Empleado--");
                Console.WriteLine("1. Consultar por cedula");
                Console.WriteLine("2. Consultar por nombre");
                Console.WriteLine("3. Consultar por # telefonico");
                Console.WriteLine("4. Salir de consulta.");
                Console.WriteLine("Ingrese su seleccion: ");
                int.TryParse(Console.ReadLine(), out opcion);
                if (opcion >= 1 && opcion <=3)
                {
                    Console.WriteLine("Ingrese el valor a buscar: ");
                    valor = Console.ReadLine();
                    indice = BusquedaEmpleado(opcion,valor);//Se envia dos valores al indice. opcion indica el tipo de busqueda (cedula, nombre, etc) y valor el string a buscar.
                    if(indice != -1)
                    {
                        Console.WriteLine("Empleado consultado: ");
                        Console.WriteLine($"Cedula: {empleado[indice].cedula}\nNombre: {empleado[indice].nombre}\nDireccion: {empleado[indice].direccion}");
                        Console.WriteLine($"Telefono: {empleado[indice].telefono}\nSalario: {empleado[indice].salario}");
                    }
                    else
                    {
                        Console.WriteLine("Empleado no encontrado.");
                    }
                }
                else if(opcion == 4) { estado = false; }
                else
                {
                    Console.WriteLine("Opcion invalida. Intente de nuevo");
                }
            }
            
        }

        public void GenerarReporte()
        {
            //Metodo Generar Reporte
            //Debido a que el arreglo podria no estar lleno desde el principio, esto indica que pueden existir datos invalidos como el -1 establecido
            //por el constructor.   
            //Para solucionar esto, se utiliza la variable cantidadEmpleado la cual primero determina la cantidad de objetos con datos validos.
            //Ese dato se utilizara despues en otras funciones para calcular el promedio o imprimir todos los empleados con datos validos.
            int opcion = 0;
            bool estado = true;
            int cantidadEmpleado = 0;
            double promedio = 0;
            int minimo = 0;
            int maximo = 0;
            //Funcion que determina la cantidad de objetos validos
            cantidadEmpleado = NumeroEmpleados();
            while (estado)
            {
                Console.WriteLine("--Generar Reportes--");
                Console.WriteLine("1. Consultar Empleado.");
                Console.WriteLine("2. Listar todos/as Empleados/as.");
                Console.WriteLine("3. Promedio de Salarios.");
                Console.WriteLine("4. Salario mas alto y bajo.");
                Console.WriteLine("5. Salida.");
                Console.WriteLine("Ingrese su seleccion: ");
                int.TryParse(Console.ReadLine(), out opcion);
                if(opcion == 1)
                {
                    ConsultarEmpleado();
                }
                else if(opcion == 2)
                {
                    //Metodo que imprime la lista de los empleados con datos validos
                    ListaEmpleados(cantidadEmpleado);
                }
                else if(opcion == 3)
                {
                    //funcion que determina el promedio de los salarios
                    promedio = PromedioSalario(cantidadEmpleado);
                    if (promedio <= 0) { Console.WriteLine("No hay datos suficientes para mostrar el promedio"); }
                    else
                    {
                        Console.WriteLine($"El promedio de salarios es {promedio} colones");
                    }
                }
                else if(opcion == 4)
                {
                    //Funciones que determinan el salario mayor y menor del objeto con datos validos.
                    maximo = Maximo(cantidadEmpleado);
                    minimo = Minimo(cantidadEmpleado);
                    Console.WriteLine($"El salario mas alto es de {maximo} colones.");
                    Console.WriteLine($"El salario mas bajo es de {minimo} colones.");
                }
                else if(opcion == 5)
                {
                    estado = false;
                }
                else
                {
                    Console.WriteLine("Seleccion invalida. Intente otra vez.");
                }
            }
        }

        public void BorrarEmpleado()
        {
            //Metodo Borrar Empleado
            //Utiliza la misma funcion de busqueda para encontrar el indice relacionado con la cedula del objeto
            //El borrado se caracteriza por devolver el objeto a su estado que el constructor inicialmente asigno.
            //Esto ultimo permitira que el objeto este disponible nuevamente para agregar un nuevo empleado.
            int indice = -1;
            string cedula = " ";
            char confirmacion = ' ';
            
            Console.WriteLine("--Borrar Empleado--");
            Console.WriteLine("Ingrese la cedula del empleado a borrar: ");
            cedula = Console.ReadLine();
            indice = BusquedaEmpleado(1, cedula);
            if (indice != -1)
            {
                Console.WriteLine("--Empleado Encontrado--");
                Console.WriteLine($"Cedula: {empleado[indice].cedula}\nNombre: {empleado[indice].nombre}\nDireccion: {empleado[indice].direccion}");
                Console.WriteLine($"Telefono: {empleado[indice].telefono}\nSalario: {empleado[indice].salario}");
                Console.WriteLine("Desea confirmar borrar el empleado? (S para confirmar/ otra tecla para anular)");
                char.TryParse(Console.ReadLine().ToUpper(), out confirmacion);

                if (confirmacion == 'S')
                {
                    Console.WriteLine("Borrando datos de Empleado...");
                    empleado[indice].cedula = "";
                    empleado[indice].nombre = "";
                    empleado[indice].direccion = "";
                    empleado[indice].telefono = "";
                    empleado[indice].salario = -1;//Lo vuelve elegible para agregar un nuevo empleado
                    Console.WriteLine("Datos eliminados...");
                }
                else { Console.WriteLine("Borrado cancelado.."); }
            }
            else
            {
                Console.WriteLine("Contacto no encontrado.");
            }
        }

        public void ModificarEmpleado()
        {
            //Metodo Modificar Empleado
            //Utiliza la misma funcion de busqueda que otros metodos para encontrar el 
            //Se puede modificar desde la cedula hasta el salario. Tanto la cedula como el salario requieren de validacion previo a que los cambios
            //sean aplicados
            bool estado = true;
            int indice = -1, opcion = 0;
            string valor = " ";

            Console.WriteLine("--Modificar datos de Empleado--");
            Console.WriteLine("Ingrese la cedula del empleado a modificar: ");
            valor = Console.ReadLine();
            indice = BusquedaEmpleado(1,valor);
            if(indice != -1)
            {
                while (estado)
                {
                    Console.WriteLine("--Empleado a Modificar--");
                    Console.WriteLine($"Cedula: {empleado[indice].cedula}\nNombre: {empleado[indice].nombre}\nDireccion: {empleado[indice].direccion}");
                    Console.WriteLine($"Telefono: {empleado[indice].telefono}\nSalario: {empleado[indice].salario}");
                    Console.WriteLine("--Opciones de modificacion--");
                    Console.WriteLine("1. Modificar Cedula");
                    Console.WriteLine("2. Modificar Nombre");
                    Console.WriteLine("3. Modificar Telefono");
                    Console.WriteLine("4. Modificar Direccion");
                    Console.WriteLine("5. Modificar Salario");
                    Console.WriteLine("6. Salida");
                    Console.WriteLine("Ingrese su seleccion: ");
                    int.TryParse(Console.ReadLine(), out opcion);
                    //Esta funcion ejecuta los cambios para modificar el objeto. El dato que retorna determina si se haran mas cambios o 
                    //se redirige al menu principal.
                    estado = DatosModificacion(opcion, estado, indice);

                }
            }
            else
            {
                Console.WriteLine("Empleado no encontrado");
            }
        }
        //========================================================


        //Parte 02: Funcion de busqueda, Modifiaciones y validaciones menores.
        //========================================================
        public int BusquedaEmpleado(int busq,string valor)
        {
            //Funcion de busqueda. Se utiliza una estructura switch para determinar el tipo de busqueda. 
            //Cada case es el tipo de busqueda de acuerdo a los atributos del objeto
            //Cada categoria de case contiene una estructura for la cual retornara un indice que caso que valor a buscar sea encontrado en el objeto
            //1 Busqueda por cedula. 2 Busqueda por nombre. 3 Busqueda por telefono.
            int retVal = -1;
            switch (busq)
            {
                case 1:
                    for (int i = 0; i < empleado.Length; i++)
                    {
                        if (empleado[i].cedula == valor)
                        {
                            retVal = i; break;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < empleado.Length; i++)
                    {
                        if (empleado[i].nombre == valor)
                        {
                            retVal = i; break;
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < empleado.Length; i++)
                    {
                        if (empleado[i].telefono == valor)
                        {
                            retVal = i; break;
                        }
                    }
                    break;
            }
            return retVal;
        }
    
        public bool DatosModificacion(int opcion, bool estado, int indice)
        {
            //Funcion Datos modificar
            //Similar a la funcion de busqueda, la funcion de modificacion utiliza cases de un switch para determinar el tipo de cambio a utilizar.
            //Si se hace una modificacion, el programa mostrara una previsualizacion de los cambios. Tambien indica si se desean hacer mas cambios
            //o no para terminar la funcion y el metodo donde se llamo.
            bool retVal = true;
            char confirmacion = ' ';
            string datoCambio;
            int salarioCambio = 0;
            switch(opcion)
            {
                case 1:
                    Console.WriteLine("Ingrese la nueva cedula: ");
                    datoCambio = Console.ReadLine();
                    datoCambio = ValidarCedula(datoCambio);
                    empleado[indice].cedula = datoCambio;
                    break;
                case 2:
                    Console.WriteLine("Ingrese el nuevo nombre: ");
                    datoCambio = Console.ReadLine();
                    empleado[indice].nombre = datoCambio;
                    break;
                case 3:
                    Console.WriteLine("Ingrese el nuevo # telefonico: ");
                    datoCambio = Console.ReadLine();
                    empleado[indice].telefono = datoCambio;
                    break;
                case 4:
                    Console.WriteLine("Ingrese el nuevo direccion: ");
                    datoCambio = Console.ReadLine();
                    empleado[indice].telefono = datoCambio;
                    break;
                case 5:
                    Console.WriteLine("Ingrese el nuevo salario: ");
                    int.TryParse(Console.ReadLine(), out salarioCambio);
                    salarioCambio = ValidarSalario(salarioCambio);
                    empleado[indice].salario = salarioCambio;
                    break;
                case 6:
                    retVal = false;
                    break;
                default:
                    Console.WriteLine("Dato ingresado invalido. Intente de nuevo.");
                    break;
            }
            if(opcion >= 1 && opcion <= 5)
            {
                Console.WriteLine("Cambios Aplicados...");
                Console.WriteLine("Datos de Empleado actualizado:");
                Console.WriteLine($"Cedula: {empleado[indice].cedula}\nNombre: {empleado[indice].nombre}\nDireccion: {empleado[indice].direccion}");
                Console.WriteLine($"Telefono: {empleado[indice].telefono}\nSalario: {empleado[indice].salario}");
            }

            if(retVal == true)
            {
                Console.WriteLine("Desea hacer otra modificacion? (S salir del menu de modificaciones, otra tecla para continuar)");
                char.TryParse(Console.ReadLine().ToUpper(), out confirmacion);
                if (confirmacion == 'S') { retVal = false; }
                else
                {
                    Console.WriteLine("Redirigiendo al menu de modificaciones.");
                }
            }
            else { Console.WriteLine("Redirigiendo.."); }

            return retVal;
        }
    
        public int NumeroEmpleados()
        {
            //Funcion numero empleados
            //Utiliza una estructura de bucles para determinar la cantidad de empleados registrados y indices vacios.
            int retVal = 0;
            for(int i = 0;i < empleado.Length; i++)
            {
                if (empleado[i].salario != -1)
                {
                    retVal++;
                }
            }
            return retVal;
        }
        public double PromedioSalario(int cantidadEmpleado)
        {
            //Funcion promedio Salario
            //El arreglo tiene la longitud de la cantidad de objetos con datos validos.
            //Su funcion es capturar los salarios validos para despues usarlos en un calculo
            //Una vez el arreglo sea llenado con los salarios, se usa la funcion AsQueryable().Average() para determinar el promedio de los datos dentro del arreglo.
            int[] salarios = new int[cantidadEmpleado];
            int indice = 0;
            double promedio = 0;
            for(int i = 0;i < empleado.Length;i++) 
            {
                if (empleado[i].salario != -1)
                {
                    salarios[indice] = empleado[i].salario;
                    indice++;
                }
            }
            promedio = salarios.AsQueryable().Average();
            //se retornar el valor obtenido
            return promedio;
        }

        public int Minimo(int cantidadEmpleado)
        {
            //Funcion salario minimo 
            //Similar al anterior, utiliza un arreglo para capturar los salarios validos.
            //Despues, utiliza la funcion .Min(); para asignar el valor minimo dentro del nuevo arreglo con salarios validos.
            int retVal = 0;
            int[] salarios = new int[cantidadEmpleado];
            int indice = 0;
            for (int i = 0; i < empleado.Length; i++)
            {
                if (empleado[i].salario != -1)
                {
                    salarios[indice] = empleado[i].salario;
                    indice++;
                }
            }
            retVal = salarios.Min();
            return retVal;
        }

        public int Maximo(int cantidadEmpleado)
        {
            //Funcion salario maximo 
            //Similar al anterior, utiliza un arreglo para capturar los salarios validos.
            //Despues, utiliza la funcion .Max(); para asignar el maximo valor dentro del nuevo arreglo con salarios validos.
            int retVal = 0;
            int[] salarios = new int[cantidadEmpleado];
            int indice = 0;
            for (int i = 0; i < empleado.Length; i++)
            {
                if (empleado[i].salario != -1)
                {
                    salarios[indice] = empleado[i].salario;
                    indice++;
                }
            }
            retVal = salarios.Max();
            return retVal;
        }

        public void ListaEmpleados(int cantidadEmpleado)
        {
            //Metodo Lista Empleados
            //Similar a anteriores metodos, utiliza la estructura for para imprimir los datos de los objetos dentro del arreglo que contegan datos validos.
            Console.WriteLine($"Cantidad de empleados registrados: {cantidadEmpleado}");
            int control = 1;
            Console.WriteLine("======INICIO DEL REPORTE=======");
            for (int i = 0; i < empleado.Length; i++)
            {
                if (empleado[i].salario != -1)
                {
                    Console.WriteLine($"Empleado # {control}");
                    Console.WriteLine($"Cedula: {empleado[i].cedula}\nNombre: {empleado[i].nombre}\nDireccion: {empleado[i].direccion}");
                    Console.WriteLine($"Telefono: {empleado[i].telefono}\nSalario: {empleado[i].salario}");
                    control++;
                }
            }
            Console.WriteLine("======FIN DEL REPORTE=======");
            Console.WriteLine("\n presione cualquier tecla para continuar.");
            Console.ReadLine();
        }
        
        public string ValidarCedula(string cedula)
        {
            //Funcion validar cedula
            //La funcion retorna el valor de la cedula. Previo a ello, utiliza una estructura for para compararla con todas las cedulas dentro del arreglo
            //Si coincide, entra en una estructura while la cual no terminara hasta que se ingrese una cedula con valor diferente.
            string retVal = " ";
            for(int i = 0;i<empleado.Length;i++)
            {
                if(cedula == empleado[i].cedula)
                {
                    bool estado = true;
                    while (estado)
                    {
                        Console.WriteLine($"Problema de validacion. El #{cedula} de cedula ya esta en uso para el empleado {empleado[i].nombre}");
                        Console.WriteLine("Ingrese un numero de cedula valido: ");
                        cedula = Console.ReadLine();
                        if(cedula == empleado[i].cedula)
                        {
                            Console.WriteLine("Cambio no es posible. El numero de cedula es igual. Intente de nuevo");
                        }
                        else
                        {
                            Console.WriteLine("Cambio guardado");
                            estado = false;
                        }
                    }
                }
            }
            retVal = cedula;

            return retVal;
        }
        
        public int ValidarSalario(int salario)
        {
            //Funcion Validar Salario
            //Esta funcion retorna el valor del salario. En caso de detectar que el valor sea o o menor, entra en una estructura while la
            //cual no terminara hasta que se ingrese un salario mayor a cero
            int retVal = 0;
            bool estado = true;
            if (salario <= 0)
            {
                Console.WriteLine("Problemas de validacion");
                while (estado)
                {
                    Console.WriteLine($"El salario ingresado es negativo o igual a cero: {salario}");
                    Console.WriteLine("Ingrese un salario valido: ");
                    int.TryParse(Console.ReadLine(), out salario);
                    if(salario <= 0)
                    {
                        Console.WriteLine("Cambio invalido. El nuevo salario ingresado es negativo o igual a cero");
                        Console.WriteLine("Intente de nuevo");
                    }
                    else
                    {
                        Console.WriteLine("Validacion superada");
                        estado = false;
                    }
                }
            }
            retVal = salario;
            return retVal;
        }
       
        public bool ValidarEspacio()
        {
            //Funcion validar espacio
            //Retorna el estado true en caso que detecte valores en todos los espacios de un arreglo.
            bool retVal = false;
            int valores = 0;
            for(int i = 0; i < empleado.Length;i++)
            {
                if (empleado[i].salario != -1)
                {
                    valores++;
                }
            }
            if( valores == empleado.Length ) { retVal = true; }
            return retVal;
        }

        public void CrearArreglo()
        {
            //Funcion crear arreglo
            //Utiliza estructura for para crear una instancia de todos los objetos requeridos. Ademas, se construyen los objetos.
            for (int i = 0; i < empleado.Length; i++)
            {
                empleado[i] = new clsEmpleado();
            }
        }
        //========================================================
        //FIN
    }
}
