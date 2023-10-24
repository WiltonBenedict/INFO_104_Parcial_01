using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//10/24/2023. Wilton Benedict
//UH. Prueba Parcial I. INFO-104
namespace INFO_104_Parcial_01
{
    internal class clsEmpleado
    {
        //Atributos de la clase junto a los metodos get; y set;
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string direccion {  get; set; }
        public string telefono { get; set; }
        public int salario { get; set; }


        //Constructor
        public clsEmpleado()
        {
            cedula = "";
            nombre = "";
            direccion = "";
            telefono = "";
            salario = -1; //Significa que el espacio en el arreglo esta disponible
        }
    }
}
