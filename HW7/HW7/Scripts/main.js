$(document).ready(function () {
    var source = '/Home/UserProfile';
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: source,
        success: showProfile,
        error: errorOnAjax
    });

    var urls = '/Home/UserRepo';
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: urls,
        success: UserReposite,
        error: errorOnAjax
    });
    
    
});


function showProfile(data) {
    console.log(data);
    $('#fullName').text(data.fullName);
    $('#nameLink').attr("href", data.html_url);
    $('#userName').text(data.userName);
    $('#email').text(data.email);
    $('#school').text(data.company);
    $('#schoolLocation').text(data.Location);
    $('#profilePic').attr("src", data.myPic);


    
    
}

function UserReposite(data) {
    
    console.log(data);
    
    for (var i = 0; i < data.length; ++i) {
        $('#thePlot').append($('<a href="' + data[i].HtmlUrl +'"><h2>' + data[i].reposName + '</h4></a>'));
        $('#thePlot').append($('<h4>' + data[i].Owner + '</h4>'));
        $('#thePlot').append($('<h4>' + data[i].LastModified + '</h4>')); 
        //$('#thePlot').append($('<h4>' + data[i].HtmlUrl + '</h4>'));
        //$('#thePlot').append($('<li>' + data[i].FullName + '</li>'));
        //<img id="profilePic" alt="Profile Pictures">
        $('#thePlot').append($('<img style="height: 100px; width: 100px;" src="' + data[i].OwnerPic + '" alt="Profile Pictures">'));
         
        $('#thePlot').append($('<br />')); 
        $('#thePlot').append($('<br />')); 
        $('#thePlot').append($('<button type="button" onclick="return commitsFunction(\'' + data[i].Owner + '\',\'' + data[i].reposName+ '\')">Click</button>'));
        
        $('#thePlot').append($('<br />')); 
        $('#thePlot').append($('<br />')); 

    }
}
function displayTable(data) {
    console.log(data);
    //alert(data.length);
    $("td").empty();
    $("tr").empty();
    $("#header").empty();

    $('#header').append($('<th>SHA</th>'));
    $('#header').append($('<th>Time Stamp</th>'));
    $('#header').append($('<th>Committer Name</th>'));
    $('#header').append($('<th>Commit Message</th>'));


        /*<tr id="shaRow">
        <tr id="timeRow">
        <tr id="committerRow">
        <tr id="MessageRow">*/

       
    for (var i = 0; i < data.length; ++i) {
        //$("strong")
        var str = data[i].Sha;
        var shortIt = str.slice(0, 9);
        //HtmlUrl
        $('.table').append($('<tr><td><a href="' + data[i].HtmlUrl + '"><div style="height:100%;width:100%">' + shortIt + '</div></a></td><td>' + data[i].TimeStamp + '</td><td>' + data[i].Committer + '</td><td>' + data[i].CommitMessage + '</td></tr>'));
        /*$('#lists').append($('<tr>' + data[i].TimeStamp + '</tr>'));
        $('#lists').append($('<tr>' + data[i].Committer + '</tr>'));
        $('#lists').append($('<tr>' + data[i].CommitMessage + '</tr>'));*/
        

    }
    $('#commits').append($('<br />'));
    $('#commits').append($('<br />'));
    $('#commits').append($('<br />'));
    $('#commits').append($('<br />'));
}
function commitsFunction(userName, repoName) {
    
    var source = '/Home/Commits?user=' + userName + '&repo=' + repoName;
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: source,
        success: displayTable,
        error: errorOnAjax
    });


}

function errorOnAjax() {
    console.log('Error on AJAX return');
}