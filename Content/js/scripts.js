$(document).ready(function() {
    $('select').material_select();
    $(".ticketizer-logo").fadeIn(3000);
    setTimeout(function(){ $(".login-form").slideDown(1000); }, 1500);

    $("#send-email-button").click(function(){
        $(".add-note-div").hide();
        $(".send-email-div").slideToggle();
    });
    $("#add-note-button").click(function(){
        $(".send-email-div").hide();
        $(".add-note-div").slideToggle();
    });
  });
