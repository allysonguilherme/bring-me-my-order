# Bring Me My Order – Mini projeto com microserviços

### Uma Arquitetura em Microserviços com RabbitMQ
Dois serviços. Diversas tecnologias. Uma amostra de sistema de pedidos e controle de estoque dividida em dois serviços principais: 
- *InventoryService*: Um microserviço dedicado a manutenção do catálogo de produtos em estoque.
- *OrderService*: Um microserviço dedicado ao gerenciamento de pedidos.

Cada microserviço roda independentemente de outro microserviço, tendo seu próprio banco de dados dedicado. Porém, para que o sistema todo colabore entre si os dois serviços se comunicam através de mensageria utilizando os recursos do RabbitMQ.

InventoryService fornece uma API pela qual é possível listar os produtos cadastrados com suas respectivas quantidades em estoque, buscar um produto pelo seu ID, adicionar e diminuir quantidade do estoque.

OrderService fornece uma API para listar pedidos feitos, buscar pedidos pelo ID, fazer novos pedidos ou cancelar pedidos existentes. Quando um novo pedido é realizado este serviço publica um evento de criação do pedido em uma fila do RabbitMQ, esta mesma fila é consumida pelo InventoryService que ao receber o evento faz a retirada da quantidade pedida caso haja disponibilidade e atualiza a quantidade restante no estoque. Quando um pedido é cancelado OrderService publica um evento de cancelamento consumido pelo InventoryService que retorna a quantidade devolvida ao estoque.

### Cache de Produtos
Além da comunicação assíncrona entre os microserviços o projeto implementa um recurso usado em muitas aplicações modernas para ganho de performance e escalabilidade. O cacheamento de consultas com Redis. Este recurso é aplicado em InventoryService da seguinte forma: quando o usuário faz a requisição de um produto pelo seu ID ou da lista de produtos através da API, a aplicação busca em primeiro lugar obter esta informação do cache armazenado no Redis e caso não encontre só então realiza uma busca no banco de dados. 
O TTL para as informações cacheadas é de 5 minutos para fins de demonstração neste projeto.
Quando um produto é atualizado, seu cache é invalidado forçando a busca por dados consistentes na próxima consulta. O mesmo ocorre com a lista de produtos quando um produto é adicionado.


### Uma Arquitetura Limpa
Neste projeto um esforço sincero foi realizado para criar uma arquitetura limpa e organizada. Para tanto baseou-se em princípios do DDD, inversão de dependência e desacoplamento entre as camadas. Os dois microserviços seguiram em geral a mesma estrutura. Por exemplo a aplicação OrderService é composta de 6 projetos:

- *OrderBusiness*: Este projeto é a camada de negócios ou domínio. Nela esta o “core” da aplicação e as regras de negócio. Ela não possui dependência com nenhum outro projeto e poderia ser facilmente portada. Nela também estão as interfaces para os repositórios e os publishers.

- *OrderInfraData*: Este projeto é responsável pelo acesso e envio dos dados para estruturas externas como o banco de dados Postgres e as filas do RabbitMQ. Aqui são implementadas algumas interfaces da camada de negócios.

- *OrderInfraIOC*: Este projeto é um contêiner de injeção de dependências, para manter o código mais organizado a injeção de todas as dependências foram concentradas neste projeto.
- *OrderApplication*: Este projeto é uma ponte ou fachada (facade) entre as camadas de regra de negócios e acesso a dados e a API. Aqui estão os serviços que utilizam os repostiórios, os DTOs e os mapeamentos entre DTOs e entidades de domínio.

- *_OrderAPI*: A API do serviço, onde estão disponiblizados os endpoints públicos.

- *OrderTests*: Este projeto cobre os mais diversos testes para assegurar a qualidade e bom funcionamento da aplicação, testes unitários e de integração.

### Tecnologias e Conceitos Utilizados:

Para mensageria:
RabbitMQ (Pubisher, Consumer)

OrderService:
- .Net 8
- Minimal API (versionamento)
- Dependency Injection,
- Entity Framework Core (Code First, Migrations)
- Background services
- Nunit para testes unitários e de integração

InventoryService:
- .Net 8
- Web API (Controllers, versionamento)
- Dependency Injection
- Nhibernate para acesso a dados (Database first) 
- Redis para cache
- Xunit para testes unitários e de integração

Banco de dados:
 - PostgreSQL 17 (bancos separados para cada microserviço)

### Como Rodar e Acessar o Projeto
Para facilitar o uso e exploração deste projeto eu concentrei todas as suas partes em um docker-compose.yml e criei um script para inicializar o banco dados de um dos serviços que usa abordagem “Database First”.
Portanto para rodar o projeto você irár precisar de ter Git, Docker e a ferramenta docker-compose instalados:

Git: https://github.com/git-guides/install-git

Docker: https://docs.docker.com/engine/install/

Docker Compose: https://docs.docker.com/compose/install/

1 – Clone o repositório em sua máquina local:

`git clone https://github.com/allysonguilherme/bring-me-my-order.git'`

2 – Navegue para o diretório ‘bring-me-my-order’

`cd bring-me-my-order`

3 – Dentro do diretório execute o comando:

`docker-compose up -d`

4 – Aguarde a construção e execução de todos os conteineres e após isso acesse as seguintes rotas em seu navegador preferido:

http://localhost:5000/swagger/index.html  - para usar a API de catálogos de produtos.

http://localhost:5001/swagger/index.html – para usar a API de gerenciamento de pedidos.
