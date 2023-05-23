// See https://aka.ms/new-console-template for more information

TransportCard.TransportCard card = new TransportCard.TransportCard();

card.TransportCardEvent += message =>
{
    Console.WriteLine(message);
};

card.AddPredicate(balance => balance >= 100);
card.AddCashbackFunc(balance => balance >= 100 ? 10 : 0);
card.AddPaymentHistory(message => Console.WriteLine(message));

card.Replenish(50); // + 50 rub. Current balance: 50
card.Payment(); // Not enough funds on the card
card.Replenish(100); // + 100 rub. Current balance: 150
card.Payment(); // Cashback - 10 rub. Payment of 30 rub successful. Current balance: 120
