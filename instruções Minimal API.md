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

## 1.5 URL e Parâmetros

### 1.5.1 Introdução

Esta seção descreve como definir rotas em uma Minimal API utilizando parâmetros de URL. O objetivo é demonstrar como capturar valores dinâmicos enviados pelo cliente e utilizá‑los diretamente nos manipuladores de requisição.

A abordagem utiliza o padrão `{parametro}` dentro da rota, permitindo que o ASP.NET Core faça o *binding* automático para tipos primitivos como `int` e `string`.

### 1.5.2 Configuração Inicial

A aplicação registra o `DbContext` e define rotas básicas para testes. O foco desta seção é o comportamento das rotas que utilizam parâmetros.

#### 1.5.2.1 Código de Configuração

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

### 1.5.3 Explicação da Sintaxe

#### 1.5.3.1 Parâmetros de Rota

A sintaxe `{parametro}` define um espaço reservado dentro da URL. O ASP.NET Core interpreta o valor recebido e tenta convertê‑lo para o tipo especificado no manipulador.

Exemplo:

    ```csharp
    app.MapGet("/rangos/{numero}", (int numero) => ... );
    ```

O valor recebido em `/rangos/10` será convertido automaticamente para `int numero = 10`.

#### 1.5.3.2 Múltiplos Parâmetros

Rotas podem conter mais de um parâmetro, desde que a ordem seja respeitada.

    ```csharp
    app.MapGet("/rangos/{numero}/{nome}", (int numero, string nome) => ... );
    ```

Chamada:  
`/rangos/25/Feijoada`  
Resultado:  
`Feijoada 25`

#### 1.5.3.3 Rotas Estáticas vs. Dinâmicas

Rotas estáticas não possuem parâmetros e sempre retornam o mesmo conteúdo.

    ```csharp
    app.MapGet("/rangos", () => "Está funcionando MUITO bem!!!");
    ```

Rotas dinâmicas dependem dos valores enviados pelo cliente.

### 1.5.4 Exemplos Práticos

#### 1.5.4.1 Retornando um número enviado na URL

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

#### 1.5.4.2 Combinando parâmetros numéricos e textuais

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

#### 1.5.4.3 Utilizando parâmetros para consultar o banco

    ```csharp
    app.MapGet("/rangos/db/{id}", async (int id, RangoDbContext db) =>
    {
        var rango = await db.Rangos.FindAsync(id);
        return rango is null ? Results.NotFound() : Results.Ok(rango);
    });
    ```

### 1.5.5 Comparação: Parâmetros de Rota vs. Query String

| Critério | Parâmetros de Rota | Query String |
| --------- | --------------------- | -------------- |
| Forma | `/rangos/10` | `/rangos?id=10` |
| Semântica | Identificação direta do recurso | Filtros e modificadores |
| Uso ideal | Acesso a um item específico | Paginação, filtros, ordenação |

### 1.5.6 Boas Práticas

* Utilizar parâmetros de rota para identificar recursos únicos.
* Evitar rotas ambíguas que possam conflitar entre si.
* Manter nomes de parâmetros claros e consistentes.
* Preferir tipos primitivos simples para facilitar o *binding*.
* Validar valores recebidos quando necessário.

### 1.5.7 Conclusão

O uso de parâmetros de rota em Minimal APIs permite criar endpoints expressivos, simples e eficientes. A conversão automática de tipos e a clareza na definição das rotas tornam o desenvolvimento mais direto e menos propenso a erros, especialmente em APIs enxutas e de alta performance.

## 1.6 Conteúdo do BD

### 1.6.1 Introdução

Esta seção apresenta como expor o conteúdo do banco de dados por meio de endpoints em uma Minimal API. O foco está na leitura de dados utilizando Entity Framework Core, retornando tanto coleções quanto registros individuais.

A abordagem utiliza *dependency injection* para disponibilizar o `RangoDbContext` diretamente nos manipuladores das rotas, permitindo consultas simples e eficientes.

### 1.6.2 Configuração Inicial

