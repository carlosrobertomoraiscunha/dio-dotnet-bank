using System;
using DIO.Bank.Entities;
using DIO.Bank.Menu;
using System.Collections.Generic;

namespace DIO.Bank
{
    class Program
    {
        static List<BankAccount> bankAccounts = new List<BankAccount>();

        static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            ConsoleKeyInfo pressedKey;

            PrintMenu();

            do
            {
                pressedKey = Console.ReadKey(true);

                if (int.TryParse(pressedKey.KeyChar.ToString(), out int result))
                {
                    if (Enum.TryParse(typeof(MenuOptions), result.ToString(), out object option))
                    {
                        SelectOption((MenuOptions)option);
                    }
                }
            } while (pressedKey.Key != ConsoleKey.Escape);
        }

        private static void SelectOption(MenuOptions option)
        {
            Console.Clear();
            Console.CursorVisible = true;

            switch (option)
            {
                case MenuOptions.Listar:
                    ListAllBankAccounts();
                    break;
                case MenuOptions.Inserir:
                    CreateNewBankAccount();
                    break;
                case MenuOptions.Transferir:
                    Transfer();
                    break;
                case MenuOptions.Sacar:
                    Withdraw();
                    break;
                case MenuOptions.Depositar:
                    Deposit();
                    break;
            }

            Console.WriteLine();
            Console.Write("Aperte qualquer tecla para continuar!");
            Console.ReadKey(true);
            Console.Clear();

            PrintMenu();
        }

        private static void Transfer()
        {
            Console.Write("Digite o número da conta de origem: ");
            int indexSource = int.Parse(Console.ReadLine());

            Console.Write("Digite o número da conta de destino: ");
            int indexTarget = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor a ser transferido: ");
            double value = double.Parse(Console.ReadLine());

            bankAccounts[indexSource].Transfer(value, bankAccounts[indexTarget]);
            Console.WriteLine("Transferência realizada com sucesso!");
        }

        private static void Deposit()
        {
            Console.Write("Digite o número da conta: ");
            int index = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor a ser depositado: ");
            double value = double.Parse(Console.ReadLine());

            bankAccounts[index].Deposit(value);
            Console.WriteLine("Depósito realizado com sucesso! O saldo atual da conta de {1} é {0}", bankAccounts[index].Balance, bankAccounts[index].Name);
        }

        private static void Withdraw()
        {
            Console.Write("Digite o número da conta: ");
            int index = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor a ser sacado: ");
            double value = double.Parse(Console.ReadLine());

            if (bankAccounts[index].Withdraw(value))
                Console.WriteLine("Saque com sucesso! Seu saldo atual é {0}", bankAccounts[index].Balance);
            else
                Console.WriteLine("Saldo insuficiente!");
        }

        private static void CreateNewBankAccount()
        {
            Console.WriteLine("Cadastrar Nova Conta");

            Console.Write("Digite 1 para conta física ou 2 para conta jurídica: ");
            int accountType = int.Parse(Console.ReadLine());

            Console.Write("Digite o nome do cliente: ");
            string name = Console.ReadLine();

            Console.Write("Digite o saldo inicial: ");
            double balance = double.Parse(Console.ReadLine());

            Console.Write("Digite o crédito: ");
            double credit = double.Parse(Console.ReadLine());

            BankAccount newBankAccount = new BankAccount(accountType: (AccountType)accountType,
                                                        name: name,
                                                        balance: balance,
                                                        credit: credit);

            bankAccounts.Add(newBankAccount);

            Console.WriteLine();
            Console.Write("Conta cadastrada com sucesso!");
        }

        private static void ListAllBankAccounts()
        {
            Console.WriteLine("Lista de Contas");
            Console.WriteLine();

            if (bankAccounts.Count == 0)
            {
                Console.WriteLine("Nenhum banco cadastrado!");
            }

            for (int i = 0; i < bankAccounts.Count; i++)
            {
                Console.WriteLine($"#{i} - {bankAccounts[i]}");
            }
        }

        private static void PrintMenu()
        {
            Console.CursorVisible = false;

            Console.WriteLine("DIO Bank a seu dispor!!!");
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine();

            foreach (var serieOption in Enum.GetValues(typeof(MenuOptions)))
            {
                Console.WriteLine($"{(int)serieOption} - {serieOption}");
            }

            Console.WriteLine("Esc - Sair");
        }
    }
}
