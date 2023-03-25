namespace CursosWebApp.Services.Interfaces
{
    public interface ICriptografia
    {
        public string TransformarSenhaEmHash(string senha);
        public bool VerificarValidadeDaSenha(string username, string senha);
    }
}
