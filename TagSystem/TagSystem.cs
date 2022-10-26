using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagSystem
{
    public class TagSystemHandle<T>
    {
        public readonly DirectoryInfo dir;
        public HashSet<TaggedData<T>> files;

        public TagSystemHandle(HashSet<(string, T)> data)
        {
            files = new();
            foreach(var kvp in data)
            {
                files.Add(new TaggedData<T>(kvp.Item1, kvp.Item2));
            }
        }

        public T[] Search(string text)
        {
            //スペースで分割
            var args = text.Split(' ');
            // 中リスト一つ分のタグがANDで判定される
            List<List<string>> SearchData = new List<List<string>>();
            List<string> current = new();
            SearchData.Add(current);
            // 分割したものをループ
            foreach(var a in args)
            {
                //ORが入ってきた場合、中リストを新しくする。
                if(a == "or" || a == "|" || a == "||")
                {
                    current.RemoveAll(t => string.IsNullOrWhiteSpace(t));
                    current = new();
                    SearchData.Add(current);
                }
                else
                {
                    current.Add(a);
                }
            }
            current.RemoveAll(t => string.IsNullOrWhiteSpace(t));
            //判定
            HashSet<TaggedData<T>> result = new();
            foreach (var l in SearchData)
            {
                foreach (var c in AndFind(l.ToArray()))
                    result.Add(c);
            }
            return result.Select(td => td.data).ToArray();
        }
        public List<TaggedData<T>> AndFind(string[] tags)
        {
            List<TaggedData<T>> result = new();
            foreach (var f in files)
            {
                foreach (var t in tags)
                {
                    if (String.IsNullOrWhiteSpace(t)) continue; //空項目を無視
                    char first = t[0];
                    
                    if (first != '!' && !f.tags.Contains(t) ||
                        1 <= t.Length && first == '!' && f.tags.Contains(t.Substring(1, t.Length - 1))
                        ) goto NextFile;
                }
                // 引数のtagsに含まれるタグをすべて含んでいるファイル
                result.Add(f);

                NextFile:;
            }

            return result;
        }
    }

    public class TaggedData<T>
    {
        public readonly IReadOnlySet<string> tags;
        public readonly string name;
        public readonly T data;

        public TaggedData(string name, T data)
        {
            this.name = name;
            this.data = data;
            HashSet<string> tags;
            this.tags = tags = new HashSet<string>();

            var tagsArray = name.Split('.');
            for(int i = 0; i < tagsArray.Length; i++)
            {
                string t = tagsArray[i];
                if (string.IsNullOrWhiteSpace(t)) continue;
                tags.Add(tagsArray[i]);
            }
        }
    }
}
