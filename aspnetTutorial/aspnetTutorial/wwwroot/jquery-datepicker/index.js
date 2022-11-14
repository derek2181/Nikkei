// Example

import $ from 'jquery';
import datepickerFactory from './jquery-datepicker';
import datepickerJAFactory from './i18n/jquery.ui.datepicker-ja';

$(function() {
  const selector = '#datepicker';

  datepickerFactory($);
  datepickerJAFactory($);

  $('#datepicker').datepicker();
  $.datepicker.regional['ja'];
});