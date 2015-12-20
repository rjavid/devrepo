var pluginName = 'popup';
var defaults = {

};

var Popup = function(element, options) {
  this.element = $(element);
  this.markup = this.element.html();
  this.options = $.extend({}, defaults, options);
  this.init();
};

Popup.prototype = {

  init: function() {
    bindEvents(this.element);
    $('.popup-body').append(this.markup);
  }
};

var returnMarkup = function() {
  return "<div data-overlay></div><div class='popup'><div class='popup-content'><div class='popup-header'><span class='btn-close' data-close>X</span></div><div class='popup-body'></div></div>"
};

var bindEvents = function(element) {
  element.empty();
  element.append(returnMarkup());
  $('.popup-content').hide();


  $('[data-open]').on('click', function() {
    $('.popup-content').show();
    $('[data-overlay]').addClass('overlay');
  });

  $('[data-close]').on('click', function() {
    $('.popup-content').hide();
    $('[data-overlay]').removeClass('overlay');
  });

}

$.fn[pluginName] = function(options) {
  return this.each(function() {
    new Popup(this, options);
  });
}