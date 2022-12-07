using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CSharp7._8Practice
{
    public class Repository
    {
        private Worker[] workers;
        private string path;

        public Repository(string path)
        {
            this.path = path;
            this.workers = new Worker[500];
        }

        /// <summary>
        /// Метод, для отображения инфы в консоли
        /// </summary>
        public void ShowInfo(Worker[] workers)
        {
            WriteLine("{0,-5}{1,-20}{2,-30}{3,-10}{4,-5}{5,-20}{6,-25}","ID",
                "Запись добавлена","Ф.И.О.","Возраст","Рост","Дата рождения","Место рождения");
            for (int i = 0; i < workers.Length; i++)
            {
                WriteLine(workers[i].Show());
            }
        }

        /// <summary>
        /// Метод, удаляющий информацию о сотруднике с указанным ID
        /// </summary>
        /// <param name="id"></param>
        public void DeleteWorker(int id)
        {
            int delI = 0;
            Worker needToDeleteWorker = GetWorkerByID(id);
            Worker[] newWorkers = new Worker[workers.Length];
            for (int i = 0; i < workers.Length; i++)
            {
                if (workers[i].ID == needToDeleteWorker.ID)
                {
                    delI = i;
                    break;
                }
                newWorkers[i] = workers[i];
            }
            for (int i = delI + 1; i < workers.Length; i++)
            {
                newWorkers[i - 1] = workers[i];
            }
            WriteLine($"Сотрудник {needToDeleteWorker.ID} {needToDeleteWorker.FIO} удален");
            using (StreamWriter removeWorker = new StreamWriter(path, false))
            {
                for (int i = 0; i < newWorkers.Length - 1; i++)
                {
                    removeWorker.WriteLine($"{newWorkers[i].ID}#{newWorkers[i].WriteTime}#" +
                        $"{newWorkers[i].FIO}#{newWorkers[i].Age}#{newWorkers[i].Growth}#" +
                        $"{newWorkers[i].DayOfBirth}#{newWorkers[i].CityOfBirth}");
                }
            }
        }

        /// <summary>
        /// Метод, считывающий информацию о сотруднике с указанным ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Worker GetWorkerByID(int id)
        {
            Worker workerWithID = new Worker();
            workers = GetAllWorkers();
            for (int i = 0; i < workers.Length; i++)
            {
                if (workers[i].ID == id)
                {
                    workerWithID = workers[i];
                    break;
                }
            }
            return workerWithID;
        }

        /// <summary>
        /// Метод, отображающий сотрудников, внесенных между 2 датами
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            workers = GetAllWorkers();
            Worker[] newWorkers = new Worker[workers.Length];
            int i = 0;
            for (int j = 0; j < workers.Length; j++)
            {
                if (Convert.ToDateTime(workers[j].WriteTime) >= dateFrom 
                    && Convert.ToDateTime(workers[j].WriteTime) <= dateTo)
                {
                    newWorkers[i] = workers[j];
                    i++;
                }
            }
            return newWorkers;
        }

        /// <summary>
        /// Метод сортировки по столбцам
        /// </summary>
        public void SortBy()
        {
            bool correct;
            Worker[] workers = GetAllWorkers();
            WriteLine("По какому столбцу желаете отсортировать?: \n1 - по ID, \n2 - по времени записи," +
                " \n3 - по Ф.И.О., \n4 - по возрасту, \n5 - по росту, \n6 - по дате рождения," +
                " \n7 - по городу рождения");
            correct = byte.TryParse(ReadLine(), out byte choose);
            WriteLine();
            if (correct)
            {
                switch (choose)
                {
                    case 1:
                        IEnumerable<Worker> sortedWorkersByID = workers.OrderBy(Worker => Worker.ID);
                        foreach (Worker worker in sortedWorkersByID)
                        {
                            WriteLine(worker.Show());
                        }
                        break;
                    case 2:
                        IEnumerable<Worker> sortedWorkersByWriteTime = workers.OrderBy(Worker => Worker.WriteTime);
                        foreach (Worker worker in sortedWorkersByWriteTime)
                        {
                            WriteLine(worker.Show());
                        }
                        break;
                    case 3:
                        IEnumerable<Worker> sortedWorkersByFIO = workers.OrderBy(Worker => Worker.FIO);
                        foreach (Worker worker in sortedWorkersByFIO)
                        {
                            WriteLine(worker.Show());
                        }
                        break;
                    case 4:
                        IEnumerable<Worker> sortedWorkersByAge = workers.OrderBy(Worker => Worker.Age);
                        foreach (Worker worker in sortedWorkersByAge)
                        {
                            WriteLine(worker.Show());
                        }
                        break;
                    case 5:
                        IEnumerable<Worker> sortedWorkersByGrowth = workers.OrderBy(Worker => Worker.Growth);
                        foreach (Worker worker in sortedWorkersByGrowth)
                        {
                            WriteLine(worker.Show());
                        }
                        break;
                    case 6:
                        IEnumerable<Worker> sortedWorkersByDateOfBirth = workers.OrderBy(Worker => Worker.DayOfBirth);
                        foreach (Worker worker in sortedWorkersByDateOfBirth)
                        {
                            WriteLine(worker.Show());
                        }
                        break;
                    case 7:
                        IEnumerable<Worker> sortedWorkersByCityOfBirth = workers.OrderBy(Worker => Worker.CityOfBirth);
                        foreach (Worker worker in sortedWorkersByCityOfBirth)
                        {
                            WriteLine(worker.Show());
                        }
                        break;

                    default:
                        WriteLine("Такого параметра не существует");
                        break;
                }
            }
        }

        /// <summary>
        /// Метод , считывающий информацию о всех сотрудниках
        /// </summary>
        /// <returns></returns>
        public Worker[] GetAllWorkers()
        {
            string[] workerInfo = File.ReadAllLines(path);
            Worker[] workerArray = new Worker[workerInfo.Length];
            string[] splitLines;
            for (int i = 0; i < workerInfo.Length; i++)
            {
                splitLines = workerInfo[i].Split('#');
                workerArray[i].ID = int.Parse(splitLines[0]);
                workerArray[i].WriteTime = splitLines[1];
                workerArray[i].FIO = splitLines[2];
                workerArray[i].Age = byte.Parse(splitLines[3]);
                workerArray[i].Growth = byte.Parse(splitLines[4]);
                workerArray[i].DayOfBirth = splitLines[5];
                workerArray[i].CityOfBirth = splitLines[6];
            }
            return workerArray;
        }

        /// <summary>
        /// Метод, добавляющий нового сотрудника
        /// </summary>
        /// <param name="worker"></param>
        public void AddWorker()
        {
            using (StreamWriter addWorker = new StreamWriter(path, true))
            {
                char key = 'y';

                do
                {
                    StringBuilder info = new StringBuilder();

                    Write("\nВведите ID сотрудника: ");
                    info.Append($"{ReadLine()}#");

                    string writeTime = DateTime.Now.ToString("g");
                    WriteLine($"Запись добавлена {writeTime}");
                    info.Append($"{writeTime}#");

                    Write("Введите Ф.И.О. сотрудника : ");
                    info.Append($"{ReadLine()}#");

                    Write("Введите возраст сотрудника: ");
                    info.Append($"{ReadLine()}#");

                    Write("Введите рост сотрудника: ");
                    info.Append($"{ReadLine()}#");

                    Write("Введите дату рождения сотрудника: ");
                    info.Append($"{ReadLine()}#");

                    Write("Введите город рождения сотрудника: ");
                    info.Append($"город {ReadLine()}");

                    addWorker.WriteLine(info);
                    Write("Продолжить ввод? y/n");
                    key = ReadKey(true).KeyChar;

                } while (char.ToLower(key) == 'y');
            }
        }

        /// <summary>
        /// Метод, создающий файл,если такого нет
        /// </summary>
        public void FileExistCheck()
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }
    }
}
