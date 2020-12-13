document.getElementById("showActualErrorButton").addEventListener("click", ShowEntireError);

function ShowEntireError() {
    document.getElementById('actualIssueDiv').style.visibility = "visible";
    document.getElementById('userFriendlyIssueDiv').style.visibility = "hidden";
    document.getElementById('userFriendlyIssueDiv').style.height = 0;
}