using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace OficinaMotocenter.Tests.Integration.Database
{
    [TestClass]
    public class DatabaseIntegrationTests
    {
        private static PostgreSqlTestcontainer? _postgresContainer;
        private static string? _connectionString;

        [ClassInitialize]
        public static async Task Setup(TestContext context)
        {
            // Configuração do container PostgreSQL
            var postgresConfiguration = new PostgreSqlTestcontainerConfiguration
            {
                Database = "oficinadb",
                Username = "postgres",
                Password = "teste123",
            };

            // Usando o ContainerBuilder genérico para construir o container PostgreSQL
            _postgresContainer = new ContainerBuilder<PostgreSqlTestcontainer>()
                .WithDatabase(postgresConfiguration)
                .Build();

            await _postgresContainer.StartAsync();

            // Usando a propriedade ConnectionString fornecida pelo Testcontainer
            _connectionString = _postgresContainer.ConnectionString;
        }

        [TestMethod]
        public async Task TestDatabaseConnection()
        {
            // Testando conexão com o banco de dados
            using (var connection = new NpgsqlConnection(_connectionString!))
            {
                await connection.OpenAsync();
                Assert.AreEqual(ConnectionState.Open, connection.State, "Falha ao conectar ao banco de dados.");
            }
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            if (_postgresContainer is not null)
            {
                await _postgresContainer.DisposeAsync(); // Limpeza automática dos recursos
            }
        }
    }
}
