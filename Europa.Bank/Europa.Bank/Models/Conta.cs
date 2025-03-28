using System;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Linq;

public class Conta
{
    private string Numero;
    private double Saldo;
    private Cliente Cliente;
    private string Chave;
    private static Random Random = new Random();


    private static List<Conta> TodasAsContas = new List<Conta>();
    private static List<string> ChavesRegistradas = new List<string>();
    private static List<string> NumerosRegistrados = new List<string>();
    public Conta(Cliente cliente)
    {
        this.Saldo = 0;
        this.Cliente = cliente;
        TodasAsContas.Add(this);
        this.Numero = GerarNumeroAlfanumerica();
        while (NumerosRegistrados.Contains(Numero))
        {
            this.Numero = GerarNumeroAlfanumerica();
        }
        NumerosRegistrados.Add(Numero);
        this.Chave = GerarNumeroAlfanumerica();
        while (ChavesRegistradas.Contains(Chave))
        {
            this.Chave = GerarNumeroAlfanumerica();
        }
        ChavesRegistradas.Add(Chave);
    }

    private string GerarNumeroAlfanumerica()
    {
        const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(caracteres, 6)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
  
    public string GetNumero()
    {
        return this.Numero;
    }
    public Cliente GetCliente()
    {
        return this.Cliente;
    }
    public string GetChave()
    {
        return this.Chave;
    }
    public void Depositar(double valor)
    {
        if (valor > 0)
        {
            Saldo = Saldo + valor;

        }
        else
        {
            throw new Exception("Valor inválido.");
        }
    }
    public void Sacar(double valor)
    {
        if (valor > 0 && Saldo >= valor)
        {
            Saldo = Saldo - valor;

        }
        else
        {
            Console.WriteLine("Valor inválido.");

        }
    }
    public double ConsultarSaldo()
    {
        return this.Saldo;
    }
    public void Transferir(string chave, double valor)
    {
        Conta contaDestino = TodasAsContas.Find(c => c.GetChave() == chave);
        if (valor > 0 && ConsultarSaldo() >= valor)
        {
            Saldo = Saldo - valor;
            contaDestino.Depositar(valor);

        }
        else
        {
            Console.WriteLine("Valor inválido.");

        }
    }
}
