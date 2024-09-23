Este é um pŕojeto de estudo/testes, com tema a criação de um blog utilizando as tecnologias .Net React e Postgres
Para rodar, vamos começar pelo banco de dados. No diretório `/fabarblog`, rode o seguinte comando:

```
dotnet ef database update
```

Em seguida, vamos subir a aplicação utilizando o `docker compose`. No diretório `/`, rodar o seguinte comando:

```
compose up --build -d
```

Em tese, basta isto para o funcionamento do projeto.
A aplicação ficará roando em dois endereços:

- http://localhost:3000 (Frontend)
- http://localhost:5000 (Backend)

---

Os EPs disponibilizados neste projeto são:

### Autenticação

- [POST] Autenticar usuário: http://localhost:5000/api/auth

### Usuários

- [POST] Criar usuário: http://localhost:5000/api/user
- [GET] Listar usuários: http://localhost:5000/api/user
- [GET] Listar um único usuário: http://localhost:5000/api/user/1
- [PUT] Editar um usuário: http://localhost:5000/api/user/1
- [DELETE] Excluir um usuário: http://localhost:5000/api/user/1

### Posts

- [POST] Criar post: http://localhost:5000/api/post
- [GET] Listar posts: http://localhost:5000/api/post
- [GET] Listar um único post: http://localhost:5000/api/post/1
- [PUT] Editar um post: http://localhost:5000/api/post/1
- [DELETE] Excluir um post: http://localhost:5000/api/post/1

Se você utiliza Insomnia, deixarei o arquivo `requests-blog.json` com os modelos das requisições.
Caso contrário, em `requests-curl.txt`, estão as requisições em formato cUrl.
