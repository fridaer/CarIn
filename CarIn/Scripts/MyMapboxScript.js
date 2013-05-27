/// <reference path="mapbox.js" />
$(document).ready(function () {

    var layer = L.mapbox.tileLayer('tobohr.map-n6vjouf7', {
        detectRetina: true,
        retinaVersion: 'tobohr.map-fkbh0rtn'
    });
    layer.on('ready', function () {
        // the layer has been fully loaded now, and you can
        // call .getTileJSON and investigate its properties
        /*        
                var map = L.mapbox.map('map', 'tobohr.map-n6vjouf7', {   
                detectRetina: true,
                retinaVersion: 'tobohr.map-fkbh0rtn'
                }).setView([57.75, 11.974749], 11);
        
        */
        var map = L.map('map')
        .setView([57.75, 11.974749], 11)
        .addLayer(L.mapbox.tileLayer('tobohr.map-n6vjouf7', {
            detectRetina: true,
            retinaVersion: 'tobohr.map-fkbh0rtn'
        }));

        var updateEvery2sec = setInterval(function () {
            var zoomlevel = map.getZoom();
            if (zoomlevel <= 11) {
                map.setView([57.75, 11.974749], 11);
                map.dragging.disable();
            }
            else {
                map.dragging.enable();
            }
        }, 500);
    

            var url = "/api/v1/CarInRESTful/GetAllInfo/";
            $.ajax({
                type: 'GET',
                url: url,
                async: true,
                success: function (json) {
                    //json[0] = 
                        //TrafficIncidents
                            //Description: "mellan Pinan och Färjeled till Hönö - Vägarbete."
                            //End: "06/28/2013 21:00:00"
                            //ID: 1
                            //IncidentId: "403657022"
                            //Lane: null
                            //LastModified: "05/07/2013 06:01:26"
                            //PointLat: "57.70863"
                            //PointLong: "11.703"
                            //RoadClosed: "False"
                            //Severity: "1"
                            //Start: "04/30/2013 23:00:00"
                            //ToPointLat: "57.70742"
                            //ToPointLong: "11.69865"
                            //Type: "9"
                            //Verified: "True"
                        //WheatherPeriods
                            //ID: 1
                            //PeriodNumber: "3"
                            //SymbolName: "Fair"
                            //TemperatureCelsius: "15"
                            //WindCode: "S"
                            //WindSpeedMps: "6.8"
                    //console.log(json);
                    var id = 0; 
                    $.each(json.TrafficIncidents, function () {
                        //console.log(this);
                        id++;

                        var myIcon = L.divIcon({ className: 'traffic-problem icon-attention'});
                        var popupContent =  '<p id="'+ id +'">'+ this.Description + '</p>';
                        var themarker = L.marker([this.PointLat, this.PointLong], { icon: myIcon }).addTo(map).bindPopup(popupContent);

                        if (this.PointLong !== this.ToPointLong || this.PointLat !== this.ToPointLat) {

                            var myIcon = L.divIcon({ className: 'traffic-problem icon-attention' });
                            var popupContent = '<p id="' + id + '">' + this.Description + '</p>';
                            var themarker = L.marker([this.ToPointLat, this.ToPointLong], { icon: myIcon }).addTo(map).bindPopup(popupContent);

                            var i = 0;
                            var points = new Array();
                            var longlat = new Array();
                            var url2 = "http://www.mapquestapi.com/directions/v1/route?key=Fmjtd%7Cluub2du8n1%2C2g%3Do5-9u2xg4&=renderAdvancedNarrative&ambiguities=ignore&avoidTimedConditions=false&doReverseGeocode=false&outFormat=json&routeType=shortest&timeType=0&enhancedNarrative=false&shapeFormat=raw&generalize=0&locale=sv_SE&unit=m&from=" + this.PointLat + "," + this.PointLong + "&to= " + this.ToPointLat + "," + this.ToPointLong + "&drivingStyle=2&highwayEfficiency=21.0";
                            $.ajax({
                                type: 'GET',
                                url: url2,
                                async: true,
                                dataType: 'jsonp',
                                success: function (jsonPolyline) {
                                    $.each(jsonPolyline.route.shape.shapePoints, function () {
                                        var temp = this.toString();

                                        var onepoint = parseFloat(temp);
                                        points.push(onepoint);
                                        //console.log(onepoint);

                                        if (i % 2 == !0) {
                                            longlat.push(points);
                                            points = [];
                                        }
                                        i++;
                                    })

                                    //console.log(longlat);

                                    var polyline_options = {
                                        color: '#000'
                                    };

                                    var polyline = L.polyline(longlat, polyline_options).addTo(map);

                                },
                                error: function (e) {
                                    //console.log("Det gick åt apan :( ");
                                }
                            });
                        }


                    });

                    $('.vader span').text(json.WheatherPeriods[0].TemperatureCelsius + "\u2103");
                },
                error: function (e) {
                    //console.log("Det gick åt apan :( ");
                }
               });

            $("#olyckaBtn").click(function () {
                $(".traffic-problem").toggle();
                $("path").toggle();
            });
         
            
            

    });
})