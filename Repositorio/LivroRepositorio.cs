using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApiBiblioteca.Model;
using WebApiBiblioteca.ORM;




namespace WebApiBiblioteca.Repositorio
{
    public class LivroRepositorio
    {
        private readonly BdBiblioetcadvjContext _context;

        public LivroRepositorio(BdBiblioetcadvjContext context)
        {
            _context = context;
        }


        public void Add(Livro livro)
        {

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbLivro = new TbLivro()
            {

                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                AnoPublicado = livro.AnoPublicado,
                FkCategoria = livro.FkCategoria,
                Disponibilidade = livro.Disponibilidade
            };


            // Adiciona a entidade ao contexto
            _context.Add(tbLivro);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbLivro = _context.TbLivros.FirstOrDefault(c => c.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbLivro != null)
            {
                // Remove a entidade do contexto
                _context.TbLivros.Remove(tbLivro);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Livro não encontrado.");
            }
        }


        public List<Livro> GetAll()
        {
            List<Livro> listFun = new List<Livro>();

            var listTb = _context.TbLivros.ToList();

            foreach (var item in listTb)
            {
                var livro = new Livro
                {
                    Id = item.Id,
                    Titulo = item.Titulo,
                    Autor = item.Autor,
                    AnoPublicado = item.AnoPublicado,
                    FkCategoria = item.FkCategoria,
                    Disponibilidade = item.Disponibilidade,

                };

                listFun.Add(livro);
            }

            return listFun;
        }


        public Livro GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbLivros.FirstOrDefault(c => c.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var livro = new Livro
            {
                Id = item.Id,
                Titulo = item.Titulo,
                Autor = item.Autor,
                AnoPublicado = item.AnoPublicado,
                FkCategoria = item.FkCategoria,
                Disponibilidade = item.Disponibilidade,

            };

            return livro; // Retorna o funcionário encontrado
        }


        public void Update(Livro livro)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbLivro = _context.TbLivros.FirstOrDefault(l => l.Id == livro.Id);

            // Verifica se a entidade foi encontrada
            if (tbLivro != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbLivro.Titulo = livro.Titulo;
                tbLivro.Autor = livro.Autor;
                tbLivro.AnoPublicado = livro.AnoPublicado;
                tbLivro.FkCategoria = livro.FkCategoria;
                tbLivro.Disponibilidade = livro.Disponibilidade;




                // Atualiza as informações no contexto
                _context.TbLivros.Update(tbLivro);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Livro não encontrado.");
            }
        }


    }
}
