﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SandBox1
{
    class Program
    {
        /// <summary>
        /// リフレクションを使ったプロパティへの代入はやっぱり遅いけど、1回20msなら別に気にする必要なくないか？
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 適当な分岐のつもり
            int flag = 1;
            // 1.Actionデリゲートとして受け取るタイプ
            Action result = flag switch
            {
                1 => Test1,
                2 => Test2,
                _ => throw new InvalidOperationException()
            };
            result();

            // 2.Actionを返却する関数として実装したタイプ
            Action(flag).Invoke();

            // 3.どれか1つでもキャストするとvarでもいけることがわかった。
            // でもって、こっちだと.Invoke()で起動できる。
            var a = flag switch
            {
                1 => (Action)Test1,
                2 => Test2,
                _ => throw new InvalidOperationException()
            };
            a.Invoke();

            // 4.そもそもActionとして受け取らないで即時実行するタイプ
            (flag switch
            {
                1 => (Action)Test1,
                2 => Test2,
                _ => throw new InvalidOperationException()
            }).Invoke();
        }

        static Action Action(int flag) => flag switch
        {
            1 => Test1,
            2 => Test2,
            _ => throw new InvalidOperationException()
        };


        static void Test1() => Console.WriteLine("test1");
        static void Test2() => Console.WriteLine("test2");
    }
}
