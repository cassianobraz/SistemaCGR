# Sistema de Controle Financeiro – Frontend

Este repositório contém o frontend de um Sistema de Controle Financeiro, desenvolvido em React + TypeScript, com foco em organização por domínio (feature-based), clareza de responsabilidades e aderência às regras de negócio propostas no desafio.

A aplicação consome uma API REST e disponibiliza funcionalidades para gerenciamento de Pessoas, Categorias e Transações, além de consultas consolidadas de totais.

## Tecnologias Utilizadas

- React + TypeScript
- Vite
- React Router
- @tanstack/react-query
- Tailwind CSS (via @tailwindcss/vite)

## Estrutura do Projeto

A arquitetura é organizada por *features*, separando responsabilidades por domínio funcional, o que facilita manutenção, escalabilidade e entendimento do código.

```
src/
├── app/
│ ├── query/ # Configuração e composição do React Query
│ ├── theme/ # Controle de tema (claro/escuro)
│ ├── App.tsx
│ ├── ErrorPage.tsx
│ ├── providers.tsx # Providers globais (Query, Theme, Router)
│ └── router.tsx # Definição das rotas
│
├── components/
│ └── ui/ # Componentes reutilizáveis (Button, Modal, Table, Field, Card, etc.)
│
├── features/
│ ├── categoria/
│ ├── pessoa/
│ └── transacao/
│
├── lib/
│ ├── http.ts # Cliente HTTP + tratamento padronizado de erros
│ └── format.ts # Utilitários (ex.: formatação monetária)
│
├── index.css
└── main.tsx
```

Cada *feature* concentra:
- Página principal
- Modal de criação
- Hooks de acesso à API (React Query)
- Tipos e contratos de dados

## Funcionalidades Implementadas

### Cadastro de Pessoas

Funcionalidades:
- Criação, listagem e exclusão

Campos:
- Identificador único (gerado automaticamente)
- Nome (texto)
- Idade (inteiro positivo)

Regra de negócio:
- Ao excluir uma pessoa, todas as transações vinculadas são removidas no backend. O frontend reflete automaticamente o novo estado após a operação.

Consulta de totais por pessoa:
- Lista todas as pessoas exibindo total de receitas, despesas e saldo (receita – despesa)
- Exibe um total geral consolidado ao final da listagem

### Cadastro de Categorias

Funcionalidades:
- Criação e listagem

Campos:
- Identificador único
- Descrição (texto)
- Finalidade: despesa, receita ou ambas

Regras aplicadas no frontend:
- A descrição é normalizada antes do envio ao backend, garantindo que a primeira letra seja sempre maiúscula, independentemente da entrada do usuário.

Consulta de totais por categoria (opcional):
- Lista categorias exibindo total de receitas, despesas e saldo por categoria
- Exibe um total geral consolidado ao final

### Cadastro de Transações

Funcionalidades:
- Criação e listagem

Campos:
- Identificador único
- Descrição (texto)
- Valor (número decimal positivo)
- Tipo (despesa ou receita)
- Categoria (restrita conforme a finalidade)
- Pessoa (identificador)

Regras de negócio:
- Caso a pessoa selecionada seja menor de 18 anos, apenas transações do tipo despesa são permitidas.
  - O frontend ajusta o fluxo do formulário, bloqueando opções inválidas antes do envio.
- A seleção de categorias é filtrada conforme o tipo da transação:
  - Para despesas: apenas categorias com finalidade despesa ou ambas.
  - Para receitas: apenas categorias com finalidade receita ou ambas.

Todas as regras são validadas no frontend para melhorar a experiência do usuário e evitar envios inválidos, sendo também validadas no backend.

## Tema (Claro/Escuro)

A aplicação possui alternância entre tema claro e escuro, disponível na sidebar.
O tema selecionado é persistido no armazenamento local e aplicado via classe `dark` no `document.documentElement`.

## Configuração de Ambiente

O projeto utiliza variável de ambiente para definir a URL base da API.

Crie um arquivo `.env` na raiz do projeto com base no `.env.example`:
```VITE_API_BASE_URL=https://sua-api.com```

## Como Executar

Instalar dependências:
 ```pnpm i```
 
Rodar em desenvolvimento:
```pnpm dev```



## Observações Finais

- O projeto prioriza clareza de regras de negócio, separação de responsabilidades e consistência de UI.
- Validações de entrada e bloqueios de seleção são tratados no frontend para reduzir erros e melhorar a experiência do usuário.
- Testes automatizados não foram incluídos nesta entrega, mantendo o foco na implementação funcional e arquitetural solicitada no desafio.