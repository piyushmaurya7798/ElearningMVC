$(function($) {

  'use strict';

  $('.js-menu-toggle').click(function(e) {

  	var $this = $(this);

  	

      if ($('body').hasClass('show-sidebar')) {
          $('#NavBoss').css("display", "block")
            $('body').removeClass('show-sidebar');
  		$this.removeClass('active');
  	} else {
            $('#NavBoss').css("display","none")
  		$('body').addClass('show-sidebar');	
  		$this.addClass('active');
  	}

  	e.preventDefault();

  });

  // click outisde offcanvas
	$(document).mouseup(function(e) {
    var container = $(".sidebar");
    if (!container.is(e.target) && container.has(e.target).length === 0) {
      if ( $('body').hasClass('show-sidebar') ) {
				$('body').removeClass('show-sidebar');
				$('body').find('.js-menu-toggle').removeClass('active');
			}
    }
	}); 

    $("#mcqs").mouseup(function () {
        $("#Mcsqdiv").css("display","block")
    })

});