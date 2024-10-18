using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApiBiblioteca.Model;
using WebApiBiblioteca.ORM;




namespace WebApiBiblioteca.Repositorio
{
    public class ReservaRepositorio
    {
        private readonly BdBiblioetcadvjContext _context;

        public ReservaRepositorio(BdBiblioetcadvjContext context)
        {
            _context = context;
        }


        public void Add(Reserva reserva)
        {

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbReserva = new TbReserva()
            {

                Id = reserva.Id,
                DataReserva = reserva.DataReserva,
                FkMembro = reserva.FkMembro,
                FkLivro = reserva.FkLivro,
            };


            // Adiciona a entidade ao contexto
            _context.Add(tbReserva);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbReserva = _context.TbReservas.FirstOrDefault(c => c.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbReserva != null)
            {
                // Remove a entidade do contexto
                _context.TbReservas.Remove(tbReserva);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Reserva não encontrado.");
            }
        }


        public List<Reserva> GetAll()
        {
            List<Reserva> listFun = new List<Reserva>();

            var listTb = _context.TbReservas.ToList();

            foreach (var item in listTb)
            {
                var reserva = new Reserva
                {
                    Id = item.Id,
                    DataReserva = item.DataReserva,
                    FkMembro = item.FkMembro,
                    FkLivro = item.FkLivro,
                };

                listFun.Add(reserva);
            }

            return listFun;
        }


        public Reserva GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbReservas.FirstOrDefault(r => r.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var reserva = new Reserva
            {
                Id = item.Id,
                DataReserva = item.DataReserva,
                FkMembro = item.FkMembro,
                FkLivro = item.FkLivro,
            };

            return reserva; // Retorna o funcionário encontrado
        }


        public void Update(Reserva reserva)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbreserva = _context.TbReservas.FirstOrDefault(r => r.Id == reserva.Id);

            // Verifica se a entidade foi encontrada
            if (tbreserva != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbreserva.DataReserva = reserva.DataReserva;
                tbreserva.FkMembro = reserva.FkMembro;
                tbreserva.FkMembro = reserva.FkLivro;




                // Atualiza as informações no contexto
                _context.TbReservas.Update(tbreserva);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Reserva não encontrado.");
            }
        }


    }
}
