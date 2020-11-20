document.addEventListener('DOMContentLoaded', (event) => {

    document.querySelector("#labeling_status").innerHTML = "[unlabeled]";

    const connection = new signalR.HubConnectionBuilder()
        .configureLogging(signalR.LogLevel.Information)
        .withUrl("/sensorshub")
        .build();

    connection.on("sensorsStreamingStarted", function () {
        console.log("SENSORS STREAMING STARTED");
        connection.stream("SensorsCaptureLoop").subscribe({
            close: false,
            next: data => {
                populateData(data);
            },
            err: err => {
                console.log(err);
            },
            complete: () => {
                console.log("finished sensors streaming");
            }
        });
    });

    connection.on("sensorsStreamingStopped", function () {
        console.log("SENSORS STREAMING STOPPED");
    });

    connection.on("sensorsDataCaptured", function (data) {
        console.log(`sensors data captured, ${data}`);
    });

    connection.on("sensorsDataNotCaptured", function () {
        console.log(`sensors data not captured, IoT device error`);
    });

    connection.start();

    document.querySelector("#source_lighter").onclick = function () {
        document.querySelector("#source").innerHTML = document.querySelector("#source_lighter").value;
        document.querySelector("#labeling_status").innerHTML = "[labeled]";
        connection.invoke("ChangeSource", document.querySelector("#source_lighter").value);
    }
    document.querySelector("#source_flashlight").onclick = function () {
        document.querySelector("#source").innerHTML = document.querySelector("#source_flashlight").value;
        document.querySelector("#labeling_status").innerHTML = "[labeled]";
        connection.invoke("ChangeSource", document.querySelector("#source_flashlight").value);
    }
    document.querySelector("#source_infrared").onclick = function () {
        document.querySelector("#source").innerHTML = document.querySelector("#source_infrared").value;
        document.querySelector("#labeling_status").innerHTML = "[labeled]";
        connection.invoke("ChangeSource", document.querySelector("#source_infrared").value);
    }
    document.querySelector("#source_daylight").onclick = function () {
        document.querySelector("#source").innerHTML = document.querySelector("#source_daylight").value;
        document.querySelector("#labeling_status").innerHTML = "[labeled]";
        connection.invoke("ChangeSource", document.querySelector("#source_daylight").value);
    }
    document.querySelector("#source_cross").onclick = function () {
        document.querySelector("#source").innerHTML = "";
        document.querySelector("#labeling_status").innerHTML = "[unlabeled]";
        connection.invoke("ChangeSource", " ");
    }
})

function populateData(data) {
    if (data !== undefined) {
        if (data.luminosity !== undefined)
            document.querySelector("#lux").innerHTML = `${data.luminosity} %`;
        if (data.temperature !== undefined)
            document.querySelector("#temp").innerHTML = `${data.temperature} &deg;C`;
        if (data.infrared !== undefined)
            document.querySelector("#infra").innerHTML = `${data.infrared} %`;
    }
}