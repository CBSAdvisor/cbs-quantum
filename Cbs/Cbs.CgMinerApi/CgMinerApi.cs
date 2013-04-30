using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;

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

        public JObject Send(string sCommand, string sParam, bool bIgnoreErrors = false)
        {
            LogLib.Log4.UserLog.InfoFormat("Send command: {0}", sCommand);

            JObject result = null;
            byte[] buffer = new byte[0x2001];
            
            try
            {
                new SocketPermission(NetworkAccess.Connect, TransportType.Tcp, "", -1).Demand();

                Socket socket = new Socket(Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
                {
                    NoDelay = false
                };

                socket.Connect(EndPoint);

                string postString = BuildPostData(sCommand, sParam);
                byte[] bytes = Encoding.ASCII.GetBytes(postString);
                int sendCount = socket.Send(bytes);
                LogLib.Log4.UserLog.InfoFormat("Sent {0} bytes.\n{1}", sendCount, postString);

                int count = socket.Receive(buffer);
                string response = Encoding.UTF8.GetString(buffer, 0, count);
                while (socket.Available > 0)
                {
                    count = socket.Receive(buffer);
                    response = response + Encoding.Unicode.GetString(buffer, 0, count);
                }

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

                result = JObject.Parse(response);
                LogLib.Log4.UserLog.InfoFormat("Received response:\n{0}", result.ToString());
            }
            catch (Exception ex)
            {
                LogLib.Log4.UserLog.ErrorFormat("Send {0}:\n{1}", sCommand, ex);
            }

            return result;
        }

        public GpuSection GetGpuSection(int gpu)
        {
            JObject response = Send("gpu", gpu.ToString(), true);

            return GpuSection.ReadFromJObject(response["GPU"] as JObject);
        }

        public IPAddress Address { get; private set; }
        public int Port { get; private set; }
        public IPEndPoint EndPoint { get; private set; }

        #region Private methods

        private Dictionary<string, string> BuildDictionary(string[] keyValPairs)
        {
            LogLib.Log4.DeveloperLog.Info("BuildDictionary:");

            Dictionary<string, string> result = new Dictionary<string, string>();

            string propName = String.Empty;
            string propValue = String.Empty;

            foreach (string pair in keyValPairs)
            {
                string[] propValPair = pair.Split('=');

                propName = propValPair[0].ToLower();
                propValue = (propValPair.Length == 2) ? propValPair[1] : String.Empty;

                if (!String.IsNullOrEmpty(propName))
                {
                    result.Add(propName, propValue);
                    LogLib.Log4.DeveloperLog.InfoFormat("{0}={1}", propName, propValue);
                }
            }

            return result;
        }

        public static string BuildPostData(string cmd, string param = "")
        {
            string s = String.Empty;

            s = "{";
            s += String.Format("\"command\":\"{0}\"", cmd);
            if (!String.IsNullOrEmpty(param))
            {
                s += String.Format(",\"parameter\":\"{0}\"", HttpUtility.UrlEncode(param));
            }
            s += "}";

            return s;
        }

        #endregion
    }
}
