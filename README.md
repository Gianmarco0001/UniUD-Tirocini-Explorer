# UniUD-Tirocini-Explorer
Ho creato questo software per semplificare la visualizzazione del file Excel relativo alle aziende associate con UniUD per il Tirocinio.

# 🎓 UniUD Tirocini Explorer

> **Basta file Excel illeggibili.** Esplora le convenzioni di tirocinio dell'Università di Udine con un'interfaccia moderna, veloce e funzionale.

---

## 🖼️ Anteprima Software

![2-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/ddb1d383-ea08-4543-88aa-2175c969d081)


## 🚀 Che cos'è?

**UniUD Tirocini Explorer** è un'applicazione desktop per Windows (WPF) nata per risolvere un problema comune agli studenti: consultare l'enorme lista di aziende convenzionate per i tirocini (spesso fornita in file Excel densi e difficili da navigare).

L'app trasforma quei dati in una **Dashboard Interattiva** che permette di filtrare, cercare e salvare le aziende preferite in pochi click.

## ✨ Funzionalità Principali

* **🎨 Interfaccia Moderna:** Basata sul **Material Design**, pulita e intuitiva.
* **⚡ Performance Elevate:** Grazie alla *UI Virtualization*, gestisce liste di migliaia di aziende senza rallentamenti.
* **🔍 Ricerca Smart:** Filtra istantaneamente per nome azienda, città o settore.
* **🎓 Filtri per Corso:** Mostra solo le aziende che cercano il tuo profilo (ING, ECO, MAT/INFO, LIN, GIU).
* **❤️ Preferiti:** Salva le aziende che ti interessano cliccando sul cuore. La lista rimane salvata anche se chiudi l'app.
* **🗺️ Integrazione Mappe:** Clicca sull'icona della mappa per vedere subito dove si trova l'azienda su Google Maps.
* **📧 Contatto Rapido:** Pulsanti diretti per inviare Email o visitare il sito web aziendale.
* **📂 Importazione Flessibile:** Supporta sia i file `.xlsx` originali di UniUD che i file `.csv`. Converte automaticamente l'Excel in formato leggibile.

## 🛠️ Tecnologie Utilizzate

* **Linguaggio:** C# (.NET 6 / .NET 8)
* **Framework UI:** WPF (Windows Presentation Foundation)
* **Librerie:**
    * `MaterialDesignInXamlToolkit` (per la grafica stile Google)
    * `CsvHelper` (per la lettura veloce dei dati)
    * `ExcelDataReader` (per la conversione da .xlsx)

## 📦 Installazione e Utilizzo

### Requisiti
* PC con Windows 10 o 11.
* File Excel delle aziende scaricato dal portale UniUD. (https://www.uniud.it/it/didattica/area-servizi-studenti/servizi-studenti/tirocini/informazioni-generali/elenco-aziende-convenzionate)

### Come avviare l'app (Standalone)
1.  Scarica l'ultima versione dalla sezione [Releases](link-alla-tua-repo/releases).
2.  Estrai o avvia direttamente il file `.exe`.
3.  Al primo avvio, ti verrà chiesto di caricare il file delle aziende. Seleziona il file `.xlsx` o `.csv` scaricato dall'università.
4.  Fatto! I dati verranno salvati localmente per gli accessi futuri.


## 👤 Autore

**Gianmarco Benedetti | GBYTEZ**


## 📄 Licenza

Questo progetto è distribuito sotto licenza **MIT**. Sentiti libero di usarlo, modificarlo e distribuirlo.

![Build Status](https://img.shields.io/badge/build-passing-brightgreen) ![Platform](https://img.shields.io/badge/platform-Windows-blue) ![License](https://img.shields.io/badge/license-MIT-green)

## DISCLAIMER

DISCLAIMER (IMPORTANTE)

Questa applicazione è un progetto studentesco NON UFFICIALE.

   Affiliazione: Questo software non è sviluppato, approvato o gestito dall'Università degli Studi di Udine. È un progetto open source creato da uno studente per utilità personale e condivisa.

   Dati: I dati visualizzati provengono dai file Excel pubblici/distribuiti dall'ateneo. L'applicazione si limita a rielaborarli graficamente. Lo sviluppatore non è responsabile per eventuali errori, omissioni o dati non aggiornati presenti nei file originali o causati dalla conversione.

   Responsabilità: Il software è fornito "così com'è", senza garanzie di alcun tipo. Utilizzalo a tuo rischio. Per informazioni ufficiali e vincolanti, fare sempre riferimento al portale ufficiale di UniUD o alla segreteria.
