$(document).ready(function () {

 $('.chart').easyPieChart({
        barColor: '#25aae1',
        trackColor: '#FFFFFF',
        scaleColor: false,
        easing: 'easeOut',
        scaleLength: 6,
        lineCap: 'round',
        lineWidth: 6, //12
        animate: {
            duration: 5000,
            enabled: true
        },
        onStep: function (from, to, percent) {
            $(this.el).find('.percent').text(Math.round(percent));
        }
    });

});