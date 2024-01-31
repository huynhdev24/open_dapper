/**
 * Delete a GasStation
 * @param {any} obj
 */
function DeleteItem(obj) {
    var gasStationId = obj.getAttribute("data-id");
    var gasID = obj.getAttribute("data-name");

    Swal.fire({
        title: "確認",
        text: "ガソリンスタンド" + gasID + "は削除されますか？",
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'はい',
        cancelButtonText: 'いいえ',
        confirmButtonColor: 'red',
    }).then((res) => {
        if (res.isConfirmed) {
            $.ajax({
                url: "/DeleteGasStation/GasStationDelete",
                data: { gasStationId: gasStationId },
                dataType: "json",
                type: "POST",
                success: function (result) {
                    if (result.content == "Ok") {
                        Swal.fire({
                            icon: 'success',
                            text: '削除しました。',
                        }).then(() => {
                            location.reload();
                        });
                    }
                }
            });
        }
    });
}