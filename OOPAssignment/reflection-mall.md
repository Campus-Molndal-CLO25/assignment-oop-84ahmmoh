# Reflektion över [LagehanteringsSystem Telsod]

## Planering

 Först behövde jag fixa några basgrejer – alltså, själva grundfunktionerna som att läsa in produkter från en CSV-fil, ordrar från en annan, och sen i slutändan spara tillbaka det uppdaterade lagret. Jag kladdade upp en snabb skiss över vilka klasser som behövdes – det blev Product och Order (jag kallar väl allt på engelska för att det bara blir så ibland, ni vet?). Gav varje klass sina egna metoder och så där, inget jätteavancerat.

Jag körde igång steg för steg. Först letade jag upp produkter i min CSV och byggde Product-objekt. Sen tog jag tag i att läsa in ordrarna och satte ihop Order-objekt. Implementation kör jag beroende på om användarens order är “okej” eller “nope” – typ ett ✔️ eller ett fet X.

När allt var fixat mot CSV:en sparade jag tillbaka lagret så det var up to date, så att säga. Grejen är att jag hela tiden ville vara säker på att inget ballade ur, så jag testade varenda liten etapp med miniversioner av CSV-filer tills allt flöt på. No surprises, please.



## Problem

Vad var den svåraste delen av uppgiften och varför? Hur löste du de problem du stötte på?

Den riktiga huvudvärken var att få inläsningen från CSV att funka smidigt—särskilt med våra kära svenska bokstäver som å, ä och ö. Plus, vissa av filerna var fulla av tomma och helt knäppa rader… sånt man bara älskar. I början brakade programmet ihop direkt med en sån där IndexOutOfRangeException varje gång nån rad hade för få kolumner. Kul va?

Så, vad blev räddningen? För det första slängde jag in en TryParse på allt som luktade siffror, t.ex. pris och antal, bara för att slippa spränga allt om det råkar vara nån random text där. Sen började jag faktiskt kolla att det var rätt antal kolumner innan jag ens gav mig på att läsa värdena. Och så fick jag styra upp med UTF-8 (för jag pallar inte fler ?-tecken istället för åäö).

## Stolthet

Vad är du mest stolt över med ditt projekt?

Det jag är absolut mest nöjd över? Utan tvekan att programmet nu pallar både lyckade OCH misslyckade ordrar – och faktiskt ger användaren tydliga besked med snygga ✓ och ✗. Man slipper gissa vad som gick fel, liksom. Allt staplas snyggt i en liten summering på slutet också: hur många ordrar som flög igenom och hur många som bombade totalt.

Och alltså, jag diggar verkligen att systemet bara borstar av sig trasiga rader i CSV-filen istället för att freaka ut och krascha. Det är ju så mycket skönare att slippa hålla på och laga en massa felformatad data varenda gång – det bara tuggar på och gör sitt jobb.

---

## Reflektion för Väl Godkänt (VG)
### Datastrukturer
Okej, så här gjorde jag: Jag slängde in alla produkter i en List<Product>. Rätt smidigt för att hålla koll på lagret, och det är busenkelt att bara loopa igenom listan när jag behöver hitta något till en order.

Sen hade jag en List<Order> för att hålla pli på alla inkommande beställningar. Listor är ju perfekta när man vill processa saker i den ordning de kommer in, så det var inte ens nåt att tveka på där.

Visst, man hade kunnat köra en Dictionary<string, Product> också, där produktnamnet är nyckeln. Det hade gått mycket snabbare att hitta produkter på det sättet, speciellt om listan började växa. Men, handen på hjärtat, jag körde på en vanlig lista ändå — vi snackar ändå om så pass få grejer att det liksom inte gör nån riktig skillnad. Keep it simple, liksom.


### Clean Code och Struktur

Jag la ner en hel del krut på att få koden både schysst modulär och snabbläst. Alltså, jag döpte klasser och metoder så det inte finns minsta tvekan om vad de gör – inga mystiska förkortningar här inte, typ "Product", "Order", "LoadProductsFromCsv", "ProcessOrders" och liknande. Man fattar direkt, liksom. 

Sen försökte jag verkligen separera grejerna ordentligt: Product-klassen snackar bara produkter, Order-klassen bryr sig bara om ordrar. Program-delen? Den sköter filhämtning, crunchar ordrar och slänger ut resultaten. Ingen klåfingrighet över gränserna där inte.

För att slippa klippa-och-klistra överallt så delade jag upp koden i metoder som LoadProductsFromCsv, LoadOrdersFromCsv, ProcessOrders, SaveUpdatedProductsToCsv… Du hör ju, inget copy-paste-helvete.

Och ja – kommentarer och lite smart felhantering slank med också. Så om nån rad i filen ballar ur med för få kolumner eller konstigt format så går koden inte bananas. Så, kort och gott: både smartare och roligare att jobba med sen.

### Framtida utveckling

Koden är ganska smidig att bygga ut, faktiskt.

Behövs fler produktfält? Bara släng in dem i Product-klassen, du behöver inte riva upp hela orderhanteringen för det. 

Och om du vill införa fler konstiga orderregler—kanske någon grej med prioritet, eller börja hålla på med reservationer—då pular du bara i ProcessOrders-metoden. Din CSV-hantering bryr sig inte det minsta om det.

Det enda som känns riktigt bökigt är om lagringen ska bytas ut mot databashantering istället för CSV. Då snackar vi ny kod för att läsa och skriva, ingen väg runt det.

Men: Eftersom datagrejerna (Product, Order) och logiken (alla Program-metoder) är ordentligt separerade, känns hela systemet rätt modulärt. Ganska lätt att bygga vidare på för framtida påhitt om man ska vara ärlig.
---

_Denna reflektion är skapad som del av inlämningsuppgiften i kursen "Grundläggande objektorienterad programmering i C#" vid Yrkeshögskolan Campus Mölndal._
