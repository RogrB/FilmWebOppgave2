Salting og Hashingfunksjon for innlogging og registrering av nye brukere er hentet fra sikkerhetskompendiumet, og skrevet av fagl�rer.
Koden er kommentert og kredittert deretter

Denne l�sningen benytter seg ikke av postnr/poststed og den p�f�lgende konverteringen mellom databaselag og view. Det blir derimot gjort en lignende konvertering mellom "Kunde" klassen som vises i viewet og en "KundeDB" som lagres i databasen for � h�ndtere passord hashing, salting og lagring i databasen. Det er ogs� laget en tredje klasse "EndreKunde" der passordfeltet ikke er "required" - for � la en bruker endre sine personopplysninger.

Denne l�sningen benytter seg heller ikke av den demonstrerte DBInit l�sningen fra forelesningen. Det blir derimot brukt et eget DBInsert view som fungerer p� samme m�te.

N�r det gjelder "using (var db = new db())", blir det brukt "using" der det g�r, mens de tilfellene hvor det ville oppst�tt en filmelding om at ressursen er utilgjengelig blir lukking av databasen overlatt til garbagecollection - dette har blitt diskutert med fagl�rer.

Jeg har i denne besvarelsen ikke kommentert koden i alt for stor grad, da metodenavnene i de fleste tilfeller er ganske selvforklarende.

Da jeg var usikker p� om det skulle lages en "streametjeneste" hvor en enkelt film blir kj�pt av gangen, eller om det skulle v�re en "handlekurv" funksjon, har jeg laget mulighet for begge. En enkelt film kan kj�pes og "vises" i et FilmVisning view, og man kan som registrert bruker legge filmer til i en "�nskeliste" som vil vises p� brukersiden.

Funksjoner (foruten det man vil forvente iht oppgavebesvarelsen):
Listing av filmer og skuespillere i respektive views kan sorteres etter �nsket rekkef�lge.
Brukere kan "stemme" (gi en vurdering) p� en film n�r de er logget inn.
Brukere kan legge igjen kommentarer p� filmer.
Egen brukerside hvor en bruker kan forandre personopplysninger.
Brukere kan legge filmer til en �nskeliste.
Brukersiden inneholder liste over filmer som en bruker har sett, stemt p� og lagt til i �nskelisten.
Brukersiden vil ogs� komme med filmforslag basert p� sjangere som brukeren tidligere har sett.
S�kefunksjon med forslag etterhver som man skriver inn s�k.


For sensur kan det opprettes egen brukerkonto, eller benyttes f�lgende konto for � f� et mer utfyllende bilde av funksjonenen p� brukersiden:

Brukernavn: 	Bruker01
Passord: 	testtest