app.validation = function (multipleErrorsMessage)
{
	var helpIconMessages = new Array();

	var initialiseUnobtrusiveIconValidation = function ()
	{
		// Before doing any validation, we read the field hints so that we can restore the relevant text after the user corrects
		// an invalid input.
		$('a.field-validation-valid').each(
			function (index, element)
			{
				helpIconMessages.push({ element: $(element).get(0), message: $(element).attr('title') });
			}
		);

		$('.body-content form').each(
			function () {
				var validator = $(this).data('validator');

				validator.settings.errorPlacement = $.proxy(
					function (error, inputElement) {
						var container = $(this).find("[data-valmsg-for='" + inputElement[0].name.replace(/([!"#$%&'()*+,./:;<=>?@@\[\\\]^`{|}~])/g, "\\$1") + "']"),
						replaceAttrValue = container.attr("data-valmsg-replace"),
						replace = replaceAttrValue ? $.parseJSON(replaceAttrValue) !== false : null;

						container.removeClass("field-validation-valid").addClass("field-validation-error");
						error.data("unobtrusiveContainer", container);

						// Custom behaviour - we want the error message as a tooltip.
						container.attr('title', error.text());


						/*
						if (replace) {
							container.empty();
							error.removeClass("input-validation-error").appendTo(container);
						}
						else {
							error.hide();
						}*/
					},
					$(this));


				validator.settings.success = $.proxy(
					function (error) {
						var container = error.data("unobtrusiveContainer"),
							replaceAttrValue = container.attr("data-valmsg-replace"),
							replace = replaceAttrValue ? $.parseJSON(replaceAttrValue) : null;

						if (container) {
							container.addClass("field-validation-valid").removeClass("field-validation-error");

							// We now replace any warning message with the original help message, which we stored when the page was first loaded.
							for (var i = 0; i < helpIconMessages.length; i++) {
								if (helpIconMessages[i].element == container.get(0)) {
									container.attr('title', helpIconMessages[i].message);
									break;
								}
							}

							error.removeData("unobtrusiveContainer");

							/*
							if (replace) {
								container.empty();
							}*/
						}
					},
					$(this));

				// Prevent accidental repeats of the form submission, which would show an anti-forgery token error.
				/*
				validator.settings.submitHandler = function (form) {
					$(form).find('input[type="submit"]').attr('disabled', 'disabled');
					form.submit();
				};*/

				$(this).unbind('invalid-form.validate');
				$(this).bind('invalid-form.validate',
					function (event, validator) {
						var container = $(this).find("[data-valmsg-summary=true]");

						if (validator.errorList.length && validator.errorList.length > 0) {
							container.addClass("validation-summary-errors").removeClass("validation-summary-valid");

							if (validator.errorList.length == 1) {
								$(container).text(validator.errorList[0].message);
							}
							else {
								$(container).text(multipleErrorsMessage);
							}
						}
					}
				);
			}
		);
	}

	return { initialiseUnobtrusiveIconValidation: initialiseUnobtrusiveIconValidation };
}