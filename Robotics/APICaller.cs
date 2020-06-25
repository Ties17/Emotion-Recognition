using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithmia;
using Emgu.CV;
using Newtonsoft.Json.Linq;

namespace Robotics
{
    class APICaller
    {
        string JSONresult { get; set; }
        public void getResponse(string binaryImageURI)
        {
            var client = new Client("PUT API KEY HERE");
            var algorithm = client.algo("deeplearning/EmotionRecognitionCNNMBP/1.0.1");
            algorithm.setOptions(timeout: 300); // optional
            var response = algorithm.pipeJson<object>(binaryImageURI);
            JSONresult =  response.result.ToString();
            Console.WriteLine(response.result);
            System.IO.File.WriteAllText(@"D:\path.txt", JSONresult);
        }
    }
}
