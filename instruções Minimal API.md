# 1 Estrutura Básica * Asp.NET Minimal API

## 1.1 Dotnet CLI

### 1.1.1 Introdução

A CLI do .NET permite criar, configurar e executar aplicações Minimal API de forma rápida. Por meio de comandos como `dotnet new`, `dotnet --list-sdks` e `dotnet -h`, é possível inspecionar o ambiente instalado, gerar projetos e executar tarefas recorrentes no desenvolvimento.

### 1.1.2 Verificação do Ambiente

O .NET fornece comandos essenciais para identificar versões instaladas e capacidades da ferramenta.

#### 1.1.2.1 Listagem de SDKs Instalados

    dotnet --list-sdks

Este comando retorna todas as versões do SDK instaladas no sistema. É útil para garantir compatibilidade entre projetos, pipelines e ambientes de produção.

#### 1.1.2.2 Ajuda Geral da CLI

    dotnet -h

Exibe os comandos disponíveis, parâmetros e descrições gerais da CLI.

### 1.1.3 Criação de Projetos Minimal API

A CLI oferece modelos prontos para iniciar uma aplicação Minimal API.

#### 1.1.3.1 Consulta aos Modelos Disponíveis

    dotnet new web -h

Exibe detalhes sobre o template `web`, incluindo parâmetros opcionais e configurações recomendadas.

#### 1.1.3.2 Exemplos de Criação de Projetos

    dotnet new web -o MinimalApi.Exemplo

O comando cria um novo projeto do tipo Minimal API no diretório especificado.

### 1.1.4 Estrutura Gerada pelo Template

A aplicação criada com `dotnet new web` possui uma estrutura simplificada, geralmente composta por:

* `Program.cs`
* `appsettings.json`
* Pasta `Properties`
* Arquivos de configuração de build

### 1.1.5 Código Exemplificativo

Um exemplo básico presente em um projeto Minimal API gerado pela CLI.

    ```csharp
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    app.MapGet("/saudacao", () => "Aplicação Minimal API criada com dotnet CLI");

    app.Run();
    ```

### 1.1.6 Explicação da Sintaxe

* `WebApplication.CreateBuilder(args)` inicializa a aplicação configurando serviços e middlewares.
* `app.MapGet(...)` define um endpoint HTTP GET com resposta direta.
* `app.Run()` inicia o servidor web.

### 1.1.7 Comparação com MVC

| Critério     | Minimal API                           | ASP.NET MVC                            |
| -----------*| ------------------------------------* | -------------------------------------* |
| Estrutura    | Enxuta, poucos arquivos               | Robusta, múltiplos diretórios          |
| Configuração | Declarativa e direta no `Program.cs`  | Baseada em Controllers, Views e Models |
| Uso          | Ideal para microserviços e APIs leves | Adequado para aplicações complexas     |

### 1.1.8 Exemplos Práticos

#### 1.1.8.1 Endpoint com Parâmetros

    ```csharp
    app.MapGet("/soma/{a:int}/{b:int}", (int a, int b) => a + b);
    ```

#### 1.1.8.2 Endpoint com Injeção de Dependência

    ```csharp
    builder.Services.AddSingleton<IRelogio, RelogioSistema>();

    app.MapGet("/hora", (IRelogio relogio) => relogio.Agora());
    ```

### 1.1.9 Boas Práticas

* Atualizar regularmente SDKs com base no resultado de `dotnet --list-sdks`.
* Utilizar `dotnet new web -h` para revisar parâmetros antes de criar novos projetos.
* Manter endpoints enxutos e orientados a uma única responsabilidade.
* Documentar endpoints e usar validação de entrada.

### 1.1.10 Conclusão

A combinação entre a CLI do .NET e Minimal APIs permite criar aplicações enxutas, rápidas e fáceis de manter. Com poucos comandos é possível estruturar, executar e evoluir projetos de forma eficiente.

## 1.2 Certificado SSL

### 1.2.1 Introdução

A utilização de certificados SSL em aplicações Minimal API garante comunicação segura por meio de criptografia TLS. Essa proteção é essencial para APIs expostas publicamente ou que trafegam informações sensíveis. Um certificado SSL autentica o servidor, estabelece criptografia e evita ataques de interceptação.

