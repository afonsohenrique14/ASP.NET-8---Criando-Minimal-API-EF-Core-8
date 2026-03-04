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

### 1.2.5 Código Exemplificativo

Um exemplo básico presente em um projeto Minimal API gerado pela CLI.

    ```csharp
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    app.MapGet("/saudacao", () => "Aplicação Minimal API criada com dotnet CLI");

    app.Run();
    ```

### 1.2.6 Explicação da Sintaxe

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

# 2 EndPoint, Conceitos, Recursos

## 2.1 URL e Parâmetros

### 2.1.1 Introdução

Esta seção descreve como definir rotas em uma Minimal API utilizando parâmetros de URL. O objetivo é demonstrar como capturar valores dinâmicos enviados pelo cliente e utilizá‑los diretamente nos manipuladores de requisição.

A abordagem utiliza o padrão `{parametro}` dentro da rota, permitindo que o ASP.NET Core faça o *binding* automático para tipos primitivos como `int` e `string`.

### 2.1.2 Configuração Inicial

A aplicação registra o `DbContext` e define rotas básicas para testes. O foco desta seção é o comportamento das rotas que utilizam parâmetros.

#### 2.1.2.1 Código de Configuração

    ```csharp
    using Microsoft.EntityFrameworkCore;
    using RangoAgil.API.DbContexts;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<RangoDbContext>(
        o => o.UseSqlite(builder.Configuration["ConnectionStrings:RangoDBConStr"])
    );

    var app = builder.Build();

    app.MapGet("/", () => "Hello World!");

    app.MapGet("/rangos/{numero}", (int numero) =>
    {
        return $"{numero}";
    });

    app.MapGet("/rangos/{numero}/{nome}", (int numero, string nome) =>
    {
        return $"{nome} {numero}";
    });

    app.MapGet("/rangos", () =>
    {
        return "Está funcionando MUITO bem!!!";
    });

    app.Run();
    ```

### 2.1.3 Explicação da Sintaxe

#### 2.1.3.1 Parâmetros de Rota

A sintaxe `{parametro}` define um espaço reservado dentro da URL. O ASP.NET Core interpreta o valor recebido e tenta convertê‑lo para o tipo especificado no manipulador.

Exemplo:

    ```csharp
    app.MapGet("/rangos/{numero}", (int numero) => ... );
    ```

O valor recebido em `/rangos/10` será convertido automaticamente para `int numero = 10`.

#### 2.1.3.2 Múltiplos Parâmetros

Rotas podem conter mais de um parâmetro, desde que a ordem seja respeitada.

    ```csharp
    app.MapGet("/rangos/{numero}/{nome}", (int numero, string nome) => ... );
    ```

Chamada:  
`/rangos/25/Feijoada`  
Resultado:  
`Feijoada 25`

#### 2.1.3.3 Rotas Estáticas vs. Dinâmicas

Rotas estáticas não possuem parâmetros e sempre retornam o mesmo conteúdo.

    ```csharp
    app.MapGet("/rangos", () => "Está funcionando MUITO bem!!!");
    ```

Rotas dinâmicas dependem dos valores enviados pelo cliente.

### 2.1.4 Exemplos Práticos

#### 2.1.4.1 Retornando um número enviado na URL

    ```csharp
    app.MapGet("/numero/{valor}", (int valor) =>
    {
        return $"Valor recebido: {valor}";
    });
    ```

Chamada:  
`/numero/42`  
Resposta:  
`Valor recebido: 42`

#### 2.1.4.2 Combinando parâmetros numéricos e textuais

    ```csharp
    app.MapGet("/produto/{id}/{categoria}", (int id, string categoria) =>
    {
        return $"Produto {id} da categoria {categoria}";
    });
    ```

Chamada:  
`/produto/7/bebidas`  
Resposta:  
`Produto 7 da categoria bebidas`

#### 2.1.4.3 Utilizando parâmetros para consultar o banco

    ```csharp
    app.MapGet("/rangos/db/{id}", async (int id, RangoDbContext db) =>
    {
        var rango = await db.Rangos.FindAsync(id);
        return rango is null ? Results.NotFound() : Results.Ok(rango);
    });
    ```

### 2.1.5 Comparação: Parâmetros de Rota vs. Query String

| Critério | Parâmetros de Rota | Query String |
| --------- | --------------------- | -------------- |
| Forma | `/rangos/10` | `/rangos?id=10` |
| Semântica | Identificação direta do recurso | Filtros e modificadores |
| Uso ideal | Acesso a um item específico | Paginação, filtros, ordenação |

### 2.1.6 Boas Práticas

* Utilizar parâmetros de rota para identificar recursos únicos.
* Evitar rotas ambíguas que possam conflitar entre si.
* Manter nomes de parâmetros claros e consistentes.
* Preferir tipos primitivos simples para facilitar o *binding*.
* Validar valores recebidos quando necessário.

### 2.1.7 Conclusão

O uso de parâmetros de rota em Minimal APIs permite criar endpoints expressivos, simples e eficientes. A conversão automática de tipos e a clareza na definição das rotas tornam o desenvolvimento mais direto e menos propenso a erros, especialmente em APIs enxutas e de alta performance.

## 2.2 Conteúdo do BD

### 2.2.1 Introdução

Esta seção apresenta como expor o conteúdo do banco de dados por meio de endpoints em uma Minimal API. O foco está na leitura de dados utilizando Entity Framework Core, retornando tanto coleções quanto registros individuais.

A abordagem utiliza *dependency injection* para disponibilizar o `RangoDbContext` diretamente nos manipuladores das rotas, permitindo consultas simples e eficientes.

### 2.2.2 Configuração Inicial

A aplicação registra o `DbContext` com SQLite e define rotas que retornam dados persistidos no banco. O objetivo é demonstrar como recuperar registros completos ou filtrados por ID.

#### 2.2.2.1 Código de Configuração

    ```csharp
    using Microsoft.EntityFrameworkCore;
    using RangoAgil.API.DbContexts;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<RangoDbContext>(
        o => o.UseSqlite(builder.Configuration["ConnectionStrings:RangoDBConStr"])
    );

    var app = builder.Build();

    app.MapGet("/", () => "Hello World!");

    app.MapGet("/rango/{id}", (RangoDbContext rangoDbContext, int id) =>
    {
        return rangoDbContext.Rangos.FirstOrDefault(x => x.Id == id);
    });

    app.MapGet("/rangos", (RangoDbContext rangoDbContext) =>
    {
        return rangoDbContext.Rangos;
    });

    app.Run();
    ```

### 2.2.3 Explicação da Sintaxe

#### 2.2.3.1 Injeção do DbContext

O ASP.NET Core injeta automaticamente o `RangoDbContext` no manipulador da rota:

    ```csharp
    (RangoDbContext rangoDbContext, int id)
    ```

Isso permite acessar o banco sem criar instâncias manualmente.

#### 2.2.3.2 Consulta por ID

O método `FirstOrDefault` retorna o primeiro registro que atende ao predicado ou `null` caso não exista.

    ```csharp
    rangoDbContext.Rangos.FirstOrDefault(x => x.Id == id);
    ```

#### 2.2.3.3 Retorno de Coleção

Ao retornar `rangoDbContext.Rangos`, o EF Core expõe um `DbSet<Rango>`, que será serializado automaticamente para JSON.

### 2.2.4 Exemplos Práticos

#### 2.2.4.1 Consultando um item específico

    ```csharp
    app.MapGet("/ingrediente/{id}", async (int id, RangoDbContext db) =>
    {
        var ingrediente = await db.Ingredientes.FindAsync(id);
        return ingrediente is null ? Results.NotFound() : Results.Ok(ingrediente);
    });
    ```

Chamada:  
`/ingrediente/3`  
Resposta:  
Objeto JSON contendo o ingrediente de ID 3.

#### 2.2.4.2 Listando todos os itens de uma tabela

    ```csharp
    app.MapGet("/ingredientes", async (RangoDbContext db) =>
    {
        return await db.Ingredientes.ToListAsync();
    });
    ```

Chamada:  
`/ingredientes`  
Resposta:  
Lista completa de ingredientes.

#### 2.2.4.3 Incluindo relacionamentos

    ```csharp
    app.MapGet("/rangos-com-ingredientes", async (RangoDbContext db) =>
    {
        return await db.Rangos
            .Include(r => r.Ingredientes)
            .ToListAsync();
    });
    ```

### 2.2.5 Comparação: `Find`, `FirstOrDefault` e `SingleOrDefault`

| Método | Comportamento | Uso Ideal |
| -------- | ---------------- | ----------- |
| `Find` | Busca por chave primária, usa cache | Consultas simples por ID |
| `FirstOrDefault` | Retorna o primeiro que atende ao predicado | Filtros flexíveis |
| `SingleOrDefault` | Exige que exista no máximo um registro | Garantia de unicidade |

### 2.2.6 Boas Práticas

* Utilizar `FindAsync` para buscas por chave primária.
* Retornar `NotFound` quando o registro não existir.
* Evitar expor `DbSet` diretamente em cenários complexos; preferir `ToListAsync`.
* Utilizar `Include` apenas quando necessário para evitar sobrecarga.
* Manter rotas claras e sem ambiguidade.

### 2.2.7 Conclusão

A leitura de dados em Minimal APIs com Entity Framework Core é direta e eficiente. A injeção automática do contexto, combinada com métodos de consulta simples, permite construir endpoints limpos e performáticos, adequados tanto para protótipos quanto para aplicações reais.

## 2.3 Tipos de Parâmetros

### 2.3.1 Introdução

Esta seção apresenta o uso de diferentes tipos de parâmetros em rotas de Minimal APIs, destacando como o ASP.NET Core realiza *route matching* quando múltiplas rotas semelhantes coexistem. O foco está na utilização de *route constraints* para diferenciar rotas que poderiam entrar em conflito, garantindo previsibilidade e segurança no processamento das requisições.

### 2.3.2 Configuração Inicial

A aplicação registra o `RangoDbContext` e define rotas que utilizam parâmetros tipados. O objetivo é demonstrar como o tipo do parâmetro influencia a seleção da rota correta.

#### 2.3.2.1 Código de Configuração

    ```csharp
    using Microsoft.EntityFrameworkCore;
    using RangoAgil.API.DbContexts;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<RangoDbContext>(
        o => o.UseSqlite(builder.Configuration["ConnectionStrings:RangoDBConStr"])
    );

    var app = builder.Build();

    app.MapGet("/", () => "Hello World!");

    app.MapGet("/rango/{id:int}", (RangoDbContext rangoDbContext, int id) =>
    {
        return rangoDbContext.Rangos.FirstOrDefault(x => x.Id == id);
    });

    app.MapGet("/rango/{nome}", (RangoDbContext rangoDbContext, string nome) =>
    {
        return rangoDbContext.Rangos.FirstOrDefault(x => x.Nome == nome);
    });

    app.MapGet("/rangos", (RangoDbContext rangoDbContext) =>
    {
        return rangoDbContext.Rangos;
    });

    app.Run();
    ```

### 2.3.3 Explicação da Sintaxe

#### 2.3.3.1 Route Constraints

A expressão `{id:int}` define que o parâmetro só será aceito se puder ser convertido para `int`.  
Isso evita conflitos com a rota `{nome}`, que aceita qualquer texto.

    ```csharp
    /rango/10     → corresponde a {id:int}
    /rango/feijoada → corresponde a {nome}
    ```

#### 2.3.3.2 Parâmetros Sem Restrição

A rota:

    ```csharp
    /rango/{nome}
    ```

aceita qualquer valor que não seja reconhecido como inteiro, permitindo buscas textuais.

#### 2.3.3.3 Ordem de Resolução

O ASP.NET Core tenta casar rotas da mais específica para a mais genérica.  
A presença de `{id:int}` torna a rota numérica mais específica, evitando ambiguidade.

### 2.3.4 Exemplos Práticos

#### 2.3.4.1 Rota com parâmetro inteiro

    ```csharp
    app.MapGet("/produto/{codigo:int}", (int codigo) =>
    {
        return $"Código recebido: {codigo}";
    });
    ```

Chamada:  
`/produto/55`  
Resposta:  
`Código recebido: 55`

#### 2.3.4.2 Rota com parâmetro textual

    ```csharp
    app.MapGet("/produto/{nome}", (string nome) =>
    {
        return $"Produto: {nome}";
    });
    ```

Chamada:  
`/produto/cafe`  
Resposta:  
`Produto: cafe`

#### 2.3.4.3 Combinando constraints adicionais

    ```csharp
    app.MapGet("/pedido/{ano:int:min(2000)}/{mes:int:range(1,12)}", (int ano, int mes) =>
    {
        return $"Pedidos de {mes}/{ano}";
    });
    ```

Chamada válida:  
`/pedido/2024/5`

Chamada inválida:  
`/pedido/1999/13`

### 2.3.5 Comparação: Parâmetros Tipados vs. Não Tipados

| Critério | Tipados (`{id:int}`) | Não Tipados (`{nome}`) |
| --------- | ------------------------ | -------------------------- |
| Validação | Automática | Manual |
| Previsibilidade | Alta | Média |
| Risco de conflito | Baixo | Alto |
| Uso ideal | Identificadores numéricos | Nomes, códigos, textos |

### 2.3.6 Boas Práticas

* Utilizar *route constraints* sempre que houver possibilidade de conflito entre rotas.
* Evitar rotas excessivamente genéricas que possam capturar valores inesperados.
* Manter nomes de parâmetros coerentes com o tipo esperado.
* Preferir parâmetros tipados para garantir validação automática.
* Documentar claramente o comportamento de rotas semelhantes.

### 2.3.7 Conclusão

O uso de tipos de parâmetros e *route constraints* em Minimal APIs permite criar rotas mais seguras, previsíveis e organizadas. A diferenciação entre rotas numéricas e textuais evita ambiguidades e melhora a clareza da API, especialmente em cenários onde múltiplos padrões de URL coexistem.

## 2.4 Parameter Binding

### 2.4.1 Introdução

Esta seção apresenta os diferentes mecanismos de *parameter binding* disponíveis em Minimal APIs. O ASP.NET Core permite extrair valores de diversas origens da requisição — como cabeçalhos, query string e rota — utilizando atributos específicos. Isso torna o código mais explícito, previsível e fácil de manter.

O exemplo demonstra como recuperar parâmetros a partir de *headers*, *query strings* e rotas, além de como combinar esses mecanismos com Entity Framework Core para consultar o banco de dados.

### 2.4.2 Código de Configuração

    ```csharp
    app.MapGet("/", () => "Hello World!");

    app.MapGet("/rango", (RangoDbContext rangoDbContext, [FromHeader(Name ="RangoId")] int id) =>
    {
        return rangoDbContext.Rangos.FirstOrDefault(x => x.Id == id);
    });

    app.MapGet("/rango/Header", (RangoDbContext rangoDbContext, [FromHeader] int id) =>
    {
        return rangoDbContext.Rangos.FirstOrDefault(x => x.Id == id);
    });

    app.MapGet("/rango/Query", (RangoDbContext rangoDbContext, [FromQuery] int id) =>
    {
        return rangoDbContext.Rangos.FirstOrDefault(x => x.Id == id);
    });

    app.MapGet("/rango/{nome}", (RangoDbContext rangoDbContext, string nome) =>
    {
        return rangoDbContext.Rangos.FirstOrDefault(x => x.Nome == nome);
    });

    app.MapGet("/rangos", (RangoDbContext rangoDbContext) =>
    {
        return rangoDbContext.Rangos;
    });

    app.Run();
    ```

### 2.4.3 Explicação da Sintaxe

#### 2.4.3.1 `[FromHeader]`

O atributo `[FromHeader]` indica que o valor deve ser extraído do cabeçalho HTTP.

    ```csharp
    [FromHeader(Name = "RangoId")] int id
    ```

* `Name` define o nome exato do cabeçalho.
* Se omitido, o nome do parâmetro é utilizado como chave.

Exemplo de chamada:

    ```
    GET /rango
    RangoId: 3
    ```

#### 2.4.3.2 `[FromQuery]`

O atributo `[FromQuery]` força a leitura do valor a partir da query string.

    ```csharp
    [FromQuery] int id
    ```

Chamada:

    ```
    /rango/Query?id=4
    ```

#### 2.4.3.3 Parâmetros de Rota

A rota:

    ```csharp
    /rango/{nome}
    ```

captura o valor diretamente da URL, sem necessidade de atributos adicionais.

Chamada:

    ```
    /rango/Rendang
    ```

### 2.4.4 Exemplos Práticos

#### 2.4.4.1 Recebendo valores de cabeçalho

    ```csharp
    app.MapGet("/cliente", ([FromHeader(Name = "ClienteId")] int id) =>
    {
        return $"Cliente recebido: {id}";
    });
    ```

Chamada:

    ```
    GET /cliente
    ClienteId: 99
    ```

#### 2.4.4.2 Recebendo valores de query string

    ```csharp
    app.MapGet("/busca", ([FromQuery] string termo) =>
    {
        return $"Termo buscado: {termo}";
    });
    ```

