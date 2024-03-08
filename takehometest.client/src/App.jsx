import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [forecasts, setForecasts] = useState();

    useEffect(() => {
        populateWeatherData();
    }, []);

    const contents = forecasts === undefined
        ? <p><em>Loading... Please refresh once the backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.id}>
                        <td>{forecast.id}</td>
                        <td>{forecast.date}</td>
                        <td>{forecast.temperatureC}</td>
                        <td>{forecast.temperatureF}</td>
                        <td>{forecast.summary}</td>
                        <td>
                            <button className="button" onClick={() => { openUpdateRecordModal(forecast) }} > Update </button>
                            <button className="button" onClick={() => { deleteWeatherData(forecast.id) }} > Delete </button>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>;

    const addRecordModal =
        <div id="myModal" className="modal">
            <div className="modal-content">
                <span className="close" onClick={() => { closeModal() }}>&times;</span>
                <h1 id="modalTitle">Add Record</h1>
                <table className="table table-striped table-modal" aria-labelledby="tabelLabel">
                    <tr>
                        <th>
                            Id
                        </th>
                        <td>
                            <input disabled={true} id="idinput" ></input>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Date
                        </th>
                        <td>
                            <input id="dateinput" ></input>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Temp. (C)
                        </th>
                        <td>
                            <input id="tempcinput"></input>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Temp. (F)
                        </th>
                        <td>
                            <input id="tempfinput"></input>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Summary
                        </th>
                        <td>
                            <input id="summaryinput"></input>
                        </td>
                    </tr>
                </table>
                <button className="button" id="modalSaveButton" onClick={() => { populateWeatherData() }} > Save </button>
            </div>
        </div>

    return (
        <div>
            <h1 id="tabelLabel">Weather forecast</h1>
            <button className="button" onClick={() => { populateWeatherData() }} > Refresh data </button>
            {forecasts === undefined ? "" : <button className="button" id="myBtn" onClick={() => { openAddRecordModal() }} > Add Record </button>}
            {contents}
            {addRecordModal}
        </div>
    );

    function openAddRecordModal() {
        // clear inputs
        var idInput = document.getElementById("idinput");
        idInput.value = null;
        var dateinput = document.getElementById("dateinput");
        dateinput.value = null;
        var tempcinput = document.getElementById("tempcinput");
        tempcinput.value = null;
        var tempfinput = document.getElementById("tempfinput");
        tempfinput.value = null;
        var summaryinput = document.getElementById("summaryinput");
        summaryinput.value = null;

        openModal();
        var title = document.getElementById("modalTitle");
        title.innerHTML = "Add Record";
        var saveButton = document.getElementById("modalSaveButton");
        saveButton.innerHTML = "Save";
        saveButton.onclick = () => createWeatherData();
    }

    function openUpdateRecordModal(data) {
        //populate inputs
        var idInput = document.getElementById("idinput");
        idInput.value = data.id;
        var dateinput = document.getElementById("dateinput");
        dateinput.value = data.date;
        var tempcinput = document.getElementById("tempcinput");
        tempcinput.value = data.temperatureC;
        var tempfinput = document.getElementById("tempfinput");
        tempfinput.value = data.temperatureF;
        var summaryinput = document.getElementById("summaryinput");
        summaryinput.value = data.summary;

        var title = document.getElementById("modalTitle");
        title.innerHTML = "Update Record";
        var saveButton = document.getElementById("modalSaveButton");
        saveButton.innerHTML = "Update";
        saveButton.onclick = () => updateWeatherData();
        openModal();
    }

    function closeModal() {
        var modal = document.getElementById("myModal");
        modal.style.display = "none";
    }

    function openModal() {
        var modal = document.getElementById("myModal");
        modal.style.display = "block";
    }

    function getInputData() {
        var idInput = document.getElementById("idinput");
        var dateinput = document.getElementById("dateinput");
        var tempcinput = document.getElementById("tempcinput");
        var tempfinput = document.getElementById("tempfinput");
        var summaryinput = document.getElementById("summaryinput");
        var data =
        {
            id: idInput.value,
            date: dateinput.value,
            temperatureC: Number(tempcinput.value),
            temperatureF: Number(tempfinput.value),
            summary: summaryinput.value
        };
        return data;
    }

    async function populateWeatherData() {
        const response = await fetch('weatherforecast');
        const data = await response.json();
        setForecasts(data);
    }

    async function updateWeatherData() {
        var data = getInputData();
        console.log(data);
        const response = await fetch('weatherforecast/update',
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data)
            });
        const result = await response.json();
        console.log(result);
        closeModal();
    }

    async function createWeatherData() {
        var data = getInputData();
        const response = await fetch('weatherforecast/create',
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data)
            });
        const result = await response.json();
        console.log(result);
        closeModal();

    }
    async function deleteWeatherData(id) {
        const response = await fetch("weatherforecast/delete?id=" + id,
            {
                method: "DELETE",
            });
        const result = await response.json();
        console.log(result);
        populateWeatherData();
    }
}

export default App;