### 1.2.2 Objetivo do Certificado SSL

O certificado SSL possibilita:

* Criptografia entre cliente e servidor.
* Autenticação da origem do servidor.
* Integridade dos dados transmitidos.
* Conformidade com requisitos de segurança e auditoria.

### 1.2.3 Tipos de Certificados

#### 1.2.3.1 Autossinado

Criado manualmente para ambientes de desenvolvimento e testes. Não é confiável para produção, pois não é validado por uma Autoridade Certificadora (CA).

#### 1.2.3.2 Emitido por Autoridade Certificadora

Utilizado em ambientes produtivos. Possui verificação de domínio, organização e validade.

### 1.2.4 Configuração de SSL na Minimal API

A configuração pode ser aplicada via `appsettings.json` ou diretamente no código durante o build da aplicação.

#### 1.2.4.1 Configuração via appsettings.json

    ```json
    {
    "Kestrel": {
        "Endpoints": {
        "Https": {
            "Url": "https://localhost:5001",
            "Certificate": {
            "Path": "certificado.pfx",
            "Password": "senha_segura"
            }
        }
        }
    }
    }
    ```

#### 1.2.4.2 Configuração via Código C\#

    ```csharp
    var builder = WebApplication.CreateBuilder(args);

    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(5001, listenOptions =>
        {
            listenOptions.UseHttps("certificado.pfx", "senha_segura");
        });
    });

    var app = builder.Build();

    app.MapGet("/seguro", () => "Conexão protegida com SSL");

    app.Run();
    ```

### 1.2.5 Explicação da Sintaxe

* `ConfigureKestrel` permite definir portas e certificados.
* `UseHttps` aplica o certificado ao endpoint definido.
* Arquivos `.pfx` contêm chave privada e certificado em formato compactado.

### 1.2.6 Exemplos Práticos

#### 1.2.6.1 Criando um Certificado Autossinado

    dotnet dev-certs https --trust

#### 1.2.6.2 Verificando Certificados Locais

    dotnet dev-certs https --check

#### 1.2.6.3 Exportando Certificado para Projeto

    dotnet dev-certs https -ep certificado.pfx -p senha_segura

### 1.2.7 Boas Práticas

* Utilizar certificados emitidos por CA em produção.
* Evitar armazenar senhas de certificados em repositórios.
* Renovar certificados antes do prazo de expiração.
* Configurar HTTPS como obrigatório.

### 1.2.8 Comparação entre HTTP e HTTPS

| Critério                 | HTTP           | HTTPS            |
| -----------------------*| -------------* | ---------------* |
| Segurança                | Nenhuma        | Criptografia TLS |
| Portas padrão            | 80             | 443              |
| Ideal para               | Testes simples | Produção         |
| Autenticação do servidor | Não            | Sim              |

### 1.2.9 Erros Comuns

* Senha incorreta no arquivo `.pfx`.
* Arquivo de certificado não encontrado no caminho configurado.
* Porta HTTPS bloqueada pelo sistema operacional.
* Uso de certificados autossinados em produção.

### 1.2.10 Conclusão

A configuração adequada de certificados SSL é fundamental para garantir segurança em APIs. A Minimal API integrada ao Kestrel oferece suporte nativo para certificados, permitindo implementar HTTPS de forma simples e eficiente.

## 1.3 Configurando EF

### 1.3.1 Introdução

A integração do Entity Framework Core em uma aplicação Minimal API permite mapear entidades, configurar o acesso ao banco de dados e realizar operações CRUD de forma eficiente. O EF Core funciona como um ORM (Object‑Relational Mapper), facilitando a persistência de dados e reduzindo a necessidade de consultas SQL manuais.

### 1.3.2 Registro do DbContext

A configuração do EF Core inicia com o registro do `DbContext` no container de injeção de dependências. No exemplo apresentado, utiliza‑se SQLite como provedor.

