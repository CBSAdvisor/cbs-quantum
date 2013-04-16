using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Cbs.CgMinerApi
{
    public class CgMinerApi
    {
        public CgMinerApi(string ipAddress, int port)
        {
            IPAddress address = Dns.GetHostEntry("").AddressList[0];
            if (!IPAddress.TryParse(ipAddress, out address))
            {
            }

            Address = address;
            EndPoint = new IPEndPoint(Address, port);

            Socket socket = new Socket(Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = false
            };
        }

        public Dictionary<string, string> Send(string sCommandMessage, bool bIgnoreErrors = false)
        {
            Dictionary<string, string> result = new Dictionary<string,string>();
            byte[] buffer = new byte[0x2001];
            
            try
            {
                new SocketPermission(NetworkAccess.Connect, TransportType.Tcp, "", -1).Demand();

                Socket socket = new Socket(Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
                {
                    NoDelay = false
                };

                socket.Connect(EndPoint);

                byte[] bytes = Encoding.ASCII.GetBytes(sCommandMessage);
                int sendCount = socket.Send(bytes);
                int count = socket.Receive(buffer);
                string response = Encoding.UTF8.GetString(buffer, 0, count);
                while (socket.Available > 0)
                {
                    count = socket.Receive(buffer);
                    response = response + Encoding.Unicode.GetString(buffer, 0, count);
                }

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

                response = Strings.Replace(response, "|", ",", 1, -1, CompareMethod.Binary);
                response = Strings.Replace(response, "\0", "", 1, -1, CompareMethod.Binary);

                String[] pairs = response.Split(',');
                foreach (string pair in pairs)
                {
                    string[] propVal = pair.Split('=');
                    result.Add(propVal[0].ToLower(), (propVal.Length == 2) ? propVal[1] : String.Empty);
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }


        public IPAddress Address { get; private set; }
        public int Port { get; private set; }
        public IPEndPoint EndPoint { get; private set; }
    }
}
