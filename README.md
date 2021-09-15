Definições
Usar padrão REST para rotas, exemplo: empresa-parceira
Rotas delete devem ter o id na url da requisição

### Getting start docker
Depois do repositorio clonado, rode o comando, na raiz do projeto, para criar imagem:
```sh
$ docker build -t {nome-container} .
```

Depois cria container:
```sh
$ docker run -d -p {porta-local}:80 --name {nome-imagem} {nome-container} .
```

Para acessar  swagger, acessar http://localhost:{porta-local}/swagger/index.html