// Caminho do arquivo dos dados dos Consultores.
string caminhoArquivoDados = "arquivos_de_texto/Consultores.txt";

// Caminho do diretório para armazenar os arquivos dos aniversariantes.
string diretorioAniversariantes = "arquivos_de_texto";

// Obtém o mês atual.
int mesAtual = DateTime.Now.Month;

// Lê o arquivo de Consultores.
string[] linhasConsultores = File.ReadAllLines(caminhoArquivoDados);

// Loop através de cada linha no arquivo.
foreach (string linha in linhasConsultores)
{
    // Divide os dados usando o caractere pipe (|).
    string[] dadoslinhasConsultores = linha.Split('|');

    // Verifica se o mês de aniversário é igual ao mês atual.
    if (ObterMesAniversario(dadoslinhasConsultores[2]) == mesAtual)
    {
        // Se sim, cria uma string com os dados do consultor.
        string dadosAniversariante = $"{dadoslinhasConsultores[0]} | {dadoslinhasConsultores[1]} | {dadoslinhasConsultores[2]}";

        // Gera um nome único para o arquivo de aniversariantes.
        string nomeArquivoAniversariantes = $"{diretorioAniversariantes}/Aniversariantes_{mesAtual}.txt";

        // Adiciona os dados do aniversariante ao arquivo ou cria um novo arquivo.
        File.AppendAllText(nomeArquivoAniversariantes, dadosAniversariante + Environment.NewLine);
    }
}

Console.WriteLine($"Arquivo de aniversariantes do mês gerado com sucesso em: /{diretorioAniversariantes}");

static int ObterMesAniversario(string dataNascimento)
{
    // Converte a string de data de nascimento para DateTime.
    DateTime dataNascimentoDt = DateTime.ParseExact(dataNascimento.Trim(), "dd/MM/yyyy", null);

    // Retorna o mês de aniversário.
    return dataNascimentoDt.Month;
}