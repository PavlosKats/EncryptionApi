#!/bin/bash

# Create nginx proxy configuration
cat > /etc/nginx/conf.d/elasticbeanstalk/00_application.conf << 'NGINX_CONF'
location / {
    proxy_pass http://127.0.0.1:5000;
    proxy_http_version 1.1;
    proxy_set_header Upgrade $http_upgrade;
    proxy_set_header Connection keep-alive;
    proxy_set_header Host $host;
    proxy_cache_bypass $http_upgrade;
    proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
    proxy_set_header X-Forwarded-Proto $scheme;
}
NGINX_CONF

# Reload nginx
service nginx reload

echo "Nginx configuration updated and reloaded"
