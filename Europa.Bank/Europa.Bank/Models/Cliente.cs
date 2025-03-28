using System;
using System.Collections.Generic;

public class Cliente
{
		private string CPF;
		private string Nome;

    private static HashSet<string> cpfsRegistrados = new HashSet<string>();

	public Cliente(string cpf, string nome)
	{
		if (cpfsRegistrados.Contains(cpf))
		{
            throw new Exception("CPF já registrado.");
        }
		cpfsRegistrados.Add(cpf);
		this.CPF = cpf;
		this.Nome = nome;
	}
    public string GetNome()
{
		return this.Nome;
}
	public string GetCPF()
{
		return this.CPF;
}
    public override string ToString()
    {
        return GetNome();
    }
}
