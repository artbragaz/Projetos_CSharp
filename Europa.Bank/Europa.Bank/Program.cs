using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Europa.Bank
{
    class Program
    {
        static void Main(string[] args)
        {



            var exit = true;
            while (exit)
            {
                Console.Clear();
                exit = PublicView();
            }

        }
        private static List<Cliente> Clientes = new List<Cliente>();
        private static List<Conta> contas = new List<Conta>();
        public static bool PublicView()
        {
            Console.WriteLine("Bem vindo ao banco Europa");
            Console.WriteLine("[1] Você deseja cadastrar um cliente?");
            Console.WriteLine("[2] Deseja cadastrar uma nova conta?");
            Console.WriteLine("[3] Você deseja logar com uma conta existente?");
            Console.WriteLine("[4] Sair ");

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Opção inválida.");                
                Console.ReadKey();
                return true;
            }

            if (option == 1)
            {
                Console.WriteLine("Digite seu CPF. ");
                String cpf = Console.ReadLine();
                if (ValidarCPF(cpf) == true)
                {
                    try
                    {

                        Console.WriteLine("Digite seu nome. ");
                        String nome = Console.ReadLine();
                        Cliente novoCliente = new Cliente(cpf, nome);
                        Clientes.Add(novoCliente);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("CPF inválido. ");
                    }
                }
                else
                {
                    Console.WriteLine("CPF inválido. ");
                }
            }
            if (option == 2)
            {
                Console.WriteLine("Digite seu CPF.");
                String cpf = Console.ReadLine();
                Cliente clienteEncontrado = Clientes.Find(c => c.GetCPF() == cpf);
                if (clienteEncontrado != null)
                {
                    Conta novaConta = new Conta(clienteEncontrado);
                    contas.Add(novaConta);
                    bool logged = true;
                    while (logged)
                    {
                        logged = PrivateView(clienteEncontrado, novaConta);
                    }
                }
                else
                {
                    Console.WriteLine("O CPF digitado não possui cliente cadastrado no Banco Europa. Por favor cadastre-se e tente novamente. ");
                }
            }
            if (option == 3)
            {
                Console.WriteLine("Digite o número da conta.");
                String numeroInformado = Console.ReadLine();
                Conta contaEncontrada = contas.Find(c => c.GetNumero() == numeroInformado);
                if (contaEncontrada != null)
                {
                    Cliente clienteDaContaEncontrada = contaEncontrada.GetCliente();

                    bool logged = true;
                    while (logged)
                    {
                        logged = PrivateView(clienteDaContaEncontrada, contaEncontrada);
                    }
                }
                else
                {
                    Console.WriteLine("Conta não encontrada.");
                }

            }
            if (option == 4)
            {
                return false;
            }
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
            return true;
        }

        public static bool PrivateView(Cliente cliente, Conta conta)
        {

            Console.Clear();
            Console.WriteLine("Ola " + cliente.GetNome() + "!");
            Console.WriteLine("Seu CPF é: " + cliente.GetCPF());
            Console.WriteLine("Sua chave é: " + conta.GetChave());
            Console.WriteLine("O número da sua conta é: " + conta.GetNumero());
            Console.WriteLine("ATENÇÃO! O número da sua conta é necessário para fazer login na sua conta, perder-lo pode ser prejudicial. ");
            Console.WriteLine("O que gostaria de fazer?");
            Console.WriteLine("Digite 1 para depositar.");
            Console.WriteLine("Digite 2 para sacar da conta atual.");
            Console.WriteLine("Digite 3 para consultar o saldo da conta atual.");
            Console.WriteLine("Digite 4 para transferir dinheiro para outra conta.");
            Console.WriteLine("Digite 0 para sair.");
            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Opção inválida.");
                Console.ReadKey();
                return true;
            }
            if (option == 1)
            {
                try
                {
                    Console.WriteLine("Digite o valor para ser depositado: ");
                    double valor = double.Parse(Console.ReadLine());
                    Console.WriteLine("Tem certeza que deseja depositar R$" + valor + " na sua conta?");
                    Console.WriteLine("[1] Sim");
                    Console.WriteLine("[2] Não");
                    var confirmacao = int.Parse(Console.ReadLine());
                    if (confirmacao == 1)
                    {
                        conta.Depositar(valor);
                        Console.WriteLine($"Depósito de R${valor} realizado com sucesso.");

                    }
                    else
                    {
                        Console.WriteLine("Depósito cancelado.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (option == 2)
            {
                try
                {
                    Console.WriteLine("Digite o valor para sacar: ");
                    double valor = double.Parse(Console.ReadLine());
                    Console.WriteLine("Tem certeza que deseja sacar R$" + valor + " da sua conta?");
                    Console.WriteLine("[1] Sim");
                    Console.WriteLine("[2] Não");
                    var confirmacao = int.Parse(Console.ReadLine());
                    if (confirmacao == 1)
                    {
                        conta.Sacar(valor);
                        Console.WriteLine($"Saque de R${valor} realizado com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine("Saque cancelado");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Valor inválido.");
                }
            }
            else if (option == 3)
            {
                Console.WriteLine("O saldo da conta atual é: R$" + conta.ConsultarSaldo());
            }
            else if (option == 4)
            {
                Console.WriteLine("Digite o valor a ser depositado: ");
                try
                {
                    double valor = double.Parse(Console.ReadLine());

                    Console.WriteLine("Digite a chave da conta que receberá a quantia: ");
                    string chaveInformada = Console.ReadLine();
                    Conta contaDestino = contas.Find(c => c.GetChave() == chaveInformada);
                    if (contaDestino != null)
                    {
                        Console.WriteLine("Tem certeza que deseja depositar R$" + valor + " na conta do cliente " + contaDestino.GetCliente());
                        Console.WriteLine("[1] Sim");
                        Console.WriteLine("[2] Não");
                        var confirmacao = int.Parse(Console.ReadLine());
                        if (confirmacao == 1)
                        {
                            conta.Transferir(chaveInformada, valor);
                            Console.WriteLine($"Transferência do valor R${valor} foi realizada com sucesso.");
                        }
                        else
                        {
                            Console.WriteLine("Transferência cancelada");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usuário não encontrado.");
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Inválido. ");
                }
            }
            else if (option == 0)
            {
                return false;
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
            return true;
        }
        static bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11 || cpf.Distinct().Count() == 1) return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = multiplicador1.Select((t, i) => (tempCpf[i] - '0') * t).Sum();
            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito1;
            soma = multiplicador2.Select((t, i) => (tempCpf[i] - '0') * t).Sum();
            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith(digito1.ToString() + digito2.ToString());
        }
    }

}
