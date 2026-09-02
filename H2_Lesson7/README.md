# Collection&lt;T&gt; – en generisk samling

## Formål

Programmet indeholder en hjemmelavet generisk klasse, `Collection<T>`, der kan holde
elementer af én valgfri type. Formålet er at vise, hvordan generics gør det muligt at
skrive **én** klasse, der virker for alle typer, i stedet for at skrive en ny samling
for hver type – og uden at man skal caste elementerne, når de hentes ud igen.

Typen bestemmes først, når samlingen oprettes (`new Collection<string>()`), og
compileren håndhæver den derefter: forsøger man at lægge en bil ned i en
`Collection<string>`, fejler det allerede ved oversættelsen i stedet for under kørslen.

Samlingen kan:

| Medlem | Beskrivelse |
|---|---|
| `Add(T element)` | Tilføjer et element til slutningen af samlingen. |
| `Remove(T element)` | Fjerner første forekomst af elementet. Returnerer `true`, hvis det lykkedes, ellers `false`. |
| `Find(Func<T, bool> predicate)` | Returnerer det første element, der opfylder betingelsen, eller `default(T)`, hvis intet matcher. |
| `Count` | Antallet af elementer i samlingen. |
| `Items` | Skrivebeskyttet visning af elementerne, så de kan gennemløbes udefra. |

Konsolprogrammet tester samlingen med to forskellige typer: `Collection<string>` og
`Collection<Car>`, hvor bilerne er genbrugt fra biludlejningen i lektion 6. Bemærk at
en `Collection<Car>` kan indeholde både `Van` og `PassengerCar`, fordi begge *er* en `Car`.

## Opsætning

Forudsætninger:

* .NET SDK 10 (kør `dotnet --version` for at tjekke). Visual Studio 2026 eller
  Visual Studio Code virker begge – projektet kan også bygges fra kommandolinjen alene.

Hent og byg:

```bash
git clone <repository-url>
cd H2
dotnet build
```

Projektet ligger i mappen `H2_Lesson7` og har en projektreference til `H2_Lesson6`,
som leverer klasserne `Car`, `Van` og `PassengerCar`. Referencen bygges automatisk med,
så der skal ikke gøres noget ekstra – men begge mapper skal være hentet.

## Brug

Kør programmet:

```bash
dotnet run --project H2_Lesson7
```

Det kører de to tests og skriver resultatet ud:

```
=== Collection<string> ===
Tilføjet 3 navne. Count = 3
Indhold: Christopher, Mette, Sofie
Find(navn starter med 'M')   -> Mette
Find(navn længere end 20)   -> (intet match)
Remove("Mette")             -> True (findes)
Remove("Mette") igen        -> False (findes ikke længere)
Count = 2, indhold: Christopher, Sofie

=== Collection<Car> ===
Tilføjet 3 biler. Count = 3
  Personbil Toyota Yaris (AB12345), 5 sæder, 495 kr./dag
  Varevogn Ford Transit (CD54321), lastevne 1200 kg, 750 kr./dag
  Personbil Skoda Octavia (EF67890), 5 sæder, 625 kr./dag
Find(dagspris under 600)     -> Personbil Toyota Yaris (AB12345), 5 sæder, 495 kr./dag
Find(reg.nr. CD54321)        -> Varevogn Ford Transit (CD54321), lastevne 1200 kg, 750 kr./dag
Find(reg.nr. XX99999)        -> (intet match)
Remove(transit)              -> True (findes)
Remove(transit) igen         -> False (findes ikke længere)
Count = 2
```

Sådan bruges klassen i egen kode:

```csharp
Collection<string> names = new Collection<string>();
names.Add("Christopher");
names.Add("Mette");

string? found = names.Find(name => name.StartsWith("M"));   // "Mette"
string? missing = names.Find(name => name.Length > 20);     // null - intet match

bool removed = names.Remove("Mette");                       // true
bool removedAgain = names.Remove("Mette");                  // false - findes ikke længere

Console.WriteLine(names.Count);                             // 1
```

To ting er værd at bemærke:

* `Find` returnerer `default(T)`, når intet matcher. For referencetyper som `string`
  og `Car` er det `null`, så husk at tjekke resultatet, før det bruges.
* `Remove` sammenligner med typens egen lighedssammenligning. For `string` betyder det,
  at teksten skal være ens, mens det for `Car` – som ikke overskriver `Equals` –
  betyder, at det skal være det samme objekt.
