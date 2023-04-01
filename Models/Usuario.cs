using MongoDB.Bson.Serialization.Attributes;

namespace Mawe.Models;

public class Usuario
{
    public int Id { get; set; }

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

    [BsonElement("idade")]
    public int Idade { get; set; }

    [BsonElement("papel")]
    public string Papel { get; set; } = null!;

    [BsonElement("cursos")]
    public List<int>? Cursos { get; set; } 
}
