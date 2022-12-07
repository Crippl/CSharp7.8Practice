namespace CSharp7._8Practice
{
    public struct Worker
    {
        public int ID { get; set; }
        public string WriteTime { get; set; }
        public string FIO { get; set; }
        public byte Age { get; set; }
        public byte Growth { get; set; }
        public string DayOfBirth { get; set; }
        public string CityOfBirth { get; set; }

        /// <summary>
        /// Метод для отображения информации о сотруднике
        /// </summary>
        /// <returns></returns>
        public string Show()
        {
            return $"{this.ID,-5}{this.WriteTime,-20}{this.FIO,-30}{this.Age,-10}{this.Growth,-5}{this.DayOfBirth,-20}{this.CityOfBirth,-25}";
        }

        public Worker(int ID, string WriteTime, string FIO, byte Age,
    byte Growth, string DayOfBirth, string CityOfBirth)
        {
            this.ID = ID;
            this.WriteTime = WriteTime;
            this.FIO = FIO;
            this.Age = Age;
            this.Growth = Growth;
            this.DayOfBirth = DayOfBirth;
            this.CityOfBirth = CityOfBirth;
        }
    }
}
