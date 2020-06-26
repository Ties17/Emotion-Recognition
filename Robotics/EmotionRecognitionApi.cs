using Algorithmia;
using Emgu.CV;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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

            
            if (JSONresult.Contains("emotions"))
            {
                string sub = "";
                char[] json = JSONresult.ToCharArray();
                for(int i = 0; i < json.Length; i++)
                {
                    if(json[i] == 'e' && json[i + 1] == 'm' && json[i + 2] == 'o' && json[i + 3] == 't')
                    {
                         sub = JSONresult.Substring(i);
                        
                        break;
                    }
                }

                int firstIndex = 0;
                int lastIndex = 0;

                for (int i = 0; i < sub.Length; i++)
                {
                    if(sub[i] == '[')
                    {
                        firstIndex = i;
                    }
                    if(sub[i] == ']' && lastIndex == 0)
                    {
                        lastIndex = i;
                    }
                }

                sub = sub.Substring(firstIndex, lastIndex);

                string[] split = sub.Split(',');
                List<string> emotions = new List<string>();
                for(int i = 0; i < split.Length; i += 2)
                {
                    if(!(i == split.Length - 1))
                    {
                        emotions.Add(split[i] + split[i + 1]);
                    }
                }

                foreach(string s in emotions)
                {
                    char[] t = s.ToCharArray();
                    int confidenceIndex = 0;
                    int labelIndex = 0;
                    for(int i = 0; i < t.Length; i++)
                    {
                        if (t[i] == 'c' && t[i + 1] == 'o' && t[i + 2] == 'n')
                        {
                            confidenceIndex = i;
                        }

                        

                    }
                }
            }

            

            string line = reader.ReadLine();

            
            return JSONresult;
        }


    }
}
