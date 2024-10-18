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
    public class ReservaController : ControllerBase
    {
        private readonly ReservaRepositorio _reservaRepo;

        public ReservaController(ReservaRepositorio reservaRepo)
        {
            _reservaRepo = reservaRepo;
        }

        [HttpGet]
        public ActionResult<List<Reserva>> GetAll()
        {
            try
            {
                var reservas = _reservaRepo.GetAll();

                if (reservas == null || !reservas.Any())
                {
                    return NotFound(new { Mensagem = "Nenhuma reserva encontrada." });
                }

                var listaComUrl = reservas.Select(reserva => new Reserva
                {
                    Id = reserva.Id,
                    DataReserva = reserva.DataReserva,
                    FkMembro = reserva.FkMembro,
                    FkLivro = reserva.FkLivro,
                }).ToList();

                return Ok(listaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao obter as reservas.", Detalhes = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Reserva> GetById(int id)
        {
            try
            {
                var reserva = _reservaRepo.GetById(id);

                if (reserva == null)
                {
                    return NotFound(new { Mensagem = "Reserva não encontrada." });
                }

                return Ok(reserva);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao obter a reserva.", Detalhes = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult<object> Post(Reserva novaReserva)
        {
            try
            {
                var reserva = new Reserva
                {
                    DataReserva = novaReserva.DataReserva,
                    FkMembro = novaReserva.FkMembro,
                    FkLivro = novaReserva.FkLivro
                };

                _reservaRepo.Add(reserva);

                var resultado = new
                {
                    Mensagem = "Reserva cadastrada com sucesso!",
                    DataReserva = reserva.DataReserva,
                    FkMembro = reserva.FkMembro,
                    FkLivro = reserva.FkLivro,
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao cadastrar a reserva.", Detalhes = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, Reserva reservaAtualizado)
        {
            try
            {
                var reservaExistente = _reservaRepo.GetById(id);

                if (reservaExistente == null)
                {
                    return NotFound(new { Mensagem = "Reserva não encontrada." });
                }

                reservaExistente.DataReserva = reservaAtualizado.DataReserva;
                reservaExistente.FkMembro = reservaAtualizado.FkMembro;
                reservaExistente.FkLivro = reservaAtualizado.FkLivro;

                _reservaRepo.Update(reservaExistente);

                var resultado = new
                {
                    Mensagem = "Reserva atualizada com sucesso!",
                    DataReserva = reservaExistente.DataReserva,
                    FkMembro = reservaExistente.FkMembro,
                    FkLivro = reservaExistente.FkLivro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao atualizar a reserva.", Detalhes = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var reservaExistente = _reservaRepo.GetById(id);

                if (reservaExistente == null)
                {
                    return NotFound(new { Mensagem = "Reserva não encontrada." });
                }

                _reservaRepo.Delete(id);

                var resultado = new
                {
                    Mensagem = "Reserva excluída com sucesso!",
                    DataReserva = reservaExistente.DataReserva,
                    FkMembro = reservaExistente.FkMembro,
                    FkLivro = reservaExistente.FkLivro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao excluir a reserva.", Detalhes = ex.Message });
            }
        }
    }
}
