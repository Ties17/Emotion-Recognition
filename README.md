To be able to run this program there are 2 steps to check.

Step 1:

For this program two files have to be added to the Debug directory in the project files.
These 2 files are:
  - apikey.txt 
  - serverPassword.txt
  
The apikey file contains one line, that is your apikey used to connect to the services from Algorithmia.
The serverPassword file also contains just one line. This being the password to the used mqtt server. If you want to use your own mqtt server the MQTTConnector class has to be changed so it connects to your server.

Step 2:

The project can give an emgu cv error saying it doesn't recognize the library.
A simple restart of visual studio can sometimes fix the problem.
If this doesn't fix the problem emgu cv has to be reinstalled via nuger package manager.
