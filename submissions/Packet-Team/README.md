## Malicious Packet Detection Team

### Idea
The goal of this project is to use anomaly detection with ML.NET to detect malicious network activity. We will simulate a network with some random non malicious agents emitting network traffic (Web request, ftp, etc) and then add a malicious agent that will conduct abnormal network traffic (port scanning, vulnerability execution, etc). We will train a model to identify the activity of the malicious agent from the activity of the non malicious agents.

### Team contact
For questions on this submission you can contact:
Mike Berg: mikeyberg@gmail.com

### Solution
we solved this problem by generating artifical network traffic, Summarizing that data into time windows and then training a model on the generated data. Please see the video for a more detailed explanation 

References Repos:

Full Source Code: https://github.com/mikekberg/ml-hackathon-2020
FlightSim (talked about in video): https://github.com/alphasoc/flightsim

### Video presentation
The video contains a presentation and demo, take a look here: https://drive.google.com/drive/folders/1yln9IPxTDAe2RAV-CconSg8-Fw0u5pXE?usp=sharing