A aplicação registra o `DbContext` com SQLite e define rotas que retornam dados persistidos no banco. O objetivo é demonstrar como recuperar registros completos ou filtrados por ID.

#### 1.6.2.1 Código de Configuração

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

### 1.6.3 Explicação da Sintaxe

#### 1.6.3.1 Injeção do DbContext

O ASP.NET Core injeta automaticamente o `RangoDbContext` no manipulador da rota:

    ```csharp
    (RangoDbContext rangoDbContext, int id)
    ```

Isso permite acessar o banco sem criar instâncias manualmente.

#### 1.6.3.2 Consulta por ID

O método `FirstOrDefault` retorna o primeiro registro que atende ao predicado ou `null` caso não exista.

    ```csharp
    rangoDbContext.Rangos.FirstOrDefault(x => x.Id == id);
    ```

#### 1.6.3.3 Retorno de Coleção

Ao retornar `rangoDbContext.Rangos`, o EF Core expõe um `DbSet<Rango>`, que será serializado automaticamente para JSON.

### 1.6.4 Exemplos Práticos

#### 1.6.4.1 Consultando um item específico

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

#### 1.6.4.2 Listando todos os itens de uma tabela

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

#### 1.6.4.3 Incluindo relacionamentos

    ```csharp
    app.MapGet("/rangos-com-ingredientes", async (RangoDbContext db) =>
    {
        return await db.Rangos
            .Include(r => r.Ingredientes)
            .ToListAsync();
    });
    ```

### 1.6.5 Comparação: `Find`, `FirstOrDefault` e `SingleOrDefault`

| Método | Comportamento | Uso Ideal |
| -------- | ---------------- | ----------- |
| `Find` | Busca por chave primária, usa cache | Consultas simples por ID |
| `FirstOrDefault` | Retorna o primeiro que atende ao predicado | Filtros flexíveis |
| `SingleOrDefault` | Exige que exista no máximo um registro | Garantia de unicidade |

### 1.6.6 Boas Práticas

* Utilizar `FindAsync` para buscas por chave primária.
* Retornar `NotFound` quando o registro não existir.
* Evitar expor `DbSet` diretamente em cenários complexos; preferir `ToListAsync`.
* Utilizar `Include` apenas quando necessário para evitar sobrecarga.
* Manter rotas claras e sem ambiguidade.

### 1.6.7 Conclusão

A leitura de dados em Minimal APIs com Entity Framework Core é direta e eficiente. A injeção automática do contexto, combinada com métodos de consulta simples, permite construir endpoints limpos e performáticos, adequados tanto para protótipos quanto para aplicações reais.

## 1.7 Tipos de Parâmetros

### 1.7.1 Introdução

Esta seção apresenta o uso de diferentes tipos de parâmetros em rotas de Minimal APIs, destacando como o ASP.NET Core realiza *route matching* quando múltiplas rotas semelhantes coexistem. O foco está na utilização de *route constraints* para diferenciar rotas que poderiam entrar em conflito, garantindo previsibilidade e segurança no processamento das requisições.

### 1.7.2 Configuração Inicial

A aplicação registra o `RangoDbContext` e define rotas que utilizam parâmetros tipados. O objetivo é demonstrar como o tipo do parâmetro influencia a seleção da rota correta.

#### 1.7.2.1 Código de Configuração

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

### 1.7.3 Explicação da Sintaxe

#### 1.7.3.1 Route Constraints

A expressão `{id:int}` define que o parâmetro só será aceito se puder ser convertido para `int`.  
Isso evita conflitos com a rota `{nome}`, que aceita qualquer texto.

    ```csharp
    /rango/10     → corresponde a {id:int}
    /rango/feijoada → corresponde a {nome}
    ```

#### 1.7.3.2 Parâmetros Sem Restrição

A rota:

    ```csharp
    /rango/{nome}
    ```

aceita qualquer valor que não seja reconhecido como inteiro, permitindo buscas textuais.

#### 1.7.3.3 Ordem de Resolução

O ASP.NET Core tenta casar rotas da mais específica para a mais genérica.  
A presença de `{id:int}` torna a rota numérica mais específica, evitando ambiguidade.

