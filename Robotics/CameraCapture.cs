using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robotics
{
    public partial class CameraCapture : Form
    {
        
        Mat capImage { get; set; }
        Mat liveImage { get; set; }
        VideoCapture cap;
        EmotionRecognitionApi api;

        public CameraCapture(EmotionRecognitionApi api)
        {
            this.api = api;
            InitializeComponent();
            cap = new VideoCapture();

            Application.Idle += new EventHandler(delegate (object sender, EventArgs e)
            {  //run this until application closed (close button click on image viewer)
                liveImage = cap.QueryFrame();
                CvInvoke.Flip(liveImage, liveImage, FlipType.Horizontal);

                CvInvoke.Resize(liveImage, liveImage, new Size(480, 360));

                LiveImage.Image = liveImage; //draw the image obtained from camera
            });
        }

        public void TakePic()
        {
            capImage = cap.QueryFrame();
            CvInvoke.Flip(capImage, capImage, FlipType.Horizontal);

            CvInvoke.Resize(capImage, capImage, new Size(480, 360));

            LiveImage.Image = capImage;

        }

        private void ImageBox_Click(object sender, EventArgs e)
        {
            
        }

        private void CapButton_Click(object sender, EventArgs e)
        {
            TakePic();
            CapImage.Image = capImage;

            string result = api.applyRecognition(capImage);
        }
    }
}
