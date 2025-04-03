namespace Models
{
    public class TipoServicio
    {
        public int Id_TipoServicio { get; set; }
        public string Nombre { get; set; } = string.Empty;

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