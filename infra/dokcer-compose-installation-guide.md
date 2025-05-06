# Download last version of docker compose (only applied to linux or MAC)
```bash
sudo curl -L "https://github.com/docker/compose/releases/download/v2.20.2/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
```

# Give permissions
```bash
sudo chmod +x /usr/local/bin/docker-compose
```

# Verify if installation was successful
```bash
docker-compose --version
```