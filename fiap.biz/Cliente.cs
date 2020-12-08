using fiap.data;
using fiap.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace fiap.biz
{
    public class Cliente
    {

        private readonly Manager _manager;
        public Cliente()
        {
            _manager = new Manager(HttpContext.Current);
        }
        public static entities.Cliente GetByCPF(string cpf)
        {
            Cliente mana = new Cliente();
            var query = new StringBuilder();

            query.AppendLine($@"SELECT * FROM Cliente where CPF = '{cpf.Trim()}'");

            return ConvertData(mana._manager.SqlQuery(query.ToString())).FirstOrDefault();
        }
        public static entities.Cliente GetByCodgo(string codigo)
        {
            Cliente mana = new Cliente();
            var query = new StringBuilder();

            query.AppendLine($@"SELECT * FROM Cliente where Codigo_cliente = '{codigo.Trim()}'");

            return ConvertData(mana._manager.SqlQuery(query.ToString())).FirstOrDefault();
        }
        public static string Save(entities.Cliente item)
        {
            Cliente mana = new Cliente();

            if (string.IsNullOrWhiteSpace(item.CPF))
                    throw new BusinessException("O CPF  não é válido. Por favor entre com um número de CPF válido.");

            if (!biz.Cliente.CheckCPF(item.CPF))
                throw new BusinessException(string.Format("O CPF '{0}' não é inválido. Por favor entre com um número de CPF válido.", item.CPF));

            var cliente = GetByCPF(item.CPF);

            if(cliente != null && (!string.IsNullOrEmpty(cliente.CPF)))
                throw new BusinessException(string.Format("já existe um cadastro para esse CPF; ", item.CPF));


            var codigo = GerarCodigo();
            var existe = GetByCodgo(codigo);
                
            if(existe != null)
                codigo = GerarCodigo();

            var query = new StringBuilder();

            query.AppendLine($@"INSERT INTO CLIENTE(Codigo_cliente, Nome, CPF, Tipo_telefone, DDD, Fone, EMAIL) 
                        VALUEs('{codigo.Trim()}', '{item.Nome.Trim()}', '{item.CPF.Trim()}', '{(int)item.Tipo}', '{item.DDD.Trim()}', '{item.Fone.Trim()}', '{item.Email.Trim()}')");

                mana._manager.SqlExecute(query.ToString());
            return "";
        }

        public static string Update(entities.Cliente item)
        {
            Cliente mana = new Cliente();
            bool atualizaFull = false;
            if (string.IsNullOrWhiteSpace(item.CPF))
                throw new BusinessException("O CPF  não é inválido. Por favor entre com um número de CPF válido.");

            if (!biz.Cliente.CheckCPF(item.CPF))
                throw new BusinessException(string.Format("O CPF '{0}' não é válido. Por favor entre com um número de CPF válido.", item.CPF));

            var cliente = GetByCPF(item.CPF);

            if (cliente != null)
            {
                if (item.CPF.Trim() != cliente.Codigo.Trim()) {
                    atualizaFull = !atualizaFull;
                }
            }else
                atualizaFull = !atualizaFull;

            var query = new StringBuilder();
            if(atualizaFull)
                query.AppendLine($@"UPDATE CLIENTE SET  Nome =  '{item.Nome.Trim()}', CPF = '{item.CPF.Trim()}', Tipo_telefone = '{(int)item.Tipo}', DDD = '{item.DDD.Trim()}', Fone = '{item.Fone.Trim()}', EMAIL = '{item.Email.Trim()}' where Codigo_cliente = '{item.Codigo.Trim()}'");
            else
                query.AppendLine($@"UPDATE CLIENTE SET Nome =  '{item.Nome.Trim()}', Tipo_telefone = '{(int)item.Tipo}', DDD = '{item.DDD.Trim()}', Fone = '{item.Fone.Trim()}', EMAIL = '{item.Email.Trim()}' where Codigo_cliente = '{item.Codigo.Trim()}'");

            mana._manager.SqlExecute(query.ToString());
            return "";
        }

        public static int Delete(string codigoCliente)
        {
            Cliente mana = new Cliente();

            if (string.IsNullOrWhiteSpace(codigoCliente))
                throw new BusinessException("Código do cliente inválido!");

            var query = new StringBuilder();

            query.AppendLine($@"DELETE FROM Cliente where Codigo_cliente = '{codigoCliente.Trim()}'");

            mana._manager.SqlQueryUniq(query.ToString());

            return 0;
        }

        public static List<entities.Cliente> GetAll(int skip, int take)
        {
            Cliente mana = new Cliente();
            var query = new StringBuilder();
           
            query.AppendLine($@"SELECT * FROM Cliente");
           
            return ConvertData(mana._manager.SqlQuery(query.ToString()));
        }
        public Dictionary<string, entities.TipoTelefone> TipoTelefoneConverter
        {
            get
            {
                return new Dictionary<string, entities.TipoTelefone>
                {
                    { "0", entities.TipoTelefone.Celular },
                    { "1", entities.TipoTelefone.Comercial },
                    { "2", entities.TipoTelefone.Residencial },
                    { "3", entities.TipoTelefone.Recados },
                };
            }
        }
        public static List<entities.Cliente> ConvertData(TableControl data)
        {
            var list = data.DataTable.Select().ToList().Select(item =>
                new entities.Cliente()
                {
                    Codigo = item["CODIGO_CLIENTE"]?.ToString()?.TrimEnd(),
                    Nome = item["NOME"]?.ToString()?.TrimEnd(),
                    CPF = item["CPF"]?.ToString()?.TrimEnd(),
                    Fone = item["FONE"]?.ToString()?.TrimEnd(),
                    Email = item["EMAIL"]?.ToString()?.TrimEnd(),
                    DDD = item["DDD"]?.ToString()?.TrimEnd(),
                    Tipo = item["Tipo_telefone"].ToInt32Nullable().ToEnumNullable<entities.TipoTelefone>()
                }).ToList();

            return list;
        }

        public static entities.Cliente Get(string codigocliente)
        {
            Cliente mana = new Cliente();
            var query = new StringBuilder();

            query.AppendLine($@"SELECT * FROM Cliente where  Codigo_cliente = '{codigocliente.Trim()}'");

            return ConvertData(mana._manager.SqlQuery(query.ToString())).FirstOrDefault();

        }
        public static string GerarCodigo()
        {
            Random randNum = new Random();
            return randNum.Next().ToString();
        }
        public static bool CheckCPF(string cpf)
        {
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            foreach (var i in "0123456789".ToCharArray())
                if (cpf == new string(i, 11))
                    return false;

            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
