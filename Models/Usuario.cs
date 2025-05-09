namespace Models
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public DateTime Fecha_Registro { get; set; } = DateTime.Now;
        public bool EsAdmin { get; set; }
        public List<int> Clientes { get; set; } = new List<int>();  // Cambiado a lista de IDs de cliente
        public bool EsEditor { get; set; }

        public Usuario() { }

        public Usuario(int id_Usuario, string nombre, string contraseña, DateTime fecha_Registro, bool esAdmin, List<int> clientes, bool esEditor)
        {
            Id_Usuario = id_Usuario;
            Nombre = nombre;
            Contraseña = contraseña;
            Fecha_Registro = fecha_Registro;
            EsAdmin = esAdmin;
            Clientes = clientes;
            EsEditor = esEditor;
        }

        public void MostrarDetalles()
        {
            Console.WriteLine($"ID: {Id_Usuario}, Nombre: {Nombre}, Fecha de Registro: {Fecha_Registro}, Es Admin: {EsAdmin}, Clientes: {Clientes.Count},Es Editor: {EsEditor}");
        }
    }
}
