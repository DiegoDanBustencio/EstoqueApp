using Microsoft.Data.Sqlite;

const string connectionString = "Data Source=estoque.db";

CriarTabela();

while (true)
{
    Console.Clear();
    Console.WriteLine("=== SISTEMA DE ESTOQUE ===");
    Console.WriteLine("1 - Cadastrar produto");
    Console.WriteLine("2 - Listar produtos");
    Console.WriteLine("3 - Editar produto");
    Console.WriteLine("4 - Remover produto");
    Console.WriteLine("5 - Limpar estoque");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha: ");

    string opcao = Console.ReadLine() ?? "";

    switch (opcao)
    {
        case "1":
            CadastrarProduto();
            break;
        case "2":
            ListarProdutos();
        break;
        case "3":
            EditarProduto();
        break;
        case "4":
            RemoverProduto();
        break;
        case "5":
            LimparEstoque();
        break;

case "0":
    return;
        default:
            Console.WriteLine("Opção inválida!");
            Pausar();
            break;
    }
}

void CriarTabela()
{
    using var connection = new SqliteConnection(connectionString);
    connection.Open();

    string sql = @"
        CREATE TABLE IF NOT EXISTS Produtos (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Nome TEXT NOT NULL,
            Quantidade INTEGER NOT NULL,
            Preco REAL NOT NULL
        );
    ";

    using var command = new SqliteCommand(sql, connection);
    command.ExecuteNonQuery();
}

void CadastrarProduto()
{
    Console.Clear();
    Console.WriteLine("=== CADASTRAR PRODUTO ===");

    Console.Write("Nome: ");
    string nome = Console.ReadLine() ?? "";

    Console.Write("Quantidade: ");
    int quantidade = int.Parse(Console.ReadLine() ?? "0");

    Console.Write("Preço: ");
    decimal preco = decimal.Parse(Console.ReadLine() ?? "0");

    using var connection = new SqliteConnection(connectionString);
    connection.Open();

    string sql = @"
        INSERT INTO Produtos (Nome, Quantidade, Preco)
        VALUES (@nome, @quantidade, @preco);
    ";

    using var command = new SqliteCommand(sql, connection);
    command.Parameters.AddWithValue("@nome", nome);
    command.Parameters.AddWithValue("@quantidade", quantidade);
    command.Parameters.AddWithValue("@preco", preco);

    command.ExecuteNonQuery();

    Console.WriteLine("Produto cadastrado com sucesso!");
    Pausar();
}

void ListarProdutos()
{
    Console.Clear();
    Console.WriteLine("=== LISTA DE PRODUTOS ===");

    using var connection = new SqliteConnection(connectionString);
    connection.Open();

    string sql = "SELECT Id, Nome, Quantidade, Preco FROM Produtos";

    using var command = new SqliteCommand(sql, connection);
    using var reader = command.ExecuteReader();

    bool encontrou = false;

    while (reader.Read())
    {
        encontrou = true;

        Console.WriteLine($"ID: {reader.GetInt32(0)}");
        Console.WriteLine($"Nome: {reader.GetString(1)}");
        Console.WriteLine($"Quantidade: {reader.GetInt32(2)}");
        Console.WriteLine($"Preço: R$ {reader.GetDecimal(3):F2}");
        Console.WriteLine("------------------------");
    }

    if (!encontrou)
    {
        Console.WriteLine("Nenhum produto cadastrado.");
    }

    Pausar();
}

void EditarProduto()
{
    Console.Clear();
    Console.WriteLine("=== EDITAR PRODUTO ===");

    Console.Write("Digite o ID do produto: ");
    int id = int.Parse(Console.ReadLine() ?? "0");

    Console.Write("Novo nome: ");
    string nome = Console.ReadLine() ?? "";

    Console.Write("Nova quantidade: ");
    int quantidade = int.Parse(Console.ReadLine() ?? "0");

    Console.Write("Novo preço: ");
    decimal preco = decimal.Parse(Console.ReadLine() ?? "0");

    using var connection = new SqliteConnection(connectionString);
    connection.Open();

    string sql = @"
        UPDATE Produtos
        SET Nome = @nome,
            Quantidade = @quantidade,
            Preco = @preco
        WHERE Id = @id;
    ";

    using var command = new SqliteCommand(sql, connection);
    command.Parameters.AddWithValue("@id", id);
    command.Parameters.AddWithValue("@nome", nome);
    command.Parameters.AddWithValue("@quantidade", quantidade);
    command.Parameters.AddWithValue("@preco", preco);

    int linhasAfetadas = command.ExecuteNonQuery();

    if (linhasAfetadas > 0)
        Console.WriteLine("Produto atualizado com sucesso!");
    else
        Console.WriteLine("Produto não encontrado.");

    Pausar();
}

void RemoverProduto()
{
    Console.Clear();
    Console.WriteLine("=== REMOVER PRODUTO ===");

    Console.Write("Digite o ID do produto: ");
    int id = int.Parse(Console.ReadLine() ?? "0");

    using var connection = new SqliteConnection(connectionString);
    connection.Open();

    string sql = "DELETE FROM Produtos WHERE Id = @id";

    using var command = new SqliteCommand(sql, connection);
    command.Parameters.AddWithValue("@id", id);

    int linhasAfetadas = command.ExecuteNonQuery();

    if (linhasAfetadas > 0)
        Console.WriteLine("Produto removido com sucesso!");
    else
        Console.WriteLine("Produto não encontrado.");

    Pausar();
}

    void LimparEstoque()
{
    Console.Clear();

    Console.WriteLine("=== LIMPAR ESTOQUE ===");
    Console.WriteLine("Tem certeza que deseja apagar TODOS os produtos?");
    Console.Write("Digite S para confirmar: ");

    string confirmacao = Console.ReadLine() ?? "";

    if (confirmacao.ToUpper() != "S")
    {
        Console.WriteLine("Operação cancelada.");
        Pausar();
        return;
    }

    using var connection = new SqliteConnection(connectionString);
    connection.Open();

    string sql = "DELETE FROM Produtos";

    using var command = new SqliteCommand(sql, connection);

    int linhasAfetadas = command.ExecuteNonQuery();

    Console.WriteLine($"{linhasAfetadas} produto(s) removido(s) com sucesso!");

    Pausar();
}

void Pausar()
{
    Console.WriteLine();
    Console.WriteLine("Pressione ENTER para continuar...");
    Console.ReadLine();
}