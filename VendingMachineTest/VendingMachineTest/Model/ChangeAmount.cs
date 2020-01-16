using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
    /// <summary>
    /// Find Best way to get changes
    /// </summary>
    public static class ChangeAmount
    {
        /// <summary>
        /// Get best coins commincation using coin available ratio.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static List<int> GetBestCoins(int m)
        {
            var Coins = LoadChangeCoins.ChangeCoins.Select(a => a.CoinAmount).ToList().Distinct().ToList();

            var list = FindFirstLevel(Coins.ToList(), m);
            List<List<int>> newList = new List<List<int>>();
            if (Coins.Contains(m))
            {
                newList.Add(new List<int>() { m });
            }
            int i = 0;
            newList.Add(list.OrderByDescending(a => a).ToList());

            while (true)
            {
                if (list.TrueForAll(a => a == Coins.LastOrDefault())) break;

                var list1 = list.ToList();
                int j = 0;
                list.ForEach(a =>
                {
                    list1.RemoveAt(j);
                    var newlist = FindFirstLevel(Coins.ToList(), a);
                    newlist.AddRange(list1.ToList());
                    newList.Add(newlist.ToList().OrderByDescending(o => o).ToList());
                    j++;
                    list1 = list.ToList();
                });

                i++;
                newList = newList.sortListColl();
                if (newList.Count <= i) break;
                list = newList[i];
            }

            if (newList.Count == 0) return new List<int>();

            //Remove not matching coin commnication and order by based on coin availability ratio
            newList = newList.RemoveNotMatch();

            if (newList.Count == 0) return new List<int>();

            var final = newList.FirstOrDefault();

            //Remove coins from main list
            LoadChangeCoins.ChangeCoins = LoadChangeCoins.ChangeCoins.ToList().Select(a => new KeyValuePair<int, int>(a.CoinAmount, (a.CoinCount - final.Count(c => c == a.CoinAmount)))).Select(a => new ChangeCoin() { CoinAmount = a.Key, CoinCount = a.Value }).ToList();

            return final;
        }

        /// <summary>
        /// Coin availability check and order by coin ratio 
        /// </summary>
        /// <param name="oList"></param>
        /// <returns></returns>
        public static List<List<int>> RemoveNotMatch(this List<List<int>> oList)
        {
            var newlst = oList.FindAll(x =>
            {
                var z = LoadChangeCoins.ChangeCoins.Select(s => new KeyValuePair<int, int>(s.CoinAmount, x.Count(c => c == s.CoinCount))).ToDictionary(j => j.Key, k => k.Value);
                return z.All(a => LoadChangeCoins.ChangeCoins.FirstOrDefault(w => w.CoinAmount == a.Key).CoinCount >= a.Value);
            });

            newlst.OrderBy(orderby =>
            {
                var persent = LoadChangeCoins.ChangeCoins.Select(s => new KeyValuePair<int, double>(s.CoinAmount, (double)((double)((double)orderby.Count(c => c == s.CoinAmount) * (double)100) / (double)s.CoinCount))).ToDictionary(j => j.Key, k => k.Value);
                var z1 = persent.Values.Aggregate((p, q) => p + q);
                return z1;
            });

            return newlst;
        }

        /// <summary>
        /// Shorting using byte check
        /// </summary>
        /// <param name="oList"></param>
        /// <returns></returns>
        public static List<List<int>> sortListColl(this List<List<int>> oList)
        {
            var nList = new List<List<int>>();
            var lstByte = new List<string>();

            oList.ForEach(a =>
            {
                var b = ConvertToByte(a);
                if (!lstByte.Contains(b))
                {
                    lstByte.Add(b);
                    nList.Add(a);
                }
            });

            return nList;
        }

        /// <summary>
        /// Conver to byte
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ConvertToByte(object o)
        {
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, o);
            return mStream.ToArray().Select(a => a.ToString()).Aggregate((a, b) => a + b);
        }

        /// <summary>
        /// Find coin commincation
        /// </summary>
        /// <param name="coins"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public static List<int> FindFirstLevel(List<int> coins, int money)
        {
            var m = money;
            List<int> com = new List<int>();

            if (coins.LastOrDefault() == money) return new List<int>() { money };

            coins.Remove(money);

            coins.ForEach(c =>
            {
                if (m == 0) return;

                var r = ReplacedBy(c, m);
                while (r.Item1)
                {
                    com.Add(r.Item2);
                    m = r.Item3;
                    if (r.Item3 > 0)
                    {
                        r = ReplacedBy(c, r.Item3);
                    }
                    else
                    {
                        break;
                    }
                }
            });

            return com;
        }


        /// <summary>
        /// Replace or change coins
        /// </summary>
        /// <param name="r"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Tuple<bool, int, int> ReplacedBy(int r, int m)
        {
            if (m >= r)
            {
                return new Tuple<bool, int, int>(true, r, m - r);
            }
            return new Tuple<bool, int, int>(false, r, m);
        }

    }
}
