# MAC/Linux Setup
## Install Docker
```bash
sudo apt-get update
sudo apt-get install -y docker.io
sudo systemctl start docker
sudo systemctl enable docker
```

## Instalar Docker Compose v2 (as plugin only on servers)
```bash
sudo apt install -y docker-compose-plugin
```

### If installation fails

#### Install dependencies
```bash
sudo apt install -y ca-certificates curl gnupg lsb-release
```

#### Add Docker's official GCP key
```bash
sudo mkdir -p /etc/apt/keyrings 
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
```

#### Add Docker's repository
```bash
echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
```

#### Re-update and install again
```bash
sudo apt update
sudo apt install -y docker-ce docker-ce-cli containerd.io docker-compose-plugin
```

## Verify the installation of Docker
```bash
docker --version
docker compose version
```

## Verify Docker is running
```bash
sudo systemctl status docker
```

## Check running containers
```bash
docker ps
```

## View logs of a container
```bash
docker logs MSSQL
```

## Bring up the container with Docker Compose
```bash
docker compose -f docker-compose.sqlserver.yml up -d
```

# Windows Setup

## Windows (Native)

1. [Download and install Docker Desktop](https://www.docker.com/products/docker-desktop)
2. Enable WSL2 backend (recommended) during installation

## Change variable 
Replace variable ${SQL_SA_PASSWORD} with your own password in the `docker-compose.sqlserver.yml` file

## Bring up the container with Docker Compose
```bash
docker compose -f docker-compose.sqlserver.yml up -d
```