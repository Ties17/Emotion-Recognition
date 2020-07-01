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

namespace Robotics
{
    public class EmotionRecognitionApi
    {
        string APIkey = "";
        Client client;
        DataDirectory dir;

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

        public string applyRecognition(Mat image)
        {
            CvInvoke.Imwrite("image.jpg", image);
            var dest = dir.file("image.jpg");
            dest.put(File.OpenRead("image.jpg"));

            ApiReq req = new ApiReq();
            req.image = "data://TiesTienhoven/faces/image.jpg";
            req.numResults = 7;
            

            var algorithm = client.algo("deeplearning/EmotionRecognitionCNNMBP/1.0.1");
            algorithm.setOptions(timeout: 300); // optional
            var response = algorithm.pipeJson<object>(JsonConvert.SerializeObject(req));
            string JSONresult = response.result.ToString();

            StringReader reader = new StringReader(JSONresult);
            JsonTextReader read = new JsonTextReader(reader);

            JObject obj = JObject.Parse(JSONresult);
            string emotie = (string) obj.SelectToken("results[0].emotions[0].label");
            double conf = (double) obj.SelectToken("results[0].emotions[0].confidence");
            Console.WriteLine(emotie);
            Console.WriteLine(conf);


            return JSONresult;
        }


    }
}
