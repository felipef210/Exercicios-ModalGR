using System.Globalization;

class Program
{
    private static void Main(string[] args)
    {
         int opcaoMenu = 0;

        // Caso o usuário opte por não o programa será encerrado.
        do
        {
            // Chama a função para inicializar a coleta de informações do colaborador.
            EscolheSaque();

            Console.WriteLine("1 - Sim");
            Console.WriteLine("2 - Não");
            Console.Write("Deseja realizar mais empréstimos ?: ");
            opcaoMenu = int.Parse(Console.ReadLine());
            Console.Clear();
        } while (opcaoMenu != 2);
        
        Console.Write("Pressione enter para encerrar.");
        Console.ReadLine();
    }

    // Recolhe as informações para realização do saque do colaborador
    static void EscolheSaque()
    {
        // ---Declaração de variáveis--- //
        int opc = 0;                     // Controla as opções de saque.

        DateTime dataAtual = DateTime.Now; // Armazena a data de hoje.

        double salarioAtual = 0;             // Armazena o salário do colaborador.
        double emprestimo = 0;               // Armazena o valor do empréstimo.
        double exibeTotalEmprestimo = 0; // Responsável pela exibição do empréstimo em forma de string.
        double nota = 0;                 // Armazena o valor da nota.

        string opcaoSaque = " ";         // Exibe a opção escolhida escrita por extenso.
                                         // ----------------------------- //

        Console.WriteLine("---------Olá Seja Bem Vindo ao Banco GR---------");

        // ---Inserção e validação dos campos--- //
        Console.Write("Digite o seu nome: ");
        string nome = Console.ReadLine(); // Armazena o nome do colaborador.

        Console.Write("Digite a data de admissão em nosso banco (dd/MM/yyyy): ");

        // Armazena a data de admissão do cliente.
        string dataAdmissao = Console.ReadLine();

        // Separa dia, mês e ano da variável para realizar verificações
        string[] partesData = dataAdmissao.Split("/");

        // Verifica se a data está no formato solicitado e se os dados sobre a data são válidos.
        try
        {
            if (!DateTime.TryParseExact(dataAdmissao, "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime data) ||
                int.Parse(partesData[0]) > 31 ||
                int.Parse(partesData[1]) > 12 ||
                dataAtual < DateTime.Parse(dataAdmissao))
            {
                //Retorna um erro caso algo não esteja de acordo.
                throw new Exception("Dados de admissão inválidos");
            }

            TimeSpan diferencaDatas = dataAtual.Subtract(data);

            Console.Write("Digite o seu salário atual: R$");

            // Verifica se o que foi digitado de fato é um número.
            if (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out salarioAtual))
            {
                throw new Exception("Valor salarial inválido");
            }

            Console.Write("Digite o valor do empréstimo: R$");

            // Verifica se o que foi digitado de fato é um número.
            if (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out emprestimo))
            {
                throw new Exception("Valor de empréstimo inválido");
            }

            // Verifica se o valor do empréstimo não excede o dobro do salário.
            // Número de dias equivalente a 5 anos levando em consideração pelo menos 1 ano bissexto.
            else if (diferencaDatas.Days < 1826 || emprestimo > 2 * salarioAtual)  
            {       
                throw new Exception("Agradecemos seu interesse, mas você não atende os requisitos mínimos do programa.");
            }

            // Verifica se valor de empréstimo digitado é múltiplo de 2.
            else if (emprestimo % 2 != 0)
            {
                throw new Exception("Insira um valor válido!");
            }
        }

        catch (Exception erro)
        {
            Console.WriteLine(erro.Message);
            Environment.Exit(400);
        }

        exibeTotalEmprestimo = emprestimo;

        Console.Clear();