#### 1.3.2.1 Configuração no Program.cs

    ```csharp
    using Microsoft.EntityFrameworkCore;
    using RangoAgil.API.DbContexts;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<RangoDbContext>(
        o => o.UseSqlite(builder.Configuration["ConnectionStrings:RangoDBConStr"])
    );

    var app = builder.Build();

    app.MapGet("/", () => "Hello World!");

    app.Run();
    ```

### 1.3.3 Criação do DbContext

O `DbContext` é responsável por representar a sessão com o banco de dados, gerenciar entidades e aplicar configurações de mapeamento.

    ```csharp
    using Microsoft.EntityFrameworkCore;
    using RangoAgil.API.Entities;

    namespace RangoAgil.API.DbContexts;

    public class RangoDbContext(DbContextOptions<RangoDbContext> options) : DbContext(options)
    {
        public DbSet<Rango> Rangos { get; set; } = null!;
        public DbSet<Ingrediente> Ingredientes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
    ```

### 1.3.4 Entidades do Domínio

As classes `Rango` e `Ingrediente` representam tabelas no banco. O EF Core utiliza anotações (`DataAnnotations`) para mapear propriedades, restrições e relacionamentos.

#### 1.3.4.1 Entidade Ingrediente

    ```csharp
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    namespace RangoAgil.API.Entities;

    public class Ingrediente
    {   
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Nome { get; set; }

        public ICollection<Rango> Rangos { get; set; } = [];

        public Ingrediente() {}

        [SetsRequiredMembers]
        public Ingrediente(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
    ```

#### 1.3.4.2 Entidade Rango

    ```csharp
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    namespace RangoAgil.API.Entities;

    public class Rango
    {   
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public required string Nome { get; set; }

        public ICollection<Ingrediente> Ingredientes { get; set; } = [];

        public Rango() {}

        [SetsRequiredMembers]
        public Rango(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
    ```

### 1.3.5 Explicação da Sintaxe

* `DbSet<T>` representa uma tabela que permite consultas e operações CRUD.
* `[Key]`, `[Required]`, `[MaxLength]` definem metadados de mapeamento.
* Construtores com `[SetsRequiredMembers]` permitem inicialização segura para propriedades `required`.
* As coleções configuram automaticamente relacionamentos muitos‑para‑muitos, pois o EF Core gera uma tabela de junção.

### 1.3.6 Configurando o Banco SQLite

A configuração usa a connection string obtida do `appsettings.json`:

    ```json
    {
    "ConnectionStrings": {
        "RangoDBConStr": "Data Source=rango.db"
    }
    }
    ```

### 1.3.7 Exemplos Práticos

#### 1.3.7.1 Inserindo Dados

    ```csharp
    app.MapPost("/ingredientes", async (Ingrediente ingrediente, RangoDbContext db) =>
    {
        db.Ingredientes.Add(ingrediente);
        await db.SaveChangesAsync();
        return Results.Created($"/ingredientes/{ingrediente.Id}", ingrediente);
    });
    ```

#### 1.3.7.2 Consultando Dados

    ```csharp
    app.MapGet("/rangos", async (RangoDbContext db) =>
    {
        return await db.Rangos.Include(r => r.Ingredientes).ToListAsync();
    });
    ```

### 1.3.8 Comparação: EF Core vs SQL Manual

| Critério                 | EF Core  | SQL Manual        |
| -----------------------*| -------* | ----------------* |
| Produtividade            | Alta     | Média             |
| Controle sobre consultas | Menor    | Total             |
| Tipagem                  | Forte    | Depende do driver |
| Curva de aprendizado     | Moderada | Variável          |

### 1.3.9 Boas Práticas

* Centralizar connection strings no `appsettings.json`.
* Utilizar `Include` apenas quando necessário.
* Criar migrations antes de publicar a aplicação.
* Evitar entidades com responsabilidades múltiplas.
* Validar dados antes de persistir.

### 1.3.10 Conclusão

A configuração do Entity Framework Core em uma Minimal API oferece uma integração eficiente para acesso a dados. O uso de `DbContext`, entidades e anotações possibilita modelagem clara, segura e extensível, garantindo uma base sólida para construção de APIs modernas.

## 1.4 Populando o DB

