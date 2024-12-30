/*! jQuery v3.7.1 | (c) OpenJS Foundation and other contributors | jquery.org/license */



$(document).ready(function () {
    // Initialize the Persian datepicker
    $(".DateInput").pDatepicker({
        format: 'YYYY/MM/DD',  // Display format for Persian date
        viewMode: 'day',
        initialValue: false,
        autoClose: true
    });

    // Before form submission, convert the Persian date and time to Gregorian with the selected time
    $('form').submit(function () {
        var persianDate = $(".DateInput").val();  // Get the value from the Persian date input field
        var timeValue = $(".TimeInput").val();    // Get the value from the time input field

        if (persianDate && timeValue) {
            // Convert Persian date to a persianDate object
            var persianDateObj = new persianDate(persianDate);

            // Convert to Gregorian and format as 'YYYY-MM-DD'
            var gregorianDate = persianDateObj.toGregorian().format('YYYY-MM-DD');

            // Combine the gregorian date with the selected time in ISO format (e.g., 'YYYY-MM-DDTHH:MM:SS')
            var gregorianDateTime = gregorianDate + 'T' + timeValue + ':00';  // Combine with time input value

            // Set the value of the input field to the Gregorian DateTime
            $(".DateInput").val(gregorianDateTime);
        }
    });
});




//******* Delete Modal **********
//< !--delete Modal starts-- >
//                        <div id="deleteModal-@supportOffice.Id" class="fixed inset-0 z-20 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full  hidden">
//                            <div class="relative top-32 mx-auto p-5 border w-96 shadow-lg rounded-md bg-white">
//                                <div class="mt-3 text-center">
//                                    <div class="mx-auto flex items-center justify-center h-12 w-12">
//                                        <img src="~/admin-assets/media/icons/adminPanel-icons/danger.svg" alt="">
//                                    </div>
//                                    <h3 class="text-lg leading-6 font-medium text-gray-900">آیا از حذف مورد اطمینان دارید؟</h3>
//                                    <div class="mt-5 sm:mt-6">
//                                        <a href="javascript:void(0);" id="deleteLink-@supportOffice.Id" class="px-4 py-2 bg-red-500 text-white text-base font-medium rounded-md mr-2 mb-2">بله</a>
//                                        <button type="button" class="px-4 py-2 bg-gray-300 text-gray-700 text-base font-medium rounded-md mr-2 mb-2" onclick="cancelAction(@supportOffice.Id)">خیر</button>
//                                    </div>
//                                </div>
//                            </div>
//                        </div>
//                        <!--delete Modal ends-- >








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


