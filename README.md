Rep_Crime_01

K8S launch:

Add in local file permission for domain: 
in C:\Windows\System32\drivers\etc\hosts add 127.0.0.1 rep-crime.com

Open a terminal in K8S folder and put commands:
kubect apply -f crime-depl.yaml
kubect apply -f police-depl.yaml
kubect apply -f mongo-depl.yaml
kubect apply -f rabbitmq-depl.yaml
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.1.1/deploy/static/provider/cloud/deploy.yaml
kubect apply -f ingress-srv.yaml

you can access your services in:
 http://rep-crime.com/api/crime [crime api]
 http://rep-crime.com/api/police [police api]