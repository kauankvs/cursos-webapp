using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mawe.Models;

public class Usuario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("username")]
    public string Username { get; set; } = null!;

    [BsonElement("nome")]
    public string Nome { get; set; } = null!;

    [BsonElement("sobrenome")]
    public string Sobrenome { get; set; } = null!;

    [BsonElement("email")]
    public string Email { get; set; } = null!;

    [BsonElement("senha")]
    public string Senha { get; set; } = null!;

    [BsonElement("nascimento")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Nascimento { get; set; }

    [BsonElement("papel")]
    public string Papel { get; set; } = null!;

    [BsonElement("cursosMatriculados")]
    public List<int> CursosMatriculados { get; set; } = new();

    [BsonElement("cursosLecionados")]
    public List<Curso> CursosLecionados { get; set; } = new();
}