$(document).ready(function () {
    $('.menu').click(function () {
        // Find the arrow elements within the clicked menu item
        let arrows = $(this).find('.arrow');

        // Toggle their rotation and visibility
        arrows.toggleClass('rotate');
    });

    $('.menu-2').click(function () {
        let arrows = $(this).find('.arrow');
        arrows.toggleClass('rotate');
    });


    // // editContents-nav&tab
    // // Show the first tab and mark its navigation button as active
    // $(".tab-container:first").show();
    // $(".nav:first").addClass("active");
    // // Function to switch tabs and corresponding navigation buttons
    // function switchTab(index) {
    //     $(".nav").removeClass("active");
    //     $(".nav").eq(index).addClass("active");
    //     $(".tab-container").hide();
    //     $(".tab-container").eq(index).show();
    //     // Reset all icons to inactive
    //     $('.nav .iconActive').hide();
    //     $('.nav .iconInactive').show();
    //     // Show active icon for the clicked button
    //     $(".nav.active .iconInactive").hide();
    //     $(".nav.active .iconActive").show();
    // }
    // // Click event for "Next" button
    // $(".Next").click(function() {
    //     let currentIndex = $(".nav.active").index();
    //     let nextIndex = currentIndex + 1;
    //     if (nextIndex < $(".nav").length) {
    //         switchTab(nextIndex);
    //     }
    // });
    // // Click event for "Previous" button
    // $(".Previous").click(function() {
    //     let currentIndex = $(".nav.active").index();
    //     let prevIndex = currentIndex - 1;
    //     if (prevIndex >= 0) {
    //         switchTab(prevIndex);
    //     }
    // });
    // // Click event for navigation buttons
    // $(".nav").click(function() {
    //     let index = $(this).index();
    //     switchTab(index);
    // });
    // // Form submission event handler
    // $("form").submit(function(event) {
    //     // Add your form submission logic here

    //     // Switch back to the first tab and its corresponding navigation button
    //     switchTab(0);
    // });



    // editContents-nav&tab
    // Show the first tab and mark its navigation button as active
    $(".tab-container:first").show();
    $(".nav:first").addClass("active");

    // Function to switch tabs and corresponding navigation buttons
    function switchTab(index) {
        $(".nav").removeClass("active");
        $(".nav").eq(index).addClass("active");
        $(".tab-container").hide();
        $(".tab-container").eq(index).show();

        // Reset all icons to inactive
        $('.nav .iconActive').hide();
        $('.nav .iconInactive').show();

        // Show active icon for the clicked button
        $(".nav.active .iconInactive").hide();
        $(".nav.active .iconActive").show();

        // Hide "Next" button if on the last tab
        if (index === $(".nav").length - 1) {
            $(".Next").hide();
        } else {
            $(".Next").show();
        }

        // Hide "Previous" button if on the first tab
        if (index === 0) {
            $(".Previous").hide();
        } else {
            $(".Previous").show();
        }
    }

    // Initial hide for "Previous" button
    $(".Previous").hide();

    // Click event for "Next" button
    $(".Next").click(function () {
        let currentIndex = $(".nav.active").index();
        let nextIndex = currentIndex + 1;
        if (nextIndex < $(".nav").length) {
            switchTab(nextIndex);
        }
    });

    // Click event for "Previous" button
    $(".Previous").click(function () {
        let currentIndex = $(".nav.active").index();
        let prevIndex = currentIndex - 1;
        if (prevIndex >= 0) {
            switchTab(prevIndex);
        }
    });

    // Click event for navigation buttons
    $(".nav").click(function () {
        let index = $(this).index();
        switchTab(index);
    });

    // Form submission event handler
    $("form").submit(function (event) {
        // Add your form submission logic here

        // Switch back to the first tab and its corresponding navigation button
        switchTab(0);
    });

    // By default, set the first button as active
    $('.nav:first-child .iconInactive').hide();
    $('.nav:first-child .iconActive').show();


    // Loop through each checkbox-input pair
    $('.toggle-group').each(function () {
        let checkbox = $(this).find('.toggle-Checkbox');
        let textInput = $(this).find('.toggle-input');

        // Initially disable the text input
        textInput.prop('disabled', true);

        // Listen for change event on the checkbox
        checkbox.change(function () {
            if ($(this).is(':checked')) {
                textInput.prop('disabled', false);
            } else {
                textInput.prop('disabled', true);
            }
        });
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

function openModal(supportOfficeId) {
    // مخفی کردن تمام مدال‌ها
    document.querySelectorAll(".removeUser-modal").forEach(modalElement => {
        modalElement.classList.add('hidden');
    });

    // نمایش مدال مربوط به این آیتم
    const modal = document.getElementById(`deleteModal-${supportOfficeId}`);
    if (modal) {
        modal.classList.remove('hidden');
    }

    // تنظیم URL حذف برای این آیتم
    const deleteLink = document.getElementById(`deleteLink-${supportOfficeId}`);
    if (deleteLink) {
        const baseUrl = '/administration/DeleteOribSupportOffice';
        deleteLink.href = `${baseUrl}/${supportOfficeId}`;
    }
}

function cancelAction(supportOfficeId) {
    // مخفی کردن مدال مربوط به این آیتم
    const modal = document.getElementById(`deleteModal-${supportOfficeId}`);
    if (modal) {
        modal.classList.add('hidden');
    }
}


function closeEditModal() {
    let modal = document.querySelectorAll(".editProfile-modal , .editPlan-modal");
    modal.forEach(function (modalElement) {
        modalElement.style.display = 'none'
    })
}
function openEditModal() {
    let modal = document.querySelectorAll(".editProfile-modal , .editPlan-modal");
    modal.forEach(function (modalElement) {
        modalElement.style.display = 'block'
    })
}
function openAddPlanModal() {
    let modal = document.querySelectorAll(" .addPlan-modal");
    modal.forEach(function (modalElement) {
        modalElement.style.display = 'block'
    })
}
function closeAddPlanModal() {
    let modal = document.querySelectorAll(" .addPlan-modal");
    modal.forEach(function (modalElement) {
        modalElement.style.display = 'none'
    })
}


// input-upload-controls
$(document).ready(function () {
    function formatFileSize(bytes) {
        const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
        if (bytes === 0) return '0 Byte';
        const i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)));
        return Math.round(bytes / Math.pow(1024, i), 2) + ' ' + sizes[i];
    }

    $('.file-input').each(function () {
        let fileInput = $(this);
        let container = fileInput.closest('.file-container');
        let fileNameDisplay = container.find('.file-name');
        let fileSizeDisplay = container.find('.file-size');
        let preview = container.find('.preview');

        fileInput.on('change', function () {
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
                    reader.onload = function (e) {
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

//CKEditor
const editors = document.querySelectorAll('#firstEditor, #secondEditor, #thirdEditor');
editors.forEach(editorElement => {
    ClassicEditor
        .create(editorElement, {
            ckfinder: {
                uploadUrl: '/UploadImage'
            },
            toolbar: [
                'undo', 'redo', 'bold', 'italic', 'underline', 'link', '|', 'alignment', 'fontSize', 'fontFamily', 'fontColor', 'highlight', 'insertTable', 'imageUpload'
            ],
            htmlSupport: {
                allow: [
                    {
                        name: /.*/,
                        attributes: true,
                        classes: true,
                        styles: true
                    }
                ]
            },
            image: {
                toolbar: ['imageTextAlternative', '|', 'imageStyle:full', 'imageStyle:side', '|', 'imageResize:50', 'imageResize:75', 'imageResize:original']
            }
        })
        .catch(error => { console.error(error); });
});