using Moq;
using Result.Domain.Enum;
using SistemaControle.Domain.Models.CategoriaAggregate;
using SistemaControle.Domain.Models.PessoaAggregate;
using SistemaControle.Domain.Models.TransacoesAggregate;
using SistemaControle.Domain.Services;
using SistemaControle.Domain.Shared;
using SistemaControle.Domain.Shared.Enums;
using static SistemaControle.Domain.Shared.CatalogoDeErros;

namespace SistemaControle.Tests.Unit.Domain.Services;

public sealed class TransacoesTests
{
    private readonly Mock<ITransacoesRepository> _repo;
    private readonly Mock<IUnitOfWork> _uow;
    private readonly TransacoesService _sut;

    public TransacoesTests()
    {
        // Arrange (Global)
        _repo = new Mock<ITransacoesRepository>(MockBehavior.Strict);
        _uow = new Mock<IUnitOfWork>(MockBehavior.Strict);
        _sut = new TransacoesService(_repo.Object, _uow.Object);
    }

    [Fact]
    public async Task CriarAsync_Descricao_Eh_Branca_Deve_Retornar_Failure_E_Nao_Persistir()
    {
        // Arrange
        var descricao = "   ";

        // Act
        var result = await _sut.CriarAsync(
            descricao,
            10m,
            TipoTransacao.Despesa,
            Guid.NewGuid(),
            Guid.NewGuid(),
            CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(TipoErro.Domain, result.ErrorType);
        Assert.Contains(result.Errors, e => e.Code == DescricaoTransacaoNull);

        _repo.Verify(r => r.ObterCategoriaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        _repo.Verify(r => r.ObterPessoaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        _repo.Verify(r => r.CriarAsync(It.IsAny<Transacao>(), It.IsAny<CancellationToken>()), Times.Never);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CriarAsync_Tipo_Eh_Invalido_Deve_Retornar_Failure_E_Nao_Persistir()
    {
        // Arrange
        var tipoInvalido = (TipoTransacao)999;

        // Act
        var result = await _sut.CriarAsync(
            "Mercado",
            10m,
            tipoInvalido,
            Guid.NewGuid(),
            Guid.NewGuid(),
            CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(TipoErro.Domain, result.ErrorType);
        Assert.Contains(result.Errors, e => e.Code == TipoTransacaoInvalida);

        _repo.Verify(r => r.ObterCategoriaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        _repo.Verify(r => r.ObterPessoaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        _repo.Verify(r => r.CriarAsync(It.IsAny<Transacao>(), It.IsAny<CancellationToken>()), Times.Never);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task CriarAsync_Valor_Eh_Invalido_Deve_Retornar_Failure_E_Nao_Persistir(decimal valor)
    {
        // Arrange
        var descricao = "Mercado";

        // Act
        var result = await _sut.CriarAsync(
            descricao,
            valor,
            TipoTransacao.Despesa,
            Guid.NewGuid(),
            Guid.NewGuid(),
            CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(TipoErro.Domain, result.ErrorType);
        Assert.Contains(result.Errors, e => e.Code == ValorTransacaoInvalida);

        _repo.Verify(r => r.ObterCategoriaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        _repo.Verify(r => r.ObterPessoaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        _repo.Verify(r => r.CriarAsync(It.IsAny<Transacao>(), It.IsAny<CancellationToken>()), Times.Never);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CriarAsync_Categoria_Nao_Encontrada_Deve_Retornar_NotFound_E_Nao_Persistir()
    {
        // Arrange
        _repo.Setup(r => r.ObterCategoriaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Categoria?)null);

        // Act
        var result = await _sut.CriarAsync(
            "Mercado",
            10m,
            TipoTransacao.Despesa,
            Guid.NewGuid(),
            Guid.NewGuid(),
            CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(TipoErro.NotFound, result.ErrorType);
        Assert.Contains(result.Errors, e => e.Code == CategoriaTransacaoNull);

        _repo.Verify(r => r.ObterPessoaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        _repo.Verify(r => r.CriarAsync(It.IsAny<Transacao>(), It.IsAny<CancellationToken>()), Times.Never);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CriarAsync_Pessoa_Nao_Encontrada_Deve_Retornar_NotFound_E_Nao_Persistir()
    {
        // Arrange
        _repo.Setup(r => r.ObterCategoriaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Categoria.Create("Alimentação", Finalidade.Despesa));

        _repo.Setup(r => r.ObterPessoaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Pessoa?)null);

        // Act
        var result = await _sut.CriarAsync(
            "Mercado",
            10m,
            TipoTransacao.Despesa,
            Guid.NewGuid(),
            Guid.NewGuid(),
            CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(TipoErro.NotFound, result.ErrorType);
        Assert.Contains(result.Errors, e => e.Code == PessoaTransacaoNull);

        _repo.Verify(r => r.CriarAsync(It.IsAny<Transacao>(), It.IsAny<CancellationToken>()), Times.Never);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CriarAsync_Pessoa_Eh_Menor_De_Idade_E_Tipo_Eh_Receita_Deve_Retornar_Failure()
    {
        // Arrange
        _repo.Setup(r => r.ObterCategoriaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Categoria.Create("Salário", Finalidade.Receita));

        _repo.Setup(r => r.ObterPessoaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Pessoa.Create("João", 17));

        // Act
        var result = await _sut.CriarAsync(
            "Salário",
            100m,
            TipoTransacao.Receita,
            Guid.NewGuid(),
            Guid.NewGuid(),
            CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(TipoErro.Domain, result.ErrorType);
        Assert.Contains(result.Errors, e => e.Code == PessoaMenorIdadeSomenteDespesa);

        _repo.Verify(r => r.CriarAsync(It.IsAny<Transacao>(), It.IsAny<CancellationToken>()), Times.Never);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CriarAsync_Categoria_Nao_Aceita_Tipo_Deve_Retornar_Failure_E_Nao_Persistir()
    {
        // Arrange
        _repo.Setup(r => r.ObterCategoriaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Categoria.Create("Salário", Finalidade.Receita));

        _repo.Setup(r => r.ObterPessoaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Pessoa.Create("Maria", 30));

        // Act
        var result = await _sut.CriarAsync(
            "Mercado",
            50m,
            TipoTransacao.Despesa,
            Guid.NewGuid(),
            Guid.NewGuid(),
            CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(TipoErro.Domain, result.ErrorType);
        Assert.Contains(result.Errors, e => e.Code == CategoriaFinalidadeInvalidaParaTipoTransacao);

        _repo.Verify(r => r.CriarAsync(It.IsAny<Transacao>(), It.IsAny<CancellationToken>()), Times.Never);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CriarAsync_Finalidade_Eh_Ambas_Deve_Aceitar_Receita_E_Persistir()
    {
        // Arrange
        var categoriaId = Guid.NewGuid();
        var pessoaId = Guid.NewGuid();

        _repo.Setup(r => r.ObterCategoriaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Categoria.Create("Freela", Finalidade.Ambas));

        _repo.Setup(r => r.ObterPessoaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Pessoa.Create("Maria", 30));

        _repo.Setup(r => r.CriarAsync(It.IsAny<Transacao>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _sut.CriarAsync(
            "Freela",
            200m,
            TipoTransacao.Receita,
            categoriaId,
            pessoaId,
            CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);

        _repo.Verify(r => r.CriarAsync(It.IsAny<Transacao>(), It.IsAny<CancellationToken>()), Times.Once);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CriarAsync_Finalidade_Eh_Receita_E_Tipo_Eh_Receita_Deve_Persistir_E_Retornar_Guid_Gerado()
    {
        // Arrange
        var categoriaId = Guid.NewGuid();
        var pessoaId = Guid.NewGuid();

        _repo.Setup(r => r.ObterCategoriaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Categoria.Create("Salário", Finalidade.Receita));

        _repo.Setup(r => r.ObterPessoaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Pessoa.Create("Maria", 30));

        Transacao? transacaoPersistida = null;

        _repo.Setup(r => r.CriarAsync(It.IsAny<Transacao>(), It.IsAny<CancellationToken>()))
            .Callback<Transacao, CancellationToken>((t, _) => transacaoPersistida = t)
            .Returns(Task.CompletedTask);

        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _sut.CriarAsync(
            "Salário",
            1000m,
            TipoTransacao.Receita,
            categoriaId,
            pessoaId,
            CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);

        Assert.NotNull(transacaoPersistida);
        Assert.Equal(result.Value, transacaoPersistida!.Id);
        Assert.Equal(TipoTransacao.Receita, transacaoPersistida.Tipo);

        _repo.Verify(r => r.CriarAsync(It.IsAny<Transacao>(), It.IsAny<CancellationToken>()), Times.Once);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CriarAsync_Finalidade_Eh_Invalida_Deve_Cair_No_Default_E_Retornar_Failure()
    {
        // Arrange
        var categoriaInvalida = Categoria.Create("Bug", (Finalidade)999);

        _repo.Setup(r => r.ObterCategoriaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(categoriaInvalida);

        _repo.Setup(r => r.ObterPessoaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Pessoa.Create("Maria", 30));

        // Act
        var result = await _sut.CriarAsync(
            "Qualquer",
            10m,
            TipoTransacao.Despesa,
            Guid.NewGuid(),
            Guid.NewGuid(),
            CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(TipoErro.Domain, result.ErrorType);
        Assert.Contains(result.Errors, e => e.Code == CategoriaFinalidadeInvalidaParaTipoTransacao);

        _repo.Verify(r => r.CriarAsync(It.IsAny<Transacao>(), It.IsAny<CancellationToken>()), Times.Never);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CriarAsync_Dados_Sao_Validos_Deve_Persistir_E_Retornar_Guid_Gerado()
    {
        // Arrange
        var categoriaId = Guid.NewGuid();
        var pessoaId = Guid.NewGuid();

        _repo.Setup(r => r.ObterCategoriaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Categoria.Create("Alimentação", Finalidade.Despesa));

        _repo.Setup(r => r.ObterPessoaPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Pessoa.Create("Maria", 30));

        Transacao? transacaoPersistida = null;

        _repo.Setup(r => r.CriarAsync(It.IsAny<Transacao>(), It.IsAny<CancellationToken>()))
            .Callback<Transacao, CancellationToken>((t, _) => transacaoPersistida = t)
            .Returns(Task.CompletedTask);

        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _sut.CriarAsync(
            "  Mercado  ",
            50m,
            TipoTransacao.Despesa,
            categoriaId,
            pessoaId,
            CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);

        Assert.NotNull(transacaoPersistida);
        Assert.Equal(result.Value, transacaoPersistida!.Id);
        Assert.Equal("  Mercado  ", transacaoPersistida.Descricao);
        Assert.Equal(50m, transacaoPersistida.Valor);
        Assert.Equal(TipoTransacao.Despesa, transacaoPersistida.Tipo);
        Assert.Equal(categoriaId, transacaoPersistida.CategoriaId);
        Assert.Equal(pessoaId, transacaoPersistida.PessoaId);

        _repo.Verify(r => r.CriarAsync(It.IsAny<Transacao>(), It.IsAny<CancellationToken>()), Times.Once);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}