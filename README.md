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

---

### Create an AKS Cluster using Azure CLI
0. Create the Resource Group:
```az group create --name <resource-group-name> --location <azure-region>```

1. check for supported versions:
```az aks get-versions --location <azure-region> --output table```

2. create
```
az aks create \
  --resource-group <resource-group-name> \
  --name <cluster-name> \
  --node-count 2 \
  --node-vm-size <Standard_DS2_v2> \
  --kubernetes-version <1.24.9> \
  --enable-addons monitoring \
  --enable-managed-identity
```
3. verify:
```az aks list -o table```

### Connect to Your AKS Cluster
1. Get Credentials:
```az aks get-credentials --resource-group <resource-group-name> --name <cluster-name> ```
</br>
(This command merges your AKS cluster credentials into your local kubeconfig file)

2. Verify your context with:
```kubectl config get-contexts```
3. Test Connectivity:
```kubectl get nodes```

You should see 2 nodes in a Ready state if everything is functioning correctly
### Cleanup
1. Remove the Resource Group:
```az group delete --name <resource-group-name> --yes --no-wait```

This deletes all resources in the group, including the AKS cluster and related resources.
The --no-wait parameter returns you to the console immediately while deletion continues in the background.

2. Verify Deletion:
```az group list --output table```








