## Team HighFive 

### Idea
#### *Problem Statement*
Be it a black friday sale or a normal weekday, systems need to be up all the time . Predicting load for a particular system will help devops/engineering system to setup systems in such a way that irrespective of an occasion, end user has seamless experience.
In this hackathon - we will take example of banking system where delay in different services can lead to poor customer experience. Better understanding load of these services using machine learning techniques can help individual bank to serve more customers. Perform proper server utilization and cost effectiveness. 

#### *Our problem statemnt example*
We chose banking domain as our example in this problem statement. In Banks, there are multiple services but we have narrowed to withdrawal service since it is heavily used and whatever solution we will propose, can be applied to other services( like mutual funds etc) as well . We will build a machine learning model which for a given day, can predict how many customers can potentially  use withdrawal service, indirectly predicting load of a service or system. Based on which bank can prepare their systems in better way.

### Team contact
For questions on this submission you can contact: <br>
<a href="mailto:moresumit416@gmail.com"><img src="https://img.shields.io/badge/gmail-%23DD0031.svg?&style=for-the-badge&logo=gmail&logoColor=white"/></a> : Sumeet More <br>
<a href="mailto:rohan.ghodke@gmail.com"><img src="https://img.shields.io/badge/gmail-%23DD0031.svg?&style=for-the-badge&logo=gmail&logoColor=white"/></a> : Rohan Ghodke 

### Solution
- As explained in problem statement, we are trying to predict load of withdrawal service of a bank for a given day using machine learning techniques.
- We classified our machine learning problem into time series regression.
- We used two approaches -  one based on machine learning algorithm(Singular spectrum analysis) and other based on deep learning algorithm(LSTM)
- We didn't get any data online hence we artifically created it and below is snippet how it looks. First column corresponds to particular date and second column corresponds to count of withdrawal being used or no of times withdrawal service was hit indirectly telling us load. This dataset can be easily constructed for any service with the help of logging tools.

    | Date       | Count(count of service being used) |
    |:------------:|:------------------------------------:|
    | 2020-11-02 | 467                                |
    | 2020-11-03 | 433                                |
    | 2020-11-04 | 406                                |
- Using both approaches, we were able to predict load for future dates (how code and output looks, model loss, test data prediction and overall time series prediction graph are mentioned in the [Presentation Deck](https://docs.google.com/presentation/d/1PXjrO0D6bjDT6jtkItrqFUNXVmp7idSPa-MsqxEwpfE/edit#slide=id.ga241919b73_0_20) )

 - Machine learning approach was purely developed in MLdotnet. Deep learning LSTM model was developed in python and tensorflow but it can be exported to onnx model and consumed by the MLdotnet and create REST api out of it.

### Video presentation
[Presentation video](https://www.awesomescreenshot.com/video/1754881?key=b04496d7acca061ed9c75037e9c87a4a)


### Deck
[Presentation Deck](https://docs.google.com/presentation/d/1PXjrO0D6bjDT6jtkItrqFUNXVmp7idSPa-MsqxEwpfE/edit?usp=sharing)

### Custom/Self generated Dataset
[Dataset](https://drive.google.com/file/d/1V-tfSfxH398WEbatqEDJvW1FoNjEH0fn/view?usp=sharing)





