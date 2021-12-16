using System;
using System.IO;
using System.Numerics;

namespace CreditCalculator
{
    class Program
    {
        // Проверяет, что число положительное, не равно нулю и не выходит за пределы диапазона типа.
        static bool IsNumberAvailable(out int number)
        {
            try
            {
                number = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                number = 0;
                return false;
            }

            if (number <= 0)
                return false;
            return true;
        }
        static bool IsNumberAvailable(out double number)
        {
            try
            {
                number = Convert.ToDouble(Console.ReadLine());
            }
            catch
            {
                number = 0;
                return false;
            }

            if (number <= 0)
                return false;
            return true;
        }

        /// <summary>
        ///  Выводит лого.
        /// </summary>
        static void Logo()
        {
            Console.WriteLine("################################################");
            Console.WriteLine("# Калькулятор для расчёта аннуитетного платежа #");
            Console.WriteLine("################################################\n");
        }
        static void LogoMenu()
        {
            Console.WriteLine("################################################\t|\t'старт' для запуска калькулятора");
            Console.WriteLine("# Калькулятор для расчёта аннуитетного платежа #\t|\t'выход' для выхода");
            Console.WriteLine("################################################\t|\tВыполнил Маслов Т. 803в2\n");
        }

        /// <summary>
        /// Запускает калькулятор.
        /// </summary>
        static void Calculator()
        {
            Console.Clear();
            Logo();

            // Позволяет изменить введённые ранее данные. 
            static void Edit(ref int loanAmount, ref double loanRate, ref int loanTerm)
            {
                string answer;

                Console.WriteLine("Какой из пунктов вы хотите изменить?\n" +
                    "1. Размер кредита.\n" +
                    "2. Ставка по кредиту.\n" +
                    "3. Срок займа.");
                Console.Write("Введите номер пункта: ");


                while (true)
                {
                    answer = Console.ReadLine();
                    switch (answer)
                    {
                        case "1":
                            Console.Write("Укажите сумму кредита: ");
                            while (!IsNumberAvailable(out loanAmount))
                            {
                                Console.WriteLine("Ошибка ввода. Число имеет неверный формат. Проверьте, что число положительное, не равно\n" +
                                    "нулю и не содержит посторонние символы.");
                                Console.Write("Укажите сумму кредита: ");
                            }
                            break;
                        case "2":
                            Console.Write("Укажите годовую ставку по кредиту: ");
                            while (!IsNumberAvailable(out loanRate))
                            {
                                Console.WriteLine("Ошибка ввода. Число имеет неверный формат. Проверьте, что число положительное, не равно\n" +
                                    "нулю и не содержит посторонние символы.");
                                Console.Write("Укажите годовую ставку по кредиту: ");
                            }
                            break;
                        case "3":
                            Console.Write("Введите число обозначающее срок кредитования в удобном вам формате (далее вы укажите, введённое число является" +
                " количеством месяцев или лет): ");
                            while (!IsNumberAvailable(out loanTerm))
                            {
                                Console.WriteLine("Ошибка ввода. Число имеет неверный формат. Проверьте, что число положительное, не равно\n" +
                                    "нулю и не содержит посторонние символы.");
                                Console.Write("Введите число обозначающее срок кредитования в удобном вам формате (далее вы укажите, введённое число является" +
                " количеством месяцев или лет): ");
                            }

                            Console.WriteLine("Месяц или год? (месяц/год)");
                            while (true)
                            {
                                answer = Console.ReadLine();
                                switch (answer)
                                {
                                    case "месяц":
                                    case "м":
                                    case "m":
                                    case "month":
                                        break;
                                    case "год":
                                    case "г":
                                    case "year":
                                    case "y":
                                        loanTerm *= 12;
                                        break;
                                    default:
                                        Console.WriteLine("Ошибка ввода. Введите 'м', если вы указали количество месяцев\n или введите 'г'," +
                                            " если указали количество лет.");
                                        continue;
                                }
                                break;
                            }
                            break;
                        case "отмена":
                        case "cancel":
                        case "c":
                            return;
                        default:
                            Console.WriteLine("Ошибка ввода. Введите номер пункта который хотите изменить (1, 2, 3).\n" +
                                "Если вы не хотите ничего менять -- напишите 'отмена'. ");
                            continue;
                    }
                    break;
                }
            }

            // Выводит введённые данные и спрашивает у пользователя их корректность.
            static void ShowAndCheckInfo(ref int loanAmount, ref double loanRate, ref int loanTerm)
            {
                string answer;

                Console.Clear();
                Logo();

                Console.WriteLine("Проверьте корректность введёных данных:\n" + "Cумма кредита: " + loanAmount + " рублей" +
                    "\nГодовая ставка по кредиту: " + loanRate + "%");
                Console.Write("Срок кредита: ");

                if (loanTerm >= 12)
                {
                    switch ((loanTerm / 12) % 10)
                    {
                        case 1:
                            Console.Write(loanTerm / 12 + " год");
                            break;
                        case 2:
                        case 3:
                        case 4:
                            Console.Write(loanTerm / 12 + " года");
                            break;
                        default:
                            Console.Write(loanTerm / 12 + " лет");
                            break;
                    }
                    switch (loanTerm % 12)
                    {
                        case 0:
                            Console.WriteLine();
                            break;
                        case 1:
                            Console.WriteLine(" и 1 месяц");
                            break;
                        case 2:
                        case 3:
                        case 4:
                            Console.WriteLine(" и " + loanTerm % 12 + " месяца");
                            break;
                        default:
                            Console.WriteLine(" и " + loanTerm % 12 + " месяцев");
                            break;
                    }
                }
                else
                {

                    switch (loanTerm)
                    {
                        case 1:
                            Console.WriteLine("1 месяц");
                            break;
                        case 2:
                        case 3:
                        case 4:
                            Console.WriteLine(loanTerm + " месяца");
                            break;
                        default:
                            Console.WriteLine(loanTerm + " месяцев");
                            break;
                    }
                }

                Console.WriteLine("Данные корректны? ");

                while (true)
                {
                    answer = Console.ReadLine();
                    switch (answer)
                    {
                        case "да":
                        case "д":
                        case "yes":
                        case "y":
                            break;
                        case "нет":
                        case "н":
                        case "no":
                        case "n":
                            Edit(ref loanAmount, ref loanRate, ref loanTerm);
                            ShowAndCheckInfo(ref loanAmount, ref loanRate, ref loanTerm);
                            break;
                        default:
                            Console.WriteLine("Ошибка ввода. Напишите 'да', если данные верны или 'нет', если неверны\n" +
                                "и вы хотите их изменить.");
                            continue;
                    }
                    break;
                }
            }

            static void SetDate(out DateTime dateTime)
            {
                int day, month, year;
                Console.Write("Требуется ввести дату получения кредита.\nУкажите число: ");

                while (!IsNumberAvailable(out day) || day > 31)
                {
                    Console.WriteLine("Ошибка ввода. Число имеет неверный формат. Проверьте, что число положительное, не равно\n" +
                        "нулю и не содержит посторонние символы.");
                    Console.Write("Укажите число: ");
                }

                Console.Write("Укажите номер месяца: ");
                while (!IsNumberAvailable(out month) || month > 12)
                {
                    Console.WriteLine("Ошибка ввода. Число имеет неверный формат. Проверьте, что число положительное, не равно\n" +
                        "нулю и не содержит посторонние символы.");
                    Console.Write("Укажите номер месяца: ");
                }
                Console.Write("Укажите год: ");
                while (!IsNumberAvailable(out year))
                {
                    Console.WriteLine("Ошибка ввода. Число имеет неверный формат. Проверьте, что число положительное, не равно\n" +
                        "нулю и не содержит посторонние символы.");
                    Console.Write("Укажите год: ");
                }

                dateTime = new DateTime(year, month, day);

                Console.WriteLine($"Вы указали: {dateTime.Day}.{dateTime.Month}.{dateTime.Year}");

            }

            static void ShowGraphic(ref int loanAmount, ref double loanRate, ref int loanTerm, ref double monthlyPayment)
            {
                double
                // Остаток суммы кредита.
                loanBalance = loanAmount,
                // Доля процента.
                fractionOfPercent,
                // Тело кредита.
                loanBody,
                sumLoan = 0,
                sumRate = 0;
                //loanBalance -= loanBody;

                DateTime dateTime;
                SetDate(out dateTime);

                string writePath = @"\GraphPayments.txt";

                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {


                    Console.WriteLine("\nДата\t\tПлатёж\t\tПроценты\tТело кредита\tОстаток");
                    Console.WriteLine("---------------------------------------------------------------");
                    Console.WriteLine(dateTime.Day + "." + dateTime.Month + "." + dateTime.Year + "\t" + 0 + "\t\t" + 0
                            + "\t\t" + 0 + "\t\t" + Math.Round(loanBalance, 2));

                    sw.WriteLine("\nДата\t\tПлатёж\t\tПроценты\tТело кредита\tОстаток");
                    sw.WriteLine("---------------------------------------------------------------");
                    sw.WriteLine(dateTime.Day + "." + dateTime.Month + "." + dateTime.Year + "\t" + 0 + "\t\t" + 0
                            + "\t\t" + 0 + "\t\t" + Math.Round(loanBalance, 2));

                    for (int i = 1; i <= loanTerm; i++)
                    {
                        dateTime = dateTime.AddMonths(1);
                        fractionOfPercent = loanBalance * (loanRate / 100 / 12);
                        loanBody = monthlyPayment - fractionOfPercent;
                        loanBalance -= loanBody;


                        if (loanBalance < 0)
                            loanBalance = 0;

                        Console.WriteLine(dateTime.Day + "." + dateTime.Month + "." + dateTime.Year + "\t" + Math.Round(monthlyPayment, 2) + "\t\t" + Math.Round(fractionOfPercent, 2)
                            + "\t\t" + Math.Round(loanBody, 2) + "\t\t" + Math.Round(loanBalance, 2));
                        sw.WriteLine(dateTime.Day + "." + dateTime.Month + "." + dateTime.Year + "\t" + Math.Round(monthlyPayment, 2) + "\t\t" + Math.Round(fractionOfPercent, 2)
                            + "\t\t" + Math.Round(loanBody, 2) + "\t\t" + Math.Round(loanBalance, 2));

                        sumLoan += loanBody;
                        sumRate += fractionOfPercent;
                    }
                    Console.WriteLine("---------------------------------------------------------------");
                    Console.Write("\t\t" + Math.Round(sumRate, 2) + "\t\t" + Math.Round(sumLoan, 2) + "\n\n");
                    sw.WriteLine("---------------------------------------------------------------");
                    sw.Write("\t\t" + Math.Round(sumRate, 2) + "\t\t" + Math.Round(sumLoan, 2) + "\n\n");
                }

                Console.WriteLine($"График был сохранён в файле {writePath}.");

            }

            double
                // Размер ежемесячного платежа.
                monthlyPayment,
                // Месячная процентная ставка.
                monthlyInterestRate,
                // Ставка по кредиту.
                loanRate,
                // Итоговая сумма.
                endSum;
            int
                // Размер кредита.
                loanAmount,
                // Срок займа.
                loanTerm;

            string answer; // Команда пользователя.            

            // Ввод суммы кредита.
            Console.Write("Укажите сумму кредита: ");
            while (!IsNumberAvailable(out loanAmount))
            {
                Console.WriteLine("Ошибка ввода. Число имеет неверный формат. Проверьте, что число положительное, не равно\n" +
                    "нулю и не содержит посторонние символы.");
                Console.Write("Укажите сумму кредита: ");
            }

            // Ввод ставки по кредиту.
            Console.Write("Укажите годовую ставку по кредиту: ");
            while (!IsNumberAvailable(out loanRate))
            {
                Console.WriteLine("Ошибка ввода. Число имеет неверный формат. Проверьте, что число положительное, не равно\n" +
                    "нулю и не содержит посторонние символы.");
                Console.Write("Укажите годовую ставку по кредиту: ");
            }

            // Ввод срока займа.
            Console.Write("Введите число обозначающее срок кредитования в удобном вам формате (далее вы укажите, введённое число является" +
                " количеством месяцев или лет): ");
            while (!IsNumberAvailable(out loanTerm))
            {
                Console.WriteLine("Ошибка ввода. Число имеет неверный формат. Проверьте, что число положительное, не равно\n" +
                    "нулю и не содержит посторонние символы.");
                Console.Write("Укажите срок кредита: ");
            }

            // Уточняет, что ввёл пользователь: количество лет или месяцев.
            Console.WriteLine("Месяц или год? (месяц/год)");
            while (true)
            {
                answer = Console.ReadLine();
                switch (answer)
                {
                    case "месяц":
                    case "м":
                    case "m":
                    case "month":
                        break;
                    case "год":
                    case "г":
                    case "year":
                    case "y":
                        loanTerm *= 12;
                        break;
                    default:
                        Console.WriteLine("Ошибка ввода. Введите 'м', если вы указали количество месяцев\n или введите 'г'," +
                            " если указали количество лет.");
                        continue;
                }
                break;
            }

            ShowAndCheckInfo(ref loanAmount, ref loanRate, ref loanTerm);

            monthlyInterestRate = loanRate / 100 / 12;

            monthlyPayment = loanAmount * (monthlyInterestRate + (monthlyInterestRate / (Math.Pow(1 + monthlyInterestRate, loanTerm) - 1)));

            Console.WriteLine("Ежемесяный платёж равен: " + Math.Round(monthlyPayment, 2) + " рублей.");
            endSum = loanTerm * Math.Round(monthlyPayment, 2);
            Console.WriteLine("Всего выплат: " + Math.Round(endSum, 2) + " рублей.");
            Console.WriteLine("Переплата: " + Math.Round(Math.Round(endSum, 2) - loanAmount, 2) + " рублей.");

            Console.WriteLine("Отобразить график платежей? (да/нет)");
            while (true)
            {
                answer = Console.ReadLine();
                switch (answer)
                {
                    case "да":
                    case "д":
                    case "yes":
                    case "y":
                        ShowGraphic(ref loanAmount, ref loanRate, ref loanTerm, ref monthlyPayment);
                        break;
                    case "нет":
                    case "н":
                    case "no":
                    case "n":
                        break;
                    default:
                        Console.WriteLine("Оштбка ввода. Ввидите 'да', если желаете отобразить график платежей\n" +
                            "или 'нет', если не желаете.");
                        continue;
                }
                break;
            }

            Console.WriteLine("Завершение. Нажмите Enter.");

            Console.ReadLine();
        }

        /// <summary>
        /// Запускает меню программы.
        /// </summary>
        static void Menu()
        {
            // Комманда от пользователя.
            string command;

            LogoMenu();

            while (true)
            {
                Console.Write("> ");
                command = Console.ReadLine();

                switch (command)
                {
                    case "exit":
                    case "ex":
                    case "e":
                    case "выход":
                    case "в":
                        return;
                    case "start":
                    case "st":
                    case "s":
                    case "старт":
                    case "с":
                        Calculator();
                        Console.Clear();
                        LogoMenu();
                        break;
                    default:
                        Console.WriteLine("Ошибка ввода.");
                        break;
                }
            }

        }
        static void Main(string[] args)
        {
            Menu();
        }
    }
}
