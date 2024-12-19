
function validateInput(input, pattern, error) {
  console.log(input);
  var data = input.value;
  var regex = pattern;
  var label = document.createElement("label");

  if (regex.test(data)) {
    input.classList.remove("invalid");
    input.classList.add("valid");
    var existingLabel = document.getElementById(input.id + "InputLabel");
    if (existingLabel) {
      existingLabel.remove();
    }
  } else {
    input.classList.remove("valid");
    input.classList.add("invalid");
    // Clear any existing label before appending the new one
    var existingLabel = document.getElementById(input.id + "InputLabel");
    if (existingLabel) {
      existingLabel.remove();
    }

    // Append the new label next to the input field
    label.id = input.id + "InputLabel";
    label.innerText = error;
    label.classList.add("labelClass");
    // input.parentNode.insertBefore(label, input.nextSibling);
    const list = document.querySelector(".form-control-box");
    list.insertBefore(label, list.children[1]);
  }
}

const moreButtons = document.querySelectorAll(".more");
moreButtons.forEach(function (button) {
  button.addEventListener("click", function () {
    const moreBox = button.nextElementSibling; 
    // Selects the next element sibling, which is the .moreBox div
    if (moreBox.style.display === "block") {
      moreBox.style.display = "none";
    } else {
      moreBox.style.display = "block";
    }
  });
});

const menus = document.querySelectorAll(".menu");
menus.forEach(function (menus) {
  menus.addEventListener("click", function () {
    if (menus.nextElementSibling.style.display === "block") {
      menus.nextElementSibling.style.display = "none";
    } else {
      menus.nextElementSibling.style.display = "block";
    }
  });
});

const secondMenu = document.querySelectorAll(".menu-2");
secondMenu.forEach(function (secondMenu) {
  secondMenu.addEventListener("click", function () {
    if (secondMenu.nextElementSibling.style.display === "block") {
      secondMenu.nextElementSibling.style.display = "none";
    } else {
      secondMenu.nextElementSibling.style.display = "block";
    }
  });
});


$(document).ready(function(){
  $('.menu').click(function(){
      let arrows = $(this).find('.arrow');
      arrows.toggleClass('rotate');
  });

  $('.menu-2').click(function () { 
    let arrows = $(this).find('.arrow');
    arrows.toggleClass('rotate');
  });
}); 





// Modal functions
function deleteAction() {
  closeModal();

}

function cancelAction() {
  closeModal();
  closeEditModal();
  closeAddPlanModal();
}

function closeModal() {
  let modal = document.querySelectorAll(".removeUser-modal")
  modal.forEach(function(modalElement){
      modalElement.style.display = "none"
  }) 
}
function openModal() {
  let modal = document.querySelectorAll(".removeUser-modal");
  modal.forEach(function(modalElement){
  modalElement.style.display = 'block'
  })
}
function closeEditModal(){
  let modal = document.querySelectorAll(".editProfile-modal , .editPlan-modal");
  modal.forEach(function(modalElement){
    modalElement.style.display = 'none'
    })
}
function openEditModal(){
  let modal = document.querySelectorAll(".editProfile-modal , .editPlan-modal");
  modal.forEach(function(modalElement){
  modalElement.style.display = 'block'
  })
}
function openAddPlanModal(){
  let modal = document.querySelectorAll(" .addPlan-modal");
  modal.forEach(function(modalElement){
  modalElement.style.display = 'block'
  })
}
function closeAddPlanModal(){
  let modal = document.querySelectorAll(" .addPlan-modal");
  modal.forEach(function(modalElement){
  modalElement.style.display = 'none'
  })
}


// input-upload-controls
$(document).ready(function() {
  function formatFileSize(bytes) {
    const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
    if (bytes === 0) return '0 Byte';
    const i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)));
    return Math.round(bytes / Math.pow(1024, i), 2) + ' ' + sizes[i];
  }

  $('.file-input').each(function() {
    let fileInput = $(this);
    let container = fileInput.closest('.file-container');
    let fileNameDisplay = container.find('.file-name');
    let fileSizeDisplay = container.find('.file-size');
    let preview = container.find('.preview');

    fileInput.on('change', function() {
      if (fileInput[0].files.length > 0) {
        let file = fileInput[0].files[0];
        let fileSize = formatFileSize(file.size);
        fileSizeDisplay.text(fileSize);
        fileNameDisplay.text(file.name);

        // Clear any previous preview
        preview.empty();

        // Only proceed if the file is an image
        if (file.type.startsWith('image/')) {
          let reader = new FileReader();
          reader.onload = function(e) {
            let img = $('<img>').attr('src', e.target.result);
            preview.append(img);
          };
          reader.readAsDataURL(file);
        } else {
          preview.text('');
        }
      } else {
        fileNameDisplay.text('No file chosen');
        preview.empty();
      }
    });
  });
});