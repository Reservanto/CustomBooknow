## Vlastní implementace rezervačního formuláře pomocí Reservanto API

- Ukázka možnosti vytvoření vlastního rezervačního formuláře za pomocí API, které poskytuje rezervační systém [Reservanto](https://www.reservanto.cz)
- Dokumentaci k API lze nalézt [zde](https://api.reservanto.cz/Help)
- Ukázkový kód je rozdělen do dvou částí, ukázky napojení na API **(Reservanto.ApiClient)** a samotný webový projekt **(Reservanto.CustomBooknow)**

### Reservanto.ApiClient

- Ukázkové rozhraní pro komunikaci s Reservanto API
- Veškerá komunikace probíhá pomocí třídy `ReservantoApi` (viz ReservantoApi.cs), která má na již připravené ukázkové metody pro komunikaci, včetně metod pro získání a obnovu **STT** (Short Time Token).
    - Forma pojmenování metod je ekvivalentní k metodám viditelným v dokumentaci API, metody jsou definovány vždy formou `{název controleru}_{název akce}`

### Reservanto.CustomBooknow

- Ukázková možnost vytvoření front-end části rezervačního formuláře
- Před prvním spuštěním je nutné nejprve správně projekt nakonfigurovat v souboru `appsettings.json`
- Hodnoty konfigurace jsou následující
    - `ClientId` - Id aplikace API **(nutné vyplnit před první spuštěním)** - způsob získání je popsán v dokumentaci v sekci "Úvod"
    - `ApiUrl` - Url adresa, na které se nachází API tj. https://api.reservanto.cz/v1
    - `RequestedRights` - Požadovaná práva, se kterými bude aplikace přistupovat k API (viz dokumentace, kapitola "Všechna práva")
    - `LongTimeToken` - Přístupový Long Time Token **(LTT)** - hodnota je doplněna při běhu aplikace
    - `ShortTimeToken` - Přístupový Short Time Token **(STT)** - hodnota je doplněna při běhu aplikace
- Při prvním spuštění je nutné nejdříve správně získat přístupové tokeny 
- Získání tokenů lze provést pomocí akce `/Api/ConnectToAPI`
- Po přístupu na výše zmíněnou akci, budete přesměrováni do systému Reservanto, kde je nutné povolit přístup pro API

