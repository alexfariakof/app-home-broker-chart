# Home Broker Chart

## Descrição 

 Este projeto foi gerado utilizando template "Angular and ASP.NET Core" do Microsoft Visual Studio 2022 como base inicial da arquitetura do projeto SPA. O projeto segue utilizando padrão "DDD" e "MVC", onde as camadas Model e Controller são desenvolvidas usando DDD com C# (Backend) e a View usando Angular (Frontend). Foi configurado para proporcionar uma forma de desenvolver testes unitários mais fluida permitindo o uso de watch e atualização dos relátorios Coverage em tempo de execução. Esta dockerizada e configurada para a realização de debug da aplicação backend em ambiente docker.

 A aplicação em questão foi projetado com uma abordagem eficaz, utilizando Angular e .Net com principais tecnologiais proporcionando uma interface intuitiva e dinâmica. Permitindo a geração de gráficos detalhados e o acesso facilitado aos dados da ação da Magazine Luiza S.A.
Apresenta gráficos enriquecidos com indicadores chave, que são gerados a partir dos dados fornecidos pela [Magazine Luiza S.A. (MGLU3.SA)](https://br.financas.yahoo.com/quote/MGLU3.SA/history?interval=1d&filter=history&frequency=1d&guccounter=1&guce_referrer=aHR0cDovL2FsZXhmYXJpYWtvZi5jb206MzAwMi8&guce_referrer_sig=AQAAAC62RafgCLzqA0qFspCgB_bTkTVwGdqun-n006ne9WxHBr6aJIwlpEtfEFqGrA0VqiJFJYhQjI4g0ofX_8-uzOjnXYYUtDXe9RpeaRJakvRswjrJ7e9Inwx61bSMMj8CzPS5Pg6wVaXiTKNd0oMLabeZRO8nn70DDpEJcamNVzpy), incluindo a Média Móvel Simples (SMA), a Média Móvel Exponencial (EMA) e o Convergência e Divergência de Médias Móveis (MCAD).

* ### Média Móvel Simples (SMA):
A SMA é um indicador que calcula a média aritmética dos preços de um ativo durante um período específico. Essa média é recalculada continuamente à medida que novos dados se tornam disponíveis, oferecendo uma visão suavizada das tendências de preço ao longo do tempo.

* ### Média Móvel Exponencial (EMA):
A EMA é uma variação da SMA que atribui maior peso aos preços mais recentes, tornando-a mais sensível às mudanças recentes de preço. Isso a torna particularmente útil para identificar rapidamente as tendências emergentes do mercado.

* ### Convergência e Divergência de Médias Móveis (MCAD):
O MCAD é um indicador de momentum que compara duas médias móveis de um ativo. A linha MACD é formada subtraindo a EMA mais longa da EMA mais curta. Além disso, uma "linha de sinal" é derivada a partir da EMA da linha MACD. O MCAD é usado para identificar mudanças potenciais nas direções das tendências e fornecer sinais de compra ou venda.


## Cloud Application in Development 
> Este projeto pode ser acessado em  [Home Broker Chart Development](http://alexfariakof.com:3002/)
![image](https://github.com/alexfariakof/Home_Broker_Chart/assets/42475620/4c98b5ca-7628-4c27-b32e-556897a05c1d)

## Local Development
###### obs.: Caso seja a primeira execução pode levar alguns minutos para a aplicação iniciar pois configurações da aplicação Angular seram realizadas.
### Configurações de execução do projeto
> Com a solução ```slnHomeBroker.sln``` do projeto aberto no Visual Studio 2022 Set o projeto HomeBrokerSPA como StartUp Project e execute o projeto. 
 * HomeBrokerSPA

   Configuração Default do projeto, executa o backend e um proxy server que aguarda a aplicação frontend subir e redirecionando para rota default do frontend abrindo o frontend no brwoser default.
 
 * IISExpress
   
   Executa o projeto usando servidor IISExpress sem realizaar redirecionamento e subir a aplicação frontend. Permite acesso apenas acesso swagger UI.
   
 * Docker
   
   Executa o projeto em ambiente docker permitindo debug da aplicação backend e abre o frontend no browser default.
   
 * Swagger

   Executa o projeto e abre aplcação com Swagger UI no browser default.
   
 * Unit Tests in Watch Mode
   
   Executa teste unitários em modo watch e abre o relatório coverage no browser default, permitinido alteração do código fazendo hot reload e atualizando o resultado do relatório.

## Docker Local Development
###### obs.: Este build demora um pouco seja paciente.

> Tenha certeza de ter Docker Engine instalado, senão vá até [Install Docker Engine](https://docs.docker.com/engine/install/).

> Com a solução ```slnHomeBroker.sln``` do projeto aberto no Visual Studio 2022 Set o projeto docker-compose como StartUp Project e execute o projeto. 
 
## Unit Tests Backend
###### obs.: Com o prompt de comando do Powershell aberto no dirtório raiz do projeto.

Run `dotnet test ./HomeBrokerXUnit/HomeBrokerXUnit.csproj` para executar testes unitários.

Run `./generate_coverage_report.ps1` para executar testes unitários e gerar o relatório coverage.

Run `./dotnet_test_watch_mode.ps1` para executar testes unitários em modo watch.


## Unit Tests Frontend
###### obs.: Com o prompt de comando do Powershell aberto no dirtório raiz do projeto frontend  ```./HomeBrokerSPA/HomeBrokerChart```.

Run `npm run test` ou `ng test` para executar testes unitários, este é eecutado em modo watch com default.

Run `generate_coverage_report.ps1` para executar testes unitários e gerar o relatório coverage, após a execução o relatório sera aberto no browser default.