Chamada:

    ```
    /busca?termo=batata
    ```

#### 2.4.4.3 Combinando rota e query string

    ```csharp
    app.MapGet("/produto/{id}", (int id, [FromQuery] bool detalhado) =>
    {
        return $"Produto {id}, detalhado: {detalhado}";
    });
    ```

Chamada:

    ```
    /produto/5?detalhado=true
    ```

### 2.4.5 Comparação entre Origens de Parâmetros

| Origem | Exemplo | Uso Ideal |
| -------- | --------- | ----------- |
| Header | `RangoId: 3` | Metadados, autenticação, identificação técnica |
| Query String | `/rango/Query?id=4` | Filtros, paginação, parâmetros opcionais |
| Rota | `/rango/Rendang` | Identificação direta do recurso |

### 2.4.6 Boas Práticas

* Utilizar `[FromHeader]` apenas para informações que fazem sentido como metadados.
* Preferir `[FromQuery]` para filtros e parâmetros opcionais.
* Utilizar parâmetros de rota para identificar recursos principais.
* Evitar misturar muitas origens de parâmetros no mesmo endpoint.
* Documentar claramente quais valores devem ser enviados em cada origem.

### 2.4.7 Conclusão

O *parameter binding* em Minimal APIs é flexível e poderoso, permitindo extrair valores de diferentes partes da requisição de forma clara e tipada. A utilização de atributos como `[FromHeader]` e `[FromQuery]` torna o comportamento explícito e reduz ambiguidades, contribuindo para APIs mais robustas e previsíveis.

## 2.5 Async Await

### 2.5.1 Introdução

Esta seção apresenta o uso de operações assíncronas em Minimal APIs utilizando `async` e `await`. O Entity Framework Core oferece métodos assíncronos para consultas ao banco de dados, permitindo que a aplicação escale melhor sob carga, evitando bloqueios de threads e melhorando a responsividade.

O objetivo é demonstrar como implementar endpoints assíncronos que consultam registros individuais e coleções completas.

### 2.5.2 Código de Configuração

    ```csharp
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using RangoAgil.API.DbContexts;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<RangoDbContext>(
        o => o.UseSqlite(builder.Configuration["ConnectionStrings:RangoDBConStr"])
    );

    var app = builder.Build();

    app.MapGet("/", () => "Hello World!");

    app.MapGet("/rango", async (RangoDbContext rangoDbContext, [FromQuery] int id) =>
    {
        return await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == id);
    });

    app.MapGet("/rango/{nome}", async (RangoDbContext rangoDbContext, string nome) =>
    {
        return await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Nome == nome);
    });

    app.MapGet("/rangos", async (RangoDbContext rangoDbContext) =>
    {
        return await rangoDbContext.Rangos.ToListAsync();
    });

    app.Run();
    ```

### 2.5.3 Explicação da Sintaxe

#### 2.5.3.1 Métodos Assíncronos do EF Core

O Entity Framework Core disponibiliza versões assíncronas dos principais métodos de consulta:

* `FirstOrDefaultAsync`
* `SingleOrDefaultAsync`
* `FindAsync`
* `ToListAsync`

Esses métodos retornam `Task<T>`, permitindo o uso de `await`.

#### 2.5.3.2 Uso de `async` e `await`

A assinatura do endpoint deve ser marcada como assíncrona:

    ```csharp
    async (RangoDbContext db, int id) => { ... }
    ```

O operador `await` suspende a execução até que a operação seja concluída, liberando a thread para outras requisições.

#### 2.5.3.3 Benefícios do Modelo Assíncrono

* Melhor escalabilidade sob alta carga.
* Evita bloqueio de threads do servidor.
* Integração nativa com EF Core e ASP.NET Core.

### 2.5.4 Exemplos Práticos

#### 2.5.4.1 Consultando um registro por ID

    ```csharp
    app.MapGet("/ingrediente", async (RangoDbContext db, [FromQuery] int id) =>
    {
        var ingrediente = await db.Ingredientes.FirstOrDefaultAsync(x => x.Id == id);
        return ingrediente is null ? Results.NotFound() : Results.Ok(ingrediente);
    });
    ```

#### 2.5.4.2 Listando registros com filtro

    ```csharp
    app.MapGet("/rangos/filtro", async (RangoDbContext db, [FromQuery] string termo) =>
    {
        return await db.Rangos
            .Where(r => r.Nome.Contains(termo))
            .ToListAsync();
    });
    ```

#### 2.5.4.3 Incluindo relacionamentos de forma assíncrona

    ```csharp
    app.MapGet("/rangos-com-ingredientes", async (RangoDbContext db) =>
    {
        return await db.Rangos
            .Include(r => r.Ingredientes)
            .ToListAsync();
    });
    ```

### 2.5.5 Comparação: Métodos Síncronos vs. Assíncronos

| Critério | Síncrono | Assíncrono |
| --------- | ---------- | ------------ |
| Bloqueio de thread | Sim | Não |
| Escalabilidade | Menor | Maior |
| Uso ideal | Scripts, testes simples | APIs em produção |
| Integração com EF Core | Completa | Completa |

### 2.5.6 Boas Práticas

* Utilizar sempre métodos assíncronos em APIs reais.
* Evitar misturar métodos síncronos e assíncronos no mesmo fluxo.
* Retornar `NotFound` quando o registro não existir.
* Utilizar `ToListAsync` antes de retornar coleções.
* Manter nomes de rotas e parâmetros consistentes.

### 2.5.7 Conclusão

O uso de `async` e `await` em Minimal APIs é essencial para construir aplicações modernas, escaláveis e eficientes. A integração com o Entity Framework Core facilita a implementação de consultas assíncronas, garantindo melhor desempenho e aproveitamento dos recursos do servidor.

## 2.6 HTTP Results

### 2.6.1 Introdução

Esta seção apresenta o uso de *HTTP Results* tipados em Minimal APIs. O ASP.NET Core fornece o namespace `TypedResults`, que permite retornar respostas HTTP fortemente tipadas, aumentando a clareza, previsibilidade e segurança dos endpoints.

O objetivo é demonstrar como retornar diferentes tipos de respostas — como `Ok`, `NoContent`, `NotFound` — utilizando *result types* combinados com `Results<T1, T2>`.

### 2.6.2 Código de Configuração

    ```csharp
    app.MapGet("/rango/{id:int}", async (RangoDbContext rangoDbContext, int id) =>
    {
        return await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == id);
    });

    app.MapGet("/rangos",
        async Task<Results<NoContent, Ok<List<Rango>>>>
        (RangoDbContext rangoDbContext, [FromQuery(Name = "name")] string? rangoNome) =>
    {
        var rangosEntity = await rangoDbContext.Rangos
                                    .Where(x => x.Nome.Contains(rangoNome!))
                                    .ToListAsync();

        if (rangosEntity.Count <= 0 || rangosEntity == null)
            return TypedResults.NoContent();

        return TypedResults.Ok(rangosEntity);
    });
    ```

### 2.6.3 Explicação da Sintaxe

#### 2.6.3.1 `Results<T1, T2>`

O tipo `Results<T1, T2>` define explicitamente quais respostas o endpoint pode retornar.  
No exemplo:

    ```csharp
    Results<NoContent, Ok<List<Rango>>>
    ```

O endpoint pode retornar:

* `204 No Content`
* `200 OK` com uma lista de `Rango`

Isso melhora a documentação automática e auxilia ferramentas como Swagger.

#### 2.6.3.2 `TypedResults.Ok`

Retorna uma resposta HTTP 200 com o corpo especificado.

    ```csharp
    TypedResults.Ok(rangosEntity);
    ```

#### 2.6.3.3 `TypedResults.NoContent`

Retorna uma resposta HTTP 204 sem corpo.

    ```csharp
    TypedResults.NoContent();
    ```

#### 2.6.3.4 Consulta com Filtro

O uso de:

    ```csharp
    .Where(x => x.Nome.Contains(rangoNome!))
    ```

permite filtrar resultados com base no nome enviado via query string.

### 2.6.4 Exemplos Práticos

#### 2.6.4.1 Endpoint com múltiplos resultados possíveis

    ```csharp
    app.MapGet("/ingredientes",
        async Task<Results<NotFound, Ok<List<Ingrediente>>>> (RangoDbContext db) =>
    {
        var ingredientes = await db.Ingredientes.ToListAsync();

        if (ingredientes.Count == 0)
            return TypedResults.NotFound();

        return TypedResults.Ok(ingredientes);
    });
    ```

#### 2.6.4.2 Retornando `Created` com TypedResults

    ```csharp
    app.MapPost("/rango",
        async Task<Created<Rango>> (RangoDbContext db, Rango novoRango) =>
    {
        db.Rangos.Add(novoRango);
        await db.SaveChangesAsync();

        return TypedResults.Created($"/rango/{novoRango.Id}", novoRango);
    });
    ```

#### 2.6.4.3 Retornando `BadRequest`

    ```csharp
    app.MapGet("/buscar",
        (string? termo) =>
    {
        if (string.IsNullOrWhiteSpace(termo))
            return TypedResults.BadRequest("O parâmetro 'termo' é obrigatório.");

        return TypedResults.Ok($"Termo recebido: {termo}");
    });
    ```

### 2.6.5 Comparação: `IResult` vs. `TypedResults`

| Critério | `IResult` | `TypedResults` |
| --------- | ----------- | ---------------- |
| Tipagem | Genérica | Fortemente tipada |
| Documentação automática | Limitada | Detalhada |
| Previsibilidade | Média | Alta |
| Uso ideal | Endpoints simples | APIs robustas e documentadas |

### 2.6.6 Boas Práticas

* Utilizar `TypedResults` para endpoints que retornam múltiplos tipos de resposta.
* Evitar retornar objetos nulos; preferir `NotFound` ou `NoContent`.
* Manter consistência nos tipos retornados por cada rota.
* Utilizar `Results<T1, T2>` para clareza e documentação automática.
* Validar parâmetros antes de consultar o banco.

### 2.6.7 Conclusão

O uso de *HTTP Results* tipados em Minimal APIs melhora a robustez e a clareza da API, permitindo respostas mais explícitas e previsíveis. A combinação de `TypedResults` com `Results<T1, T2>` fornece uma abordagem moderna e segura para lidar com múltiplos cenários de retorno, tornando a API mais confiável e bem estruturada.

## 2.7 Organizando Código

### 2.7.1 Introdução

Esta seção aborda estratégias para organizar o código em Minimal APIs, mantendo clareza, separação de responsabilidades e facilidade de manutenção. À medida que a API cresce, torna‑se essencial estruturar endpoints, regras de negócio e consultas ao banco de dados de forma modular e previsível.

O exemplo apresentado demonstra como aplicar filtros opcionais, retornar resultados tipados e manter o endpoint enxuto e legível.

### 2.7.2 Código do Endpoint Organizado

    ```csharp
    app.MapGet("/rangos",
        async Task<Results<NoContent, Ok<List<Rango>>>>
        (RangoDbContext rangoDbContext, [FromQuery(Name = "name")] string? rangoNome) =>
        {
            var rangosEntity = await rangoDbContext.Rangos
                                    .Where(
                                        x =>
                                            rangoNome == null ||
                                            x.Nome.Contains(rangoNome!)
                                    )
                                    .ToListAsync();

            if (rangosEntity.Count <= 0 || rangosEntity == null)
                return TypedResults.NoContent();

            return TypedResults.Ok(rangosEntity);
        });
    ```

### 2.7.3 Explicação da Sintaxe

#### 2.7.3.1 Filtro Opcional

A expressão:

    ```csharp
    rangoNome == null || x.Nome.Contains(rangoNome!)
    ```

permite que o endpoint funcione tanto com quanto sem filtro.  

* Se `rangoNome` for `null`, todos os registros são retornados.  
* Se houver valor, aplica‑se o filtro `Contains`.

### 2.8.5 Conclusão

Organizar o código em Minimal APIs é fundamental para manter a aplicação sustentável à medida que cresce. A separação entre lógica de consulta, validação e retorno, combinada com o uso de métodos auxiliares e extensões, resulta em uma API mais limpa, modular e fácil de evoluir.

## 2.8 AutoMapper e DTOs

### 2.8.1 Introdução

Esta seção apresenta o uso de **AutoMapper** e **DTOs (Data Transfer Objects)** em Minimal APIs. O objetivo é separar as entidades do domínio dos modelos expostos pela API, garantindo segurança, clareza e controle sobre os dados retornados ao cliente.

O AutoMapper automatiza o processo de conversão entre entidades e DTOs, reduzindo código repetitivo e evitando exposição indevida de propriedades internas.

### 2.8.2 Configuração Inicial

A aplicação registra o `DbContext`, adiciona o AutoMapper e define endpoints que retornam DTOs mapeados a partir das entidades.

#### 2.8.2.1 Código de Configuração

    ```csharp
    using System.Net;
    using System.Text.Json.Serialization;
    using AutoMapper;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using RangoAgil.API.DbContexts;
    using RangoAgil.API.Entities;
    using RangoAgil.API.Models;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<RangoDbContext>(
        o => o.UseSqlite(builder.Configuration["ConnectionStrings:RangoDBConStr"])
    );

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    var app = builder.Build();

    app.MapGet("/", () => "Hello World!");

    app.MapGet("/rangos",
        async Task<Results<NoContent, Ok<List<Rango>>>>
        (RangoDbContext rangoDbContext, [FromQuery(Name = "name")] string? rangoNome) =>
        {
            var rangosEntity = await rangoDbContext.Rangos
                                    .Where(
                                        x =>
                                            rangoNome == null ||
                                            x.Nome.ToLower().Contains(rangoNome.ToLower())
                                    )
                                    .ToListAsync();

            if (rangosEntity.Count <= 0 || rangosEntity == null)
                return TypedResults.NoContent();

            return TypedResults.Ok(rangosEntity);
        });

    app.MapGet("/rango/{id:int}", async (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        int id) =>
    {
        return mapper.Map<RangoDTO>(
            await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == id)
        );
    });

    app.MapGet("/rango/{rangoId:int}/ingredientes", async (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        int rangoId) =>
    {
        return mapper.Map<IEnumerable<IngredienteDTO>>(
            (await rangoDbContext.Rangos
                .Include(r => r.Ingredientes)
                .FirstOrDefaultAsync(r => r.Id == rangoId))?.Ingredientes
        );
    });

    app.Run();
    ```

### 2.8.3 Perfis de Mapeamento

O AutoMapper utiliza perfis para definir como entidades são convertidas em DTOs.

#### 2.8.3.1 Código do Profile

    ```csharp
    using AutoMapper;
    using RangoAgil.API.Entities;
    using RangoAgil.API.Models;

    namespace RangoAgil.API.Profiles;

    public class RangoAgilProfile : Profile
    {
        public RangoAgilProfile()
        {
            CreateMap<Rango, RangoDTO>().ReverseMap();

            CreateMap<Ingrediente, IngredienteDTO>()
                .ForMember(
                    d => d.RangoId,
                    o => o.MapFrom(s => s.Rangos.First().Id)
                );
        }
    }
    ```

### 2.8.4 DTOs Utilizados

#### 2.8.4.1 RangoDTO

    ```csharp
    namespace RangoAgil.API.Models;

    public class RangoDTO
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
    }
    ```

#### 2.8.4.2 IngredienteDTO

    ```csharp
    namespace RangoAgil.API.Models;

    public class IngredienteDTO
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public int RangoId { get; set; }
    }
    ```

### 2.8.5 Explicação da Sintaxe

#### 2.8.5.1 `CreateMap<TSource, TDestination>()`

Define o mapeamento entre entidade e DTO.  
O uso de `.ReverseMap()` permite converter nos dois sentidos.

#### 2.8.5.2 `ForMember`

Permite personalizar o mapeamento de propriedades específicas.

    ```csharp
    .ForMember(
        d => d.RangoId,
        o => o.MapFrom(s => s.Rangos.First().Id)
    )
    ```

Essa configuração extrai o primeiro `Rango` associado ao ingrediente e usa seu ID no DTO.

#### 2.8.5.3 Injeção de `IMapper`

O AutoMapper é injetado diretamente no endpoint:

    ```csharp
    (RangoDbContext db, IMapper mapper, int id)
    ```

### 2.8.6 Exemplos Práticos

#### 2.8.6.1 Retornando um DTO de Rango

    ```csharp
    app.MapGet("/rango-dto/{id:int}", async (RangoDbContext db, IMapper mapper, int id) =>
    {
        var entity = await db.Rangos.FindAsync(id);
        return entity is null ? Results.NotFound() : Results.Ok(mapper.Map<RangoDTO>(entity));
    });
    ```

#### 2.8.6.2 Retornando Ingredientes com DTOs

    ```csharp
    app.MapGet("/ingredientes-dto", async (RangoDbContext db, IMapper mapper) =>
    {
        var ingredientes = await db.Ingredientes.Include(i => i.Rangos).ToListAsync();
        return mapper.Map<IEnumerable<IngredienteDTO>>(ingredientes);
    });
    ```

