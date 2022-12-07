using System;
using static System.Console;


namespace CSharp7._8Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            bool correct;
            string path = @"Repository.txt";
            Repository repository = new Repository(path);
            repository.FileExistCheck();

            for (int i = 0; i < int.MaxValue; i++)
            {
                WriteLine("Что вы желаете сделать? \n1 - Отобразить список сотрудников, \n2 - Добавить новую запись," +
                    " \n3 - Найти сотрудника по ID, \n4 - Удалить сотрудника, \n5 - Отобразить сотрудников в диапазоне дат," +
                    " \n6 - Выйти из программы");
                correct = byte.TryParse(ReadLine(), out byte choose);
                if (correct)
                {
                    switch (choose)
                    {
                        case 1:
                            repository.ShowInfo(repository.GetAllWorkers());
                            WriteLine();
                            Write("Желаете отсортировать? y/n ");
                            char key = Convert.ToChar(ReadLine());
                            WriteLine();
                            if (key=='y')
                            {
                                {
                                    repository.SortBy();
                                    WriteLine();
                                    Write("Продолжить сортировку? y/n ");
                                    key = ReadKey(true).KeyChar;
                                } while (char.ToLower(key) == 'y') ;
                                WriteLine();
                            }
                            break;
                        case 2:
                            repository.AddWorker();
                            WriteLine();
                            break;
                        case 3:
                            Write("Введите ID сотрудника : ");
                            WriteLine((repository.GetWorkerByID(int.Parse(ReadLine()))).Show());
                            break;
                        case 4:
                            Write("Введите ID сотрудника, запись о котором ходите удалить : ");
                            repository.DeleteWorker(int.Parse(ReadLine()));
                            break;
                        case 5:
                            WriteLine("Введите начальную и конечную даты через Enter в формате dd.mm.yyyy: ");
                            repository.ShowInfo(repository.GetWorkersBetweenTwoDates
                                (Convert.ToDateTime(ReadLine()),Convert.ToDateTime(ReadLine())));
                            break;
                        case 6:
                            System.Environment.Exit(0);
                            break;
                        default:
                            WriteLine("Такого параметра не существует");
                            break;
                    }
                }
            }
        }
    }
}
