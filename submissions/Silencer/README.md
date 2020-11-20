## Team ML.NET to the moon and back!

### Idea
I thought it would be fun to really test how far I can go with ML.NET and F#. Less so in the validity of my model (it would need a lot of extra work) but rather in the type of application one can build with it. I'd say, that a Chrome extension built in Bolero WebAssembly is pretty much as fringe as it gets right now (apart from maybe building something for Tizen SmartTVs).

The goal is to have something in my browser, that checks my Twitter DMs for toxic comments and simple removes them before I have to see them. As all of this contains sensible information I don't want any outgoing web requests, so inference has to happen in the browser.

My original pitch can be found [here](https://github.com/virtualmlnet/hackathon-2020/issues/1).

### Team contact
For questions on this submission you can contact:

Gregor Beyerle: gregor_beyerle@outlook.com or on [Twitter](https://twitter.com/GBeyerle)

### Solution
Silencer™ is a Chrome Browser Extension, that starts to get active whenever you open up a message thread within your Twitter DMs. It scans the page for messages (and observes the thread for new messages), extracts the text, infers whether the text could be toxic and - in case it decides, that the text is just too rude - changes the text to a warning disclaimer.

The inference process is done using a ML.NET model running directly in the Browser via Bolero (WebAssembly), which is itself built on top of Blazor.

The repository can be found [here](https://github.com/WalternativE/Silencer).

### Video presentation
You can find the presentation video [on YouTube](https://youtu.be/FIKfwZ34KFI).