### 2.8.7 Comparação: Entidades vs. DTOs

| Critério | Entidades | DTOs |
| --------- | ----------- | ------ |
| Exposição de dados | Completa | Controlada |
| Acoplamento | Alto | Baixo |
| Segurança | Menor | Maior |
| Uso ideal | Persistência | Comunicação com o cliente |

### 2.8.8 Boas Práticas

* Nunca expor entidades diretamente em APIs públicas.
* Utilizar DTOs para controlar exatamente o que é retornado.
* Centralizar mapeamentos em perfis AutoMapper.
* Evitar lógica complexa dentro de DTOs.
* Validar dados antes de mapear para entidades.

### 2.8.9 Conclusão

O uso de AutoMapper e DTOs em Minimal APIs melhora a organização, segurança e clareza da aplicação. A separação entre entidades e modelos de transferência evita exposição indevida de dados e facilita a evolução da API, mantendo o código limpo e sustentável.

# 3 Manipulação de Recursos 

## 3.1 Endpoint Post

### 3.1.1 Introdução

Esta seção apresenta a criação de um endpoint **POST** em uma Minimal API utilizando **AutoMapper**, **DTOs** e **Entity Framework Core**. O objetivo é demonstrar como receber dados do cliente, validá‑los implicitamente via modelo, mapear para uma entidade, persistir no banco de dados e retornar um DTO representando o recurso criado.

O padrão adotado mantém o endpoint limpo, organizado e alinhado às boas práticas de APIs REST.

### 3.1.2 Estrutura do Endpoint POST

    ```csharp
    app.MapPost("/rango", async Task<Ok<RangoDTO>>
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        RangoForCreationDTO rangoForCreation) =>  
    {
        var rangosEntity = mapper.Map<Rango>(rangoForCreation);

        rangoDbContext.Add(rangosEntity);
        await rangoDbContext.SaveChangesAsync();

        var rangoToReturn = mapper.Map<RangoDTO>(rangosEntity);

        return TypedResults.Ok(rangoToReturn); 
    });
    ```

### 3.1.3 Explicação da Sintaxe

#### 3.1.3.1 Recebendo o DTO de Criação

O parâmetro:

    ```csharp
    RangoForCreationDTO rangoForCreation
    ```

representa o corpo da requisição enviado pelo cliente.  
Esse DTO contém apenas os campos necessários para criar um novo registro, evitando exposição de propriedades sensíveis ou desnecessárias.

#### 3.1.3.2 Mapeamento com AutoMapper

A conversão entre DTO e entidade é feita por:

    ```csharp
    mapper.Map<Rango>(rangoForCreation);
    ```

Esse processo reduz código repetitivo e garante consistência entre modelos.

#### 3.1.3.3 Persistência no Banco de Dados

A entidade é adicionada ao contexto:

    ```csharp
    rangoDbContext.Add(rangosEntity);
    await rangoDbContext.SaveChangesAsync();
    ```

O método `SaveChangesAsync` grava o novo registro no banco e atualiza propriedades como o `Id`.

#### 3.1.3.4 Retorno do DTO

Após salvar, o endpoint retorna um DTO representando o recurso criado:

    ```csharp
    TypedResults.Ok(rangoToReturn);
    ```

O uso de `Ok<T>` deixa explícito o tipo retornado, facilitando documentação e integração.

### 3.1.4 Exemplo Prático de Requisição

#### 3.1.4.1 Corpo da Requisição (JSON)

    ```json
    {
    "nome": "Feijoada Especial"
    }
    ```

#### 3.1.4.2 Resposta Esperada

    ```json
    {
    "id": 6,
    "nome": "Feijoada Especial"
    }
    ```

### 3.1.5 Comparação: POST com AutoMapper vs. POST Manual

| Critério | Com AutoMapper | Sem AutoMapper |
|---------|----------------|----------------|
| Código repetitivo | Baixo | Alto |
| Manutenção | Fácil | Difícil |
| Risco de inconsistência | Reduzido | Elevado |
| Clareza | Alta | Média |

### 3.1.6 Boas Práticas

- Utilizar DTOs de criação para controlar exatamente o que pode ser enviado pelo cliente.
- Validar dados no DTO antes do mapeamento (via *data annotations* ou validação manual).
- Retornar sempre o recurso criado no formato DTO.
- Manter o endpoint enxuto, delegando lógica de transformação ao AutoMapper.
- Utilizar `TypedResults` para respostas claras e tipadas.

### 3.1.7 Conclusão

A implementação de um endpoint POST utilizando AutoMapper e DTOs torna o código mais limpo, seguro e escalável. A separação entre modelos de entrada, entidades e modelos de saída garante controle total sobre os dados trafegados, enquanto o AutoMapper reduz complexidade e aumenta a produtividade no desenvolvimento de Minimal APIs.

## 3.2 Retorno URI POST

### 3.2.1 Introdução
Esta seção apresenta a forma correta de retornar o **URI do recurso criado** em um endpoint POST de Minimal API. Em APIs REST, após criar um recurso, é recomendado retornar:

- **HTTP 201 Created**
- O **Location Header** apontando para o endpoint que recupera o recurso recém‑criado
- O **DTO do recurso** no corpo da resposta

O ASP.NET Core oferece duas abordagens principais para isso:

1. **CreatedAtRoute** — utilizando o nome de uma rota existente  
2. **Created** — utilizando um link absoluto gerado manualmente

### 3.2.2 Estrutura do Endpoint POST com CreatedAtRoute

```csharp
app.MapPost("/rango", async Task<CreatedAtRoute<RangoDTO>>
(
    RangoDbContext rangoDbContext,
    IMapper mapper,
    RangoForCreationDTO rangoForCreation
) =>
{
    var rangosEntity = mapper.Map<Rango>(rangoForCreation);

    rangoDbContext.Add(rangosEntity);
    await rangoDbContext.SaveChangesAsync();

    var rangoToReturn = mapper.Map<RangoDTO>(rangosEntity);

    return TypedResults.CreatedAtRoute(
        rangoToReturn,
        "GetRango",
        new { id = rangoToReturn.Id }
    );
});
```

### 3.2.3 Explicação da Sintaxe

#### 3.2.3.1 `CreatedAtRoute`
O método:

```csharp
TypedResults.CreatedAtRoute(...)
```

gera automaticamente:

- **Status 201 Created**
- **Location Header** com a rota nomeada
- O corpo contendo o DTO criado

A rota referenciada deve ter sido registrada com um nome, por exemplo:

```csharp
app.MapGet("/rango/{id:int}", ...).WithName("GetRango");
```

#### 3.2.3.2 Parâmetros do CreatedAtRoute

```csharp
CreatedAtRoute(
    rangoToReturn,        // Corpo da resposta
    "GetRango",           // Nome da rota
    new { id = rangoToReturn.Id } // Parâmetros da rota
)
```

O ASP.NET Core monta automaticamente o URI final substituindo `{id}` pelo valor correto.

### 3.2.4 Alternativa: Gerando o Link Manualmente

```csharp
var linkToReturn = linkGenerator.GetUriByName(
    httpContext,
    "GetRango",
    new { id = rangoToReturn.Id }
);

return TypedResults.Created(linkToReturn, rangoToReturn);
```

Essa abordagem é útil quando:

- O endpoint POST não está no mesmo módulo que o GET
- É necessário gerar links adicionais
- O link precisa ser manipulado antes de retornar

### 3.2.5 Exemplo Prático de Requisição

#### 3.2.5.1 Corpo da Requisição

```json
{
  "nome": "Rango Especial"
}
```

#### 3.2.5.2 Resposta (HTTP 201 Created)

Headers:

```
Location: https://localhost:5001/rango/7
```

Body:

```json
{
  "id": 7,
  "nome": "Rango Especial"
}
```

### 3.2.6 Comparação: CreatedAtRoute vs Created

| Critério | CreatedAtRoute | Created |
|---------|----------------|---------|
| Usa rota nomeada | Sim | Não |
| Gera URI automaticamente | Sim | Não |
| Recomendado para REST | Sim | Sim |
| Flexibilidade | Média | Alta |

### 3.2.7 Boas Práticas

- Sempre nomear rotas GET que retornam um recurso específico.
- Utilizar `CreatedAtRoute` para manter consistência RESTful.
- Garantir que o DTO retornado não exponha dados sensíveis.
- Validar o DTO de criação antes de mapear para entidade.
- Manter o endpoint POST enxuto, delegando mapeamento ao AutoMapper.

### 3.2.8 Conclusão
O uso de `CreatedAtRoute` em endpoints POST garante conformidade com padrões REST, melhora a experiência do cliente e facilita a navegação entre recursos. A abordagem apresentada integra AutoMapper, DTOs e rotas nomeadas de forma clara e eficiente, resultando em uma API bem estruturada e alinhada às melhores práticas modernas.

## 3.3 Endpoint PUT

### 3.3.1 Introdução
Esta seção descreve a implementação de um endpoint responsável por **atualizar** um recurso existente utilizando o método HTTP **PUT**.  O objetivo é demonstrar como localizar um registro, validar sua existência, aplicar alterações via AutoMapper e persistir as modificações no banco de dados.

A abordagem segue o padrão REST, onde o PUT substitui ou atualiza integralmente o recurso identificado pelo ID.

### 3.3.2 Estrutura do Endpoint PUT

```csharp
app.MapPut("/rango/{id:int}", async Task<Results<NotFound, Ok<RangoDTO>>> (
    RangoDbContext rangoDbContext,
    IMapper mapper,
    RangoForUpdateDTO rangoForUpdate,
    int id
    ) =>
{
    var EntityRango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == id);

    if (EntityRango == null)
        return TypedResults.NotFound();

    mapper.Map(rangoForUpdate, EntityRango);

    await rangoDbContext.SaveChangesAsync();

    var rangoToReturn = mapper.Map<RangoDTO>(EntityRango);

    return TypedResults.Ok(rangoToReturn);
});
```

### 3.3.3 Explicação da Sintaxe

#### 3.3.3.1 Localização do Recurso
A busca é feita por ID:

```csharp
await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == id);
```

Se o recurso não existir, o endpoint retorna:

```csharp
TypedResults.NotFound();
```

#### 3.3.3.2 Aplicação das Alterações
O AutoMapper atualiza a entidade existente com os dados enviados:

```csharp
mapper.Map(rangoForUpdate, EntityRango);
```

Esse padrão evita recriar a entidade e garante que apenas os campos permitidos sejam alterados.

#### 3.3.3.3 Persistência
Após o mapeamento, as alterações são salvas:

```csharp
await rangoDbContext.SaveChangesAsync();
```

#### 3.3.3.4 Retorno Tipado
O endpoint retorna o DTO atualizado:

```csharp
TypedResults.Ok(rangoToReturn);
```

O uso de `Results<NotFound, Ok<RangoDTO>>` deixa explícito que o endpoint pode retornar apenas esses dois tipos de resposta.

### 3.3.4 Exemplos Práticos

#### 3.3.4.1 Corpo da Requisição (JSON)

```json
{
  "nome": "Rango Atualizado"
}
```

#### 3.3.4.2 Resposta (HTTP 200 OK)

```json
{
  "id": 3,
  "nome": "Rango Atualizado"
}
```

#### 3.3.4.3 Caso o ID não exista

Resposta:

```
404 Not Found
```

### 3.3.5 Comparação: PUT vs PATCH

| Critério | PUT | PATCH |
|---------|------|--------|
| Atualização | Completa | Parcial |
| Corpo esperado | DTO completo | Campos parciais |
| Idempotência | Sim | Sim (na maioria dos casos) |
| Uso ideal | Substituir recurso | Alterar poucos campos |

### 3.3.6 Boas Práticas

- Validar a existência do recurso antes de atualizar.
- Utilizar DTOs específicos para atualização (`RangoForUpdateDTO`).
- Evitar atualizar propriedades sensíveis diretamente.
- Manter o endpoint enxuto, delegando mapeamento ao AutoMapper.
- Retornar sempre o recurso atualizado para facilitar o consumo da API.

### 3.3.7 Conclusão
O endpoint PUT apresentado segue boas práticas REST, utilizando AutoMapper para simplificar o processo de atualização e retornando respostas tipadas para maior clareza. A abordagem garante segurança, previsibilidade e organização no fluxo de atualização de recursos.

## 3.4 Endpoint Delete

### 3.4.1 Introdução
Esta seção apresenta a implementação de um endpoint **DELETE** em uma Minimal API. O objetivo é remover um recurso existente do banco de dados de forma segura, validando sua existência antes da exclusão e retornando respostas HTTP adequadas. O padrão segue as práticas REST, onde a remoção de um recurso identificado por ID deve retornar um status que indique sucesso ou falha.

### 3.4.2 Estrutura do Endpoint DELETE

```csharp
app.MapDelete("/rango/{id:int}", async Task<Results<NotFound, Ok>> (
    RangoDbContext rangoDbContext,
    int id
    ) =>
{
    var EntityRango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == id);

    if (EntityRango == null)
        return TypedResults.NotFound();

    rangoDbContext.Rangos.Remove(EntityRango);

    await rangoDbContext.SaveChangesAsync();

    return TypedResults.Ok();
});
```

### 3.4.3 Explicação da Sintaxe

#### 3.4.3.1 Localização do Recurso
A busca é realizada com:

```csharp
FirstOrDefaultAsync(x => x.Id == id)
```

Se o recurso não existir, o endpoint retorna:

```csharp
TypedResults.NotFound();
```

Esse comportamento evita tentativas de exclusão inválidas e mantém a integridade da API.

#### 3.4.3.2 Remoção da Entidade
A exclusão é feita por:

```csharp
rangoDbContext.Rangos.Remove(EntityRango);
```

O EF Core marca a entidade como removida e, ao salvar, executa o comando DELETE no banco.

#### 3.4.3.3 Persistência da Alteração
A operação é concluída com:

```csharp
await rangoDbContext.SaveChangesAsync();
```

Após isso, o recurso deixa de existir no banco.

#### 3.4.3.4 Retorno Tipado
O endpoint retorna:

```csharp
TypedResults.Ok();
```

Esse retorno indica que a operação foi bem-sucedida, mesmo sem corpo na resposta.

### 3.4.4 Exemplos Práticos

#### 3.4.4.1 Requisição DELETE

```
DELETE /rango/3
```

#### 3.4.4.2 Resposta de Sucesso

```
200 OK
```

#### 3.4.4.3 Recurso Inexistente

```
DELETE /rango/999
```

Resposta:

```
404 Not Found
```

### 3.4.5 Comparação: DELETE vs Soft Delete

| Critério | DELETE (remoção real) | Soft Delete |
|---------|------------------------|-------------|
| Persistência | Remove do banco | Marca como inativo |
| Recuperação | Não possível | Possível |
| Simplicidade | Alta | Média |
| Uso ideal | Dados descartáveis | Dados históricos ou críticos |

### 3.4.6 Boas Práticas

- Validar a existência do recurso antes de remover.
- Evitar retornar o objeto excluído; apenas o status é suficiente.
- Utilizar rotas claras e tipadas (`{id:int}`).
- Considerar Soft Delete quando o histórico for importante.
- Manter consistência entre endpoints PUT, POST e DELETE.

### 3.4.7 Conclusão
O endpoint DELETE apresentado segue boas práticas REST, garantindo segurança e clareza no processo de remoção de recursos. A validação prévia, o uso de respostas tipadas e a integração com o EF Core tornam a operação previsível e robusta, mantendo a API organizada e confiável.

## 3.5 Padronização de URLs

### 3.5.1 Introdução
Esta seção apresenta a padronização de URLs em Minimal APIs, garantindo consistência, previsibilidade e alinhamento com boas práticas REST. A padronização facilita a navegação, melhora a documentação automática e reduz ambiguidades entre endpoints. O conjunto de rotas apresentado segue um padrão uniforme baseado no recurso principal **/rangos**, com variações para operações CRUD e sub-recursos.

### 3.5.2 Estrutura Geral das Rotas
O padrão adotado segue convenções REST:

- **Coleção:** `/rangos`
- **Recurso específico:** `/rangos/{rangoId:int}`
- **Sub-recurso:** `/rangos/{rangoId:int}/ingredientes`
- **Operações CRUD:** GET, POST, PUT, DELETE

Esse formato facilita a compreensão da API e mantém coerência entre endpoints.

### 3.5.3 Endpoints Padronizados

#### 3.5.3.1 GET Coleção com Filtro Opcional

```csharp
app.MapGet("/rangos",
    async Task<Results<NoContent, Ok<IEnumerable<RangoDTO>>>>
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        [FromQuery(Name ="name")] string? rangoNome
    ) =>
    {
        var rangosEntity = await rangoDbContext.Rangos
            .Where(x =>
                rangoNome == null ||
                x.Nome.ToLower().Contains(rangoNome.ToLower())
            )
            .ToListAsync();

        if (rangosEntity.Count <= 0 || rangosEntity == null)
            return TypedResults.NoContent();

        return TypedResults.Ok(mapper.Map<IEnumerable<RangoDTO>>(rangosEntity));
    });
```

