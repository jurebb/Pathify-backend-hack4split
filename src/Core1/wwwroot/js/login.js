const $loginOverlay = $("#login-overlay");
  $("#login-button").on("click", () => {
      $loginOverlay.addClass("shown");
  });

$('.message a').click(function(){
   $('form').animate({height: "toggle", opacity: "toggle"}, "slow");
});