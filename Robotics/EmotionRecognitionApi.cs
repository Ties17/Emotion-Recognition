using Algorithmia;
using Emgu.CV;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
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

        }
    }
}
