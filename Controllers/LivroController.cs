using Microsoft.AspNetCore.Mvc;
using WebApiBiblioteca.Model;
using WebApiBiblioteca.Repositorio;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WebApiBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LivroController : ControllerBase
    {
        private readonly LivroRepositorio _livroRepo;

        public LivroController(LivroRepositorio livroRepo)
        {
            _livroRepo = livroRepo;
        }

        [HttpGet]
        public ActionResult<List<Livro>> GetAll()
        {
            try
            {
                var livros = _livroRepo.GetAll();

                if (livros == null || !livros.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum livro encontrado." });
                }

                var listaComUrl = livros.Select(livro => new Livro
                {
                    Id = livro.Id,
                    Titulo = livro.Titulo,
                    Autor = livro.Autor,
                    AnoPublicado = livro.AnoPublicado,
                    FkCategoria = livro.FkCategoria,
                    Disponibilidade = livro.Disponibilidade,
                }).ToList();

                return Ok(listaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao obter os livros.", Detalhes = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Livro> GetById(int id)
        {
            try
            {
                var livro = _livroRepo.GetById(id);

                if (livro == null)
                {
                    return NotFound(new { Mensagem = "Livro não encontrado." });
                }

                return Ok(livro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao obter o livro.", Detalhes = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult<object> Post(Livro novoLivro)
        {
            try
            {
                var livro = new Livro
                {
                    Titulo = novoLivro.Titulo,
                    Autor = novoLivro.Autor,
                    AnoPublicado = novoLivro.AnoPublicado,
                    FkCategoria = novoLivro.FkCategoria,
                    Disponibilidade = novoLivro.Disponibilidade
                };

                _livroRepo.Add(livro);

                var resultado = new
                {
                    Mensagem = "Livro cadastrado com sucesso!",
                    Titulo = livro.Titulo,
                    Autor = livro.Autor,
                    AnoPublicado = livro.AnoPublicado,
                    FkCategoria = livro.FkCategoria,
                    Disponibilidade = livro.Disponibilidade
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao cadastrar o livro.", Detalhes = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, Livro livroAtualizado)
        {
            try
            {
                var livroExistente = _livroRepo.GetById(id);

                if (livroExistente == null)
                {
                    return NotFound(new { Mensagem = "Livro não encontrado." });
                }

                livroExistente.Titulo = livroAtualizado.Titulo;
                livroExistente.Autor = livroAtualizado.Autor;
                livroExistente.AnoPublicado = livroAtualizado.AnoPublicado;
                livroExistente.FkCategoria = livroAtualizado.FkCategoria;
                livroExistente.Disponibilidade = livroAtualizado.Disponibilidade;

                _livroRepo.Update(livroExistente);

                var resultado = new
                {
                    Mensagem = "Livro atualizado com sucesso!",
                    Titulo = livroExistente.Titulo,
                    Autor = livroExistente.Autor,
                    AnoPublicado = livroExistente.AnoPublicado,
                    FkCategoria = livroExistente.FkCategoria,
                    Disponibilidade = livroExistente.Disponibilidade,
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao atualizar o livro.", Detalhes = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var livroExistente = _livroRepo.GetById(id);

                if (livroExistente == null)
                {
                    return NotFound(new { Mensagem = "Livro não encontrado." });
                }

                _livroRepo.Delete(id);

                var resultado = new
                {
                    Mensagem = "Livro excluído com sucesso!",
                    Titulo = livroExistente.Titulo,
                    Autor = livroExistente.Autor,
                    AnoPublicado = livroExistente.AnoPublicado,
                    FkCategoria = livroExistente.FkCategoria,
                    Disponibilidade = livroExistente.Disponibilidade,
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao excluir o livro.", Detalhes = ex.Message });
            }
        }
    }
}
