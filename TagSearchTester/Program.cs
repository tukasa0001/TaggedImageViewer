using System;
using System.Collections.Generic;
using System.IO;
using TagSystem;

namespace TagSearchTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("== TagSearchTester ==\n");
            HashSet<(string, FileInfo)> data = new();
            foreach(var fi in new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles("*", SearchOption.AllDirectories))
            {
                data.Add((fi.Name, fi));
            }
            var handle = new TagSystemHandle<FileInfo>(data);

            Console.WriteLine("== ファイル・タグ列挙 ==");
            foreach (var f in handle.files)
            {
                Console.Write($"{f.name} : ");
                foreach (var t in f.tags)
                    Console.Write(t + "  ");

                Console.Write("\n");
            }
            Console.WriteLine("検索方法: 一致させたいタグをスペース区切りで入力してください。");
            Console.WriteLine("\"or\"で区切ることで、片方一致検索が可能です。");
            Console.WriteLine("例: \"T1 T2 or T1 T3 or T2 T3\" => T1, T2, T3のうちどれか2つが含まれているファイル");
            while (true)
            {
                Console.WriteLine("== 検索 ==");
                Console.Write("検索 >");
                var SearchIn = Console.ReadLine();
                var SearchResult = handle.Search(SearchIn);
                Console.WriteLine("検索結果: ");
                foreach (var f in SearchResult)
                {
                    Console.WriteLine($"    {f.Name}");
                }
            }
        }
    }
}
