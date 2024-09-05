# Locadora De Veiculos 2024

<div align="center">

| <img width="60" src="https://user-images.githubusercontent.com/25181517/121405754-b4f48f80-c95d-11eb-8893-fc325bde617f.png"> | <img width="60" src="https://miro.medium.com/v2/resize:fit:300/0*cdEEkdP1WAuz-Xkb.png"> | <img width="60" src="https://raw.githubusercontent.com/altmann/FluentResults/master/resources/icons/FluentResults-Icon-64.png"> | <img width="60" src="https://rodrigoesilva.wordpress.com/wp-content/uploads/2011/04/sqlserver_sql_server_2008_logo.png"> |
|:---:|:---:|:---:|:---:|
| .NET Core | ASP.NET Core | FluentResults | Microsoft SQL Server |
|
| <img width="60" src="https://www.infoport.es/wp-content/uploads/2023/09/entity-core.png"> | <img width="60" src="https://api.nuget.org/v3-flatcontainer/dapper/2.1.35/icon"> | <img width="60" src="https://www.lambdatest.com/blog/wp-content/uploads/2021/03/MSTest.png"> | <img width="60" src="https://user-images.githubusercontent.com/25181517/184103699-d1b83c07-2d83-4d99-9a1e-83bd89e08117.png"> |
| EF Core | Dapper | MSTest | Selenium |

</div>

## Projeto
**Desenvolvido durante o curso Fullstack da [Academia do Programador](https://www.academiadoprogramador.net) 2024**

### Arquitetura
- DDD
- N-Camadas

### Stack:
- NET 8.0
- ASP.NET MVC
- Microsoft Identity
- Microsoft SQL Server
- Entity Framework Core
- AutoMapper
- Dapper
- FluentResults
- Selenium

### Inclui:
- Testes de Unidade
- Testes de Integra��o
- Testes e2e
- Autentica��o e Autoriza��o com Microsoft Identity
---

## Detalhes

O sistema visa facilitar o gerenciamento das opera��es de uma locadora de autom�veis,
abrangendo desde o cadastro de funcion�rios, grupos de autom�veis, ve�culos e clientes at� a
configura��o de pre�os de alugu�is e devolu��es.

O sistema permitir� a cria��o de usu�rios para empresas que desejam utilizar a plataforma para
gerenciar o aluguel de seus ve�culos. Usu�rios administradores dessas empresas ter�o a
capacidade de cadastrar e gerenciar seus funcion�rios, incluindo a ativa��o e desativa��o de
contas de usu�rio. Esses funcion�rios realizar�o tarefas operacionais, como o cadastro de ve�culos,
registro de loca��es, entre outras atividades relacionadas.

Al�m disso, o sistema calcula o valor dos alugu�is considerando diversos fatores, como tipo do
ve�culo, plano escolhido e taxas adicionais.

A locadora oferecer� uma lista de taxas e servi�os que os clientes poder�o adicionar aos alugu�is.
Cada taxa ou servi�o ter� um pre�o e indica��o se � fixo ou calculado por dia, afetando o pre�o
total do aluguel.

Ser� poss�vel registrar a devolu��o dos ve�culos, aplicando multas em caso de atraso. O sistema
tamb�m permitir� configurar o pre�o do combust�vel para inclus�o no c�lculo dos alugu�is.

O objetivo � proporcionar uma gest�o eficiente e uma experi�ncia positiva tanto para os
funcion�rios quanto para os clientes da locadora.

## Requisitos

- .NET SDK (recomendado .NET 8.0 ou superior) para compila��o e execu��o do projeto.
---
## Como Usar

#### Clone o Reposit�rio
```
git clone https://github.com/academia-do-programador/locadora-de-veiculos-2024.git
```

#### Navegue at� a pasta raiz da solu��o
```
cd locadora-de-veiculos-2024
```

#### Restaure as depend�ncias
```
dotnet restore
```

#### Navegue at� a pasta do projeto
```
cd LocadoraDeVeiculos.WebApp
```

#### Execute o projeto
```
dotnet run
```