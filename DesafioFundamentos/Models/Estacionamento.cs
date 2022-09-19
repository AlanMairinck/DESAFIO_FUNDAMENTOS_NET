using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        /// <summary>
        /// Método para validar se o formato da placa está correto.
        /// </summary>
        /// <param name="placa">recebe uma string com a placa do veículo.</param>
        /// <returns>retorna True caso a placa esteja correta.</returns>
        public bool ValidarPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa) || placa.Length != 7) {
                return false; 
            }

            placa = placa.Replace("-", "").Trim();

             // Verifica se o caractere da posição 4 é uma letra, se sim, aplica a validação para o formato de placa do Mercosul,
             // senão, aplica a validação do formato de placa padrão.
            if (char.IsLetter(placa, 4))
            {
                //Verifica se a placa está no formato: três letras, um número, uma letra e dois números.
                var padraoMercosul = new Regex("[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}");
                return padraoMercosul.IsMatch(placa);
            }
            else
            {
                // Verifica se os 3 primeiros caracteres são letras e se os 4 últimos são números.
                var padraoNormal = new Regex("[a-zA-Z]{3}[0-9]{4}");
                return padraoNormal.IsMatch(placa);
            }
        }

        public void AdicionarVeiculo()
        {
            // TODO: Pedir para o usuário digitar uma placa (ReadLine) e adicionar na lista "veiculos"
            // *IMPLEMENTE AQUI*
            Console.WriteLine("Digite a placa do veículo para estacionar: \n");
            string placa = Console.ReadLine();
            bool retorno = false;
            bool placaValida = ValidarPlaca(placa);

            if (this.veiculos.Contains(placa.ToUpper()))
            {
                Console.WriteLine($"Veículo {placa.ToUpper()} já estacionado.\n");
            }
            else
            {
                if (!placaValida)
                {
                    while (!placaValida && !retorno)
                    {
                        Console.WriteLine("Placa informada não corresponde ao padrão. \n");

                        Console.WriteLine("Digite a sua opção:");
                        Console.WriteLine("1 - Digitar a placa do veículo para estacionar");
                        Console.WriteLine("2 - Voltar ao menu inicial");

                        switch (Console.ReadLine())
                        {
                            case "1":
                                Console.WriteLine("Digite a placa do veículo para estacionar: \n");
                                placa = Console.ReadLine();
                                placaValida = ValidarPlaca(placa);
                                break;

                            case "2":
                                retorno = true;
                                break;

                            default:
                                Console.WriteLine("Opção inválida");
                                break;
                        }
                    }
                }

                if (placaValida)
                {
                    this.veiculos.Add(placa.ToUpper());
                    Console.WriteLine("Carro cadastrado com sucesso. \n");
                }
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            // Pedir para o usuário digitar a placa e armazenar na variável placa
            // *IMPLEMENTE AQUI*
            string placa = "";
            placa = Console.ReadLine();

            // Verifica se o veículo existe
            if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                // TODO: Pedir para o usuário digitar a quantidade de horas que o veículo permaneceu estacionado,
                // TODO: Realizar o seguinte cálculo: "precoInicial + precoPorHora * horas" para a variável valorTotal                
                // *IMPLEMENTE AQUI*
                int horas = 0;
                decimal valorTotal = 0;

                string validaHoras = Console.ReadLine();

                if(validaHoras.All(char.IsNumber))
                    horas = Convert.ToInt32(validaHoras);

                if(horas <= 0)
                    Console.WriteLine("O valor de horas informado não pode ser inferior a 1.");
                else
                {
                    valorTotal = this.precoInicial + this.precoPorHora * horas;

                    this.veiculos.Remove(placa);

                    // TODO: Remover a placa digitada da lista de veículos
                    // *IMPLEMENTE AQUI*

                    Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
                }
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são: \n");
                // TODO: Realizar um laço de repetição, exibindo os veículos estacionados
                // *IMPLEMENTE AQUI*
                foreach (string veiculosEstacionados in this.veiculos)
                {
                    Console.WriteLine($"{veiculosEstacionados}");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}
