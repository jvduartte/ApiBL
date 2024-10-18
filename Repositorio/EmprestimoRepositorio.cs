using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApiBiblioteca.Model;
using WebApiBiblioteca.ORM;




namespace WebApiBiblioteca.Repositorio
{
    public class EmprestimoRepositorio
    {
        private readonly BdBiblioetcadvjContext _context;

        public EmprestimoRepositorio(BdBiblioetcadvjContext context)
        {
            _context = context;
        }


        public void Add(Emprestimo emprestimo)
        {

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbEmprestimo = new TbEmprestimo()
            {

                Id = emprestimo.Id,
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataDevolucao = emprestimo.DataDevolucao,
                FkMembro = emprestimo.FkMembro,
                FkLivro = emprestimo.FkLivro,
            };


            // Adiciona a entidade ao contexto
            _context.Add(tbEmprestimo);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEmprestimo = _context.TbEmprestimos.FirstOrDefault(e => e.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbEmprestimo != null)
            {
                // Remove a entidade do contexto
                _context.TbEmprestimos.Remove(tbEmprestimo);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Emprestimo não encontrado.");
            }
        }


        public List<Emprestimo> GetAll()
        {
            List<Emprestimo> listFun = new List<Emprestimo>();

            var listTb = _context.TbEmprestimos.ToList();

            foreach (var item in listTb)
            {
                var emprestimo = new Emprestimo
                {
                    Id = item.Id,
                    DataEmprestimo = item.DataEmprestimo,
                    DataDevolucao = item.DataDevolucao,
                    FkMembro = item.FkMembro,
                    FkLivro = item.FkLivro,
                };

                listFun.Add(emprestimo);
            }

            return listFun;
        }


        public Emprestimo GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbEmprestimos.FirstOrDefault(e => e.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var emprestimo = new Emprestimo
            {
                Id = item.Id,
                DataEmprestimo = item.DataEmprestimo,
                DataDevolucao = item.DataDevolucao,
                FkMembro = item.FkMembro,
                FkLivro = item.FkLivro,
            };

            return emprestimo; // Retorna o funcionário encontrado
        }


        public void Update(Emprestimo emprestimo)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEmprestimo = _context.TbEmprestimos.FirstOrDefault(e => e.Id == emprestimo.Id);

            // Verifica se a entidade foi encontrada
            if (tbEmprestimo != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbEmprestimo.DataEmprestimo = emprestimo.DataEmprestimo;
                tbEmprestimo.DataDevolucao = emprestimo.DataDevolucao;
                tbEmprestimo.FkMembro = emprestimo.FkMembro;
                tbEmprestimo.FkLivro = emprestimo.FkLivro;


                // Atualiza as informações no contexto
                _context.TbEmprestimos.Update(tbEmprestimo);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Emprestimo não encontrado.");
            }
        }


    }
}
