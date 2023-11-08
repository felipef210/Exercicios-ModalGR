/*
    Esse código segue um modelo de criptografia AES (Advanced Encryption Standard). Esse é um modelo
    que a mesma chave serve tanto para codificar quanto para descodificar uma informação, ou seja,
    é uma criptografia simétrica.
*/

using System.Security.Cryptography;
using System.Text;

public class Seguranca2
{
    public static string CriptografarSenha(string senha, string chaveSecreta)
    {
        // Chama o método para ajustar o tamanho da chave para 16 bytes.
        chaveSecreta = AjustarTamanhoChave(chaveSecreta);

        // Objeto da classe Aes é criado para utilizar seus métodos de criptografia.
        using (Aes aesAlg = Aes.Create())
        {
            // Define o valor da chave do objeto Aes com valor em bytes de "chaveSecreta".
            aesAlg.Key = Encoding.UTF8.GetBytes(chaveSecreta);

            /*Gerando o IV garantimos que a mesma senha tenha várias criptografias diferentes. 
            
            Exemplo: 

            Senha 1 - felipe    Criptografado 1 - f7T25lHx/TmxHufgF9H/9OMftSiKXWIKkY25gVoQBx0=
            Senha 2 - felipe    Criptografado 2 - yx+znvVOEhG35c5CLuMPumI1JtynMVhsQR6otKcFqpI=
            
            Esse é o diferencial desse método de criptografia em relação aos outros 2 criados nesse exercício.
            */
            aesAlg.GenerateIV();

            // Cria uma criptografia usando a chave secreta e o valor armazenado no vetor IV
            ICryptoTransform encriptador = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Armazena os dados criptografados
            using (MemoryStream msCriptografado = new MemoryStream())
            {
                using (CryptoStream csCriptografado = new CryptoStream(msCriptografado, encriptador, CryptoStreamMode.Write))
                {
                    using (StreamWriter swCriptografado = new StreamWriter(csCriptografado))
                    {
                        swCriptografado.Write(senha);
                    }
                }

                // Combina o IV com os dados criptografados e converte para base64
                return Convert.ToBase64String(aesAlg.IV.Concat(msCriptografado.ToArray()).ToArray());
            }
        }
    }

    // Método para ajustar o tamanho da chave para 16 bytes.
    private static string AjustarTamanhoChave(string chave)
    {
        // 16 bytes = 128 bits
        if (chave.Length < 16) 
        {
            // Se a chave for menor que 16 bytes, preenche com zeros à direita.
            chave = chave.PadRight(16, '0');
        }
        else if (chave.Length > 16)
        {
            // Se a chave for maior que 16 bytes, corta para 16 bytes.
            chave = chave.Substring(0, 16);
        }

        return chave;
    }
}