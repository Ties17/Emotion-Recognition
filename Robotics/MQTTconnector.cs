using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Robotics
{
    class MQTTconnector
    {
        private string password;
        public void publishToServer(string message)
        {
            MqttClient myClient = new MqttClient("tcp://maxwell.bps-software.nl:1883");
            readPassword();
            string clientId = Guid.NewGuid().ToString();
            myClient.Connect(clientId, "androidTI", password);

            myClient.Publish("/ExcellentieTraject/MaskListener", Encoding.UTF8.GetBytes(message));
            System.Console.ReadLine();
        }

        private void readPassword()
        {
            password = System.IO.File.ReadAllText(Environment.CurrentDirectory + "/serverPassword.txt");
        }
    }
}
