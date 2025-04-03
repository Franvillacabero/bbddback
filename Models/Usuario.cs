namespace Models
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public DateTime Fecha_Registro { get; set; } = DateTime.Now;


        public Usuario() { }

        public Usuario(int id_Usuario, string nombre, string contraseña, DateTime fecha_Registro)
        {
            Id_Usuario = id_Usuario;
            Nombre = nombre;
            Contraseña = contraseña;
            Fecha_Registro = fecha_Registro;
        }

        public void MostrarDetalles()
        {
            Console.WriteLine($"ID: {Id_Usuario}, Nombre: {Nombre}, Contraseña: {Contraseña}, Fecha de Registro: {Fecha_Registro}");
        }
    }
}