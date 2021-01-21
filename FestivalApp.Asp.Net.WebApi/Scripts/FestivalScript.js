$(document).ready(function () {
    var host = "https://" + window.location.host;
    var token = null;
    var headers = {};
    var festivalsEndpoint = "/api/festivals";
    var placesEndpoint = "/api/places";
    


    //pripremanje dogadjaja
    $("body").on("click", "#btnDelete", deleteFestivals);
    




    loadFestivals();

    //funkcija ucitavanja festivala
    function loadFestivals() {
        var requestUrl = host + festivalsEndpoint;
        $.getJSON(requestUrl, setFestivals);
    }

    //popunjavanje tabele festivala
    function setFestivals(data, status) {

        var $container = $("#data");
        $container.empty();

        if (status === "success") {
            // ispis naslova
            var div = $("<div></div>");
            var h1 = $("<h1>Festivals</h1>");
            div.append(h1);
            // ispis tabele
            var table = $("<table class='table table-bordered'></table>");
            var header;
            if (token) {
                header = $("<thead><tr style='background-color:lightsteelblue'><td>Name</td><td>Place</td><td>First year</td><td>Ticket price</td><td>Option</td></tr></thead>");
            } else {
                header = $("<thead><tr style='background-color:lightsteelblue'><td>Name</td><td>Place</td><td>First year</td><td>Ticket price</td></tr></thead>");
            }

            table.append(header);
            tbody = $("<tbody></tbody>");
            for (i = 0; i < data.length; i++) {
                // prikazujemo novi red u tabeli
                var row = "<tr>";
                // prikaz podataka
                var displayData = "<td>" + data[i].Name + "</td><td>" + data[i].Place.Name + "</td><td>" + data[i].FirstYear + "</td><td>" + data[i].TicketPrice + "</td>";

                // prikaz dugmadi za izmenu i brisanje
                var stringId = data[i].Id.toString();
                var displayDelete = "<td><button class='btn btn-danger' id=btnDelete name=" + stringId + ">Delete</button></td>";
               

                // prikaz samo ako je korisnik prijavljen
                if (token) {
                    row += displayData + displayDelete + "</tr>";//prikazujemo display sa tokenom jer je tu puna tabela
                } else {
                    row += displayData + "</tr>";//displej sa osnovnim podacima koji nema token
                }
                // dodati red
                tbody.append(row);
            }
            table.append(tbody);

            div.append(table);

            $container.append(div);
        }
    }

    //kad se pritisne dugme registracija
    $("#regButt").click(function () {
        console.log(data);
        $("#info").empty().append("Enter your e-mail and password!");
        $("#regForm").css("display", "block");
        $("#regButt").css("display", "none");
        $("#logButt").css("display", "none");



    });
    //kad se pritisne dugme prijava
    $("#logButt").click(function () {
        console.log(data);
        $("#info").empty().append("Enter your e-mail and password!");
        $("#logForm").css("display", "block");
        $("#regButt").css("display", "none");
        $("#logButt").css("display", "none");



    });


    // registracija korisnika
    $("#regForm").submit(function (e) {
        e.preventDefault();

        var email = $("#regEmail").val();
        var pass1 = $("#regPass").val();
        var pass2 = $("#regPass1").val();

        // objekat koji se salje
        var sendData = {
            "Email": email,
            "Password": pass1,
            "ConfirmPassword": pass2
        };

        $.ajax({
            type: "POST",
            url: host + "/api/Account/Register",
            data: sendData

        }).done(function (data) {
            $("#regForm").css("display", "none");
            $("#logForm").css("display", "block");
            $("#regEmail").val("");
            $("#regLoz").val("");
            $("#regLoz2").val("");

        }).fail(function (data) {
            alert("Error!");
        });

    });
    //na klik dugmeta odustani u okviru forme registracije
    $("#back").click(function () {
        location.reload();
    });

    // prijava korisnika
    $("#logForm").submit(function (e) {
        e.preventDefault();

        var email = $("#logEmail").val();
        var pass = $("#logPass").val();

        // objekat koji se salje
        var sendData = {
            "grant_type": "password",
            "username": email,
            "password": pass
        };

        $.ajax({
            "type": "POST",
            "url": host + "/Token",
            "data": sendData

        }).done(function (data) {
            console.log(data);
            $("#info").empty().append("User: " + data.userName);
            token = data.access_token;
            $("#searchFestival").css("display", "block");
            $("#createForm").css("display", "block");
            $("#logForm").css("display", "none");
            $("#logOutButt").css("display", "block");
            $("#refresh").css("display", "block");
            $("#priEmail").val("");
            $("#priLoz").val("");
            loadFestivals();

        }).fail(function (data) {
            alert("Error!");
        });
    });
    //na klik dugmeta odustani u okviru forme prijave
    $("#back1").click(function (e) {
        e.preventDefault();
        location.reload();
    });

    // odjava korisnika sa sistema
    $("#logOutButt").click(function () {
        token = null;
        headers = {};

        location.reload();
    });
    $("#refresh").click(function (e) {
        e.preventDefault();
        if (token) {
            headers.Authorization = "Bearer " + token;
        }

        loadFestivals();


    });

    //brisanje 
    function deleteFestivals() {

        var deleteID = this.name;

        // korisnik mora biti ulogovan
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        // saljemo zahtev 
        $.ajax({
            url: host + festivalsEndpoint + "/" + deleteID.toString(),
            type: "DELETE",
            headers: headers
        })
            .done(function (data, status) {
                loadFestivals();
            })
            .fail(function (data, status) {
                alert("Error!");
            });
    }


    //popunjavanje selekta za dodavanje
    $.ajax({
        "type": "GET",
        "url": host + placesEndpoint
    }).done(function (data, status) {
        var select = $("#placeCreate");
        for (var i = 0; i < data.length; i++) {
            var option = "<option value='" + data[i].Id + "'>" + data[i].Name + "</option>";
            select.append(option);
        }
    });

    //dodavanje 
    $("#createForm").submit(function (e) {
        e.preventDefault();
        if (token) {
            headers.Authorization = "Bearer " + token;
        }

        var name = $("#nameCreate").val();
        var price = $("#priceCreate").val();
        var year = $("#yearCreate").val();
        var place = $("#placeCreate").val()


        var sendData = {

            "Name": name,
            "TicketPrice": price,
            "FirstYear": year,
            "PlaceId": place

        };

        $.ajax({
            "type": "POST",
            "url": host + festivalsEndpoint,
            "data": sendData,
            "headers": headers
        }).done(function () {

            $("#nameCreate").val("");
            $("#priceCreate").val("");
            $("#yearCreate").val("");
            $("#placeCreate").val("1");



            loadFestivals();
        }).fail(function (data, status) { alert("Error!"); });
    });

    //pretraga
    $("#searchFestival").submit(function (e) {
        e.preventDefault();


        var from = $("#from").val();
        var to = $("#to").val();

        if (token) {
            headers.Authorization = "Bearer " + token;
        }

        var sendData = {
            "Min": from,
            "Max": to
        };

        $.ajax({
           
            url: host + festivalsEndpoint + "/search",
            type: "POST",
            data: sendData,
            headers: headers
        }).done(function (data, status) {
            $("#from").val("");
            $("#to").val("");

            setFestivals(data, status);
        }).fail(function (data, status) { alert("Error"); });
    });




});