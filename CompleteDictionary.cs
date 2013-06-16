using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickLauncher
{
    /// <summary>
    /// 補完対象のアイテムコンテナ
    /// </summary>
    public class CompleteItem
    {
        /// <summary>
        /// 補完アイテムの種類を表す列挙型
        /// </summary>
        public enum CompleteItemType
        {
            /// <summary>
            /// プログラムへのショートカット
            /// </summary>
            ProgramShortcut,
            /// <summary>
            /// QuickLauncher自身のコマンド
            /// </summary>
            ApplicationFunction,
            /// <summary>
            /// コマンドライン命令
            /// </summary>
            CommandLineFunction,
            /// <summary>
            /// Web検索命令
            /// </summary>
            WebFunction,
            /// <summary>
            /// ユーザ定義命令
            /// </summary>
            UserFunction
        }

        /// <summary>
        /// 補完アイテム名
        /// コマンド名を兼ねる
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 実行ファイルパス，またはコマンド
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 補完アイテムの種類
        /// </summary>
        public CompleteItemType Type { get; set; }
    }

    /// <summary>
    /// 補完アイテムのソート済みリスト
    /// </summary>
    public class CompleteDictionary : SortedDictionary<string, CompleteItem>
    {
        /// <summary>
        /// 新しい補完アイテムを追加する
        /// </summary>
        /// <param name="name">補完名</param>
        /// <param name="path">ファイルパス，コマンド</param>
        /// <param name="type">補完種類</param>
        public void Add(string name, string path, CompleteItem.CompleteItemType type)
        {
            this.Add(name, new CompleteItem() { Name = name, Path = path, Type = type });
        }

        /// <summary>
        /// 大文字小文字を無視してキーマッチングし，結果を返す
        /// </summary>
        /// <param name="key">検索対象キー</param>
        /// <returns>一致するキーが合った時，それに対応するCompleteItem</returns>
        public CompleteItem Get(string key)
        {
            string[] keys = key.Split(' ');
            foreach (var i in this)
            {
                switch (i.Value.Type)
                {
                    case CompleteItem.CompleteItemType.ApplicationFunction:
                    case CompleteItem.CompleteItemType.ProgramShortcut:
                        if (string.Compare(i.Key, key, true) == 0) return i.Value;
                        break;
                    case CompleteItem.CompleteItemType.WebFunction:
                    case CompleteItem.CompleteItemType.CommandLineFunction:
                    case CompleteItem.CompleteItemType.UserFunction:
                        if (string.Compare(i.Key, keys[0], true) == 0) return i.Value;
                        break;
                }
                
            }
            throw new KeyNotFoundException("No such key item");
        }
    }
}
