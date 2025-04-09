namespace Models {

    public class Cliente
    {
        public int Id_Cliente { get; set; }
        public string Nombre_Empresa { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public string? Notas { get; set; } = string.Empty;

        public Cliente() { }

        public Cliente(int id_Cliente, string nombre_Empresa, DateTime fechaRegistro, string? notas = null)
        {
            Id_Cliente = id_Cliente;
            Nombre_Empresa = nombre_Empresa;
            FechaRegistro = fechaRegistro;
            Notas = notas;
        }

        public void MostrarDetalles()
        {
            Console.WriteLine($"ID Cliente: {Id_Cliente}");
            Console.WriteLine($"Nombre Empresa: {Nombre_Empresa}");
            Console.WriteLine($"Fecha Registro: {FechaRegistro}");
            Console.WriteLine($"Notas: {Notas}");
        }
    }
}