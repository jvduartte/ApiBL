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
    public class EmprestimoController : ControllerBase
    {
        private readonly EmprestimoRepositorio _emprestimoRepo;

        public EmprestimoController(EmprestimoRepositorio emprestimoRepo)
        {
            _emprestimoRepo = emprestimoRepo;
        }

        [HttpGet]
        public ActionResult<List<Emprestimo>> GetAll()
        {
            try
            {
                var emprestimos = _emprestimoRepo.GetAll();

                if (emprestimos == null || !emprestimos.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum empréstimo encontrado." });
                }

                var listaComUrl = emprestimos.Select(emprestimo => new Emprestimo
                {
                    Id = emprestimo.Id,
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataDevolucao = emprestimo.DataDevolucao,
                    FkMembro = emprestimo.FkMembro,
                    FkLivro = emprestimo.FkLivro,
                }).ToList();

                return Ok(listaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao obter os empréstimos.", Detalhes = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Emprestimo> GetById(int id)
        {
            try
            {
                var emprestimo = _emprestimoRepo.GetById(id);

                if (emprestimo == null)
                {
                    return NotFound(new { Mensagem = "Empréstimo não encontrado." });
                }

                return Ok(emprestimo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao obter o empréstimo.", Detalhes = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult<object> Post(Emprestimo novoEmprestimo)
        {
            try
            {
                var emprestimo = new Emprestimo
                {
                    DataEmprestimo = novoEmprestimo.DataEmprestimo,
                    DataDevolucao = novoEmprestimo.DataDevolucao,
                    FkMembro = novoEmprestimo.FkMembro,
                    FkLivro = novoEmprestimo.FkLivro
                };

                _emprestimoRepo.Add(emprestimo);

                var resultado = new
                {
                    Mensagem = "Empréstimo cadastrado com sucesso!",
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataDevolucao = emprestimo.DataDevolucao,
                    FkMembro = emprestimo.FkMembro,
                    FkLivro = emprestimo.FkLivro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao cadastrar o empréstimo.", Detalhes = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, Emprestimo emprestimoAtualizado)
        {
            try
            {
                var emprestimoExistente = _emprestimoRepo.GetById(id);

                if (emprestimoExistente == null)
                {
                    return NotFound(new { Mensagem = "Empréstimo não encontrado." });
                }

                emprestimoExistente.DataEmprestimo = emprestimoAtualizado.DataEmprestimo;
                emprestimoExistente.DataDevolucao = emprestimoAtualizado.DataDevolucao;
                emprestimoExistente.FkMembro = emprestimoAtualizado.FkMembro;
                emprestimoExistente.FkLivro = emprestimoAtualizado.FkLivro;

                _emprestimoRepo.Update(emprestimoExistente);

                var resultado = new
                {
                    Mensagem = "Empréstimo atualizado com sucesso!",
                    DataEmprestimo = emprestimoExistente.DataEmprestimo,
                    DataDevolucao = emprestimoExistente.DataDevolucao,
                    FkMembro = emprestimoExistente.FkMembro,
                    FkLivro = emprestimoExistente.FkLivro,
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao atualizar o empréstimo.", Detalhes = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var emprestimoExistente = _emprestimoRepo.GetById(id);

                if (emprestimoExistente == null)
                {
                    return NotFound(new { Mensagem = "Empréstimo não encontrado." });
                }

                _emprestimoRepo.Delete(id);

                var resultado = new
                {
                    Mensagem = "Empréstimo excluído com sucesso!",
                    DataEmprestimo = emprestimoExistente.DataEmprestimo,
                    DataDevolucao = emprestimoExistente.DataDevolucao,
                    FkMembro = emprestimoExistente.FkMembro,
                    FkLivro = emprestimoExistente.FkLivro,
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao excluir o empréstimo.", Detalhes = ex.Message });
            }
        }
    }
}