### 1.4.1 Introdução

Esta seção descreve a configuração de *seed data* utilizando Entity Framework Core dentro de um contexto de banco de dados aplicado a uma Minimal API. O objetivo é garantir que a aplicação inicie com dados consistentes para entidades relacionadas, permitindo testes, demonstrações e validações sem necessidade de inserções manuais.

A abordagem utiliza o método `OnModelCreating` para registrar dados iniciais tanto para entidades simples quanto para relacionamentos muitos-para-muitos.

### 1.4.2 Estrutura do DbContext

O contexto `RangoDbContext` herda de `DbContext` e expõe dois conjuntos de entidades: `Rango` e `Ingrediente`. A configuração de *seed* é realizada por meio do `ModelBuilder`.

#### 1.4.2.1 Código do DbContext

    ```csharp
    using Microsoft.EntityFrameworkCore;
    using RangoAgil.API.Entities;

    namespace RangoAgil.API.DbContexts;

    public class RangoDbContext(DbContextOptions<RangoDbContext> options) : DbContext(options)
    {
        public DbSet<Rango> Rangos { get; set; } = null!;
        public DbSet<Ingrediente> Ingredientes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Ingrediente>()
                .HasData(
                    new { Id = 1, Nome = "Carne de Vaca" },
                    new { Id = 2, Nome = "Cebola" },
                    new { Id = 3, Nome = "Cerveja Escura" },
                    new { Id = 4, Nome = "Fatia de Pão Integral" },
                    new { Id = 5, Nome = "Mostarda" },
                    new { Id = 6, Nome = "Chicória" },
                    new { Id = 7, Nome = "Maionese" },
                    new { Id = 8, Nome = "Vários Temperos" },
                    new { Id = 9, Nome = "Mexilhões" },
                    new { Id = 10, Nome = "Aipo" },
                    new { Id = 11, Nome = "Batatas Fritas" },
                    new { Id = 12, Nome = "Tomate" },
                    new { Id = 13, Nome = "Extrato de Tomate" },
                    new { Id = 14, Nome = "Folha de Louro" },
                    new { Id = 15, Nome = "Cenoura" },
                    new { Id = 16, Nome = "Alho" },
                    new { Id = 17, Nome = "Vinho Tinto" },
                    new { Id = 18, Nome = "Leite de Coco" },
                    new { Id = 19, Nome = "Gengibre" },
                    new { Id = 20, Nome = "Pimenta Malagueta" },
                    new { Id = 21, Nome = "Tamarindo" },
                    new { Id = 22, Nome = "Peixe Firme" },
                    new { Id = 23, Nome = "Pasta de Gengibre e Alho" },
                    new { Id = 24, Nome = "Garam Masala" }
                );

            _ = modelBuilder.Entity<Rango>()
                .HasData(
                    new { Id = 1, Nome = "Ensopado Flamengo de Carne de Vaca com Chicória" },
                    new { Id = 2, Nome = "Mexilhões com Batatas Fritas" },
                    new { Id = 3, Nome = "Ragu alla Bolognese" },
                    new { Id = 4, Nome = "Rendang" },
                    new { Id = 5, Nome = "Masala de Peixe" }
                );

            _ = modelBuilder.Entity<Rango>()
                .HasMany(d => d.Ingredientes)
                .WithMany(i => i.Rangos)
                .UsingEntity(e => e.HasData(
                    new { RangosId = 1, IngredientesId = 1 },
                    new { RangosId = 1, IngredientesId = 2 },
                    new { RangosId = 1, IngredientesId = 3 },
                    new { RangosId = 1, IngredientesId = 4 },
                    new { RangosId = 1, IngredientesId = 5 },
                    new { RangosId = 1, IngredientesId = 6 },
                    new { RangosId = 1, IngredientesId = 7 },
                    new { RangosId = 1, IngredientesId = 8 },
                    new { RangosId = 1, IngredientesId = 14 },
                    new { RangosId = 2, IngredientesId = 9 },
                    new { RangosId = 2, IngredientesId = 19 },
                    new { RangosId = 2, IngredientesId = 11 },
                    new { RangosId = 2, IngredientesId = 12 },
                    new { RangosId = 2, IngredientesId = 13 },
                    new { RangosId = 2, IngredientesId = 2 },
                    new { RangosId = 2, IngredientesId = 21 },
                    new { RangosId = 2, IngredientesId = 8 },
                    new { RangosId = 3, IngredientesId = 1 },
                    new { RangosId = 3, IngredientesId = 12 },
                    new { RangosId = 3, IngredientesId = 17 },
                    new { RangosId = 3, IngredientesId = 14 },
                    new { RangosId = 3, IngredientesId = 2 },
                    new { RangosId = 3, IngredientesId = 16 },
                    new { RangosId = 3, IngredientesId = 23 },
                    new { RangosId = 3, IngredientesId = 8 },
                    new { RangosId = 4, IngredientesId = 1 },
                    new { RangosId = 4, IngredientesId = 18 },
                    new { RangosId = 4, IngredientesId = 16 },
                    new { RangosId = 4, IngredientesId = 20 },
                    new { RangosId = 4, IngredientesId = 22 },
                    new { RangosId = 4, IngredientesId = 2 },
                    new { RangosId = 4, IngredientesId = 21 },
                    new { RangosId = 4, IngredientesId = 8 },
                    new { RangosId = 5, IngredientesId = 24 },
                    new { RangosId = 5, IngredientesId = 10 },
                    new { RangosId = 5, IngredientesId = 23 },
                    new { RangosId = 5, IngredientesId = 2 },
                    new { RangosId = 5, IngredientesId = 12 },
                    new { RangosId = 5, IngredientesId = 18 },
                    new { RangosId = 5, IngredientesId = 14 },
                    new { RangosId = 5, IngredientesId = 20 },
                    new { RangosId = 5, IngredientesId = 13 }
                ));

            base.OnModelCreating(modelBuilder);
        }
    }
    ```

