﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEstacionamento
{
    public class Estacionamento
    {
        private List<Veiculo> _veiculo;
        public string Nome { get; private set; }
        public string Documento { get; private set; }
        public IReadOnlyList<Veiculo> Veiculo => _veiculo;

        public Estacionamento(string nome, string documento)
        {
            Nome = nome;
            Documento = documento;
            _veiculo ??= new List<Veiculo>(); 
        }

        public void AcionarVeiculo(string placa, string modelo, string cor, ETipoVeiculo tipoVeiculo, bool diariaAdquirida, bool duchaAdquirida)
        {
            Veiculo veiculo = new Veiculo(placa, modelo, cor, tipoVeiculo);

            if (veiculo is null)
                throw new Exception("Não foi iniciado um veiculo");

            veiculo.IniciarDiaria(new Diaria(DateTime.Now, veiculo,diariaAdquirida,duchaAdquirida));

            _veiculo.Add(veiculo);

        }

        public void FinalizarDiaria(string placa)
        {
            var veiculo = _veiculo.Where(c => c.Placa == placa );

            if(veiculo.Count() == 0)
                throw new Exception("Veiculo não encontrado");

            foreach (var c in veiculo)                
            {
                c.Diaria.AtualizarDiaria(DateTime.Now.AddMinutes(30));
                //c.Diaria.AtualizarDiaria(DateTime.Now);

            }

        }

        public void GerarTicket(string placa)
        {
            var veiculo = _veiculo.Where(c => c.Placa == placa);

            if (veiculo.Count() == 0)
                throw new Exception("Veiculo não encontrado");

            foreach (var c in veiculo)
            {
                Console.WriteLine("Resumo diaria:");
                Console.WriteLine(@"Placa: " + c.Placa + 
                                  "\nHora de entrada: " + c.Diaria.DataHoraInicio +
                                  "\nHora de saída: " + c.Diaria.DataHoraFim +
                                  "\nTotal da diaria: " + c.Diaria.ValorDiaria);

            }

        }

    }
}