#### 3.5.3.2 GET Recurso Específico

```csharp
app.MapGet("/rangos/{rangoId:int}", async Task<Results<NotFound, Ok<RangoDTO>>>
(
    RangoDbContext rangoDbContext,
    IMapper mapper,
    int rangoId
) =>
{
    var EntityRango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == rangoId);

    return EntityRango is null
        ? TypedResults.NotFound()
        : TypedResults.Ok(mapper.Map<RangoDTO>(EntityRango));

}).WithName("GetRangos");
```

#### 3.5.3.3 GET Sub-recurso (Ingredientes de um Rango)

```csharp
app.MapGet("/rangos/{rangoId:int}/ingredientes",
    async Task<Results<NoContent, Ok<IEnumerable<IngredienteDTO>>>>
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        int rangoId
    ) =>
{
    var EntityIngredientes = await rangoDbContext.Rangos
        .Include(r => r.Ingredientes)
        .FirstOrDefaultAsync(r => r.Id == rangoId);

    return EntityIngredientes is null
        ? TypedResults.NoContent()
        : TypedResults.Ok(mapper.Map<IEnumerable<IngredienteDTO>>(EntityIngredientes.Ingredientes));
});
```

#### 3.5.3.4 POST Criar Novo Rango

```csharp
app.MapPost("/rangos", async Task<CreatedAtRoute<RangoDTO>>
(
    RangoDbContext rangoDbContext,
    IMapper mapper,
    RangoForCreationDTO rangoForCreation
) =>
{
    var rangosEntity = mapper.Map<Rango>(rangoForCreation);

    rangoDbContext.Add(rangosEntity);
    await rangoDbContext.SaveChangesAsync();

    var rangoToReturn = mapper.Map<RangoDTO>(rangosEntity);

    return TypedResults.CreatedAtRoute(
        rangoToReturn,
        "GetRangos",
        new { rangoId = rangoToReturn.Id }
    );
});
```

#### 3.5.3.5 PUT Atualizar Rango

```csharp
app.MapPut("/rangos/{rangoId:int}", async Task<Results<NotFound, Ok<RangoDTO>>>
(
    RangoDbContext rangoDbContext,
    IMapper mapper,
    RangoForUpdateDTO rangoForUpdate,
    int rangoId
) =>
{
    var EntityRango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == rangoId);

    if (EntityRango == null)
        return TypedResults.NotFound();

    mapper.Map(rangoForUpdate, EntityRango);

    await rangoDbContext.SaveChangesAsync();

    var rangoToReturn = mapper.Map<RangoDTO>(EntityRango);

    return TypedResults.Ok(rangoToReturn);
});
```

#### 3.5.3.6 DELETE Remover Rango

```csharp
app.MapDelete("/rangos/{rangoId:int}", async Task<Results<NotFound, Ok>>
(
    RangoDbContext rangoDbContext,
    int rangoId
) =>
{
    var EntityRango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == rangoId);

    if (EntityRango == null)
        return TypedResults.NotFound();

    rangoDbContext.Rangos.Remove(EntityRango);

    await rangoDbContext.SaveChangesAsync();

    return TypedResults.Ok();
});
```

### 3.5.4 Benefícios da Padronização

- **Consistência:** URLs seguem um padrão uniforme, facilitando o uso da API.
- **Previsibilidade:** Clientes conseguem inferir rotas sem consultar documentação.
- **Escalabilidade:** Novos endpoints podem ser adicionados mantendo o mesmo padrão.
- **Compatibilidade REST:** Estrutura clara de recursos e sub-recursos.
- **Integração com ferramentas:** Swagger e Postman interpretam melhor rotas padronizadas.

### 3.5.5 Boas Práticas

- Utilizar sempre substantivos no plural para coleções (`/rangos`).
- Manter o ID como parte da rota para recursos específicos.
- Utilizar sub-recursos apenas quando fizer sentido hierárquico.
- Nomear rotas GET individuais para uso com `CreatedAtRoute`.
- Evitar misturar padrões diferentes dentro da mesma API.

### 3.5.6 Conclusão
A padronização de URLs é essencial para manter uma API clara, intuitiva e alinhada às práticas REST. O conjunto de endpoints apresentado demonstra uma estrutura consistente, facilitando manutenção, documentação e evolução da aplicação. Essa abordagem melhora a experiência de desenvolvedores e consumidores da API, garantindo previsibilidade e organização.

## 3.6 Groupping Endpoints

### 3.6.1 Introdução
Esta seção apresenta a organização de endpoints por meio de **grupos de rotas** (*route groups*) em Minimal APIs. O objetivo é estruturar a API de forma modular, clara e escalável, agrupando endpoints relacionados sob um mesmo prefixo. Essa abordagem reduz repetição de código, melhora a legibilidade e facilita a aplicação de configurações compartilhadas, como versionamento, autorização e filtros.

A estrutura utilizada cria três níveis de agrupamento:

- `/rangos` — grupo principal do recurso.
- `/rangos/{rangoId:int}` — grupo de operações sobre um recurso específico.
- `/rangos/{rangoId:int}/ingredientes` — sub-recurso relacionado.

### 3.6.2 Estrutura dos Grupos de Endpoints

```csharp
var rangosEndpoints = app.MapGroup("/rangos");
var rangosComIDEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");
var ingredientesEndpoints = rangosComIDEndpoints.MapGroup("/ingredientes");
```

Essa estrutura cria uma hierarquia clara e evita repetição de prefixos em cada endpoint.

### 3.6.3 Endpoints do Grupo Principal `/rangos`

#### 3.6.3.1 GET Coleção com Filtro Opcional

```csharp
rangosEndpoints.MapGet("",
    async Task<Results<NoContent, Ok<IEnumerable<RangoDTO>>>>
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        [FromQuery(Name ="name")] string? rangoNome
    ) =>
    {
        var rangosEntity = await rangoDbContext.Rangos
            .Where(x =>
                rangoNome == null ||
                x.Nome.ToLower().Contains(rangoNome.ToLower())
            )
            .ToListAsync();

        if (rangosEntity.Count <= 0 || rangosEntity == null)
            return TypedResults.NoContent();

        return TypedResults.Ok(mapper.Map<IEnumerable<RangoDTO>>(rangosEntity));
    });
```

#### 3.6.3.2 POST Criar Novo Rango

```csharp
rangosEndpoints.MapPost("",
    async Task<CreatedAtRoute<RangoDTO>>
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        RangoForCreationDTO rangoForCreation
    ) =>
    {
        var rangosEntity = mapper.Map<Rango>(rangoForCreation);

        rangoDbContext.Add(rangosEntity);
        await rangoDbContext.SaveChangesAsync();

        var rangoToReturn = mapper.Map<RangoDTO>(rangosEntity);

        return TypedResults.CreatedAtRoute(
            rangoToReturn,
            "GetRangos",
            new { rangoId = rangoToReturn.Id }
        );
    });
```

### 3.6.4 Endpoints do Grupo `/rangos/{rangoId:int}`

#### 3.6.4.1 GET Recurso Específico

```csharp
rangosComIDEndpoints.MapGet("",
    async Task<Results<NotFound, Ok<RangoDTO>>>
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        int rangoId
    ) =>
    {
        var EntityRango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == rangoId);

        return EntityRango is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(mapper.Map<RangoDTO>(EntityRango));
    }).WithName("GetRangos");
```

#### 3.6.4.2 PUT Atualizar Rango

```csharp
rangosComIDEndpoints.MapPut("",
    async Task<Results<NotFound, Ok<RangoDTO>>>
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        RangoForUpdateDTO rangoForUpdate,
        int rangoId
    ) =>
    {
        var EntityRango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == rangoId);

        if (EntityRango == null)
            return TypedResults.NotFound();

        mapper.Map(rangoForUpdate, EntityRango);

        await rangoDbContext.SaveChangesAsync();

        var rangoToReturn = mapper.Map<RangoDTO>(EntityRango);

        return TypedResults.Ok(rangoToReturn);
    });
```

#### 3.6.4.3 DELETE Remover Rango

```csharp
rangosComIDEndpoints.MapDelete("",
    async Task<Results<NotFound, Ok>>
    (
        RangoDbContext rangoDbContext,
        int rangoId
    ) =>
    {
        var EntityRango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == rangoId);

        if (EntityRango == null)
            return TypedResults.NotFound();

        rangoDbContext.Rangos.Remove(EntityRango);

        await rangoDbContext.SaveChangesAsync();

        return TypedResults.Ok();
    });
```

### 3.6.5 Endpoints do Subgrupo `/rangos/{rangoId:int}/ingredientes`

#### 3.6.5.1 GET Ingredientes do Rango

```csharp
ingredientesEndpoints.MapGet("",
    async Task<Results<NoContent, Ok<IEnumerable<IngredienteDTO>>>>
    (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        int rangoId
    ) =>
    {
        var EntityIngredientes = await rangoDbContext.Rangos
            .Include(r => r.Ingredientes)
            .FirstOrDefaultAsync(r => r.Id == rangoId);

        return EntityIngredientes is null
            ? TypedResults.NoContent()
            : TypedResults.Ok(mapper.Map<IEnumerable<IngredienteDTO>>(EntityIngredientes.Ingredientes));
    });
```

### 3.6.6 Benefícios do Agrupamento de Endpoints

- Redução de repetição de prefixos.
- Organização hierárquica clara.
- Facilita aplicação de políticas compartilhadas (ex.: autenticação).
- Melhora a legibilidade e manutenção.
- Permite versionamento modular da API.
- Mantém a estrutura REST de forma natural.

### 3.6.7 Boas Práticas

- Criar grupos para cada recurso principal.
- Utilizar subgrupos para relacionamentos hierárquicos.
- Nomear rotas GET individuais para uso com `CreatedAtRoute`.
- Evitar misturar rotas fora da estrutura agrupada.
- Manter consistência entre nomes e padrões de URL.

### 3.6.8 Conclusão
O agrupamento de endpoints é uma técnica essencial para manter Minimal APIs organizadas, escaláveis e fáceis de navegar. A estrutura apresentada cria uma hierarquia clara de recursos e sub-recursos, reduz duplicação e melhora a manutenção da API como um todo. Essa abordagem é especialmente útil em APIs maiores, onde a modularidade se torna fundamental.

# 4 Estrutura da Minimal API

## 4.1 Endpoint Handlers

### 4.1.1 Introdução
À medida que a Minimal API cresce, manter toda a lógica diretamente no `Program.cs` torna-se difícil de organizar, testar e evoluir. Para resolver isso, introduzimos **Endpoint Handlers**, classes estáticas que encapsulam a lógica de cada endpoint. Essa abordagem melhora a legibilidade, separa responsabilidades e deixa o arquivo principal responsável apenas pelo mapeamento das rotas.

---

### 4.1.2 Estrutura Geral dos Handlers
Os handlers são organizados em classes estáticas dentro de um namespace dedicado, como:

```
RangoAgil.API.EndpointHandlers
```

Cada classe representa um conjunto de operações relacionadas a um recurso específico:

- `RangosHandlers` — operações sobre Rangos  
- `IngredientesHandlers` — operações sobre Ingredientes  

Essa separação segue o padrão REST já estabelecido na API.

---

### 4.1.3 Handler de Ingredientes

```csharp
public static class IngredientesHandlers
{
    public static async Task<Results<NoContent, Ok<IEnumerable<IngredienteDTO>>>> 
        GetIngredientesAsyn(
            RangoDbContext rangoDbContext,
            IMapper mapper,
            int rangoId)
    {
        var EntityIngredientes = await rangoDbContext.Rangos
            .Include(r => r.Ingredientes)
            .FirstOrDefaultAsync(r => r.Id == rangoId);

        return EntityIngredientes is null
            ? TypedResults.NoContent()
            : TypedResults.Ok(
                mapper.Map<IEnumerable<IngredienteDTO>>(EntityIngredientes.Ingredientes)
            );
    }
}
```

#### 4.1.3.1 Explicação
- O método recebe apenas o necessário: `DbContext`, `IMapper` e o ID.
- A consulta inclui os ingredientes via `Include`.
- O retorno é tipado (`NoContent` ou `Ok`).
- O mapeamento ocorre apenas no retorno.

---

### 4.1.4 Handlers de Rangos

#### 4.1.4.1 GET – Listar Rangos com Filtro Opcional

```csharp
public static async Task<Results<NoContent, Ok<IEnumerable<RangoDTO>>>> 
    GetRangosAsync(
        RangoDbContext rangoDbContext,
        IMapper mapper,
        [FromQuery(Name ="name")] string? rangoNome)
{
    var rangosEntity = await rangoDbContext.Rangos
        .Where(x =>
            rangoNome == null ||
            x.Nome.ToLower().Contains(rangoNome.ToLower()))
        .ToListAsync();

    if (rangosEntity.Count <= 0 || rangosEntity == null)
        return TypedResults.NoContent();

    return TypedResults.Ok(mapper.Map<IEnumerable<RangoDTO>>(rangosEntity));
}
```

#### 4.1.4.2 GET – Buscar Rango por ID

```csharp
public static async Task<Results<NotFound, Ok<RangoDTO>>> 
    GetRangoByIdAsync(
        RangoDbContext rangoDbContext,
        IMapper mapper,
        int rangoId)
{
    var EntityRango = await rangoDbContext.Rangos
        .FirstOrDefaultAsync(x => x.Id == rangoId);

    return EntityRango is null
        ? TypedResults.NotFound()
        : TypedResults.Ok(mapper.Map<RangoDTO>(EntityRango));
}
```

#### 4.1.4.3 POST – Criar Novo Rango

```csharp
public static async Task<CreatedAtRoute<RangoDTO>> 
    RangoPostAsync(
        RangoDbContext rangoDbContext,
        IMapper mapper,
        RangoForCreationDTO rangoForCreation)
{
    var rangosEntity = mapper.Map<Rango>(rangoForCreation);

    rangoDbContext.Add(rangosEntity);
    await rangoDbContext.SaveChangesAsync();

    var rangoToReturn = mapper.Map<RangoDTO>(rangosEntity);

    return TypedResults.CreatedAtRoute(
        rangoToReturn,
        "GetRangos",
        new { rangoId = rangoToReturn.Id }
    );
}
```

#### 4.1.4.4 PUT – Atualizar Rango

```csharp
public static async Task<Results<NotFound, Ok<RangoDTO>>> 
    RangoPutAsync(
        RangoDbContext rangoDbContext,
        IMapper mapper,
        RangoForUpdateDTO rangoForUpdate,
        int rangoId)
{
    var EntityRango = await rangoDbContext.Rangos
        .FirstOrDefaultAsync(x => x.Id == rangoId);

    if (EntityRango == null)
        return TypedResults.NotFound();

    mapper.Map(rangoForUpdate, EntityRango);

    await rangoDbContext.SaveChangesAsync();

    var rangoToReturn = mapper.Map<RangoDTO>(EntityRango);

    return TypedResults.Ok(rangoToReturn);
}
```

#### 4.1.4.5 DELETE – Remover Rango

```csharp
public static async Task<Results<NotFound, Ok>> 
    RangoDeleteAsync(
        RangoDbContext rangoDbContext,
        int rangoId)
{
    var EntityRango = await rangoDbContext.Rangos
        .FirstOrDefaultAsync(x => x.Id == rangoId);

    if (EntityRango == null)
        return TypedResults.NotFound();

    rangoDbContext.Rangos.Remove(EntityRango);

    await rangoDbContext.SaveChangesAsync();

    return TypedResults.Ok();
}
```

---

### 4.1.5 Comparação entre Lógica Inline e Handlers

| Critério | Inline no Program.cs | Endpoint Handlers |
|---------|------------------------|--------------------|
| Organização | Baixa | Alta |
| Testabilidade | Difícil | Fácil |
| Reutilização | Limitada | Alta |
| Escalabilidade | Ruim | Excelente |
| Separação de responsabilidades | Fraca | Clara |

---

### 4.1.6 Boas Práticas
- Criar um handler por recurso.
- Manter métodos pequenos e focados.
- Utilizar retornos tipados (`Results<T1, T2>`).
- Evitar lógica de negócio dentro do `Program.cs`.
- Centralizar mapeamentos no AutoMapper.

---

### 4.1.7 Conclusão
A adoção de Endpoint Handlers transforma a Minimal API em uma arquitetura mais limpa, modular e sustentável. A lógica de cada operação fica isolada, facilitando testes, manutenção e evolução da aplicação. Essa abordagem se integra perfeitamente com o agrupamento de endpoints e com o uso de DTOs e AutoMapper, consolidando uma API profissional e bem estruturada.

---
## 4.2 Métodos de Extensão