### 1.4.3 Explicação da Sintaxe

#### 1.4.3.1 `HasData`

O método `HasData` registra dados iniciais que serão inseridos durante a migração. Ele aceita objetos anônimos contendo os campos necessários para preencher a tabela.

#### 1.4.3.2 Relacionamento muitos-para-muitos

A configuração:

    ```csharp
    .HasMany(d => d.Ingredientes)
    .WithMany(i => i.Rangos)
    .UsingEntity(e => e.HasData(...));
    ```

gera automaticamente a tabela intermediária e popula suas chaves compostas.

### 1.4.4 Exemplos Práticos

#### 1.4.4.1 Consultando dados populados em uma Minimal API

    ```csharp
    app.MapGet("/rangos", async (RangoDbContext db) =>
    {
        return await db.Rangos
            .Include(r => r.Ingredientes)
            .ToListAsync();
    });
    ```

#### 1.4.4.2 Inserindo novo ingrediente após o seed

    ```csharp
    app.MapPost("/ingredientes", async (Ingrediente ingrediente, RangoDbContext db) =>
    {
        db.Ingredientes.Add(ingrediente);
        await db.SaveChangesAsync();
        return Results.Created($"/ingredientes/{ingrediente.Id}", ingrediente);
    });
    ```

### 1.4.5 Comparação: Seed via `OnModelCreating` vs. Seed via API

| Critério | OnModelCreating | API |
| --------- | ---------------- | ---- |
| Execução | Durante migração | Em tempo de execução |
| Controle | Determinístico | Depende do cliente |
| Uso ideal | Dados fixos | Dados dinâmicos |

### 1.4.6 Boas Práticas

* Utilizar `HasData` apenas para dados estáticos e essenciais.
* Evitar inserir dados sensíveis ou dependentes de ambiente.
* Manter consistência entre IDs para evitar conflitos em migrações.
* Documentar claramente o propósito de cada conjunto de dados populados.

### 1.4.7 Conclusão

A população inicial do banco de dados é fundamental para garantir previsibilidade e facilitar o desenvolvimento de Minimal APIs. A abordagem apresentada permite estruturar dados iniciais de forma clara, escalável e alinhada às capacidades do Entity Framework Core.
