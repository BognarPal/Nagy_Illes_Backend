docker build . --file Discite.Api\Dockerfile -t ghcr.io/jedlik-gyor/nagyilles-backend:1.0.0

docker push ghcr.io/jedlik-gyor/nagyilles-backend:1.0.0
docker rmi ghcr.io/jedlik-gyor/nagyilles-backend:1.0.0