using System;

namespace DIO.Bank.Entities
{
    public class BankAccount
    {
        public string Name { get; private set; }
        public double Credit { get; private set; }
        public double Balance { get; private set; }
        public AccountType AccountType { get; private set; }

        public BankAccount(string name, double credit, double balance, AccountType accountType)
        {
            this.Name = name;
            this.Credit = credit;
            this.Balance = balance;
            this.AccountType = accountType;
        }

        public bool Withdraw(double value)
        {
            if (this.Balance - value < (this.Credit * -1))
            {
                return false;
            }

            this.Balance -= value;

            return true;
        }

        public void Deposit(double value)
        {
            this.Balance += value;
        }

        public void Transfer(double value, BankAccount bankAccount)
        {
            if (this.Withdraw(value))
                bankAccount.Deposit(value);
        }

        public override string ToString()
        {
            string text = "";

            text += $"Tipo de Conta: {this.AccountType} | ";
            text += $"Nome: {this.Name} | ";
            text += $"Saldo: {this.Balance} | ";
            text += $"Crédito: {this.Credit}";

            return text;
        }
    }
}