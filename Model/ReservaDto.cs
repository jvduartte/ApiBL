namespace WebApiBiblioteca.Model
{
    public class ReservaDto
    {
        public DateTime DataReserva { get; set; }

        public int FkMembro { get; set; }

        public int FkLivro { get; set; }
    }
}
