Salting og Hashingfunksjon for innlogging og registrering av nye brukere er hentet fra sikkerhetskompendiumet, og skrevet av faglærer.
Koden er kommentert og kredittert deretter

Denne løsningen benytter seg ikke av postnr/poststed og den påfølgende konverteringen mellom databaselag og view. Det blir derimot gjort en lignende konvertering mellom "Kunde" klassen som vises i viewet og en "KundeDB" som lagres i databasen for å håndtere passord hashing, salting og lagring i databasen. Det er også laget en tredje klasse "EndreKunde" der passordfeltet ikke er "required" - for å la en bruker endre sine personopplysninger.

Denne løsningen benytter seg heller ikke av den demonstrerte DBInit løsningen fra forelesningen. Det blir derimot brukt et eget DBInsert view som fungerer på samme måte.

Når det gjelder "using (var db = new db())", blir det brukt "using" der det går, mens de tilfellene hvor det ville oppstått en filmelding om at ressursen er utilgjengelig blir lukking av databasen overlatt til garbagecollection - dette har blitt diskutert med faglærer.

Jeg har i denne besvarelsen ikke kommentert koden i alt for stor grad, da metodenavnene i de fleste tilfeller er ganske selvforklarende.

Da jeg var usikker på om det skulle lages en "streametjeneste" hvor en enkelt film blir kjøpt av gangen, eller om det skulle være en "handlekurv" funksjon, har jeg laget mulighet for begge. En enkelt film kan kjøpes og "vises" i et FilmVisning view, og man kan som registrert bruker legge filmer til i en "ønskeliste" som vil vises på brukersiden.

Funksjoner (foruten det man vil forvente iht oppgavebesvarelsen):
Listing av filmer og skuespillere i respektive views kan sorteres etter ønsket rekkefølge.
Brukere kan "stemme" (gi en vurdering) på en film når de er logget inn.
Brukere kan legge igjen kommentarer på filmer.
Egen brukerside hvor en bruker kan forandre personopplysninger.
Brukere kan legge filmer til en ønskeliste.
Brukersiden inneholder liste over filmer som en bruker har sett, stemt på og lagt til i ønskelisten.
Brukersiden vil også komme med filmforslag basert på sjangere som brukeren tidligere har sett.
Søkefunksjon med forslag etterhver som man skriver inn søk.


For sensur kan det opprettes egen brukerkonto, eller benyttes følgende konto for å få et mer utfyllende bilde av funksjonenen på brukersiden:

Brukernavn: 	Bruker01
Passord: 	testtest