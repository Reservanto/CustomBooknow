using System;
using System.Collections.Generic;
using Reservanto.ApiClient.Core;
using Reservanto.ApiClient.Public;
using Reservanto.ApiClient.Requests;
using Reservanto.ApiClient.Responses;
using Reservanto.ApiClient.Public.Interface;

namespace Reservanto.ApiClient
{
	/// <summary>
	/// Api klient pro práci s API s přihlášeným obchodníkem.
	/// </summary>
	public class ReservantoApi : ReservantoApiBase
	{
		/// <summary>
		/// Vytvoří novou instanci napojení na Reservanto API.
		/// </summary>
		/// <param name="apiUrl">Url adresa, na které se nachází API (např. https://merchant.reservanto.cz/api_v1). </param>
		/// <param name="clientId">Client Id přiděleno Reservantem.</param>
		/// <param name="ltt">Long Time Token, pro přístup k API.</param>
		public ReservantoApi(string apiUrl, string clientId, string ltt)
			: base(apiUrl, clientId, ltt, null)
		{
		}

		/// <summary>
		/// Vrací, že aktuální instance napojení na API není v Admin módu.
		/// </summary>
		protected override bool AdminMode => false;

		#region Booking

		/// <summary>
		/// Vytvoří platbu na danou rezervaci od právě přihlášeného obchodníka.
		/// Nutná oprávnění: Booking_w
		/// </summary>
		/// <returns>Vrací odpověď API s id nově vytvořené platby i s případnými validačními errory.</returns>
		public PaymentCreatedModel Booking_CreatePaymentForBooking(int customerId, int appointmentId, int paymentMethodId)
		{
			var response = Safe(() => MakeRequest<Response<PaymentCreatedModel>>(
				"Booking/CreatePaymentForBooking",
				new CreatePaymentRequest(appointmentId, customerId, paymentMethodId))
			);

			return response?.Result;
		}

		/// <summary>
		/// Stornuje danou rezervaci.
		/// </summary>
		/// <returns>Vrací odpověď API i s případnými validačními errory.</returns>
		public BookingStatusResponse Booking_Cancel(int appointmentId, int customerId)
		{
			var response = Safe(() => MakeRequest<BookingStatusResponse>(
				"Booking/Cancel",
				new EventRequest(appointmentId, customerId))
			);

			return response;
		}

		/// <summary>
		/// Potvrdí rezervaci, která čeká na potvrzení. Pokud je rezervace již potvrzená, tak API vrátí chybu.
		/// Nutná oprávnění: Booking_w.
		/// </summary>
		/// <returns>Vrací odpověď API i s případnými validačními errory.</returns>
		public BookingStatusResponse Booking_Confirm(int appointmentId, int customerId)
		{
			var response = Safe(() => MakeRequest<BookingStatusResponse>(
				"Booking/Confirm",
				new EventRequest(appointmentId, customerId))
			);

			return response;
		}

		/// <summary>
		/// Zamítne rezervaci, která čeká na potvrzení. Pokud je rezervace již potvrzená, tak API vrátí chybu.
		/// Nutná oprávnění: Booking_w.
		/// </summary>
		/// <returns>Vrací odpověď API i s případnými validačními errory.</returns>
		public BookingStatusResponse Booking_Reject(int appointmentId, int customerId)
		{
			var response = Safe(() => MakeRequest<BookingStatusResponse>(
				"Booking/Reject",
				new EventRequest(appointmentId, customerId))
			);

			return response;
		}

		#endregion

		#region BookingResource

		/// <summary>
		/// Načte seznam všech zdrojů od právě přihlášeného obchodníka.
		/// Nutná oprávnění: BookingResource_r
		/// </summary>
		/// <returns>Vrací list se seznamem zdrojů (null pokud vznikla chyba v komunikaci).</returns>
		public List<BookingResourceModel> BookingResource_GetList()
		{
			var response = Safe(() => MakeRequest<ListResponse<BookingResourceModel>>("BookingResource/GetList"));

			return response?.Items;
		}

		/// <summary>
		/// Načte konkrétní zdroj pro právě přihlášeného obchodníka.
		/// Nutná oprávnění: BookingResource_r
		/// </summary>
		/// <param name="bookingResourceId">Id požadovaného zdroje.</param>
		/// <returns>Zdroj s požadovaným id (null pokud vznikla chyba v komunikaci).</returns>
		public BookingResourceModel BookingResource_Get(int bookingResourceId)
		{
			var response = Safe(() => MakeRequest<Response<BookingResourceModel>>(
				"BookingResource/Get",
				new BookingResourceRequest(bookingResourceId))
			);

			return response?.Result;
		}

