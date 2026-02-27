using System.Buffers;

namespace ClassWork
{
    class Order
    {
        private int _orderid;
        private string _customerName;
        private decimal _totalAmount;
        private bool _discounted;
        private string _status;
        private DateTime _currDate;


        public Order()
        {
            _currDate = DateTime.Now;
            _totalAmount = 0;
            _discounted = false;
            _status = "New";
        }
        public Order(in int _orderid, string _customerName)
        {
            this._orderid = _orderid;
            this._customerName = _customerName;
            _status = "New";
            _totalAmount = 0;
            _discounted = false;
            _currDate = DateTime.Now;
        }
        public int orderid
        {
            get { return _orderid; }
        }
        public string customerName
        {
            get { return _customerName; }

            set
            {
                if (string.IsNullOrWhiteSpace(value))

                    _customerName = value;
            }
        }

        public decimal totalAmount
        {
            get { return _totalAmount; }
        }

        public void Additem(decimal Price)
        {
            _totalAmount += Price;
        }
        public void ApplyDiscount(decimal Percentage)
        {
            if (!_discounted && Percentage >= 1 && Percentage <= 30)
            {
                decimal discount = _totalAmount * (Percentage) / 100;
                _totalAmount -= discount;
                _discounted = true;
            }
            else
            {
                Console.WriteLine("Discount is Already applied");
            }
        }

        public string getOrderSummary()
        {
            return $"orderID: {_orderid}\n" +
                $"Customer: {_customerName}\n" +
                $"Total Amount :{_totalAmount}\n" +
                $"Status: {_status} ";

        }

        static void Main(string[] args)
        {
            Order o1 = new Order(101, "Ekta");
            o1.Additem(1);
            o1.Additem(1500);
            o1.Additem(425);
            o1.ApplyDiscount(30);
            o1.ApplyDiscount(20);

            Console.WriteLine(o1.getOrderSummary());
            Order o2 = new Order();
            o2.customerName = "Manvi";


        }
    }
}