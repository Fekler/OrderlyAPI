
# Sales Order Management API

API desenvolvida com o objetivo de gerenciar pedidos de clientes, produtos, usuários e fornecer visão gerencial sobre vendas.

📡 A API está publicada em:  
**📖 https://sales-dev-api.fekler.tec.br/swagger/index.html** - Documentação


A Api esta documentada utilizando o padrão OpenAPI e Swagger para exibição visual.

---

## 🔧 Tecnologias Utilizadas

- **.NET 9**
- **C# 12**
- **Entity Framework Core**
- **Migrations (Code First)**
- **Mapster (DTO Mapping)**
- **JWT Authentication**
- **Docker**
- **PostgresSQL**
- **GitHub Actions (CI/CD)**
- **Configuração Nginx**

---

## 🧱 Arquitetura do Projeto

A aplicação está organizada seguindo **Clean Architecture**, com divisão clara de responsabilidades:

```
📁 Domain
📁 Application
📁 Infrastructure
📁 SharedKernel
📁 WebApi
```

- **Domain**: Entidades, enums, validações e regras de negócio puras.
- **Application**: DTOs, interfaces, casos de uso, regras de aplicação e mapeamento.
- **Infrastructure**: Repositórios e persistência.
- **SharedKernel**: Utilitários genéricos como `ApiResponse`, criptografia e enums reutilizáveis.
- **WebApi**: Controllers, endpoints e configuração de middlewares.

---

## 🔐 Autenticação e Autorização

- **JWT** com geração de tokens via `TokenBusiness`
- Perfis de usuários:
  - `Admin`: controle total, incluindo CRUD de usuários
  - `Seller`: gerenciamento de estoque e pedidos
  - `Client`: visualização de produtos e criação de pedidos

---

## 📦 Funcionalidades Implementadas

### Usuários
- Criação, edição, listagem e inativação
- Alteração de senha
- Login com verificação de senha criptografada

### Produtos
- Cadastro e atualização
- Listagem por categoria
- Controle de estoque

### Pedidos
- Criação de pedidos com múltiplos itens
- Cálculo automático de valor total
- Atualização de status (Pending, Approved, Cancelled etc.)

### Dashboard (Visão Gerencial)
- Total de pedidos e receita
- Produtos vendidos
- Pedidos pendentes
- Clientes mais ativos

---

## 📡 Endpoints REST

A seguir alguns dos principais endpoints disponíveis:

| Verbo | Rota | Descrição |
|-------|------|-----------|
| `POST` | `/api/auth/login` | Autentica e gera token JWT |
| `GET` | `/api/user` | Lista todos os usuários |
| `POST` | `/api/user` | Cria novo usuário |
| `POST` | `/api/products` | Cadastra produto |
| `GET` | `/api/products` | Lista produtos |
| `POST` | `/api/orders` | Cria pedido com múltiplos itens |
| `GET` | `/api/orders/user/{uuid}` | Lista pedidos de um usuário |
| `GET` | `/api/dashboard/summary` | Exibe dados resumidos de vendas |

---

## 🧪 Testes

- Cobertura de testes unitários para regras de domínio e casos de uso
- Testes de integração simulando requisições REST
- Estrutura preparada para uso com xUnit
  
![Captura de tela 2025-05-16 104638](https://github.com/user-attachments/assets/cd05c7ed-c01e-4911-aefc-094833a9b017)

---

## 🚀 CI/CD

- **Build** automatizado via GitHub Actions
- **Dockerfile** com publicação 
- Deploy automático para ambiente remoto via GHCR(Git Hub Container Registry)

---
## ☁️ Cloud

- **Recursos** Consumindo VM.Standard.E2.1.Micro da Oracle Cloud no ambiente de produção e VM.Standard.A1.Flexshape para ambiente de desenvolvimento
- **Database** Consumindo banco PostgreSQL da neon.tech

---
## ⚙️ Executando o Projeto

### Pré-requisitos
- .NET 9 SDK
- Docker
- PostgresSQL

### Variáveis de Ambiente
Utilize um arquivo `.env` ou configure:
```env
TOKEN_JWT_SECRET=suachavesecreta
DATABASE_URL=...
```

### Executar Localmente
```bash
dotnet restore
dotnet ef database update
dotnet run --project SalesOrderManagement.WebApi
```

### Docker
Os comandos a seguir constroem e executam a imagem Docker da API. Certifique-se de ter o Docker instalado e em execução.
As variaveis de ambientes passadas são opcionais se houver o arquivo `.env` na raiz do projeto.

```bash
docker build -t sales-api .
docker run \
-e DATABASE_URL="Sua string de conexão banco postgreSQL" \
-e TOKEN_JWT_SECRET="sua chave secreta" \
-d -p 5000:80 sales-api
```

---

## 📁 Organização do Código

- **Padrões implementados**:
  - Clean Architecture
  - SOLID
  - DDD simplificado
  - Validações centralizadas (RuleValidator)
  - Mapster para mapeamentos
  - Code First com migrations
  - Repositórios genéricos
  - Business Rules e Use Cases
  - Responses genéricos e estruturados (`ApiResponse<T>` e `Response<T>`)

---

## 📬 Contato

> Desenvolvido por [Fekler](https://github.com/Fekler).
