namespace Models
{
    public class Campo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public Campo() { }

        public Campo(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public void MostrarDetalles()
        {
            Console.WriteLine($"ID: {Id}, Nombre: {Nombre}");
        }
    }
}