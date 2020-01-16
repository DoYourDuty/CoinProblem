//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Text;
//using System.Threading.Tasks;
//using VendingMachine;

//namespace VendingMachine
//{
//    static class Program
//    {
//        static Dictionary<int, int> CoinsAvailable = new Dictionary<int, int>() { { 10, 5 }, { 5, 5 }, { 1, 10 } };

//        static void Main1(string[] args)
//        {
//        first:
//            Console.WriteLine();
//            Console.WriteLine();
//            Console.WriteLine("Enter Number");
//            Console.WriteLine("---------------------------");
//            var m = Convert.ToInt32(Console.ReadLine());


//            var newList = GetBestCoins(m);

//            newList.ForEach(b => Console.Write(b.ToString() + ", "));
//            Console.WriteLine();

//            Console.WriteLine("---------------------------"); Console.WriteLine("---------------------------");

//            Console.WriteLine();
//            Console.WriteLine();

//            //Console.WriteLine("Do you want try again press y");
//            //if (Console.ReadKey().Key == ConsoleKey.Y)
//            { goto first; }

//            Console.ReadLine();
//        }

//        public static List<int> GetBestCoins(int m)
//        {
//            var Coins = CoinsAvailable.Keys.ToList().Distinct().ToList();

//            var list = FindFirstLevel(Coins.ToList(), m);
//            List<List<int>> newList = new List<List<int>>();
//            if (Coins.Contains(m))
//            {
//                //Console.WriteLine(m);
//                newList.Add(new List<int>() { m });
//            }
//            int i = 0;
//            newList.Add(list.OrderByDescending(a => a).ToList());

//            while (true)
//            {
//                if (list.TrueForAll(a => a == Coins.LastOrDefault())) break;

//                var list1 = list.ToList();
//                int j = 0;
//                list.ForEach(a =>
//                {
//                    list1.RemoveAt(j);
//                    var newlist = FindFirstLevel(Coins.ToList(), a);
//                    newlist.AddRange(list1.ToList());
//                    newList.Add(newlist.ToList().OrderByDescending(o => o).ToList());
//                    j++;
//                    list1 = list.ToList();
//                });

//                i++;
//                newList = newList.sortListColl();
//                if (newList.Count <= i) break;
//                list = newList[i];
//            }
//            //var temp = newList.FindAll(x =>
//            // {
//            //     var z = CoinsAvailable.Select(s => new KeyValuePair<int, int>(s.Key, x.Count(c => c == s.Key))).ToDictionary(j => j.Key, k => k.Value);
//            //     return z.All(a => CoinsAvailable[a.Key] >= a.Value);
//            // });

//            if (newList.Count == 0) return new List<int>();

//            newList = newList.RemoveNotMatch();

//            if (newList.Count == 0) return new List<int>();

//            var final = newList.FirstOrDefault();

//            //var final = newList.OrderBy(x =>
//            //{
//            //    var z = CoinsAvailable.Select(s => new KeyValuePair<int, double>(s.Key, (double)((double)((double)x.Count(c => c == s.Key) * (double)100) / (double)s.Value))).ToDictionary(j => j.Key, k => k.Value);
//            //    var z1 = z.Values.Aggregate((p, q) => p + q);
//            //    return z1;
//            //}).FirstOrDefault();

//            CoinsAvailable = CoinsAvailable.ToList().Select(a => new KeyValuePair<int, int>(a.Key, (a.Value - final.Count(c => c == a.Key)))).ToDictionary(j => j.Key, k => k.Value);

//            return final;
//        }

//        //public static void AddInList(this List<List<int>> newList1, List<int> x)
//        //{           

//        //    //var z = CoinsAvailable.Select(s => new KeyValuePair<int, int>(s.Key, x.Count(c => c == s.Key))).ToDictionary(j => j.Key, k => k.Value);
//        //    //var zq = z.All(a => CoinsAvailable[a.Key] >= a.Value);
//        //    //if (zq)
//        //    //{
//        //    //    newlst.Add(x);
//        //    //}           

//        //    //newList1 = SortList(newList1.ToList());
//        //}

//        public static List<List<int>> RemoveNotMatch(this List<List<int>> oList)
//        {

//            var newlst = oList.FindAll(x =>
//            {
//                var z = CoinsAvailable.Select(s => new KeyValuePair<int, int>(s.Key, x.Count(c => c == s.Key))).ToDictionary(j => j.Key, k => k.Value);
//                return z.All(a => CoinsAvailable[a.Key] >= a.Value);
//            });

//            //new List<List<int>>();


//            newlst.OrderBy(orderby =>
//            {
//                var persent = CoinsAvailable.Select(s => new KeyValuePair<int, double>(s.Key, (double)((double)((double)orderby.Count(c => c == s.Key) * (double)100) / (double)s.Value))).ToDictionary(j => j.Key, k => k.Value);
//                var z1 = persent.Values.Aggregate((p, q) => p + q);
//                return z1;
//            });

//            return newlst;
//        }

//        public static List<List<int>> sortListColl(this List<List<int>> oList)
//        {
//            var nList = new List<List<int>>();
//            var lstByte = new List<string>();

//            oList.ForEach(a =>
//            {
//                var b = ConvertToByte(a);
//                if (!lstByte.Contains(b))
//                {
//                    lstByte.Add(b);
//                    //var x = oList.Select(m=> m.GetHashCode()).toli;
//                    nList.Add(a);
//                }
//            });

//            return nList; //.OrderBy(a=> a).ToList();
//        }

//        public static string ConvertToByte(object o)
//        {
//            var binFormatter = new BinaryFormatter();
//            var mStream = new MemoryStream();
//            binFormatter.Serialize(mStream, o);

//            //This gives you the byte array.

//            return mStream.ToArray().Select(a => a.ToString()).Aggregate((a, b) => a + b);
//        }


//        public static List<int> FindFirstLevel(List<int> coins, int money)
//        {
//            var m = money;
//            List<int> com = new List<int>();

//            if (coins.LastOrDefault() == money) return new List<int>() { money };

//            coins.Remove(money);

//            coins.ForEach(c =>
//            {
//                if (m == 0) return;

//                var r = ReplacedBy(c, m);
//                while (r.Item1)
//                {
//                    com.Add(r.Item2);
//                    m = r.Item3;
//                    if (r.Item3 > 0)
//                    {
//                        r = ReplacedBy(c, r.Item3);
//                    }
//                    else
//                    {
//                        break;
//                    }
//                }
//            });

//            return com;
//        }

//        public static Tuple<bool, int, int> ReplacedBy(int r, int m)
//        {
//            if (m >= r)
//            {
//                return new Tuple<bool, int, int>(true, r, m - r);
//            }

//            return new Tuple<bool, int, int>(false, r, m);
//        }
//    }




//    //public class A
//    //{
//    //    static A()
//    //    {
//    //        Console.WriteLine("A - Static called");
//    //    }

//    //    public A()
//    //    {

//    //        Console.WriteLine("A - instance called");
//    //    }
//    //}

//    //public class B : A
//    //{
//    //    static B()
//    //    {
//    //        Console.WriteLine("B - Static called");
//    //    }

//    //    public B()
//    //    {

//    //        Console.WriteLine("B - instance called");
//    //    }

//    //}
//}
