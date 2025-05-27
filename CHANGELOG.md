# Changelog - Refatoração e Melhoria do Projeto

---

## Contexto Geral 

- Refatoração substancial para separar responsabilidades e reduzir acoplamento.
- Implementação de testes unitários para ~50% de cobertura dos fluxos principais.
- Preparação para implementação futura de interface fluente.
- Organização modular clara em camadas Domain, Application e Presentation.
- Documentação detalhada para facilitar manutenção e evolução.

---

## Projeto Application

### Adicionado: Testes Unitários para `GameService`

- Cobertura dos métodos principais do serviço de regras de negócio:
    - Criação de jogadores (Humano, Dealer, Computador).
    - Verificação de fichas do jogador humano.
    - Definição de estratégias, incluindo cenários com listas vazias.
    - Realização de apostas, cobrindo fluxo normal e casos sem jogadores.
    - Distribuição de cartas e avaliação dos vencedores, testando robustez contra listas vazias.
    - Obtenção do estado da mesa e puxar cartas.
- Testes focados em garantir comportamento esperado e evitar exceções inesperadas.
- Uso de mocks para simular entradas e garantir previsibilidade.
- Justificativa: Garante confiabilidade da lógica de jogo e facilita futuras alterações.

---

## Projeto Domain

### Refatorações e Melhorias Gerais

- Modificações em classes centrais (Baralho, Carta, Jogador) para melhor encapsulamento:
    - Propriedades privadas e métodos de acesso controlados.
    - Uso de LINQ para expressões concisas e seguras.
    - Troca do gerador `Random` por `RandomNumberGenerator` para maior segurança.
- Extração e centralização de lógica em métodos específicos para clareza e reutilização.
- Atualização de namespace para sintaxe moderna.
- Justificativa: Aumenta legibilidade, segurança e mantém consistência de código.

---

## Projeto Presentation

### Refatoração e Modularização

- Criação de `MesaController` para orquestrar fluxo do jogo.
- Criação de `MesaView` para exibição e interação no console.
- Extração da lógica de negócio para `GameService` na camada Application.
- Remoção da classe `Mesa` que misturava controle e apresentação.
- Introdução da interface `IGameService` para abstração da lógica.
- Justificativa: Segue princípios SOLID (especialmente SRP e ISP), facilitando manutenção, testes e evolução do código.

---

## Gerais

### Documentação e Qualidade de Código

- Organização detalhada do changelog para rastreabilidade das mudanças.
- Preparação para implementação futura de interface fluente, melhorando usabilidade da API.
- Indicação da necessidade de aplicação de linter para padronização do estilo.
- Uso consistente de testes para prevenir regressões e garantir qualidade.