$("#request").click(function () {
    /*var string = $("#count").val();
    
    
    if (string != "") {
        sendRequest(string);
    }
    else {
        $("td").empty();
        $("tr").empty();
        $("th").empty();
        $("h4").empty();
        $('#student').append($('<h4>No result for empty name</h4>'));
        console.log('Error on AJAX return');
    }*/
    var source = '/Home/ShowAll';
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: source,
        success: showAllAthletes,
        error: errorOnAjax
    });


});
function showAllAthletes(data) {
    console.log(data);
    $('#btn').empty();
    $('#resultAll').append($('<div id="btn"></div>'));
    for (var i = 0; i < data.length; ++i) {
        var n = i + 1;
        //'<button type="button" onclick="return showStudent(\'' + data[i] + '\')">Click</button>'));
        $('#btn').append($('<li><button type="button" onclick="return sendRequest(\'' + data[i] + '\')">' + data[i] + '</button></li>'));
        $('#btn').append($('<br />'));


    }
}
function sendRequest(string) {

    var source = '/Home/SearchJson?studentName=' + string;
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: source,
        success: showData,
        error: errorOnAjax
    });
}
function showData(data) {
    window.scrollBy(0, 1000);
    if (data.length == 0) {
        $("h4").empty();
        $("td").empty();
        $("tr").empty();
        $("th").empty();
        $('#result').append($('<h2>There is no race Time For this Atlete</h2>'));
    }
    else {
    console.log(data);
    var string = $("#count").val();
    $("h4").empty();
    $('#result').append($('<h4>Showing Result for:"' + data[0].Name + '"</h4>'));

    $("td").empty();
    $("tr").empty();
    $("th").empty();

    $('#header').append($('<th>Date</th>'));
    $('#header').append($('<th>Event Title</th>'));
    $('#header').append($('<th>Meet Title</th>'));
    $('#header').append($('<th>Time</th>'));

    for (var i = 0; i < data.length; ++i) {
        var date = new Date(parseInt(data[i].Date.substr(6)));
        var curr_date = date.getDate();
        var curr_month = date.getMonth() + 1;
        var curr_year = date.getFullYear();

        $('.table').append($('<tr><td>' + curr_month + "-" + curr_date + "-" + curr_year + '</td><td>'
            + data[i].EventTitle + '</td><td>' + data[i].MeetTitle + '</td><td>' + data[i].Time + '</td></tr>'));


    }
    }
}

$(document).ready(function () {
   // if (window.location == '@Url.Action("StudentStat", "Home")') {
        var source = '/Home/showTeam';
        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: source,
            success: showTeamData,
            error: errorOnAjax
        });
    //} 

    


});

function showTeamData(data) {
    console.log(data);
    for (var i = 0; i < data.length; ++i) {
        var n = i + 1;
        //'<button type="button" onclick="return showStudent(\'' + data[i] + '\')">Click</button>'));
        $('#teams').append($('<button type="button" onclick="return showStudent(\'' + data[i] + '\')">' + data[i] + '</button>'));
        
    }

}
function showStudent(string) {

    var source = '/Home/showStudent?teamName=' + string;
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: source,
        success: showStudentData,
        error: errorOnAjax
    });
}
function showStudentData(data) {
    console.log(data);
    $('#studData').empty();
    //$('h2').empty();
    //$('#students').append($('<h2 id="sheader">Student Name:</h2>'));
    $('#students').append($('<div id="studData"></div>'));
    
    
    for (var i = 0; i < data[0].length; ++i) { 


       
        $('#studData').append($('<button type="button" onclick="return showStudentDistance(\'' + data[0][i].Name + '\')">' + data[0][i].Name+'</button>'));


    }
}
function showStudentDistance(name) {

    var source = '/Home/showStudentDistance?name=' + name;
    $.ajax({
        type: 'GET',
        dataType: 'Json',
        url: source,
        success: showDistance,
        error: errorOnAjax
    });
}
function showDistance(data) {
    console.log(data);
    $('#studDistance').empty();
    $('#distance').append($('<div id="studDistance"></div>'));
    $('p').empty();
    $('#distance').append($('<p>Showing race distance for: ' + data[0].Name + '</p>'));
    for (var i = 0; i < data.length; i++) {
        $('#studDistance').append($('<button type="button" onclick="return studentRaceGraph(\'' + data[i].Name + '\',\'' + data[i].EventTitle + '\')">' + data[i].EventTitle +'</button>'));
        //$('#students').append($('<button type="button" onclick="return showStudentDistance(\'' + data[0][i].Name + '\')">' + 'Show Student Statistic</button>'));
        
    }
}
function studentRaceGraph(name, distance) {
    var source = '/Home/studentRaceGraph?name=' + name + '&n=' + distance;
    $.ajax({
        type: 'GET',
        dataType: 'Json',
        url: source,
        success: showGraph,
        error: errorOnAjax
    });
}
function showGraph(data) {
    console.log(data);
    var xa = [];
    var ya = [];
    var date1 = new Date(parseInt(data[0].Date.substr(6)));
    if (data.length == 1) {
        ya.push(0);
        xa.push(date1);
    } {
        for (var i = 0; i < data.length; i++) {
            var date = new Date(parseInt(data[i].Date.substr(6)));
            ya.push(data[i].Time);
            xa.push(date);

        }
    }
    var trace = {
        x: xa,
        y: ya,
        mode: 'lines',
        type: 'scatter'
    };
    var data = [trace];
    var layout = {};
    Plotly.newPlot('theplot', data, layout);
}


function errorOnAjax() {
    console.log('Error on AJAX return');
}