### 4.2.1 Introdução
Com o crescimento da Minimal API, mesmo utilizando **Endpoint Handlers** e **grupos de rotas**, o arquivo `Program.cs` ainda pode acumular muitos mapeamentos. Para manter a organização e a escalabilidade, introduzimos **métodos de extensão** para registrar endpoints.

Essa abordagem permite que cada conjunto de endpoints seja encapsulado em métodos reutilizáveis, deixando o `Program.cs` limpo e focado apenas na configuração da aplicação.

---

### 4.2.2 Estrutura dos Métodos de Extensão
Os métodos de extensão são definidos em uma classe estática dentro de um namespace específico, como:

```
RangoAgil.API.Extensions
```

Cada método agrupa o registro de endpoints relacionados a um recurso, como:

- `RegisterRangosEndpoints`
- `RegisterIngredientesEndpoints`

Isso mantém a API modular e facilita a manutenção.

---

### 4.2.3 Implementação dos Métodos de Extensão

#### 4.2.3.1 Registro dos Endpoints de Rangos

```csharp
public static class EndpointRouteBuilderExtensions
{
    public static void RegisterRangosEndpoints(this IEndpointRouteBuilder app)
    {
        var rangosEndpoints = app.MapGroup("/rangos");
        var rangosComIDEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");

        rangosEndpoints.MapGet("", RangosHandlers.GetRangosAsync);

        rangosComIDEndpoints.MapGet("", RangosHandlers.GetRangoByIdAsync)
                            .WithName("GetRangos");

        rangosEndpoints.MapPost("", RangosHandlers.RangoPostAsync);

        rangosComIDEndpoints.MapPut("", RangosHandlers.RangoPutAsync);

        rangosComIDEndpoints.MapDelete("", RangosHandlers.RangoDeleteAsync);
    }
```

#### 4.2.3.2 Registro dos Endpoints de Ingredientes

```csharp
    public static void RegisterIngredientesEndpoints(this IEndpointRouteBuilder app)
    {
        var ingredientesEndpoints = app.MapGroup("rangos/{rangoId:int}/ingredientes");

        ingredientesEndpoints.MapGet("", IngredientesHandlers.GetIngredientesAsyn);
    }
}
```

#### 4.2.3.3 Explicação
- Cada método de extensão recebe `this IEndpointRouteBuilder app`, permitindo ser chamado diretamente sobre `app`.
- Os grupos de rotas são criados dentro do método, mantendo o padrão REST.
- Os handlers são referenciados diretamente, eliminando lógica duplicada.
- A rota nomeada `"GetRangos"` continua funcionando para `CreatedAtRoute`.

---

### 4.2.4 Uso dos Métodos de Extensão no Program.cs

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RangoDbContext>(
    o => o.UseSqlite(builder.Configuration["ConnectionStrings:RangoDBConStr"])
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RegisterRangosEndpoints();
app.RegisterIngredientesEndpoints();

app.Run();
```

#### 4.2.4.1 Explicação
- O `Program.cs` agora contém apenas chamadas de registro.
- A API fica mais limpa, modular e fácil de navegar.
- A ordem de registro é clara e explícita.

---

### 4.2.5 Comparação: Program.cs sem Extensões vs. com Extensões

| Critério | Sem Extensões | Com Extensões |
|---------|----------------|----------------|
| Organização | Baixa | Alta |
| Tamanho do Program.cs | Grande | Pequeno |
| Reutilização | Limitada | Alta |
| Testabilidade | Média | Alta |
| Escalabilidade | Difícil | Excelente |

---

### 4.2.6 Boas Práticas
- Criar um método de extensão por recurso principal.
- Manter nomes claros e consistentes (`RegisterXEndpoints`).
- Evitar lógica dentro dos métodos; apenas mapeamento.
- Utilizar grupos de rotas dentro dos métodos para manter o padrão REST.
- Registrar os métodos no `Program.cs` em ordem lógica.

---

### 4.2.7 Conclusão
Os métodos de extensão elevam a organização da Minimal API a um novo nível. Eles permitem que o `Program.cs` permaneça limpo e focado, enquanto toda a estrutura de rotas fica encapsulada em módulos reutilizáveis. Combinados com Endpoint Handlers e grupos de rotas, formam uma arquitetura robusta, escalável e profissional.

---

# 5 Manipulaçã de Exceçõoes de Logs

## 5.1 Tratando Exceção no Middleware: Minimal API

### 5.1.1 Introdução
O tratamento centralizado de exceções em aplicações Minimal API permite capturar falhas inesperadas e retornar respostas consistentes ao cliente. Em ambientes de produção, é essencial evitar a exposição de detalhes internos da aplicação, fornecendo mensagens controladas e códigos de status adequados.

### 5.1.2 Objetivo do Middleware de Exceção
O middleware de exceção tem como finalidade:
- Interceptar erros não tratados.
- Registrar falhas para diagnóstico.
- Retornar respostas padronizadas.
- Evitar vazamento de informações sensíveis.

### 5.1.3 Estrutura do Middleware no Ambiente de Produção
A configuração condicional baseada no ambiente garante que mensagens detalhadas só sejam exibidas em desenvolvimento. Em produção, a aplicação deve retornar apenas informações genéricas.

```csharp
if (app.Environment.IsProduction())
{
    app.UseExceptionHandler(configureApplicationBuilder =>
    {
        configureApplicationBuilder.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync("An unexpected problem happened.");
        });
    });
}
```

### 5.1.4 Explicação da Sintaxe
- `app.Environment.IsProduction()`  
  Verifica se o ambiente atual é Produção.
- `app.UseExceptionHandler(...)`  
  Registra um middleware que captura exceções não tratadas.
- `configureApplicationBuilder.Run(...)`  
  Define o pipeline final para lidar com a exceção capturada.
- `context.Response.StatusCode`  
  Define o código HTTP retornado ao cliente.
- `context.Response.ContentType`  
  Define o tipo de conteúdo da resposta.
- `context.Response.WriteAsync(...)`  
  Envia a mensagem ao cliente.

### 5.1.5 Exemplo Prático com Registro de Logs
Um cenário comum envolve registrar a exceção antes de retornar a resposta.

```csharp
if (app.Environment.IsProduction())
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            var logger = context.RequestServices.GetRequiredService<ILoggerFactory>()
                                               .CreateLogger("GlobalException");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            logger.LogError("Erro inesperado no processamento da requisição.");

            await context.Response.WriteAsync("{\"message\": \"Erro interno no servidor.\"}");
        });
    });
}
```

### 5.1.6 Comparação: Desenvolvimento vs Produção

| Ambiente | Comportamento | Exposição de Detalhes | Objetivo |
|---------|----------------|------------------------|----------|
| Desenvolvimento | Exibe página detalhada de erro | Alta | Facilitar depuração |
| Produção | Retorna mensagem genérica | Baixa | Segurança e estabilidade |

### 5.1.7 Boas Práticas
- Registrar exceções com informações suficientes para diagnóstico.
- Nunca retornar stack trace ao cliente em produção.
- Utilizar formatos consistentes de resposta (JSON, XML, texto).
- Integrar com ferramentas de observabilidade (Application Insights, Elastic Stack).
- Criar middlewares personalizados quando necessário.

### 5.1.8 Exemplo de Middleware Personalizado
Um middleware customizado pode padronizar respostas e integrar logs de forma mais completa.

```csharp
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync("{\"message\": \"Erro interno no servidor.\"}");
        }
    }
}
```

Registro no pipeline:

```csharp
app.UseMiddleware<GlobalExceptionMiddleware>();
```

### 5.1.9 Conclusão
O tratamento de exceções em Minimal API é fundamental para garantir robustez, segurança e previsibilidade. A configuração adequada do middleware permite respostas consistentes e evita exposição de detalhes sensíveis, especialmente em produção.

---

## 5.2 Add Problem Details em Minimal API

### 5.2.1 Introdução
O uso de *Problem Details* em Minimal API padroniza respostas de erro conforme a RFC 7807, permitindo que clientes recebam informações estruturadas sobre falhas ocorridas durante o processamento da requisição. Essa abordagem melhora a interoperabilidade, facilita diagnósticos e mantém consistência entre diferentes endpoints.

### 5.2.2 Registro do Serviço de Problem Details
A configuração inicia com o registro do serviço responsável por gerar automaticamente objetos `ProblemDetails` quando exceções ou erros de validação ocorrem.

```csharp
builder.Services.AddProblemDetails();
var app = builder.Build();
```

Esse registro habilita o pipeline para produzir respostas no formato JSON contendo:
- `type`
- `title`
- `status`
- `detail`
- `instance`

### 5.2.3 Integração com o Middleware de Exceção
Em ambientes de produção, o middleware de exceção pode ser simplificado quando `AddProblemDetails` está habilitado. O método `UseExceptionHandler()` passa a gerar automaticamente uma resposta estruturada.

```csharp
if (app.Environment.IsProduction())
{
    app.UseExceptionHandler();
}
```

Essa configuração substitui a necessidade de um manipulador manual, como no exemplo comentado:

```csharp
// app.UseExceptionHandler(configureApplicatioonBuilder =>
// {
//     configureApplicatioonBuilder.Run(async context =>
//     {
//         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//         context.Response.ContentType = "text/html";
//         await context.Response.WriteAsync("An unexpected problem happened.");
//     });
// });
```

### 5.2.4 Explicação da Sintaxe
- `builder.Services.AddProblemDetails()`  
  Registra o serviço responsável por gerar respostas no padrão RFC 7807.
- `app.UseExceptionHandler()`  
  Ativa o middleware que captura exceções e delega ao serviço de Problem Details a criação da resposta.
- `app.Environment.IsProduction()`  
  Garante que o tratamento genérico seja aplicado apenas em produção.

### 5.2.5 Exemplo Prático de Erro Gerado Automaticamente
Quando ocorre uma exceção não tratada, a resposta gerada segue o padrão:

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.6.1",
  "title": "An error occurred while processing your request.",
  "status": 500,
  "traceId": "00-abc123..."
}
```

Essa resposta é produzida sem necessidade de código adicional no middleware.

### 5.2.6 Comparação: Middleware Manual vs Problem Details

| Abordagem | Vantagens | Desvantagens |
|----------|-----------|--------------|
| Middleware manual | Total controle sobre a resposta | Maior código, risco de inconsistência |
| Problem Details | Padronização, menor código, compatível com RFC 7807 | Menor personalização sem extensões adicionais |

### 5.2.7 Boas Práticas
- Utilizar Problem Details como padrão para erros em APIs REST.
- Evitar mensagens genéricas quando for seguro fornecer detalhes adicionais.
- Manter consistência entre erros de validação e erros de exceção.
- Integrar logs estruturados para complementar o diagnóstico.

### 5.2.8 Exemplo Avançado com Personalização
É possível personalizar o comportamento de Problem Details:

```csharp
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["requestId"] = context.HttpContext.TraceIdentifier;
        context.ProblemDetails.Extensions["timestamp"] = DateTime.UtcNow;
    };
});
```

Essa abordagem adiciona metadados úteis para rastreamento e auditoria.

### 5.2.9 Conclusão
A integração de `AddProblemDetails` com `UseExceptionHandler` simplifica o tratamento de erros em Minimal API, reduz código repetitivo e garante respostas padronizadas. Essa prática melhora a experiência do cliente e facilita a manutenção da aplicação.

---
## 5.3 Logs de Aplicação em Minimal API

### 5.3.1 Introdução
O uso de logs em Minimal API é fundamental para monitorar o comportamento da aplicação, diagnosticar falhas, registrar eventos relevantes e acompanhar o fluxo de execução. A injeção de `ILogger<T>` permite registrar informações estruturadas e integradas ao provedor de logs configurado (Console, Debug, Application Insights, Elastic Stack, entre outros).

### 5.3.2 Objetivo do Log no Endpoint
O método apresentado utiliza logs para:
- Registrar buscas realizadas no banco de dados.
- Informar quando nenhum resultado é encontrado.
- Registrar o retorno bem-sucedido da operação.
- Facilitar auditoria e rastreamento de requisições.

### 5.3.3 Código do Endpoint com Logging

```csharp
public static async Task<Results<NoContent, Ok<IEnumerable<RangoDTO>>>> GetRangosAsync
(
    RangoDbContext rangoDbContext,
    IMapper mapper,
    ILogger<RangoDTO> logger,
    [FromQuery(Name = "name")] string? rangoNome
)
{
    var rangosEntity = await rangoDbContext.Rangos
                                .Where(x =>
                                    rangoNome == null ||
                                    x.Nome.ToLower().Contains(rangoNome.ToLower())
                                )
                                .ToListAsync();

    if (rangosEntity.Count <= 0 || rangosEntity == null)
    {
        logger.LogInformation($"Rango não encontrado. Parâmetro: {rangoNome}");
        return TypedResults.NoContent();
    }

    logger.LogInformation("Retornando o Rango encontrado");
    return TypedResults.Ok(mapper.Map<IEnumerable<RangoDTO>>(rangosEntity));
}
```

### 5.3.4 Explicação da Sintaxe
- `ILogger<RangoDTO>`  
  Injeta um logger tipado, permitindo categorizar logs por classe.
- `logger.LogInformation(...)`  
  Registra mensagens informativas no pipeline de logs.
- `Results<NoContent, Ok<IEnumerable<RangoDTO>>>`  
  Define respostas possíveis do endpoint, permitindo retorno tipado.
- `rangoNome == null || x.Nome.ToLower().Contains(...)`  
  Realiza filtro condicional baseado no parâmetro de consulta.
- `TypedResults.NoContent()`  
  Retorna HTTP 204 quando nenhum registro é encontrado.

### 5.3.5 Boas Práticas de Logging
- Registrar apenas informações relevantes ao diagnóstico.
- Evitar logs excessivos em loops ou operações de alta frequência.
- Utilizar níveis adequados: `Information`, `Warning`, `Error`, `Critical`.
- Incluir parâmetros relevantes para rastreamento.
- Integrar logs com correlação de requisições (`TraceIdentifier`).

### 5.3.6 Exemplo Prático com Níveis de Log
A seguir, um exemplo com logs mais detalhados:

```csharp
public static async Task<Results<NoContent, Ok<IEnumerable<RangoDTO>>>> GetRangosAsync
(
    RangoDbContext rangoDbContext,
    IMapper mapper,
    ILogger<RangoDTO> logger,
    [FromQuery(Name = "name")] string? rangoNome
)
{
    logger.LogInformation("Iniciando busca de rangos. Parâmetro recebido: {nome}", rangoNome);

    var rangosEntity = await rangoDbContext.Rangos
                                .Where(x =>
                                    rangoNome == null ||
                                    x.Nome.ToLower().Contains(rangoNome.ToLower())
                                )
                                .ToListAsync();

    if (rangosEntity == null || rangosEntity.Count == 0)
    {
        logger.LogWarning("Nenhum rango encontrado para o parâmetro: {nome}", rangoNome);
        return TypedResults.NoContent();
    }

    logger.LogInformation("Quantidade de rangos encontrados: {quantidade}", rangosEntity.Count);

    var resultado = mapper.Map<IEnumerable<RangoDTO>>(rangosEntity);
    logger.LogInformation("Retornando resultado mapeado para DTO");

    return TypedResults.Ok(resultado);
}
```

### 5.3.7 Comparação: Log Simples vs Log Estruturado

| Tipo de Log | Características | Vantagens |
|-------------|-----------------|-----------|
| Texto simples | Mensagens fixas | Fácil leitura, porém pouco estruturado |
| Log estruturado | Uso de placeholders `{valor}` | Melhor análise em ferramentas de observabilidade |

### 5.3.8 Integração com Observabilidade
A utilização de logs estruturados facilita integração com:
- Application Insights  
- Elastic Stack (ELK)  
- Seq  
- Grafana Loki  

Essas ferramentas permitem consultas avançadas, dashboards e alertas.

### 5.3.9 Conclusão
O uso adequado de logs em Minimal API melhora a rastreabilidade, facilita a manutenção e permite identificar problemas rapidamente. A combinação de logs estruturados, níveis adequados e integração com ferramentas de observabilidade fortalece a confiabilidade da aplicação.

---

# 6 Endpoint Filter

## 6.1 Criando Endpoint Filter em Minimal API

### 6.1.1 Introdução
Endpoint Filters permitem interceptar a execução de um endpoint antes e depois do processamento principal, adicionando lógica de validação, auditoria, autorização ou transformação de dados. Essa funcionalidade amplia o pipeline de Minimal API sem a necessidade de criar middlewares globais, permitindo regras específicas por endpoint.

### 6.1.2 Objetivo do Endpoint Filter
O filtro apresentado tem como finalidade impedir a atualização de um recurso específico quando o identificador recebido corresponde a um valor pré-definido. Esse tipo de regra é útil para cenários como:
- Proteção de registros imutáveis.
- Validação de parâmetros antes da execução do handler.
- Retorno antecipado de erros padronizados.
- Aplicação de regras de negócio específicas.

### 6.1.3 Implementação do Endpoint Filter

