using System;

using Банкомат.Money;

namespace Банкомат
{
    internal class Program
    {
        private static bool exit;

        private static void AddMoney()
        {
            try
            {
                Console.WriteLine("Какую купюру вы вводите? (100,1000,20,50,500)");
                uint value = Convert.ToUInt32(Console.ReadLine());
                MoneyBase money = value switch
                {
                    20  => new Money20(), 50    => new Money50(), 100 => new Money100(),
                    500 => new Money500(), 1000 => new Money1000(), _ => null
                };

                if (money is null)
                {
                    Console.WriteLine("такой купюры нет!");

                    return;
                }

                Console.WriteLine("Какой количество?");
                uint count = Convert.ToUInt32(Console.ReadLine());
                Bank.Bank.Instance.AddMoney(money, count);
                Console.WriteLine("Успех!");
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка!");
            }
        }


        private static void Command(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.D1:
                    AddMoney();

                    break;

                case ConsoleKey.D2:
                    RemoveMoney();

                    break;

                case ConsoleKey.D0:
                    exit = true;

                    break;
            }
        }

        private static void InstanceOnNoMoneyEvent()
        {
            Console.WriteLine("Снять столько денег не возможно! Отмена операции");
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в банк!");
            while (!exit)
            {
                Console.WriteLine("Что Вы хотите сделать?");
                Console.WriteLine("Добавить деньги: нажмите 1");
                Console.WriteLine("Снять деньги: нажмите 2");
                Console.WriteLine("Выйти: нажмите 0");
                Command(Console.ReadKey().Key);
            }
        }

        private static void RemoveMoney()
        {
            try
            {
                Console.WriteLine("Какую сумму выводите?");
                decimal value = Convert.ToDecimal(Console.ReadLine());
                Bank.Bank.Instance.RemoveMoney(value);
                Console.WriteLine("Успех!");
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка!");
            }
        }

        static Program() => Bank.Bank.Instance.NoMoneyEvent += InstanceOnNoMoneyEvent;
    }
}