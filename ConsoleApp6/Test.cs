using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public class Test
    {
        private static readonly string Text = "Преимущественно спокойный и сдержанный, но легко выходит из себя, если его план рушиться из-за глупой ошибки или из-за ошибок подчинённых. Считает что Сталлионграду требуется символ, который будет олицетворять силу страны и вселять ужас во врагов (символом Нарим видит сверхтяжёлый танк или огромный дирижабль). В свободное время Нарим пытается проектировать боевые дирижабли, сверхтяжёлые танки и бронепоезда (не всегда реализуемые). Некоторые из своих проектов он иногда предлагает начальству. По мнению Нарима его проекты (например боевой дирижабль с корабельными орудиями, артиллерийский бронепоезд или десантный дирижабль) могут сильно поддержать наступление армии.";
        private static Stopwatch stopwatch = new Stopwatch();
        private static int charactersTyped = 0;
        private static readonly int ConsoleColumns = Console.WindowWidth;
        private static int x = 0;
        private static int y = 2;
        private static bool testCompleted = false;
        static int time = 60;

        public static void StartTest()
        {
            Console.Write("Введите ваше имя: ");
            string name = Console.ReadLine();
            stopwatch = Stopwatch.StartNew();
            Console.WriteLine("Начните печатать текст:");
            Console.WriteLine(Text);
            Console.SetCursorPosition(0, 2);
            x = Console.CursorLeft;
            y = Console.CursorTop;
            timer.Start();
            stopwatch.Start();
            while (!testCompleted)
            {
                var keyInfo = Console.ReadKey(true);

                if (!testCompleted && charactersTyped < Text.Length && keyInfo.KeyChar == Text[charactersTyped])
                {
                    Console.SetCursorPosition(x, y);
                    x = Console.CursorLeft;
                    y = Console.CursorTop;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(keyInfo.KeyChar);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    charactersTyped++;
                    ConsoleMoveCursorForward();
                    Console.SetCursorPosition(x, y);
                }

                if (charactersTyped == Text.Length | time < 1)
                {
                    testCompleted = true;
                }
            }

            stopwatch.Stop();

            User user = new User
            {
                Name = name,
                CharactersPerMinute = CalculateCharactersPerMinute(stopwatch.Elapsed.TotalMinutes),
                CharactersPerSecond = CalculateCharactersPerSecond(stopwatch.Elapsed.TotalSeconds)
            };

            Records.AddUser(user);

            Console.Clear();
            Console.WriteLine("\nТаблица рекордов:");
            GetLeaderboard();

            Console.WriteLine("\nТест окончен! Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        static Thread timer = new Thread(_ =>
        {
            timer.IsBackground = true;
            while (time > 0)
            {
                Console.SetCursorPosition(1, 10);
                Console.WriteLine(time -= 1);
                Thread.Sleep(1000);
            }
        });



        private static int CalculateCharactersPerMinute(double elapsedMinutes)
        {
            int charactersPerMinute = (int)(charactersTyped / elapsedMinutes);

            return charactersPerMinute;
        }

        private static int CalculateCharactersPerSecond(double elapsedSeconds)
        {
            int charactersPerSecond = (int)(charactersTyped / elapsedSeconds);

            return charactersPerSecond;
        }

        private static void GetLeaderboard()
        {
            var users = Records.GetUsers();

            Console.WriteLine("{0,-10} {1,-10} {2,-10}", "Имя", "Симв./мин", "Симв./сек");

            foreach (var user in users)
            {
                Console.WriteLine("{0,-10} {1,-10} {2,-10}", user.Name, user.CharactersPerMinute, user.CharactersPerSecond);
            }
        }
        private static void ConsoleMoveCursorForward()
        {
            x++;
            if (x != ConsoleColumns) return;
            x = 0;
            y++;
        }
    }
}
