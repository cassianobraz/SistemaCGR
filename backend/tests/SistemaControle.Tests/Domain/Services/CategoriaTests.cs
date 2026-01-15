using Moq;
using Result.Domain.Enum;
using SistemaControle.Domain.Models.CategoriaAggregate;
using SistemaControle.Domain.Services;
using SistemaControle.Domain.Shared;
using SistemaControle.Domain.Shared.Enums;
using static SistemaControle.Domain.Shared.CatalogoDeErros;

namespace SistemaControle.UnitTests.Domain.Services;

public sealed class CategoriaTests
{
    private readonly Mock<ICategoriaRepository> _repo;
    private readonly Mock<IUnitOfWork> _uow;
    private readonly CategoriaService _sut;

    public CategoriaTests()
    {
        // Arrange (Global)
        _repo = new Mock<ICategoriaRepository>(MockBehavior.Strict);
        _uow = new Mock<IUnitOfWork>(MockBehavior.Strict);
        _sut = new CategoriaService(_repo.Object, _uow.Object);
    }

    [Fact]
    public async Task CriarAsync_Descricao_Eh_Branca_Deve_Retornar_Failure_E_Nao_Persistir()
    {
        // Arrange
        var descricao = "   ";
        var finalidade = Finalidade.Despesa;

        // Act
        var result = await _sut.CriarAsync(descricao, finalidade, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(TipoErro.Domain, result.ErrorType);
        Assert.Contains(result.Errors, e => e.Code == DescricaoCategoriaNull);

        _repo.Verify(r => r.CriarAsync(It.IsAny<Categoria>(), It.IsAny<CancellationToken>()), Times.Never);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CriarAsync_Finalidade_Eh_Invalida_Deve_Retornar_Failure_E_Nao_Persistir()
    {
        // Arrange
        var descricao = "Alimentação";
        var finalidadeInvalida = (Finalidade)999;

        // Act
        var result = await _sut.CriarAsync(descricao, finalidadeInvalida, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(TipoErro.Domain, result.ErrorType);
        Assert.Contains(result.Errors, e => e.Code == FinalidadeCategoriaInvalida);

        _repo.Verify(r => r.CriarAsync(It.IsAny<Categoria>(), It.IsAny<CancellationToken>()), Times.Never);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CriarAsync_Dados_Sao_Validos_Deve_Persistir_E_Retornar_Guid_Gerado()
    {
        // Arrange
        Categoria? categoriaPersistida = null;

        _repo.Setup(r => r.CriarAsync(It.IsAny<Categoria>(), It.IsAny<CancellationToken>()))
            .Callback<Categoria, CancellationToken>((c, _) => categoriaPersistida = c)
            .Returns(Task.CompletedTask);

        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var descricao = "  Mercado  ";
        var finalidade = Finalidade.Despesa;

        // Act
        var result = await _sut.CriarAsync(descricao, finalidade, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);

        Assert.NotNull(categoriaPersistida);
        Assert.Equal(result.Value, categoriaPersistida!.Id);
        Assert.Equal("Mercado", categoriaPersistida.Descricao);
        Assert.Equal(Finalidade.Despesa, categoriaPersistida.Finalidade);

        _repo.Verify(r => r.CriarAsync(It.IsAny<Categoria>(), It.IsAny<CancellationToken>()), Times.Once);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}