﻿using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;

using System.Drawing;
using System.Windows;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotics
{
    class Source
    {

        public static void Main(string[] args)
        {
            CameraCapture cam = new CameraCapture();
            cam.ShowDialog();
            //cam.TakePic();
            
        }



    }
}
