using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Reviews.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Review.Commands.AddReview;

internal sealed class AddReviewHandler : ICommandHandler<AddReviewCommand, ReviewInfoModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    
    public AddReviewHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IIdentityService identityService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<Result<ReviewInfoModel>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }
        
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Model.ProductId, cancellationToken);

        if (product is null)
        {
            return ProductErrors.NotFound(request.Model.ProductId);
        }

        var review = _mapper.Map<Domain.Review>(request.Model);
        review.Owner = user;
        product.Reviews.Add(review);

        await _unitOfWork.ProductRepository.UpdateAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ReviewInfoModel>(review);
    }
}