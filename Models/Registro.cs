namespace Models
{
    public class Registro
    {

        public int Id_Registro { get; set; }
        public int Id_Tipo_Servicio { get; set; }  // Clave foránea
        public string Usuario { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public string? Notas { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Relación con TipoServicio
        public TipoServicio TipoServicio { get; set; } = null!;

        public Registro() { }

        public Registro(int id_Registro, int id_Tipo_Servicio, string usuario, string contraseña, string? notas, DateTime fechaCreacion)
        {
            Id_Registro = id_Registro;
            Id_Tipo_Servicio = id_Tipo_Servicio;
            Usuario = usuario;
            Contraseña = contraseña;
            Notas = notas;
            FechaCreacion = fechaCreacion;
        }
        
        public void MostrarDetalles()
        {
            Console.WriteLine($"ID: {Id_Registro}, ID Tipo Servicio: {Id_Tipo_Servicio}, Usuario: {Usuario}, Contraseña: {Contraseña}, Notas: {Notas}, Fecha de Creación: {FechaCreacion}");
        }
    }
}