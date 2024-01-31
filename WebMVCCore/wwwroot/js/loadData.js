// load Data GasStationList AJAX
function loadData(searchGasName, searchDistrict, jsonListGasType, page) {
    document.getElementById('tblData').innerHTML = '';
    $.ajax({
        url: '/ListGasStation/LoadGasStationList',
        type: 'GET',
        data: {
            searchGasName: searchGasName,
            searchDistrict: searchDistrict,
            jsonListGasType: jsonListGasType,
            page: page,
        },
        dataType: 'json',
        success: function (response) {
            if (response.status) {
                var data = response.data;
                console.log(data);
                var html = '';
                $.each(data, function (item) {
                    html +=
                        `<tr>
                            <td class="custom-fonttable custom-fonttable-align">`
                            +
                            (
                                data.gasStationName != undefined && data.gasStationName != "" && data.gasStationName.length > 30
                                ?
                                `<a id="${data.gasStationName}" name="gasId" data-name="${data.gasStationName}" title="${data.gasStationName}" style="color: black; vertical-align: middle" href="~/GasStation/GasStationFeedBackView/${data.gasStationId}">${data.gasStationName.substring(0, 30)}...)</a>`
                                :
                                `<a id="${data.gasStationId}" name="gasId" data-name="${data.gasStationName}" style="color: black; vertical-align: middle" href="~/GasStation/GasStationFeedBackView/${data.gasStationId}">${data.gasStationName}</a>`
                            )
                            +
                            `</td>
                            <td class="custom-fonttable custom-fonttable-align">
                                ${data.gasTypes}
                            </td>
                            <td class="custom-fonttable">
                                ${data.districtName}
                            </td>
                            <td class="custom-fonttable">
                                ${data.longitude},${data.latitude}
                            </td>
                            <td class="custom-fonttable">
                                <img class="icon-size" src="~/img/${data.rating}.png" alt="${data.rating}" />
                            </td>
                            <td class="custom-fonttable">
                                <a href="~/EditGasStation/GasStationEdit/${data.gasStationId}"> <button class="btn btn-success">Edit</button></a>
                            </td>
                            <td class="custom-fonttable">
                                <button onclick="DeleteItem(this)" data-name="${data.gasStationName}" data-id="${data.gasStationId}" class="btn btn-danger">Del</button>
                            </td>
                        </tr>`
                });
                console.log(html);
                document.getElementById('tblData').innerHTML = html;
            }
        }
    })
}
//loadData();
