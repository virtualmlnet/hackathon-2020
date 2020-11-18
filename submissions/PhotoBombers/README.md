## Team PhotoBombers

### Idea
We thought it would be fun to photobomb people's photos... Not!    
We thought it's great to help people tag photos based on the objects found in them.    
Then, they can use these tags to easily find/filter/organize their photo collection.


### Team contact
For questions on this submission you can contact:    
Paul Amazona: whatevergeek@outlook.com    
I'm also Paul (whatevergeek@outlook.com) in the hackathon-2020 channel at  virtual-mlnet.slack.com

### Solution
The problem is solved by leveraging the image file's [exif](https://en.wikipedia.org/wiki/Exif) metadata info.    
exif has a tag property ([40094](https://www.exiv2.org/tags.html)) that can be used to store the keywords that represent the objects found in the photo.   

For object detection, I used the tiny yolo model from the [ML.NET Object Detection Tutorial](https://docs.microsoft.com/en-us/dotnet/machine-learning/tutorials/object-detection-onnx).
The main object detection project is based on this. I just turned it into a library and created some API methods to be used in the PhotoBomb CLI Tool.

PhotoBomb CLI Tool is the main deliverable of this [MVP solution](https://github.com/photobombers/photobomb/projects/1 ).
Given a directory containing photos (*.jpg), it is used to tag objects found in those photos. The user can then filter windows search using tag.
Or use exiftool in linux.

### Project Repository    
https://github.com/photobombers/photobomb 

#### PhotoBomb CLI Tool Version Used in the Demo
https://github.com/photobombers/photobomb/releases 

#### Data Used in the Demo
* Photos from [ML.NET Object Detection Tutorial](https://docs.microsoft.com/en-us/dotnet/machine-learning/tutorials/object-detection-onnx)
* Photos from [Open Images 2019 - Object Detection](https://www.kaggle.com/c/open-images-2019-object-detection)

#### Hackathon Tasks    
https://github.com/photobombers/photobomb/projects/1    
#### Post Hackathon Tasks (for further community collaboration)    
https://github.com/photobombers/photobomb/projects/2    



### Video presentation 
[![PhotoBomb Demo](http://img.youtube.com/vi/NRWafaQ4cqo/0.jpg)](http://www.youtube.com/watch?v=NRWafaQ4cqo "PhotoBomb Demo")