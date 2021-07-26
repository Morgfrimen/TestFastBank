﻿using System;
using System.Collections.Generic;
using System.Linq;

using Банкомат.Data;
using Банкомат.Money;

namespace Банкомат.Bank
{
    internal sealed class Bank
    {
        private MoneyBase[] _money;

        internal IDictionary<MoneyBase, uint> Cache { get; }

        internal void AddMoney(MoneyBase money, uint count)
        {
            this.AddMoneyEvent.Invoke(money, count);
        }

        internal void RemoveMoney(decimal sum)
        {
            _money ??= Cache.Keys.OrderByDescending(item=>item.ValueMoney).ToArray();

            for (int iteration = 0; iteration < MoneyBase.CountAllMoneyValue; iteration++)
            {
                sum = RemoveOneValue(_money[iteration], sum);
                if(sum == 0)
                    break;
            }

        }


        private decimal RemoveOneValue(MoneyBase money, decimal sum)
        {
            uint countMoneyValue = (uint) Math.Floor(sum / money.ValueMoney);

            if (!this.RemoveMoneyEvent.Invoke(money, countMoneyValue)) 
                throw new Exception("Нельзя удалять!");

            return sum - countMoneyValue * money.ValueMoney;
        }

#region Singleton

        private Bank()
        {
            Cache = new TXTData("TestData.txt").GetMoney();
            this.AddMoneyEvent += OnAddMoneyEvent;
            this.RemoveMoneyEvent += OnRemoveMoneyEvent;
        }

        private static Bank _instance;

        internal static Bank Instance
        {
            get
            {
                _instance ??= new();

                return _instance;
            }
        }

#endregion

        private bool OnRemoveMoneyEvent(MoneyBase arg1, uint arg2)
        {
            uint countBank = Cache[arg1];

            try
            {
                uint newCount = checked(countBank - arg2);
            }
            catch (OverflowException)
            {
                this.NoMoneyEvent?.Invoke();

                return false;
            }

            return true;
        }

        private void OnAddMoneyEvent(MoneyBase obj, uint count)
        {
            Cache.Add(obj, count);
        }


        private event Func<MoneyBase, uint,bool> RemoveMoneyEvent;
        private event Action<MoneyBase, uint> AddMoneyEvent;
        internal event Action NoMoneyEvent;
    }
}