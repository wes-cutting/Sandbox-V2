/// <reference path="/js/rentler.js" />

/*
Base namespace configuration.
*/
rentler = window.rentler || {};
rentler.__namespace = true;

rentler.featuredCalendar = rentler.featuredCalendar || {};
rentler.featuredCalendar.__namespace = true;

(function ($self, undefined) {

    $self.create = function(buildingId, calendarDates, onSelected) {
            
        var blackoutDates = new Array();

        for (var i = 0; i < calendarDates.BlackoutDates.length; ++i) {
            blackoutDates.push(new Date(calendarDates.BlackoutDates[i]))
        }

        var featuredDates = new Array();

        for (var i = 0; i < calendarDates.FeaturedDates.length; ++i) {
            featuredDates.push(new Date(calendarDates.FeaturedDates[i]))
        }
                        
        var selectableDateCount = 30 - (calendarDates.FeaturedDates.length + calendarDates.ReservedDates.length);

        $('#featuredCalendar').datepick('destroy');

        $('#featuredCalendar').datepick({
            pickerClass: 'featuredPicker',
            fixedWeeks: false,
            changeMonth: false,
            showOtherMonths: false,
            minDate: new Date(),
            multiSelect: selectableDateCount,
            monthsToShow: 3,
            monthsToStep: 3,
            onShow: function (picker, inst) {
                picker.find('td:has(span.datepick-other-month)').addClass('noborder');
                picker.find('.datepick-today').removeClass('datepick-highlight');
            },
            onDate: function (date, current) {
                for (var i = 0; i < featuredDates.length; ++i) {
                    if (featuredDates[i].getMonth() == date.getMonth() &&
                        featuredDates[i].getDate() == date.getDate() &&
                        featuredDates[i].getYear() == date.getYear()) {
                        return { selectable: false, dateClass: 'featured', title: 'Featured', content: date.getDate() };
                    }
                }

                for (var i = 0; i < blackoutDates.length; ++i) {
                    if (blackoutDates[i].getMonth() == date.getMonth() &&
                        blackoutDates[i].getDate() == date.getDate() &&
                        blackoutDates[i].getYear() == date.getYear()) {
                        return { selectable: false, dateClass: 'blackout', title: 'Sold Out', content: date.getDate() };
                    }
                }

                return {};
            },
            onSelect: function (dates) {
                var dateInputs = "";
                for (var i = 0; i < dates.length; i++) {
                    var d = $.datepick.formatDate('mm/dd/yyyy', dates[i]);
                    dateInputs += '<input type="hidden" name="Input.FeaturedDates[' + i + ']" value="' + d + '"/>';
                }

                $('#selectedDates').html(dateInputs);
                
                // call onSelected handler after selection
                onSelected(dates);
            },
            prevText: '',
            todayText: '',
            nextText: ''
        });

        // select reserved dates                    
        for (var i = 0; i < calendarDates.ReservedDates.length; ++i) {
            var rd = new Date(calendarDates.ReservedDates[i]);
            rd.setHours(12);
            var rdTicks = rd.getTime();
            var elemClassName = '.dp' + rdTicks;
            var elemSelector = 'a' + elemClassName;

            $('#featuredCalendar').datepick(
                'selectDate',
                $('#featuredCalendar').find(elemSelector).get(0)
            );
        }
    };    

    function configure() {
        /// <summary>Configures all of the amplify requests.</summary>        
    };

    configure();

} (rentler.featuredCalendar));