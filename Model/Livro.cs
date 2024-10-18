namespace WebApiBiblioteca.Model
{
    public class Livro
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string Autor { get; set; } = null!;

        public int AnoPublicado { get; set; }

        public int FkCategoria { get; set; }

        public bool Disponibilidade { get; set; }

    }
}
