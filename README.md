# Příjem tankování

Aplikace pro převod datových souborů obsahujících transakce tankování UniPOS a SelfServiceSystem
do formátu, který akceptuje systém AutoPlan Kniha jízd.

## Funkce

- Načtení transakčních souborů z čerpacích systémů UniPOS a SelfServiceSystem
- Automatická konverze do kompatibilního datového formátu pro AutoPlan Kniha jízd
- Filtrování nevalidních transakcí – např. cizí SPZ nebo záznamy, které nemají být zahrnuty do knihy jízd
- Evidence SPZ k filtrování v lokálním CSV souboru, který aplikace sama vytvoří a udržuje
- Překlad čísel osobních karet z UniPOS na SPZ vozidel
	- UniPOS registruje tankovací karty buď na vozidla, nebo na osoby
	- aplikace provádí převod čísel karet přiřazených osobám na příslušné SPZ vozidel, aby byl export pro AutoPlan knihu jízd konzistentní
- Jednoduché uživatelské rozhraní ve Windows Forms
- Možnost spouštět převody a filtrace i z příkazové řádky (viz níže)

## Instalace a spuštění

1. 1. Naklonujte repozitář:
```bash
git clone https://github.com/RomanDotF/P-jem-tankov-n-.git
```
2. Otevřete řešení Příjem tankování.sln ve Visual Studio 2022.
3. Stiskněte F5 (Spustit) nebo použijte nabídku Ladění → Spustit.

## Použití z příkazové řádky

Aplikaci lze spustit i bez grafického rozhraní s následujícími příkazy:

### Převod UniPOS

```bash
Příjem_tankování.exe prevod-unipos <vstup.csv> <vystup.csv> <misto_tankovani>
```
- <vstup.csv> – cesta ke vstupnímu UniPOS CSV souboru
- <vystup.csv> – cesta k výstupnímu CSV souboru
- <misto_tankovani> – název místa tankování (např. Praha, Brno)

### Převod SelfServiceSystem

```bash
Příjem_tankování.exe prevod-sss <vstup.csv> <vystup.csv> <misto_tankovani>
```
- <vstup.csv> – cesta ke vstupnímu SelfServiceSystem CSV souboru
- <vystup.csv> – cesta k výstupnímu CSV souboru
- <misto_tankovani> – název místa tankování (např. Praha, Brno)

### Odebrání mýta z datového souboru OMV

```bash
Příjem_tankování.exe odebrat-myto <vstup.csv> <vystup.csv>
```
- <vstup.dat> – cesta ke vstupnímu OMV DAT souboru
- <vystup.dat> – cesta k výstupnímu DAT souboru

### Nápověda

```bash
Příjem_tankování.exe help
```
Zobrazí nápovědu s dostupnými příkazy a jejich parametry.
Obsah repozitáře
- Příjem tankování.sln – hlavní řešení projektu
- Form1.cs, Form1.Designer.cs – hlavní logika aplikace
- Program.cs – vstupní bod aplikace, zpracování argumentů
- NapovedaForm.cs – okno s nápovědou
- VylouceneSPZForm.cs, OsobniKartyForm.cs – správa SPZ a osobních karet
- file-covers.ico, file-covers.png – logo a ikona aplikace
- .gitignore, .gitattributes – konfigurační soubory Git
- LICENSE.txt – projekt je licencován pod GPL-3.0
## Licence

Tento projekt je licencován pod GPL-3.0 – viz soubor LICENSE.txt.

## Autor

Roman Fic

Vytvořeno pro společnost LUKROM, spol. s r.o.

## Motivace

Úprava cílové aplikace je vždy otázkou peněz – ať už jde o přímé náklady na implementaci, nebo čas strávený na vlastním vývoji, který se také počítá do nákladů.
Na druhou stranu vývoj a jakákoli tvůrčí práce mohou člověka bavit, posouvat dál v jeho vědění, myšlení a celkově ho obohacovat.
Proto tento projekt vznikl nejen jako praktické řešení konkrétní potřeby, ale i jako příležitost k učení a osobnímu růstu.