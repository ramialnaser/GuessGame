
var unsentStrokes = [];
var uri;
var gameid = document.getElementById("gameId").innerText;
//alert(gameid);

var connection = new signalR.HubConnectionBuilder()
    .withUrl('/TheGame')
    .build();
connection.on('newStroke', drawStroke)
connection.on('clearCanvas', clearCanvas)
connection.start()
    .then(() => console.log('connected!'))
    .catch(err => console.error)

var canvas = document.getElementById('draw-canvas')
var ctx = canvas.getContext('2d')
ctx.lineWidth = 4

var clearButton = document.getElementById('clear')
clearButton.addEventListener('click', ev => {
    ev.preventDefault()
    if (confirm("Are you sure you want to clear everyone's canvases???")) {
        clearCanvas()
        connection.send('ClearCanvas')
    }
})

var saveButton = document.getElementById('save')
saveButton.addEventListener('click', ev => {    
    var dataURI = canvas.toDataURL();
    console.log(dataURI);
    
   

    var formData = new FormData();

    formData.append("gameid", gameid);
    formData.append("img", dataURI);

    fetch(`${urlGames}`, {
        method: 'PUT',
        body: formData
    })
        .then(response => response.text())
        .then(data => {
            window.location.href = "/Home/MyPage";
        })
        .catch(error => console.error('Unable to get Account.', error));



    clearCanvas();
    connection.send('ClearCanvas')

   

})


var colorButton = document.getElementById('color')

function clearCanvas() {
    ctx.clearRect(0, 0, canvas.width, canvas.height)
}

var penDown = false
var previous = { x: 0, y: 0, ts: 0 }

canvas.addEventListener('mousedown', mouseDown)
canvas.addEventListener('touchstart', mouseDown)

canvas.addEventListener('mouseup', mouseUp)
canvas.addEventListener('touchend', mouseUp)
canvas.addEventListener('touchcancel', mouseUp)

canvas.addEventListener('mousemove', mouseMove)
canvas.addEventListener('touchmove', mouseMove)

function mouseDown() {
    penDown = true
}

function mouseUp() {
    penDown = false
}

function mouseMove(ev) {
    ev.preventDefault();
    const millisecondsSinceLastStroke = (new Date()).getTime() - previous.ts
    if (penDown && millisecondsSinceLastStroke < 100) {
        var start = {
            x: previous.x - canvas.offsetLeft,
            y: previous.y - canvas.offsetTop
        }
        var end = {
            x: ev.pageX - canvas.offsetLeft,
            y: ev.pageY - canvas.offsetTop
        }
        drawStroke(start, end, colorButton.value)
        unsentStrokes.push({
            start: start,
            end: end,
            color: colorButton.value
        })
    }
    previous = {
        x: ev.pageX,
        y: ev.pageY,
        ts: (new Date()).getTime()
    }
}

function drawStroke(start, end, color) {
    color = color || "#000"
    ctx.strokeStyle = color
    ctx.beginPath()
    ctx.moveTo(start.x, start.y)
    ctx.lineTo(end.x, end.y)
    ctx.stroke()
}

setInterval(function () {
    if (unsentStrokes.length) {
        connection.send('NewStrokes', unsentStrokes);
        unsentStrokes = [];
    }
}, 250);


var sendButton = document.getElementById('sendvalue')
sendButton.addEventListener('click', ev => {
    var clientGuess = document.getElementById('clientGuess')
    if (clientGuess.value == null || clientGuess.value == "")
        alert("You cannot submit an empty guess value")
    else {
        console.log(clientGuess.value);
        clientGuess.disabled = true;
        sendButton.disabled = true;

        //var urdds = urlGames + gameid + "/" + clientGuess.value;
        //alert(urdds);


        fetch(`${urlGames}` + gameid + "/" + clientGuess.value, {
            method: 'PUT'            
        })
            .then(response => response.text())            
            .catch(error => console.error('Unable to get Account.', error));

    }


})