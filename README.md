## API

### webapi
API'en er et forsøg på at lave et hjemmelavet auth API, som skal fungere som backend til et website.

Anvender SQLite som database, docker volumes for persistent data, hvis man ønsker at køre den i docker.
Database oprettes automatisk med entityframework, på den placering man vælger i docker compose eller i program.cs.

Grundet til valg af docker, nemt at oprette flere instanser af denne API, blot lav en ny docker compose fil, hvis du anvende den til flere websider på samme tid, med forskellige databaser.

### Kørsel med Docker Compose
For at starte API'en:
docker compose -f docker-compose.dev.yml up -d --build
docker compose -f docker-compose.prod.yml up -d --build

(Hvis du laver ændringer i Dockerfilen skal du køre --build, ellers kan du udelade det)
