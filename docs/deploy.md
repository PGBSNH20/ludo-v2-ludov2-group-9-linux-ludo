# Deployment

To the deploy our application, we planned to use docker cotnainers to deploy, and we succeded before a big merge was merged.
To deploy the application goto [Commit](https://github.com/PGBSNH20/ludo-v2-ludov2-group-9-linux-ludo/tree/963378a28b5d59c2b603c9cd7ca21976cb258ddb) and clone.
Go into the root folder and run `ocker-compose -f docker/docker-compose.yml up` this will deploy a "development" version o the app.

To deploy for production please copy `docker/docker-compose.prod.yml` and create a `.env` from `.env.example`, after that run `docker-compose up -d`.

To make our deployments better we had thoughts about using github actions to automate the builds, which would save alot of time and make it possible for dynamic updates.

In the current state the main branch is unstable.

Deployed on [ludo.nerdyhamster.net](https://ludo.nerdyhamster.net)
