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
    public class MembroController : ControllerBase
    {
        private readonly MembroRepositorio _membroRepo;

        public MembroController(MembroRepositorio membroRepo)
        {
            _membroRepo = membroRepo;
        }

        // GET: api/<MembroController>
        [HttpGet]
        public ActionResult<List<Membro>> GetAll()
        {
            try
            {
                var membros = _membroRepo.GetAll();

                if (membros == null || !membros.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum membro encontrado." });
                }

                var listaComUrl = membros.Select(membro => new Membro
                {
                    Id = membro.Id,
                    Nome = membro.Nome,
                    Email = membro.Email,
                    Telefone = membro.Telefone,
                    DataCadastro = membro.DataCadastro,
                    TipoMembro = membro.TipoMembro,
                }).ToList();

                return Ok(listaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao obter os membros.", Detalhes = ex.Message });
            }
        }

        // GET api/<MembroController>/5
        [HttpGet("{id}")]
        public ActionResult<Membro> GetById(int id)
        {
            try
            {
                var membro = _membroRepo.GetById(id);

                if (membro == null)
                {
                    return NotFound(new { Mensagem = "Membro não encontrado." });
                }

                var membroComUrl = new Membro
                {
                    Id = membro.Id,
                    Nome = membro.Nome,
                    Email = membro.Email,
                    Telefone = membro.Telefone,
                    DataCadastro = membro.DataCadastro,
                    TipoMembro = membro.TipoMembro,
                };

                return Ok(membroComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao obter o membro.", Detalhes = ex.Message });
            }
        }

        // POST api/<MembroController>
        [HttpPost]
        public ActionResult<object> Post(Membro novoMembro)
        {
            try
            {
                var membro = new Membro
                {
                    Nome = novoMembro.Nome,
                    Email = novoMembro.Email,
                    Telefone = novoMembro.Telefone,
                    DataCadastro = novoMembro.DataCadastro,
                    TipoMembro = novoMembro.TipoMembro,
                };

                _membroRepo.Add(membro);

                var resultado = new
                {
                    Mensagem = "Membro cadastrado com sucesso!",
                    Nome = membro.Nome,
                    Email = membro.Email,
                    Telefone = membro.Telefone,
                    DataCadastro = membro.DataCadastro,
                    TipoMembro = membro.TipoMembro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao cadastrar o membro.", Detalhes = ex.Message });
            }
        }

        // PUT api/<MembroController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, Membro membroAtualizado)
        {
            try
            {
                var membroExistente = _membroRepo.GetById(id);

                if (membroExistente == null)
                {
                    return NotFound(new { Mensagem = "Membro não encontrado." });
                }

                membroExistente.Nome = membroAtualizado.Nome;
                membroExistente.Email = membroAtualizado.Email;
                membroExistente.Telefone = membroAtualizado.Telefone;
                membroExistente.DataCadastro = membroAtualizado.DataCadastro;
                membroExistente.TipoMembro = membroAtualizado.TipoMembro;

                _membroRepo.Update(membroExistente);

                var resultado = new
                {
                    Mensagem = "Membro atualizado com sucesso!",
                    Nome = membroExistente.Nome,
                    Email = membroExistente.Email,
                    Telefone = membroExistente.Telefone,
                    DataCadastro = membroExistente.DataCadastro,
                    TipoMembro = membroExistente.TipoMembro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao atualizar o membro.", Detalhes = ex.Message });
            }
        }

        // DELETE api/<MembroController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var membroExistente = _membroRepo.GetById(id);

                if (membroExistente == null)
                {
                    return NotFound(new { Mensagem = "Membro não encontrado." });
                }

                // Chama o método de delete que já contém a lógica de verificação
                _membroRepo.Delete(id);

                var resultado = new
                {
                    Mensagem = "Membro excluído com sucesso!",
                    Nome = membroExistente.Nome,
                    Email = membroExistente.Email,
                    Telefone = membroExistente.Telefone,
                    DataCadastro = membroExistente.DataCadastro,
                    TipoMembro = membroExistente.TipoMembro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao excluir o membro.", Detalhes = ex.Message });
            }
        }

    }
}