### 1.7.4 Exemplos Práticos

#### 1.7.4.1 Rota com parâmetro inteiro

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

#### 1.7.4.2 Rota com parâmetro textual

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

#### 1.7.4.3 Combinando constraints adicionais

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

### 1.7.5 Comparação: Parâmetros Tipados vs. Não Tipados

| Critério | Tipados (`{id:int}`) | Não Tipados (`{nome}`) |
| --------- | ------------------------ | -------------------------- |
| Validação | Automática | Manual |
| Previsibilidade | Alta | Média |
| Risco de conflito | Baixo | Alto |
| Uso ideal | Identificadores numéricos | Nomes, códigos, textos |

### 1.7.6 Boas Práticas

* Utilizar *route constraints* sempre que houver possibilidade de conflito entre rotas.
* Evitar rotas excessivamente genéricas que possam capturar valores inesperados.
* Manter nomes de parâmetros coerentes com o tipo esperado.
* Preferir parâmetros tipados para garantir validação automática.
* Documentar claramente o comportamento de rotas semelhantes.

### 1.7.7 Conclusão

O uso de tipos de parâmetros e *route constraints* em Minimal APIs permite criar rotas mais seguras, previsíveis e organizadas. A diferenciação entre rotas numéricas e textuais evita ambiguidades e melhora a clareza da API, especialmente em cenários onde múltiplos padrões de URL coexistem.

## 1.8 Parameter Binding

### 1.8.1 Introdução

Esta seção apresenta os diferentes mecanismos de *parameter binding* disponíveis em Minimal APIs. O ASP.NET Core permite extrair valores de diversas origens da requisição — como cabeçalhos, query string e rota — utilizando atributos específicos. Isso torna o código mais explícito, previsível e fácil de manter.

O exemplo demonstra como recuperar parâmetros a partir de *headers*, *query strings* e rotas, além de como combinar esses mecanismos com Entity Framework Core para consultar o banco de dados.

### 1.8.2 Código de Configuração

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

### 1.8.3 Explicação da Sintaxe

#### 1.8.3.1 `[FromHeader]`

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

#### 1.8.3.2 `[FromQuery]`

O atributo `[FromQuery]` força a leitura do valor a partir da query string.

    ```csharp
    [FromQuery] int id
    ```

Chamada:

    ```
    /rango/Query?id=4
    ```

#### 1.8.3.3 Parâmetros de Rota

A rota:

    ```csharp
    /rango/{nome}
    ```

captura o valor diretamente da URL, sem necessidade de atributos adicionais.

Chamada:

    ```
    /rango/Rendang
    ```

### 1.8.4 Exemplos Práticos

#### 1.8.4.1 Recebendo valores de cabeçalho

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

#### 1.8.4.2 Recebendo valores de query string

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

#### 1.8.4.3 Combinando rota e query string

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

### 1.8.5 Comparação entre Origens de Parâmetros

| Origem | Exemplo | Uso Ideal |
| -------- | --------- | ----------- |
| Header | `RangoId: 3` | Metadados, autenticação, identificação técnica |
| Query String | `/rango/Query?id=4` | Filtros, paginação, parâmetros opcionais |
| Rota | `/rango/Rendang` | Identificação direta do recurso |

### 1.8.6 Boas Práticas

* Utilizar `[FromHeader]` apenas para informações que fazem sentido como metadados.
* Preferir `[FromQuery]` para filtros e parâmetros opcionais.
* Utilizar parâmetros de rota para identificar recursos principais.
* Evitar misturar muitas origens de parâmetros no mesmo endpoint.
* Documentar claramente quais valores devem ser enviados em cada origem.

### 1.8.7 Conclusão

O *parameter binding* em Minimal APIs é flexível e poderoso, permitindo extrair valores de diferentes partes da requisição de forma clara e tipada. A utilização de atributos como `[FromHeader]` e `[FromQuery]` torna o comportamento explícito e reduz ambiguidades, contribuindo para APIs mais robustas e previsíveis.

## 1.9 Async Await

### 1.9.1 Introdução

