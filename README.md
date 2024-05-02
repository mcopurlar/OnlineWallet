# Online Wallet

## Overview

Project contains 2 web apis, shared libraries and postman collection/environments. Docker support is applicable. It will spin up Postgresql and the apis. /files folder is included in the repo since it already contains seeded data for postgresql.

For simplicity there is a single repo that contains all the source of different apis and even postman collections.


## How to run project

### Required & Optional tools: 
1. Docker (preferably latest versions. docker engine 25.0.3 and api version 1.44 are the versions used)
2. Postman is nice to have to see some of the requests/responses.
3. dotnet 8 sdk to run at local if preferred

### Commands:

run it on root folder

```sh
docker compose up
```

Through postman, you can add beneficiaries for user(make sure user is in db, there are some users already added in db) and retrieve them. Also top up mobile phones can be requested. Directly calling account balance is also possible.

You can directly spin up apis from local but make sure postgresql container is up and running.

> ℹ️: **App is tested on windows. Postgresql /files folder might be corrupted if linux is used**



