using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ServeSocket
{
    class Program
    {
        static  Byte[] buf = new byte[1024];
        static void Main(string[] args)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 4546));
            socket.Listen(10);
            Console.WriteLine("服务器端创建成功");
            socket.BeginAccept(new AsyncCallback(AcceptMessage), socket);

      
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
