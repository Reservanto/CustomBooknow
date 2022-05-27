var simpleWeekPicker = function ($weekPicker, onWeekChange) {
	/// <summary>
	/// 	Objekt pro práci se základním jQeury datepickerem, umožňující výběr týdnů.
	/// </summary>
	/// <param name="$weekpicker">JQuery element, na který se vytváří weekpicker.</param>
	/// <param name="onWeekChange">Callback, který se volá po změně týdne.</param>

	// Uložení reference na aktuální objekt.
	var self = this;

	// Počet vteřin v týdnu.
	var weekInSecodns = 7 * 24 * 60 * 60 * 1000;

	// Private proměnné pro uložení počtáku a konce týdne.
	var startDate;
	var endDate;

	// Na načtení objektu CustomBooking.Core - vytvoří datepicker a nastaví ho.
	CustomBooking.Core.ready(function () {
		// Nastaví lokalizaci datepickerů.
		$.datepicker.setDefaults(DatePickerLocale["cs"]);

		// Inicializuje datepicker.
		$weekPicker.datepicker({
			showOtherMonths: true,
			selectOtherMonths: true,
			onSelect: function (dateText, inst) {
				// Na výběr dne, vybírám celý týden.
				onSelectCallback($(this));

				// A datepicker se schová.
				$('.week-picker').toggle();
			},
			beforeShowDay: function (date) {
				var cssClass = '';

				// Před zobrazením dne, hlídá celý týden a zobrazí ten.
				if (date >= startDate && date <= endDate)
					cssClass = 'ui-datepicker-current-day';

				return [true, cssClass];
			},
			onChangeMonthYear: function (year, month, inst) {
				selectCurrentWeek();
			}
		});

		// A zvýrazní aktuální týden.
		onSelectCallback($weekPicker);
		$weekPicker.datepicker("refresh");
	});

	var selectCurrentWeek = function () {
		/// <summary>
		/// 	Provede a zvýrazní výběr týden, ve kterém se nachází aktuálně vybraný den.
		/// </summary>
		window.setTimeout(function () {
			$weekPicker.find('.ui-datepicker-current-day a').addClass('ui-state-active')
		}, 1);
	}

	function onSelectCallback($elem) {
		/// <summary>
		/// 	Callback, který je volán po výběru data.
		/// </summary>
		var date = $elem.datepicker('getDate');

		// Vypočítá počátek a konec týdne.
		startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 1);
		endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 7);

		// Vybere aktuální týden.
		selectCurrentWeek();

		// A zavolá callback.
		onWeekChange(date);
	}

	self.nextWeek = function () {
		/// <summary>
		/// 	Funkce, která přepne kalendář na další týden.
		/// </summary>
		switchWeek(true);
	}

	self.prevWeek = function () {
		/// <summary>
		/// 	Funkce, která přepne kalendář na předchozí týden.
		/// </summary>
		switchWeek(false);
	}

	function switchWeek(forward) {
		/// <summary>
		/// 	Funkce, která přepne kalendář na předchozí/další týden.
		/// </summary>
		/// <param name="forward">Příznak, jestli se má kalendář posunout na další týden (true) nebo na přechozí (false).</param>

		$datepicker = $weekPicker;
		$datepicker.datepicker("setDate", new Date(+$datepicker.datepicker("getDate") + (forward ? 1 : -1) * weekInSecodns));
		onSelectCallback($datepicker);
		$datepicker.datepicker("refresh");
	}
}