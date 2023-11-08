class Program
{
    static void Main()
    {
        int opcaoMenu = 0;

        // Caso o usuário opte por "não" o programa será encerrado.
        do
        {
            // Chama a função para escolher qual método de criptografia para cada senha.
            EscolherCripto();

            Console.WriteLine("1 - Sim");
            Console.WriteLine("2 - Não");
            Console.Write("Deseja digitar mais senhas ?: ");
            opcaoMenu = int.Parse(Console.ReadLine());
            Console.Clear();
        } while (opcaoMenu != 2);
        
        Console.Write("Pressione enter para encerrar.");
        Console.ReadLine();
    }

    // Função criada para escolha de métodos de criptografia.
    static void EscolherCripto()
    {
        int opcaoCripto = 0;
        string [] senhas = new string [3];
        string chave = "#modalGR#GPTW#top#maiorEmpresaTecnologia#baixadaSantista";

        for (int i = 0; i < senhas.Length; i++)
        {   
            Console.Write($"Seja bem-vindo, digite a sua senha {i+1}: ");
            string inputSenhaSecret = EscondeSenha(); // Chama a função que deixa a senha secreta.
            senhas[i] = inputSenhaSecret;
            Console.Write("Digite qual método de criptografia você deseja (de 1 a 3): ");
            if (!int.TryParse(Console.ReadLine(), out opcaoCripto))
            {
                throw new Exception("Digite um número de 1 a 3.");
            }

            switch (opcaoCripto)
            {
                // Chama a classe Segurança1 = método de codificação em base64
                case 1:
                    var senhaCriptografada = Seguranca1.CriptografarSenha(senhas[i], chave);
                    Console.WriteLine($"Sua senha criptografada: {senhaCriptografada}\n");
                    break;

                // Chama a classe Segurança2 = método AES (Advanced Encryption Standard)
                case 2:
                    senhaCriptografada = Seguranca2.CriptografarSenha(senhas[i], chave);
                    Console.WriteLine($"Sua senha criptografada: {senhaCriptografada}\n");
                    break;

                // Chama a classe Segurança3 = método HMAC (Hash-based Message Authentication Code)
                case 3:
                    senhaCriptografada = Seguranca3.CriptografarSenha(senhas[i], chave);
                    Console.WriteLine($"Sua senha criptografada: {senhaCriptografada}\n");
                    break;

                default:
                    throw new Exception("Opção inválida");
            }
        }
    }

    // Função criada para retornar no console "*" no lugar dos caracteres digitados.
    static string EscondeSenha()
    {
        string senha = "";
        ConsoleKeyInfo secret;

        do
        {
            secret = Console.ReadKey(true);

            // Verifica se a tecla pressionada não é Enter
            if (secret.Key != ConsoleKey.Enter)
            {
                if ((secret.Key == ConsoleKey.Backspace || secret.Key == ConsoleKey.Delete)
                    && senha.Length > 0)
                {
                    // Remove o último caractere se a tecla Backspace for pressionada
                    senha = senha.Substring(0, senha.Length - 1);
                    Console.Write("\b \b"); // Apaga o caractere no console
                }
                else if (secret.Key != ConsoleKey.Backspace && secret.Key != ConsoleKey.Delete)
                {
                    // Adiciona o caractere se não for Delete ou Backspace
                    senha += secret.KeyChar;
                    Console.Write("*"); // Substitui a tecla pressionada por um asterisco
                }
            }
        } while (secret.Key != ConsoleKey.Enter);

        Console.WriteLine(); // Adiciona uma quebra de linha após a senha
        return senha;
    }
}