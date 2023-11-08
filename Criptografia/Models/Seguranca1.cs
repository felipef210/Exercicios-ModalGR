/*
    Esse código apresenta na verdade uma forma de codificar a informação (senha) digitada pelo 
    usuário, essa codificação foi feita em base64.

    O método CriptografarSenha recebe uma senha e uma chave. Ele realiza a codificação base64 da senha, concatena essa codificação com a chave fornecida no enunciado e alguns caracteres adicionais ("====="). Após isso, realiza outra codificação base64 na concatenação com os caracteres especiais. O resultado final é retornado como a "senha criptografada".
*/

public class Seguranca1
{
    public static string CriptografarSenha (string senha, string chave)
    {
        // Criptografa a senha digitada.
        var codificar = Criptografa64(senha);
        // Concatena senha já criptografada com "====" e a "chave".
        var codificarComChave = string.Concat(chave, "=====", codificar);
        // Criptografa a senha concatenada novamente.
        var codificar2x = Criptografa64(codificarComChave);

        // Retorna a criptografia final.
        return codificar2x;
    }

    static private string Criptografa64 (string toEncode)
    {
        // Criptografa para base 64.
        byte[] toEncodeAsByte = System.Text.ASCIIEncoding.UTF8.GetBytes(toEncode);
        // Converte para string.
        string returnValue = System.Convert.ToBase64String(toEncodeAsByte);

        //Retorna valor em string.
        return returnValue;
    }
}