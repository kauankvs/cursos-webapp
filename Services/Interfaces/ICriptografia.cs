namespace CursosWebApp.Services.Interfaces
{
    public interface ICriptografia
    {
        public string TransformarSenhaEmHash(string senha);
        public Task<bool> VerificarValidadeDaSenha(string email, string senha);
    }
}
