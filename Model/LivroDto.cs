namespace WebApiBiblioteca.Model
{
    public class LivroDto
    {
        public string Titulo { get; set; } = null!;

        public string Autor { get; set; } = null!;

        public int AnoPublicado { get; set; }

        public int FkCategoria { get; set; }

        public bool Disponibilidade { get; set; }
    }
}
