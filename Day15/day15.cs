using System;

namespace HeightAddition
{
    class Height
    {
        public int Feet { get; set; }
        public double Inches { get; set; }

        public Height()
        {
            Feet = 0;
            Inches = 0.0;
        }

        public Height(int feet, double inches)
        {
            Feet = feet;
            Inches = inches;
        }

        public Height AddHeights(Height h2)
        {
            int totalFeet = this.Feet + h2.Feet;
            double totalInches = this.Inches + h2.Inches;

            if (totalInches >= 12)
            {
                totalFeet += (int)(totalInches / 12);
                totalInches = totalInches % 12;
            }

            return new Height(totalFeet, totalInches);
        }

        public override string ToString()
        {
            return $"Height - {Feet} feet {Inches} inches";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Height person1 = new Height(5, 6.5);
            Height person2 = new Height(5, 7.5);

            Height totalHeight = person1.AddHeights(person2);

            Console.WriteLine(person1);
            Console.WriteLine(person2);
            Console.WriteLine(totalHeight);
        }
    }
}
