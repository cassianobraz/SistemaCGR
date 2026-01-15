# 📊 Sistema de controle de gastos residenciais

Projeto backend desenvolvido com foco em **boas práticas**, **regras de negócio bem definidas** e **testes unitários consistentes**.  
O sistema permite o controle de pessoas, categorias e transações financeiras, além de consultas consolidadas de totais.

---

## 🧠 Visão Geral

O sistema oferece:

- Cadastro de **Pessoas**
- Cadastro de **Categorias**
- Cadastro de **Transações financeiras** (Receitas e Despesas)
- Consulta de **totais por pessoa**
- Consulta de **totais por categoria**
- Validações de domínio centralizadas
- Testes unitários cobrindo os principais fluxos de negócio

O foco principal do projeto é **qualidade de código**, **organização** e **clareza das regras**, simulando cenários reais de backend.

---

## 🏗️ Arquitetura

O projeto segue uma separação clara de camadas, inspirada em princípios de Clean Architecture, facilitando manutenção, testes e evolução do sistema.

```text
src/
├── SistemaControle.Api
├── SistemaControle.Application
├── SistemaControle.Domain
└── SistemaControle.Infra

tests/
└── SistemaControleTests
    └── Unit
        └── Domain
            └── Services
```

### 📌 Responsabilidade das Camadas

- **Api**
  - Controllers
  - Recebe e responde requisições HTTP
  - Não contém regras de negócio

- **Application**
  - Handlers (MediatR)
  - DTOs
  - Mapeamento de dados
  - Orquestra a comunicação entre API e Domínio

- **Domain**
  - Entidades
  - Serviços de domínio
  - Regras de negócio
  - Contratos (interfaces)

- **Infra**
  - Repositórios
  - Entity Framework Core
  - Acesso ao banco de dados
  - Unit of Work

---

## 📚 Regras de Negócio

### 👤 Pessoa
- Nome é obrigatório
- Idade deve estar entre **1 e 120**
- Só é possível excluir uma pessoa existente

---

### 🗂️ Categoria
- Descrição obrigatória
- Finalidade válida:
  - `Despesa`
  - `Receita`
  - `Ambas`

---

### 💰 Transações

As regras abaixo são aplicadas exclusivamente no **serviço de domínio**:

- Descrição obrigatória
- Valor maior que zero
- Tipo da transação deve ser válido
- Categoria deve existir
- Pessoa deve existir
- **Pessoa menor de 18 anos só pode cadastrar despesas**
- Categoria deve aceitar o tipo da transação:
  - Categoria `Despesa` → apenas despesas
  - Categoria `Receita` → apenas receitas
  - Categoria `Ambas` → aceita ambos

---

## 📊 Consultas

### 🔹 Totais por Pessoa
Retorna:
- Lista de pessoas com:
  - Total de receitas
  - Total de despesas
  - Saldo (receita - despesa)
- Totais gerais:
  - Total de receitas
  - Total de despesas
  - Saldo geral

---

### 🔹 Totais por Categoria
Retorna:
- Lista de categorias com:
  - Total de receitas
  - Total de despesas
  - Saldo
- Totais gerais consolidados

---

## 🧪 Testes Unitários

Os testes foram desenvolvidos utilizando **xUnit** e **Moq**, focando diretamente nas **regras de negócio**.

### Destaques dos testes:
- Testes unitários isolados por serviço de domínio
- Uso de `MockBehavior.Strict` para evitar chamadas indevidas
- Padrão claro **Arrange / Act / Assert**
- Cobertura de cenários de erro e sucesso
- Validação de regras condicionais (idade, finalidade, tipo de transação)

### 📈 Cobertura
- **96% de cobertura de linhas**
- Mais de **500 linhas cobertas**
- Todos os fluxos críticos do domínio estão testados

Esse nível de cobertura garante maior segurança para evolução do código e refatorações futuras.

---

## 🚀 Tecnologias Utilizadas

- **.NET 8**
- **C#**
- **ASP.NET Core**
- **Entity Framework Core**
- **PostgreSQL**
- **MediatR**
- **xUnit**
- **Moq**

---

## 🎯 Objetivo do Projeto

Este projeto foi desenvolvido com o objetivo de:

- Consolidar fundamentos de **backend com .NET**
- Aplicar **regras de negócio no domínio**
- Praticar **testes unitários com foco em qualidade**
- Demonstrar organização, clareza e responsabilidade de código

Mesmo sendo um projeto simples, a preocupação principal foi desenvolver algo **bem estruturado**, **testável** e **fácil de manter**.

---

## 📌 Considerações Finais

O projeto prioriza **clareza e consistência**, simulando situações comuns do dia a dia de um backend.  
Todas as decisões foram pensadas para facilitar manutenção, testes e evolução futura.

Fico à disposição para explicar qualquer parte da implementação ou discutir possíveis melhorias.
