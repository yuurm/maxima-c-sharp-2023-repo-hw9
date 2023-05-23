namespace TransportCard
{
    using System;

    public partial class TransportCard
    {
        private int balance = 0;
        public delegate void TransportCardEventHandler(string message);

        public event TransportCardEventHandler TransportCardEvent;

        public void Replenish(int amount)
        {
            balance += amount;
            TransportCardEvent?.Invoke($"Card replenished with {amount} rubles. Current balance: {balance}");
        }
    }

    public partial class TransportCard
    {
        public bool Payment(int amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
                TransportCardEvent?.Invoke($"Payment of {amount} rubles successful. Current balance: {balance}");
                return true;
            }
            else
            {
                TransportCardEvent?.Invoke("Not enough funds on the card");
                return false;
            }
        }
    }

    public partial class TransportCard
    {
        public void AddPredicate(Func<int, bool> predicate)
        {
            if (predicate != null)
            {
                TransportCardEvent += message =>
                {
                    if (predicate(balance))
                    {
                        Console.WriteLine(message);
                    }
                };
            }
        }

        public void AddCashbackFunc(Func<int, int> cashbackFunc)
        {
            if (cashbackFunc != null)
            {
                TransportCardEvent += message =>
                {
                    int cashback = cashbackFunc(balance);
                    if (cashback > 0)
                    {
                        Console.WriteLine($"Cashback of {cashback} rubles. {message}");
                    }
                    else
                    {
                        Console.WriteLine(message);
                    }
                };
            }
        }

        public void AddPaymentHistory(Action<string> paymentHistoryFunc)
        {
            if (paymentHistoryFunc != null)
            {
                TransportCardEvent += message =>
                {
                    paymentHistoryFunc($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} - {message}");
                };
            }
        }
    }
}
