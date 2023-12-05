namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Test.StartTest();

                Console.Clear();
                Console.WriteLine("Тест завершен. Нажмите Enter, чтобы сыграть еще раз, или Esc для выхода.");

                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }

                Console.Clear();
            }
        }
    }
}