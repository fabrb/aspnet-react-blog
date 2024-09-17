compose up --build -d

.net 8

postgress fabarblog:
user: blogger
pass: 123456

docker compose up --build -d && docker logs blog-2024-test-backend-1 --follow
