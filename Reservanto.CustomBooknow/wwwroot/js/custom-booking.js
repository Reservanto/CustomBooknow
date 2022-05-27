// TODO: Okomentovat učesat

var CustomBooking = CustomBooking || {};

CustomBooking.Config = {
	/// <summary>
	/// 	Objekt s nastavením (konfigurací) stránkování.
	/// </summary>
	prevPageText: "Back",
	nextPageText: "Next step",

	nextPageUse: true,
	nextPage: "/empty",

	prevPageUse: false,
	prevPage: "/empty",

	errorText: "Došlo k chybě, zkuste to znovu později.",
};

CustomBooking.Core = (function (document) {
	/// <summary>
	/// 	Objekt umožňující vytvořit "stránkování" v rezervačním formuláři.
	/// 	Výsledkem je, že při přenačítání stránke se obnovuje pouze vnitřní obsah, nikoliv celé okno prohlížeče.
	/// </summary>

	/* Privátní proměnné */
	var nextPage = null,
		prevPage = null,
		content = null,
		loader = null,
		form = null,
		request = false,
		initialized = false,
		validationMessage = null,
		validationTimeout = null,
		afterRefreshCallback = [],
		pageHierarchy = [];

	function init() {
		/// <summary>
		/// 	Provede inicializaci.
		/// </summary>

		// Získá elementy pro ovládání.
		nextPage = $("#nextPage");
		prevPage = $("#prevPage");
		content = $("#content #inner");
		loader = $("#loader");

		// Nastaví že je inicializován.
		initialized = true;

		// A vytvoří event listenery na přepínání stránek.
		prevPage.click(function () { loadPage(false); });
		nextPage.click(function () {
			if (CustomBooking.Config.nextPageClose) {
				closeModal()
			}
			else {
				loadPage(true);
			}
		});

		// Nakonec provede obnovu hodnot.
		refresh();
	}

	function loadPage(next) {
		/// <summary>
		/// 	Provede načtení další/předchozí stránky.
		/// </summary>
		/// <param name="next">Příznak, jestli se má formulář posunout na další stránku (true), nebo na předchozí (false).</param>

		// Pouze pokud žádný dataz ještě neprobíhá.
		if (!request) {
			request = true;

			var url;
			if (next) {
				currentPageValue = getValueFromForm("CurrentPage");

				// Přidá do historie stránek aktuální.
				pageHierarchy.push(currentPageValue);

				// Posune stránkování.
				setValueToForm("PrevPage", currentPageValue);
				setValueToForm("CurrentPage", getValueFromForm("NextPage"));

				// Zjistí url, kam má udělat dotaz.
				url = CustomBooking.Config.nextPage;
			} else {
				// Posune stránkování.
				setValueToForm("NextPage", null);
				setValueToForm("CurrentPage", pageHierarchy.pop());
				setValueToForm("PrevPage", pageHierarchy[pageHierarchy.length - 1]);

				// Zjistí url, kam má udělat dotaz.
				url = CustomBooking.Config.prevPage;
			}

			// Provede dotaz.
			loadUrl(url);
		}
	}

	function loadUrl(url) {
		/// <summary>
		/// 	Provede načtení stránky, která se nachází na dané adrese.
		/// </summary>
		/// <param name="next">Url adresa stránky.</param>

		// nejdřív schová aktuální obsah.
		content.fadeOut(200);

		// Zobrazí načítání.
		loader.fadeIn(200);

		setTimeout(function () {
			// Provede dotaz na server.
			form.ajaxSubmit({
				url: url,
				type: "post",
				success: function (data, state, xhr) {
					// Reset zákazu stránky
					CustomBooking.Config.nextPageDisabled = false;

					/* data Preprocessing */
					data = data.replace(/\$\(document\)\.ready\(/g, "CustomBooking.Core.ready(");

					var getlinks = new RegExp("<link[^>]*>", "g");

					// Nastaví styly, které jsou nové.
					$(data.match(getlinks)).each(function (index, element) {
						var $element = $(element);

						if (document.createStyleSheet) {
							document.createStyleSheet($element.attr("href"));
						}
						else {
							$element.appendTo("head");
						}
					});

					data = data.replace(/<link[^>]*>/g, "");

					// Nahradí obsah stránky.
					content.html(data);

					ajaxComplete(false);
				},
				error: function (data) {
					// Posune stránkování zpět.
					setValueToForm("NextPage", getValueFromForm("CurrentPage"));
					setValueToForm("CurrentPage", pageHierarchy.pop());
					setValueToForm("PrevPage", pageHierarchy[pageHierarchy.length - 1]);

					// Status 400 = validace.
					if (data.status === 400) {
						ajaxComplete(true, data.responseText);
					}
					else {
						ajaxComplete(true);
					}
				}
			});
		}, 200);
	}

	function ajaxComplete(error, errorMessage) {
		/// <summary>
		/// 	Akce, provedené po dokončení ajax volání na server.
		/// </summary>

		// Načte nový obsah.
		content.fadeIn(200);

		// Skryje načítání.
		loader.fadeOut(200);

		// Obnoví.
		refresh();
		request = false;

		// A zpracuje potencionální errorové hlášky.
		if (error) {
			if (errorMessage) {
				showValidationMessage(errorMessage);
			} else {
				alert(CustomBooking.Config.errorText);
			}
		} else {
			showValidationMessage();
		}
	}

	function refresh() {
		/// <summary>
		/// 	Akce, obnovující nastavení celého formuláře.
		/// </summary>
		if (!initialized)
			return;

		if (!refreshForm())
			return;

		// Zobrazení předchozí stránky.
		if (CustomBooking.Config.prevPageUse) {
			prevPage.html(CustomBooking.Config.prevPageText);
			prevPage.show();
		} else {
			prevPage.hide();
		}

		// Zobrazení následující stránky.
		if (CustomBooking.Config.nextPageUse) {
			nextPage.html(CustomBooking.Config.nextPageText);
			nextPage.show();
		} else {
			nextPage.hide();
		}
	}

	function refreshForm() {
		/// <summary>
		/// 	Akce, zajišťující načtený formulář, kam se průběžně ukládají hodnoty.
		/// </summary>
		form = $("#BookingModel");

		// Formulář není - vrací false.
		if (form.length == 0) {
			return false;
		}

		// Jinak provede callbacky po načtení.
		if (afterRefreshCallback.length > 0) {
			$(afterRefreshCallback).each(function (index, callback) {
				callback();
			});
			afterRefreshCallback = [];
		}

		// A vrátí true.
		return true;
	}

	function executeAfterRefresh(func) {
		/// <summary>
		/// 	Funkce, která přidá callback po načtení formuláře.
		/// </summary>
		afterRefreshCallback.push(func);
	}

	function showValidationMessage(newMessage) {
		/// <summary>
		/// 	Akce pro zobrazení potencionálních validačních hlášek.
		/// </summary>
		validationMessage = newMessage || validationMessage;

		// Existuje validační chyba.
		if (validationMessage != null) {
			// Zobrazení hlášky.
			if ($("#errorMSG").length == 0) {
				$("#BookingModel").after("<div style='display:none;' id='errorMSG'><p></p></div>");
			}
			$("#errorMSG p").html(validationMessage.replace(/(\n)|(\\n)/g, "<br/>"));
			$("#errorMSG").fadeIn(100);

			// Skrytí hlášky
			if (validationTimeout != null) {
				clearTimeout(validationTimeout);
			}
			validationTimeout = setTimeout(hideValidationMessage, 6000); //za 6 sec

			validationMessage = null;
		}
	}

	function hideValidationMessage() {
		/// <summary>
		/// 	Akce pro schování potencionálních validačních hlášek.
		/// </summary>

		// Vyčití timeout pro zobrazení validace.
		if (validationTimeout != null) {
			clearTimeout(validationTimeout);
			validationTimeout = null;
		}

		// Pokud je zobrazena - shová se.
		if ($("#errorMSG").is(":visible")) {
			$("#errorMSG").fadeOut(400);
		}
	}

	//#region Formulář - jednoduché nastavení hodnot

	function setValueToForm(selector, value) {
		/// <summary>
		/// 	Nastaví hodnotu do formuláře.
		/// </summary>
		getFormNode(selector).val(value);
	}

	function getValueFromForm(selector) {
		/// <summary>
		/// 	Vrátí hodnotu nastavenou ve formuláři.
		/// </summary>
		return getFormNode(selector).val();
	}

	function getFormNode(name) {
		/// <summary>
		/// 	Vrátí hodnotu nastavenou ve formuláři.
		/// </summary>
		var node = null;
		if (form == null || form.length == 0) {
			refreshForm();
		}
		form.find("input").each(function () {
			var $this = $(this);
			var thisName = $this.attr("name").toString();
			if (thisName == name) {
				node = $this;
				return false;//break
			}
			return true;//continue
		});
		return node;
	}

	function getFormSerialized() {
		/// <summary>
		/// 	Vrátí serializovaný formulář.
		/// </summary>
		if (form == null || form.length == 0) {
			refreshForm();
		}
		return form.serialize();
	}

	//#endregion

	/* public */
	return {
		init: init,
		refresh: refresh,
		setFormValue: setValueToForm,
		getFormValue: getValueFromForm,
		getSerializedForm: getFormSerialized,
		showValidationMessage: showValidationMessage,
		goToUrl: loadUrl,
		ready: executeAfterRefresh,
	};
}(document));

$(document).ready(function () {
	CustomBooking.Core.init();
});