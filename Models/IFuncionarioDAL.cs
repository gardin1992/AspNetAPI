using System;
using System.Collections.Generic;

namespace WebApplication3.Models
{
    interface IFuncionarioDAL
    {
        public IEnumerable<Funcionario> GetAllFuncionarios();
        public void AddFuncionario(Funcionario funcionario);
        public void UpdateFuncionario(Funcionario funcionario);
        Funcionario GetFuncionario(int? id);
        public void DeleteFuncionario(int? id);
    }
}
