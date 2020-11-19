var isInceptionTrained = false;
var sensorsUrl;
var sensorsHub;
var sensors = {};

document.addEventListener('DOMContentLoaded', async (event) => {

    // initialize
    document.querySelector("#stop").style.display = "none";
    document.querySelector("#live").style.display = "none";

    await getSettings().then((response) => response.json())
        .then(function (data) {
            sensorsUrl = data.isFakingIoT ? data.fakeUrl : data.sensorsUrl;
            sensorsHub = data.sensorsHub;
        })
        .catch(function (err) {
            console.log(err.message);
        });

    ////////////////////// SENSORS /////////////////////////////
    const sensorsConnection = new signalR.HubConnectionBuilder()
        .configureLogging(signalR.LogLevel.Information)
        .withUrl(sensorsUrl + sensorsHub)
        .build();

    sensorsConnection.on("sensorsStreamingStarted", function () {
        console.log("SENSORS STREAMING STARTED");
        document.querySelector("#live").style.display = "block";
        sensorsConnection.stream("SensorsCaptureLoop").subscribe({
            close: false,
            next: data => {
                populateSensorsData(data);
            },
            err: err => {
                console.log(err);
            },
            complete: () => {
                console.log("finished sensors streaming");
            }
        });
    });

    sensorsConnection.on("sensorsStreamingStopped", function () {
        document.querySelector("#live").style.display = "none";
        console.log("SENSORS STREAMING STOPPED");
    });

    sensorsConnection.on("sensorsDataCaptured", function (data) {
        console.log(`sensors data captured, ${data}`);
    });

    sensorsConnection.start();

    ////// EVENTS ///////////////////////////////////////////////

    document.querySelector("#start").onclick = function () {
        sensorsConnection.invoke("StartSensorsStreaming");

        document.querySelector("#start").style.display = "none";
        document.querySelector("#stop").style.display = "block";
        document.querySelector("#save_csv").disabled = true;
    }

    document.querySelector("#stop").onclick = function () {
        sensorsConnection.invoke("StopSensorsStreaming");

        document.querySelector("#start").style.display = "block";
        document.querySelector("#stop").style.display = "none";
        document.querySelector("#save_csv").disabled = false;
    }

    document.querySelector("#clear_list").onclick = function () {
        clearList();
    }

    document.querySelector("#save_csv").onclick = function () {
        saveCsv();
    }
})

function populateSensorsData(data) {
    if (data !== undefined) {
        if (data.luminosity !== undefined) {
            sensors.luminosity = data.luminosity;
            document.querySelector("#lux").innerHTML = `${data.luminosity} %`;
        }
        if (data.temperature !== undefined) {
            sensors.temperature = data.temperature;
            document.querySelector("#temp").innerHTML = `${data.temperature} &deg;C`;
        }
        if (data.infrared !== undefined) {
            sensors.infrared = data.infrared;
            document.querySelector("#infra").innerHTML = `${data.infrared} %`;
        }
        if (data.createdAt !== undefined) {
            sensors.createdAt = data.createdAt;
        }
        if (data.source !== undefined) {
            sensors.source = data.source;
            document.querySelector("#source").innerHTML = `${data.source}`;
        }
        populateList(sensors);
    }
}

async function getSettings() {

    var url = "main/settings";

    var result = fetch(url, {
        method: 'GET',
        mode: 'cors'
    })

    return result;
}

function populateList(sensors) {

    if (sensors.temperature !== undefined
        && sensors.luminosity !== undefined
        && sensors.infrared !== undefined
        && sensors.source !== undefined
        && sensors.createdAt !== undefined) {

        var checked = sensors.source == null || (sensors.source !== null && sensors.source.trim() == "") ? "" : "checked";

        document.querySelector("#readings tbody").innerHTML = `<tr><td>${sensors.temperature}</td><td>${sensors.luminosity}</td><td>${sensors.infrared}</td><td>${sensors.createdAt}</td><td>${sensors.source}</td><td><input type='checkbox' ${checked} /></td></tr>`
            + document.querySelector("#readings tbody").innerHTML;
    }
}

function clearList() {
    document.querySelector("#readings tbody").innerHTML = "";
}

function tableToJson(table) {
    var data = [];
    for (var i = 1; i < table.rows.length; i++) {
        var tableRow = table.rows[i];
        if (tableRow.cells[5].innerHTML.includes(" checked")) {
            var rowData = {
                temperature: tableRow.cells[0].innerHTML,
                luminosity: tableRow.cells[1].innerHTML,
                infrared: tableRow.cells[2].innerHTML,
                createdAt: tableRow.cells[3].innerHTML,
                source: tableRow.cells[4].innerHTML,
            };
            data.push(rowData);
        }
    }
    return data;
}

function saveCsv() {
    let url = 'main/save_csv';
    var list = tableToJson(document.querySelector("#readings"))

    fetch(url, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(list)
    })
        .then((response) => response.json())
        .then(function (data) {
            alert("Data saved to csv!");
            clearList();
        })
        .catch(function (err) {
            console.log(err.message);
        });
}