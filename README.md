# Deploy a 3-Tier Web Application in AKS

## The Sample Application
- Backend (.NET 7) called Basic3TierAPI
- Frontend (HTML/CSS/JavaScript) served via Nginx, called Basic3TierUI
- PostgreSQL database

## Pre-requisites
1. Azure Subscription with an existing AKS cluster (with at least one node).
2. Azure CLI and kubectl installed and configured.
- Example: ```az login``` → ```az account set --subscription <subscription-id>``` → ```az aks get-credentials --resource-group <rg-name> --name <cluster-name>```
3. Docker Hub account to push your images.
4. (Optional) A StorageClass in AKS for persistent volumes (typically ```default``` or ```managed``` in Azure)

## Steps
1. Containerize the Services: .NET API Image, UI (Nginx) Image and official PostgreSQL Image
2. Creat deployment and service manifests for each Servic(backend, frontend and database) 
3. Deploy the 3tier application on AKS
4. Install/Verify Ingress Controller
5. Expose the Frontend with an Ingress Resource on AKS 
6. Verify: navigate to the Ingress endpoint (```kubectl get ingress ui-ingress```) in browser to verify that the Frontend is accessible

## Flow
Users access the frontend (Nginx) via an Ingress → the frontend calls the API service via an internal ClusterIP → the API connects to PostgreSQL.






