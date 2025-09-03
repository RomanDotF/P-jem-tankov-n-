# Příjem tankování

Aplikace pro převod datových souborů obsahujících transakce tankování **UniPOS** a **SelfServisSystem**  
do formátu, který akceptuje systém **AutoPlan Kniha jízd**.

## Funkce
- Načtení transakčních souborů z čerpacích systémů **UniPOS** a **SelfServisSystem**
- Automatická konverze do kompatibilního datového formátu pro **AutoPlan Kniha jízd**
- **Filtrování nevalidních transakcí** – např. cizí SPZ nebo záznamy, které nemají být zahrnuty do knihy jízd
- Evidence SPZ k filtrování v **lokálním CSV souboru**, který aplikace sama vytvoří a udržuje
- **Překlad čísel osobních karet z UniPOS na SPZ vozidel**  
  - UniPOS registruje tankovací karty buď na **vozidla**, nebo na **osoby**  
  - aplikace provádí převod čísel karet přiřazených osobám na příslušné **SPZ vozidel**, aby byl export pro AutoPlan knihu jízd konzistentní
- Jednoduché uživatelské rozhraní ve Windows Forms
## Instalace a spuštění
1. Naklonujte repozitář:
   ```bash
   git clone https://github.com/RomanDotF/P-jem-tankov-n-.git
2. Otevřete řešení Příjem tankování.sln ve Visual Studio 2022.
3. Stiskněte F5 (Spustit) nebo použijte nabídku Ladění → Spustit.

## Obsah repozitáře
- .sln – hlavní řešení projektu
- Form1.cs, Form1.Designer.cs – zdrojový kód aplikace
- .gitignore, .gitattributes – konfigurační soubory Git
- LICENSE.txt – projekt je licencován pod GPL-3.0

## Licence
Tento projekt je licencován pod GPL-3.0 – viz soubor LICENSE.txt.

## Autor
Roman Fic