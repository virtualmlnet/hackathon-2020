## Team Wine Lovers

### Idea
The goal of the project is to identify the grape variety and location of a wine based on a description of its taste.

### Team contact
For questions on this submission you can contact:
Mircea Dogaru: mircea.dogaru.md@gmail.com

### Solution
I started with a Kaggle data set containing more than 130k wine reviews. The set was cleaned up and variety, country and region were merged into one columns and used as the label to predict. This was then fed to AutoML. The accuracy was not great as the dataset wasn't balanced properly but it's good enough for this POC.

The resulting consume model was wrapped in an ASP Net Core 5 MVC application which presents an input box for prediction and displays the prediction as a label.

This could be extended further, e.g. integrate with Amazon's API to return wine product links for the predicted grape variety so the user can go and buy them straight away.

The source code can be found here: https://github.com/M1rceaDogaru/virtual-som

This was the dataset used: https://www.kaggle.com/zynicide/wine-reviews

### Video presentation
https://drive.google.com/file/d/1lo19N-Waz7A6Uz05NVJW1dCAtjfb651E/view?usp=sharing