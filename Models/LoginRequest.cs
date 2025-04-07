namespace Models
{
    public class LoginRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;

        public LoginRequest() { }

        public LoginRequest(string nombre, string contraseña)
        {
            Nombre = nombre;
            Contraseña = contraseña;
        }

        public void MostrarDetalles()
        {
            Console.WriteLine($"Nombre: {Nombre}, Contraseña: {Contraseña}");
        }
    }
}