using System;
using System.Text;
using System.Net;
using System.IO;

namespace WebRequestPostUrlEncoded
{
    class Program
    {
        //目標網址
        static string targetURL = "";
        //以下是key跟value
        static string param1Key = "";
        static string param1Value = "";
        static string param2Key = "";
        static string param2Value = "";

        static void Main(string[] args)
        {
            //request設定
            HttpWebRequest httpWebRequest= (HttpWebRequest)WebRequest.Create(targetURL);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Timeout = 3000;
            //把參數格式化
            string paramString = String.Format("{0}={1}&{2}={3}",
                WebUtility.UrlEncode(param1Key), WebUtility.UrlEncode(param1Value),
                WebUtility.UrlEncode(param2Key), WebUtility.UrlEncode(param2Value));
            //轉成bytes
            byte[] paramBytes = Encoding.UTF8.GetBytes(paramString);
            //設定request的內容長度
            httpWebRequest.ContentLength = paramBytes.Length;
            //送出body內容
            using(Stream reqStream = httpWebRequest.GetRequestStream())
            {
                reqStream.Write(paramBytes, 0, paramBytes.Length);
            }
            //接收回應的內容
            using(WebResponse response = httpWebRequest.GetResponse())
            {
                using(StreamReader reader=new StreamReader(response.GetResponseStream()))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
        }
    }
}
