# Backend

[![Build Status](https://travis-ci.com/FightCore/Backend.svg?branch=master)](https://travis-ci.com/FightCore/Backend)

The backend project for the FightCore application.
This contains both the API and the Identity Server for simplicity reasons.

## Tech used

The FightCore API runs as a dotnet core 2.2 API using the Identity Server 4 as
a way of authenticating users.
OAuth 2 should be properly implemented to allow the users of the API to execute
any action that is also possible on the website and more.

This is also the design ideology behind FightCore and as well its API.
It's meant to be as open as possible and allow changes from the smash developing community.

## Docker

To build the docker image for FightCore.Backend and/or FightCore.Identity,
open a commandline in the solution folder and execute the following command  
`docker build . -f FightCore.Backend/Dockerfile -t <tag>`  

There is also `docker-compose.yml` file in the solution folder,
please use following to build and run images `docker-compose up`  

To change any settings in for the container pass those as environment settings in the
`docker-compose.yml` file.  

You might have to configure SQL server as described in [this StackOverflow post](https://stackoverflow.com/a/50170217).

## Contributing

If anything is missing in your eyes please feel free to submit an issue or
even a pull request to add these features.
