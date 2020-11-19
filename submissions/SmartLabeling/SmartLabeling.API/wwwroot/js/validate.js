document.addEventListener('DOMContentLoaded', async (event) => {

    // initialize

    //document.querySelector("#start").onclick = function () {
    //    cameraConnection.invoke("StartCameraStreaming");
    //    sensorsConnection.invoke("StartSensorsStreaming");

    //    document.querySelector("#start").style.display = "none";
    //    document.querySelector("#stop").style.display = "block";
    //    document.querySelector("#save_csv").disabled = true;
    //}

    //document.querySelector("#clear_list").onclick = function () {
    //    clearList();
    //}

    //document.querySelector("#load_datasets").onclick = function () {
        getDatasets();
    //}

    document.querySelector("#train").onclick = function () {
        startMLTraining();
    }
})

function populateDatasets(data) {
    if (data !== undefined) {
        document.querySelector("#datasets").innerHTML = "";
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

function getDatasets() {

    var url = "main/datasets";

    fetch(url, {
        method: 'GET',
        mode: 'cors'
    })
        .then((response) => response.json())
        .then(function (data) {
            populateDatasets(data);
        })
        .catch(function (err) {
            console.log(err.message);
        });
}

function startMLTraining() {

    document.querySelector("#micro_accuracy_label").innerHTML = "";
    document.querySelector("#macro_accuracy_label").innerHTML = "";
    document.querySelector("#micro_accuracy").innerHTML = "";
    document.querySelector("#macro_accuracy").innerHTML = "";

    var url = "main/train_ml";

    fetch(url, {
        method: 'GET',
        mode: 'cors'
    })
        .then((response) => response.json())
        .then(function (data) {
            document.querySelector("#micro_accuracy_label").innerHTML = "Micro accuracy: ";
            document.querySelector("#macro_accuracy_label").innerHTML = "Macro accuracy: ";
            document.querySelector("#micro_accuracy").innerHTML = (data.microAccuracy * 100).toFixed(2) + "%";
            document.querySelector("#macro_accuracy").innerHTML = (data.macroAccuracy * 100).toFixed(2) + "%";
        })
        .catch(function (err) {
            console.log(err.message);
        });
}

function getPredictionBySensors(data) {

    let url = 'main/predict_image';

    document.querySelector("#").innerHTML = "?";

    fetch(url, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/octet-stream'
        },
        body: data
    })
        .then((response) => response.json())
        .then(function (data) {
            if (data !== undefined) {
                //document.querySelector("#").innerHTML = data;
            }
            //populateList(sensors, capture);
        })
        .catch(function (err) {
            console.log(err.message);
        });
}

/*
<li class="list-group-item d-flex justify-content-between align-items-center">
    <input class="form-check-input mr-1" type="checkbox" value="" aria-label="...">
    Morbi leo risus
    <span class="badge bg-primary rounded-pill">1</span>
</li>
*/

function populateDatasets(data) {

    var html = "";
    for (var file of data) {
        html += '<li class="list-group-item d-flex justify-content-between align-items-center">';
        html += `<input class="form-check-input mr-1" type="checkbox" checked value="" aria-label="...">`;
        html += `${file.fileName}`;
        html += `<span class="badge bg-primary rounded-pill">${file.rowsCount}</span>`;
    }

    document.querySelector("#datasets").innerHTML = html;
}

function clearList() {
    document.querySelector("#datasets").innerHTML = "";
}
