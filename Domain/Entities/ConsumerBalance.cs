using System;

namespace Domain.Entities;

public class ConsumerBalance
{
    public ConsumerBalance()
    {}

    public ConsumerBalance(string consumerId, decimal balance, int version)
    {
        ConsumerId = consumerId;
        Balance = balance;
        Version = version;
    }


    public string ConsumerId { get; init; }

    public decimal Balance { get; private set; }

    public int Version { get; init; }

    public void UpdateBalance(decimal amount)
    {
        ValidateAmount(amount);

        Balance += amount;
    }

    private void ValidateAmount(decimal amount)
    {
        if (amount + Balance < 0)
        {
            throw new ArgumentException("Attempt to withdraw amount greater than current balance");
        }
    }
}