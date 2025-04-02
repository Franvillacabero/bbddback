namespace Models
{
    public class TipoServicio
    {
        public int Id_TipoServicio { get; set; }
        public string Nombre { get; set; } = string.Empty;

        // Relación con Registros (Un TipoServicio tiene muchos Registros)
        public List<Registro> Registros { get; set; } = new();

        public TipoServicio() { }

        public TipoServicio(int id_TipoServicio, string nombre)
        {
            Id_TipoServicio = id_TipoServicio;
            Nombre = nombre;
        }

        public void MostrarDetalles()
        {
            Console.WriteLine($"ID: {Id_TipoServicio}, Nombre: {Nombre}");
        }
    }
}