Esta seção apresenta o uso de operações assíncronas em Minimal APIs utilizando `async` e `await`. O Entity Framework Core oferece métodos assíncronos para consultas ao banco de dados, permitindo que a aplicação escale melhor sob carga, evitando bloqueios de threads e melhorando a responsividade.

O objetivo é demonstrar como implementar endpoints assíncronos que consultam registros individuais e coleções completas.

### 1.9.2 Código de Configuração

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

### 1.9.3 Explicação da Sintaxe

#### 1.9.3.1 Métodos Assíncronos do EF Core

O Entity Framework Core disponibiliza versões assíncronas dos principais métodos de consulta:

* `FirstOrDefaultAsync`
* `SingleOrDefaultAsync`
* `FindAsync`
* `ToListAsync`

Esses métodos retornam `Task<T>`, permitindo o uso de `await`.

#### 1.9.3.2 Uso de `async` e `await`

A assinatura do endpoint deve ser marcada como assíncrona:

    ```csharp
    async (RangoDbContext db, int id) => { ... }
    ```

O operador `await` suspende a execução até que a operação seja concluída, liberando a thread para outras requisições.

#### 1.9.3.3 Benefícios do Modelo Assíncrono

* Melhor escalabilidade sob alta carga.
* Evita bloqueio de threads do servidor.
* Integração nativa com EF Core e ASP.NET Core.

### 1.9.4 Exemplos Práticos

#### 1.9.4.1 Consultando um registro por ID

    ```csharp
    app.MapGet("/ingrediente", async (RangoDbContext db, [FromQuery] int id) =>
    {
        var ingrediente = await db.Ingredientes.FirstOrDefaultAsync(x => x.Id == id);
        return ingrediente is null ? Results.NotFound() : Results.Ok(ingrediente);
    });
    ```

#### 1.9.4.2 Listando registros com filtro

    ```csharp
    app.MapGet("/rangos/filtro", async (RangoDbContext db, [FromQuery] string termo) =>
    {
        return await db.Rangos
            .Where(r => r.Nome.Contains(termo))
            .ToListAsync();
    });
    ```

#### 1.9.4.3 Incluindo relacionamentos de forma assíncrona

    ```csharp
    app.MapGet("/rangos-com-ingredientes", async (RangoDbContext db) =>
    {
        return await db.Rangos
            .Include(r => r.Ingredientes)
            .ToListAsync();
    });
    ```

### 1.9.5 Comparação: Métodos Síncronos vs. Assíncronos

| Critério | Síncrono | Assíncrono |
| --------- | ---------- | ------------ |
| Bloqueio de thread | Sim | Não |
| Escalabilidade | Menor | Maior |
| Uso ideal | Scripts, testes simples | APIs em produção |
| Integração com EF Core | Completa | Completa |

### 1.9.6 Boas Práticas

* Utilizar sempre métodos assíncronos em APIs reais.
* Evitar misturar métodos síncronos e assíncronos no mesmo fluxo.
* Retornar `NotFound` quando o registro não existir.
* Utilizar `ToListAsync` antes de retornar coleções.
* Manter nomes de rotas e parâmetros consistentes.

### 1.9.7 Conclusão

O uso de `async` e `await` em Minimal APIs é essencial para construir aplicações modernas, escaláveis e eficientes. A integração com o Entity Framework Core facilita a implementação de consultas assíncronas, garantindo melhor desempenho e aproveitamento dos recursos do servidor.

## 1.10 HTTP Results

### 1.10.1 Introdução

Esta seção apresenta o uso de *HTTP Results* tipados em Minimal APIs. O ASP.NET Core fornece o namespace `TypedResults`, que permite retornar respostas HTTP fortemente tipadas, aumentando a clareza, previsibilidade e segurança dos endpoints.

O objetivo é demonstrar como retornar diferentes tipos de respostas — como `Ok`, `NoContent`, `NotFound` — utilizando *result types* combinados com `Results<T1, T2>`.

### 1.10.2 Código de Configuração

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

### 1.10.3 Explicação da Sintaxe

