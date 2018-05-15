using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientSocket
{
    class Program
    {
        static Byte[] buf = new byte[1024];
        static void Main(string[] args)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("localhost", 4546);
            Console.WriteLine("连接成功!");
          
            socket.BeginReceive(buf, 0, buf.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);

            Console.ReadKey();
            while (true)
            {
                var message = "Message from client : " + Console.ReadLine();
                var outputBuffer = Encoding.Unicode.GetBytes(message);
                socket.BeginSend(outputBuffer,0,outputBuffer.Length,SocketFlags.None,null,null);
            }

            
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