```csharp
rangosComIDEndpoints.MapPut("", RangosHandlers.RangoPutAsync)
    .AddEndpointFilter(
        async (context, next) =>
        {
            var rangoId = context.GetArgument<int>(3);
            var tropeiro_id = 8;

            if (rangoId == tropeiro_id)
            {
                return TypedResults.Problem(new()
                {
                    Status = 400,
                    Title = "Rango já é Perfeito, você não precisa modificar nada aqui.",
                    Detail = "Você não pode modificar essa Receita"
                });
            }

            var result = await next.Invoke(context);
            return result;
        }
    );
```

### 6.1.4 Explicação da Sintaxe
- `AddEndpointFilter(...)`  
  Adiciona um filtro específico ao endpoint, executado antes do handler.
- `context.GetArgument<int>(3)`  
  Obtém o argumento do endpoint pela posição. No exemplo, o ID do recurso está na posição 3.
- `next.Invoke(context)`  
  Executa o próximo elemento do pipeline, normalmente o handler do endpoint.
- `TypedResults.Problem(...)`  
  Retorna uma resposta estruturada no padrão Problem Details (RFC 7807).
- `Status = 400`  
  Define o código HTTP indicando erro de requisição.

### 6.1.5 Exemplo Prático com Validação Adicional
Um filtro pode validar múltiplos parâmetros antes de permitir a execução do handler.

```csharp
.AddEndpointFilter(async (context, next) =>
{
    var id = context.GetArgument<int>(3);
    var dto = context.GetArgument<RangoDTO>(4);

    if (id <= 0)
    {
        return TypedResults.Problem(new()
        {
            Status = 400,
            Title = "ID inválido",
            Detail = "O identificador deve ser maior que zero."
        });
    }

    if (string.IsNullOrWhiteSpace(dto.Nome))
    {
        return TypedResults.Problem(new()
        {
            Status = 400,
            Title = "Nome inválido",
            Detail = "O nome do Rango é obrigatório."
        });
    }

    return await next(context);
});
```

### 6.1.6 Comparação: Endpoint Filter vs Middleware

| Característica | Endpoint Filter | Middleware |
|----------------|-----------------|------------|
| Escopo | Por endpoint | Global |
| Acesso a argumentos | Sim, via `context.GetArgument` | Não |
| Uso ideal | Validações específicas | Regras gerais |
| Ordem de execução | Antes do handler | Antes de todos os endpoints |

### 6.1.7 Boas Práticas
- Utilizar filtros para regras específicas de endpoints, evitando poluir middlewares globais.
- Evitar lógica complexa dentro do filtro; delegar ao domínio quando necessário.
- Retornar Problem Details para manter consistência nas respostas de erro.
- Documentar a posição dos argumentos quando usados com `GetArgument`.

### 6.1.8 Exemplo Avançado com Logs e Métricas
Um filtro pode registrar logs e medir o tempo de execução do handler.

```csharp
.AddEndpointFilter(async (context, next) =>
{
    var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>()
                                                   .CreateLogger("RangoFilter");

    var inicio = DateTime.UtcNow;
    logger.LogInformation("Iniciando execução do endpoint.");

    var resultado = await next(context);

    var duracao = DateTime.UtcNow - inicio;
    logger.LogInformation($"Execução concluída em {duracao.TotalMilliseconds} ms.");

    return resultado;
});
```

### 6.1.9 Conclusão
Endpoint Filters ampliam a flexibilidade das Minimal APIs, permitindo validações e regras de negócio específicas por endpoint. A abordagem melhora a organização do código, reduz duplicação e mantém o pipeline mais limpo e modular.

---
## 6.2 Reutilizando Endpoint Filter em Minimal API

### 6.2.1 Introdução
A reutilização de Endpoint Filters permite aplicar regras de negócio de forma consistente em múltiplos endpoints, evitando duplicação de código e mantendo o pipeline mais organizado. A implementação de filtros parametrizáveis amplia ainda mais essa flexibilidade, permitindo que um mesmo filtro seja configurado com valores diferentes conforme a necessidade de cada rota.

### 6.2.2 Objetivo do Filtro Parametrizável
O filtro `RangosIsLockedFilter` tem como finalidade impedir alterações ou exclusões de registros específicos, definidos por um identificador imutável. A parametrização via construtor permite reutilizar o mesmo filtro para diferentes IDs bloqueados, mantendo a lógica centralizada e configurável.

Esse padrão é útil para:
- Proteger múltiplos registros especiais.
- Aplicar regras de negócio específicas por endpoint.
- Evitar repetição de validações nos handlers.
- Facilitar manutenção e evolução das regras.

### 6.2.3 Implementação do Filtro Parametrizável

```csharp
using System;

namespace RangoAgil.API.EndpointFilters;

public class RangosIsLockedFilter : IEndpointFilter
{
    public readonly int _LockedRangoID;

    public RangosIsLockedFilter(int lockedRangoID)
    {
        _LockedRangoID = lockedRangoID;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        int rangoId;

        if (context.HttpContext.Request.Method == "PUT")
        {
            rangoId = context.GetArgument<int>(3);
        }
        else if (context.HttpContext.Request.Method == "DELETE")
        {
            rangoId = context.GetArgument<int>(1);
        }
        else
        {
            throw new NotSupportedException("This filter is not supported for this scenario.");
        }

        if (rangoId == _LockedRangoID)
        {
            return TypedResults.Problem(new()
            {
                Status = 400,
                Title = "Rango já é Perfeito, você não precisa modificar ou deletar nada aqui.",
                Detail = "Você não pode modificar ou deletar essa Receita"
            });
        }

        var result = await next.Invoke(context);
        return result;
    }
}
```

### 6.2.4 Explicação da Sintaxe
- `RangosIsLockedFilter(int lockedRangoID)`  
  Permite configurar o filtro com diferentes IDs bloqueados.
- `context.GetArgument<int>(index)`  
  Obtém o argumento do endpoint pela posição, variando conforme o verbo HTTP.
- `TypedResults.Problem(...)`  
  Retorna resposta padronizada no formato Problem Details.
- `throw new NotSupportedException(...)`  
  Garante que o filtro só seja aplicado a cenários suportados.
- `next.Invoke(context)`  
  Continua o pipeline caso a validação seja aprovada.

### 6.2.5 Registro do Filtro em Múltiplos Endpoints

```csharp
public static void RegisterRangosEndpoints(this IEndpointRouteBuilder app)
{
    var rangosEndpoints = app.MapGroup("/rangos");
    var rangosComIDEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");

    rangosEndpoints.MapGet("", RangosHandlers.GetRangosAsync);

    rangosComIDEndpoints.MapGet("", RangosHandlers.GetRangoByIdAsync)
                        .WithName("GetRangos");

    rangosEndpoints.MapPost("", RangosHandlers.RangoPostAsync);

    rangosComIDEndpoints.MapPut("", RangosHandlers.RangoPutAsync)
                        .AddEndpointFilter(new RangosIsLockedFilter(8))
                        .AddEndpointFilter(new RangosIsLockedFilter(10));

    rangosComIDEndpoints.MapDelete("", RangosHandlers.RangoDeleteAsync)
                        .AddEndpointFilter(new RangosIsLockedFilter(8))
                        .AddEndpointFilter(new RangosIsLockedFilter(10));
}
```

### 6.2.6 Comparação: Filtro Parametrizável vs Filtro Fixo

| Característica | Parametrizável | Fixo |
|----------------|----------------|------|
| Reutilização | Alta | Baixa |
| Flexibilidade | Permite múltiplas configurações | Uma única regra |
| Manutenção | Centralizada | Pode gerar duplicação |
| Aplicação | Configurável por endpoint | Limitada ao comportamento interno |

### 6.2.7 Boas Práticas
- Utilizar filtros parametrizáveis quando múltiplos valores precisam ser protegidos.
- Documentar a posição dos argumentos usados no filtro.
- Evitar lógica complexa dentro do filtro; delegar ao domínio quando necessário.
- Garantir que o filtro seja aplicado apenas a verbos suportados.
- Manter consistência nas respostas de erro utilizando Problem Details.

### 6.2.8 Exemplo Avançado com Múltiplos IDs Bloqueados
Um filtro pode ser adaptado para receber uma lista de IDs bloqueados:

```csharp
public class RangosIsLockedFilter : IEndpointFilter
{
    private readonly HashSet<int> _lockedIds;

    public RangosIsLockedFilter(IEnumerable<int> lockedIds)
    {
        _lockedIds = new HashSet<int>(lockedIds);
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var id = context.GetArgument<int>(3);

        if (_lockedIds.Contains(id))
        {
            return TypedResults.Problem(new()
            {
                Status = 400,
                Title = "Rango protegido",
                Detail = "Este Rango não pode ser modificado."
            });
        }

        return await next(context);
    }
}
```

### 6.2.9 Conclusão
A reutilização de Endpoint Filters parametrizáveis fortalece a modularidade e a consistência das regras aplicadas aos endpoints. Essa abordagem reduz duplicação, facilita manutenção e permite aplicar regras específicas de forma clara e configurável.

---
## 6.3 Agrupando Endpoint Filter em Minimal API

### 6.3.1 Introdução
O agrupamento de Endpoint Filters permite aplicar múltiplos filtros simultaneamente a um conjunto de endpoints, evitando repetição de código e garantindo que regras de negócio sejam aplicadas de forma consistente. Essa abordagem é especialmente útil quando vários endpoints compartilham as mesmas restrições, como bloqueio de IDs específicos, validações ou auditorias.

### 6.3.2 Objetivo do Agrupamento de Filtros
O agrupamento tem como finalidade:
- Centralizar a aplicação de filtros em um único ponto.
- Reduzir duplicação ao evitar chamadas repetidas de `.AddEndpointFilter(...)`.
- Facilitar manutenção, permitindo adicionar ou remover filtros sem alterar cada endpoint individualmente.
- Garantir que todos os endpoints do grupo compartilhem o mesmo comportamento.

### 6.3.3 Implementação do Agrupamento de Filtros

```csharp
public static void RegisterRangosEndpoints(this IEndpointRouteBuilder app)
{
    var rangosEndpoints = app.MapGroup("/rangos");
    var rangosComIDEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");
    var rangosComIDEndpointsAndLockFilterEndpoints = rangosComIDEndpoints
        .AddEndpointFilter(new RangosIsLockedFilter(8))
        .AddEndpointFilter(new RangosIsLockedFilter(10));

    rangosEndpoints.MapGet("", RangosHandlers.GetRangosAsync);

    rangosComIDEndpoints.MapGet("", RangosHandlers.GetRangoByIdAsync)
                        .WithName("GetRangos");

    rangosEndpoints.MapPost("", RangosHandlers.RangoPostAsync);

    rangosComIDEndpointsAndLockFilterEndpoints.MapPut("", RangosHandlers.RangoPutAsync);
    rangosComIDEndpointsAndLockFilterEndpoints.MapDelete("", RangosHandlers.RangoDeleteAsync);
}
```

### 6.3.4 Explicação da Sintaxe
- `MapGroup("/rangos")`  
  Cria um agrupamento base para todos os endpoints relacionados a rangos.
- `MapGroup("/{rangoId:int}")`  
  Cria um subgrupo para endpoints que operam sobre um ID específico.
- `AddEndpointFilter(...)`  
  Aplica filtros ao grupo, garantindo que todos os endpoints subsequentes herdem esses filtros.
- `rangosComIDEndpointsAndLockFilterEndpoints.MapPut(...)`  
  Aplica o filtro agrupado automaticamente ao endpoint PUT.
- `new RangosIsLockedFilter(8)` e `new RangosIsLockedFilter(10)`  
  Configura dois filtros parametrizados, bloqueando IDs específicos.

### 6.3.5 Benefícios do Agrupamento de Filtros

- **Consistência** — todos os endpoints do grupo compartilham as mesmas regras.
- **Escalabilidade** — novos endpoints adicionados ao grupo automaticamente recebem os filtros.
- **Organização** — reduz poluição visual e repetição de código.
- **Flexibilidade** — permite combinar múltiplos filtros em cadeia.

### 6.3.6 Comparação: Filtros Agrupados vs Filtros Individuais

| Característica | Filtros Agrupados | Filtros Individuais |
|----------------|-------------------|----------------------|
| Repetição de código | Baixa | Alta |
| Manutenção | Centralizada | Distribuída |
| Aplicação | Automática para o grupo | Manual por endpoint |
| Flexibilidade | Alta | Moderada |

### 6.3.7 Boas Práticas
- Criar grupos de endpoints com responsabilidades claras antes de aplicar filtros.
- Utilizar filtros agrupados para regras que se repetem em múltiplos endpoints.
- Evitar aplicar filtros desnecessários a endpoints que não precisam da regra.
- Documentar quais filtros estão aplicados ao grupo para facilitar manutenção.
- Preferir filtros parametrizáveis quando múltiplos valores precisam ser bloqueados.

### 6.3.8 Exemplo Avançado com Agrupamento e Filtros Adicionais
Um grupo pode receber filtros de auditoria, validação e bloqueio simultaneamente:

```csharp
var rangosComIDEndpointsAndFilters = rangosComIDEndpoints
    .AddEndpointFilter(new RangosIsLockedFilter(8))
    .AddEndpointFilter(new RangosIsLockedFilter(10))
    .AddEndpointFilter(new RangoAuditFilter());
```

Esse padrão permite compor regras complexas sem alterar os handlers.

### 6.3.9 Conclusão
O agrupamento de Endpoint Filters é uma técnica poderosa para manter o código organizado, reutilizável e consistente. Ao aplicar filtros diretamente ao grupo de endpoints, a aplicação ganha modularidade e reduz duplicação, facilitando a evolução das regras de negócio.

---
## 6.4 Logger em Endpoint Filter

### 6.4.1 Introdução
A utilização de logs dentro de Endpoint Filters permite registrar informações importantes sobre o comportamento dos endpoints após a execução do handler. Esse padrão é útil para auditoria, rastreamento de erros, monitoramento de respostas e identificação de padrões de falha. O filtro apresentado adiciona uma camada de observabilidade ao pipeline, registrando quando um recurso solicitado retorna *NotFound (404)*.

### 6.4.2 Objetivo do Filtro de Log
O filtro `LogNotFoundResponseFilter` tem como finalidade:
- Detectar respostas com status HTTP 404.
- Registrar informações relevantes sobre o recurso solicitado.
- Auxiliar na identificação de rotas acessadas incorretamente.
- Complementar o comportamento de filtros de validação ou bloqueio já existentes.

Esse filtro é aplicado após a execução do handler, garantindo que a lógica de log seja baseada no resultado final do endpoint.

### 6.4.3 Implementação do Filtro de Log

```csharp
using System.Net;

namespace RangoAgil.API.EndpointFilters;

public class LogNotFoundResponseFilter(ILogger<LogNotFoundResponseFilter> logger) : IEndpointFilter
{
    public readonly ILogger<LogNotFoundResponseFilter> _logger = logger;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);
        var actualResults = (result is INestedHttpResult result1) ? result1.Result : (IResult)result!;

        if (actualResults is IStatusCodeHttpResult { StatusCode: (int)HttpStatusCode.NotFound })
        {
            _logger.LogInformation($"Resource {context.HttpContext.Request.Path} was not found.");
        }

        return result;
    }
}
```

### 6.4.4 Explicação da Sintaxe
- `ILogger<LogNotFoundResponseFilter>` injeta um logger específico para o filtro.
- `next(context)` executa o handler e captura o resultado retornado.
- `INestedHttpResult` permite acessar resultados encapsulados, comuns em respostas tipadas.
- `IStatusCodeHttpResult` identifica respostas que possuem código HTTP associado.
- `HttpStatusCode.NotFound` representa o status 404.
- `_logger.LogInformation(...)` registra a ocorrência de recurso não encontrado.

### 6.4.5 Registro do Filtro no Agrupamento de Endpoints

```csharp
public static void RegisterRangosEndpoints(this IEndpointRouteBuilder app)
{
    var rangosEndpoints = app.MapGroup("/rangos");
    var rangosComIDEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");
    var rangosComIDEndpointsAndLockFilterEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}")
        .AddEndpointFilter(new RangosIsLockedFilter(8))
        .AddEndpointFilter(new RangosIsLockedFilter(10))
        .AddEndpointFilter<LogNotFoundResponseFilter>();

    rangosEndpoints.MapGet("", RangosHandlers.GetRangosAsync);

    rangosComIDEndpoints.MapGet("", RangosHandlers.GetRangoByIdAsync)
                        .WithName("GetRangos");

    rangosEndpoints.MapPost("", RangosHandlers.RangoPostAsync);

    rangosComIDEndpointsAndLockFilterEndpoints.MapPut("", RangosHandlers.RangoPutAsync);
    rangosComIDEndpointsAndLockFilterEndpoints.MapDelete("", RangosHandlers.RangoDeleteAsync);
}
```

