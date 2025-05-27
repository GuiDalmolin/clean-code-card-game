using Domain.Entities;

namespace Domain.Dtos;

public record DefinirVencedoresDto(Jogador Jogador, int PontuacaoJogador, int PontuacaoDealer);