		/// <summary>
		/// Vrací dostupnost (zda je možné je rezervovat) vybraných zdrojů ve vybraném časovém úseku.
		/// Ve vrácené dictionary nemusí být všechny klíče (id) předané v parametru.
		/// Nutná oprávnění: FreeSpace_r
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="resourceIds"></param>
		/// <returns>
		/// Vrací dictionary kde pod id zdroje je odpověď, zda je v daný čas dostupný.
		/// (Není zaručeno, že všechny předaná id budou mít v odpovědi záznam [NePublic nebo cizí tu nejsou])
		/// </returns>
		public Dictionary<int, bool> BookingResource_GetAvailability(DateTime from, DateTime to, IEnumerable<int> resourceIds)
		{
			var response = Safe(() => MakeRequest<IntKeyDictionaryResponse<bool>>(
				"BookingResource/GetAvailability",
				new BookingResourceAvailabilityRequest(from, to, resourceIds))
			);

			return response?.TypedValues;
		}

		/// <summary>
		/// Získá zdroj na základě jeho externího identifikátoru.
		/// </summary>
		public BookingResourceModel GetByExternalIdentifier(string externalId)
		{
			var response = Safe(() => MakeRequest<Response<BookingResourceModel>>(
				"ExternalIdentifier/Get",
				ExternalIdentifierRequest.GetByExternalIdentifier(externalId, ExternalIdentifierObjectTypes.BOOKING_RESOURCE_OBJECT_TYPE))
			);

			return response?.Result;
		}

		#endregion

		#region BookingService

		/// <summary>
		/// Načte seznam všech služeb od právě přihlášeného obchodníka.
		/// Nutná oprávnění: BookingService_r
		/// </summary>
		/// <returns>Vrací list se seznamem služeb (null pokud vznikla chyba v komunikaci).</returns>
		public List<BookingServiceModel> BookingService_GetList()
		{
			var response = Safe(() => MakeRequest<BookingServicesResponse>("BookingService/GetList"));

			return response?.BookingServices;
		}

		/// <summary>
		/// Načte konkrétní službu od právě přihlášeného obchodníka.
		/// Nutná oprávnění: BookingService_r
		/// </summary>
		/// <returns>Vrací konkrétní službu (null pokud vznikla chyba v komunikaci).</returns>
		public BookingServiceModel BookingService_Get(int bookingServiceId)
		{
			var response = Safe(() => MakeRequest<Response<BookingServiceModel>>(
				"BookingService/Get",
				new BookingServiceRequest(bookingServiceId))
			);

			return response?.Result;
		}

		/// <summary>
		/// Načte seznam všech služeb, které poskytuje daný zdroj od právě přihlášeného obchodníka.
		/// Nutná oprávnění: BookingService_r
		/// </summary>
		/// <returns>Vrací list se seznamem služeb (null pokud vznikla chyba v komunikaci).</returns>
		public List<BookingServiceModel> BookingService_GetForResource(int bookingResourceId)
		{
			var response = Safe(() => MakeRequest<ListResponse<BookingServiceModel>>(
				"BookingService/GetForBookingResource",
				new BookingResourceRequest(bookingResourceId))
			);

			return response?.Items;
		}

		#endregion

		#region Classes

		/// <summary>
		/// Vytvoří rezervaci na vybranou událost pro daného zákazníka od právě přihlášeného obchodníka.
		/// Nutná oprávnění: Booking_w
		/// </summary>
		/// <returns>Vrací odpověď API i s případnými validačními errory.</returns>
		public BookingStatusResponse Classes_CreateBooking(IBooking booking)
		{
			var response = Safe(() => MakeRequest<BookingStatusResponse>(
				"Classes/CreateBooking",
				new ClassesBookingCreateRequest(booking))
			);

			return response;
		}

		/// <summary>
		/// Načte seznam všech možných událostí, na které je možno se zarezervovat.
		/// Nutná oprávnění: FreeSpace_r
		/// </summary>
		/// <returns>Vrací list se seznamem všech rezervovatelných událostí. (null pokud vznikla chyba v komunikaci).</returns>
		public List<ClassAppointmentModel> Classes_GetAvailableAppointments(int segmentId, int locationId, DateTime from, DateTime to)
		{
			var response = Safe(() => MakeRequest<ListResponse<ClassAppointmentModel>>(
				"Classes/GetAvailableAppointments",
				new ClassesAvailableAppointmentsRequest(segmentId, locationId, from, to))
			);

			return response?.Items;
		}

		/// <summary>
		/// Načte konkrétní událost od právě přihlášeného obchodníka.
		/// Nutná oprávnění: FreeSpace_r
		/// </summary>
		/// <returns>Vrací konkrétní událost (null pokud vznikla chyba v komunikaci).</returns>
		public ClassAppointmentModel Classes_Get(int appointmentId)
		{
			var response = Safe(() => MakeRequest<Response<ClassAppointmentModel>>(
				"Classes/Get",
				new AppointmentRequest(appointmentId))
			);

			return response?.Result;
		}