### 6.4.6 Benefícios do Uso de Logs em Filtros
- Registro automático de respostas críticas, como 404.
- Observabilidade aprimorada sem modificar handlers.
- Redução de duplicação de código de log.
- Facilidade para identificar rotas acessadas incorretamente.
- Integração natural com ferramentas de monitoramento.

### 6.4.7 Comparação: Log no Handler vs Log no Filtro

| Característica | Log no Handler | Log no Filtro |
|----------------|----------------|----------------|
| Localização | Dentro da lógica do endpoint | Fora do handler, no pipeline |
| Reutilização | Baixa | Alta |
| Consistência | Depende do desenvolvedor | Garantida pelo filtro |
| Escopo | Específico | Abrange múltiplos endpoints |
| Manutenção | Pode gerar duplicação | Centralizada |

### 6.4.8 Boas Práticas
- Utilizar filtros para logs que se repetem em vários endpoints.
- Registrar apenas informações úteis para diagnóstico.
- Evitar logs redundantes ou excessivos.
- Manter filtros pequenos e focados em uma única responsabilidade.
- Integrar logs com correlação de requisições (`TraceIdentifier`) quando necessário.

### 6.4.9 Exemplo Avançado com Log Estruturado
Um filtro pode registrar informações adicionais sobre a requisição:

```csharp
_logger.LogInformation("NotFound detected. Path: {path}, Method: {method}, TraceId: {trace}",
    context.HttpContext.Request.Path,
    context.HttpContext.Request.Method,
    context.HttpContext.TraceIdentifier);
```

Esse padrão facilita consultas em ferramentas como Elastic Stack, Grafana Loki ou Application Insights.

---

## 6.5 Validação por Endpoint Filter em Minimal API

### 6.5.1 Introdução
A validação de dados é um dos pilares fundamentais no desenvolvimento de APIs. Em Minimal API, a validação pode ser aplicada diretamente no pipeline por meio de Endpoint Filters, permitindo que regras de validação sejam executadas antes da chamada ao handler. Essa abordagem mantém os handlers mais limpos, centraliza a lógica de validação e garante consistência entre endpoints.

### 6.5.2 Objetivo do Filtro de Validação
O filtro `ValidateAnnotationFilter` utiliza atributos de validação (`DataAnnotations`) combinados com a biblioteca `MiniValidation` para validar objetos recebidos no corpo da requisição. O objetivo é:
- Garantir que o DTO enviado pelo cliente esteja em conformidade com as regras declaradas.
- Retornar erros estruturados no formato Problem Details.
- Evitar que handlers recebam dados inválidos.
- Centralizar a validação, reduzindo duplicação de código.

#### 6.5.2.1 Instalação da biblioteca `MiniValidation`

```shell
dotnet add package MiniValidation   
```

### 6.5.3 Modelo com Data Annotations

```csharp
using System;
using System.ComponentModel.DataAnnotations;

namespace RangoAgil.API.Models;

public class RangoForCreationDTO
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Nome { get; set; }
}
```

Esse DTO exige que:
- O campo `Nome` seja obrigatório.
- O nome tenha entre 3 e 100 caracteres.

### 6.5.4 Implementação do Endpoint Filter de Validação

```csharp
using System;
using MiniValidation;
using RangoAgil.API.Models;

namespace RangoAgil.API.EndpointFilters;

public class ValidateAnnotationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var rangoForCreationDTO = context.GetArgument<RangoForCreationDTO>(2);

        if (!MiniValidator.TryValidate(rangoForCreationDTO, out var validationErros))
        {
            return TypedResults.ValidationProblem(validationErros);
        }

        return await next(context);
    }
}
```

### 6.5.5 Explicação da Sintaxe
- `context.GetArgument<RangoForCreationDTO>(2)` obtém o DTO enviado no corpo da requisição.
- `MiniValidator.TryValidate(...)` executa a validação baseada nos atributos declarados no modelo.
- `TypedResults.ValidationProblem(...)` retorna erros estruturados conforme RFC 7807.
- `next(context)` executa o handler apenas se a validação for bem-sucedida.

### 6.5.6 Registro do Filtro no Endpoint POST

```csharp
public static void RegisterRangosEndpoints(this IEndpointRouteBuilder app)
{
    var rangosEndpoints = app.MapGroup("/rangos");
    var rangosComIDEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");
    var rangosComIDEndpointsAndLockFilterEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}")
        .AddEndpointFilter(new RangosIsLockedFilter(8))
        .AddEndpointFilter(new RangosIsLockedFilter(10))
        .AddEndpointFilter<LogNotFoundResponseFilter>();

    rangosEndpoints.MapGet("", RangosHandlers.GetRangosAsync);

    rangosComIDEndpoints.MapGet("", RangosHandlers.GetRangoByIdAsync)
                        .WithName("GetRangos");

    rangosEndpoints.MapPost("", RangosHandlers.RangoPostAsync)
                   .AddEndpointFilter<ValidateAnnotationFilter>();

    rangosComIDEndpointsAndLockFilterEndpoints.MapPut("", RangosHandlers.RangoPutAsync);
    rangosComIDEndpointsAndLockFilterEndpoints.MapDelete("", RangosHandlers.RangoDeleteAsync);
}
```

### 6.5.7 Benefícios da Validação via Endpoint Filter
- **Centralização** da lógica de validação.
- **Reutilização** do filtro em múltiplos endpoints.
- **Separação de responsabilidades**, mantendo handlers focados na lógica de negócio.
- **Consistência** nas respostas de erro.
- **Flexibilidade** para combinar validação com outros filtros (logs, bloqueios, auditoria).

### 6.5.8 Comparação: Validação no Handler vs Endpoint Filter

| Característica | Handler | Endpoint Filter |
|----------------|---------|-----------------|
| Reutilização | Baixa | Alta |
| Organização | Mistura validação com lógica de negócio | Validação isolada |
| Consistência | Depende do desenvolvedor | Garantida |
| Manutenção | Pode gerar duplicação | Centralizada |
| Escalabilidade | Limitada | Ideal para múltiplos endpoints |

### 6.5.9 Exemplo Avançado com Múltiplos DTOs
Um filtro pode validar diferentes tipos de DTOs usando reflexão:

```csharp
public class GenericValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        foreach (var arg in context.Arguments)
        {
            if (MiniValidator.TryValidate(arg, out var errors) == false)
            {
                return TypedResults.ValidationProblem(errors);
            }
        }

        return await next(context);
    }
}
```

Esse padrão permite validar qualquer objeto anotado com DataAnnotations.

---

# 7 Segurança e Configuração

## 7.1 Swagger em Minimal API

### 7.1.1 Introdução
Swagger é uma ferramenta essencial para documentação, exploração e teste de APIs REST. Em aplicações Minimal API, a integração com *Swashbuckle.AspNetCore* permite gerar automaticamente a especificação OpenAPI e disponibilizar uma interface gráfica interativa para inspeção dos endpoints. Isso facilita o desenvolvimento, depuração e comunicação entre equipes.

### 7.1.2 Instalação do Swashbuckle.AspNetCore
A instalação é realizada via CLI:

```bash
dotnet add package Swashbuckle.AspNetCore
```

Esse pacote adiciona suporte completo ao Swagger e ao Swagger UI.

### 7.1.3 Configuração do LaunchSettings para Abrir o Swagger
O arquivo `launchSettings.json` pode ser configurado para abrir automaticamente o Swagger ao iniciar a aplicação:

```json
{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5148",
      "launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "https://localhost:7043;http://localhost:5148",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

Essa configuração garante que o navegador abra diretamente a interface do Swagger ao iniciar o projeto.

### 7.1.4 Registro dos Serviços Necessários
A aplicação precisa registrar os serviços responsáveis pela geração da documentação OpenAPI:

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

- `AddEndpointsApiExplorer()` habilita a descoberta automática dos endpoints.
- `AddSwaggerGen()` gera a especificação OpenAPI e configura o Swagger UI.

### 7.1.5 Configuração do ProblemDetails
A aplicação também utiliza Problem Details com metadados adicionais:

```csharp
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["requestId"] = context.HttpContext.TraceIdentifier;
        context.ProblemDetails.Extensions["timestamp"] = DateTime.UtcNow;
    };
});
```

Essa configuração adiciona informações úteis para rastreamento e auditoria.

### 7.1.6 Pipeline da Aplicação com Swagger

```csharp
var app = builder.Build();

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RegisterRangosEndpoints();
app.RegisterIngredientesEndpoints();

app.Run();
```

- Em **produção**, o Swagger é desabilitado por padrão.
- Em **desenvolvimento**, o Swagger e o Swagger UI são habilitados automaticamente.

### 7.1.7 Explicação da Sintaxe
- `app.UseSwagger()` gera o documento OpenAPI em tempo de execução.
- `app.UseSwaggerUI()` habilita a interface gráfica interativa.
- `launchUrl: "swagger"` abre automaticamente o Swagger no navegador.
- `AddEndpointsApiExplorer()` é obrigatório para Minimal API, pois descobre endpoints definidos via `MapGet`, `MapPost`, etc.

### 7.1.8 Exemplo Prático de Endpoint Documentado
Swagger detecta automaticamente endpoints Minimal API:

```csharp
app.MapGet("/rangos", async (RangoDbContext db) =>
{
    var rangos = await db.Rangos.ToListAsync();
    return TypedResults.Ok(rangos);
})
.WithName("GetRangos")
.WithSummary("Retorna todos os rangos cadastrados.")
.WithDescription("Endpoint responsável por listar todos os rangos disponíveis no banco de dados.");
```

As extensões `WithSummary` e `WithDescription` enriquecem a documentação gerada.

### 7.1.9 Boas Práticas
- Habilitar Swagger apenas em desenvolvimento.
- Utilizar `WithSummary`, `WithDescription` e `WithTags` para melhorar a documentação.
- Versionar a API e configurar múltiplos documentos OpenAPI quando necessário.
- Adicionar segurança (JWT, API Key) ao Swagger quando aplicável.
- Manter consistência entre nomes, descrições e respostas documentadas.

### 7.1.10 Comparação: Swagger vs Ferramentas Alternativas

| Ferramenta | Características | Vantagens |
|------------|-----------------|-----------|
| Swagger (Swashbuckle) | Padrão de mercado, integrado ao ASP.NET | Fácil configuração, UI robusta |
| NSwag | Gera clientes TypeScript/C# | Ideal para geração automática de SDKs |
| OpenAPI CLI | Independente de linguagem | Flexível, porém menos integrado |

### 7.1.11 Conclusão
A integração do Swagger em Minimal API melhora a experiência de desenvolvimento, facilita testes e fornece documentação clara e acessível. A configuração apresentada garante uma documentação completa, organizada e alinhada às boas práticas do ecossistema .NET.

---
## 7.2 Endpoint Deprecated, Summary e Descrição em Minimal API

### 7.2.1 Introdução
A anotação de endpoints com informações de *deprecated*, *summary* e *description* melhora a documentação gerada pelo Swagger/OpenAPI, tornando a API mais clara para consumidores e facilitando a evolução da aplicação. A combinação de metadados personalizados, *operation filters* e extensões nativas do ASP.NET permite marcar rotas como obsoletas, orientar migrações e enriquecer a documentação.

### 7.2.2 Objetivo da Marcação de Endpoints
A marcação de endpoints como obsoletos tem como finalidade:
- Informar consumidores sobre rotas que serão removidas futuramente.
- Evitar uso indevido de endpoints antigos.
- Facilitar transição para novas versões da API.
- Documentar claramente o motivo e a alternativa recomendada.

O uso de *summary* e *description* complementa a documentação, tornando o Swagger mais informativo e profissional.

### 7.2.3 Implementação do Endpoint Deprecated

```csharp
app.MapGet("/pratos/{pratoId:int}", (int pratoId) => $"O prato {pratoId} é delicioso.")
    .WithMetadata(new DeprecatedInSwaggerMetadata())
    .AddOpenApiOperationTransformer((operation, context, ct) =>
    {
        operation.Deprecated = true;
        return Task.CompletedTask;
    })
    .WithSummary("Este endpoint está deprecated e será descontinuado na versão 2 desta API.")
    .WithDescription("Por favor utilize a outra rota desta API sendo ela /rangos/{rangoId} para evitar maiores transtornos.");
```

Esse endpoint:
- É marcado como *deprecated*.
- Recebe um resumo explicando sua descontinuação.
- Recebe uma descrição orientando o uso da rota alternativa.

### 7.2.4 Metadado Personalizado para Deprecated

```csharp
namespace RangoAgil.API.Metadatas;

public class DeprecatedInSwaggerMetadata
{
}
```

Esse metadado é utilizado pelo *operation filter* para identificar endpoints obsoletos.

### 7.2.5 Operation Filter para Marcar Deprecated no Swagger

```csharp
using Microsoft.OpenApi;
using RangoAgil.API.Metadatas;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RangoAgil.API.OperationFilters;

public sealed class DeprecatedInSwaggerOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasMetadata =
            context.ApiDescription.ActionDescriptor.EndpointMetadata
                .OfType<DeprecatedInSwaggerMetadata>()
                .Any();

        if (hasMetadata)
        {
            operation.Deprecated = true;
        }
    }
}
```

Esse filtro:
- Verifica se o endpoint possui o metadado `DeprecatedInSwaggerMetadata`.
- Marca a operação como obsoleta no documento OpenAPI.

### 7.2.6 Registro do Swagger com Operation Filter

```csharp
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<DeprecatedInSwaggerOperationFilter>();
});
builder.Services.AddOpenApi();
```

A adição do `AddOpenApi()` habilita recursos adicionais de documentação no ASP.NET.

### 7.2.7 Registro dos Endpoints com Summary e Description

```csharp
public static void RegisterRangosEndpoints(this IEndpointRouteBuilder app)
{
    app.MapGet("/pratos/{pratoId:int}", (int pratoId) => $"O prato {pratoId} é delicioso.")
        .WithMetadata(new DeprecatedInSwaggerMetadata())
        .AddOpenApiOperationTransformer((operation, context, ct) =>
        {
            operation.Deprecated = true;
            return Task.CompletedTask;
        })
        .WithSummary("Este endpoint está deprecated e será descontinuado na versão 2 desta API.")
        .WithDescription("Por favor utilize a outra rota desta API sendo ela /rangos/{rangoId} para evitar maiores transtornos.");

    var rangosEndpoints = app.MapGroup("/rangos");

    var rangosComIDEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");
    var rangosComIDEndpointsAndLockFilterEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}")
        .AddEndpointFilter(new RangosIsLockedFilter(8))
        .AddEndpointFilter(new RangosIsLockedFilter(10))
        .AddEndpointFilter<LogNotFoundResponseFilter>();

    rangosEndpoints.MapGet("", RangosHandlers.GetRangosAsync)
        .WithSummary("Está rota retornará uma lista com todos os rangos.");

    rangosComIDEndpoints.MapGet("", RangosHandlers.GetRangoByIdAsync)
        .WithName("GetRangos");

    rangosEndpoints.MapPost("", RangosHandlers.RangoPostAsync)
        .AddEndpointFilter<ValidateAnnotationFilter>();

    rangosComIDEndpointsAndLockFilterEndpoints.MapPut("", RangosHandlers.RangoPutAsync);
    rangosComIDEndpointsAndLockFilterEndpoints.MapDelete("", RangosHandlers.RangoDeleteAsync);
}
```

### 7.2.8 Explicação da Sintaxe
- `WithMetadata(...)` adiciona metadados personalizados ao endpoint.
- `AddOpenApiOperationTransformer(...)` altera a operação OpenAPI antes da geração do documento.
- `operation.Deprecated = true` marca o endpoint como obsoleto.
- `WithSummary(...)` adiciona um resumo curto exibido no Swagger UI.
- `WithDescription(...)` adiciona uma descrição detalhada para orientar o consumidor da API.

### 7.2.9 Benefícios da Documentação Avançada
- Melhora a experiência de desenvolvedores que consomem a API.
- Facilita a migração entre versões.
- Reduz erros de uso de endpoints obsoletos.
- Torna o Swagger mais completo e profissional.
- Permite versionamento gradual da API.

### 7.2.10 Comparação: Deprecated via Metadata vs via Operation Transformer

| Abordagem | Características | Vantagens |
|----------|-----------------|-----------|
| Metadado + OperationFilter | Centralizado, reutilizável | Ideal para grandes APIs |
| OperationTransformer direto no endpoint | Local, explícito | Útil para casos pontuais |

### 7.2.11 Conclusão
A combinação de metadados personalizados, filtros de operação e extensões de documentação permite criar uma API bem documentada, clara e preparada para evolução. A marcação de endpoints como *deprecated*, aliada ao uso de *summary* e *description*, melhora a comunicação com consumidores e reduz riscos de uso incorreto.

---
## 7.3 Autenticação e Autorização em Minimal API

### 7.3.1 Introdução
A autenticação e autorização são componentes essenciais para proteger APIs, controlar acesso a recursos e garantir que apenas usuários ou sistemas autorizados executem determinadas operações. Em Minimal API, a integração com JWT Bearer é direta e segue o padrão do ASP.NET Core, permitindo validar tokens emitidos por provedores externos ou internos.

A configuração apresentada utiliza o esquema **Bearer**, valida emissor e audiência, e aplica políticas de autorização diretamente nos grupos de endpoints.

### 7.3.2 Configuração do Pacote de Autenticação JWT
A instalação do pacote é realizada via CLI:

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

Esse pacote habilita o middleware de autenticação JWT Bearer.

### 7.3.3 Configuração do JWT no appsettings.json

```json
"Authentication": {
  "DefaultScheme": "Bearer",
  "Schemes": {
    "Bearer": {
      "ValidAudiences": [
        "rangos-api"
      ],
      "ValidIssuer": "the_component_that_created_the_token"
    }
  }
}
```

Essa configuração define:
- **DefaultScheme** como Bearer.
- **ValidIssuer** para validar quem emitiu o token.
- **ValidAudiences** para validar quem pode consumir o token.

### 7.3.4 Registro dos Serviços de Autenticação e Autorização

```csharp
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
```

- `AddAuthentication()` registra o middleware de autenticação.
- `AddJwtBearer()` habilita validação de tokens JWT.
- `AddAuthorization()` habilita políticas e atributos de autorização.

### 7.3.5 Pipeline de Autenticação e Autorização

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

A ordem é importante:
1. **Autenticação** identifica o usuário.
2. **Autorização** verifica se o usuário pode acessar o recurso.

### 7.3.6 Aplicação de Autorização nos Endpoints

```csharp
var rangosEndpoints = app.MapGroup("/rangos")
    .RequireAuthorization();
