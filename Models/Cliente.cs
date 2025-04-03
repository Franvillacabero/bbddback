namespace Models {

    public class Cliente
    {
        public int Id_Cliente { get; set; }
        public string Nombre_Empresa { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public Cliente() { }

        public Cliente(int id_Cliente, string nombre_Empresa, DateTime fechaRegistro)
        {
            Id_Cliente = id_Cliente;
            Nombre_Empresa = nombre_Empresa;
            FechaRegistro = fechaRegistro;
        }

        public void MostrarDetalles()
        {
            Console.WriteLine($"ID: {Id_Cliente}, Nombre Empresa: {Nombre_Empresa}, Fecha de Registro: {FechaRegistro}");
        }
    }
}