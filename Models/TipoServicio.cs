namespace Models
{
    public class TipoServicio
    {
        public int Id_TipoServicio { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<int> Campos { get; set; } = new List<int>(); // Lista de IDs de campos

        public TipoServicio() { }

        public TipoServicio(int id_TipoServicio, string nombre, List<int> campos)
        {
            Id_TipoServicio = id_TipoServicio;
            Nombre = nombre;
            Campos = campos;
        }

        public void MostrarDetalles()
        {
            Console.WriteLine($"ID: {Id_TipoServicio}, Nombre: {Nombre}, Campos: {Campos.Count}");
        }
    }
}