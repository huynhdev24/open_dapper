function validateForm() {

    var isValidated = true;

    // check required input
    document.querySelectorAll('.required').forEach(element => {
        // case: handle input required
        if (element.value == "") {
            element.style.borderColor = 'red';
            element.parentElement.getElementsByClassName('custom-error')[0].style.color = 'red';
            element.parentElement.getElementsByClassName('custom-error')[0].style.display = 'block';
            element.parentElement.getElementsByClassName('custom-error')[0].innerHTML = element.getAttribute('data-name')+'を入力してください。';
            isValidated = false; // if this control is blank then checkValidate = false
        }
        if (element.value != "") {
            element.style.borderColor = 'black';
            element.parentElement.getElementsByClassName('custom-error')[0].style.color = 'red';
            element.parentElement.getElementsByClassName('custom-error')[0].style.display = 'none';
            element.parentElement.getElementsByClassName('custom-error')[0].innerHTML = '';
        }
    });

    // check email input
    document.querySelectorAll('.isEmail').forEach(element => {
        var regex = /^([a-zA-Z0-9_.-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        // case: handle input is email
        if (element.value != "") {
            if (!regex.test(element.value)) {
                element.style.borderColor = 'red';
                element.parentElement.getElementsByClassName('custom-error')[0].style.display = 'block';
                element.parentElement.getElementsByClassName('custom-error')[0].innerHTML = '正しい形式を入力してください。';
                isValidated = false;
            } else {
                element.style.borderColor = 'black';
                element.parentElement.getElementsByClassName('custom-error')[0].style.display = 'none';
                element.parentElement.getElementsByClassName('custom-error')[0].innerHTML = '';
            }
        }
    });

    // check password input
    document.querySelectorAll('.isPass').forEach(element => {
        // case: handle input is password
        if (element.value != "") {
            if (element.value.length > 100) {
                element.style.borderColor = 'red';
                element.parentElement.getElementsByClassName('custom-error')[0].style.display = 'block';
                element.parentElement.getElementsByClassName('custom-error')[0].innerHTML = 'Validated';
                isValidated = false;
            } else {
                element.style.borderColor = 'black';
                element.parentElement.getElementsByClassName('custom-error')[0].style.display = 'none';
                element.parentElement.getElementsByClassName('custom-error')[0].innerHTML = '';
            }
        }
    });

    // check maxlength
    document.querySelectorAll('.maxlength').forEach(element => {
        // case: handle maxlength input
        if (element.value != "") {
            if (element.value.length > element.getAttribute('data-maxlength')) {
                element.style.borderColor = 'red';
                element.parentElement.getElementsByClassName('custom-error')[0].style.color = 'red';
                element.parentElement.getElementsByClassName('custom-error')[0].style.display = 'block';
                element.parentElement.getElementsByClassName('custom-error')[0].innerHTML = element.getAttribute('data-name') + 'の長さは' + element.getAttribute('data-maxlength') + '文字を超えてはなりません';
                isValidated = false;
            } else {
                element.style.borderColor = 'black';
                element.parentElement.getElementsByClassName('custom-error')[0].style.color = 'red';
                element.parentElement.getElementsByClassName('custom-error')[0].style.display = 'none';
                element.parentElement.getElementsByClassName('custom-error')[0].innerHTML = '';
            }
        }
    });

    return isValidated;
}
