using CapitalFlow.Api.Features.Users.RegisterUser;
using CapitalFlow.Domain.Entities.Customers.Customers;
using CapitalFlow.Domain.Entities.Customers.GraphicAccounts;
using CapitalFlow.Domain.Entities.PurchasingAndDistribution.Custodies;
using CapitalFlow.Domain.Entities.Users;
using CapitalFlow.Persistence.Database;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CapitalFlow.Api.Features.Customers.Customers.RegisterCustomer;

public sealed class RegisterCustomerCommandHandler : ICommandHandler<RegisterCustomerCommand, ErrorOr<RegisterCustomerResponse>>
{
    private readonly CapitalFlowDbContext _database;
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<RegisterCustomerCommandHandler> _logger;

    public RegisterCustomerCommandHandler(CapitalFlowDbContext database, ICustomerRepository customerRepository, ILogger<RegisterCustomerCommandHandler> logger)
    {
        _database = database;
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public async Task<ErrorOr<RegisterCustomerResponse>> ExecuteAsync(RegisterCustomerCommand request, CancellationToken ct)
    {
        var executionStrategy = _database.Database.CreateExecutionStrategy();
        
        var transactionResult = await executionStrategy.ExecuteAsync(async () => await RegisterCustomerAsync(request, ct));

        return transactionResult;
    }

    private async Task<ErrorOr<RegisterCustomerResponse>> RegisterCustomerAsync(RegisterCustomerCommand request, CancellationToken ct)
    {
        _logger.LogInformation("Starting registration process for customer: {CustomerName}", request.Name);

        await using var transaction = await _database.Database.BeginTransactionAsync(ct);

        var registerUserCommand = new RegisterUserCommand
        {
            Name = request.Name,
            Email =  request.Email,
            Password =  request.Password,
            Type = UserType.Customer
        };

        var userResult = await registerUserCommand.ExecuteAsync(ct);

        if (userResult.IsError)
        {
            _logger.LogWarning("Customer registration failed for email: {Email}", request.Email);
            return userResult.Errors;
        }
        
        var userId = userResult.Value.Id;

        var customerResult = await Customer.CreateAsync(
            name: request.Name,
            email: request.Email,
            cpf: request.Cpf,
            userId: userId,
            monthlyAmount: request.MonthlyAmount,
            customerRepository: _customerRepository,
            ct: ct
        );

        if (customerResult.IsError)
        {
            _logger.LogWarning("Customer creation failed for email: {CustomerEmail}", request.Email);
            await transaction.RollbackAsync(ct);
            return customerResult.Errors;
        }

        var customerId = customerResult.Value.Id;

        var accountResult = GraphicAccount.Create(customerId);

        if (accountResult.IsError)
        {
            _logger.LogWarning("Failed to create graphical account.");
            await transaction.RollbackAsync(ct);
            return accountResult.Errors;
        }

        var accountId = accountResult.Value.Id;

        var custody = Custody.Create(accountId, "", 0, 0);

        if (custody.IsError)
        {
            _logger.LogWarning("Failed to create custody.");
            await transaction.RollbackAsync(ct);
            return custody.Errors;
        }
        
        var customer = customerResult.Value;
        var account = accountResult.Value;
        
        _database.Customers.Add(customer);
        _database.GraphicAccounts.Add(account);
        _database.Custodies.Add(custody.Value);

        await _database.SaveChangesAsync(ct);
        
        await transaction.CommitAsync(ct);
        
        _logger.LogInformation("Customer registration successfuly for customer: {customerId}", customer.Id);

        var response = new RegisterCustomerResponse
        {
            Id = customer.Id,
            Name = customer.Name,
            Cpf = customer.Cpf,
            Email = customer.Email,
            UserId = userId,
            MonthlyAmount = customer.MonthlyAmount,
            Status = customer.Status,
            JoinDate = customer.JoinDate,

            GraphicAccount = new GraphicAccountResponse
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                Type = account.Type,
                CreatedAt = account.CreatedAt,
            }
        };

        return response;
    }
}