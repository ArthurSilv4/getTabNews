# API de Filtro do TabNews

Esta é uma API pública que retorna os posts mais recentes do TabNews cujo título começa com a palavra **"filtro"**. A ideia é facilitar o acesso a conteúdos filtrados diretamente da plataforma TabNews, por meio de uma interface simples e acessível.

## Objetivo

Criar uma API pública que consome a API oficial do TabNews, aplica um filtro personalizado e disponibiliza os resultados através de um endpoint HTTP.

## Como funciona

- A API faz uma requisição para a API do TabNews.
- Filtra os posts cujo título começa com a palavra **"filtro"**.
- Retorna uma lista com os posts filtrados

## URL pública da API

Acesse a interface web para testar a API:  
[https://arthursilv4.github.io/front-getTabNews/](https://arthursilv4.github.io/front-getTabNews/)

```bash
GET https://gettabnews.onrender.com/posts?filter={filtro}
```

## Exemplo de resposta

```json
[
  {
    "id": "1a2b3c4d-1111-2222-3333-444455556666",
    "owner_id": "abcd1234-5678-90ef-ghij-klmnopqrstuv",
    "parent_id": null,
    "slug": "filtro-de-dados-em-python",
    "title": "Filtro de dados em Python",
    "body": "Veja como filtrar dados facilmente em Python utilizando listas por compreensão e funções como filter().",
    "status": "published",
    "type": "content",
    "source_url": "https://www.tabnews.com.br/usuario/filtro-de-dados-em-python",
    "created_at": "2025-05-31T12:00:00Z",
    "updated_at": "2025-05-31T12:00:00Z",
    "published_at": "2025-05-31T12:00:00Z",
    "deleted_at": null,
    "owner_username": "usuario",
    "tabcoins": 5,
    "tabcoins_credit": 0,
    "tabcoins_debit": 0,
    "children_deep_count": 0
    },
    {
    "id": "2b3c4d5e-7777-8888-9999-aaaabbbbcccc",
    "owner_id": "efgh5678-1234-90ab-cdef-ghijklmnopqr",
    "parent_id": null,
    "slug": "filtro-de-informacoes-na-web",
    "title": "Filtro de informações na web",
    "body": "Dicas e técnicas para filtrar informações relevantes na web e evitar sobrecarga de dados.",
    "status": "published",
    "type": "content",
    "source_url": "https://www.tabnews.com.br/usuario/filtro-de-informacoes-na-web",
    "created_at": "2025-05-30T18:00:00Z",
    "updated_at": "2025-05-30T18:00:00Z",
    "published_at": "2025-05-30T18:00:00Z",
    "deleted_at": null,
    "owner_username": "usuario",
    "tabcoins": 3,
    "tabcoins_credit": 0,
    "tabcoins_debit": 0,
    "children_deep_count": 0
    }
]
```

## Tecnologias utilizadas

- Minimal API C# ASP.NET Core
- API pública do TabNews
- Hospedagem Render

## Como rodar localmente

Clone o repositório:

```bash
git clone https://github.com/ArthurSilv4/getTabNews.git
cd getTabNews
```

Restaure as dependências e execute localmente com .NET:

```bash
dotnet restore
dotnet run
```

Acesse a documentação Swagger em:

```bash
http://localhost:5000/swagger
```

## Possíveis melhorias

- Cache para evitar múltiplas chamadas à API do TabNews

## Licença

MIT
