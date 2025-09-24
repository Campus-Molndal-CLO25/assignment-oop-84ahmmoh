LagerhanteringsSystem Telsod  
Projektbeskrivning  

Det här är ett lagerhanteringssystem utan som läser in produkter och ordrar från CSV-filer, behandlar beställningarna genom att uppdatera lagret, och skriver direkt ut vad som skickades (✓) och vad som failade (✗). När allt är klart sparas det nya lagret i en CSV-fil. Enkelt, tydligt.

Screenshot

Ja, bilden visar typ exakt vad du förväntar dig: Programmet tuggar sig genom orders, pipar ut vilka som gick iväg och vilka som inte höll måttet, samtidigt som lagret justeras i kulisserna.

Så drar du igång programmet
Det här behöver du:

- .NET 8.0 eller senare (inte äldre grejer, snälla)
- Visual Studio, VS Code, JetBrains Rider eller bara terminalen – pick your poison

Klona projektet såhär:
git clone https://github.com/orgs/Campus-Molndal-CLO25/repositories/assignment-oop-84ahmmoh
cd assignment-oop-[ditt-username]

Starta programmet

Visual Studio:

1. Öppna filen LagerhanteringsSystemTelsod.sln eller bara hela projektmappen
2. Släng på F5 eller tryck på "Start"-knappen

Visual Studio Code:

- Öppna mappen: code .
- Har du inte C# Dev Kit? Installera det, annars blir det gråt.
- F5 eller kör på "Run → Start Debugging" i menyn

Terminal/kommandotolk:

dotnet run

Obs! Du måste ha med dig lager.csv och ordrar.csv i projektmappen – annars har programmet inget att jobba med. 

Hur använder man det då?

Uppstart, klart – programmet dammsuger automatiskt upp lager.csv och ordrar.csv. Sedan mosar den igenom ALLA ordrar och du får direkt respons istället för en tråkig logg:

✓ Order skickad
✗ Order kunde inte skickas (t.ex. tomt lager, sorry)

Efteråt? Producerar den en uppdaterad lagerfil: lager_uppdaterat.csv.

Funktioner

Standardgrejer

- Läser in produkter → Product-objekt
- Läser in ordrar → Order-objekt
- Tuggar igenom ordrar och justerar lagret
- Säger till exakt vad som hände med varje beställning
- Sparar lagerstatus till en ny CSV

Extra för VG-jägare

- Robust felhantering för knäppa CSV-rader
- Astydlig feedback med ✓ och ✗ – inget mumlande
- Sammanställer hur många orders som gick igenom vs. vad som kraschade

Projektstruktur

projektmapp/
├── Program.cs          # Programmet drar igång här
├── Product.cs          # Product-klassen
├── Order.cs            # Order-klassen
├── README.md           # Du läser den nu
├── reflection.md       # Lite tankar om projektet
├── lager.csv           # Lagerdemo
├── ordrar.csv          # Beställningsdemo
└── screenshot.png      # Ja, en bild på eländet

Teknologisnack

Språk: C#  
Framework: .NET 8.0  
Utvecklingsmiljö: Visual Studio / VS Code / annat om du är wild  

Spara projektet för evigheten

Vill du att det här fortsätter existera efter kursen? Gör såhär:

- Besök projektet: https://github.com/orgs/Campus-Molndal-CLO25/repositories/assignment-oop-84ahmmoh
- Klicka "Fork" uppe till höger och välj ditt eget konto
- Putsa till beskrivningen, uppdatera topics, lägg in README, CSV-filer, screenshots – allt som hör till!

Varför bry sig om detta?

- Portfolio: Visa framtida arbetsgivare att du kan kod
- Backup: Skolans repo plockas ner och då är det borta, pang bom
- Referens: Behöver du koden igen senare? Nice att ha!

Länkar

GitHub Repo: https://github.com/orgs/Campus-Molndal-CLO25/repositories/assignment-oop-84ahmmoh
Din privata fork: (byt ut till din egen länk efter du fork:at)  
Kurshemsida: [lägg in länk om det är relevant]