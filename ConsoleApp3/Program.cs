using System;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = "88.212.241.115";
            var dateTime = UnixTimeStampToDateTime(1380619079).ToString("yyyy");
            var intPort = int.Parse(dateTime);
            var message = "Greetings\n";

            TcpClient client = new TcpClient(host, intPort);

            var data = Encoding.ASCII.GetBytes(message);

            NetworkStream stream = client.GetStream();

            stream.Write(data, 0, data.Length);

            Console.WriteLine("Sent: {0}", message);

            data = new Byte[256];

            var responseData = String.Empty;

            var bytes = stream.Read(data, 0, data.Length);
            responseData = Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);

            stream.Close();
            client.Close();
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
