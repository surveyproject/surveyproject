//<![CDATA[
$(function () { $('#slider-range-min').slider({ orientation: 'horizontal', animate: true, range: 'min', value: 2.567, min: -1.345, max: 8, step: 0.005, slide: function (event, ui) { $('#amount').val('' + ui.value); } }); $('#amount').val('' + $('#slider-range-min').slider('value')); });
//]]>