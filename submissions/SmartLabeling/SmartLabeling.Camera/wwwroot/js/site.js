document.addEventListener('DOMContentLoaded', (event) => {

    const connection = new signalR.HubConnectionBuilder()
        .configureLogging(signalR.LogLevel.Information)
        .withUrl("/camerahub")
        .build();

    connection.on("cameraStreamingStarted", function () {
        console.log("CAMERA STREAMING STARTED");
        connection.stream("CameraCaptureLoop").subscribe({
            close: false,
            next: data => {
                populateData(data);
            },
            err: err => {
                console.log(err);
            },
            complete: () => {
                console.log("finished camera streaming");
            }
        });
    });

    connection.on("cameraStreamingStopped", function () {
        console.log("CAMERA STREAMING STOPPED");
    });

    connection.on("cameraImageCaptured", function (data) {
        console.log(`image captured, size: ${data}`);
    });

    connection.on("cameraImageNotCaptured", function () {
        console.log(`image not captured, IoT device error`);
    });

    connection.start();
})

function populateData(data) {
    if (data !== undefined) {
        if (data.image !== undefined) {
            document.querySelector("#camera").setAttribute("src", `data:image/jpg;base64,${data.image}`);
        }
    }
}

