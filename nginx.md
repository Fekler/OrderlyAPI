upstream backend {
    server localhost:9000;
}

server {
    listen 443 ssl http2;
    server_name salesmanagement-api.fekler.tec.br;
    ssl_certificate /etc/letsencrypt/live/salesmanagement-api.fekler.tec.br/fullchain.pem; # managed by Certbot
    ssl_certificate_key /etc/letsencrypt/live/salesmanagement-api.fekler.tec.br/privkey.pem; # managed by Certbot

    ssl_protocols TLSv1.2 TLSv1.3;
    ssl_ciphers HIGH:!aNULL:!MD5;

    location / {
        proxy_pass http://backend;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }

}

server {
    if ($host = salesmanagement-api.fekler.tec.br) {
        return 301 https://$host$request_uri;
    } # managed by Certbot


    listen 80;
    server_name salesmanagement-api.fekler.tec.br;
    return 301 https://$host$request_uri;


}