        // ---Menu de interação com o usuário--- //
        Console.WriteLine("---Opções de saque---");
        Console.WriteLine("1 - Notas de maior valor");
        Console.WriteLine("2 - Notas de menor valor");
        Console.WriteLine("3 - Meio a meio");
        Console.Write("Selecione uma das opções acima: ");
        opc = int.Parse(Console.ReadLine());
        // ------------------------------------ //

        Console.Clear();

        switch (opc)
        {
            // Inicializa o valor das notas em 100 já que se trata de notas maiores.
            case 1:
                nota = 100;
                opcaoSaque = "em notas maiores";
                SeparadorQuebraLinha(false);
                Console.WriteLine($"Saque do cliente {nome} de R${exibeTotalEmprestimo.ToString("N2")} {opcaoSaque}:");
                SacarNotas(emprestimo, nota);
                SeparadorQuebraLinha(false);
                break;

            // Inicializa o valor das notas em 20 já que se trata de notas menores.
            case 2:
                nota = 20;
                opcaoSaque = "em notas menores";
                SeparadorQuebraLinha(false);
                Console.WriteLine($"Saque do cliente {nome} de R${exibeTotalEmprestimo.ToString("N2")} {opcaoSaque}:");
                SacarNotas(emprestimo, nota);
                SeparadorQuebraLinha(false);
                break;

            // Divide o total do empréstimo em duas partes, na 1ª metade o valor de notas começa em 
            // 100 para as notas maiores, na 2ª metade o valor começa em 20 para as notas menores.
            case 3:
                opcaoSaque = "meio a meio";
                Console.WriteLine($"Saque do cliente {nome} de R${exibeTotalEmprestimo.ToString("N2")} {opcaoSaque}:");
                double metadeEmprestimo = emprestimo / 2;

                nota = 100;
                SeparadorQuebraLinha(true);
                Console.WriteLine($"R${metadeEmprestimo.ToString("N2")} em notas de maior valor:");
                SacarNotas(metadeEmprestimo, nota);
                SeparadorQuebraLinha(false);

                nota = 20;
                SeparadorQuebraLinha(true);
                Console.WriteLine($"R${metadeEmprestimo.ToString("N2")} em notas de menor valor:");
                SacarNotas(metadeEmprestimo, nota);
                SeparadorQuebraLinha(false);
                break;
        }
        // --------------------------------- //
    }
        

    // ---Cálculo da quantidade notas a ser sacada--- //
    static void SacarNotas(double valor, double nota)
    {
        int nNotas = 0; // Contabiliza a quantidade de notas sacadas.
        while (true)
        {
            // Caso o valor de empréstimo for maior que a nota.
            if (valor >= nota)
            {
                valor -= nota; // Subtrai o valor do empréstimo pelo valor da nota.
                nNotas++;      // Incremente a quantidade de notas.
            }

            // Se for menor, encontra o valor da nota mais próximo que seja menor que o empréstimo.
            else
            {
                // Imprime apenas se houver sacado alguma nota daquele valor.
                if (nNotas > 0)
                {
                    Console.WriteLine($"{nNotas} X {nota} reais");
                }

                nNotas = 0; // Zera a quantidade de notas para iniciar a contagem de outros valores.

                // ---Passa o valor da nota para o próximo menor que ele.---
                if (nota == 100)
                {
                    nota = 50;
                }

                else if (nota == 50)
                {
                    nota = 20;
                }

                else if (nota == 20)
                {
                    nota = 10;
                }

                else if (nota == 10)
                {
                    nota = 5;
                }

                else if (nota == 5)
                {
                    nota = 2;
                }

                // ------------------------------ //

                // Prevenção de bugs. //
                if (valor == 6 || valor == 8)
                {
                    nota = 2;
                }
                // ------------------ //

                else if (valor == 0)
                {
                    break;
                }
            }
        }
    }
    // ---------------------------------------//

    static void SeparadorQuebraLinha(bool quebraLinha)
    {
        if (quebraLinha) Console.WriteLine("\n=======================");

        else Console.WriteLine("=======================");
    }
}