#### 1.10.3.1 `Results<T1, T2>`

O tipo `Results<T1, T2>` define explicitamente quais respostas o endpoint pode retornar.  
No exemplo:

    ```csharp
    Results<NoContent, Ok<List<Rango>>>
    ```

O endpoint pode retornar:

* `204 No Content`
* `200 OK` com uma lista de `Rango`

Isso melhora a documentação automática e auxilia ferramentas como Swagger.

#### 1.10.3.2 `TypedResults.Ok`

Retorna uma resposta HTTP 200 com o corpo especificado.

    ```csharp
    TypedResults.Ok(rangosEntity);
    ```

#### 1.10.3.3 `TypedResults.NoContent`

Retorna uma resposta HTTP 204 sem corpo.

    ```csharp
    TypedResults.NoContent();
    ```

#### 1.10.3.4 Consulta com Filtro

O uso de:

    ```csharp
    .Where(x => x.Nome.Contains(rangoNome!))
    ```

permite filtrar resultados com base no nome enviado via query string.

### 1.10.4 Exemplos Práticos

#### 1.10.4.1 Endpoint com múltiplos resultados possíveis

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

#### 1.10.4.2 Retornando `Created` com TypedResults

    ```csharp
    app.MapPost("/rango",
        async Task<Created<Rango>> (RangoDbContext db, Rango novoRango) =>
    {
        db.Rangos.Add(novoRango);
        await db.SaveChangesAsync();

        return TypedResults.Created($"/rango/{novoRango.Id}", novoRango);
    });
    ```

#### 1.10.4.3 Retornando `BadRequest`

    ```csharp
    app.MapGet("/buscar",
        (string? termo) =>
    {
        if (string.IsNullOrWhiteSpace(termo))
            return TypedResults.BadRequest("O parâmetro 'termo' é obrigatório.");

        return TypedResults.Ok($"Termo recebido: {termo}");
    });
    ```

### 1.10.5 Comparação: `IResult` vs. `TypedResults`

| Critério | `IResult` | `TypedResults` |
| --------- | ----------- | ---------------- |
| Tipagem | Genérica | Fortemente tipada |
| Documentação automática | Limitada | Detalhada |
| Previsibilidade | Média | Alta |
| Uso ideal | Endpoints simples | APIs robustas e documentadas |

### 1.10.6 Boas Práticas

* Utilizar `TypedResults` para endpoints que retornam múltiplos tipos de resposta.
* Evitar retornar objetos nulos; preferir `NotFound` ou `NoContent`.
* Manter consistência nos tipos retornados por cada rota.
* Utilizar `Results<T1, T2>` para clareza e documentação automática.
* Validar parâmetros antes de consultar o banco.

### 1.10.7 Conclusão

O uso de *HTTP Results* tipados em Minimal APIs melhora a robustez e a clareza da API, permitindo respostas mais explícitas e previsíveis. A combinação de `TypedResults` com `Results<T1, T2>` fornece uma abordagem moderna e segura para lidar com múltiplos cenários de retorno, tornando a API mais confiável e bem estruturada.

## 1.11 Organizando Código

### 1.11.1 Introdução

Esta seção aborda estratégias para organizar o código em Minimal APIs, mantendo clareza, separação de responsabilidades e facilidade de manutenção. À medida que a API cresce, torna‑se essencial estruturar endpoints, regras de negócio e consultas ao banco de dados de forma modular e previsível.

O exemplo apresentado demonstra como aplicar filtros opcionais, retornar resultados tipados e manter o endpoint enxuto e legível.

### 1.11.2 Código do Endpoint Organizado

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

### 1.11.3 Explicação da Sintaxe

#### 1.11.3.1 Filtro Opcional

A expressão:

    ```csharp
    rangoNome == null || x.Nome.Contains(rangoNome!)
    ```

permite que o endpoint funcione tanto com quanto sem filtro.  

* Se `rangoNome` for `null`, todos os registros são retornados.  
* Se houver valor, aplica‑se o filtro `Contains`.

### 1.11.5 Conclusão

