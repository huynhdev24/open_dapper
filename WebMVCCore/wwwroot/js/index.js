////const { post } = require("jquery");

// validate Login
function requiredLogin() {
    var email = document.getElementById("focus").value;
    var password = document.getElementById("password").value; 
    var checkValidate = true
    var checkMail = true

    //document.querySelectorAll('.required').forEach(element => function () {
    //    console.log('element:', element)
    //})
    $('.required').each(function () {
        console.log($(this))
    })
    //document.querySelectorAll('.required').forEach(element => function () {
    //    console.log('element:', element)
    //})



    $(".msg-error").css("display","none")
    if (email != "" || password != "") {
        if (email != "") {
            document.getElementById('email').style.color = 'red';
            document.getElementById('email').innerHTML = '';
            document.getElementById('focus').style.borderColor = 'black';
            document.getElementById('email').style.display = 'none';
        }
        if (password != "") {
            document.getElementById('pass').style.color = 'red';
            document.getElementById('pass').innerHTML = '';
            document.getElementById('password').style.borderColor = 'black';
            document.getElementById('pass').style.display = 'none';

        }
    }
    if (email == "" || password == "") {
        if (email == "") {
            document.getElementById('email').style.color = 'red';
            document.getElementById('email').innerHTML = 'メールを入力してください。';
            document.getElementById('focus').style.borderColor = 'red';
            document.getElementById('email').style.display = 'block';
            checkMail = false
        }
        if (password == "") {
            document.getElementById('pass').style.color = 'red';
            document.getElementById('pass').innerHTML = 'パスワードを入力してください。';
            document.getElementById('password').style.borderColor = 'red';
            document.getElementById('pass').style.display = 'block';
        }
        checkValidate = false;
    }
    if (!isEmail(email) && checkMail) {
        document.getElementById('email').innerHTML = '正しい形式を入力してください。';
        document.getElementById('focus').style.borderColor = 'red';
        document.getElementById('email').style.display = 'block';
        checkValidate = false;
    }
    if (!checkValidate) {
        return false
    }
    else {
        $.ajax({
            url: "/Home/Login",
            method: "POST",
            data: { email: email, pass: password },
            success: function (result) {
                if (result.statusCode == 400) {
                    $(".msg-error").css("display", "block")
                    document.getElementById('focus').style.borderColor = 'red';
                    document.getElementById('password').style.borderColor = 'red';
                }
                else {
                    location.href= result.value
                }

            },
            error: function(result) {
                return false

            },
        })
    }
}

// submitForm 
function submitForm() {
    var listGasType = [];
    var elCheckBoxGasType = document.querySelectorAll("input[name=checkBoxGasType]")
    let count = 0;
    for (let i = 0; i < elCheckBoxGasType.length; i++) {
        console.log(elCheckBoxGasType[i])
        if (elCheckBoxGasType[i].checked) {
            listGasType[count] = elCheckBoxGasType[i].value
            count++
        }
    }
    let jsonListGasType = JSON.stringify(listGasType)
    var form = document.getElementById("gasList");
    const el = document.createElement("input");
    el.name = "jsonListGasType";
    el.value = jsonListGasType;
    el.hidden = true;
    form.appendChild(el);
}

// validate Email
function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

/**
 * available functions
 * */

// validate Form create
//function requestAdd() {
//    var name = document.getElementById("focus").value;
//    var long = document.getElementById("long").value;
//    var lat = document.getElementById("lat").value;
//    var district = document.getElementById("selected").value;
//    var address = document.getElementById("addr").value;
//    var openingtime = document.getElementById("openingtime").value;
//    var rating = document.getElementsByName("Rating");
//    var RChecked = null;
//    for (i = 0; i < rating.length; i++) {
//        if (rating[i].checked)
//            RChecked = rating[i].value;
//    }
//    var fuelType = document.getElementsByClassName("checkbox");
//    var FChecked = null;
//    for (i = 0; i < fuelType.length; i++) {
//        if (fuelType[i].checked)
//            FChecked = fuelType[i].value;
//    }

//    if (name != "" || long != 0 || lat != 0 || district != "" || address != "" || openingtime != "" || RChecked != null || FChecked != null) {
//        if (name != "") {
//            document.getElementById('name').style.color = 'red';
//            document.getElementById('name').innerHTML = '';
//        }
//        if (FChecked != null) {
//            document.getElementById('fuel').style.color = 'red';
//            document.getElementById('fuel').innerHTML = '';
//        }
//        if (long != "") {
//            document.getElementById('longi').style.color = 'red';
//            document.getElementById('longi').innerHTML = '';
//        }
//        if (lat != "") {
//            document.getElementById('lati').style.color = 'red';
//            document.getElementById('lati').innerHTML = '';
//        }
//        if (district != "") {
//            document.getElementById('district').style.color = 'red';
//            document.getElementById('district').innerHTML = '';
//        }
//        if (address != "") {
//            document.getElementById('address').style.color = 'red';
//            document.getElementById('address').innerHTML = '';
//        }
//        if (openingtime != "") {
//            document.getElementById('open').style.color = 'red';
//            document.getElementById('open').innerHTML = '';
//        }
//        if (RChecked != null) {
//            document.getElementById('rate').style.color = 'red';
//            document.getElementById('rate').innerHTML = '';
//        }
//    }
//    if (name == "" || long == 0 || lat == 0 || district == "" || address == "" || openingtime == "" || RChecked == null || FChecked == null) {
//        if (name == "") {
//            document.getElementById('name').style.color = 'red';
//            document.getElementById('name').innerHTML = 'Name を入力してください。';
//        }
//        if (FChecked == null) {
//            document.getElementById('fuel').style.color = 'red';
//            document.getElementById('fuel').innerHTML = 'Fuel を入力してください。';
//        }
//        if (long == 0) {
//            document.getElementById('longi').style.color = 'red';
//            document.getElementById('longi').innerHTML = 'Longitude を入力してください。';
//        }
//        if (lat == 0) {
//            document.getElementById('lati').style.color = 'red';
//            document.getElementById('lati').innerHTML = 'Latitude を入力してください。';
//        }
//        if (district == "") {
//            document.getElementById('district').style.color = 'red';
//            document.getElementById('district').innerHTML = 'District を入力してください。';
//        }
//        if (address == "") {
//            document.getElementById('address').style.color = 'red';
//            document.getElementById('address').innerHTML = 'Address を入力してください。';
//        }
//        if (openingtime == "") {
//            document.getElementById('open').style.color = 'red';
//            document.getElementById('open').innerHTML = 'Opening Time を入力してください。';
//        }
//        if (RChecked == null) {
//            document.getElementById('rate').style.color = 'red';
//            document.getElementById('rate').innerHTML = 'Rating を入力してください。';
//        }
//        return false;
//    }
//}

//function longFormat(longi) {
//    result = longi.replace(/^-?(([-+]?)([\d]{1,3})((\.)(\d+))?)/g, "$1.");
//    document.getElementById("long").value = result;
//}

//function latFormat(lati) {
//    result = lati.replace(/^-?(([-+]?)([\d]{1,2})((\.)(\d+))?)/g, "$1.");
//    document.getElementById("lat").value = result;
//}

//function checkLong(value) {
//    if (value.length > 9) {
//        result = value.slice(0, -1);
//        document.getElementById("long").value = result;
//    }
//}

//function checkLat(value) {
//    if (value.length > 8) {
//        result = value.slice(0, -1);
//        document.getElementById("lat").value = result;
//    }
//}



