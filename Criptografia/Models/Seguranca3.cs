/*
    Já nesse terceiro método foi implementado uma hash SHA-256 (Secure Hash Algorithm 256 bits).
    Esse método função de hash não é reversível, ou seja, não é possível descriptografar o resultado para obter a senha original.
*/

using System.Security.Cryptography;
using System.Text;

public class Seguranca3
{
    public static string CriptografarSenha(string senha, string chaveSecreta)
    {
        // Chave secreta fornecia pelo enunciado
        using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(chaveSecreta)))
        {
            // Aqui é efetuado um cálculo para converter a senha em bytes SHA-256.
            byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));

            // Converte a hash para string e é implementado um "-" para representar a chave em hexadecimal.
            string senhaCriptografada = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            
            // Retorna a string já formatada.
            return senhaCriptografada;
        }
    }
}