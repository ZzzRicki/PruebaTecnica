using System.Collections.Generic;
using PruebaTecnica.Models; // <- sustituye por el namespace real donde existe Movimiento
namespace PruebaTecnica.Models
{
    public class CrearCuenta
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public string NombreSocio { get; set; } = string.Empty;
        public decimal SaldoInicial { get; set; }
        public List<Movimiento> Movimientos { get; set; } = new();
    }
}
