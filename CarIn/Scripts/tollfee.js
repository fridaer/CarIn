//06:00–06:29	8 kr
//06:30–06:59	13 kr
//07:00–07:59	18 kr
//08:00–08:29	13 kr
//08:30–14:59	8 kr
//15:00–15:29	13 kr
//15:30–16:59	18 kr
//17:00–17:59	13 kr
//18:00–18:29	8 kr
//18:30–05:59	0 kr

//1 januari
//28-29 mars
//1 april
//30 april
//1 maj
//8-9 maj
//5-6 juni
//21 juni
//1-31 juli
//1 november
//24-26 december
//31 december

"use strict";

var hour = 0;
var min = 0;
var TimeNow = 0;
var month = 0;
var date = 0;
var daynumber = 0;

function GetTimeNow() {
    TimeNow = new Date();
    hour = TimeNow.getHours();
    min = TimeNow.getMinutes();
    month = TimeNow.getMonth() + 1;
    date = TimeNow.getDate();
    daynumber = TimeNow.getDay();

}

function CheckTollFee(month, date, daynumber, hour, min) {
    switch (daynumber) {
        case 6:
        case 0:
            return 0;
            break;
    }
    switch (month) {
        case 1:
            switch (date) {
                case 1:
                    return 0;
                    break;
            }
        case 3:
            switch (date) {
                case 29:
                case 30:
                    return 0;
                    break;
            }
        case 4:
            switch (date) {
                case 1:
                case 30:
                    return 0;
                    break;
            }
        case 5:
            switch (date) {
                case 1:
                case 8:
                case 9:
                    return 0;
                    break;
            }
        case 6:
            switch (date) {
                case 5:
                case 6:
                case 21:
                    return 0;
                    break;
            }
        case 7:
            switch (month) {
                case 7:
                    return 0;
                    break;
            }
        case 11:
            switch (date) {
                case 1:
                    return 0;
                    break;
            }
        case 12:
            switch (date) {
                case 24:
                case 25:
                case 26:
                case 31:
                    return 0;
                    break;
            }

            break;
    }

    if (hour === 6 && min <= 29 && min >= 0) {
        return 8;
    }
    else if (hour === 6 && min <= 59 && min >= 30) {
        return 13;
    }
    else if (hour === 7 && min <= 59 && min >= 0) {
        return 18;
    }
    else if (hour === 8 && min <= 29 && min >= 0) {
        return 13;
    }
    else if (hour === 8 && min <= 29 && min >= 59) {
        return 8;
    }
    else if (hour >= 9 && hour <= 14) {
        return 8;
    }
    else if (hour === 15 && min <= 29 && min >= 0) {
        return 13;
    }
    else if (hour === 15 && min <= 59 && min >= 30) {
        return 18;
    }
    else if (hour === 16) {
        return 18;
    }
    else if (hour === 17) {
        return 13;
    }
    else if (hour === 18 && min <= 29 && min >= 0) {
        return 8;
    }
    else {
        return 0;
    }
}



function CheckTollHelpFunction() {
    GetTimeNow();
    var Fee = CheckTollFee(month, date, daynumber, hour, min);

    if (hour === 18 && min > 14) {
        alertInTooltipbox("Det kostar just nu " + Fee + " kr i trängselskatt , eller väntar du till 18:30 så är det gratis ;)");
    }
    else {
        alertInTooltipbox("Det kostar just nu " + Fee + " kr i trängselskatt");
    }
}
$(document).ready(function () {
    CheckTollHelpFunction();
});