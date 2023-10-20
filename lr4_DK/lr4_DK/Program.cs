using System;
using McMaster.Extensions.CommandLineUtils;
using lr4Lib;

class Program
{
    static int resolveLab(int num, string pathInput, string pathOutput)
    {
        if (pathInput  == null || pathOutput == null)
        {
            if (!Directory.Exists(Environment.GetEnvironmentVariable("LAB_PATH"))) return 2;
            lr1.Resolve(Environment.GetEnvironmentVariable("LAB_PATH") + "input.txt",
                Environment.GetEnvironmentVariable("LAB_PATH") + "output.txt");
        }
        else
        {
            if (!Directory.Exists(pathInput) || !Directory.Exists(pathOutput)) return 2;
            lr1.Resolve(pathInput, pathOutput);
        }
        return 0;
    }
    static void Main(string[] args)
    {
        var app = new CommandLineApplication();
        app.Name = "MyApp";

        app.Command("set-path", setPathCmd =>
        {
            setPathCmd.Description = "Задає шлях до папки інпут та аутпут файлів";
            var pathOption = setPathCmd.Option("-p|--path", "Шлях до папки", CommandOptionType.SingleValue);

            setPathCmd.OnExecute(() =>
            {
                string? labPath = pathOption.Value();
                if (labPath == null) return 1;
                else if (!Directory.Exists(labPath)) return 2;

                Environment.SetEnvironmentVariable("LAB_PATH", labPath);
                Console.WriteLine(Environment.GetEnvironmentVariable("LAB_PATH"));
                return 0;
            });
        });

        app.Command("version", versionCmd =>
        {
            versionCmd.Description = "Виводить інформацію про автора та версію";
            versionCmd.OnExecute(() =>
            {
                Console.WriteLine("Автор: Kolotsei Denis");
                Console.WriteLine("Версія: 1.0");
                return 0;
            });
        });

        app.Command("help", versionCmd =>
        {
            versionCmd.Description = "Допомога";
            versionCmd.OnExecute(() =>
            {
                app.ShowHelp();
                return 0;
            });
        });

        app.Command("run", runCmd =>
        {
            runCmd.Description = "Запуск практичних завдань";
            runCmd.OnExecute(() =>
            {
                Console.WriteLine("Для запуску введіть 'run lab1', 'run lab2' або 'run lab3'");
                return 1;
            });

            runCmd.Command("lab1", lab1Cmd =>
            {
                lab1Cmd.Description = "Запуск практичної лабораторної роботи 1";
                var inputOption = lab1Cmd.Option("-i|--input", "Ім'я вхідного файлу", CommandOptionType.SingleValue);
                var outputOption = lab1Cmd.Option("-o|--output", "Ім'я вихідного файлу", CommandOptionType.SingleValue);

                lab1Cmd.OnExecute(() =>
                {
                    resolveLab(1, inputOption.Value(), outputOption.Value());
                });
            });

            runCmd.Command("lab2", lab2Cmd =>
            {
                lab2Cmd.Description = "Запуск практичної лабораторної роботи 1";
                var inputOption = lab2Cmd.Option("-i|--input", "Ім'я вхідного файлу", CommandOptionType.SingleValue);
                var outputOption = lab2Cmd.Option("-o|--output", "Ім'я вихідного файлу", CommandOptionType.SingleValue);

                lab2Cmd.OnExecute(() =>
                {
                    resolveLab(2, inputOption.Value(), outputOption.Value());
                });
            });

            runCmd.Command("lab3", lab3Cmd =>
            {
                lab3Cmd.Description = "Запуск практичної лабораторної роботи 1";
                var inputOption = lab3Cmd.Option("-i|--input", "Ім'я вхідного файлу", CommandOptionType.SingleValue);
                var outputOption = lab3Cmd.Option("-o|--output", "Ім'я вихідного файлу", CommandOptionType.SingleValue);

                lab3Cmd.OnExecute(() =>
                {
                    resolveLab(3, inputOption.Value(), outputOption.Value());
                });
            });
        });

        while (true)
        {
            Console.Write("Введіть команду: ");
            string input = Console.ReadLine();
            var inputArgs = input.Split(' ');
            try
            {
                int res = app.Execute(inputArgs);
                if(res == 1)
                {
                    Console.WriteLine("Немає бовязкових параметрів.");
                }
                else if(res == 2)
                {
                    Console.WriteLine("Невірно вказаний шлях.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Невідома команда. Для довідки введіть 'help'.");
            }
            Console.WriteLine("------------------------");
        }
    }
}