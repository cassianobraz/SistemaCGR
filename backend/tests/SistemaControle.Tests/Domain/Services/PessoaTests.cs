using Moq;
using Result.Domain.Enum;
using SistemaControle.Domain.Models.PessoaAggregate;
using SistemaControle.Domain.Services;
using SistemaControle.Domain.Shared;
using static SistemaControle.Domain.Shared.CatalogoDeErros;

namespace SistemaControle.UnitTests.Domain.Services;

public sealed class PessoaTests
{
    private readonly Mock<IPessoaRepository> _repo;
    private readonly Mock<IUnitOfWork> _uow;
    private readonly PessoaService _sut;

    public PessoaTests()
    {
        // Arrange (Global)
        _repo = new Mock<IPessoaRepository>(MockBehavior.Strict);
        _uow = new Mock<IUnitOfWork>(MockBehavior.Strict);
        _sut = new PessoaService(_repo.Object, _uow.Object);
    }

    [Fact]
    public async Task CriarAsync_Nome_Eh_Branco_Deve_Retornar_Failure_E_Nao_Persistir()
    {
        // Arrange
        var nome = "   ";
        var idade = 25;

        // Act
        var result = await _sut.CriarAsync(nome, idade, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(TipoErro.Domain, result.ErrorType);
        Assert.Contains(result.Errors, e => e.Code == NomePessoaInvalido);

        _repo.Verify(r => r.CriarAsync(It.IsAny<Pessoa>(), It.IsAny<CancellationToken>()), Times.Never);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(121)]
    [InlineData(999)]
    public async Task CriarAsync_Idade_Fora_Do_Intervalo_Deve_Retornar_Failure_E_Nao_Persistir(int idade)
    {
        // Arrange
        var nome = "Cassiano";

        // Act
        var result = await _sut.CriarAsync(nome, idade, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(TipoErro.Domain, result.ErrorType);
        Assert.Contains(result.Errors, e => e.Code == IdadePessoaInvalida);

        _repo.Verify(r => r.CriarAsync(It.IsAny<Pessoa>(), It.IsAny<CancellationToken>()), Times.Never);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CriarAsync_Dados_Sao_Validos_Deve_Persistir_E_Retornar_Guid_Gerado()
    {
        // Arrange
        Pessoa? pessoaPersistida = null;

        _repo.Setup(r => r.CriarAsync(It.IsAny<Pessoa>(), It.IsAny<CancellationToken>()))
            .Callback<Pessoa, CancellationToken>((p, _) => pessoaPersistida = p)
            .Returns(Task.CompletedTask);

        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var nome = "  Cassiano  ";
        var idade = 25;

        // Act
        var result = await _sut.CriarAsync(nome, idade, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);

        Assert.NotNull(pessoaPersistida);
        Assert.Equal(result.Value, pessoaPersistida!.Id);
        Assert.Equal("  Cassiano  ", pessoaPersistida.Nome);
        Assert.Equal(25, pessoaPersistida.Idade);

        _repo.Verify(r => r.CriarAsync(It.IsAny<Pessoa>(), It.IsAny<CancellationToken>()), Times.Once);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExcluirAsync_Pessoa_Nao_Encontrada_Deve_Retornar_Failure_E_Nao_Excluir()
    {
        // Arrange
        _repo.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Pessoa?)null);

        var id = Guid.NewGuid();

        // Act
        var result = await _sut.ExcluirAsync(id, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(TipoErro.Domain, result.ErrorType);
        Assert.Contains(result.Errors, e => e.Code == PessoaNaoEncontrada);

        _repo.Verify(r => r.ExcluirAsync(It.IsAny<Pessoa>(), It.IsAny<CancellationToken>()), Times.Never);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ExcluirAsync_Pessoa_Encontrada_Deve_Excluir_E_Salvar()
    {
        // Arrange
        var pessoa = Pessoa.Create("Maria", 30);

        _repo.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(pessoa);

        _repo.Setup(r => r.ExcluirAsync(It.IsAny<Pessoa>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _sut.ExcluirAsync(pessoa.Id, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);

        _repo.Verify(r => r.ExcluirAsync(It.Is<Pessoa>(p => p.Id == pessoa.Id), It.IsAny<CancellationToken>()), Times.Once);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}