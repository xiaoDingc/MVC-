using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ServeSocket
{
    enum week {
        m=1,
        n=2,
        c=3,
    }

    class Program
    {
        static  Byte[] buf = new byte[1024];
        static void Main(string[] args)
        {
            //var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //socket.Bind(new IPEndPoint(IPAddress.Any, 4546));
            //socket.Listen(10);
            //Console.WriteLine("服务器端创建成功");
            //socket.BeginAccept(new AsyncCallback(AcceptMessage), socket);
            int sum = 0;
            int.TryParse("1234",out sum);
            Console.WriteLine(sum);
            week wec=(week)1;
            Console.WriteLine(wec==week.m);
            ArrayList arrayList = new ArrayList();
            arrayList.Add("123");
            arrayList.Add(123);
            arrayList.Add("abc");
            List<int> c = new List<int>();
            foreach (var item in arrayList)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        public static void AcceptMessage(IAsyncResult ar)
        {
                var socket = ar.AsyncState as Socket;
                var client = socket.EndAccept(ar);
                Timer time = new Timer();
                time.Enabled = true;
                time.Interval = 2000;
                time.Elapsed += ((o, a) =>
                {
                    if (client.Connected)
                    {
                        try
                        {
                            client.Send(Encoding.Default.GetBytes("Hi,Iam receive you request:" + DateTime.Now.ToString()+ client.RemoteEndPoint), SocketFlags.None);
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e.Message);
                        }

                    }
                    else
                    {
                        time.Stop();
                        time.Enabled = false;
                        Console.WriteLine("sth is closed:"+ client.RemoteEndPoint);
                    }

                });
                time.Start();
                client.BeginReceive(buf, 0, buf.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), client);
                socket.BeginAccept(new AsyncCallback(AcceptMessage), socket);
        }

        public static void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                var socket = ar.AsyncState as Socket;
                int length = socket.EndReceive(ar);
                string str = Encoding.Default.GetString(buf, 0, length);
                Console.WriteLine(str);
                socket.BeginReceive(buf, 0, buf.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }

    }
}
