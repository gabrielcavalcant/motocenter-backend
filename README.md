# 🏍️ Sistema de Gerenciamento de Manutenção de Motos

Este projeto é um sistema completo para gerenciar a manutenção de motos, incluindo funcionalidades como cadastro de motos, clientes, itens de estoque, equipes e membros, além de controle de manutenções e gerenciamento de estoque. O sistema é construído com tecnologias modernas e práticas recomendadas de arquitetura e desenvolvimento.

## 📚 Sumário

- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Arquitetura do Projeto](#arquitetura-do-projeto)
- [Requisitos Mínimos do Sistema](#requisitos-mínimos-do-sistema)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Como Executar o Projeto](#como-executar-o-projeto)
- [Descrição dos Serviços](#descrição-dos-serviços)
- [Como Acessar a Aplicação](#como-acessar-a-aplicação)
- [Observações](#observações)

## 🛠 Tecnologias Utilizadas

- **Linguagem:** C# (.NET 8)
- **Framework Web:** ASP.NET Core Web API
- **Banco de Dados:** PostgreSQL
- **ORM:** Entity Framework Core
- **Documentação da API:** Swagger
- **Contêineres:** Docker e Docker Compose
- **Ferramentas de Banco de Dados:** pgAdmin
- **Testes de Integração:** MSTest, Testcontainers
- **Padrões de Projeto e Arquitetura:**
  - Arquitetura Limpa (Clean Architecture)
  - Injeção de Dependência
  - Repositório Genérico
  - AutoMapper

## 🏗 Arquitetura do Projeto

O sistema segue os princípios da Arquitetura Limpa, separando as responsabilidades em diferentes camadas:

- **Domain:** Contém as entidades e interfaces de repositório.
- **Application:** Contém a lógica de negócio e os casos de uso.
- **Persistence:** Implementa os repositórios e lida com o acesso ao banco de dados usando o Entity Framework Core.
- **CrossCutting:** Contém serviços e utilitários que são usados em várias camadas, como mapeamentos do AutoMapper.
- **API:** O ponto de entrada da aplicação, onde os controladores da API estão definidos.
- **Tests.Integration:** Contém os testes de integração usando o MSTest e o Testcontainers para testar interações com o banco de dados PostgreSQL em contêineres Docker.

## 💻 Requisitos Mínimos do Sistema

- **.NET SDK:** 8.0.404 ou superior
- **Docker:** Versão 27.2.0 ou superior
- **Docker Compose:** Compatível com a versão do Docker instalada
- **Memória RAM:** 4 GB
- **Processador:** 2 núcleos
- **Armazenamento:** 10 GB livres
- **Sistema Operacional:** Compatível com Docker (Linux, macOS, Windows)

## 📁 Estrutura do Projeto

```
├── OficinaMotocenter.sln
├── README.md
├── docker-compose.yml
├── .gitignore
├── .gitattributes
├── .dockerignore
├── OficinaMotocenter.Domain
├── OficinaMotocenter.Application
├── OficinaMotocenter.Persistence
├── OficinaMotocenter.CrossCutting
├── OficinaMotorcycle.API
└── OficinaMotocenter.Tests.Integration
```

## 🚀 Como Executar o Projeto

### Clonar o Repositório

```bash
git clone https://github.com/gabrielcavalcant/motocenter-backend.git
cd motocenter-backend
```

### Executar com Docker Compose

Certifique-se de que o Docker e o Docker Compose estão instalados e em execução.

```bash
docker-compose up -d
```

### Verificar se os Serviços Estão em Execução

```bash
docker ps
```

Você deve ver os contêineres `oficinamotorcycle.api`, `postgres` e `pgadmin` em execução.

## 📜 Descrição dos Serviços

### API (OficinaMotorcycle.API)

Este serviço hospeda a API RESTful construída com ASP.NET Core Web API. Ela expõe endpoints para gerenciar motos, clientes, itens de estoque, equipes, membros e manutenções. A API segue os princípios REST e está documentada com o Swagger.

- **URL de Acesso:** [http://localhost:8080/swagger](http://localhost:8080/swagger)

### Banco de Dados PostgreSQL

O PostgreSQL é o banco de dados relacional utilizado para armazenar todas as informações do sistema.

- **Porta Padrão:** 5432 (configurado internamente no Docker)
- **Credenciais:**
  - **Usuário:** postgres
  - **Senha:** teste123
  - **Banco de Dados:** oficinadb

### pgAdmin

O pgAdmin é uma ferramenta de gerenciamento para o PostgreSQL, permitindo visualizar e manipular os dados do banco de dados através de uma interface gráfica.

- **URL de Acesso:** [http://localhost:5050](http://localhost:5050)
- **Credenciais:**
  - **Email:** admin@example.com
  - **Senha:** adminpassword

## 🌐 Como Acessar a Aplicação

- **API Swagger:** [http://localhost:8080/swagger](http://localhost:8080/swagger)
- **pgAdmin:** [http://localhost:5050](http://localhost:5050)

## 📦 Docker Compose Detalhado

O arquivo `docker-compose.yml` define os serviços necessários para executar o projeto.

```yaml
version: '3.8'

services:
  oficinamotorcycle.api:
    build:
      context: .
      dockerfile: OficinaMotorcycle.API/Dockerfile
    ports:
      - "8080:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Host=postgres;Port=5432;Database=oficinadb;Username=postgres;Password=teste123
    depends_on:
      - postgres
    networks:
      - mynetwork
    volumes:
      - ./logs:/app/logs

  postgres:
    image: postgres
    restart: always
    environment:
      POSTGRES_DB: oficinadb
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: teste123
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
    networks:
      - mynetwork

  pgadmin:
    image: dpage/pgadmin4
    environment:
      PGADMIN_DEFAULT_EMAIL: admin@example.com
      PGADMIN_DEFAULT_PASSWORD: adminpassword
    ports:
      - "5050:80"
    depends_on:
      - postgres
    networks:
      - mynetwork

volumes:
  postgres_data:

networks:
  mynetwork:
```

## 🧪 Testes de Integração

Os testes de integração estão localizados na pasta `OficinaMotocenter.Tests.Integration`. Eles utilizam o MSTest como framework de testes e o Testcontainers para iniciar um contêiner PostgreSQL para testes isolados do banco de dados.

### Executando os Testes

Para executar os testes, certifique-se de que o Docker está em execução e use o seguinte comando:

```bash
dotnet test
```

## 🔧 Observações

- **Portas Disponíveis:** Certifique-se de que as portas `8080`, `5050` e `5432` estejam disponíveis no seu host.
- **Variáveis de Ambiente:** As strings de conexão e outras configurações são passadas via variáveis de ambiente no `docker-compose.yml`.
- **Logs:** Os logs da aplicação são montados no volume `./logs` para fácil acesso.
- **Parar o Projeto:** Para parar e remover os contêineres, use:

  ```bash
  docker-compose down
  ```

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## 🤝 Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.

**Obrigado por utilizar o Sistema de Gerenciamento de Manutenção de Motos!**
