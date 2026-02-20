## API

### webapi
API'en er forsøg på at lave et hjemmelavet auth API, som skal fungere som backend til et website.
Denne API bruger SQLite som database og peger på en lokal mappe. 
Den bruger Docker volumes, så dataen er persistent. 
Databasen oprettes automatisk på den angivne placering, hvis den ikke allerede findes, via Entity Framework.

I API'en findes der allerede 2 compose filer. Disse er eksempler

### Kørsel med Docker Compose
For at starte API'en:
docker compose -f docker-compose.dev.yml up -d --build
docker compose -f docker-compose.prod.yml up -d --build

(Hvis du laver ændringer i Dockerfilen skal du køre --build, ellers kan du udelade det)