		#endregion

		#region Customer 

		/// <summary>
		/// Vytvoří zákazníka podle předaných informací pro právě přihlášeného obchodníka.
		/// Nutná oprávnění: Customer_w
		/// </summary>
		/// <returns>Vrací odpověď API i s případnými validačními errory.</returns>
		public CreateCustomerResponse Customer_Create(ICustomer customer)
		{
			var response = Safe(() => MakeRequest<CreateCustomerResponse>(
				"Customer/Create",
				new CustomerCreateRequest(customer))
			);

			return response;
		}

		/// <summary>
		/// Vyhledá ve všech zákaznících podle předaného modelu.
		/// Nutná oprávnění: Customer_r
		/// </summary>
		/// <returns>Vrací list se zákazníky, kteří se podobají tomu, co bylo vyhedáno (null pokud vznikla chyba v komunikaci).</returns>
		public List<CustomerModel> Customer_Search(ICustomer customer)
		{
			var response = Safe(() => MakeRequest<CustomerListResponse>(
				"Customer/Search",
				new CustomerSearchRequest(customer))
			);

			return response?.Customers;
		}

		/// <summary>
		/// Vyhledá zákazníka podle jeho identifikátoru.
		/// Notná oprávnění: Customer_r
		/// </summary>
		/// <returns>Nalezeného zákazníka s předaným identifikátorem.</returns>
		public CustomerModel Customer_Get(int id)
		{
			var response = Safe(() => MakeRequest<CustomerResponse>(
				"Customer/Get",
				new CustomerRequest(id))
			);

			return response.Customer;
		}

		#endregion

		#region Event


		/// <summary>
		/// Načte konkrétní rezervaci pro právě přihlášeného obchodníka.
		/// Nutná oprávnění: Event_r
		/// </summary>
		/// <param name="appointmentId">Id požadované události, spolu s <paramref name="customerId"/> identifikuje rezervaci.</param>
		/// <param name="customerId">Id požadovaného zákazníka, spolu s <paramref name="appointmentId"/> identifikuje rezervaci.</param>
		/// <returns>Zdroj s požadovaným id (null pokud vznikla chyba v komunikaci).</returns>
		public EventModel Event_Get(int appointmentId, int customerId)
		{
			var response = Safe(() => MakeRequest<Response<EventModel>>(
				"Event/Get",
				new EventRequest(appointmentId, customerId))
			);

			return response?.Result;
		}

		#endregion

		#region ExternalIdentifiers

		/// <summary>
		/// Vytvoří externí identifikátor a propojí ho s Reservantím objektem.
		/// </summary>
		public Response CreateExternalIdentifier(string externalId, int reservantoId, string objectType, string externalData = null)
		{
			var response = Safe(() => MakeRequest<Response<LocationModel>>(
				"ExternalIdentifier/Add",
				ExternalIdentifierRequest.CreateExternalIdentifier(externalId, reservantoId, objectType, externalData))
			);

			return response;
		}

		/// <summary>
		/// Upraví data externího identifikátoru (propojení vůči Reservantímu objektu se nemění).
		/// </summary>
		public Response EditExternalIdentifierData(string externalId, string objectType, string externalData = null)
		{
			var response = Safe(() => MakeRequest<Response<LocationModel>>(
				"ExternalIdentifier/Edit",
				ExternalIdentifierRequest.EditExternalIdentifier(externalId, objectType, externalData))
			);

			return response;
		}

		/// <summary>
		/// Smaže externí identifikátor a odpojí ho od Reservantího objektu.
		/// </summary>
		public Response DeleteExternalIdentifierData(string externalId, string objectType)
		{
			var response = Safe(() => MakeRequest<Response<LocationModel>>(
				"ExternalIdentifier/Delete",
				ExternalIdentifierRequest.DeleteExternalIdentifier(externalId, objectType))
			);

			return response;
		}

		#endregion

		#region Location

		/// <summary>
		/// Načte seznam všech provozoven od právě přihlášeného obchodníka.
		/// Nutná oprávnění: Location_r
		/// </summary>
		/// <returns>Vrací list se seznamem provozoven (null pokud vznikla chyba v komunikaci).</returns>
		public List<LocationModel> Location_GetList()
		{
			var response = Safe(() => MakeRequest<ListResponse<LocationModel>>("Location/GetList"));

			return response?.Items;
		}

