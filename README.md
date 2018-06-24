# SampleApp
Simple .NET Core 2 API setup


- docker build -t sampleapi .
- docker run -d -p 8080:80 --name sample sampleapi

- http://localhost:8080/api/shows?page=0


- kubectl create -f pod.yml