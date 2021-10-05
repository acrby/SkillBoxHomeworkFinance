using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxHomeworkFinance
{
    class Record
    {
        public int Month { get; set; }
        public int Income { get; set; }
        public int Consumption { get; set; }
        public int Profit { get; set; }
        public bool BadMonth { get; set; }

        public Record(int Month, int Income, int Consumption, int Profit, bool BadMonth)
        {
            this.Month = Month;
            this.Income = Income;
            this.Consumption = Consumption;
            this.Profit = Profit;
            this.BadMonth = BadMonth;
        }

        public Record(int Month, int Income, int Consumption) : this(Month, Income, Consumption, 0, false)
        {
        }

        public void PrintResults(bool marksBadMonthes)
        {
            if (marksBadMonthes)
            {

                if (this.BadMonth == true)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{this.Month,-5} {this.Income,-20} {this.Consumption,-20} {this.Profit,-20}");
                    Console.ResetColor();
                }
                else
                    Console.WriteLine($"{this.Month,-5} {this.Income,-20} {this.Consumption,-20} {this.Profit,-20}");
            } else
                Console.WriteLine($"{this.Month,-5} {this.Income,-20} {this.Consumption,-20} {this.Profit,-20}");
        }
    }
}
