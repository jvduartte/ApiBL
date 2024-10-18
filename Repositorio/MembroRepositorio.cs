using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApiBiblioteca.Model;
using WebApiBiblioteca.ORM;




namespace WebApiBiblioteca.Repositorio
{
    public class MembroRepositorio
    {
        private readonly BdBiblioetcadvjContext _context;

        public MembroRepositorio(BdBiblioetcadvjContext context)
        {
            _context = context;
        }


        public void Add(Membro membro)
        {

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var TbMembro = new TbMembro()
            {

                Nome = membro.Nome,
                Email = membro.Email,
                Telefone = membro.Telefone,
                DataCadastro = membro.DataCadastro,
                TipoMembro = membro.TipoMembro,
            };


            // Adiciona a entidade ao contexto
            _context.Add(TbMembro);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbMembro = _context.TbMembros.FirstOrDefault(c => c.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbMembro != null)
            {
                // Verifica se existem reservas relacionadas ao membro
                var tbReserva = _context.TbReservas.FirstOrDefault(c => c.FkMembro == id);
                if (tbReserva != null)
                {
                    throw new Exception("Não é possível excluir o membro, pois ele possui reservas associadas.");
                }

                // Verifica se existem empréstimos relacionados ao membro
                var tbEmprestimo = _context.TbEmprestimos.FirstOrDefault(c => c.FkMembro == id);
                if (tbEmprestimo != null)
                {
                    throw new Exception("Não é possível excluir o membro, pois ele possui empréstimos associados.");
                }

                // Remove a entidade do contexto
                _context.TbMembros.Remove(tbMembro);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Membro não encontrado.");
            }
        }



        public List<Membro> GetAll()
        {
            List<Membro> listFun = new List<Membro>();

            var listTb = _context.TbMembros.ToList();

            foreach (var item in listTb)
            {
                var membro = new Membro
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Email = item.Email,
                    Telefone = item.Telefone,
                    DataCadastro = item.DataCadastro,
                    TipoMembro = item.TipoMembro,


                };

                listFun.Add(membro);
            }

            return listFun;
        }


        public Membro GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbMembros.FirstOrDefault(c => c.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var membro = new Membro
            {
                Id = item.Id,
                Nome = item.Nome,
                Email = item.Email,
                Telefone = item.Telefone,
                DataCadastro = item.DataCadastro,
                TipoMembro = item.TipoMembro,
            };

            return membro; // Retorna o funcionário encontrado
        }


        public void Update(Membro membro)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbMembro = _context.TbMembros.FirstOrDefault(m => m.Id == membro.Id);

            // Verifica se a entidade foi encontrada
            if (tbMembro != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbMembro.Nome = membro.Nome;
                tbMembro.Email = membro.Email;
                tbMembro.Telefone = membro.Telefone;
                tbMembro.DataCadastro = membro.DataCadastro;
                tbMembro.TipoMembro = membro.TipoMembro;


                // Atualiza as informações no contexto
                _context.TbMembros.Update(tbMembro);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Membro não encontrado.");
            }
        }


    }
}
