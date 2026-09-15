namespace PruebaTecnica.Models
{
    public class Movimiento
    {
        public int Id { get; set; }

        public int CuentaId { get; set; }

        public decimal Monto { get; set; }

        public string Tipo { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

    }
}
