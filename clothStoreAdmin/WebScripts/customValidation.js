//Image Validation and Preview
function onImageChange(eve) {
    var fuData = document.getElementById($(eve).attr("id"));
    var FileUploadPath = fuData.value;
    var MAX_SIZE = 1024 * 1024 * 4;
    if (FileUploadPath == '') {
        alert("Please upload an image");
        ImageReset(eve);
    }
    else {
        var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();
        //The file uploaded is an image
        if (Extension == "png" || Extension == "bmp" || Extension == "jpeg" || Extension == "jpg") {
            if (fuData.files && fuData.files[0]) {
                var size = fuData.files[0].size;
                if (size > MAX_SIZE) {
                    alert("Image Size Less Then 4 Mb!")
                    ImageReset(eve);
                    return;
                }
                else {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var image = new Image();
                        //Set the Base64 string return from FileReader as source.
                        image.src = e.target.result;
                        image.onload = function () {
                            //Determine the Height and Width.
                            var height = this.height;
                            var width = this.width;
                            if (height < 1000 || width < 1100) {
                                alert("Height Must Be Greater Than 1000px and Width Must Be Greater Than 1100px!");
                                ImageReset(eve);
                            }
                            else {
                                $("#" + $(eve).attr("id") + "Remove").show();
                                $("#" + $(eve).attr("id") + "Preview").show();
                                $("#" + $(eve).attr("id") + "Show").attr("src", e.target.result);
                            }
                        }
                    }
                    reader.readAsDataURL(fuData.files[0]);
                }
            }
        }
            //The file upload is NOT an image
        else {
            alert("Photo Only Allows File Types of PNG, JPG, JPEG and BMP. ");
            ImageReset(eve);
        }
    }
}

//Image Preview Reset
function ImageReset(eve) {
    $(eve).val("");
    $("#" + $(eve).attr("id") + "Remove").hide();
    $("#" + $(eve).attr("id") + "Preview").hide();
    $("#" + $(eve).attr("id") + "Show").attr("src", "");
}

// Image Remove
function ImageRemove(eve, action) {
    $("#" + $(eve).attr("id")).hide();
    var id = $(eve).attr("id").replace("Remove", "");
    $("#" + id).val("");
    $("#" + id + "Preview").hide();
    $("#" + id + "Show").attr("src", "");
    //Swith is used for to delete existing image on edit action
    switch (action) {
        case "edit": var imageName = $("#" + id + "Name").val();
            if (imageName != "") { $("#" + id + "Name").val(imageName + "," + "remove") }
            break;
        default: break;
    }
}

//Color Validation
function getColor(colorCode) {
    var n_match = ntc.name(colorCode);
    n_rgb = n_match[0]; // RGB value of closest match
    n_name = n_match[1]; // Text string: Color name
    n_exactmatch = n_match[2]; // True if exact color match
    return n_name;
}