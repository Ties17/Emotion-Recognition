using Algorithmia;
using Emgu.CV;
using Emgu.CV.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace Robotics
{
    public class EmotionRecognitionApi
    {
        string APIkey = "";
        static Client client;
        DataDirectory dir;
        static Dictionary<string, double> result = new Dictionary<string, double>();

        static MQTTconnector mqtt = new MQTTconnector();

        public EmotionRecognitionApi()
        {
            readAPIKey();
            createDirectory();
        }

        private void readAPIKey()
        {
             APIkey = System.IO.File.ReadAllText(Environment.CurrentDirectory + "/apikey.txt");
             client = new Client(APIkey);
        }

        private void createDirectory()
        {
            dir = client.dir("data://TiesTienhoven/faces");
            if (!dir.exists())
            {
                dir.create();
            }

        }

        public void applyRecognition(Mat image)
        {
            CvInvoke.Imwrite("image.jpg", image);
            var dest = dir.file("image.jpg");
            dest.put(File.OpenRead("image.jpg"));

            Thread t = new Thread(recognition);
            t.Start();

            
        }

        public static void recognition()
        {
            ApiReq req = new ApiReq();
            req.image = "data://TiesTienhoven/faces/image.jpg";
            req.numResults = 7;


            var algorithm = client.algo("deeplearning/EmotionRecognitionCNNMBP/1.0.1");
            algorithm.setOptions(timeout: 300); // optional
            var response = algorithm.pipeJson<object>(JsonConvert.SerializeObject(req));
            string JSONresult = response.result.ToString();

            JObject obj = JObject.Parse(JSONresult);
            Dictionary<string, double> emoties = new Dictionary<string, double>();

            for (int i = 0; i < 7; i++)
            {
                string emotie = (string)obj.SelectToken("results[0].emotions[" + i + "].label");
                double conf = (double)obj.SelectToken("results[0].emotions[" + i + "].confidence");
                emoties.Add(emotie, conf);
                Console.WriteLine(emotie);
                Console.WriteLine(conf);
            }

            result = emoties;

            mqtt.publishToServer(characterMapping());
        }
        
        public static string characterMapping()
        {
            Random random = new Random();
            string[] characters = { "Toad", "Camila", "Slime Mold", "Nano bots", "Coral crabs", "Salamander" };

            string mainEmotion = result.Keys.ElementAt<string>(0);
            string secondEmotion = result.Keys.ElementAt<string>(1);

            switch (mainEmotion)
            {
                case "Disgust":
                    return characters[0];
                case "Happy":
                    if (secondEmotion == "Surprised")
                    {
                        return characters[1];
                    }
                    else if (secondEmotion == "Fear")
                    {
                        return characters[5];
                    }
                    return characters[random.Next(6)];
                case "Surprised":
                    return characters[2];
                case "Neutral":
                    if (secondEmotion == "Surprised")
                    {
                        return characters[3];
                    }
                    else if (secondEmotion == "Happy")
                    {
                        return characters[4];
                    }
                    return characters[random.Next(6)];
            }
            return characters[random.Next(6)];
        }

    }
}