		/// <summary>
		/// Načte konkrétní provozovnu pro právě přihlášeného obchodníka.
		/// Nutná oprávnění: Location_r
		/// </summary>
		/// <param name="locationId">Id požadované provozovny.</param>
		/// <returns>Provozovnu s požadovaným id (null pokud vznikla chyba v komunikaci).</returns>
		public LocationModel Location_Get(int locationId)
		{
			var response = Safe(() => MakeRequest<Response<LocationModel>>(
				"Location/Get",
				new LocationRequest(locationId))
			);

			return response?.Result;
		}

		#endregion

		#region OneToOne

		/// <summary>
		/// Načte seznam všech možných počátků rezervací pro danou kombinaci zdroj, služba od právě přihlášeného obchodníka.
		/// Nutná oprávnění: BookingService_r
		/// </summary>
		/// <returns>Vrací list se seznamem všech možných počátků rezervací. (null pokud vznikla chyba v komunikaci).</returns>
		public List<double> OneToOne_GetAvailableStarts(int bookingResourceId, int bookingServiceId, DateTime from, DateTime to)
		{
			var response = Safe(() => MakeRequest<FreeSpacesResponse>(
				"OneToOne/GetAvailableStarts",
				new FreeSpacesRequest(bookingResourceId, bookingServiceId, from, to))
			);

			return response?.Starts;
		}

		/// <summary>
		/// Vytvoří rezervaci na vybraný zdroj a službu pro daného zákazníka od právě přihlášeného obchodníka.
		/// Nutná oprávnění: Booking_w
		/// </summary>
		/// <returns>Vrací odpověď API i s případnými validačními errory.</returns>
		public BookingStatusResponse OneToOne_CreateBooking(IBooking booking)
		{
			var response = Safe(() => MakeRequest<BookingStatusResponse>(
				"OneToOne/CreateBooking",
				new BookingCreateRequest(booking))
			);

			return response;
		}

		#endregion

		#region Segment

		/// <summary>
		/// Načte seznam všech segmentů pro přihlášeného obchodníka.
		/// Nutná oprvánění: Segment_r
		/// </summary>
		/// <returns>Vrací list se seznamem všech zdrojů (null pokud vznikla chyba v komunikaci)</returns>
		public List<SegmentModel> Segment_GetList()
		{
			var response = Safe(() => MakeRequest<ListResponse<SegmentModel>>("Segment/GetList"));

			return response?.Items;
		}

		#endregion

		#region Tag

		/// <summary>
		/// Načte seznam všech štítků pro zdroje právě přihlášeného obchodníka.<para/>
		/// Nutná oprávnění: BookingResource_r
		/// </summary>
		/// <returns>Vrací list se seznamem štítků (null pokud vznikla chyba v komunikaci).</returns>
		public List<TagModel> Tag_GetListForResource()
		{
			var response = Safe(() => MakeRequest<ListResponse<TagModel>>("Tag/GetListForResource"));

			return response?.Items;
		}

		/// <summary>
		/// Načte seznam všech štítků pro zákazníky právě přihlášeného obchodníka.<para/>
		/// Nutná oprávnění: Customer_r
		/// </summary>
		/// <returns>Vrací list se seznamem štítků (null pokud vznikla chyba v komunikaci).</returns>
		public List<TagModel> Tag_GetListForCustomer()
		{
			var response = Safe(() => MakeRequest<ListResponse<TagModel>>("Tag/GetListForCustomer"));

			return response?.Items;
		}

		#endregion

		#region Test

		/// <summary>
		/// Odešle požadavek typu "echo" (ověření dostupnosti serveru).
		/// Pokud kominkace v pořádku není, vyhodí příslušnou výjimku.
		/// </summary>
		/// <param name="text">Text, který se odesílá na server.</param>
		/// <returns>Text, obdržený od serveru, pokud je komunikace v pořádku je stejný jako <paramref name="text"/>.</returns>
		public string Test_Echo(string text)
		{
			var response = MakeRequest<EchoResponse>("Test/Echo", new TextRequest() { Text = text });

			return response.ReceivedText;
		}

		#endregion

		#region WorkingHours

		/// <summary>
		/// Načte seznam všech pracovních hodin v daném intervalu od právě přihlášeného obchodníka.
		/// Nutná oprávnění: Location_r, BookingResource_r
		/// </summary>
		/// <returns>Vrací list se seznamem všech pracovních hodin. (null pokud vznikla chyba v komunikaci).</returns>
		public List<WorkingHoursModel> WorkingHours_GetForPeriod(int? locationId, int? bookingResourceId, DateTime from, DateTime to)
		{
			var response = Safe(() => MakeRequest<WorkingHoursResponse>(
				"WorkingHours/GetForPeriod",
				new WorkingHoursRequest(locationId, bookingResourceId, from, to))
			);

			return response?.Items;
		}

		#endregion
	}
}