Organizar o código em Minimal APIs é fundamental para manter a aplicação sustentável à medida que cresce. A separação entre lógica de consulta, validação e retorno, combinada com o uso de métodos auxiliares e extensões, resulta em uma API mais limpa, modular e fácil de evoluir.

## 1.12 AutoMapper e DTOs

### 1.12.1 Introdução

Esta seção apresenta o uso de **AutoMapper** e **DTOs (Data Transfer Objects)** em Minimal APIs. O objetivo é separar as entidades do domínio dos modelos expostos pela API, garantindo segurança, clareza e controle sobre os dados retornados ao cliente.

O AutoMapper automatiza o processo de conversão entre entidades e DTOs, reduzindo código repetitivo e evitando exposição indevida de propriedades internas.

### 1.12.2 Configuração Inicial

A aplicação registra o `DbContext`, adiciona o AutoMapper e define endpoints que retornam DTOs mapeados a partir das entidades.

#### 1.12.2.1 Código de Configuração

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

### 1.12.3 Perfis de Mapeamento

O AutoMapper utiliza perfis para definir como entidades são convertidas em DTOs.

#### 1.12.3.1 Código do Profile

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

### 1.12.4 DTOs Utilizados

#### 1.12.4.1 RangoDTO

    ```csharp
    namespace RangoAgil.API.Models;

    public class RangoDTO
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
    }
    ```

#### 1.12.4.2 IngredienteDTO

    ```csharp
    namespace RangoAgil.API.Models;

    public class IngredienteDTO
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public int RangoId { get; set; }
    }
    ```

### 1.12.5 Explicação da Sintaxe

#### 1.12.5.1 `CreateMap<TSource, TDestination>()`

Define o mapeamento entre entidade e DTO.  
O uso de `.ReverseMap()` permite converter nos dois sentidos.

#### 1.12.5.2 `ForMember`

Permite personalizar o mapeamento de propriedades específicas.

    ```csharp
    .ForMember(
        d => d.RangoId,
        o => o.MapFrom(s => s.Rangos.First().Id)
    )
    ```

Essa configuração extrai o primeiro `Rango` associado ao ingrediente e usa seu ID no DTO.

#### 1.12.5.3 Injeção de `IMapper`

O AutoMapper é injetado diretamente no endpoint:

    ```csharp
    (RangoDbContext db, IMapper mapper, int id)
    ```

### 1.12.6 Exemplos Práticos

#### 1.12.6.1 Retornando um DTO de Rango

    ```csharp
    app.MapGet("/rango-dto/{id:int}", async (RangoDbContext db, IMapper mapper, int id) =>
    {
        var entity = await db.Rangos.FindAsync(id);
        return entity is null ? Results.NotFound() : Results.Ok(mapper.Map<RangoDTO>(entity));
    });
    ```

#### 1.12.6.2 Retornando Ingredientes com DTOs

    ```csharp
    app.MapGet("/ingredientes-dto", async (RangoDbContext db, IMapper mapper) =>
    {
        var ingredientes = await db.Ingredientes.Include(i => i.Rangos).ToListAsync();
        return mapper.Map<IEnumerable<IngredienteDTO>>(ingredientes);
    });
    ```

### 1.12.7 Comparação: Entidades vs. DTOs

| Critério | Entidades | DTOs |
| --------- | ----------- | ------ |
| Exposição de dados | Completa | Controlada |
| Acoplamento | Alto | Baixo |
| Segurança | Menor | Maior |
| Uso ideal | Persistência | Comunicação com o cliente |

### 1.12.8 Boas Práticas

* Nunca expor entidades diretamente em APIs públicas.
* Utilizar DTOs para controlar exatamente o que é retornado.
* Centralizar mapeamentos em perfis AutoMapper.
* Evitar lógica complexa dentro de DTOs.
* Validar dados antes de mapear para entidades.

### 1.12.9 Conclusão

O uso de AutoMapper e DTOs em Minimal APIs melhora a organização, segurança e clareza da aplicação. A separação entre entidades e modelos de transferência evita exposição indevida de dados e facilita a evolução da API, mantendo o código limpo e sustentável.
