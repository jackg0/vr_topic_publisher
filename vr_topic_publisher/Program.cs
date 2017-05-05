using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;


namespace vr_topic_publisher
{ 
    public class Client
    {
        public static void Main()
        {

            TcpClient client = new TcpClient();
            client.Connect("10.0.0.45", 9090);

            NetworkStream stream = client.GetStream();

            Status publish = new vr_topic_publisher.Status();
            Message msg = new vr_topic_publisher.Message();
            publish.op = "publish";
            publish.topic = "/pwm_transfer";
            publish.type = "std_msgs/Int32";
            Random rand = new Random();
            while (client.Connected)
            {
                msg.data = rand.Next(10, 90);
                publish.msg = msg;

                string output = JsonConvert.SerializeObject(publish);

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] data = asen.GetBytes(output);


                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
            client.Close();

        }

    }

    public class Status
    {
        public string op { get; set; }
        public string topic { get; set; }
        public string type { get; set; }
        public Message msg { get; set; }
        public Status() { }
    }

    public class Message
    {
        public Int32 data { get; set; }
        public Message() { }
    }
}
