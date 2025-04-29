# 🃏 Projeto Blackjack em C#

Este é um projeto de implementação do jogo **Blackjack (21)** utilizando **C#** com interface em **console** (modo texto).  
O objetivo é simular uma experiência completa de Blackjack com lógica de jogo, pontuação e controle de saldo do jogador.

---

## 🚀 Tecnologias Utilizadas

- 💻 Linguagem: **C#**
- 🧰 Ambiente: **.NET 8.0** ou superior
- 📦 Interface: **Console (modo texto)**

---

## ▶️ Como Executar

1. **Clone o repositório:**

```bash
git clone https://github.com/LeonardoPessini/BlackJack.git
cd nome-do-repositorio
```

2. **Compile e execute com o .NET CLI:**

```bash
dotnet run
```

<br/>

## 🃏 Requisito Funcional – Jogo de Blackjack

**Nome:** Mecânica básica do jogo de Blackjack

### 📄 Descrição
O sistema deve permitir que um jogador humano jogue Blackjack contra um dealer controlado pelo sistema, seguindo as regras clássicas do jogo.  
O objetivo do jogador é obter uma mão com valor total mais próximo de 21 do que a do dealer, **sem ultrapassar esse limite**.

Um **Blackjack natural** ocorre quando o jogador recebe, nas duas primeiras cartas, um Ás e uma carta de valor 10 (10, J, Q ou K), totalizando exatamente 21.  
Essa mão é considerada uma vitória automática (exceto quando o dealer também tiver um Blackjack natural, resultando em empate).  
O pagamento padrão para um Blackjack natural é de **3:2**.

Durante o jogo, o jogador pode escolher entre as seguintes ações:

- **Hit**: Pedir mais uma carta.
- **Stand**: Parar e manter a mão atual, sem receber mais cartas.
- **Double Down**: Dobrar a aposta original e receber apenas uma carta adicional.
- **Split**: Se as duas cartas iniciais forem de mesmo valor, o jogador pode dividir a mão em duas mãos separadas, com uma aposta adicional em cada.
- **Surrender**: Em algumas variações, o jogador pode desistir da mão inicial, perdendo metade da aposta.

---

### 💵 Proporções de Pagamento

| Situação             | Pagamento  |
|----------------------|------------|
| Vitória normal       | 1:1        |
| Blackjack natural    | 3:2        |
| Empate (Push)        | Aposta devolvida |

---

### ✅ Critérios de Aceitação

1. O sistema deve distribuir duas cartas iniciais para o jogador e duas para o dealer (uma virada para cima e uma virada para baixo).
2. O jogador deve poder escolher entre as ações disponíveis: `Hit`, `Stand`, `Double Down`, `Split` (se aplicável) e `Surrender` (se ativado).
3. O sistema deve calcular corretamente o valor da mão de acordo com as regras de pontuação do Blackjack.
4. O dealer deve seguir a lógica padrão: comprar cartas até atingir pelo menos 17 pontos.
5. O sistema deve reconhecer vitórias, derrotas, empates (`push`) e Blackjack natural.
6. O sistema deve aplicar corretamente os pagamentos conforme a proporção de cada situação.
7. O sistema deve exibir o resultado final da rodada e atualizar o saldo do jogador com base no resultado.
8. O sistema deve permitir múltiplas rodadas consecutivas, mantendo o saldo do jogador.
9. O sistema deve lidar corretamente com situações de `Split` e `Double Down`.
10. A interface deve ser clara, permitindo ao jogador visualizar suas cartas, as do dealer (conforme permitido) e todas as opções de jogada disponíveis.
11. O sistema deve prevenir ações inválidas (ex: tentar `Hit` após `Stand`).

---

<br/>

## 📌 Status do Projeto
🚧 Em desenvolvimento – versão console com foco na lógica de jogo e validação de regras.

## 📄 Licença
Este projeto está licenciado sob a MIT License.

---