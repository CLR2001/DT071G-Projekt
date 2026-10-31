# ◈ Projektuppgift - DT071G
Detta repository innehåller källkoden för en interaktiv konsolbaserad frågesportapplikation (Quiz) utvecklad i C# och .NET.

## ✦ Projektbeskrivning
Denna konsolapplikation är byggd med fokus på god objektorienterad struktur, separation of concerns och designprinciper som dependency inversion. Applikationen hanterar spelflöden, dynamisk filtrering av frågor, samt sparande och läsning av high scores via JSON-filer. För en modern och stilren användarupplevelse i terminalen används `Spectre.Console`.

## ⚙ Funktioner
- **1:** Starta och spela quiz-sessioner med anpassningsbara inställningar för kategorier och svårighetsgrader.
- **2:** Flexibel arkitektur med interfaces (`IQuestionLoader`, `IHighScoreStorage`) för JSON-baserad datahantering.
- **3:** Interaktiva menyer och formaterade vyer (`MenuScreen`, `GameScreen`, `HighScoreScreen`, m.fl.) för ett smidigt gränssnitt i konsolen.

## ⌨ Installation & Setup

1. **Klona projektet:**
   ```bash
   git clone https://github.com/CLR2001/DT071G-Projekt.git
   ```
2. **Installera beroenden:**
   ```bash
   dotnet add package Spectre.Console
   ```
3. **Starta applikationen:**
   ```bash
   dotnet run
   ```

## ⬀ Länkar
- [Rapport](/Ludvig%20Rosenqvist-DT071G-Projektuppgift.pdf)
- [Video demonstration](https://youtu.be/zagOafPVQcw)
- [Flödesschema - Huvudloop](/gameLoop.png)
- [Flödesschema - Spelloop](/mainLoop.png)

## ⬢ Utvecklare
**Ludvig Rosenqvist** — *Student*
🔗 [GitHub](https://github.com/CLR2001)
