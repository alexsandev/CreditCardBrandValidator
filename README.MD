# 💳 Projeto: Validador de Bandeiras de Cartão de Crédito

Este projeto foi desenvolvido como recurso de aprendizagem para o Bootcamp da DIO "TIVIT: Dotnet com GitHub Copilot". Trata-se de uma **Web API** construída com **ASP.NET Core** que demonstra a aplicação de regras de negócio para a validação e identificação de cartões de crédito.

## 🎓 Objetivos Didáticos
O foco deste projeto é colocar em prática conceitos fundamentais de engenharia de software e desenvolvimento backend:

* **Desenvolvimento de APIs REST:** Criação de endpoints para comunicação padronizada via protocolo HTTP.
* **Lógica de Programação:** Implementação do **Algoritmo de Luhn**, uma fórmula de checksum essencial para verificação de dígitos.
* **Regex (Expressões Regulares):** Uso de padrões de busca para identificação de IIN/BIN (os primeiros dígitos que definem a bandeira).
* **Clean Architecture:** Organização do código em camadas para separar a lógica de negócio das tecnologias de interface (API), garantindo um código mais testável e fácil de manter.
* **GitHub Copilot:** Utilização do GitHub Copilot e outras ferramentas de IA para auxilio no processo de desenvolvimento.

---

## 🛠️ Como o projeto funciona?

O sistema recebe um número de cartão e realiza dois processos principais:

1.  **Validação de Integridade:** Utiliza o **Algoritmo de Luhn** para verificar se o número inserido é matematicamente consistente, prevenindo erros de digitação.
2.  **Identificação da Bandeira:** Através do mapeamento dos primeiros dígitos (BIN), o sistema identifica se o cartão pertence a emissores como **Visa, Mastercard, Elo, Hipercard**, entre outros.

---

## 📂 Estrutura de Camadas (Clean Architecture)

Para manter o código organizado e profissional, o projeto segue os princípios da Arquitetura Limpa:

* **Domínio/Core:** Contém as entidades e a lógica "pura" de validação, independente de frameworks.
* **Aplicação:** Camada que gerencia o fluxo de dados e as regras de serviço.
* **Apresentação (API):** Camada externa que contém os endpoints, responsável por expor os serviços para o mundo exterior.

---

## 🚀 Como rodar o projeto

Para executar esta API localmente, você precisará do **SDK do .NET** instalado.

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/alexsandev/CreditCardBrandValidator](https://github.com/alexsandev/CreditCardBrandValidator)
    ```
2.  **Entre na pasta do projeto:**
    ```bash
    cd CreditCardBrandValidator
    ```
3.  **Execute a aplicação:**
    ```bash
    dotnet run
    ```
4.  **Acesse a documentação (Swagger):**
    Com o projeto rodando, abra o navegador em: `http://localhost:5000/swagger` (ou a porta exibida no seu console) para testar os endpoints interativamente.

---

## 

**Autor:** Alexsandev

**Tecnologias:** .NET Core, C#, Swagger, Clean Architecture.

---