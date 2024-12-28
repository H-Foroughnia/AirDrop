function MusicSheetPagin() {
  //   let request;
  //   if (window = XMLHttpRequest){
  //   request = new XMLHttpRequest()
  //   }
  //   else{
  //  request = new ActiveXObject("Microsoft.XMLHTTP")
  //   }

  //   request.open("GET" ,"login.html")
  //   request.onreadystatechange = function(){
  //     if(request.readyState = XMLHttpRequest.DONE && request.status ===200 ){
  //        document.getElementById("musicSheet_box").innerHTML = request.responseText;
  //       }
  //       else{
  //       document.getElementById("musicSheet_box").innerHTML = 'Error fetching data: ' + request.status;
  //       }
  //     };
  //     request.send();
  fetch("login.html")
    .then((response) => response.text())
    .then(
      (text) => (document.getElementById("musicSheet_box").innerHTML = text)
    );
}
function NotesPagin() {
  fetch("./ajax-data/NotesPage.html")
    .then((response) => response.text())
    .then((text) => (document.getElementById("Notes_box").innerHTML = text));
}

