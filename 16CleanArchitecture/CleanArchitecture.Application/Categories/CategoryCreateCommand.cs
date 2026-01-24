using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Categories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Categories;

public sealed record CategoryCreateCommand(
    string Name) : IRequest<Result<Guid>>;

public sealed class CategoryCreateCommandValidator : AbstractValidator<CategoryCreateCommand>
{
    public CategoryCreateCommandValidator()
    {
        RuleFor(p => p.Name).MinimumLength(2).WithMessage("Geçerli bir ürün adı girin");
    }
}

internal sealed class CategoryCreateCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CategoryCreateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
    {
        bool isNameExists = await categoryRepository
            .GetAll()
            .AnyAsync(p => p.Name == new Name(request.Name), cancellationToken);
        if (isNameExists)
        {
            return Result<Guid>.Failure("Bu kategori adı daha önce kullanılmış");
        }

        Name name = new(request.Name);//
        var category = Category.Create(name);
        //Category category = request.Adapt<Category>();

        categoryRepository.Add(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}