using Mawe.Models;
using Mawe.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mawe.Services.Implementations
{
    public class UsuariosCollectionService: IUsuariosCollectionService
    {
        private IMongoCollection<Usuario> _collection;
        public UsuariosCollectionService()
        {
            var settings = MongoClientSettings.FromConnectionString(Configuracoes.MongoConnection);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("mawe");
            _collection = database.GetCollection<Usuario>("usuarios");
        }

        public async Task AdicionarUsuarioAsync(Usuario usuario)
            => await _collection.InsertOneAsync(usuario);

        public async Task<Usuario?> SelecionarUsuarioPorEmailAsync(string email)
            => await _collection.Find(u => u.Email == email).FirstOrDefaultAsync();

        public async Task AdicionarCursoAsync(string email, Curso curso)
        {
            var filtro = Builders<Usuario>.Filter.Eq(e => e.Email, email);
            var update = Builders<Usuario>.Update.Push<Curso>(e => e.CursosLecionados, curso);
            await _collection.FindOneAndUpdateAsync(filtro, update);
        }

        public async Task<List<Usuario>> SelecionarTodosUsuariosTutoresAsync()
        {
            List<Usuario> usuarios = await _collection.Find(u => u.CursosLecionados != null).ToListAsync();
            return usuarios;
        }

        public async Task<Usuario> SelecionarUsuariosPorNomeDoCurso(string nomeUnico)
        {
            var filter = Builders<Usuario>.Filter.ElemMatch(u => u.CursosLecionados, c => c.NomeUnico == nomeUnico);
            Usuario usuario = await _collection.Find(filter).FirstOrDefaultAsync();
            return usuario;
        }
    }
}
