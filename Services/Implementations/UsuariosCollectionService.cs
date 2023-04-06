using Mawe.Models;
using Mawe.Services.Interfaces;
using MongoDB.Driver;

namespace Mawe.Services.Implementations
{
    public class UsuariosCollectionService: IUsuariosCollectionService
    {
        private IMongoCollection<Usuario> _collection;
        public UsuariosCollectionService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
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
    }
}
