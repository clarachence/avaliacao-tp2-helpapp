# 📘 Avaliação Técnica – Clean Architecture + Azure SQL

Este repositório contém minha entrega referente à avaliação técnica baseada no repositório original do professor:  
[https://github.com/victoricoma/avaliacao-tp2-helpapp](https://github.com/victoricoma/avaliacao-tp2-helpapp)

---

## ✅ Objetivo

Implementar os repositórios `Category` e `Product` seguindo os padrões da Clean Architecture, aplicar a migration `Initial` e conectar a aplicação com uma instância de SQL Server no Azure.

---

## 🚀 Funcionalidades implementadas

- [x] Repositórios `CategoryRepository` e `ProductRepository`
- [x] Configurações com `EntityTypeConfiguration` para `Category` e `Product`
- [x] Injeção de dependência configurada (`DependencyInjectionAPI`)
- [x] Migration `Initial` criada com `HasData()` para categorias
- [x] Banco de dados SQL Server criado no Azure
- [x] Migration aplicada com sucesso no Azure via `dotnet ef database update`

---
# 🔧 Comandos utilizados
## Criação da migration
dotnet ef migrations add Initial --project Infra.Data --startup-project WebAPI

## Aplicação no banco de dados (Azure)
dotnet ef database update --project Infra.Data --startup-project WebAPI


## Aplicação no banco de dados (Azure)
dotnet ef database update --project Infra.Data --startup-project WebAPI

# 🔗 String de conexão (mascarada)

"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:servidor-sql-aluno.database.windows.net,1433;Initial Catalog=NomeDoBanco;Persist Security Info=False;User ID=aluno_azure;Password=********;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}

# ☁️ Configuração no Azure
SQL Server criado no portal Azure

Banco de dados nomeado: avaliacao_tp2_aluno

IP local autorizado no firewall

Autenticação SQL ativada

Migration aplicada com sucesso diretamente do Visual Studio Terminal

# 🖼️ Prints de evidência (opcional)
Insira prints aqui comprovando:
![image](https://github.com/user-attachments/assets/83abb5ae-5542-4f60-8231-bfaa548ce018)
![image](https://github.com/user-attachments/assets/ae1ce169-d4a2-4f0b-8685-dfaa19e16b06)
![image](https://github.com/user-attachments/assets/276df780-6f5e-42c8-8e4d-30b1ca5afe7f)



Aplicação bem-sucedida da migration no Azure

Tabelas e dados populados

# 👨‍💻 Dados do aluno
Nome: Clara Domitila Chence
Curso: Desenvolvimento de Software Multiplataforma – 3º Semestre

Professor: Victor Icoma

Branch da entrega: avaliacao-clarachence

## 🧱 Estrutura da aplicação

```bash
📦 src
 ┣ 📂 Domain
 ┣ 📂 Application
 ┣ 📂 Infra
 ┃ ┣ 📂 Data
 ┃ ┃ ┣ 📂 Migrations
 ┃ ┃ ┣ 📂 Repositories
 ┃ ┃ ┗ 📂 EntityConfiguration
 ┗ 📂 WebAPI

