# Gerenciamento de Notas - Admin Panel

## Visão Geral

Este é o painel administrativo desenvolvido em .NET para gerenciar o sistema de notas. O principal objetivo deste painel é fornecer uma interface segura e eficiente para administradores controlarem e monitorarem usuários e suas atividades no sistema. O painel utiliza o MongoDB Cloud para armazenamento de dados, garantindo uma solução escalável e moderna.

## Funcionalidades

O projeto conta com diversas funcionalidades principais:

### 1. **Gerenciamento de Usuários**
   - **Listar Usuários**: Exibição de todos os usuários cadastrados no sistema.
   - **Ativar/Desativar Contas**: Habilitar ou desabilitar contas de usuários conforme necessário.
   - **Reset de Senhas**: Possibilitar o reset de senhas dos usuários diretamente pelo admin.

### 2. **Monitoramento de Atividades**
   - **Visualização de Logs**: Acesso aos logs gerados e armazenados no MongoDB Cloud, permitindo uma análise detalhada das atividades e operações do sistema.
   - **Métricas do Sistema**: Exibição de um dashboard com métricas detalhadas sobre o sistema, como o número total de usuários, número de notas criadas, atividades recentes, e uso de recursos. Gráficos e estatísticas permitem uma visão abrangente da performance e uso do sistema.

## Tecnologias Utilizadas

- **.NET Core**: Framework para desenvolvimento de aplicações web robustas e escaláveis.
- **Core MVC**: Estrutura para construção de aplicações web com o padrão Model-View-Controller.
- **MongoDB Cloud**: Banco de dados NoSQL hospedado na nuvem, utilizado para armazenar as informações dos usuários, logs, e outras entidades.
- **Docker**: Para facilitar o deployment e a instalação local do projeto.
- **Bootstrap**: Framework CSS para desenvolvimento front-end responsivo e acessível.
- **Chart.js**: Biblioteca JavaScript para exibição de gráficos e estatísticas interativas.
- **Serilog**: Para logging estruturado na aplicação.

## Instalação

### Pré-requisitos

- Docker instalado em sua máquina.

### Configuração do Ambiente

O painel administrativo não está hospedado online por questões de segurança. No entanto, você pode instalar e rodar o projeto localmente utilizando Docker apenas em Windows.

1. Acesse o link abaixo e faça download da versão mais recente:

https://github.com/ThalesLJ/OrganizandoTudo.Admin/releases/tag/Windows
   
