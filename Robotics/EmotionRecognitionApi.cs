using Algorithmia;
using Emgu.CV;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotics
{
    public class EmotionRecognitionApi
    {
        string APIkey = "";
        Client client;

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
            var directory = client.dir("data://TiesTienhoven/faces");
            if (!directory.exists())
            {
                directory.create();
            }

        }

        public string applyRecognition(Mat image)
        {
            VectorOfByte buffer = new VectorOfByte();
            CvInvoke.Imencode(".jpg", image, buffer);
            string enc = "data:image/png;base64" + Convert.ToBase64String(buffer.ToArray());

            var algorithm = client.algo("deeplearning/EmotionRecognitionCNNMBP/1.0.1");
            //algorithm.setOptions(timeout: 300); // optional
            var response = algorithm.pipeJson<object>(enc);
            string JSONresult = response.result.ToString();
            return JSONresult;
        }

        public string characterMapping(ListDictionary listDictionary)
        {
            Random random = new Random();
            string[] characters = { "Toad", "Camila", "Slime Mold", "Nano bots", "Coral crabs", "Salamander" };
            switch (listDictionary[0].ToString())
            {
                case "Disgust":
                    return characters[0];
                case "Happy":
                    if (listDictionary[1].ToString() == "Surprised")
                    {
                        return characters[1];
                    }
                    else if (listDictionary[1].ToString() == "Fears")
                    {
                        return characters[5];
                    }
                    return characters[random.Next(6)];
                case "Surprised":
                    return characters[2];
                case "Neutral":
                    if (listDictionary[1].ToString() == "Surprised")
                    {
                        return characters[3];
                    }
                    else if (listDictionary[1].ToString() == "Happy")
                    {
                        return characters[4];
                    }
                    return characters[random.Next(6)];
            }
            return characters[random.Next(6)];
        }


    }
}
