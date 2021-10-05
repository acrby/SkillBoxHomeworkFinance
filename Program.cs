using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace SkillBoxHomeworkFinance
{
    class Program
    {

        /// <summary>
        /// Самописный метод Convert для строки со спецсимволами
        /// </summary>
        /// <param name="str">Строка-вхождение с числами и спецсимволами</param>
        /// <param name="digit">out-ссылка на число int в которое конвертируемся</param>
        /// <returns></returns>
        static bool StringToDigitConvert(string str, out int digit)
        {
            bool completed = false;
            string bufferStr = "";

            #region Разбираем число из ячейки и фильтруем только цифры, для корректной конверсии
            for (int index = 0; index < str.Length; index++)
            {
                if (Char.IsDigit(str[index]))
                    bufferStr += str[index];
            }
            digit = Convert.ToInt32(bufferStr);
            #endregion

            return completed;
        }

        /// <summary>
        /// Самописная функция чтения файла
        /// </summary>
        /// <param name="filename">Имя файла с расширением, которое будет прочитано в директории Решения</param>
        static List<Record> ReadAndCollectFile(string filename)
        {
            List<Record> records = new List<Record>();
            #region Чтение и запись в коллекцию
            using (StreamReader data = new StreamReader(filename, Encoding.UTF8))
            {

                string line;
                int month, income, consumption, profit;
                while (!data.EndOfStream)
                {
                    data.ReadLine();
                    while ((line = data.ReadLine()) != null)
                    {
                        string[] datastring = line.Split(',');
                        StringToDigitConvert(datastring[0], out month);
                        StringToDigitConvert(datastring[1], out income);
                        StringToDigitConvert(datastring[2], out consumption);
                        profit = income - consumption;
                        records.Add(new Record(month, income, consumption, profit, false));
                        // Console.WriteLine($"{month,-5} {income,-20} {consumption,-20} {profit,-20}");
                    }
                }
            }
            #endregion
            return records;
        }

        static bool FindBadMonthes(ref List<Record> records)
        {
            const int TARGET_OF_MIN_PROFIT_MONTH = 3; // Настройка количества месяцев с MIN-profit, которое требуется найти
            List<int> minProfits = new List<int>();
            List<int> minMonthes = new List<int>();
            // List<int> minMonthesDubles = new List<int>();
            bool finded = false;

            int yandex = 0;

            #region Поиск ВСЕХ Bad-месяцев и его дублей
            while (yandex < TARGET_OF_MIN_PROFIT_MONTH)
            {
                #region Поиск ОДНОГО Bad-месяца и его дублей
                minProfits.Add(records[0].Profit);
                minMonthes.Add(records[0].Month);

                for (int index = 1; index < records.Count; index++)
                {
                    if ((minProfits[yandex] > records[index].Profit) && (!records[index].BadMonth))
                    {
                        minProfits[yandex] = records[index].Profit;
                        minMonthes[yandex] = records[index].Month;
                        finded = true;
                    }

                }

                records[minMonthes[yandex] - 1].BadMonth = true;

                for (int index = 0; index < records.Count; index++)
                {
                    if ((minProfits[yandex] == records[index].Profit) && (minMonthes[yandex] != records[index].Month) && (!records[index].BadMonth))
                        records[records[index].Month - 1].BadMonth = true;
                }
                #endregion

                yandex++;
            }
            #endregion

            return finded;
        }

        static void Main(string[] args)
        {
            string filename = "Test.csv";
            List<Record> records = ReadAndCollectFile(filename);
            bool finded = FindBadMonthes(ref records);

            Console.WriteLine($"{"Месяц",-5} {"Доход",-20} {"Расходы",-20} {"Прибыль",-20}");
            foreach (Record record in records)
                record.PrintResults(finded);

            Console.ReadKey();
        }
    }
}
