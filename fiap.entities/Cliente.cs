using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fiap.entities
{
    public class Cliente
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Fone { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public string DDD { get; set; }
        public TipoTelefone? Tipo { get; set; }
    }

    public enum TipoTelefone { Celular, Comercial, Residencial, Recados }
}
