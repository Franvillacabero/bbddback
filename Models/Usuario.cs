namespace Models
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public string Correo { get; set; }
        public DateTime Fecha_Registro { get; set; } = DateTime.Now;


        public Usuario() { }

        public Usuario(int id_usuarios, string nombre, string apellido, string correo, string contraseña, DateTime fechaCreacion, DateTime fechaModificacion)
        {
            Id_Usuario = id_usuarios;
            Nombre = nombre;
            Correo = correo;
            Contraseña = contraseña;
            Fecha_Registro = fechaCreacion;

        }

        public void MostrarDetalles()
        {
            Console.WriteLine($"ID: {Id_Usuario}, Nombre: {Nombre}, Correo: {Correo}, Contraseña: {Contraseña}, Fecha de Creación: {Fecha_Registro}");
        }
    }
}