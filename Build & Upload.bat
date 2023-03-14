docker build . --file Discite.Api\Dockerfile -t ghcr.io/jedlik-gyor/nagyilles-backend:1.0.2

docker push ghcr.io/jedlik-gyor/nagyilles-backend:1.0.2
docker rmi ghcr.io/jedlik-gyor/nagyilles-backend:1.0.2