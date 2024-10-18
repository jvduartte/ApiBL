using Microsoft.AspNetCore.Mvc;
using WebApiBiblioteca.Model;
using WebApiBiblioteca.Repositorio;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WP_BibliotecaDVJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaRepositorio _categoriaRepo;

        public CategoriaController(CategoriaRepositorio categoriaRepo)
        {
            _categoriaRepo = categoriaRepo;
        }

        [HttpGet]
        public ActionResult<List<Categoria>> GetAll()
        {
            try
            {
                var categorias = _categoriaRepo.GetAll();

                if (categorias == null || !categorias.Any())
                {
                    return NotFound(new { Mensagem = "Nenhuma categoria encontrada." });
                }

                var listaComUrl = categorias.Select(categoria => new Categoria
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    Descricao = categoria.Descricao,
                }).ToList();

                return Ok(listaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao obter as categorias.", Detalhes = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Categoria> GetById(int id)
        {
            try
            {
                var categoria = _categoriaRepo.GetById(id);

                if (categoria == null)
                {
                    return NotFound(new { Mensagem = "Categoria não encontrada." });
                }

                var categoriaComUrl = new Categoria
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    Descricao = categoria.Descricao,
                };

                return Ok(categoriaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao obter a categoria.", Detalhes = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult<object> Post([FromForm] Categoria novaCategoria)
        {
            try
            {
                var categoria = new Categoria
                {
                    Nome = novaCategoria.Nome,
                    Descricao = novaCategoria.Descricao
                };

                _categoriaRepo.Add(categoria);

                var resultado = new
                {
                    Mensagem = "Categoria cadastrada com sucesso!",
                    Nome = categoria.Nome,
                    Descricao = categoria.Descricao
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao cadastrar a categoria.", Detalhes = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] CategoriaDto categoriaAtualizada)
        {
            try
            {
                var categoriaExistente = _categoriaRepo.GetById(id);

                if (categoriaExistente == null)
                {
                    return NotFound(new { Mensagem = "Categoria não encontrada." });
                }

                categoriaExistente.Nome = categoriaAtualizada.Nome;
                categoriaExistente.Descricao = categoriaAtualizada.Descricao;

                _categoriaRepo.Update(categoriaExistente);

                var resultado = new
                {
                    Mensagem = "Categoria atualizada com sucesso!",
                    Nome = categoriaExistente.Nome,
                    Descricao = categoriaExistente.Descricao,
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao atualizar a categoria.", Detalhes = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoriaExistente = _categoriaRepo.GetById(id);

                if (categoriaExistente == null)
                {
                    return NotFound(new { Mensagem = "Categoria não encontrada." });
                }

                _categoriaRepo.Delete(id);

                var resultado = new
                {
                    Mensagem = "Categoria excluída com sucesso!",
                    Nome = categoriaExistente.Nome,
                    Descricao = categoriaExistente.Descricao
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao excluir a categoria.", Detalhes = ex.Message });
            }
        }
    }
}
