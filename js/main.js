jQuery(document).ready(function() {
   
            setTimeout(function() {
                $container.animate({
                    'opacity': 1
                }, 1300);
            }, 200);
            jQuery(window).trigger('resize')
			
        });
	

jQuery(document).ready(function() {

	
 
    jQuery('.scroll-to').on('click', function() {
        event.preventDefault();
        var target = jQuery(this).attr('href');
        jQuery('html, body').animate({
            scrollTop: jQuery(target).offset().top
        }, 1000, "easeInOutExpo");
    });
  

  
});

