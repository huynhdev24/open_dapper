/**
 * add a new GasStation
 * */
function formSubmit() {
    var GasStationName = document.getElementById('GasStationName').value;
    var Longitude = document.getElementById('Longitude').value;
    var Latitude = document.getElementById('Latitude').value;
    var Rating = document.getElementsByName('Rating');
    var District = document.getElementById('District').value;
    var Address = document.getElementById('Address').value;
    var OpeningTime = document.getElementById('OpeningTime').value;

    // handle GasType
    var listGasType = [];
    var elCheckBoxGasType = document.querySelectorAll("input[name=Type]")
    let count = 0;
    for (let i = 0; i < elCheckBoxGasType.length; i++) {
        console.log(elCheckBoxGasType[i])
        if (elCheckBoxGasType[i].checked) {
            listGasType[count] = elCheckBoxGasType[i].value
            count++
        }
    }

    //handle Rating
    var ratingChecked = Rating[0];
    for (let i = 0; i < Rating.length; i++) {
        console.log(Rating[i])
        if (Rating[i].checked) {
            ratingChecked = Rating[i];
        }
    }
    console.log(ratingChecked);
    let jsonListGasType = JSON.stringify(listGasType)
    console.log(jsonListGasType);

    // handle validate and call AJAX add GasStation
    if (validateForm()) {
        Swal.fire({
            title: "確認",
            text: "下記の内容で保存してよろしいですか？",
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'はい',
            cancelButtonText: 'いいえ',
            confirmButtonColor: 'red',
        }).then((res) => {
            console.log(res);
            console.log($.ajax());
            if (res.isConfirmed) {
                $.ajax({
                    url: "/AddGasStation/GasStationAdd",
                    data: {
                        GasStationName: GasStationName,
                        JsonListGasType: jsonListGasType,
                        Longitude: Longitude,
                        Latitude: Latitude,
                        District: District,
                        Address: Address,
                        OpeningTime: OpeningTime,
                        Rating: ratingChecked.value,
                    },
                    dataType: "json",
                    type: "POST",
                    success: function (result) {
                        if (result.content == "Ok") {
                            Swal.fire({
                                icon: 'success',
                                text: '情報を保存しました。',
                            }).then(() => {
                                window.location = "/ListGasStation/GasStationList";
                            });
                        } else {

                        }
                        if (result.statusCode == 400) {
                            $(".msg-error").css("display", "block")
                        } else {

                        }
                    }
                });
            }
        });
    }
}