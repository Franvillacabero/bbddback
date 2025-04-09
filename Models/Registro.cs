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
        public string? Url { get; set; } = string.Empty;
        public string? Url_2 { get; set; } = string.Empty;
        public string? Isp { get; set; } = string.Empty;
        public string? Nombre_BBDD { get; set; } = string.Empty;

        public Registro() { }

        public Registro(int id_Registro, int id_Cliente, int id_TipoServicio, string usuario, string contraseña, string? notas, string? url, string? url_2, string? isp, string? nombre_BBDD, DateTime fechaCreacion)
        {
            Id_Registro = id_Registro;
            Id_Cliente = id_Cliente;
            Id_TipoServicio = id_TipoServicio;
            Usuario = usuario;
            Contraseña = contraseña;
            Notas = notas;
            FechaCreacion = fechaCreacion;
            Url = url;
            Url_2 = url_2;
            Isp = isp;
            Nombre_BBDD = nombre_BBDD;
        }

        public void MostrarDetalles() {
            Console.WriteLine($"ID Registro: {Id_Registro}");
            Console.WriteLine($"ID Cliente: {Id_Cliente}");
            Console.WriteLine($"ID Tipo Servicio: {Id_TipoServicio}");
            Console.WriteLine($"Usuario: {Usuario}");
            Console.WriteLine($"Contraseña: {Contraseña}");
            Console.WriteLine($"Notas: {Notas}");
            Console.WriteLine($"URL: {Url}");
            Console.WriteLine($"URL 2: {Url_2}");
            Console.WriteLine($"ISP: {Isp}");
            Console.WriteLine($"Nombre BBDD: {Nombre_BBDD}");
            Console.WriteLine($"Fecha Creación: {FechaCreacion}");
        }
    }
}