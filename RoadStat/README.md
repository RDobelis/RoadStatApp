# RoadStat

## Sistēmas apraksts

Sistēma paredzēta automašīnu skaita un to vidējā ātruma konkrētā ceļa posmā statistikas ielādei un apskatei. Lietotājs lietotājs vēlas atlasīt ierakstus pēc noteiktiem kritērijiem. Atsevišķā sadaļā lietotājs vēlas redzēt automašīnu vidējo ātrumu vienas dienas šķērsgriezumā grafika veidā.

## Funkcionālās prasības

- Sistēmā jāvar ielādēt statistikas dati par mēnesi – tiek doti sākotnējie dati (fails ‘speed.txt’) viena mēneša griezumā – datums, ātrums un automašīnas reģistrācijas numurs, kas atdalīti ar TAB rakstzīmi. Datu ielādi sistēmā var nodrošināt vai nu ar atsevišķu konsoles app, vai caur Web UI, pēc autora izvēles;
- atsevišķā sadaļā/lapā sistēmas lietotājam jāvar atlasīt statistikas dati:
  - tabulas veidā jāparāda datums, ātrums un automašīnas reģ.numurs;
  - dati jāvar filtrēt pēc šādiem kritērijiem:
    - ātrums (jāatlasa ieraksti, kas vienādi vai lielāki par norādīto vērtību);
    - datums no (visi ieraksti sākot no norādītā datuma);
    - datums līdz (visi ieraksti līdz norādītajam datumam, ieskaitot);
  - jebkuru filtru jāvar arī nenorādīt, t.sk., visi filtri arī var būt nenorādīti;
  - labā vienlaicīgi jāattēlo ne vairāk par 20 rindām;
- sistēmas lietotājam jāvar izvēlēties konkrēta mēneša diena, par kuru parādīt datus, sistēmai jāparāda vidējā ātruma grafiks dienas griezumā ar soli viena stunda (attiecīgi grafikā jābūt 24 vērtībām);

## Nefunkcionālās prasības

- izmantojamās tehnoloģijas:
  - ASP.NET Core;
  - TypeScript (vēlams);
  - citas tehnoloģijas pēc autora ieskatiem;
  - EntityFramework Code-first (vēlams, bet ja izmanto citu vai papildus – nepieciešams skaidrojums) – ielādēto datu glabāšanai;
- solution saknes direktorijā jāizveido fails Readme.md (markdown), kurā jānorāda:
  - autora vārds, uzvārds;
  - īsa risinājuma uzstādīšanas/palaišanas instrukcija;
  - problēmas, ar kurām autors saskārās darba izpildes gaitā (ja ir);
  - potenciālo nākotnes uzlabojumu apraksts (ja ir);
  - citi komentāri;
- risinājumu jānodod kā ZIP (7z) arhīvs, kurā jāiekļauj:
  - viss pirmkods (solution);
  - nokompilēts risinājums, kas gatavs palaišanai;
  - citi risinājuma palaišanai nepieciešamie dati (piemēram, DB, SQL skripti (ja nepieciešams), citi dati);
  - ekrānattēls no strādājošas sistēmas;

## Vērtēšanas kritēriji

- risinājuma atbilstība uzstādītajām prasībām;
- risinājuma veiktspēja;
- [sekundāri] risinājuma modularitāte, koda uzbūve un stils;
- [maznozīmīgi] sistēmas UI lietošanas ērtums, UI dizains.

Paredzētais risinājuma izpildes laiks: 1-2h. Atļauts izmantot koda vedņus un veidnes.
