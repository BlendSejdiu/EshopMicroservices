using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace BasketAPI.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDTO BasketCheckoutDTO): ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDTO).NotNull().WithMessage("BasketCheckoutDTO can't be null");
        RuleFor(x => x.BasketCheckoutDTO.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(command.BasketCheckoutDTO.UserName, cancellationToken);
        if (basket is null)
            return new CheckoutBasketResult(false);

        var eventMessage = command.BasketCheckoutDTO.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteBasket(command.BasketCheckoutDTO.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}