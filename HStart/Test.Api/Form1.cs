using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Cbs.CgMinerApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test.Api
{
    public partial class Form1 : Form
    {
        private CgMinerApi cgMinerApi = new CgMinerApi("127.0.0.1", 4028);
        public Form1()
        {
            InitializeComponent();

            JObject jObj = cgMinerApi.Send("gpu", "0", true);

            StatusSection statSect = JsonConvert.DeserializeObject<StatusSection>(jObj["STATUS"][0].ToString());
            GpuSection gpuSect = JsonConvert.DeserializeObject<GpuSection>(jObj["GPU"][0].ToString());
        }

        string Query(string cmd, string param = "")
        {
            var dataStr = BuildPostData(cmd, param);
            var data = Encoding.ASCII.GetBytes(dataStr);

            IPAddress address = Dns.GetHostEntry("").AddressList[0];
            if (!IPAddress.TryParse("127.0.0.1", out address))
            {
            }

            IPEndPoint endPoint = new IPEndPoint(address, 4028);

            Socket socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = false
            };

            socket.Connect(endPoint);
            int sendCount = socket.Send(data);

            byte[] buffer = new byte[0x2001];
            int count = socket.Receive(buffer);
            string response = Encoding.UTF8.GetString(buffer, 0, count);
            while (socket.Available > 0)
            {
                count = socket.Receive(buffer);
                response = response + Encoding.Unicode.GetString(buffer, 0, count);
            }

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            var result = JObject.Parse(response);

            var gpuInfo = JsonConvert.DeserializeObject<GpuSection>(result["GPU"][0].ToString());

            return response;
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
    }
}
