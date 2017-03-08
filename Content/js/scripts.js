$(document).ready(function() {
    $('select').material_select();

  $('#request-feedback-form').submit(function(event) {
    var name    = document.getElementById('admin-name');
    var email   = document.getElementById('admin-email');
    var recipient = document.getElementById('recipient');
    var message = document.getElementById('inputMessage');

    if (!name.value || !email.value || !message.value) {
      alertify.error("Please complete entire form.");
      return false;
    } else {
      $.ajax({
        method: 'POST',
        url: '//formspree.io/gmkhanna@gmail.com',
        data: $('#request-feedback-form').serialize(),
        datatype: 'json'
      });
      event.preventDefault();
      $(this).get(0).reset();
      alertify.success("Message Sent");
    }
  });
});
