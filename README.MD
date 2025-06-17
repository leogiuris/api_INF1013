

### Pré-requisitos

* **[.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)**
* **[Docker Desktop](https://www.docker.com/products/docker-desktop/)**

---

### Configuração do Banco de Dados

1. **Inicie os Containers Docker**: Navegue até o diretório raiz do projeto (onde o arquivo `docker-compose.yml` está localizado) e inicie os containers do banco de dados e do phpMyAdmin:

   ```bash
   docker-compose up -d
   ```

   Esse comando irá:

   * Criar um container MySQL chamado `ModelagemAPI-mysql`.
   * Inicializar o banco de dados `ModelagemAPI_db` usando o arquivo `schema_ModelagemAPI.sql`.
   * Iniciar um container do phpMyAdmin chamado `ModelagemAPI-pma` para gerenciamento do banco (acessível em `http://localhost:8080`).

2. **Aplicar Migrações do Banco de Dados**: O arquivo `schema_ModelagemAPI.sql` inicializa o banco, mas também são utilizadas migrações do Entity Framework Core para gerenciar alterações no esquema. Execute os comandos abaixo para garantir que o esquema do banco esteja atualizado com os modelos da aplicação:

   ```bash
   dotnet ef migrations add InitialCreate --project ModelagemAPI.csproj
   dotnet ef database update --project ModelagemAPI.csproj
   ```


---

### Executando a Aplicação

Após configurar o banco de dados, você pode iniciar o servidor da API .NET:

Navegue até o diretório raiz do projeto (`API_MODELAGEM`) e execute:

```bash
dotnet run --project ModelagemAPI.csproj
```

O servidor normalmente será iniciado em `http://localhost:5270` (isso pode ser configurado em `Properties/launchSettings.json`).

---

### Testando e Acessando a API

Você pode interagir com a API de diversas formas:

* **Swagger UI**: Abra o navegador e vá até `http://localhost:5270/swagger`. Isso fornece uma documentação interativa dos endpoints da API, permitindo enviar requisições e visualizar as respostas diretamente.

* **Arquivo HTTP**: Use o arquivo `ModelagemAPI.http` localizado na raiz do projeto. A maioria das IDEs modernas (como o VS Code com a extensão "REST Client") permite executar requisições diretamente a partir desse arquivo. Ele contém exemplos para todas as operações CRUD.

  * **Exemplo de GET para listar todas as entidades**:

    ```http
    GET {{API_HostAddress}}/api/Entities/
    Accept: application/json
    ```

* **Chamadas Diretas à API**: Use ferramentas como Postman, curl ou sua própria aplicação cliente para enviar requisições HTTP aos endpoints da API (por exemplo, `http://localhost:5270/api/Entities`).

---
