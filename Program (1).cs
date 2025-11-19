using Microsoft.Data.Sqlite;
using System;

class Program
{
    static string connectionString = "Data Source=alunos.db";

    static void Main()
    {
        CriarTabela();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE ALUNOS ===");
            Console.WriteLine("1 - Adicionar aluno");
            Console.WriteLine("2 - Listar alunos");
            Console.WriteLine("3 - Atualizar aluno");
            Console.WriteLine("4 - Excluir aluno");
            Console.WriteLine("5 - Sair");
            Console.Write("Escolha: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1": Adicionar(); break;
                case "2": Listar(); break;
                case "3": Atualizar(); break;
                case "4": Excluir(); break;
                case "5": return;
                default: Console.WriteLine("Opção inválida"); break;
            }

            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }
    }

    // ==========================
    // Criar tabela 
    // ==========================
    static void CriarTabela()
    {
        using (var con = new SqliteConnection(connectionString))
        {
            con.Open();
            string sql = @"
                CREATE TABLE IF NOT EXISTS Alunos(
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome TEXT NOT NULL,
                    Idade INTEGER
                );
            ";

            using (var cmd = new SqliteCommand(sql, con))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    // ================
    // CREATE (Inserir)
    // ================
    static void Adicionar()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Idade: ");
        int idade = int.Parse(Console.ReadLine());

        using (var con = new SqliteConnection(connectionString))
        {
            con.Open();
            string sql = "INSERT INTO Alunos (Nome, Idade) VALUES (@nome, @idade)";

            using (var cmd = new SqliteCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@idade", idade);
                cmd.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Aluno cadastrado com sucesso!");
    }

    // ===============
    // READ (Listar)
    // ===============
    static void Listar()
    {
        using (var con = new SqliteConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM Alunos";

            using (var cmd = new SqliteCommand(sql, con))
            using (var reader = cmd.ExecuteReader())
            {
                Console.WriteLine("=== LISTA DE ALUNOS ===");

                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["Id"]}  Nome: {reader["Nome"]}  Idade: {reader["Idade"]}");
                }
            }
        }
    }

    // =================
    // UPDATE (Editar)
    // =================
    static void Atualizar()
    {
        Console.Write("Digite o ID do aluno a atualizar: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Novo nome: ");
        string nome = Console.ReadLine();

        Console.Write("Nova idade: ");
        int idade = int.Parse(Console.ReadLine());

        using (var con = new SqliteConnection(connectionString))
        {
            con.Open();
            string sql = "UPDATE Alunos SET Nome=@nome, Idade=@idade WHERE Id=@id";

            using (var cmd = new SqliteCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@idade", idade);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Aluno atualizado com sucesso!");
    }

    // ===============
    // DELETE (Excluir)
    // ===============
    static void Excluir()
    {
        Console.Write("Digite o ID do aluno a excluir: ");
        int id = int.Parse(Console.ReadLine());

        using (var con = new SqliteConnection(connectionString))
        {
            con.Open();
            string sql = "DELETE FROM Alunos WHERE Id=@id";

            using (var cmd = new SqliteCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Aluno excluído com sucesso!");
    }
}