```

Esse grupo exige autenticação para todos os endpoints internos.

### 7.3.7 Exceções com AllowAnonymous

```csharp
rangosComIDEndpoints.MapGet("", RangosHandlers.GetRangoByIdAsync)
    .WithName("GetRangos")
    .AllowAnonymous();
```

Esse endpoint específico permite acesso sem autenticação, mesmo estando dentro de um grupo protegido.

### 7.3.8 Registro Completo dos Endpoints com Autorização

```csharp
public static void RegisterRangosEndpoints(this IEndpointRouteBuilder app)
{
    app.MapGet("/pratos/{pratoId:int}", (int pratoId)=> $"O prato {pratoId} é delicioso.")
        .WithMetadata(new DeprecatedInSwaggerMetadata())
        .AddOpenApiOperationTransformer((operation, context, ct) =>
        {
            operation.Deprecated = true;
            return Task.CompletedTask;
        })
        .WithSummary("Este endpoint está deprecated e será descontinuado na versão 2 desta API.")
        .WithDescription("Por favor utilize a outra rota desta API sendo ela /rangos/{rangoId} para evitar maiores transtornos.");

    var rangosEndpoints = app.MapGroup("/rangos")
        .RequireAuthorization();

    var rangosComIDEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");
    var rangosComIDEndpointsAndLockFilterEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}")
        .AddEndpointFilter(new RangosIsLockedFilter(8))
        .AddEndpointFilter(new RangosIsLockedFilter(10))
        .AddEndpointFilter<LogNotFoundResponseFilter>();

    rangosEndpoints.MapGet("", RangosHandlers.GetRangosAsync)
        .WithSummary("Está rota retornará uma lista com todos os rangos.");

    rangosComIDEndpoints.MapGet("", RangosHandlers.GetRangoByIdAsync)
        .WithName("GetRangos")
        .AllowAnonymous();

    rangosEndpoints.MapPost("", RangosHandlers.RangoPostAsync)
        .AddEndpointFilter<ValidateAnnotationFilter>();

    rangosComIDEndpointsAndLockFilterEndpoints.MapPut("", RangosHandlers.RangoPutAsync);
    rangosComIDEndpointsAndLockFilterEndpoints.MapDelete("", RangosHandlers.RangoDeleteAsync);
}
```

### 7.3.9 Explicação da Sintaxe
- `RequireAuthorization()` aplica autorização a todos os endpoints do grupo.
- `AllowAnonymous()` permite exceções dentro de grupos protegidos.
- `AddJwtBearer()` habilita validação de tokens JWT.
- `UseAuthentication()` deve vir antes de `UseAuthorization()` no pipeline.
- `ValidIssuer` e `ValidAudiences` garantem que o token seja confiável.

### 7.3.10 Boas Práticas
- Proteger grupos inteiros com `RequireAuthorization()` para evitar endpoints esquecidos.
- Usar `AllowAnonymous()` apenas quando necessário.
- Validar emissor e audiência para evitar tokens falsificados.
- Utilizar HTTPS para proteger o envio do token.
- Documentar no Swagger quais endpoints exigem autenticação.

### 7.3.11 Comparação: Autenticação vs Autorização

| Conceito | Função | Exemplo |
|---------|--------|---------|
| Autenticação | Identifica quem é o usuário | Validar token JWT |
| Autorização | Verifica se o usuário pode acessar o recurso | Permitir apenas admins em um endpoint |

### 7.3.12 Conclusão
A integração de autenticação e autorização com JWT em Minimal API fornece uma camada robusta de segurança, permitindo controlar acesso a recursos sensíveis e proteger operações críticas. A combinação de grupos protegidos, filtros e validação de tokens garante uma API segura, escalável e alinhada às boas práticas modernas.

---

## 7.4 Criando Token via Terminal com `dotnet user-jwts`

### 7.4.1 Introdução
A criação de tokens JWT diretamente pelo terminal utilizando o comando `dotnet user-jwts` é uma forma prática de gerar tokens válidos para desenvolvimento e testes locais. Esse recurso é integrado ao .NET SDK e facilita a autenticação em APIs protegidas sem necessidade de implementar imediatamente um provedor de identidade completo.

O token gerado segue as configurações definidas no `appsettings.json`, incluindo emissor (*issuer*) e audiência (*audience*), garantindo compatibilidade com o middleware `JwtBearer`.

### 7.4.2 Criando o Token JWT via Terminal
O comando abaixo cria um token JWT com a audiência configurada para `"rangos-api"`:

```bash
dotnet user-jwts create --audience rangos-api
```

Após executar o comando, o terminal exibirá:
- O token JWT completo.
- A data de expiração.
- O issuer utilizado.
- As claims padrão.

Esse token pode ser utilizado diretamente no Swagger ou em ferramentas como Postman e Insomnia.

### 7.4.3 Configuração do `appsettings.json` para Tokens Criados via Terminal

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "RangoDBConStr": "Data Source=Rango.db"
  },
  "AllowedHosts": "*",
  "Authentication": {
    "DefaultScheme": "Bearer",
    "Schemes": {
      "Bearer": {
        "ValidAudiences": [
          "rangos-api"
        ],
        "ValidIssuer": "dotnet-user-jwts"
      }
    }
  }
}
```

Explicação dos campos:
- **DefaultScheme** define o esquema padrão de autenticação.
- **ValidAudiences** deve corresponder ao parâmetro `--audience` usado no terminal.
- **ValidIssuer** deve ser `"dotnet-user-jwts"` quando o token é criado via CLI.
- O middleware `JwtBearer` validará automaticamente essas informações.

### 7.4.4 Integração com a Aplicação

```csharp
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
```

E no pipeline:

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

Com isso, qualquer endpoint protegido por `.RequireAuthorization()` exigirá um token JWT válido.

### 7.4.5 Testando o Token no Swagger
Com o Swagger habilitado em ambiente de desenvolvimento:

1. Execute a API.
2. Abra o Swagger UI.
3. Clique em **Authorize**.
4. Insira o token no formato:

```
Bearer SEU_TOKEN_AQUI
```

5. Execute os endpoints protegidos normalmente.

### 7.4.6 Validando o Token no jwt.io
O site https://www.jwt.io/ permite:
- Decodificar o token.
- Visualizar claims.
- Verificar estrutura.
- Validar assinatura (quando configurado).

Esse recurso é útil para depuração e entendimento do conteúdo do JWT.

### 7.4.7 Boas Práticas
- Utilizar `dotnet user-jwts` apenas em desenvolvimento.
- Nunca expor tokens gerados em ambientes públicos.
- Validar emissor e audiência para evitar tokens falsificados.
- Utilizar HTTPS para proteger o envio do token.
- Documentar no Swagger quais endpoints exigem autenticação.

### 7.4.8 Comparação: Token via Terminal vs Token via Identity Provider

| Método | Características | Uso Ideal |
|--------|------------------|-----------|
| `dotnet user-jwts` | Rápido, local, sem servidor externo | Desenvolvimento e testes |
| Identity Provider (Azure AD, Auth0, Keycloak) | Seguro, escalável, com roles e políticas | Produção |

### 7.4.9 Conclusão
A criação de tokens JWT via terminal com `dotnet user-jwts` simplifica o desenvolvimento de APIs protegidas, permitindo testar autenticação e autorização de forma rápida e segura. A integração com o middleware `JwtBearer` garante compatibilidade imediata com Minimal API e facilita a evolução para provedores de identidade mais robustos no futuro.

---
## 7.5 Token com Policy, Role e Claim em Minimal API

### 7.5.1 Introdução
A combinação de **roles**, **claims** e **policies** permite criar regras de autorização altamente específicas em Minimal API. Essa abordagem garante que apenas usuários com permissões adequadas acessem determinados recursos, fortalecendo a segurança da aplicação. A integração com tokens JWT criados via `dotnet user-jwts` facilita o desenvolvimento local e a validação dessas regras.

### 7.5.2 Criando Token com Role e Claim via Terminal
O comando abaixo cria um token JWT contendo:
- **audience**: rangos-api  
- **role**: admin  
- **claim**: country=Brazil  

```bash
dotnet user-jwts create --audience rangos-api --claim country=Brazil --role admin
```

Esse token será validado pelas políticas configuradas na aplicação.

### 7.5.3 Configuração do appsettings.json para Validar Role e Claim

```json
"Authentication": {
  "DefaultScheme": "Bearer",
  "Schemes": {
    "Bearer": {
      "ValidAudiences": [
        "rangos-api"
      ],
      "ValidIssuer": "dotnet-user-jwts"
    }
  }
}
```

Essa configuração garante que o middleware `JwtBearer` valide corretamente o token emitido via CLI.

### 7.5.4 Definição de Policies com Role e Claim

```csharp
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequireAdminFromBrazil", policy =>
        policy
            .RequireRole("admin")
            .RequireClaim("country", "Brazil")
    )
    .AddPolicy("RequirManagerFromBrazil", policy =>
        policy
            .RequireRole("Manager")
            .RequireClaim("country", "Brazil")
    );
```

Explicação:
- **RequireRole("admin")** exige que o token contenha o role admin.
- **RequireClaim("country", "Brazil")** exige que o token contenha a claim country=Brazil.
- Policies permitem combinar múltiplas regras de forma declarativa.

### 7.5.5 Aplicando Policy em Grupos de Endpoints

```csharp
var rangosComIDEndpointsAndLockFilterEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}")
    .RequireAuthorization("RequireAdminFromBrazil")
    .AddEndpointFilter(new RangosIsLockedFilter(8))
    .AddEndpointFilter(new RangosIsLockedFilter(10))
    .AddEndpointFilter<LogNotFoundResponseFilter>();
```

Esse grupo exige:
- Token válido.
- Role admin.
- Claim country=Brazil.

Somente usuários com essas características poderão acessar PUT e DELETE.

### 7.5.6 Explicação da Sintaxe
- **RequireAuthorization("RequireAdminFromBrazil")** aplica a policy ao grupo inteiro.
- **RequireRole** e **RequireClaim** são avaliados pelo middleware de autorização.
- **dotnet user-jwts** injeta automaticamente roles e claims no token.
- **AddEndpointFilter** continua funcionando normalmente, complementando a autorização com regras de negócio.

### 7.5.7 Exemplo Completo de Token Gerado
Após executar o comando, o terminal exibirá algo como:

```
Token:
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

Claims:
- role: admin
- country: Brazil
- aud: rangos-api
- iss: dotnet-user-jwts
```

Esse token pode ser usado no Swagger ou Postman.

### 7.5.8 Comparação entre Role, Claim e Policy

| Elemento | Função | Exemplo |
|----------|--------|---------|
| Role | Representa o papel do usuário | admin, Manager |
| Claim | Informação adicional sobre o usuário | country=Brazil |
| Policy | Combinação de regras | role + claim |

Policies permitem criar regras complexas sem poluir os endpoints.

### 7.5.9 Boas Práticas
- Criar policies descritivas e específicas.
- Evitar lógica de autorização dentro dos handlers.
- Utilizar claims para informações contextuais (país, departamento, nível de acesso).
- Utilizar roles para permissões amplas (admin, manager).
- Testar tokens no https://www.jwt.io/ para validar conteúdo.

### 7.5.10 Conclusão
A combinação de tokens JWT com roles, claims e policies fornece uma camada robusta de autorização em Minimal API. Essa abordagem permite proteger endpoints sensíveis, aplicar regras específicas por grupo e manter o código organizado e seguro.

---

## 7.6 Adicionando Token no Swagger

### 7.6.1 Introdução
A integração de autenticação JWT com Swagger permite testar endpoints protegidos diretamente pela interface gráfica, fornecendo uma experiência completa de desenvolvimento e depuração. Para isso, é necessário configurar o Swagger para reconhecer o esquema de segurança *Bearer* e permitir que o usuário insira o token JWT no botão **Authorize**.

A configuração apresentada adiciona uma definição de segurança personalizada chamada **TokenAuthRango**, além de um requisito global que aplica essa definição a todas as operações documentadas.

### 7.6.2 Definição do Esquema de Segurança no Swagger

```csharp
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("TokenAuthRango",
        new OpenApiSecurityScheme
        {
            Name = "Autorization",
            Description = "Token baseado em autenticação e autorização",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            In = ParameterLocation.Header
        }
    );

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("TokenAuthRango", document)] = new List<string>()
    });

    options.OperationFilter<DeprecatedInSwaggerOperationFilter>();
});
```

### 7.6.3 Explicação da Sintaxe
- **AddSecurityDefinition("TokenAuthRango", …)**  
  Registra um esquema de segurança chamado *TokenAuthRango*, que será exibido no Swagger UI.
- **Type = SecuritySchemeType.Http**  
  Indica que o esquema utiliza autenticação HTTP.
- **Scheme = "Bearer"**  
  Define o uso do padrão Bearer Token.
- **In = ParameterLocation.Header**  
  O token será enviado no cabeçalho da requisição.
- **AddSecurityRequirement(...)**  
  Aplica o esquema de segurança a todas as operações, exigindo que o usuário forneça um token para endpoints protegidos.
- **OpenApiSecuritySchemeReference**  
  Faz referência ao esquema definido anteriormente, garantindo consistência entre definição e requisito.

### 7.6.4 Como o Swagger Passa a Funcionar com JWT
Após essa configuração:
- O Swagger exibirá o botão **Authorize**.
- Ao clicar, o usuário poderá inserir o token no formato:

```
Bearer SEU_TOKEN_AQUI
```

- Todas as requisições subsequentes enviadas pelo Swagger incluirão automaticamente o cabeçalho:

```
Authorization: Bearer SEU_TOKEN_AQUI
```

- Endpoints protegidos por `.RequireAuthorization()` funcionarão normalmente.

### 7.6.5 Exemplo Prático de Uso
1. Gerar token via terminal:

```bash
dotnet user-jwts create --audience rangos-api --claim country=Brazil --role admin
```

2. Abrir o Swagger em ambiente de desenvolvimento.
3. Clicar em **Authorize**.
4. Inserir o token gerado.
5. Testar endpoints protegidos, como:

```
PUT /rangos/{rangoId}
DELETE /rangos/{rangoId}
```

### 7.6.6 Benefícios da Integração
- Facilita testes de autenticação e autorização.
- Permite validar policies, roles e claims diretamente no Swagger.
- Melhora a documentação da API ao indicar claramente que o endpoint exige token.
- Reduz erros de configuração ao testar manualmente.

### 7.6.7 Comparação: Segurança no Swagger vs Segurança no Código

| Aspecto | Swagger | Código |
|--------|---------|--------|
| Testes | Permite testar tokens facilmente | Depende de ferramentas externas |
| Documentação | Exibe requisitos de segurança | Não documenta automaticamente |
| Segurança | Apenas para desenvolvimento | Segurança real da API |
| Usabilidade | Interface amigável | Requer conhecimento técnico |

### 7.6.8 Conclusão
Adicionar suporte a tokens JWT no Swagger é essencial para testar APIs protegidas de forma prática e eficiente. A configuração apresentada integra o esquema Bearer ao Swagger, permitindo que desenvolvedores utilizem tokens gerados via CLI ou provedores externos diretamente na interface de documentação.

A seguir, seria interessante documentar a seção **7.7**, abordando como adicionar *roles*, *claims* e *policies* diretamente na documentação do Swagger para melhorar ainda mais a clareza da API.
