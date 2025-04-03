namespace Models
{
    public class Registro
    {

        public int Id_Registro { get; set; }
        public int Id_Cliente { get; set; }  // Clave foránea
        public int Id_TipoServicio { get; set; }  // Clave foránea
        public string Usuario { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public string? Notas { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public Registro() { }

        public Registro(int id_Registro, int id_Cliente, int id_TipoServicio, string usuario, string contraseña, string? notas, DateTime fechaCreacion)
        {
            Id_Registro = id_Registro;
            Id_Cliente = id_Cliente;
            Id_TipoServicio = id_TipoServicio;
            Usuario = usuario;
            Contraseña = contraseña;
            Notas = notas;
            FechaCreacion = fechaCreacion;
        }

        public void MostrarDetalles()
        {
            Console.WriteLine($"ID: {Id_Registro}, ID Cliente: {Id_Cliente}, ID Tipo Servicio: {Id_TipoServicio}, Usuario: {Usuario}, Contraseña: {Contraseña}, Notas: {Notas}, Fecha de Creación: {FechaCreacion}");
        }
    }
}